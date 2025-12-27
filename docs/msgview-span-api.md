# MsgView Zero-Allocation Span API

**Date:** 2024-12-27
**Status:** Complete - Tests Passing

## Overview

Enhanced MsgView with a zero-allocation API for probing FIX message buffers without string allocation. This allows routing, filtering, and field access patterns that avoid heap allocations entirely.

## Benchmark Results

*AMD Ryzen 9 7950X, .NET 9.0.4, Ubuntu 22.04 WSL*

| Pattern | String API | Span API | Speed | Allocation |
|---------|-----------|----------|-------|------------|
| Single field check | 11.2 ns / 24 B | 8.7 ns / 0 B | **23% faster** | **0 B** |
| Routing (3 values) | 11.3 ns / 24 B | 9.2 ns / 0 B | **18% faster** | **0 B** |
| Prefix check (`StartsWith`) | 22.0 ns / 40 B | 9.3 ns / 0 B | **57% faster** | **0 B** |
| Two-field validation | 26.6 ns / 64 B | 14.5 ns / 0 B | **45% faster** | **0 B** |
| Raw span access | 11.2 ns / 24 B | 6.0 ns / 0 B | **46% faster** | **0 B** |

## Motivation

Previously, checking a message type required:
```csharp
if (view.GetString(35) == "AE") { ... }  // allocates 24 bytes
```

Now possible:
```csharp
if (view.IsTagEqual(35, "AE"u8)) { ... }  // zero allocation, 23% faster
```

## New API Summary

### Span-Based Comparison

```csharp
// Direct byte comparison - zero allocation
bool match = view.IsTagEqual(35, "AE"u8);

// String overload - uses stackalloc internally
bool match = view.IsTagEqual(35, "AE");

// Prefix check
bool isFix = view.TagStartsWith(8, "FIX"u8);
```

### Multi-Value Routing

```csharp
// Returns index of matching value, or -1 if no match
// Useful for switch-based message routing
int idx = view.MatchTag(35, "0"u8, "A"u8, "5"u8, "AE"u8);
// 0 = Heartbeat, 1 = Logon, 2 = Logout, 3 = Trade Capture Report

switch (view.MatchTag(35, "0"u8, "A"u8, "5"u8))
{
    case 0: HandleHeartbeat(view); break;
    case 1: HandleLogon(view); break;
    case 2: HandleLogout(view); break;
    default: HandleOther(view); break;
}
```

### Raw Span Access

```csharp
// Get raw bytes - zero allocation
ReadOnlySpan<byte> msgType = view.GetSpan(35);

// Try-pattern
if (view.TryGetSpan(35, out var span))
{
    // use span...
}
```

### Try-Pattern Getters (Avoids Nullable Boxing)

```csharp
// Instead of: int? bodyLen = view.GetInt32(9);
if (view.TryGetInt32(9, out var bodyLen))
{
    // use bodyLen directly, no boxing
}

// Also available:
view.TryGetInt64(tag, out long value)
view.TryGetDouble(tag, out double value)
view.TryGetDecimal(tag, out decimal value)
view.TryGetBool(tag, out bool value)
```

### Lightweight Repeated Tag Iteration

These methods do NOT trigger structure parsing - they use simple linear scan.
Ideal for small-medium messages or when you just need to count/iterate occurrences.

```csharp
// Count occurrences
int count = view.CountTag(453);  // PartyID

// Callback-based iteration
view.ForEachTagPosition(453, pos => {
    var span = ((AsciiView)view).GetSpanAtPosition(pos);
    // process...
});

// Zero-allocation enumerator (struct-based)
foreach (var pos in view.EnumerateTagPositions(453))
{
    var span = ((AsciiView)view).GetSpanAtPosition(pos);
    // process each occurrence...
}

// Get all positions (allocates array)
int[] positions = view.GetAllTagPositions(453);
```

### Position-Based Access (AsciiView only)

For use with `EnumerateTagPositions`:

```csharp
var asciiView = (AsciiView)view;
foreach (var pos in view.EnumerateTagPositions(tag))
{
    ReadOnlySpan<byte> span = asciiView.GetSpanAtPosition(pos);
    bool match = asciiView.IsEqualAtPosition(pos, "expected"u8);

    if (asciiView.TryGetInt32AtPosition(pos, out var intVal)) { }
    if (asciiView.TryGetInt64AtPosition(pos, out var longVal)) { }
    if (asciiView.TryGetDoubleAtPosition(pos, out var doubleVal)) { }
}
```

## Files Changed

| File | Changes |
|------|---------|
| `PureFix.Types/ElasticBuffer.cs` | Added `GetSpan`, `SequenceEqual`, `StartsWith`, `MatchValue` methods |
| `PureFix.Buffer/Ascii/MsgView.cs` | Added abstract span API + `TagPositionEnumerable`/`TagPositionEnumerator` structs |
| `PureFix.Buffer/Ascii/AsciiView.cs` | Implemented all abstract methods + position-based access helpers |

## New Files

| File | Purpose |
|------|---------|
| `PureFix.Test.ModularTypes/MsgViewSpanApiTest.cs` | 32 tests covering all new API |
| `PureFix.Benchmarks/SpanApiAccessBenchmarks.cs` | Benchmarks comparing string vs span patterns |

## Test Status

```
Passed!  - Failed: 0, Passed: 405, Skipped: 11, Total: 416
```

All 32 new span API tests pass.

## Benchmarks

`SpanApiAccessBenchmarks.cs` compares:
- `GetString` vs `IsTagEqual` for equality checks
- `GetString` + switch vs `MatchTag` for routing
- `GetInt32` (nullable) vs `TryGetInt32` (out param)
- Multi-field validation patterns

Run with:
```bash
dotnet run -c Release --project PureFix.Benchmarks -- --filter "*SpanApiAccessBenchmarks*"
```

## Next Steps / Ideas

1. **Run benchmarks** to quantify allocation savings
2. **Consider sorted tag access** for larger messages (> 20 tags) - binary search after sorting Tags array
3. **Use span API in generated parsers** for session messages (Logon, Heartbeat) to avoid structure parsing entirely
4. **Add more MatchTag overloads** if 4 values isn't enough for common routing scenarios

## Architecture Notes

The span-based methods use the same `GetPosition(tag)` path which:
- Uses linear scan for simple field access (no structure parse)
- Falls back to indexed lookup if structure was already parsed

The `TagPositionEnumerator` is a `ref struct` to ensure zero allocation during iteration.

ElasticBuffer methods are marked `[MethodImpl(MethodImplOptions.AggressiveInlining)]` for performance.
