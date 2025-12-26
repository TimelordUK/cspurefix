using Microsoft.Extensions.ObjectPool;
using PureFix.Buffer.Segment;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Types;
using PureFix.Types.Core;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer.Ascii
{
    public class TagIndex : IDisposable
    {
        private static readonly ArrayPool<TagPos> ArrayPool = ArrayPool<TagPos>.Shared;
        private static readonly ObjectPool<TagIndex> ObjectPool = new DefaultObjectPool<TagIndex>(
            new DefaultPooledObjectPolicy<TagIndex>(), maximumRetained: 16);

        /// <summary>
        /// Rents a TagIndex from the pool and initializes it.
        /// Call Dispose() when done to return both the sorted array and TagIndex to their pools.
        /// </summary>
        public static TagIndex Rent(IContainedSet set, Tags tags, int count)
        {
            var instance = ObjectPool.Get();
            instance.Initialize(set, tags, count);
            return instance;
        }

        /// <summary>
        /// Creates a TagIndex directly (not pooled). Use Rent() for pooled instances.
        /// </summary>
        public TagIndex(IContainedSet set, Tags tags, int count)
        {
            Initialize(set, tags, count);
        }

        /// <summary>
        /// Default constructor for object pool.
        /// </summary>
        public TagIndex()
        {
            // Dictionaries are created with default capacity
            // They will grow as needed and retain capacity across reuses
        }

        private void Initialize(IContainedSet set, Tags tags, int count)
        {
            Set = set;
            _count = count;
            _disposed = false;

            // Rent array from pool (may be larger than needed)
            _sortedTagPosForwards = ArrayPool.Rent(count);

            // Copy tags into rented array
            for (var i = 0; i < count; i++)
            {
                _sortedTagPosForwards[i] = tags[i];
            }

            // Sort only the portion we're using
            Array.Sort(_sortedTagPosForwards, 0, count, Comparer<TagPos>.Create(TagPos.Compare));
            PopulateTagSpans(_sortedTagPosForwards, count);
            CalcGroups(tags, count);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            // Return sorted array to ArrayPool
            if (_sortedTagPosForwards != null)
            {
                ArrayPool.Return(_sortedTagPosForwards);
                _sortedTagPosForwards = null!;
            }

            // Clear all dictionaries (keeps capacity for reuse)
            _tagSpans.Clear();
            _noOfTag2NoOfPos.Clear();
            _tag2delim.Clear();
            _repeated.Clear();
            _names.Clear();
            _groups.Clear();
            _componentGroupWrappers.Clear();
            _cache.Clear();

            // Return self to object pool
            ObjectPool.Return(this);
        }

        private void CalcGroups(Tags tags, int count)
        {
            for (var i = 0; i < count; ++i)
            {
                var tag = tags[i];
                var (parent, _) = Set.TagToField.GetValueOrDefault(tag.Tag);
                CalcRepeated(tag);
                if (parent == null) continue;
                _names.Add(parent.Name);
                if (!IsNumInGroup(tag)) continue;
                if (parent.Fields.Count == 1)
                {
                    _componentGroupWrappers.Add(parent.Name);
                }
                CalcDelim(tags, count, tag);
            }
        }

        private bool IsNumInGroup(TagPos tag)
        {
            return _tag2delim.ContainsKey(tag.Tag) && Set.TagToSimpleDefinition.TryGetValue(tag.Tag, out var sd) && sd.IsNumInGroup;
        }

        private void CalcRepeated(TagPos tag)
        {
            if (_tagSpans.TryGetValue(tag.Tag, out var range) && range.End.Value > range.Start.Value)
            {
                _repeated.Add(tag.Tag);
            }
        }

        private void CalcDelim(Tags tags, int count, TagPos tag)
        {
            var delimPos = Math.Min(tag.Position + 1, count - 1);
            var delimTag = tags[delimPos];
            if (!_tagSpans.TryGetValue(delimTag.Tag, out _)) return;
            _tag2delim[tag.Tag] = delimTag.Tag;
            _noOfTag2NoOfPos[tag.Tag] = tag;
            if (Set.TagToField.TryGetValue(delimTag.Tag, out var pf) && pf.parent != null)
            {
                _groups[pf.field.Name] = (GroupFieldDefinition)pf.parent;
            }
        }

        /// <summary>
        /// Gets spans for all tags in the array. Creates a new dictionary.
        /// </summary>
        public static Dictionary<int, Range> GetSpans(TagPos[] sortedTagPosForwards)
        {
            return GetSpans(sortedTagPosForwards, sortedTagPosForwards.Length);
        }

        /// <summary>
        /// Gets spans for the first 'count' tags in the array. Creates a new dictionary.
        /// Used when array is rented from pool and may be larger than needed.
        /// </summary>
        public static Dictionary<int, Range> GetSpans(TagPos[] sortedTagPosForwards, int count)
        {
            var tagSpans = new Dictionary<int, Range>(count);
            PopulateTagSpans(tagSpans, sortedTagPosForwards, count);
            return tagSpans;
        }

        /// <summary>
        /// Populates tag spans into an existing dictionary.
        /// Dictionary is expected to be empty (caller should Clear() if reusing).
        /// </summary>
        private void PopulateTagSpans(TagPos[] sortedTagPosForwards, int count)
        {
            PopulateTagSpans(_tagSpans, sortedTagPosForwards, count);
        }

        private static void PopulateTagSpans(Dictionary<int, Range> tagSpans, TagPos[] sortedTagPosForwards, int count)
        {
            for (var i = 0; i < count; ++i)
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
            var tags = s.FlattenedTag;
            for (var x = 0; x < tags.Count; ++x)
            {
                var t = tags[x];
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
        public IContainedSet Set { get; private set; } = null!;
        public IReadOnlyDictionary<int, Range> TagSpans => _tagSpans;
        public IReadOnlyDictionary<string, GroupFieldDefinition> Groups => _groups;
        public IReadOnlyDictionary<int, TagPos> NoOfTag2NoOfPos => _noOfTag2NoOfPos;
        public IReadOnlyDictionary<int, int> Tag2delim => _tag2delim;
        public IReadOnlySet<int> Repeated => _repeated;
        public IReadOnlySet<string> ComponentGroupWrappers => _componentGroupWrappers;

        public TagPos this[int i] => _sortedTagPosForwards[i];

        // Mutable fields for pooling - these are reused across instances
        private TagPos[] _sortedTagPosForwards = null!;
        private int _count;
        private readonly Dictionary<int, Range> _tagSpans = new();
        private readonly Dictionary<int, TagPos> _noOfTag2NoOfPos = new();
        private readonly Dictionary<int, int> _tag2delim = new();
        private readonly HashSet<int> _repeated = new();
        private readonly HashSet<string> _names = new();
        private readonly Dictionary<string, GroupFieldDefinition> _groups = new();
        private readonly HashSet<string> _componentGroupWrappers = new();
        private readonly Dictionary<string, SegmentView> _cache = new();
        private bool _disposed;
    }
}
