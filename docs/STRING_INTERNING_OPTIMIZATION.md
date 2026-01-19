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

## Proposed Solution

### 1. Optimization Hint File

Create a metadata file alongside the FIX dictionary XML that specifies which tags should use interned strings.

**File:** `Data/FIX44.optimization.json` (alongside `Data/FIX44.xml`)

```json
{
  "version": "1.0",
  "internStrings": {
    "tags": [8, 35, 49, 50, 56, 57],
    "description": "High-frequency repeated values - intern to reduce allocations"
  }
}
```

### 2. Generator Changes

The generator (`PureFix.App`) reads the optimization file and emits different code for specified tags:

**Before (current):**
```csharp
BeginString = view.GetString(8);
MsgType = view.GetString(35);
SenderCompID = view.GetString(49);
```

**After (with interning):**
```csharp
BeginString = view.GetInternString(8);
MsgType = view.GetInternString(35);
SenderCompID = view.GetInternString(49);
```

### 3. IMessageView Extension

Add `GetInternString` method to the view interface:

```csharp
public interface IMessageView
{
    // Existing
    string? GetString(int tag);

    // New - returns interned string
    string? GetInternString(int tag);
}
```

### 4. Implementation Options

#### Option A: Use `string.Intern()`

Simple, uses CLR's built-in intern pool:

```csharp
public string? GetInternString(int tag)
{
    var value = GetString(tag);
    return value != null ? string.Intern(value) : null;
}
```

**Pros:** Simple, no custom code
**Cons:** Intern pool never shrinks, strings live forever

#### Option B: Custom Intern Pool

Per-session or global pool with controlled lifetime:

```csharp
public class StringInternPool
{
    private readonly ConcurrentDictionary<string, string> _pool = new();

    public string Intern(string value)
    {
        return _pool.GetOrAdd(value, value);
    }

    public void Clear() => _pool.Clear();
}
```

**Pros:** Can clear pool on session reset, bounded memory
**Cons:** More code, need to manage pool lifetime

#### Option C: Hybrid - Small Fixed Pool for Known Values

Pre-populate pool with known values (MsgTypes, BeginStrings):

```csharp
public static class FixStringPool
{
    private static readonly Dictionary<string, string> _known = new()
    {
        // BeginStrings
        ["FIX.4.2"] = "FIX.4.2",
        ["FIX.4.4"] = "FIX.4.4",
        ["FIX.5.0SP2"] = "FIX.5.0SP2",

        // Common MsgTypes
        ["A"] = "A",   // Logon
        ["0"] = "0",   // Heartbeat
        ["1"] = "1",   // TestRequest
        ["2"] = "2",   // ResendRequest
        ["5"] = "5",   // Logout
        ["D"] = "D",   // NewOrderSingle
        ["8"] = "8",   // ExecutionReport
        ["AE"] = "AE", // TradeCaptureReport
        // etc.
    };

    public static string Intern(string value)
    {
        return _known.TryGetValue(value, out var interned) ? interned : value;
    }
}
```

**Pros:** Zero allocation for known values, no pool growth
**Cons:** Unknown values still allocate, need to maintain list

## Prime Candidates for Interning

| Tag | Field | Cardinality | Notes |
|-----|-------|-------------|-------|
| 8 | BeginString | ~5 values | FIX.4.0, FIX.4.2, FIX.4.4, FIX.5.0, FIX.5.0SP2 |
| 35 | MsgType | ~50 values | A, D, 8, AE, etc. - all defined in dictionary |
| 49 | SenderCompID | 1 per session | Same for all messages |
| 56 | TargetCompID | 1 per session | Same for all messages |
| 50 | SenderSubID | 1 per session | Usually same |
| 57 | TargetSubID | 1 per session | Usually same |
| 22 | SecurityIDSource | ~10 values | 1, 2, 4, 5, etc. |
| 48 | SecurityID | Varies | Could be high cardinality - maybe skip |

## File Structure

```
Data/
├── FIX44.xml                    # QuickFix dictionary
├── FIX44.optimization.json      # Interning hints (optional)
├── FIX50SP2.xml
└── FIX50SP2.optimization.json
```

## Generator Integration

```csharp
// In generator, when emitting Parse method:
var optimization = LoadOptimizationFile(dictionaryPath);
var internTags = optimization?.InternStrings?.Tags ?? Array.Empty<int>();

foreach (var field in messageFields)
{
    if (field.Type == "string" && internTags.Contains(field.Tag))
    {
        // Emit interned version
        writer.WriteLine($"{field.Name} = view.GetInternString({field.Tag});");
    }
    else
    {
        // Emit normal version
        writer.WriteLine($"{field.Name} = view.GetString({field.Tag});");
    }
}
```

## Metrics to Track

Before implementing, measure:
1. Current string allocations per message (dotMemory or similar)
2. Allocation rate during steady-state message flow
3. GC pressure from short-lived strings

After implementing:
1. Reduction in allocations
2. Memory pool size (if using custom pool)
3. Any performance impact from interning lookup

## Open Questions

1. **Pool scope:** Global singleton vs per-session pool?
2. **Unknown values:** Intern all strings or only known values?
3. **Backwards compatibility:** Make interning opt-in via config?
4. **Enum fields:** Should enum string values also be interned?

## Next Steps

1. Profile current allocation patterns to quantify the problem
2. Decide on implementation approach (A, B, or C)
3. Implement `GetInternString` in `AsciiView`
4. Update generator to read optimization hints
5. Measure improvement

---

*Design sketch for future implementation. Details may change based on profiling results.*
