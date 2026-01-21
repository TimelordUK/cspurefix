# Tag-by-Tag Segment Parser Design

## Problem Statement

The current `AsciiSegmentParser` uses a **stack-based recursive descent** approach:
- Push segments onto stack as we enter them
- Pop/unwind when tags don't match expected order
- Requires tags to appear in definition order

This breaks with real-world messages (e.g., Bloomberg) where:
- Tags within a component may be scattered/out-of-order
- Not all tags are present in each group instance
- Components may not be physically contiguous

## Existing Infrastructure

We already have the building blocks:

### TagIndex (already exists)
- Sorts tags by tag number, not position
- Creates spans for each tag: `_tagSpans[tag] → Range(start, end)`
- Identifies repeated tags: `_repeated.Contains(tag)`
- Maps group delimiters: `_tag2delim[tag]`
- Provides `GetInstance(name)` → `SegmentView` for scattered components

### SegmentView (already exists)
- Represents non-contiguous component as list of `TagPos`
- View doesn't care if tags are contiguous or scattered
- `MsgView.GetPosition()` already handles both modes

### ContainedSet (already exists)
- `TagToField[tag]` → `(parent, field)` - immediate lookup
- `ContainedTag[tag]` → owning set
- `NameToSet[name]` → component definition
- `FlattenedTag` → all tags in a component

## Proposed Design: "Jigsaw Placement" Parser

### Core Concept

Instead of stack-based navigation, we:
1. **Walk tags linearly** (one at a time, in message order)
2. **Look up each tag** directly via `TagToField` or `ContainedTag`
3. **Place into correct segment** without needing to unwind
4. **Assemble structure** from placed tags

### Key Insight: The "Golden Tag" (Delimiter)

For repeating groups:
- The **first tag in group definition** is the delimiter
- Each occurrence partitions a new group instance
- Other tags "attach" to the most recent delimiter occurrence

Example:
```
NoPartyIDs=2 → delimiter for Parties group
  PartyID=ABC, PartyRole=1, PartyIDSource=D  → instance 0
  PartyID=XYZ, PartyRole=2                   → instance 1 (missing PartyIDSource)
```

### Data Structures

```csharp
/// <summary>
/// Accumulates tags for a single segment/component during parsing.
/// Unlike SegmentDescription which is the output, this tracks state during parse.
/// </summary>
internal class SegmentAccumulator
{
    public string Name { get; }
    public IContainedSet Set { get; }
    public List<TagPos> Tags { get; } = new();
    public int Depth { get; }

    // For groups: which instance are we accumulating?
    public int GroupInstance { get; set; } = 0;

    // Track min/max positions for contiguity check
    public int MinPosition { get; private set; } = int.MaxValue;
    public int MaxPosition { get; private set; } = int.MinValue;

    public void Add(TagPos tag)
    {
        Tags.Add(tag);
        MinPosition = Math.Min(MinPosition, tag.Position);
        MaxPosition = Math.Max(MaxPosition, tag.Position);
    }

    public bool IsContiguous => MaxPosition - MinPosition + 1 == Tags.Count;
}
```

```csharp
/// <summary>
/// Parser state for tag-by-tag approach. No stack needed.
/// </summary>
internal class TagByTagContext
{
    // Active accumulators by component name
    public Dictionary<string, SegmentAccumulator> ActiveSegments { get; } = new();

    // For groups: track current instance per group name
    public Dictionary<string, int> GroupInstanceCounts { get; } = new();

    // For groups: accumulator per (groupName, instanceIndex)
    public Dictionary<(string, int), SegmentAccumulator> GroupInstances { get; } = new();

    // Output segments in discovery order
    public List<SegmentDescription> Segments { get; } = new();
}
```

### Algorithm

```
PARSE(msgType, tags, last):
    msgDef = definitions.GetMessage(msgType)
    context = new TagByTagContext()

    // Pass 1: Build tag index (already have this)
    tagIndex = TagIndex.Rent(msgDef, tags, last)

    // Pass 2: Walk tags in message order, place each one
    for i = 0 to last:
        tag = tags[i]
        PlaceTag(context, tagIndex, tag, i)

    // Pass 3: Finalize - convert accumulators to segments
    FinalizeSegments(context)

    return Structure(tags, context.Segments)

PLACE_TAG(context, tagIndex, tag, position):
    // Look up where this tag belongs
    (parent, field) = msgDef.Set.TagToField.GetValueOrDefault(tag.Tag)

    if parent == null:
        // Unknown tag - create gap segment
        PlaceInGap(context, tag, position)
        return

    // Is this a group delimiter (first tag in a group)?
    if IsGroupDelimiter(parent, tag):
        // Start new group instance
        StartNewGroupInstance(context, parent.Name, tag, position)
        return

    // Is this tag inside a group?
    groupName = FindContainingGroup(parent)
    if groupName != null:
        // Add to current instance of that group
        AddToGroupInstance(context, groupName, tag, position)
        return

    // Regular component/field
    AddToComponent(context, parent.Name, tag, position)

IS_GROUP_DELIMITER(parent, tag):
    // The delimiter is the first field in the group definition
    if parent.Type != ComponentType.Group:
        return false
    groupDef = parent as GroupDefinition
    return groupDef.Fields[0].Tag == tag.Tag

FIND_CONTAINING_GROUP(set):
    // Walk up the containment hierarchy to find if we're inside a group
    current = set
    while current != null:
        if current.Type == ComponentType.Group:
            return current.Name
        current = current.Parent
    return null
```

### Handling Repeated Tags

When a tag appears multiple times (`tagIndex._repeated.Contains(tag)`):

1. **Group Delimiter**: Each occurrence starts new instance
2. **Non-Delimiter in Group**: Attach to "current" instance based on position
3. **Outside Group**: Could be error or just repeated field (rare)

The key insight: **position relative to delimiters determines instance**

```csharp
int DetermineGroupInstance(string groupName, int tagPosition)
{
    // Find all delimiter positions for this group
    var delimPositions = context.GroupInstances
        .Where(kv => kv.Key.Item1 == groupName)
        .Select(kv => kv.Value.MinPosition)
        .OrderBy(p => p)
        .ToList();

    // Binary search to find which instance this position falls into
    for (int i = delimPositions.Count - 1; i >= 0; i--)
    {
        if (tagPosition >= delimPositions[i])
            return i;
    }
    return 0;
}
```

### Output: Same Structure Format

The output remains compatible with existing code:

```csharp
SegmentDescription CreateFromAccumulator(SegmentAccumulator acc)
{
    if (acc.IsContiguous)
    {
        // Can use simple start/end positions
        return new SegmentDescription(
            acc.Name,
            acc.Tags[0].Tag, acc.Tags[^1].Tag,
            acc.MinPosition, acc.MaxPosition,
            acc.Depth, SegmentType.Component, acc.Set,
            segmentView: null  // Not needed for contiguous
        );
    }
    else
    {
        // Must use SegmentView for scattered tags
        var view = new SegmentView(acc.Name, acc.Tags, acc.Set);
        return new SegmentDescription(
            acc.Name,
            acc.Tags[0].Tag, acc.Tags[^1].Tag,
            acc.MinPosition, acc.MaxPosition,
            acc.Depth, SegmentType.Component, acc.Set,
            segmentView: view
        );
    }
}
```

### Switching Between Parsers

The parser selection can be:

1. **Configuration-based**: User specifies which parser to use
2. **Detection-based**: Start with stack parser, switch if out-of-order detected
3. **Definition-based**: Some message types known to need tag-by-tag

```csharp
public class HybridSegmentParser : ISegmentParser
{
    private readonly AsciiSegmentParser _stackParser;
    private readonly TagByTagSegmentParser _indexParser;

    public Structure? Parse(string msgType, Tags tags, int last)
    {
        // Option 1: Try stack parser first
        var result = _stackParser.Parse(msgType, tags, last);

        // If it detected out-of-order issues, retry with index parser
        if (result?.HasOutOfOrderWarnings == true)
        {
            return _indexParser.Parse(msgType, tags, last);
        }

        return result;
    }
}
```

## Advantages

1. **No Stack**: Eliminates push/pop complexity
2. **Order-Independent**: Tags can appear in any order
3. **Simpler Logic**: Each tag placed independently based on definition lookup
4. **Better Error Handling**: Unknown tags don't break the parse
5. **Same Output**: Produces identical `Structure` format

## Risks / Considerations

1. **Performance**: Multiple dictionary lookups per tag vs stack peek
   - Mitigate: `ContainedTag` lookup is O(1)

2. **Group Instance Detection**: Position-based heuristic may fail for edge cases
   - Mitigate: Delimiter tag is reliable anchor

3. **Nested Groups**: Groups within groups need careful instance tracking
   - Each level needs its own delimiter tracking

4. **Backward Compatibility**: Existing tests expect specific segment structure
   - Mitigate: Output format unchanged, just built differently

## Implementation Plan

### Phase 1: Core Infrastructure
- [ ] Create `TagByTagSegmentParser` class
- [ ] Create `SegmentAccumulator` and `TagByTagContext`
- [ ] Implement basic `PlaceTag` logic for non-group tags

### Phase 2: Group Handling
- [ ] Implement group delimiter detection
- [ ] Implement instance tracking via positions
- [ ] Handle nested groups

### Phase 3: Integration
- [ ] Add parser selection mechanism
- [ ] Add out-of-order detection to existing parser
- [ ] Wire up hybrid parser option

### Phase 4: Testing
- [ ] Unit tests for tag placement logic
- [ ] Test with Bloomberg-style out-of-order messages
- [ ] Performance comparison with stack parser
- [ ] Regression tests with existing message corpus

## Design Decisions (Resolved)

### 1. Depth Tracking

**Decision**: Use ContainedSet's existing maps to determine depth.

ContainedSet already maintains maps of all components at or below the current level. If a tag doesn't belong locally, it must belong deeper. The depth can be computed by walking the containment hierarchy.

```csharp
int ComputeDepth(int tag, IContainedSet rootSet)
{
    // Walk containment hierarchy to find the tag's home
    var (parent, field) = rootSet.TagToField.GetValueOrDefault(tag);
    if (parent == null) return -1;  // Unknown tag

    // Count levels from root to parent
    int depth = 0;
    var current = parent;
    while (current != rootSet)
    {
        depth++;
        current = current.Parent;  // Assumes parent reference exists
    }
    return depth;
}
```

**Current Gap**: `IContainedSet` doesn't have a `Parent` reference or `Depth` property. Options:

a) **Add `Depth` to IContainedSet** - Pre-compute when building definition. Clean but requires dictionary change.

b) **Add `Parent` to IContainedSet** - Enables walking up. More flexible but heavier.

c) **Build depth map per message type** - At parse init, build `Dictionary<string, int>` mapping component name → depth. Computed once, reused for all tags.

```csharp
// Option (c) - build at parse time
private Dictionary<string, int> BuildDepthMap(IContainedSet root)
{
    var map = new Dictionary<string, int>();
    BuildDepthMapRecursive(root, 0, map);
    return map;
}

private void BuildDepthMapRecursive(IContainedSet set, int depth, Dictionary<string, int> map)
{
    map[set.Name] = depth;
    foreach (var comp in set.Components.Values)
        BuildDepthMapRecursive(comp, depth + 1, map);
    foreach (var group in set.Groups.Values)
        BuildDepthMapRecursive(group, depth + 1, map);
}
```

**Future Optimization - Locality Caching**: Tags from the same component tend to cluster together. Cache the last successfully placed component:

```csharp
IContainedSet? _lastPlacedComponent;

int PlaceTag(int tag)
{
    // Fast path: check if tag belongs in last component
    if (_lastPlacedComponent?.LocalTag.ContainsKey(tag) == true)
        return _cachedDepth;  // O(1) hit

    // Cache miss: walk from root, update cache
    _lastPlacedComponent = null;
    var (component, depth) = WalkToFind(tag);
    _lastPlacedComponent = component;
    _cachedDepth = depth;
    return depth;
}
```

High hit rate for well-formed messages, graceful fallback for scattered tags.

### 2. Discovery/Segment Order

**Decision**: Order doesn't matter for consumers.

The Structure exists for navigation: "give me the Instrument component" → returns view with those tags. The view includes all logically contained tags based on dictionary definition, not physical layout.

If any ordering is needed for output, use the **field declaration order** from the message definition XML, not discovery order.

### 3. Tag Uniqueness (Verified)

**Decision**: Tags are globally unique - no disambiguation needed.

Verified by analyzing FIX44.xml and FIX50SP2.xml: The FIX protocol explicitly avoids tag number collisions. Different semantic roles use different tags:
- Tag 55 = Symbol (in Instrument)
- Tag 600 = LegSymbol (in InstrumentLeg)
- Tag 311 = UnderlyingSymbol (in UnderlyingInstrument)

This means: Given a tag number, there is exactly ONE component it can belong to within a message. No ambiguity resolution needed.

### 4. Missing Group Delimiter

**Decision**: Invalid per FIX spec - treat as error.

- If delimiter missing but repeated non-delimiter tags seen → invalid message
- QuickFix enforces: first declared tag in group definition MUST be present (regardless of "required" attribute)
- This is industry-accepted behavior given QuickFix's widespread use

### 5. Parser Selection Strategy

**Decision**: Client explicitly chooses strategy - no heuristics.

```csharp
public enum ParserStrategy
{
    /// <summary>
    /// Stack-based recursive descent. Fast, requires in-order tags. Default.
    /// </summary>
    StackBased,

    /// <summary>
    /// Tag-by-tag placement. Slower but handles out-of-order tags correctly.
    /// </summary>
    TagByTag
}
```

The tag-by-tag parser trades efficiency for accuracy - it works harder to place things correctly rather than assuming ordered input.
