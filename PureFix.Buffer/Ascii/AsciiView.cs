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

        // For lazy structure parsing
        private AsciiSegmentParser? _segmentParser;
        private string? _msgType;
        private bool _structureParsed;
        private bool _isPooled;

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
            AsciiSegmentParser? segmentParser,
            string? msgType)
        {
            var view = Pool.Get();
            view.Initialize(definitions, null, storage, null, ptr, delimiter, writeDelimiter, segmentParser, msgType);
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
            AsciiSegmentParser? segmentParser = null,
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
            AsciiSegmentParser? segmentParser,
            string? msgType)
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
        }

        private void Reset()
        {
            ResetBase();
            _segmentParser = null;
            _msgType = null;
            _structureParsed = false;
            Storage = null!;
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
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetString(tag.Value.Start, tag.Value.Start + tag.Value.Len);
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
