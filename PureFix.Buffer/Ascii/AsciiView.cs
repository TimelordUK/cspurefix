using PureFix.Buffer.Segment;
using PureFix.Dictionary.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.tag;

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

        public T? GetTyped<T>(int tag)
        {
            var position = GetPosition(tag);
            if (position< 0)
            {
                return default(T?);
            }

            if (typeof(T) == typeof(string))
            {
                return (T?) Convert.ChangeType(StringAtPosition(position), typeof(T));
            }

            if (typeof(T) == typeof(int))
            {
                return (T?)Convert.ChangeType(LongAtPosition(position), typeof(T));
            }

            if (typeof(T) == typeof(double))
            {
                return (T?)Convert.ChangeType(FloatAtPosition(position), typeof(T));
            }

            if (typeof(T) == typeof(decimal))
            {
                return (T?)Convert.ChangeType(DecimalAtPosition(position), typeof(T));
            }

            return default(T?);
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
            return tag == null ? null : Buffer.GetString(tag.Value.Start, tag.Value.Start + tag.Value.Len);
        }

        protected long? LongAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetWholeNumber(tag.Value.Start, tag.Value.Start + tag.Value.Len - 1);
        }

        protected double? FloatAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetFloat(tag.Value.Start, tag.Value.Start + tag.Value.Len - 1);
        }

        protected decimal? DecimalAtPosition(int position)
        {
            var tag = GetTag(position);
            return tag == null ? null : Buffer.GetDecimal(tag.Value.Start, tag.Value.Start + tag.Value.Len - 1);
        }
    }
}
