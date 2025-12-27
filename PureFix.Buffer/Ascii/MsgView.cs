using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PureFix.Buffer.Segment;
using PureFix.Dictionary.Definition;
using PureFix.Types;
using PureFix.Types.Core;


namespace PureFix.Buffer.Ascii
{
    public abstract partial class MsgView
    {
        public IFixDefinitions Definitions { get; private protected set; } = null!;

        // Backing fields for lazy-loaded properties
        private SegmentDescription? _segment;
        private Structure? _structure;

        public SegmentDescription? Segment
        {
            get { EnsureStructureParsed(); return _segment; }
            protected set => _segment = value;
        }

        public Structure? Structure
        {
            get { EnsureStructureParsed(); return _structure; }
            protected set => _structure = value;
        }

        // Direct Tags access - can be set independently of Structure for lazy parsing
        public Tags? Tags { get; protected set; }

        // Start position for linear scan (skip header: BeginString, BodyLength, MsgType)
        protected int TagScanStart { get; set; } = 3;

        // do not allocate unless tags are fetched as a view may be used to fetch a component
        // without fetching a specific tag.
        protected TagPos[]? SortedTagPosForwards;
        protected Dictionary<int, Range>? TagSpans;

        /// <summary>
        /// Default constructor for object pooling. Call Initialize() after renting.
        /// </summary>
        protected MsgView() { }

        protected MsgView(IFixDefinitions definitions, SegmentDescription? segment, Structure? structure, Tags? tags = null)
        {
            Definitions = definitions;
            _segment = segment;
            _structure = structure;
            Tags = tags ?? structure?.Tags;
        }

        /// <summary>
        /// Resets base view state for pooling reuse.
        /// </summary>
        protected void ResetBase()
        {
            _segment = null;
            _structure = null;
            Tags = null;
            TagScanStart = 3;
            SortedTagPosForwards = null;
            TagSpans = null;
        }

        /// <summary>
        /// Override in derived classes to trigger lazy structure parsing on-demand.
        /// Called automatically when component/group access is needed.
        /// </summary>
        protected virtual void EnsureStructureParsed() { }

        /*
         * sort the local copy of tags sliced for this view (i.e. only ones bounded)
         * and compute a range within that sorted set of start end positions as some
         * tags for repeated groups will have more than one instance.
         */

        private void EnumerateSpan()
        {
            if (TagSpans != null) return;
            if (Tags == null) return;
            if (Structure == null) return;
            if (Segment == null) return;

            SortedTagPosForwards = Structure.Value.GetSortedTags(Segment);
            TagSpans = TagIndex.GetSpans(SortedTagPosForwards);
        }

        // "BeginString" or 8
        public abstract DateTime? GetDateTime(int tag);
        public abstract TimeOnly? GetTimeOnly(int tag);
        public abstract DateOnly? GetDateOnly(int tag);
        public abstract int? GetInt32(string name);
        public abstract int? GetInt32(int tag);
        public abstract double? GetDouble(int tag);
        public abstract double? GetDouble(string name);
        public abstract bool? GetBool(string name);
        public abstract bool? GetBool(int tag);
        public abstract decimal? GetDecimal(string name);
        public abstract decimal? GetDecimal(int tag);
        public abstract byte[]? GetByteArray(string name);
        public abstract byte[]? GetByteArray(int tag);
        public abstract Memory<byte>? GetMemory(int tag);
        public abstract MonthYear? GetMonthYear(int tag);
        public abstract string BufferString();

        // ===== Zero-allocation span-based API =====

        /// <summary>
        /// Gets the raw bytes for a tag value as a ReadOnlySpan. Zero allocation.
        /// Returns empty span if tag not found.
        /// </summary>
        public abstract ReadOnlySpan<byte> GetSpan(int tag);

        /// <summary>
        /// Tries to get the raw bytes for a tag value. Zero allocation.
        /// </summary>
        /// <returns>true if tag found, false otherwise</returns>
        public abstract bool TryGetSpan(int tag, out ReadOnlySpan<byte> value);

        /// <summary>
        /// Compares a tag's value to an expected byte sequence. Zero allocation.
        /// Returns false if tag not found.
        /// </summary>
        public abstract bool IsTagEqual(int tag, ReadOnlySpan<byte> expected);

        /// <summary>
        /// Compares a tag's value to an expected string (converted to UTF8). Minimal allocation.
        /// Returns false if tag not found.
        /// </summary>
        public bool IsTagEqual(int tag, string expected)
        {
            Span<byte> bytes = stackalloc byte[expected.Length];
            System.Text.Encoding.ASCII.GetBytes(expected, bytes);
            return IsTagEqual(tag, bytes);
        }

        /// <summary>
        /// Checks if a tag's value starts with a prefix. Zero allocation.
        /// Returns false if tag not found.
        /// </summary>
        public abstract bool TagStartsWith(int tag, ReadOnlySpan<byte> prefix);

        /// <summary>
        /// Finds which value in a list matches the tag. Zero allocation.
        /// Returns the index of the matching value (0-based), or -1 if tag not found or no match.
        /// </summary>
        public abstract int MatchTag(int tag, ReadOnlySpan<byte> value0, ReadOnlySpan<byte> value1);

        /// <summary>
        /// Finds which value in a list matches the tag. Zero allocation.
        /// Returns the index of the matching value (0-based), or -1 if tag not found or no match.
        /// </summary>
        public abstract int MatchTag(int tag, ReadOnlySpan<byte> value0, ReadOnlySpan<byte> value1, ReadOnlySpan<byte> value2);

        /// <summary>
        /// Finds which value in a list matches the tag. Zero allocation.
        /// Returns the index of the matching value (0-based), or -1 if tag not found or no match.
        /// </summary>
        public abstract int MatchTag(int tag, ReadOnlySpan<byte> value0, ReadOnlySpan<byte> value1, ReadOnlySpan<byte> value2, ReadOnlySpan<byte> value3);

        // ===== Try-pattern methods (avoid nullable boxing) =====

        /// <summary>
        /// Tries to get an int32 value for a tag. Avoids nullable boxing.
        /// </summary>
        public abstract bool TryGetInt32(int tag, out int value);

        /// <summary>
        /// Tries to get a long value for a tag. Avoids nullable boxing.
        /// </summary>
        public abstract bool TryGetInt64(int tag, out long value);

        /// <summary>
        /// Tries to get a double value for a tag. Avoids nullable boxing.
        /// </summary>
        public abstract bool TryGetDouble(int tag, out double value);

        /// <summary>
        /// Tries to get a decimal value for a tag. Avoids nullable boxing.
        /// </summary>
        public abstract bool TryGetDecimal(int tag, out decimal value);

        /// <summary>
        /// Tries to get a boolean value for a tag. Avoids nullable boxing.
        /// </summary>
        public abstract bool TryGetBool(int tag, out bool value);

        // ===== Lightweight repeated tag iteration (no structure parse) =====

        /// <summary>
        /// Counts occurrences of a tag using linear scan. Does NOT trigger structure parsing.
        /// O(n) where n is total number of tags.
        /// </summary>
        public int CountTag(int tag)
        {
            if (Tags == null) return 0;
            var count = 0;
            var total = Tags.NextTagPos;
            for (var i = 0; i < total; i++)
            {
                if (Tags[i].Tag == tag) count++;
            }
            return count;
        }

        /// <summary>
        /// Invokes a callback for each occurrence of a tag. Does NOT trigger structure parsing.
        /// The callback receives the position index in the Tags array.
        /// Returns the number of occurrences found.
        /// </summary>
        public int ForEachTagPosition(int tag, Action<int> callback)
        {
            if (Tags == null) return 0;
            var count = 0;
            var total = Tags.NextTagPos;
            for (var i = 0; i < total; i++)
            {
                if (Tags[i].Tag == tag)
                {
                    callback(i);
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Gets an enumerator for all positions of a tag. Does NOT trigger structure parsing.
        /// Zero allocation - uses a struct enumerator.
        /// </summary>
        public TagPositionEnumerable EnumerateTagPositions(int tag) => new(Tags, tag);

        /// <summary>
        /// Zero-allocation enumerator for tag positions.
        /// </summary>
        public readonly struct TagPositionEnumerable
        {
            private readonly Tags? _tags;
            private readonly int _tag;

            public TagPositionEnumerable(Tags? tags, int tag)
            {
                _tags = tags;
                _tag = tag;
            }

            public TagPositionEnumerator GetEnumerator() => new(_tags, _tag);
        }

        /// <summary>
        /// Zero-allocation enumerator for tag positions.
        /// </summary>
        public ref struct TagPositionEnumerator
        {
            private readonly Tags? _tags;
            private readonly int _tag;
            private readonly int _count;
            private int _index;

            public TagPositionEnumerator(Tags? tags, int tag)
            {
                _tags = tags;
                _tag = tag;
                _count = tags?.NextTagPos ?? 0;
                _index = -1;
            }

            public int Current => _index;

            public bool MoveNext()
            {
                if (_tags == null) return false;
                while (++_index < _count)
                {
                    if (_tags[_index].Tag == _tag) return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Gets all positions of a tag without structure parsing, returning as a span.
        /// Uses stackalloc for small counts, falls back to array allocation for larger.
        /// For performance-critical code, prefer EnumerateTagPositions or ForEachTagPosition.
        /// </summary>
        public int[] GetAllTagPositions(int tag)
        {
            if (Tags == null) return [];

            // First pass: count occurrences
            var count = CountTag(tag);
            if (count == 0) return [];

            // Second pass: collect positions
            var positions = new int[count];
            var idx = 0;
            var total = Tags.NextTagPos;
            for (var i = 0; i < total && idx < count; i++)
            {
                if (Tags[i].Tag == tag)
                {
                    positions[idx++] = i;
                }
            }
            return positions;
        }

        public int GroupCount()
        {
            EnsureStructureParsed();
            var count = Segment?.DelimiterPositions.Count ?? 0;
            return count;
        }

        public string? GetString(string name)
        {
            return Definitions.Simple.TryGetValue(name, out var typed) ? GetString(typed.Tag) : null;
        }

        public string? GetString(int tag)
        {
            var position = GetPosition(tag);
            return position < 0 ? null : StringAtPosition(position);
        }

        public string?[]? GetStrings()
        {
            return AllStrings();
        }

        public string?[]? GetStrings(string name)
        {
            return Definitions.Simple.TryGetValue(name, out var typed) ? GetStrings(typed.Tag) : null;
        }

        public string?[]? GetStrings(int tag)
        {
            var range = GetPositions(tag);
            if (range == null) return null;
            if (SortedTagPosForwards == null) return null;
            var i = range.Value.Start.Value;
            var j = range.Value.End.Value;
            var numbers = Enumerable.Range(i, j - i + 1);
            return numbers.Select(k => SortedTagPosForwards[k].Position).Select(StringAtPosition).ToArray();
        }

    /**
     * if this view represents a repeated group then return a sub view representing
     * this instance of repeated group.
     * @param i the index to return i.e. 0 is first within repeated group
     */
        public MsgView? GetGroupInstance(int i)
        {
            EnsureStructureParsed();
            var instance = Segment?.GetInstance(i);
            return instance == null ? null : Create(instance);
        }

        public MsgView? this[int i] => GetGroupInstance(i);

        public MsgView? GetView(string name)
        {
            EnsureStructureParsed();

            // As this is the most common case we'll optimize for it
            if (name.IndexOf('.') == -1)
            {
                return Process(this, name);
            }

            var parts = name.Split('.');
            return parts.Aggregate(this, static (a, current) => Process(a, current)!);

            static MsgView? Process(MsgView a, string current)
            {
                a.EnsureStructureParsed();
                var subStructure = a.Structure;
                if (a.Segment == null)
                {
                    return a;
                }

                var singleton = subStructure?.FirstContainedWithin(current, a.Segment);
                if (singleton != null)
                {
                    return a.Create(singleton);
                }

                if (a?.Segment?.Set?.LocalNameToField.TryGetValue(current, out var component) ?? false)
                {
                    var abbreviation = subStructure?.FirstContainedWithin(component.Name, a.Segment);
                    if (abbreviation != null)
                    {
                        return a.Create(abbreviation);
                    }
                }
                return null;
            }
        }

        protected int GetPosition(int tag)
        {
            // Fast path: use indexed lookup if Structure is available
            if (Structure != null)
            {
                if (TagSpans == null) EnumerateSpan();
                if (TagSpans != null && SortedTagPosForwards != null)
                {
                    if (TagSpans.TryGetValue(tag, out var r))
                    {
                        return SortedTagPosForwards[r.Start.Value].Position;
                    }
                    return -1;
                }
            }

            // Slow path: linear scan for simple field access without structure
            // This is efficient for small session messages (Heartbeat, Logon, etc.)
            return LinearScanForTag(tag);
        }

        /// <summary>
        /// Linear scan through tags to find a tag by number.
        /// O(n) but avoids all structure parsing allocation.
        /// For small messages like Heartbeat (10 fields), this is faster than building indexes.
        /// </summary>
        private int LinearScanForTag(int tag)
        {
            if (Tags == null) return -1;

            var count = Tags.NextTagPos;
            // Start from TagScanStart to skip header fields (BeginString, BodyLength, MsgType)
            // which have fixed positions anyway
            for (var i = TagScanStart; i < count; i++)
            {
                if (Tags[i].Tag == tag)
                {
                    return i;
                }
            }

            // Also check header fields if not found in body
            for (var i = 0; i < TagScanStart && i < count; i++)
            {
                if (Tags[i].Tag == tag)
                {
                    return i;
                }
            }

            return -1;
        }

        private string?[]? AllStrings()
        {
            EnsureStructureParsed();
            if (Segment == null) return null;
            var range = new int[Segment.EndPosition - Segment.StartPosition +1];
            var j = 0;
            for (var i = Segment.StartPosition; i <= Segment.EndPosition; ++i)
            {
                range[j++] = i;
            }
            return range.Select(StringAtPosition).ToArray();
        }

        protected Range? GetPositions(int tag)
        {
            EnsureStructureParsed();
            EnumerateSpan();
            if (TagSpans == null) return null;
            if (!TagSpans.TryGetValue(tag, out var r))
            {
                return null;
            }

            return r;
        }

        protected static string AsToken(SimpleFieldDefinition? field, string val, int i, int count, TagPos tagpos)
        {
            const int perLine = 2;
            var newLine = Environment.NewLine;
            // [280] 814 (ApplQueueResolution) = 2[OverlayLast][281] 10 (CheckSum) = 80
            string desc;
            string name;
            if (field != null)
            {
                name = field.Name;
                desc = field.IsEnum ? $"{val} [{field.ResolveEnum(val)}]" : $"{val}";
            }
            else
            {
                desc = $"{val}";
                name = "unknown";
            }
            string delimiter;
            if (i == 1 || (i < count && i % perLine - 1 == 0))
            {
                delimiter = newLine;
            }
            else
            {
                delimiter = i < count ? ", " : "";
            }

            return $"[{i}] {tagpos.Tag} ({name}) = {desc}{delimiter}";
        }


        /**
         * easy human-readable format showing each field, its position, value and resolved
         * enum.
         */
        public override string ToString()
        {
            return Stringify(AsToken);
        }

        private string Stringify(Func<SimpleFieldDefinition, string, int, int, TagPos, string> getToken)
        {
            EnsureStructureParsed();
            if (Structure == null) return "";
            var buffer = new StringBuilder();
            var tags = Structure.Value.Tags;
            if (Segment == null) return "";
            var count = Segment.EndPosition - Segment.StartPosition;
            var simple = Definitions.TagToSimple;

            for (var i = Segment.StartPosition; i <= Segment.EndPosition; ++i)
            {
                var tagPos = tags[i];
                simple.TryGetValue(tagPos.Tag, out var field);
                var val = StringAtPosition(i) ?? "";
                // [0] 8 (BeginString) = FIX4.4
                var token = field != null
                    ? getToken(field, val, i - Segment.StartPosition, count, tagPos)
                    : $"[{i}] {tagPos.Tag} (unknown) = {val}, ";
                buffer.Append(token);
            }

            return buffer.ToString();
        }

        protected abstract MsgView Create(SegmentDescription singleton);
        protected abstract string? StringAtPosition(int position);

        public abstract int Checksum();
    }
}
