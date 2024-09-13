using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Segment;
using PureFix.Types;

namespace PureFix.Buffer.Ascii
{
    public record struct Structure 
    {
        private Dictionary<string, SegmentDescription>? _singletons;
        // do not create unless needed
        private Dictionary<string, List<SegmentDescription>>? _arrays;
        public IReadOnlyList<SegmentDescription> Segments { get; }

        public Tags Tags { get; }

        public Structure(Tags tags, IReadOnlyList<SegmentDescription> segments)
        {
            Tags = tags;
            Segments = segments;
            BoundLayout();
        }

        public readonly IReadOnlyList<SegmentDescription>? GetInstances(string name)
        {
            return _arrays?.GetValueOrDefault(name);
        }

        public readonly SegmentDescription? GetInstance(string name)
        {
            return _singletons?.GetValueOrDefault(name);
        }

        public readonly SegmentDescription? Msg() => Segments.Count >= 2 ? Segments[^2] : null;

        public readonly SegmentDescription? FirstContainedWithin(string name, SegmentDescription segment)
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

        private void AddToGroup(SegmentDescription current)
        {
            if (current.Name == null) return;
            _arrays ??= [];
            if (!_arrays.TryGetValue(current.Name, out var instances))
            {
                _arrays[current.Name] = instances = [];
            }
            instances.Add(current);
        }

        private void BoundLayout(SegmentDescription? segment = null)
        {
            for (var i = 0; i < Segments.Count; i++)
            {
                var current = Segments[i];

                if (current.Name == null) continue;
                if (segment != null && !segment.Contains(current)) continue;
                switch (current.Type)
                {
                    case SegmentType.Group:
                    case SegmentType.Component:
                    case SegmentType.Msg:
                    case SegmentType.Batch:
                        _singletons ??= [];
                        if (_arrays != null && _arrays.ContainsKey(current.Name) )
                        {
                            // this is a component but repeated within a group and we need to store all instances
                            AddToGroup(current);
                        } else if (_singletons.Remove(current.Name, out var single)) {
                            AddToGroup(single);
                            AddToGroup(current);
                        }
                        else
                        {
                            _singletons[current.Name] = current;
                        }
                        break;
                }
            }
        }
    }
}
