using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Segment;
using PureFix.Dictionary.Contained;
using PureFix.Types;


namespace PureFix.Buffer.Ascii
{
    public class Structure2
    {
        private readonly TagPos[] _sortedTagPosForwards;
        private readonly Dictionary<int, Range> _tagSpans;
        private readonly Dictionary<string, SegmentView> _cache = [];
        private readonly SegmentView _top;


        public IReadOnlyList<SegmentView>? GetInstances(string name)
        {
            return new List<SegmentView>();
        }

        public SegmentView? GetInstance(string name)
        {
            if (_cache.TryGetValue(name, out var singleton))
            {
                return singleton;
            }

            var s = _top.Set.NameToSet.GetValueOrDefault(name);
            if (s == null) return null;
            var res = new SegmentView(name, s);
            for (var x = 0; x < s.FlattenedTag.Count; ++x)
            {
                var t = s.FlattenedTag[x];
                if (!_tagSpans.TryGetValue(t, out var range)) continue;
                var start = range.Start.Value;
                var end = range.End.Value;
                if (start == end)
                {
                    res.Add(_sortedTagPosForwards[start]);
                }
                else
                {
                    for (var j = start; j <= end; ++j)
                    {
                        res.Add(_sortedTagPosForwards[j]);
                    }
                }
            }
            _cache[name] = res;
            return res;
        }

        public Structure2(IContainedSet set, Tags tags, int last)
        {
            var msg = new SegmentView(set.Name, set);
            _cache[set.Name] = msg;
            for (var i = 0; i <= last; ++i)
            {
                var tag = tags[i];
                msg.Add(tag);
            }

            _top = msg;
            _sortedTagPosForwards = tags[..last];
            Array.Sort(_sortedTagPosForwards, TagPos.Compare);

            // We can make a worse case guess at the size of span dictionary
            // But it'll save reallocating 
            _tagSpans = new Dictionary<int, Range>(_sortedTagPosForwards.Length);

           Index();
        }

        private void Index()
        {
            for (var i = 0; i < _sortedTagPosForwards.Length; ++i)
            {
                var t = _sortedTagPosForwards[i];
                if (_tagSpans.TryGetValue(t.Tag, out var c))
                {
                    _tagSpans[t.Tag] = new Range(c.Start, i);
                }
                else
                {
                    _tagSpans[t.Tag] = new Range(i, i);
                }
            }
        }
    }

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
