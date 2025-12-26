# Optimization Opportunities

Potential further memory optimizations identified after lazy structure parsing implementation.

## Current State (Post Lazy Parsing)

| Message | Tokenize Alloc | Notes |
|---------|---------------|-------|
| Heartbeat | 256 B | Just the AsciiView wrapper |
| ExecutionReport | 3.4 KB | View + some tag array growth |

Storage (ElasticBuffer + Tags) is pooled. TagIndex and Context are pooled. The 256B is the AsciiView object itself.

## Opportunity 1: Pool AsciiView (High Impact)

**Current**: Each message creates a new AsciiView (256B).

**Location**: `AsciiParser.GetView()` creates new instance.

**Lifecycle analysis** (`FixSession.OnRx`):
```csharp
m_parser.ParseFrom(buffer, len, (_, v) => _messages.Add(v), OnFixLog);
foreach (var msg in _messages)
{
    await RxOnMsg(msg);                              // Callback completes
    m_parser.Return(((AsciiView)msg).Storage);       // Storage returned
}
```

**Multi-message scenario** (single buffer contains multiple FIX messages):
```
AsciiParseState.BeginMessage() rents NEW Storage for each message.

Parse msg1 → Rent Storage1 → Create View1(Storage1) → Add to _messages
Parse msg2 → Rent Storage2 → Create View2(Storage2) → Add to _messages
Parse msg3 → Rent Storage3 → Create View3(Storage3) → Add to _messages
───────────────────────────────────────────────────────────────────────
Process View1 → Callback → Return Storage1 (View1 can also be returned here)
Process View2 → Callback → Return Storage2 (View2 can also be returned here)
Process View3 → Callback → Return Storage3 (View3 can also be returned here)
```

Each view has its **own** storage (from `pool.Rent()` in `BeginMessage()`).
Views are held on `_messages` list but each references independent storage.
View never escapes the callback scope. Pooling follows same pattern as Storage.

**Implementation approach**:
```csharp
public class AsciiView : MsgView, IDisposable
{
    private static readonly ObjectPool<AsciiView> Pool =
        new DefaultObjectPool<AsciiView>(new DefaultPooledObjectPolicy<AsciiView>(), 16);

    internal static AsciiView Rent(...)
    {
        var view = Pool.Get();
        view.Initialize(...);
        return view;
    }

    public void Dispose()
    {
        Reset();
        Pool.Return(this);
    }

    private void Reset()
    {
        _structureParsed = false;
        _structure = null;
        _segment = null;
        SortedTagPosForwards = null;
        TagSpans = null;
        // Clear references to allow GC of Storage if needed
    }
}
```

**Changes required**:
1. Add `ObjectPool<AsciiView>` to AsciiView
2. Add `Rent()` static method and `Reset()` instance method
3. Change `AsciiParser.GetView()` to use `AsciiView.Rent()`
4. Change `FixSession.OnRx` to dispose views after callback

**Risk**: Low - lifecycle is already well-defined.

**Impact**: Eliminates 256B per message.

---

## Opportunity 2: Lightweight GetStrings/GetPositions (Medium Impact)

**Current**: `GetStrings(tag)` triggers full structure parse even though we only need sorted tags.

**Location**: `MsgView.cs:124-133`
```csharp
public string?[]? GetStrings(int tag)
{
    var range = GetPositions(tag);  // ← Triggers EnsureStructureParsed()
    ...
}

protected Range? GetPositions(int tag)
{
    EnsureStructureParsed();  // ← Full parse: TagIndex, Context, Structure
    EnumerateSpan();          // ← Creates new Dictionary<int, Range>
    ...
}
```

**What we actually need**: Just a sorted view of tags to group same-tag occurrences.

**Proposed approach**: Add a lightweight "sorted tags only" path:
```csharp
protected Range? GetPositionsLightweight(int tag)
{
    // If structure already parsed, use it
    if (_structure != null) return GetPositionsFromStructure(tag);

    // Otherwise, just sort tags without full structure parse
    EnsureSortedTags();  // Sort tags in-place or pooled copy
    return GetRangeFromSortedTags(tag);
}

private void EnsureSortedTags()
{
    if (SortedTagPosForwards != null) return;
    if (Tags == null) return;

    var count = Tags.NextTagPos;
    SortedTagPosForwards = ArrayPool<TagPos>.Shared.Rent(count);
    for (var i = 0; i < count; i++)
        SortedTagPosForwards[i] = Tags[i];
    Array.Sort(SortedTagPosForwards, 0, count, Comparer<TagPos>.Create(TagPos.Compare));

    // Build spans dictionary (could also pool this)
    TagSpans = TagIndex.GetSpans(SortedTagPosForwards, count);
}
```

**Impact**: Avoids 1-40KB structure allocation when only fetching repeated tag values.

---

## Opportunity 3: Cache GetSortedTags Result (Low-Medium Impact)

**Current**: `Structure.GetSortedTags()` allocates a new array every call.

**Location**: `Structure.cs:44-56`
```csharp
public TagPos[] GetSortedTags(SegmentDescription segmentDescription)
{
    var sortedTagPosForwards = segmentDescription.SegmentView != null
        ? segmentDescription.SegmentView.Tags.ToArray()   // ← allocation
        : Tags.Slice(start, end);                          // ← allocation
    Array.Sort(sortedTagPosForwards, TagPos.Compare);
    return sortedTagPosForwards;
}
```

**Proposed**: Cache in SegmentDescription or use ArrayPool:
```csharp
public TagPos[] GetSortedTags(SegmentDescription segmentDescription)
{
    // Cache check
    if (segmentDescription.CachedSortedTags != null)
        return segmentDescription.CachedSortedTags;

    // ... existing logic but cache result
    segmentDescription.CachedSortedTags = sortedTagPosForwards;
    return sortedTagPosForwards;
}
```

**Impact**: Avoids repeated allocation when accessing same component multiple times.

---

## Opportunity 4: Pool TagSpans Dictionary (Low Impact)

**Current**: `EnumerateSpan()` calls `TagIndex.GetSpans()` which creates new `Dictionary<int, Range>`.

**Location**: `MsgView.cs:74-75`
```csharp
SortedTagPosForwards = Structure.Value.GetSortedTags(Segment);
TagSpans = TagIndex.GetSpans(SortedTagPosForwards);  // ← new Dictionary
```

**Proposed**: Pool the dictionary similarly to TagIndex's internal dictionaries.

**Impact**: Small - dictionary is typically 100-400 bytes.

---

## Opportunity 5: Avoid Structure Segment List Copy (Low Impact)

**Current**: Structure constructor copies the segments list.

**Location**: `Structure.cs:24-31`
```csharp
public Structure(Tags tags, IReadOnlyList<SegmentDescription> segments)
{
    Segments = segments is List<SegmentDescription> list
        ? new List<SegmentDescription>(list)  // ← Copy
        : segments.ToList();                   // ← Copy
    (_singletons, _arrays) = BoundLayout(Segments);
}
```

**Reason**: Allows Context (which owns original list) to be pooled and reused.

**Alternative**: If Context lifetime could be extended, could avoid copy. But this complicates pooling.

**Impact**: 100-500 bytes per structure parse.

---

## Priority Matrix

| Opportunity | Allocation Saved | Frequency | Complexity | Priority |
|-------------|-----------------|-----------|------------|----------|
| Pool AsciiView | 256B | Every message | Medium | **High** |
| Lightweight GetStrings | 1-40KB | When fetching repeated tags | Medium | **Medium** |
| Cache GetSortedTags | 50-2000B | Per component access | Low | Low |
| Pool TagSpans | 100-400B | Per structure parse | Low | Low |
| Avoid segment copy | 100-500B | Per structure parse | Medium | Low |

## Implementation Order

1. **Pool AsciiView** - Highest impact, clean lifecycle
2. **Lightweight GetStrings** - Good ROI for repeated-tag access patterns
3. Others as needed based on profiling

## Notes on Span Indexer (`[]`) Overhead

The JIT heavily optimizes `Span<T>` indexing:
- Bounds checks are elided in loops with known bounds
- The indexer is a JIT intrinsic
- No meaningful gains expected from manual `Unsafe.Add` in this codebase

Current patterns like `Tags[i].Tag` are already optimal.
