using PureFix.Buffer.Segment;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Types.Core;

namespace PureFix.Buffer.Ascii;

/// <summary>
/// Tag-by-tag segment parser that handles out-of-order tags.
/// Places each tag individually using dictionary lookups rather than
/// assuming tags appear in definition order.
///
/// This is more expensive than stack-based parsing but handles
/// Bloomberg-style messages where component tags may be scattered.
/// </summary>
public class TagByTagSegmentParser(IFixDefinitions definitions) : ISegmentParser
{
    public IFixDefinitions Definitions { get; } = definitions;

    // Locality cache: last component we placed a tag into
    private IContainedSet? _lastPlacedComponent;
    private int _lastPlacedDepth;

    /// <summary>
    /// Accumulates tags for each component during parsing.
    /// Key is component name, value is the view being built.
    /// </summary>
    private readonly Dictionary<string, SegmentView> _componentViews = new();

    /// <summary>
    /// For components nested inside groups: tracks per-instance views.
    /// Key is (componentName, parentGroupName, instanceIndex), value is the view for that instance.
    /// </summary>
    private readonly Dictionary<(string componentName, string parentGroupName, int instanceIndex), SegmentView> _nestedComponentViews = new();

    /// <summary>
    /// For groups: tracks current instance count per group name.
    /// </summary>
    private readonly Dictionary<string, int> _groupInstanceCounts = new();

    /// <summary>
    /// For groups: accumulates tags per (groupName, instanceIndex).
    /// </summary>
    private readonly Dictionary<(string name, int instance), SegmentView> _groupInstances = new();

    /// <summary>
    /// For nested groups (groups inside other groups): tracks per-parent-instance.
    /// Key is (groupName, parentGroupName, parentInstanceIndex, localInstanceIndex).
    /// </summary>
    private readonly Dictionary<(string groupName, string parentGroupName, int parentInstance, int localInstance), SegmentView> _nestedGroupInstances = new();

    /// <summary>
    /// For nested groups: tracks instance count per (groupName, parentGroupName, parentInstanceIndex).
    /// </summary>
    private readonly Dictionary<(string groupName, string parentGroupName, int parentInstance), int> _nestedGroupInstanceCounts = new();

    /// <summary>
    /// Tracks which parent group (and instance) we're currently processing within.
    /// Used to create instance-specific component views for nested components.
    /// </summary>
    private (string groupName, int instanceIndex)? _currentGroupContext;

    /// <summary>
    /// Tracks which nested group (and instance) we're currently processing within.
    /// Used for components inside nested groups (groups inside other groups).
    /// Key is (nestedGroupName, parentGroupName, parentInstance, localInstance).
    /// </summary>
    private (string nestedGroupName, string parentGroupName, int parentInstance, int localInstance)? _currentNestedGroupContext;

    /// <summary>
    /// For components inside nested groups: tracks per-nested-group-instance views.
    /// Key is (componentName, nestedGroupName, parentGroupName, parentInstance, localInstance).
    /// </summary>
    private readonly Dictionary<(string componentName, string nestedGroupName, string parentGroupName, int parentInstance, int localInstance), SegmentView> _nestedGroupComponentViews = new();

    /// <summary>
    /// Output segments in discovery order.
    /// </summary>
    private readonly List<SegmentDescription> _segments = new();

    public Structure? Parse(string msgType, Tags tags, int last)
    {
        if (!Definitions.Message.TryGetValue(msgType, out var msgDefinition))
        {
            return null;
        }

        // Reset state for new parse
        Reset();

        // Create root message segment
        var msgSegment = new SegmentDescription(
            msgDefinition.Name,
            tags[0].Tag,
            msgDefinition,
            0,
            0,  // depth 0
            SegmentType.Msg);

        // Walk through all tags and place each one
        for (var i = 0; i <= last; i++)
        {
            var tag = tags[i];
            PlaceTag(msgDefinition, tag, i);
        }

        // Finalize: convert accumulated views to segments
        FinalizeSegments(msgDefinition, msgSegment, last, tags);

        return new Structure(tags, _segments);
    }

    private void Reset()
    {
        _lastPlacedComponent = null;
        _lastPlacedDepth = 0;
        _componentViews.Clear();
        _nestedComponentViews.Clear();
        _groupInstanceCounts.Clear();
        _groupInstances.Clear();
        _nestedGroupInstances.Clear();
        _nestedGroupInstanceCounts.Clear();
        _currentGroupContext = null;
        _currentNestedGroupContext = null;
        _nestedGroupComponentViews.Clear();
        _segments.Clear();
    }

    // Debug flag - set to true to trace specific tags (for debugging only)
    private const bool DebugTrace = false;
    private static readonly HashSet<int> DebugTags = new() { 454, 455, 456 };

    private void PlaceTag(MessageDefinition msgDefinition, TagPos tag, int position)
    {
        // Fast path: check locality cache (only for depth > 0, not message-level tags)
        // IMPORTANT: Don't use cache for group delimiter tags - they need full processing
        // to create new group instances
        if (_lastPlacedDepth > 0 && _lastPlacedComponent?.LocalTag.ContainsKey(tag.Tag) == true)
        {
            // Check if this is a group delimiter - if so, don't use the cache
            // We need to check both the cached component and any containing group
            var isDelimiter = IsGroupDelimiter(_lastPlacedComponent, tag.Tag);

            // Also check if this tag is a delimiter for a containing group
            // (e.g., tag 311 inside UnderlyingInstrument is a delimiter for NoUnderlyings)
            if (!isDelimiter)
            {
                var cachedContainingGroup = FindContainingGroup(_lastPlacedComponent, msgDefinition);
                if (cachedContainingGroup != null)
                {
                    isDelimiter = IsGroupDelimiter(cachedContainingGroup, tag.Tag);
                }
            }

            if (!isDelimiter)
            {
                // For groups, add to the current group instance, not to a component view
                // Note: AddToCurrentGroupInstance already calls AddToParentComponents
                if (_lastPlacedComponent is GroupFieldDefinition)
                {
                    AddToCurrentGroupInstance(_lastPlacedComponent, _lastPlacedComponent, tag, msgDefinition);
                }
                else
                {
                    AddToComponent(_lastPlacedComponent.Name, _lastPlacedComponent, tag);
                    // Only propagate to parent components for non-group tags (groups handle this internally)
                    if (_lastPlacedDepth > 1)
                    {
                        AddToParentComponents(tag, msgDefinition);
                    }
                }
                return;
            }
        }

        // Cache miss: find where this tag belongs
        var (owningSet, depth) = FindOwningSet(tag.Tag, msgDefinition, 0);

        if (DebugTrace && tag.Tag == 454)
        {
            Console.WriteLine($"DEBUG: Tag 454 - owningSet={owningSet?.Name ?? "null"}, depth={depth}");
        }

        if (owningSet == null)
        {
            // Unknown tag - could create a gap segment
            // For now, skip it
            return;
        }

        // Update locality cache
        _lastPlacedComponent = owningSet;
        _lastPlacedDepth = depth;

        // Check if this is a group delimiter - first check the owning set itself
        if (IsGroupDelimiter(owningSet, tag.Tag))
        {
            StartNewGroupInstance(owningSet, tag, msgDefinition);
            return;
        }

        // Check if this tag is inside a group
        var containingGroup = FindContainingGroup(owningSet, msgDefinition);
        if (containingGroup != null)
        {
            // Also check if this tag is a delimiter for the containing group
            // This handles cases where the delimiter field is inside a nested component
            // (e.g., UnderlyingSymbol(311) is inside UnderlyingInstrument but is also
            // the delimiter for NoUnderlyings group)
            if (IsGroupDelimiter(containingGroup, tag.Tag))
            {
                StartNewGroupInstance(containingGroup, tag, msgDefinition);
                return;
            }

            AddToCurrentGroupInstance(containingGroup, owningSet, tag, msgDefinition);
            return;
        }

        // Tag is not inside any group - clear the group context
        // This ensures that when we move from a group section (e.g., Parties)
        // to a non-group section (e.g., Instrument), the context is reset
        _currentGroupContext = null;

        // Skip message-level tags (depth=0) - they're part of the message, not a component
        // These are accessed directly from the message view, not through GetView("ComponentName")
        if (depth == 0)
        {
            return;
        }

        // Regular component tag (depth > 0)
        AddToComponent(owningSet.Name, owningSet, tag);

        // Also add to parent components so nested tags appear in their parent's view
        // (e.g., tag 454 in SecAltIDGrp should also appear in Instrument)
        // Only do this for deeply nested components (depth > 1), not direct children
        if (depth > 1)
        {
            AddToParentComponents(tag, msgDefinition);
        }
    }

    /// <summary>
    /// Walks down the containment hierarchy to find where a tag belongs.
    /// Returns the owning set and its depth.
    ///
    /// Note: ContainedTag may be flattened (pointing directly to the final container),
    /// so we also walk through Components and Groups explicitly to find the proper depth.
    /// </summary>
    private (IContainedSet? set, int depth) FindOwningSet(int tag, IContainedSet currentSet, int currentDepth)
    {
        // Safety: prevent infinite recursion (max depth of 20 should be more than enough for any FIX message)
        if (currentDepth > 20)
        {
            return (null, -1);
        }

        // Is it local to this set?
        if (currentSet.LocalTag.ContainsKey(tag))
        {
            return (currentSet, currentDepth);
        }

        // Check child components first - recurse if they contain this tag
        foreach (var component in currentSet.Components.Values)
        {
            // Check if this component contains the tag (locally or via ContainedTag)
            if (component.LocalTag.ContainsKey(tag) || component.ContainedTag.ContainsKey(tag))
            {
                return FindOwningSet(tag, component, currentDepth + 1);
            }
        }

        // Check for count tags (e.g., NoSecurityAltID=454)
        // Count tags are in ContainedTag pointing to a group, but not LOCAL to that group
        // They belong to the current set (parent of the group), not the group itself
        // Only claim count tags at component level (depth > 0), not message level
        if (currentDepth > 0 && currentSet.ContainedTag.TryGetValue(tag, out var targetSet))
        {
            // If the target is a group that doesn't have this tag locally,
            // this is a count tag that belongs to the current set
            if (targetSet is GroupFieldDefinition && !targetSet.LocalTag.ContainsKey(tag))
            {
                if (DebugTrace && tag == 454)
                {
                    Console.WriteLine($"DEBUG: Tag 454 - count tag for group {targetSet.Name}, belongs to {currentSet.Name} at depth {currentDepth}");
                }
                return (currentSet, currentDepth);
            }
        }

        // Check child groups
        foreach (var group in currentSet.Groups.Values)
        {
            // Only recurse if the tag is actually LOCAL to this group
            if (group.LocalTag.ContainsKey(tag))
            {
                return FindOwningSet(tag, group, currentDepth + 1);
            }

            // Check nested groups/components inside this group
            foreach (var nestedGroup in group.Groups.Values)
            {
                if (nestedGroup.LocalTag.ContainsKey(tag))
                {
                    return FindOwningSet(tag, group, currentDepth + 1);
                }
            }
        }

        // Fall back to ContainedTag for any remaining cases
        if (currentSet.ContainedTag.TryGetValue(tag, out var childSet))
        {
            // Prevent infinite loop if child points back to same set
            if (ReferenceEquals(childSet, currentSet))
            {
                return (null, -1);
            }

            // Check if the child actually has this tag (locally or contained)
            if (!childSet.LocalTag.ContainsKey(tag) && !childSet.ContainedTag.ContainsKey(tag))
            {
                // Count tag that doesn't fit anywhere - belongs to current set
                return (currentSet, currentDepth);
            }

            return FindOwningSet(tag, childSet, currentDepth + 1);
        }

        // Unknown tag
        return (null, -1);
    }

    /// <summary>
    /// Finds the containment path from root to the tag's owning set.
    /// Returns list of (set, depth) tuples representing the path.
    /// Used to add tags to parent components when placing in nested groups.
    /// </summary>
    private List<(IContainedSet set, int depth)> FindOwningPath(int tag, IContainedSet currentSet, int currentDepth)
    {
        var path = new List<(IContainedSet, int)>();

        // Safety: prevent infinite recursion
        if (currentDepth > 20)
        {
            return path;
        }

        // Add current set to path (except for root message at depth 0)
        if (currentDepth > 0)
        {
            path.Add((currentSet, currentDepth));
        }

        // Is it local to this set?
        if (currentSet.LocalTag.ContainsKey(tag))
        {
            return path;
        }

        // It must be deeper - find which child contains it
        if (currentSet.ContainedTag.TryGetValue(tag, out var childSet))
        {
            if (!ReferenceEquals(childSet, currentSet))
            {
                path.AddRange(FindOwningPath(tag, childSet, currentDepth + 1));
            }
        }

        return path;
    }

    /// <summary>
    /// Checks if this tag is the delimiter (first field) of a group.
    /// The delimiter can be a direct simple field or the first field of a component.
    /// </summary>
    private static bool IsGroupDelimiter(IContainedSet set, int tag)
    {
        if (set is not GroupFieldDefinition groupDef)
        {
            return false;
        }

        // The delimiter is the first field in the group
        var firstField = groupDef.Fields.FirstOrDefault();
        if (firstField is ContainedSimpleField simpleField)
        {
            return simpleField.Definition.Tag == tag;
        }

        // If the first field is a component, check its first field
        if (firstField is ContainedComponentField componentRef && componentRef.Definition != null)
        {
            return IsFirstFieldOfComponent(componentRef.Definition, tag);
        }

        return false;
    }

    /// <summary>
    /// Recursively finds the first simple field tag of a component.
    /// </summary>
    private static bool IsFirstFieldOfComponent(ComponentFieldDefinition component, int tag)
    {
        var firstField = component.Fields.FirstOrDefault();
        if (firstField is ContainedSimpleField simpleField)
        {
            return simpleField.Definition.Tag == tag;
        }

        // If the first field is another component, recurse
        if (firstField is ContainedComponentField nestedComponent && nestedComponent.Definition != null)
        {
            return IsFirstFieldOfComponent(nestedComponent.Definition, tag);
        }

        return false;
    }

    /// <summary>
    /// Walks down the containment hierarchy to find if a tag is inside a group.
    /// Returns the containing group if found.
    /// </summary>
    private IContainedSet? FindContainingGroup(IContainedSet set, IContainedSet root)
    {
        // Check if the set itself is a group
        if (set is GroupFieldDefinition)
        {
            return set;
        }

        // Recursively check all groups in the hierarchy
        return FindContainingGroupRecursive(set, root);
    }

    private IContainedSet? FindContainingGroupRecursive(IContainedSet set, IContainedSet current)
    {
        // Check direct groups in the current set
        foreach (var group in current.Groups.Values)
        {
            if (group.NameToSet.ContainsKey(set.Name) || group.Name == set.Name)
            {
                return group;
            }
            // Check nested groups
            var nested = FindContainingGroupRecursive(set, group);
            if (nested != null) return nested;
        }

        // Check groups inside components
        foreach (var component in current.Components.Values)
        {
            var found = FindContainingGroupRecursive(set, component);
            if (found != null) return found;
        }

        return null;
    }

    private void StartNewGroupInstance(IContainedSet groupSet, TagPos delimiterTag, MessageDefinition msgDefinition)
    {
        var groupName = groupSet.Name;

        // Check if we're inside a parent group context (nested group case)
        if (_currentGroupContext.HasValue)
        {
            var (parentGroupName, parentInstance) = _currentGroupContext.Value;

            // Don't nest a group inside itself
            if (groupName != parentGroupName)
            {
                // This is a nested group - track per parent instance
                var nestedKey = (groupName, parentGroupName, parentInstance);
                if (!_nestedGroupInstanceCounts.TryGetValue(nestedKey, out var localInstance))
                {
                    localInstance = 0;
                }
                _nestedGroupInstanceCounts[nestedKey] = localInstance + 1;

                var view = new SegmentView($"{groupName}@{parentGroupName}[{parentInstance}]_{localInstance}", groupSet);
                view.Add(delimiterTag);
                _nestedGroupInstances[(groupName, parentGroupName, parentInstance, localInstance)] = view;

                // Don't update _currentGroupContext - keep the parent group as context
                // But do update the nested group context so components inside know their parent
                _currentNestedGroupContext = (groupName, parentGroupName, parentInstance, localInstance);

                // Clear locality cache so the next tag goes through full lookup
                _lastPlacedComponent = null;
                _lastPlacedDepth = 0;

                // Also add to parent components in the containment path
                AddToParentComponents(delimiterTag, msgDefinition);
                return;
            }
        }

        // Top-level group or same group (new instance of current group)
        // Increment instance count
        if (!_groupInstanceCounts.TryGetValue(groupName, out var instanceCount))
        {
            instanceCount = 0;
        }
        _groupInstanceCounts[groupName] = instanceCount + 1;

        if (DebugTrace && DebugTags.Contains(delimiterTag.Tag))
        {
            Console.WriteLine($"DEBUG: StartNewGroupInstance({groupName}[{instanceCount}], tag={delimiterTag.Tag})");
        }

        // Create new view for this instance
        var view2 = new SegmentView($"{groupName}[{instanceCount}]", groupSet);
        view2.Add(delimiterTag);
        _groupInstances[(groupName, instanceCount)] = view2;

        // Update the current group context for nested components
        _currentGroupContext = (groupName, instanceCount);
        // Clear nested group context when we start a new top-level group instance
        _currentNestedGroupContext = null;

        // Clear locality cache so the next tag goes through full lookup
        // This ensures we detect subsequent group delimiters correctly
        _lastPlacedComponent = null;
        _lastPlacedDepth = 0;

        // Also add to parent components in the containment path
        AddToParentComponents(delimiterTag, msgDefinition);
    }

    private void AddToCurrentGroupInstance(IContainedSet group, IContainedSet owningSet, TagPos tag, MessageDefinition msgDefinition)
    {
        var groupName = group.Name;

        // Check if this is a nested group (inside another group context)
        if (_currentGroupContext.HasValue && groupName != _currentGroupContext.Value.groupName)
        {
            var (parentGroupName, parentInstance) = _currentGroupContext.Value;
            var nestedKey = (groupName, parentGroupName, parentInstance);

            if (_nestedGroupInstanceCounts.TryGetValue(nestedKey, out var localCount) && localCount > 0)
            {
                var currentLocalInstance = localCount - 1;
                var fullKey = (groupName, parentGroupName, parentInstance, currentLocalInstance);
                if (_nestedGroupInstances.TryGetValue(fullKey, out var nestedView))
                {
                    nestedView.Add(tag);
                }
            }

            // Also add to parent components in the containment path
            AddToParentComponents(tag, msgDefinition);
            return;
        }

        // Top-level group
        // Get current instance (last one created)
        if (!_groupInstanceCounts.TryGetValue(groupName, out var instanceCount) || instanceCount == 0)
        {
            // No group instance started yet - this is an error condition
            // For now, create instance 0
            instanceCount = 0;
            _groupInstanceCounts[groupName] = 1;
            _groupInstances[(groupName, 0)] = new SegmentView($"{groupName}[0]", group);
        }

        var currentInstance = instanceCount - 1;
        if (_groupInstances.TryGetValue((groupName, currentInstance), out var view))
        {
            view.Add(tag);
        }

        // Also add to parent components in the containment path
        AddToParentComponents(tag, msgDefinition);
    }

    /// <summary>
    /// Adds a tag to all parent components that contain it according to the dictionary.
    /// Uses ContainedTag which already has the flattened containment information.
    /// Recursively walks the component hierarchy to include nested wrapper components.
    /// When inside a group instance, creates instance-specific component views.
    /// </summary>
    private void AddToParentComponents(TagPos tag, MessageDefinition msgDefinition)
    {
        AddToParentComponentsRecursive(tag, msgDefinition, isInsideGroup: false);
    }

    private void AddToParentComponentsRecursive(TagPos tag, IContainedSet currentSet, bool isInsideGroup, bool isInsideNestedGroup = false)
    {
        // Iterate through child components of the current set
        foreach (var component in currentSet.Components.Values)
        {
            // If this component contains the tag (either locally or via nested containment),
            // add it to that component's view and recurse into its children
            if (component.ContainedTag.ContainsKey(tag.Tag) || component.LocalTag.ContainsKey(tag.Tag))
            {
                // If we're inside a nested group context, use that for instance-specific views
                if (_currentNestedGroupContext.HasValue && isInsideNestedGroup)
                {
                    AddToNestedGroupComponent(component.Name, component, tag, _currentNestedGroupContext.Value);
                }
                // Otherwise, if we're inside a top-level group context, use that
                else if (_currentGroupContext.HasValue && isInsideGroup)
                {
                    AddToNestedComponent(component.Name, component, tag, _currentGroupContext.Value);
                }
                else
                {
                    AddToComponent(component.Name, component, tag);
                }

                // Recurse to find nested wrapper components (e.g., SecAltIDGrp inside Instrument)
                AddToParentComponentsRecursive(tag, component, isInsideGroup, isInsideNestedGroup);
            }
        }

        // Check if any child groups contain this tag - if so, mark that we're inside a group
        foreach (var group in currentSet.Groups.Values)
        {
            if (group.ContainedTag.ContainsKey(tag.Tag) || group.LocalTag.ContainsKey(tag.Tag))
            {
                // If we're already inside a group and encounter another group, that's a nested group
                // Use the nested group context if we have one and this group matches
                var enteringNestedGroup = isInsideGroup || isInsideNestedGroup;
                // Components nested inside this group should be instance-specific
                AddToParentComponentsRecursive(tag, group, isInsideGroup: true, isInsideNestedGroup: enteringNestedGroup);
            }
        }
    }

    private void AddToNestedComponent(string name, IContainedSet set, TagPos tag, (string groupName, int instanceIndex) context)
    {
        var key = (name, context.groupName, context.instanceIndex);
        if (!_nestedComponentViews.TryGetValue(key, out var view))
        {
            view = new SegmentView($"{name}@{context.groupName}[{context.instanceIndex}]", set);
            _nestedComponentViews[key] = view;
        }
        view.Add(tag);

        if (DebugTrace && DebugTags.Contains(tag.Tag))
        {
            Console.WriteLine($"DEBUG: AddToNestedComponent({name}@{context.groupName}[{context.instanceIndex}], tag={tag.Tag}) - view now has {view.Tags.Count} tags");
        }
    }

    private void AddToNestedGroupComponent(string name, IContainedSet set, TagPos tag, (string nestedGroupName, string parentGroupName, int parentInstance, int localInstance) context)
    {
        var key = (name, context.nestedGroupName, context.parentGroupName, context.parentInstance, context.localInstance);
        if (!_nestedGroupComponentViews.TryGetValue(key, out var view))
        {
            view = new SegmentView($"{name}@{context.nestedGroupName}[{context.parentGroupName}[{context.parentInstance}]_{context.localInstance}]", set);
            _nestedGroupComponentViews[key] = view;
        }
        view.Add(tag);

        if (DebugTrace && DebugTags.Contains(tag.Tag))
        {
            Console.WriteLine($"DEBUG: AddToNestedGroupComponent({name}@nested, tag={tag.Tag}) - view now has {view.Tags.Count} tags");
        }
    }

    private void AddToComponent(string name, IContainedSet set, TagPos tag)
    {
        if (!_componentViews.TryGetValue(name, out var view))
        {
            view = new SegmentView(name, set);
            _componentViews[name] = view;
        }
        view.Add(tag);

        if (DebugTrace && DebugTags.Contains(tag.Tag))
        {
            Console.WriteLine($"DEBUG: AddToComponent({name}, tag={tag.Tag}) - view now has {view.Tags.Count} tags");
        }
    }

    private void FinalizeSegments(MessageDefinition msgDefinition, SegmentDescription msgSegment, int last, Tags tags)
    {
        // Set message segment end position
        msgSegment.End(0, last, tags[last].Tag);

        // Collect StandardTrailer separately - it must be last in the segment list
        SegmentDescription? trailerSegment = null;

        // Add component segments (except StandardTrailer)
        foreach (var (name, view) in _componentViews)
        {
            var depth = ComputeDepth(name, msgDefinition);
            var segment = new SegmentDescription(
                name,
                view.StartTag,
                view.Set,
                view.StartPosition,
                depth,
                SegmentType.Component);
            segment.Add(view);

            if (name == "StandardTrailer")
            {
                trailerSegment = segment;
            }
            else
            {
                _segments.Add(segment);
            }
        }

        // Add group segments - combine instances into a single segment per group
        // with delimiter positions for each instance
        var groupSegments = new Dictionary<string, SegmentDescription>();
        var groupViews = new Dictionary<string, List<SegmentView>>();

        // First pass: collect instances by group name
        foreach (var ((groupName, instance), view) in _groupInstances.OrderBy(x => (x.Key.name, x.Key.instance)))
        {
            if (!groupViews.TryGetValue(groupName, out var views))
            {
                views = [];
                groupViews[groupName] = views;
            }
            views.Add(view);
        }

        // Second pass: create combined segment for each group
        foreach (var (groupName, views) in groupViews)
        {
            if (views.Count == 0) continue;

            var firstView = views[0];
            var depth = ComputeDepth(groupName, msgDefinition);

            // Create combined segment
            var segment = new SegmentDescription(
                groupName,
                firstView.StartTag,
                firstView.Set,
                firstView.StartPosition,
                depth,
                SegmentType.Group);

            // Initialize delimiter tracking
            segment.StartGroup(firstView.StartTag);

            // Create a combined SegmentView with all tags from all instances
            var combinedView = new SegmentView(groupName, firstView.Set);
            var minPosition = int.MaxValue;
            var maxPosition = int.MinValue;

            foreach (var view in views)
            {
                // Add delimiter position for this instance (first tag position)
                segment.AddDelimiterPosition(view.StartPosition);

                // Store the instance's SegmentView for GetInstance to use
                segment.AddInstanceView(view);

                // Add all tags to combined view
                foreach (var tag in view.Tags)
                {
                    combinedView.Add(tag);
                }

                // Track overall positions
                if (view.StartPosition < minPosition) minPosition = view.StartPosition;
                if (view.EndPosition > maxPosition) maxPosition = view.EndPosition;
            }

            // Set the combined segment's positions
            segment.Add(combinedView);

            _segments.Add(segment);
        }

        // Add nested component segments - these are components inside group instances
        // Create one segment per (componentName, parentGroup, instanceIndex) tuple
        // Extend end position to include any nested groups within the component
        foreach (var ((componentName, parentGroupName, instanceIndex), view) in _nestedComponentViews
            .OrderBy(x => (x.Key.componentName, x.Key.parentGroupName, x.Key.instanceIndex)))
        {
            // Get the parent group instance's SegmentView to inherit positions
            if (!_groupInstances.TryGetValue((parentGroupName, instanceIndex), out var parentInstanceView))
            {
                continue; // Parent instance not found, skip
            }

            // Calculate extended end position by finding any nested groups within this component
            // that might have tags beyond the component's direct tags
            var extendedEnd = parentInstanceView.EndPosition;
            foreach (var ((nestedGroupName, nestedParentGroup, nestedParentInstance, _), nestedView) in _nestedGroupInstances)
            {
                // Check if this nested group is inside the current component
                if (nestedParentGroup == parentGroupName && nestedParentInstance == instanceIndex)
                {
                    // This nested group is inside our parent group - check if the component contains it
                    if (view.Set?.NameToSet.ContainsKey(nestedGroupName) == true)
                    {
                        // Extend end position to include this nested group's tags
                        if (nestedView.EndPosition > extendedEnd)
                        {
                            extendedEnd = nestedView.EndPosition;
                        }
                    }
                }
            }

            var depth = ComputeDepth(componentName, msgDefinition);
            var segment = new SegmentDescription(
                componentName,
                view.StartTag,
                view.Set,
                parentInstanceView.StartPosition, // Use parent instance's start position
                depth,
                SegmentType.Component);

            // Create a modified view with positions extended to include nested content
            view.AdjustPositions(parentInstanceView.StartPosition, extendedEnd);
            segment.Add(view);

            _segments.Add(segment);
        }

        // Add nested group segments - groups inside other groups
        // Group by (groupName, parentGroupName, parentInstance) to combine local instances
        var nestedGroupsByParent = _nestedGroupInstances
            .GroupBy(x => (x.Key.groupName, x.Key.parentGroupName, x.Key.parentInstance))
            .OrderBy(g => (g.Key.groupName, g.Key.parentGroupName, g.Key.parentInstance));

        foreach (var group in nestedGroupsByParent)
        {
            var (groupName, parentGroupName, parentInstance) = group.Key;
            var localInstances = group.OrderBy(x => x.Key.localInstance).ToList();
            if (localInstances.Count == 0) continue;

            var firstView = localInstances[0].Value;
            var depth = ComputeDepth(groupName, msgDefinition);

            // Create combined view first to get natural tag positions
            var combinedView = new SegmentView(groupName, firstView.Set);
            foreach (var (_, localView) in localInstances)
            {
                foreach (var tag in localView.Tags)
                {
                    combinedView.Add(tag);
                }
            }

            // Create a segment using the actual tag positions (not overriding to parent)
            // This allows nested groups inside nested groups to have proper containment
            var segment = new SegmentDescription(
                groupName,
                firstView.StartTag,
                firstView.Set,
                combinedView.StartPosition, // Use actual tag start position
                depth,
                SegmentType.Group);

            // Initialize delimiter tracking
            segment.StartGroup(firstView.StartTag);

            // Add delimiter positions and instance views
            foreach (var (_, localView) in localInstances)
            {
                segment.AddDelimiterPosition(localView.StartPosition);
                segment.AddInstanceView(localView);
            }

            segment.Add(combinedView);
            _segments.Add(segment);
        }

        // Add components inside nested groups - these are components inside groups that are inside other groups
        // (e.g., NstdPtysSubGrp inside NestedPartyIDs inside Legs)
        foreach (var ((componentName, nestedGroupName, parentGroupName, parentInstance, localInstance), view) in _nestedGroupComponentViews
            .OrderBy(x => (x.Key.componentName, x.Key.nestedGroupName, x.Key.parentGroupName, x.Key.parentInstance, x.Key.localInstance)))
        {
            // Get the nested group instance's SegmentView to inherit positions
            var nestedKey = (nestedGroupName, parentGroupName, parentInstance, localInstance);
            if (!_nestedGroupInstances.TryGetValue(nestedKey, out var nestedInstanceView))
            {
                continue; // Nested group instance not found, skip
            }

            var depth = ComputeDepth(componentName, msgDefinition);
            var segment = new SegmentDescription(
                componentName,
                view.StartTag,
                view.Set,
                nestedInstanceView.StartPosition, // Use nested group instance's start position
                depth,
                SegmentType.Component);

            // Adjust positions to match the NESTED group instance (not the parent)
            // This ensures containment checks work correctly when finding NstdPtysSubGrp
            // from a NoNestedPartyIDs instance
            view.AdjustPositions(nestedInstanceView.StartPosition, nestedInstanceView.EndPosition);
            segment.Add(view);

            _segments.Add(segment);
        }

        // Add Msg segment at [^2] position (Structure.Msg() returns Segments[^2])
        _segments.Add(msgSegment);

        // Add StandardTrailer at [^1] position (must be last)
        if (trailerSegment != null)
        {
            _segments.Add(trailerSegment);
        }
    }

    private int ComputeDepth(string componentName, IContainedSet root)
    {
        return ComputeDepthRecursive(componentName, root, 0);
    }

    private int ComputeDepthRecursive(string name, IContainedSet current, int depth)
    {
        if (current.Name == name)
        {
            return depth;
        }

        foreach (var comp in current.Components.Values)
        {
            var result = ComputeDepthRecursive(name, comp, depth + 1);
            if (result >= 0) return result;
        }

        foreach (var group in current.Groups.Values)
        {
            var result = ComputeDepthRecursive(name, group, depth + 1);
            if (result >= 0) return result;
        }

        return -1;
    }
}
