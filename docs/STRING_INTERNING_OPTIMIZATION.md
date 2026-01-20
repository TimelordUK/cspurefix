# String Interning Optimization for Generated Types

**Status:** Design idea - for discussion
**Goal:** Reduce string allocations for high-frequency repeated field values

## Problem

When parsing FIX messages, the generated code calls `view.GetString(tag)` for string fields. For fields like `BeginString`, `MsgType`, `SenderCompID`, etc., the values are repeated identically across thousands of messages, yet we allocate a new string each time.

Example from `Components/StandardHeader.cs`:
```csharp
void IFixParser.Parse(IMessageView? view)
{
    if (view is null) return;

    BeginString = view.GetString(8);      // Always "FIX.4.4" - allocates every time
    BodyLength = view.GetInt32(9);
    MsgType = view.GetString(35);         // Small set: "A", "D", "8", etc.
    SenderCompID = view.GetString(49);    // Same for entire session
    TargetCompID = view.GetString(56);    // Same for entire session
    SenderSubID = view.GetString(50);     // Usually same per session
    TargetSubID = view.GetString(57);     // Usually same per session
    // ...
}
```

At 100k messages/day (common on futures rollover), that's 100k allocations of "FIX.4.4", 100k allocations of the same SenderCompID, etc.

---

## Prerequisite: Parser-Per-Session Architecture

### The Current Problem

Before implementing string interning, we need to address a more fundamental issue: **the parser is a singleton shared across all sessions within an acceptor**.

```
Acceptor (1 DI Container)
├── AsciiParser (SINGLETON - shared!)
│   └── _state (AsciiParseState) ← NOT THREAD-SAFE
│
├── Session A (Client 1) ─┐
├── Session B (Client 2) ─┼── All call m_parser.ParseFrom()
├── Session C (Client 3) ─┘   on the SAME parser instance
```

In `BaseAppDI.cs`:
```csharp
_builder.Services.AddSingleton<IMessageParser>(sp => {
    var cfg = sp.GetRequiredService<IFixConfig>();
    return new AsciiParser(cfg.Definitions!) { Delimiter = cfg.Delimiter };
});
```

And in session factories like `TradeCaptureSessionFactory`:
```csharp
public FixSession MakeSession()
{
    return new TradeCaptureServer(_config, ..., _parser, _encoder);  // SAME _parser instance
}
```

### Why This Is a Problem

1. **Thread safety**: If two clients send data simultaneously, their Reader tasks could call `ParseFrom()` concurrently, corrupting `_state.ParseState`, `_state.Buffer`, etc.

2. **String store injection**: We can't add `_currentStringStore` to the parser because it would be shared/overwritten by different sessions.

3. **Stats pollution**: Parser stats (`_receivedBytes`, `_parsedMessages`) are aggregated across all sessions - not useful for per-session diagnostics.

### Solution: Parser-Per-Session

Each session should get its own parser instance. The parser is lightweight:

| Component | Memory | Shareable? |
|-----------|--------|------------|
| `_state` (AsciiParseState) | ~200 bytes | No - has mutable parse state |
| `_pool` (StoragePool) | ~1KB | Could share, but simpler not to |
| `_segmentParser` | ~100 bytes | Yes - stateless, just definitions |
| `Definitions` | Reference | Yes - immutable |

**Recommended changes:**

```csharp
// 1. Session factory creates parser per session
public FixSession MakeSession()
{
    var parser = new AsciiParser(_definitions) { Delimiter = _delimiter };
    var stringStore = new SessionStringStore();

    return new TradeCaptureServer(_config, ..., parser, stringStore);
}

// 2. Remove singleton registration from DI (or make it Transient)
// In BaseAppDI, change to factory pattern or remove registration entirely
```

### Benefits

1. **Thread isolation**: No race conditions between sessions
2. **Clean string store injection**: Each parser knows its session's store
3. **Per-session stats**: Parser stats are meaningful per-session
4. **Simplified lifecycle**: Parser dies with session, no cleanup needed

### Migration Path

This is a **non-breaking change** for most users:
- Initiators already have one session per container - no change
- Acceptors need factory update to create parser per session
- Memory impact is minimal (~1-2KB per session)

---

## Analysis: Why Simple Interning Doesn't Work

### The Flaw in Naive Approaches

Options like `string.Intern(GetString(tag))` or dictionary-based pools that key on strings have a critical flaw: **they allocate a temporary string first, then intern it**.

```csharp
// BAD - still allocates before interning
public string? GetInternString(int tag)
{
    var value = GetString(tag);  // ← ALLOCATES HERE (24-40 bytes)
    return value != null ? string.Intern(value) : null;  // Too late!
}
```

This defeats the purpose. We need to go **directly from bytes to interned string** without any intermediate allocation.

### What We Already Have: MsgType Pattern

`PureFix.Types.Core/MsgType.cs` already solves this correctly:

```csharp
public static string Intern(ReadOnlySpan<byte> bytes)
{
    return bytes.Length switch
    {
        1 => InternSingle(bytes[0]),      // Switch on byte → const string
        2 => InternDouble(bytes[0], bytes[1]),
        _ => Encoding.ASCII.GetString(bytes)  // Fallback allocates
    };
}

private static string InternSingle(byte b) => b switch
{
    (byte)'0' => Heartbeat,      // const string "0"
    (byte)'A' => Logon,          // const string "A"
    (byte)'D' => NewOrderSingle, // const string "D"
    (byte)'8' => ExecutionReport,// const string "8"
    // ... 150+ patterns
    _ => ((char)b).ToString()
};
```

This is **zero-allocation** for known values - switch on raw bytes, return pre-allocated const strings.

---

## Two Categories of Internable Strings

### Category 1: Known Finite Sets (Global, Static)

Fields where the complete set of values is known at compile time:

| Tag | Field | Values | Strategy |
|-----|-------|--------|----------|
| 8 | BeginString | ~5 | Static switch like MsgType |
| 35 | MsgType | ~150 | Already implemented |
| 22 | SecurityIDSource | ~10 | Static switch |

These use **static pools** with byte-pattern matching. Zero allocation, zero lookup cost.

### Category 2: Session Constants (Per-Session, Runtime)

Fields that are constant for a session but unknown at compile time:

| Tag | Field | Cardinality | Strategy |
|-----|-------|-------------|----------|
| 49 | SenderCompID | 1 per session | Session string store |
| 56 | TargetCompID | 1 per session | Session string store |
| 50 | SenderSubID | 1 per session | Session string store |
| 57 | TargetSubID | 1 per session | Session string store |

These use a **per-session byte-keyed store**. First access allocates, subsequent accesses return the same instance.

### Category 3: Limited Runtime Sets (Future Enhancement)

Fields with bounded but runtime-determined value sets:

| Tag | Field | Example Values |
|-----|-------|----------------|
| 167 | SecurityType | FXSWAP, OPTION, FORWARD |
| 54 | Side | 1, 2, 3, 4, 5, 6 |

Could use session store or global pool depending on cardinality.

---

## Architecture: Session String Store

### Design Principles

1. **Key on bytes, not strings** - avoid allocating to check if value exists
2. **Per-session lifecycle** - store dies with session, no cross-session pollution
3. **Transparent to Parse** - optimization hidden in view layer
4. **Observable** - hit counts for debugging/tuning

### Core Interface

```csharp
public interface ISessionStringStore
{
    /// <summary>
    /// Returns interned string for the given bytes. First call allocates,
    /// subsequent calls return the same instance.
    /// </summary>
    string GetOrAdd(int tag, ReadOnlySpan<byte> bytes);

    /// <summary>
    /// Clears all interned strings. Called on session stop.
    /// </summary>
    void Clear();

    /// <summary>
    /// Returns statistics for observability.
    /// </summary>
    SessionStringStoreStats GetStats();
}

public readonly struct SessionStringStoreStats
{
    public int TotalTags { get; init; }
    public int TotalStrings { get; init; }
    public long TotalHits { get; init; }
    public long TotalMisses { get; init; }
    public IReadOnlyDictionary<int, TagStats> PerTagStats { get; init; }
}

public readonly struct TagStats
{
    public int UniqueValues { get; init; }
    public long Hits { get; init; }
    public long Misses { get; init; }
}
```

### Implementation

```csharp
public class SessionStringStore : ISessionStringStore
{
    // Per-tag storage: tag → list of (byte key, string value)
    // Using arrays for cache-friendly iteration (typically 1-2 entries per tag)
    private readonly Dictionary<int, List<(byte[] Key, string Value)>> _store = new();

    // Stats tracking
    private readonly Dictionary<int, (long hits, long misses)> _stats = new();
    private readonly object _lock = new();

    public string GetOrAdd(int tag, ReadOnlySpan<byte> bytes)
    {
        lock (_lock)
        {
            if (!_store.TryGetValue(tag, out var entries))
            {
                entries = new List<(byte[], string)>(capacity: 2);
                _store[tag] = entries;
                _stats[tag] = (0, 0);
            }

            // Check existing entries (typically 0-2 iterations)
            foreach (var (key, value) in entries)
            {
                if (bytes.SequenceEqual(key))
                {
                    // HIT - return existing string
                    var s = _stats[tag];
                    _stats[tag] = (s.hits + 1, s.misses);
                    return value;
                }
            }

            // MISS - allocate once, store for future
            var newKey = bytes.ToArray();
            var newValue = Encoding.ASCII.GetString(bytes);
            entries.Add((newKey, newValue));

            var stats = _stats[tag];
            _stats[tag] = (stats.hits, stats.misses + 1);

            return newValue;
        }
    }

    public void Clear()
    {
        lock (_lock)
        {
            _store.Clear();
            _stats.Clear();
        }
    }

    public SessionStringStoreStats GetStats()
    {
        lock (_lock)
        {
            var perTag = _stats.ToDictionary(
                kvp => kvp.Key,
                kvp => new TagStats
                {
                    UniqueValues = _store.TryGetValue(kvp.Key, out var e) ? e.Count : 0,
                    Hits = kvp.Value.hits,
                    Misses = kvp.Value.misses
                });

            return new SessionStringStoreStats
            {
                TotalTags = _store.Count,
                TotalStrings = _store.Values.Sum(v => v.Count),
                TotalHits = _stats.Values.Sum(v => v.hits),
                TotalMisses = _stats.Values.Sum(v => v.misses),
                PerTagStats = perTag
            };
        }
    }
}
```

### Lock-Free Alternative (If Contention Is an Issue)

```csharp
public class LockFreeSessionStringStore : ISessionStringStore
{
    // Use immutable snapshots with Interlocked.CompareExchange
    private volatile ImmutableDictionary<int, ImmutableArray<(byte[] Key, string Value)>> _store
        = ImmutableDictionary<int, ImmutableArray<(byte[] Key, string Value)>>.Empty;

    public string GetOrAdd(int tag, ReadOnlySpan<byte> bytes)
    {
        var snapshot = _store;

        // Fast path: check existing
        if (snapshot.TryGetValue(tag, out var entries))
        {
            foreach (var (key, value) in entries)
            {
                if (bytes.SequenceEqual(key)) return value;
            }
        }

        // Slow path: add new (rare, only on first occurrence)
        // ... CAS loop to update immutable dictionary
    }
}
```

---

## Session Injection Mechanism

### Target Architecture (Parser-Per-Session)

With the parser-per-session change, injection becomes straightforward:

```
Session Factory                                        View Pool (Static)
      │                                                      │
      ▼                                                      ▼
┌─────────────┐                                       ┌────────────┐
│ MakeSession │──creates per TCP connection──────────▶│ AsciiView  │
│             │                                       │  (pooled)  │
└─────────────┘                                       └────────────┘
      │                                                      │
      ├── AsciiParser (owned by session)                     │
      ├── SessionStringStore (owned by session)              │
      └── Parser holds reference to store                    │
                    │                                        │
                    ▼                                        ▼
              Parser.GetView() ──passes store──▶ AsciiView.Rent(stringStore)
                                                             │
                                                             ▼
                                              view.GetString(tag) uses store
```

**Key insight:** When parser is per-session, it can directly hold the string store reference. No need to pass it through callbacks.

### Injection Flow

```
Session.OnRx()
    │
    ▼
m_parser.ParseFrom(buffer, len, onView, onDecode)
    │
    ├── parser owns ISessionStringStore (set at construction)
    │
    ▼
Parser.GetView() → AsciiView.Rent(..., _stringStore)
    │
    ├── view holds reference during rental
    │
    ▼
view.GetString(tag) → checks if tag is internable → uses store
```

### Changes Required

#### 1. ISessionStringStore Interface (new file)

```csharp
// PureFix.Types/ISessionStringStore.cs
public interface ISessionStringStore { ... }
```

#### 2. AsciiView Changes

```csharp
// PureFix.Buffer/Ascii/AsciiView.cs
public class AsciiView : MsgView
{
    private ISessionStringStore? _stringStore;

    // Updated Rent signature
    internal static AsciiView Rent(
        IFixDefinitions definitions,
        StoragePool.Storage storage,
        int ptr, int delimiter, int writeDelimiter,
        AsciiSegmentParser? segmentParser,
        string? msgType,
        ISessionStringStore? stringStore = null)  // NEW parameter
    {
        var view = Pool.Get();
        view.Initialize(...);
        view._stringStore = stringStore;
        return view;
    }

    public override void Reset()
    {
        base.Reset();
        _stringStore = null;  // Don't hold reference after return
    }

    // Interning tags (could be configurable)
    private static readonly HashSet<int> InternableTags = new() { 49, 56, 50, 57 };

    protected override string? StringAtPosition(int position)
    {
        var tagPos = GetTag(position);
        if (tagPos == null) return null;

        var tag = Tags[position].Tag;
        var start = tagPos.Value.Start;
        var len = tagPos.Value.Len;

        // Session constants: use store
        if (_stringStore != null && InternableTags.Contains(tag))
        {
            var span = Buffer.GetSpan(start, start + len);
            return _stringStore.GetOrAdd(tag, span);
        }

        // Known finite sets: use static pools
        if (tag == 8)  // BeginString
        {
            var span = Buffer.GetSpan(start, start + len);
            return BeginStringPool.Intern(span);
        }

        // Default: allocate
        return Buffer.GetString(start, start + len);
    }
}
```

#### 3. AsciiParser Changes

With parser-per-session, the parser owns the string store directly:

```csharp
// PureFix.Buffer/Ascii/AsciiParser.cs
public class AsciiParser : IMessageParser
{
    // NEW: Parser owns the string store (set at construction or via property)
    public ISessionStringStore? StringStore { get; set; }

    // Existing constructor unchanged, add overload or setter
    public AsciiParser(IFixDefinitions definitions, ISessionStringStore? stringStore = null)
    {
        Definitions = definitions;
        StringStore = stringStore;
        _state = new AsciiParseState(definitions, _pool);
        _segmentParser = new AsciiSegmentParser(Definitions);
        _state.BeginMessage();
    }

    // ParseFrom signature UNCHANGED - no need to pass store through
    public void ParseFrom(ReadOnlySpan<byte> readFrom, int end,
                          Action<int, MsgView>? onView,
                          Action<StoragePool.Storage>? onDecode = null)
    {
        // ... existing code unchanged ...
    }

    private AsciiView? GetView(int ptr)
    {
        // ... existing checks ...
        return AsciiView.Rent(
            Definitions,
            _state.Storage,
            ptr,
            Delimiter,
            WriteDelimiter,
            _segmentParser,
            _state.MsgType,
            StringStore);  // Pass the parser's store to the view
    }
}
```

**Note:** The `ParseFrom` signature is unchanged - the store flows through the parser's owned reference, not through method parameters. This is cleaner and avoids API changes.

#### 4. FixSession Changes

With parser-per-session, FixSession changes are minimal. The parser owns the string store, so FixSession just needs to log stats on shutdown:

```csharp
// PureFix.Transport/Session/FixSession.cs
public abstract class FixSession
{
    // OnRx UNCHANGED - just calls m_parser.ParseFrom() as before
    public async Task OnRx(byte[] buffer, int len)
    {
        _messages.Clear();
        m_parser.ParseFrom(buffer, len, (_, v) => _messages.Add(v), OnFixLog);
        // ... rest unchanged ...
    }

    protected override void OnSessionStopping()
    {
        base.OnSessionStopping();

        // Log stats before clearing (parser owns the store)
        if (m_parser is AsciiParser { StringStore: { } store })
        {
            var stats = store.GetStats();
            m_log?.Info($"Session string store: {stats.TotalStrings} strings, " +
                       $"{stats.TotalHits} hits, {stats.TotalMisses} misses");
            store.Clear();
        }
    }
}
```

#### 5. Session Factory Changes

The factory creates both parser and string store per session:

```csharp
// In your session factory (e.g., TradeCaptureSessionFactory)
public class TradeCaptureSessionFactory : ISessionFactory
{
    private readonly IFixDefinitions _definitions;
    private readonly byte _delimiter;
    // ... other dependencies (config, recovery, etc.)

    // NOTE: Do NOT hold a shared _parser field anymore

    public FixSession MakeSession()
    {
        // Create fresh parser and store per session
        var stringStore = new SessionStringStore();
        var parser = new AsciiParser(_definitions, stringStore)
        {
            Delimiter = _delimiter
        };

        return new TradeCaptureServer(_config, _recovery, _logFactory,
            _messageFactory, parser, _encoder, _clock);
    }
}
```

**Key change:** The factory no longer holds a `_parser` field. Each `MakeSession()` call creates a fresh parser with its own string store.

---

## Lifecycle and Isolation

### Acceptor with Multiple Sessions

```
Client A connects ──▶ TcpAcceptorListener ──▶ MakeSession() ──▶ Session A
                                                                    │
                                                            owns StringStore A

Client B connects ──▶ TcpAcceptorListener ──▶ MakeSession() ──▶ Session B
                                                                    │
                                                            owns StringStore B
```

Each session has its own string store. No cross-contamination possible.

### Session Restart / Reconnection

```
Client A connects    ──▶ Session A1 created (StringStore A1)
Client A disconnects ──▶ Session A1 stopped  (StringStore A1 cleared)
Client A reconnects  ──▶ Session A2 created (StringStore A2 - fresh)
```

The `SessionRegistry` handles this:
1. New connection creates new session with fresh string store
2. If same SessionId reconnects, old session is stopped (store cleared)
3. New session takes over with its own fresh store

### Field Changes During Development

If a client changes their SenderCompID during testing:
- Old session dies (store cleared)
- New session gets new CompID stored fresh

No "stale" values persist. The store is purely transient.

---

## Static Pools for Known Finite Sets

### BeginStringPool

```csharp
// PureFix.Types.Core/BeginStringPool.cs
public static class BeginStringPool
{
    public const string Fix40 = "FIX.4.0";
    public const string Fix41 = "FIX.4.1";
    public const string Fix42 = "FIX.4.2";
    public const string Fix43 = "FIX.4.3";
    public const string Fix44 = "FIX.4.4";
    public const string Fix50 = "FIXT.1.1";
    public const string Fix50SP2 = "FIX.5.0SP2";

    public static string Intern(ReadOnlySpan<byte> bytes)
    {
        // Fast path: check length first
        if (bytes.Length == 7)
        {
            // "FIX.4.X" pattern
            if (bytes[0] == 'F' && bytes[4] == '4')
            {
                return bytes[6] switch
                {
                    (byte)'0' => Fix40,
                    (byte)'1' => Fix41,
                    (byte)'2' => Fix42,
                    (byte)'3' => Fix43,
                    (byte)'4' => Fix44,
                    _ => Encoding.ASCII.GetString(bytes)
                };
            }
        }
        else if (bytes.Length == 8 && bytes.SequenceEqual("FIXT.1.1"u8))
        {
            return Fix50;
        }
        else if (bytes.Length == 10 && bytes.SequenceEqual("FIX.5.0SP2"u8))
        {
            return Fix50SP2;
        }

        // Unknown - allocate (should be rare/never in production)
        return Encoding.ASCII.GetString(bytes);
    }
}
```

---

## Observability: Hit Count Tracking

The stats help tune which tags to intern:

```csharp
// After session stops, log like:
Session string store stats:
  Tag 49 (SenderCompID): 1 unique, 100,000 hits, 1 miss
  Tag 56 (TargetCompID): 1 unique, 100,000 hits, 1 miss
  Tag 50 (SenderSubID):  1 unique, 50,000 hits, 1 miss
  Tag 57 (TargetSubID):  1 unique, 50,000 hits, 1 miss
  Total: 4 strings cached, 300,000 allocations saved
```

This tells us:
- CompID interning saved 200k allocations
- The optimization is working as expected
- Could extend to other high-hit tags

---

## Performance Considerations

| Operation | Cost | Notes |
|-----------|------|-------|
| Current `GetString()` | ~10ns + 24B alloc | string.Create |
| Static pool lookup | ~3-5ns, 0B | Switch on bytes |
| Session store hit | ~15-20ns, 0B | Lock + span compare |
| Session store miss | ~50ns + alloc | Lock + copy + allocate (once per session) |

For 100k messages with 4 interned header fields:
- **Current:** 400k allocations, ~10MB heap churn
- **With interning:** 4 allocations (first message), 0 after

The lock contention is negligible because:
1. FIX sessions are single-threaded (one reader per session)
2. Even if shared, the critical section is ~20 instructions

---

## What Stays Hidden

The Parse method in generated types remains unchanged:

```csharp
void IFixParser.Parse(IMessageView? view)
{
    if (view is null) return;

    BeginString = view.GetString(8);    // ← Same call, but now may return interned
    MsgType = view.GetString(35);       // ← Already uses MsgType.Intern internally
    SenderCompID = view.GetString(49);  // ← Uses session store if available
    // ...
}
```

**No generator changes needed.** The optimization is entirely in the view layer.

---

## Implementation Order

### Phase 0: Parser-Per-Session (Prerequisite)
1. Update session factories to create parser per session (not hold shared reference)
2. Remove/change singleton IMessageParser registration from DI
3. **Validate:** Run demo with 2 initiators → 1 acceptor, confirm no cross-session pollution
4. Update existing example factories (TradeCaptureSessionFactory, etc.)

### Phase 1: Foundation
5. Add `ISessionStringStore` interface and `SessionStringStore` implementation
6. Add `BeginStringPool` static class
7. Add `GetSpan(start, end)` to ElasticBuffer if not present

### Phase 2: Wiring
8. Add `StringStore` property to `AsciiParser`
9. Update `AsciiView.Rent()` to accept string store
10. Update `AsciiParser.GetView()` to pass store to view
11. Update `AsciiView.StringAtPosition()` to use store for internable tags

### Phase 3: Session Factory Integration
12. Update session factories to create string store alongside parser
13. Add stats logging on session stop in `FixSession.OnSessionStopping()`

### Phase 4: Validation
14. Add benchmarks comparing before/after allocations
15. Profile in realistic scenario (100k message replay)
16. Validate multi-client acceptor with string store isolation

---

## Open Questions

1. **Which tags to intern by default?** Current proposal: 8, 49, 56, 50, 57. Make configurable?

2. **Lock-free vs locked store?** Locked is simpler, FIX sessions are single-threaded anyway.

3. **Should MsgType routing use the store?** Currently uses `MsgType.Intern()` directly. Could unify.

4. **Extend to body fields?** Tag 167 (SecurityType) etc. - add in Phase 2 after validating header works.

---

*Design sketch for future implementation. Details may change based on profiling results.*
