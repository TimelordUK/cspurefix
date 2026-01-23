using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.Core;

namespace PureFix.Types
{
    /// <summary>
    /// An implementation of a FIX writer that uses an elastic buffer for performance
    /// </summary>
    public sealed class DefaultFixWriter(ElasticBuffer buffer, Tags tags, byte delimiter = 1) : IFixWriter
    {
        private const byte EQ = (byte)'=';
        public byte Delimiter { get; } = delimiter;

        /// <summary>
        /// The tags that are populated whilst writing
        /// </summary>
        public Tags Tags
        {
            get{return tags;}
        }

        /// <summary>
        /// The buffer being written to
        /// </summary>
        public ElasticBuffer Buffer
        {
            get{return buffer;}
        }

        public void WriteBoolean(int tag, bool value)
        {
            buffer.WriteWholeNumber(tag);
            buffer.WriteChar(EQ);
            var start = buffer.Pos;
            buffer.WriteBoolean(value);
            buffer.WriteChar(Delimiter);
            tags.Store(start, buffer.Pos - start - 1, tag);
        }

        public void WriteBuffer(int tag, byte[] value)
        {
            buffer.WriteWholeNumber(tag);
            buffer.WriteChar(EQ);
            var start = buffer.Pos;
            buffer.WriteBuffer(value);
            buffer.WriteChar(Delimiter);
            tags.Store(start, buffer.Pos - start - 1, tag);
        }

        public void WriteLocalDateOnly(int tag, DateOnly value)
        {
            buffer.WriteWholeNumber(tag);
            buffer.WriteChar(EQ);
            var start = buffer.Pos;
            buffer.WriteLocalDateOnly(value);
            buffer.WriteChar(Delimiter);
            tags.Store(start, buffer.Pos - start - 1, tag);
        }

        public void WriteMonthYear(int tag, MonthYear value)
        {
            buffer.WriteWholeNumber(tag);
            buffer.WriteChar(EQ);
            var start = buffer.Pos;
            buffer.WriteMonthYear(value);
            buffer.WriteChar(Delimiter);
            tags.Store(start, buffer.Pos - start - 1, tag);
        }

        public void WriteNumber(int tag, double value)
        {
            buffer.WriteWholeNumber(tag);
            buffer.WriteChar(EQ);
            var start = buffer.Pos;
            buffer.WriteNumber(value);
            buffer.WriteChar(Delimiter);
            tags.Store(start, buffer.Pos - start - 1, tag);


        }

        public void WriteString(int tag, string value)
        {
            buffer.WriteWholeNumber(tag);
            buffer.WriteChar(EQ);
            var start = buffer.Pos;
            buffer.WriteString(value);
            buffer.WriteChar(Delimiter);
            tags.Store(start, buffer.Pos - start - 1, tag);

        }

        public void WriteUtcDateOnly(int tag, DateOnly value)
        {
            buffer.WriteWholeNumber(tag);
            buffer.WriteChar(EQ);
            var start = buffer.Pos;
            buffer.WriteUtcDateOnly(value);
            buffer.WriteChar(Delimiter);
            tags.Store(start, buffer.Pos - start - 1, tag);
        }

        public void WriteUtcTimeStamp(int tag, DateTime value)
        {
            buffer.WriteWholeNumber(tag);
            buffer.WriteChar(EQ);
            var start = buffer.Pos;
            buffer.WriteUtcTimeStamp(value);
            buffer.WriteChar(Delimiter);
            tags.Store(start, buffer.Pos - start - 1, tag);
        }

        public void WriteWholeNumber(int tag, int value)
        {
            buffer.WriteWholeNumber(tag);
            buffer.WriteChar(EQ);
            var start = buffer.Pos;
            buffer.WriteWholeNumber(value);
            buffer.WriteChar(Delimiter);
            tags.Store(start, buffer.Pos - start - 1, tag);
        }

        public void WriteTimeOnly(int tag, TimeOnly value)
        {
            buffer.WriteWholeNumber(tag);
            buffer.WriteChar(EQ);
            var start = buffer.Pos;
            buffer.WriteTimeOnly(value);
            buffer.WriteChar(Delimiter);
            tags.Store(start, buffer.Pos - start - 1, tag);
        }
    }
}
