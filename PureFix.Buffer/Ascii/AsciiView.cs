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
        public int Ptr { get; }
        public int Delimiter { get; }
        public int WriteDelimiter { get; } 
        public StoragePool.Storage Storage { get; }
        public ElasticBuffer Buffer => Storage.Buffer;

        public override string BufferString()
        {
            return Buffer.ToString();
        }

        public AsciiView (
            IFixDefinitions definitions,
            SegmentDescription segment,
            StoragePool.Storage storage,
            Structure? structure,
            int ptr,
            int delimiter,
            int writeDelimiter) : base(definitions, segment, structure)
        {
            Ptr = ptr;
            Delimiter = delimiter;
            WriteDelimiter = writeDelimiter;
            Storage = storage;
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
            if (Structure == null) return null;
            var tags = Structure.Value.Tags;
            if (position < 0 || position >= tags.NextTagPos)
            {
                return null;
            }
            var tag = tags[position];
            return tag;
        }

        public override int Checksum()
        {
            if (Structure == null) return -1;
            var t = GetPosition((int)MsgTag.CheckSum);
            var prev = Structure.Value.Tags[t - 1];
            var tp = prev.Start + prev.Len + 1;
            var cs = Buffer.Sum(tp);
            var delimiter = Delimiter;
            var writeDelimiter = WriteDelimiter;
            if (writeDelimiter != delimiter)
            {
                var changes = Structure.Value.Tags.NextTagPos - 1;
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
                return default;
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
                var position when position < 0 => default,
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
                var position when position < 0 => default,
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
                var position when position < 0 => default,
                var position => BufferAtPosition(position)?.ToArray()
            };
        }

        public override Memory<byte>? GetMemory(int tag)
        {
            return GetPosition(tag) switch
            {
                var position when position < 0 => default,
                var position => BufferAtPosition(position)
            };
        }

        public override DateTime? GetDateTime(int tag)
        {
            var position = GetPosition(tag);
            if (position < 0)
            {
                return default;
            }

            return UtcTimestampAtPosition(position);
        } 

        public override TimeOnly? GetTimeOnly(int tag)
        {
            var position = GetPosition(tag);
            if (position < 0)
            {
                return default;
            }

            return UtcTimeOnlyAtPosition(position);
        }

        public override DateOnly? GetDateOnly(int tag)
        {
            var position = GetPosition(tag);
            if (position < 0)
            {
                return default;
            }

            var sf = Definitions.TagToSimple.GetValueOrDefault(tag);
            if (sf == null) return default;

            switch (sf.TagType)
            {
                case TagType.UtcDateOnly:
                    return UtcDateOnlyAtPosition(position);

                case TagType.LocalDate:
                    return LocalDateOnlyAtPosition(position);

                default:
                    return default;
            }
        }

        public override MonthYear? GetMonthYear(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetMonthYear(tag.Value.Start, tag.Value.End);
        }
    }
}
