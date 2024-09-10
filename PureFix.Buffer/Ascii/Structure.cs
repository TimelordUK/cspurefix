using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Segment;
using PureFix.Tag;

namespace PureFix.Buffer.Ascii
{
    public record struct Structure 
    {
        private Dictionary<string, SegmentDescription>? _components;
        // do not create unless needed
        private Dictionary<string, List<SegmentDescription>>? _groups;
        public IReadOnlyList<SegmentDescription> Segments { get; }

        public Tags Tags { get; }

        public Structure(Tags tags, IReadOnlyList<SegmentDescription> segments)
        {
            Tags = tags;
            Segments = segments;
            BoundLayout();
        }

        public readonly SegmentDescription? Msg() => Segments.Count >= 2 ? Segments[^2] : null;

        public readonly SegmentDescription? FirstContainedWithin(string name, SegmentDescription segment)
        {
            if (_components == null)
            {
                if (_groups != null && _groups.TryGetValue(name, out var instances))
                {
                    return instances.FirstOrDefault(segment.Contains);
                }
                return null;
            }

            if (!_components.TryGetValue(name, out var component))
            {
                if (_groups != null && _groups.TryGetValue(name, out var instances))
                {
                    return instances.FirstOrDefault(segment.Contains);
                }
            }
            else
            {
                if (segment.Contains(component)) return component;
            }

            return null;
        }

        private void BoundLayout(SegmentDescription? segment = null)
        {
            foreach (var current in Segments)
            {
                if (current.Name == null) continue;
                if (segment != null && !segment.Contains(current)) continue;
                switch (current.Type)
                {
                    case SegmentType.Group:
                    {
                        _groups ??= [];
                        if (!_groups.TryGetValue(current.Name, out var instances))
                        {
                            _groups[current.Name] = instances = [];
                        }

                        instances.Add(current);
                        break;
                    }

                    case SegmentType.Component:
                    case SegmentType.Msg:
                    case SegmentType.Batch:
                        _components ??= [];
                        _components[current.Name] = current;
                        break;
                }
            }
        }
    }
}
