using PureFix.Buffer.Segment;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer.Ascii
{
    public class TagIndex
    {
        public TagIndex(IContainedSet set, TagPos[] tags)
        {
            Set = set;
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
                var (parent, field) = Set.TagToField.GetValueOrDefault(tag.Tag);
                if (parent == null) continue;
                _names.Add(parent.Name);
                if (!_tag2delim.ContainsKey(tag.Tag) && Set.TagToSimpleDefinition.TryGetValue(tag.Tag, out var sd) && sd.Type == "NUMINGROUP")
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
                if (Set.TagToField.TryGetValue(delimTag.Tag, out var pf) && pf.parent != null)
                {
                    _groups[pf.field.Name] = (GroupFieldDefinition)pf.parent;
                }
            }
        }

        public static Dictionary<int, Range> GetSpans(TagPos[] sortedTagPosForwards)
        {
            // We can make a worse case guess at the size of span dictionary
            // But it'll save reallocating 
            var tagSpans = new Dictionary<int, Range>(sortedTagPosForwards.Length);

            for (var i = 0; i < sortedTagPosForwards.Length; ++i)
            {
                var t = sortedTagPosForwards[i];
                if (tagSpans.TryGetValue(t.Tag, out var c))
                {
                    tagSpans[t.Tag] = new Range(c.Start, i);
                }
                else
                {
                    tagSpans[t.Tag] = new Range(i, i);
                }
            }
            return tagSpans;
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

            var s = Set.NameToSet.GetValueOrDefault(name);
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

        public IReadOnlySet<string> Names => _names;
        public IContainedSet Set { get; }

        public IReadOnlyDictionary<int, Range> TagSpans => _tagSpans;
        public IReadOnlyDictionary<string, GroupFieldDefinition> Groups => _groups;
        public IReadOnlyDictionary<int, TagPos> NoOfTag2NoOfPos => _noOfTag2NoOfPos;
        public IReadOnlyDictionary<int, int> Tag2delim => _tag2delim;
        public IReadOnlySet<int> Repeated => _repeated;
        public IReadOnlySet<string> ComponentGroupWrappers => _componentGroupWrappers;

        public TagPos this[int i] => _sortedTagPosForwards[i];

        private readonly TagPos[] _sortedTagPosForwards;
        private readonly Dictionary<int, Range> _tagSpans;
        private readonly Dictionary<int, TagPos> _noOfTag2NoOfPos = [];
        private readonly Dictionary<int, int> _tag2delim = [];
        private readonly HashSet<int> _repeated = [];
        private readonly HashSet<string> _names = [];
        private readonly Dictionary<string, GroupFieldDefinition> _groups = [];
        private readonly HashSet<string> _componentGroupWrappers = [];
        private readonly Dictionary<string, SegmentView> _cache = [];
    }
}
