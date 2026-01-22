using PureFix.Buffer.Segment;
using PureFix.Dictionary.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.Core;
using Microsoft.Extensions.ObjectPool;
using System.Xml.Linq;
using System.Threading.Channels;

namespace PureFix.Buffer.Ascii
{
    public class AsciiView : MsgView
    {
        private static readonly ObjectPool<AsciiView> Pool = new DefaultObjectPool<AsciiView>(
            new DefaultPooledObjectPolicy<AsciiView>(), maximumRetained: 16);

        public int Ptr { get; private set; }
        public int Delimiter { get; private set; }
        public int WriteDelimiter { get; private set; }
        public StoragePool.Storage Storage { get; private set; } = null!;
        public ElasticBuffer Buffer => Storage.Buffer;

        // For lazy structure parsing - accepts any ISegmentParser implementation
        private ISegmentParser? _segmentParser;
        private string? _msgType;
        private bool _structureParsed;
        private bool _isPooled;

        // For string interning
        private ISessionStringStore? _stringStore;

        // Tags that should use the session string store (CompIDs)
        private static readonly HashSet<int> SessionInternableTags = new() { 49, 56, 50, 57 };

        // BeginString tag uses static pool
        private const int TagBeginString = 8;

        public override string BufferString()
        {
            return Buffer.ToString();
        }

        /// <summary>
        /// Rents a view from the pool and initializes it.
        /// Call Return() when done to return the view to the pool.
        /// </summary>
        internal static AsciiView Rent(
            IFixDefinitions definitions,
            StoragePool.Storage storage,
            int ptr,
            int delimiter,
            int writeDelimiter,
            ISegmentParser? segmentParser,
            string? msgType,
            ISessionStringStore? stringStore = null)
        {
            var view = Pool.Get();
            view.Initialize(definitions, null, storage, null, ptr, delimiter, writeDelimiter, segmentParser, msgType, stringStore);
            view._isPooled = true;
            return view;
        }

        /// <summary>
        /// Returns this view to the pool if it was rented.
        /// Safe to call on non-pooled views (e.g., cloned views).
        /// </summary>
        public void Return()
        {
            if (!_isPooled) return;
            _isPooled = false;
            Reset();
            Pool.Return(this);
        }

        /// <summary>
        /// Default constructor for object pooling.
        /// </summary>
        public AsciiView() { }

        public AsciiView (
            IFixDefinitions definitions,
            SegmentDescription? segment,
            StoragePool.Storage storage,
            Structure? structure,
            int ptr,
            int delimiter,
            int writeDelimiter,
            ISegmentParser? segmentParser = null,
            string? msgType = null) : base(definitions, segment, structure, storage.Locations)
        {
            Ptr = ptr;
            Delimiter = delimiter;
            WriteDelimiter = writeDelimiter;
            Storage = storage;
            _segmentParser = segmentParser;
            _msgType = msgType;
            _structureParsed = structure != null;
            _isPooled = false;
        }

        private void Initialize(
            IFixDefinitions definitions,
            SegmentDescription? segment,
            StoragePool.Storage storage,
            Structure? structure,
            int ptr,
            int delimiter,
            int writeDelimiter,
            ISegmentParser? segmentParser,
            string? msgType,
            ISessionStringStore? stringStore = null)
        {
            Definitions = definitions;
            Segment = segment;
            Structure = structure;
            Tags = storage.Locations;
            Ptr = ptr;
            Delimiter = delimiter;
            WriteDelimiter = writeDelimiter;
            Storage = storage;
            _segmentParser = segmentParser;
            _msgType = msgType;
            _structureParsed = structure != null;
            _stringStore = stringStore;
        }

        private void Reset()
        {
            ResetBase();
            _segmentParser = null;
            _msgType = null;
            _structureParsed = false;
            Storage = null!;
            _stringStore = null;  // Don't hold reference after return to pool
        }

        /// <summary>
        /// Triggers structure parsing on-demand if not already parsed.
        /// Called automatically when component/group access is needed.
        /// </summary>
        protected override void EnsureStructureParsed()
        {
            if (_structureParsed) return;
            _structureParsed = true;

            if (_segmentParser == null || _msgType == null || Tags == null) return;

            var structure = _segmentParser.Parse(_msgType, Tags, Tags.NextTagPos - 1);
            if (structure != null)
            {
                Structure = structure;
                Segment = structure.Value.Msg();
            }
            else
            {
                // Fallback for unknown message types
                Structure = new Structure(Tags, []);
                Segment = new SegmentDescription("unknown", Tags[0].Tag, null, 0, 1, SegmentType.Unknown)
                {
                    EndPosition = Tags.NextTagPos - 1
                };
            }
        }

    
        protected override MsgView Create(SegmentDescription singleton)
        {
            return new AsciiView(Definitions,
                singleton,
                Storage,
                Structure,
                Ptr,
                Delimiter,
                WriteDelimiter);
        }

        private TagPos? GetTag(int position)
        {
            if (Tags == null) return null;
            if (position < 0 || position >= Tags.NextTagPos)
            {
                return null;
            }
            return Tags[position];
        }

        public override int Checksum()
        {
            if (Tags == null) return -1;
            var t = GetPosition((int)MsgTag.CheckSum);
            if (t < 1) return -1;
            var prev = Tags[t - 1];
            var tp = prev.Start + prev.Len + 1;
            var cs = Buffer.Sum(tp);
            var delimiter = Delimiter;
            var writeDelimiter = WriteDelimiter;
            if (writeDelimiter != delimiter)
            {
                var changes = Tags.NextTagPos - 1;
                cs -= changes * writeDelimiter;
                cs += changes * delimiter;
            }
            return cs % 256;
        }
  
        protected override string? StringAtPosition(int position)
        {
            var tagPos = GetTag(position);
            if (tagPos == null) return null;

            var tag = Tags![position].Tag;
            var start = tagPos.Value.Start;
            var len = tagPos.Value.Len;

            // BeginString (tag 8): use static pool - zero allocation for known versions
            if (tag == TagBeginString)
            {
                // GetSpan uses inclusive end, so end = start + len - 1
                var span = Buffer.GetSpan(start, start + len - 1);
                return BeginStringPool.Intern(span);
            }

            // Session constants (CompIDs): use session store if available
            if (_stringStore != null && SessionInternableTags.Contains(tag))
            {
                var span = Buffer.GetSpan(start, start + len - 1);
                return _stringStore.GetOrAdd(tag, span);
            }

            // Default: allocate new string (GetString uses exclusive end)
            return Buffer.GetString(start, start + len);
        }

        protected long? LongAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetWholeNumber(tag.Value.Start, tag.Value.End);
        }

        protected double? FloatAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetFloat(tag.Value.Start, tag.Value.End);
        }

        protected decimal? DecimalAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetDecimal(tag.Value.Start, tag.Value.End);
        }

        protected Memory<byte>? BufferAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetBuffer(tag.Value.Start, tag.Value.End);
        }

        protected bool? BoolAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetBoolean(tag.Value.Start);
        }

        protected DateOnly? UtcDateOnlyAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetUtcDateOnly(tag.Value.Start, tag.Value.End);
        }

        protected TimeOnly? UtcTimeOnlyAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetTimeOnly(tag.Value.Start, tag.Value.End);
        }

        protected DateOnly? LocalDateOnlyAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetLocalDateOnly(tag.Value.Start, tag.Value.End);
        }

        protected DateTime? UtcTimestampAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetUtcTimeStamp(tag.Value.Start, tag.Value.End);
        }

        public override int? GetInt32(string name)
        {
            return Definitions.Simple.TryGetValue(name, out var typed) ? GetInt32(typed.Tag) : null;
        }

        public override int? GetInt32(int tag)
        {
            var position = GetPosition(tag);
            if (position < 0)
            {
                return null;
            }

            var value = LongAtPosition(position);
            return value is null ? null : (int)value.Value;
        }

        public override double? GetDouble(string name)
        {
            return Definitions.Simple.TryGetValue(name, out var typed) ? GetDouble(typed.Tag) : null;
        }

        public override double? GetDouble(int tag)
        {
            return GetPosition(tag) switch
            {
                < 0 => default,
                var position => FloatAtPosition(position)
            };
        }

        public override bool? GetBool(string name)
        {
            return Definitions.Simple.TryGetValue(name, out var typed) ? GetBool(typed.Tag) : null;
        }

        public override bool? GetBool(int tag)
        {
            return GetPosition(tag) switch
            {
                < 0 => default,
                var position => BoolAtPosition(position)
            };
        }

        public override decimal? GetDecimal(string name)
        {
            return Definitions.Simple.TryGetValue(name, out var typed) ? GetDecimal(typed.Tag) : null;
        }

        public override decimal? GetDecimal(int tag)
        {
            return GetPosition(tag) switch
            {
                < 0 => default,
                var position => DecimalAtPosition(position)
            };
        }

        public override byte[]? GetByteArray(string name)
        {
            return Definitions.Simple.TryGetValue(name, out var typed) ? GetByteArray(typed.Tag) : null;
        }

        public override byte[]? GetByteArray(int tag)
        {
            return GetPosition(tag) switch
            {
                < 0 => default,
                var position => BufferAtPosition(position)?.ToArray()
            };
        }

        public override Memory<byte>? GetMemory(int tag)
        {
            return GetPosition(tag) switch
            {
                < 0 => default,
                var position => BufferAtPosition(position)
            };
        }

        public override DateTime? GetDateTime(int tag)
        {
            var position = GetPosition(tag);
            if (position < 0)
            {
                return null;
            }

            return UtcTimestampAtPosition(position);
        } 

        public override TimeOnly? GetTimeOnly(int tag)
        {
            var position = GetPosition(tag);
            if (position < 0)
            {
                return null;
            }

            return UtcTimeOnlyAtPosition(position);
        }

        public override DateOnly? GetDateOnly(int tag)
        {
            var position = GetPosition(tag);
            if (position < 0)
            {
                return null;
            }

            var sf = Definitions.TagToSimple.GetValueOrDefault(tag);
            if (sf == null) return null;

            switch (sf.TagType)
            {
                case TagType.UtcDateOnly:
                    return UtcDateOnlyAtPosition(position);

                case TagType.LocalDate:
                    return LocalDateOnlyAtPosition(position);

                default:
                    return null;
            }
        }

        public override MonthYear? GetMonthYear(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetMonthYear(tag.Value.Start, tag.Value.End);
        }

        // ===== Zero-allocation span-based API implementations =====

        public override ReadOnlySpan<byte> GetSpan(int tag)
        {
            var position = GetPosition(tag);
            if (position < 0) return ReadOnlySpan<byte>.Empty;
            var tagPos = GetTag(position);
            if (tagPos == null) return ReadOnlySpan<byte>.Empty;
            return Buffer.GetSpan(tagPos.Value.Start, tagPos.Value.End);
        }

        public override bool TryGetSpan(int tag, out ReadOnlySpan<byte> value)
        {
            var position = GetPosition(tag);
            if (position < 0)
            {
                value = ReadOnlySpan<byte>.Empty;
                return false;
            }
            var tagPos = GetTag(position);
            if (tagPos == null)
            {
                value = ReadOnlySpan<byte>.Empty;
                return false;
            }
            value = Buffer.GetSpan(tagPos.Value.Start, tagPos.Value.End);
            return true;
        }

        public override bool IsTagEqual(int tag, ReadOnlySpan<byte> expected)
        {
            var position = GetPosition(tag);
            if (position < 0) return false;
            var tagPos = GetTag(position);
            if (tagPos == null) return false;
            return Buffer.SequenceEqual(tagPos.Value.Start, tagPos.Value.End, expected);
        }

        public override bool TagStartsWith(int tag, ReadOnlySpan<byte> prefix)
        {
            var position = GetPosition(tag);
            if (position < 0) return false;
            var tagPos = GetTag(position);
            if (tagPos == null) return false;
            return Buffer.StartsWith(tagPos.Value.Start, tagPos.Value.End, prefix);
        }

        public override int MatchTag(int tag, ReadOnlySpan<byte> value0, ReadOnlySpan<byte> value1)
        {
            var position = GetPosition(tag);
            if (position < 0) return -1;
            var tagPos = GetTag(position);
            if (tagPos == null) return -1;
            return Buffer.MatchValue(tagPos.Value.Start, tagPos.Value.End, value0, value1);
        }

        public override int MatchTag(int tag, ReadOnlySpan<byte> value0, ReadOnlySpan<byte> value1, ReadOnlySpan<byte> value2)
        {
            var position = GetPosition(tag);
            if (position < 0) return -1;
            var tagPos = GetTag(position);
            if (tagPos == null) return -1;
            return Buffer.MatchValue(tagPos.Value.Start, tagPos.Value.End, value0, value1, value2);
        }

        public override int MatchTag(int tag, ReadOnlySpan<byte> value0, ReadOnlySpan<byte> value1, ReadOnlySpan<byte> value2, ReadOnlySpan<byte> value3)
        {
            var position = GetPosition(tag);
            if (position < 0) return -1;
            var tagPos = GetTag(position);
            if (tagPos == null) return -1;
            return Buffer.MatchValue(tagPos.Value.Start, tagPos.Value.End, value0, value1, value2, value3);
        }

        // ===== Try-pattern method implementations =====

        public override bool TryGetInt32(int tag, out int value)
        {
            var position = GetPosition(tag);
            if (position < 0)
            {
                value = 0;
                return false;
            }
            var longVal = LongAtPosition(position);
            if (longVal == null)
            {
                value = 0;
                return false;
            }
            value = (int)longVal.Value;
            return true;
        }

        public override bool TryGetInt64(int tag, out long value)
        {
            var position = GetPosition(tag);
            if (position < 0)
            {
                value = 0;
                return false;
            }
            var longVal = LongAtPosition(position);
            if (longVal == null)
            {
                value = 0;
                return false;
            }
            value = longVal.Value;
            return true;
        }

        public override bool TryGetDouble(int tag, out double value)
        {
            var position = GetPosition(tag);
            if (position < 0)
            {
                value = 0;
                return false;
            }
            var doubleVal = FloatAtPosition(position);
            if (doubleVal == null)
            {
                value = 0;
                return false;
            }
            value = doubleVal.Value;
            return true;
        }

        public override bool TryGetDecimal(int tag, out decimal value)
        {
            var position = GetPosition(tag);
            if (position < 0)
            {
                value = 0;
                return false;
            }
            var decimalVal = DecimalAtPosition(position);
            if (decimalVal == null)
            {
                value = 0;
                return false;
            }
            value = decimalVal.Value;
            return true;
        }

        public override bool TryGetBool(int tag, out bool value)
        {
            var position = GetPosition(tag);
            if (position < 0)
            {
                value = false;
                return false;
            }
            var boolVal = BoolAtPosition(position);
            if (boolVal == null)
            {
                value = false;
                return false;
            }
            value = boolVal.Value;
            return true;
        }

        // ===== Position-based access (for use with EnumerateTagPositions) =====

        /// <summary>
        /// Gets the raw span at a specific position index. Zero allocation.
        /// Use with EnumerateTagPositions for repeated tag access.
        /// </summary>
        public ReadOnlySpan<byte> GetSpanAtPosition(int position)
        {
            var tagPos = GetTag(position);
            if (tagPos == null) return ReadOnlySpan<byte>.Empty;
            return Buffer.GetSpan(tagPos.Value.Start, tagPos.Value.End);
        }

        /// <summary>
        /// Compares value at a specific position to expected bytes. Zero allocation.
        /// Use with EnumerateTagPositions for repeated tag access.
        /// </summary>
        public bool IsEqualAtPosition(int position, ReadOnlySpan<byte> expected)
        {
            var tagPos = GetTag(position);
            if (tagPos == null) return false;
            return Buffer.SequenceEqual(tagPos.Value.Start, tagPos.Value.End, expected);
        }

        /// <summary>
        /// Gets int32 at a specific position without boxing.
        /// Use with EnumerateTagPositions for repeated tag access.
        /// </summary>
        public bool TryGetInt32AtPosition(int position, out int value)
        {
            var longVal = LongAtPosition(position);
            if (longVal == null)
            {
                value = 0;
                return false;
            }
            value = (int)longVal.Value;
            return true;
        }

        /// <summary>
        /// Gets int64 at a specific position without boxing.
        /// Use with EnumerateTagPositions for repeated tag access.
        /// </summary>
        public bool TryGetInt64AtPosition(int position, out long value)
        {
            var longVal = LongAtPosition(position);
            if (longVal == null)
            {
                value = 0;
                return false;
            }
            value = longVal.Value;
            return true;
        }

        /// <summary>
        /// Gets double at a specific position without boxing.
        /// Use with EnumerateTagPositions for repeated tag access.
        /// </summary>
        public bool TryGetDoubleAtPosition(int position, out double value)
        {
            var doubleVal = FloatAtPosition(position);
            if (doubleVal == null)
            {
                value = 0;
                return false;
            }
            value = doubleVal.Value;
            return true;
        }

        /// <summary>
        /// Creates an independent deep copy of this view that the caller owns.
        /// Use this when you need to hold onto the view data after the parse callback returns.
        /// The cloned view is not pooled and must be managed by the caller.
        /// </summary>
        public AsciiView Clone()
        {
            // Clone the buffer to get our own copy of the bytes
            var clonedBuffer = Buffer.Clone();

            // Clone the tags to get our own copy of tag positions
            var clonedTags = new Tags(Structure?.Tags ?? new Tags());

            // Create new storage with cloned data (not from pool - caller owns it)
            var clonedStorage = new ClonedStorage(clonedBuffer, clonedTags);

            // Create new structure with cloned tags but same segments (position metadata is still valid)
            Structure? clonedStructure = Structure.HasValue
                ? new Structure(clonedTags, Structure.Value.Segments)
                : null;

            return new AsciiView(
                Definitions,
                Segment!,
                clonedStorage,
                clonedStructure,
                Ptr,
                Delimiter,
                WriteDelimiter);
        }

        /// <summary>
        /// Lightweight storage wrapper for cloned views (not pooled).
        /// </summary>
        private sealed class ClonedStorage(ElasticBuffer buffer, Tags locations) : StoragePool.Storage
        {
            public override ElasticBuffer Buffer => buffer;
            public override Tags Locations => locations;
        }
    }
}
