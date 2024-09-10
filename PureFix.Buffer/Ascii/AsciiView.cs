using PureFix.Buffer.Segment;
using PureFix.Dictionary.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Tag;
using Microsoft.Extensions.ObjectPool;

namespace PureFix.Buffer.Ascii
{
    public class AsciiView : MsgView
    {
        public int Ptr { get; }
        public int Delimiter { get; }
        public int WriteDelimiter { get; } 
        public ElasticBuffer Buffer { get; }


        public AsciiView (
            FixDefinitions definitions,
            SegmentDescription segment,
            ElasticBuffer buffer,
            Structure? structure,
            int ptr,
            int delimiter,
            int writeDelimiter) : base(definitions, segment, structure)
        {
            Ptr = ptr;
            Delimiter = delimiter;
            WriteDelimiter = writeDelimiter;
            Buffer = buffer;
        }

    
        protected override MsgView Create(SegmentDescription singleton)
        {
            return new AsciiView(Definitions,
                singleton,
                Buffer,
                Structure,
                Ptr,
                Delimiter,
                WriteDelimiter);
        }

        private TagPos? GetTag(int position)
        {
            if (Structure == null) return null;
            var tags = Structure.Tags;
            if (position < 0 || position >= tags.NextTagPos)
            {
                return null;
            }
            var tag = tags[position];
            return tag;
        }

        protected override string? StringAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetString(tag.Start, tag.Start + tag.Len);
        }

        protected long? LongAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetWholeNumber(tag.Start, tag.End);
        }

        protected double? FloatAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetFloat(tag.Start, tag.End);
        }

        protected decimal? DecimalAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetDecimal(tag.Start, tag.End);
        }

        protected Memory<byte>? BufferAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetBuffer(tag.Start, tag.End);
        }

        protected bool? BoolAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetBoolean(tag.Start);
        }

        protected DateTime? UtcDateOnlyAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetUtcDateOnly(tag.Start, tag.End);
        }
        protected DateTime? UtcTimeOnlyAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetUtcTimeOnly(tag.Start, tag.End);
        }

        protected DateTime? LocalDateOnlyAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetLocalDateOnly(tag.Start, tag.End);
        }

        protected DateTime? UtcTimestampAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetUtcTimeStamp(tag.Start, tag.End);
        }

        public override T? GetTyped<T>(string name) where T : default
        {
            if (Definitions.Simple.TryGetValue(name, out var typed))
            {
                return GetTyped<T>(typed.Tag);
            }
            return default;
        }

        public override T? GetTyped<T>(int tag) where T : default
        {
            var position = GetPosition(tag);
            if (position < 0)
            {
                return default;
            }

            if (typeof(T) == typeof(DateTime))
            {
                var sf = Definitions.TagToSimple.GetValueOrDefault(tag);
                if (sf == null) return default;
                switch (sf.TagType)
                {
                    case TagType.UtcDateOnly:
                        return (T?)Convert.ChangeType(UtcDateOnlyAtPosition(position), typeof(T));

                    case TagType.UtcTimeOnly:
                        return (T?)Convert.ChangeType(UtcTimeOnlyAtPosition(position), typeof(T));

                    case TagType.UtcTimestamp:
                        return (T?)Convert.ChangeType(UtcTimestampAtPosition(position), typeof(T));

                    case TagType.LocalDate:
                        return (T?)Convert.ChangeType(LocalDateOnlyAtPosition(position), typeof(T));
                }
            }

            if (typeof(T) == typeof(string))
            {
                return (T?)Convert.ChangeType(StringAtPosition(position), typeof(T));
            }

            if (typeof(T) == typeof(int))
            {
                return (T?)Convert.ChangeType(LongAtPosition(position), typeof(T));
            }

            if (typeof(T) == typeof(double))
            {
                return (T?)Convert.ChangeType(FloatAtPosition(position), typeof(T));
            }

            if (typeof(T) == typeof(bool))
            {
                return (T?)Convert.ChangeType(BoolAtPosition(position), typeof(T));
            }

            if (typeof(T) == typeof(decimal))
            {
                return (T?)Convert.ChangeType(DecimalAtPosition(position), typeof(T));
            }

            if (typeof(T) == typeof(byte[]))
            {
                var b = BufferAtPosition(position);
                if (b == null) return default;
                return (T?)Convert.ChangeType(b.Value.ToArray(), typeof(T));
            }

            if (typeof(T) == typeof(Memory<byte>))
            {
                return (T?)Convert.ChangeType(BufferAtPosition(position), typeof(T));
            }

            return default;
        }
    }
}
