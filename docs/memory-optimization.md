# Memory Optimization Architecture

Technical reference for PureFix's memory optimization strategy.

## Three-Stage Parsing Architecture

```
Raw FIX Bytes
    │
    ▼
┌─────────────────────────────────────────────────────────────┐
│ Stage 1: Tokenization (always runs)                         │
│ • State machine scans bytes → Tags[] stored in Storage      │
│ • Storage is pooled (ElasticBuffer + Tags)                  │
│ • Creates lightweight AsciiView (256B allocation)           │
│ • O(n) linear scan, no dictionaries built                   │
└─────────────────────────────────────────────────────────────┘
    │
    ▼
┌─────────────────────────────────────────────────────────────┐
│ Stage 2: Structure Parsing (on-demand only)                 │
│ • Triggered by: GetView(), GetGroupInstance(), GroupCount() │
│ • NOT triggered by: GetString(), GetInt32(), GetDouble()    │
│ • Uses pooled TagIndex + Context objects                    │
│ • Builds segment tree + sorted tag indexes                  │
│ • Allocation: 1-40 KB depending on complexity               │
└─────────────────────────────────────────────────────────────┘
    │
    ▼
┌─────────────────────────────────────────────────────────────┐
│ Stage 3: Field Extraction (typed messages)                  │
│ • Extracts values into strongly-typed message objects       │
│ • Message objects can be pooled via Reset()                 │
│ • Allocation: 40-1480B new, 0B pooled                       │
└─────────────────────────────────────────────────────────────┘
```

## Key Design Decisions

### Lazy Structure Parsing (commit 879af247)

**Problem**: Previously, every message triggered full structural analysis (dictionaries, segment trees) even for simple session messages like Heartbeat.

**Solution**: Structure parsing is deferred until actually needed. Simple field access like `view.GetString(tag)` uses O(n) linear scan through tags.

**When linear scan wins**:
- Session messages (Heartbeat ~10 fields, Logon ~22 fields)
- Drop copy handlers accessing 2-3 fields
- Routing/filtering based on few fields
- Break-even point is roughly ~20 fields

**Code path** (`MsgView.cs:189-208`):
```csharp
protected int GetPosition(int tag)
{
    // Fast path: indexed lookup if Structure is available
    if (Structure != null) { ... indexed lookup ... }

    // Slow path: linear scan - no allocation overhead
    return LinearScanForTag(tag);
}
```

### Object Pooling Strategy

| Pool | Type | Max Retained | Key Optimization |
|------|------|--------------|------------------|
| StoragePool | Storage | Unbounded | ElasticBuffer + Tags reused |
| ArrayPool | byte[] | ~1MB total | Shared .NET pool |
| ArrayPool | TagPos[] | Shared | For sorted tag arrays |
| ObjectPool | TagIndex | 16 | Dictionaries retain capacity |
| ObjectPool | Context | 16 | Stacks/lists retain capacity |

### Capacity Retention Pattern

**Critical insight**: `Dictionary.Clear()` retains internal bucket arrays. This avoids reallocation when pooled objects are reused.

```csharp
// TagIndex.cs:71-95
public void Dispose()
{
    // Return array to pool
    ArrayPool.Return(_sortedTagPosForwards);

    // Clear dictionaries (keeps capacity for reuse!)
    _tagSpans.Clear();
    _noOfTag2NoOfPos.Clear();
    _tag2delim.Clear();
    // ... etc

    // Return self to object pool
    ObjectPool.Return(this);
}
```

## Pooling Hierarchy

```
StoragePool (1 per AsciiParser)
├── Storage
│   ├── ElasticBuffer → byte[] from ArrayPool<byte>
│   └── Tags → TagPos[] (grows by 2x, never shrinks)
│
TagIndex ObjectPool (global, max 16)
├── _sortedTagPosForwards → TagPos[] from ArrayPool<TagPos>
├── _tagSpans → Dictionary<int, Range> (capacity retained)
├── _noOfTag2NoOfPos → Dictionary<int, TagPos>
├── _tag2delim → Dictionary<int, int>
├── _groups → Dictionary<string, GroupFieldDefinition>
└── ... other dictionaries

Context ObjectPool (global, max 16)
├── Segment stack
└── Position state lists
```

## Performance Results

### Stage 1: Tokenization (buffer to view)

| Message Type | Fields | Size | Time | Allocated (pooled) |
|--------------|--------|------|------|-------------------|
| Heartbeat | ~10 | 131 B | 645 ns | 112 B |
| Logon | ~22 | 214 B | 1.0 μs | 112 B |
| QuoteRequest | ~30 | 334 B | 1.5 μs | 112 B |
| OrderCancelReject | ~370 | 3.9 KB | 17 μs | 1.2 KB |
| ExecutionReport | ~646 | 6.6 KB | 29 μs | 3.2 KB |

*Storage and views are pooled and reused. Remaining allocation is from delegate/closure in benchmark.*

### Stage 2: Field Extraction (view to typed message)

| Message Type | Extract Time | New Allocation | Pooled Allocation |
|--------------|--------------|----------------|-------------------|
| Heartbeat | 4.7 ns | 40 B | 0 B |
| Logon | 5.9 ns | 112 B | 0 B |
| QuoteRequest | 5.7 ns | 96 B | 0 B |
| OrderCancelReject | 7.6 ns | 216 B | 0 B |
| ExecutionReport | 27 ns | 1,480 B | 0 B |

### Lazy Parsing Impact

**Before** (always eager structure parsing):
- Heartbeat: 1.7 μs, 2.1 KB allocation
- ExecutionReport: 102 μs, 40 KB allocation

**After** (lazy, on-demand):
- Heartbeat: 0.72 μs, 1.3 KB allocation (2.4x faster)
- ExecutionReport: 30.8 μs, 3.4 KB allocation (3.3x faster)

## Commit History

| Commit | Optimization | Heartbeat Alloc | ExecutionReport Alloc |
|--------|--------------|-----------------|----------------------|
| da51dc3d | Pool TagIndex | 7.9 KB → 3.4 KB | 133 KB → 100 KB |
| 44d1b0b9 | Pool Context | 3.4 KB → 3.2 KB | 100 KB → 98 KB |
| 879af247 | Lazy structure | 2.1 KB → 1.3 KB | 40 KB → 3.4 KB |
| 4b50113f | Fix benchmarks | (methodology fix) | (methodology fix) |

## Zero-Allocation View Access API

For scenarios where you only need to probe a few fields (routing, filtering, validation), the span-based API eliminates all per-field allocation:

| Pattern | String API | Span API | Speed | Allocation |
|---------|-----------|----------|-------|------------|
| Single field check | 11.2 ns / 24 B | 8.7 ns / 0 B | **23% faster** | **0 B** |
| Routing (3 values) | 11.3 ns / 24 B | 9.2 ns / 0 B | **18% faster** | **0 B** |
| Prefix check | 22.0 ns / 40 B | 9.3 ns / 0 B | **57% faster** | **0 B** |
| Two-field validation | 26.6 ns / 64 B | 14.5 ns / 0 B | **45% faster** | **0 B** |

```csharp
// Zero-allocation message routing
if (view.IsTagEqual(35, "AE"u8))
{
    // Trade Capture Report - extract only what we need
    var tradeId = view.GetSpan(571);  // zero allocation
}

// Multi-value routing without string allocation
switch (view.MatchTag(35, "0"u8, "A"u8, "5"u8))
{
    case 0: HandleHeartbeat(view); break;
    case 1: HandleLogon(view); break;
    case 2: HandleLogout(view); break;
}

// Validate without allocating strings
if (view.IsTagEqual(35, "A"u8) && view.TagStartsWith(8, "FIX"u8))
{
    // valid FIX logon
}
```

See [msgview-span-api.md](msgview-span-api.md) for complete API reference.
