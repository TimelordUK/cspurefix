using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using PureFix.Buffer.Segment;
using PureFix.Types;

namespace PureFix.Buffer.Ascii
{
    public readonly record struct Structure 
    {
        private readonly Dictionary<string, SegmentDescription>? _singletons;
        private readonly Dictionary<string, List<SegmentDescription>>? _arrays;
        public readonly IReadOnlyList<SegmentDescription> Segments { get; }

        public Tags Tags { get; }

        public Structure(Tags tags, IReadOnlyList<SegmentDescription> segments)
        {
            Tags = tags;
            Segments = segments;
            (_singletons, _arrays) = BoundLayout(Segments);
        }

        public IReadOnlyList<SegmentDescription>? GetInstances(string name)
        {
            return _arrays?.GetValueOrDefault(name);
        }

        public SegmentDescription? GetInstance(string name)
        {
            return _singletons?.GetValueOrDefault(name);
        }

        public TagPos[] GetSortedTags(SegmentDescription segmentDescription)
        {
            var end = segmentDescription.EndPosition + 1;
            var start = segmentDescription.StartPosition;

            // these may not have tags all in a slice, so need to take the view which
            // is all tags within the component regardless of where they are.
            var sortedTagPosForwards = segmentDescription.SegmentView != null ? segmentDescription.SegmentView.Tags.ToArray() :
                // slice out the section of tags which represents this view 
                Tags.Slice(start, end);
            Array.Sort(sortedTagPosForwards, TagPos.Compare);
            return sortedTagPosForwards;
        }

        public SegmentDescription? Msg() => Segments.Count >= 2 ? Segments[^2] : null;

        public SegmentDescription? FirstContainedWithin(string name, SegmentDescription segment)
        {
            if (_singletons == null)
            {
                if (_arrays != null && _arrays.TryGetValue(name, out var instances))
                {
                    return GetSegmentDescription(instances, segment);
                }
                return null;
            }

            if (!_singletons.TryGetValue(name, out var component))
            {
                if (_arrays != null && _arrays.TryGetValue(name, out var instances))
                {
                    return GetSegmentDescription(instances, segment);
                }
            }
            else
            {
                if (segment.Contains(component)) return component;
            }

            return null;

            static SegmentDescription? GetSegmentDescription(IReadOnlyList<SegmentDescription> descriptions, SegmentDescription segment)
            {
                for (int i = 0, length = descriptions.Count; i < length; i++)
                {
                    var item = descriptions[i];
                    if (segment.Contains(item))
                    {
                        return item;
                    }
                }

                return null;
            }
        }

        private static Dictionary<string, List<SegmentDescription>>? AddToGroup(Dictionary<string, List<SegmentDescription>>? arrays, SegmentDescription current)
        {
            if (current.Name == null) return arrays;
            
            arrays ??= [];
            
            if (!arrays.TryGetValue(current.Name, out var instances))
            {
                arrays[current.Name] = instances = [];
            }

            instances.Add(current);

            return arrays;
        }

        private static (Dictionary<string, SegmentDescription>? Singletons, Dictionary<string, List<SegmentDescription>>? Arrays) BoundLayout(IReadOnlyList<SegmentDescription> segments, SegmentDescription? segment = null)
        {
            Dictionary<string, SegmentDescription>? singletons = null;
            Dictionary<string, List<SegmentDescription>>? arrays = null;

            for (var i = 0; i < segments.Count; i++)
            {
                var current = segments[i];

                if (current.Name == null) continue;
                if (segment != null && !segment.Contains(current)) continue;
                switch (current.Type)
                {
                    case SegmentType.Group:
                    case SegmentType.Component:
                    case SegmentType.Msg:
                    case SegmentType.Batch:
                        singletons ??= [];
                        if (arrays != null && arrays.ContainsKey(current.Name) )
                        {
                            // this is a component but repeated within a group and we need to store all instances
                            arrays = AddToGroup(arrays, current);
                        } 
                        // an instance previously held as an instance added along with new enty to a list
                        else if (singletons.Remove(current.Name, out var single)) {
                            arrays = AddToGroup(arrays, single);
                            arrays = AddToGroup(arrays, current);
                        }
                        else
                        {
                            singletons[current.Name] = current;
                        }
                        break;
                }
            }

            return (singletons, arrays);
        }
    }
}
