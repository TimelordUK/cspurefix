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
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Types;


namespace PureFix.Buffer.Ascii
{
    public class TagIndex
    {
        public TagIndex(IContainedSet set, TagPos[] tags)
        {
            _set = set;
            _sortedTagPosForwards = tags[..tags.Length];
            Array.Sort(_sortedTagPosForwards, TagPos.Compare);
            _tagSpans = new Dictionary<int, Range>(tags.Length);

            Index();
            CalcGroups(tags);
        }

        private void CalcGroups(TagPos[] tags)
        {
            for (var i = 0; i < tags.Length; ++i)
            {
                var tag = tags[i];
                var (parent, field) = _set.TagToField.GetValueOrDefault(tag.Tag);
                _names.Add(parent.Name);
                if (!_tag2delim.ContainsKey(tag.Tag) && _set.TagToSimpleDefinition.TryGetValue(tag.Tag, out var sd) && sd.Type == "NUMINGROUP")
                {
                    if (parent.Fields.Count == 1)
                    {
                        _componentGroupWrappers.Add(parent.Name);
                    }
                    CalcDelim(tags, tag);
                }
            }
        }

        private void CalcDelim(TagPos[] tags, TagPos tag)
        {
            var delimPos = Math.Min(tag.Position + 1, tags.Length - 1);
            var delimTag = tags[delimPos];
            if (_tagSpans.TryGetValue(delimTag.Tag, out var _))
            {
                _tag2delim[tag.Tag] = delimTag.Tag;
                _noOfTag2NoOfPos[tag.Tag] = tag;
                if (_set.TagToField.TryGetValue(delimTag.Tag, out var pf))
                {
                    _groups[pf.field.Name] = (GroupFieldDefinition)pf.parent;
                }
            }
        }
    

        private void Index()
        {
            for (var i = 0; i < _sortedTagPosForwards.Length; ++i)
            {
                var t = _sortedTagPosForwards[i];
                if (_tagSpans.TryGetValue(t.Tag, out var c))
                {
                    _tagSpans[t.Tag] = new Range(c.Start, i);
                    _repeated.Add(t.Tag);
                }
                else
                {
                    _tagSpans[t.Tag] = new Range(i, i);
                }
            }
        }

        public SegmentView? GetInstance(string name)
        {
            if (_cache.TryGetValue(name, out var singleton))
            {
                return singleton;
            }

            var s = _set.NameToSet.GetValueOrDefault(name);
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

        public IReadOnlySet<string> Names { get { return _names; } }
        public IContainedSet Set { get { return _set; } }
        public IReadOnlyDictionary<int, Range> TagSpans => _tagSpans;
        public IReadOnlyDictionary<string, GroupFieldDefinition> Groups => _groups;
        public IReadOnlyDictionary<int, TagPos> NoOfTag2NoOfPos => _noOfTag2NoOfPos;
        public IReadOnlyDictionary<int, int> Tag2delim => _tag2delim;
        public IReadOnlySet<int> Repeated => _repeated;
        public IReadOnlySet<string> ComponentGroupWrappers => _componentGroupWrappers;

        public TagPos this[int i]
        {
            get
            {
                return _sortedTagPosForwards[i];
            }
        }

        private readonly TagPos[] _sortedTagPosForwards;
        private readonly Dictionary<int, Range> _tagSpans;
        private readonly Dictionary<int, TagPos> _noOfTag2NoOfPos = [];
        private readonly Dictionary<int, int> _tag2delim = [];
        private readonly HashSet<int> _repeated = [];
        private readonly HashSet<string> _names = [];
        private readonly Dictionary<string, GroupFieldDefinition> _groups = [];
        private readonly HashSet<string> _componentGroupWrappers = [];
        private readonly IContainedSet _set;
        private readonly Dictionary<string, SegmentView> _cache = [];
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
