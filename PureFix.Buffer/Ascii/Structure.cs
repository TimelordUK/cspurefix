using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Segment;

namespace PureFix.Buffer.Ascii
{
    public class Structure
    {
        private readonly Dictionary<string, SegmentDescription> _components = new();
        private readonly Dictionary<string, List<SegmentDescription>> _groups = new();

        public IReadOnlyDictionary<string, List<SegmentDescription>> Groups => _groups;
        public IReadOnlyDictionary<string, SegmentDescription> Components => _components;

        public IReadOnlyList<SegmentDescription> Segments { get; }
        public Structure(IReadOnlyList<SegmentDescription> segments)
        {
            Segments = segments;
            BoundLayout();
        }

        public SegmentDescription? Msg() => Segments.Count >= 2 ? Segments[^2] : null;

        public SegmentDescription? FirstContainedWithin(string name, SegmentDescription segment)
        {
            if (!_components.TryGetValue(name, out var component))
            {
                if (_groups.TryGetValue(name, out var instances))
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
                if (segment != null && !segment.Contains(current)) continue;
                if (current.Name == null) continue;
                if (_groups.TryGetValue(current.Name, out var instances))
                {
                    instances.Add(current);
                }
                else if (_components.Remove(current.Name, out var component))
                {
                    // assume a group 
                    _groups[current.Name] = [component, current];
                }
            }
        }
    }
}
