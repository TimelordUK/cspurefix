using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    /// <summary>
    /// An implementation of a FIX writer that uses an elastic buffer for performance
    /// </summary>
    public sealed class DefaultFixWriter : IFixWriter
    {
        private const byte EQ = (byte)'=';
        private const byte Delimiter = 1;

        private readonly ElasticBuffer m_Buffer;
        private readonly Tags m_Tags;

        public DefaultFixWriter(ElasticBuffer buffer, Tags tags)
        {
            m_Buffer = buffer;
            m_Tags = tags;
        }

        /// <summary>
        /// The tags that are populated whilst writing
        /// </summary>
        public Tags Tags
        {
            get{return m_Tags;}
        }

        /// <summary>
        /// The buffer being written to
        /// </summary>
        public ElasticBuffer Buffer
        {
            get{return m_Buffer;}
        }

        public void WriteBoolean(int tag, bool value)
        {
            m_Buffer.WriteWholeNumber(tag);
            m_Buffer.WriteChar(EQ);
            var start = m_Buffer.Pos;
            m_Buffer.WriteBoolean(value);
            m_Buffer.WriteChar(Delimiter);
            m_Tags.Store(start, m_Buffer.Pos - start - 1, tag);
        }

        public void WriteBuffer(int tag, byte[] value)
        {
            m_Buffer.WriteWholeNumber(tag);
            m_Buffer.WriteChar(EQ);
            var start = m_Buffer.Pos;
            m_Buffer.WriteBuffer(value);
            m_Buffer.WriteChar(Delimiter);
            m_Tags.Store(start, m_Buffer.Pos - start - 1, tag);
        }

        public void WriteLocalDateOnly(int tag, DateOnly value)
        {
            m_Buffer.WriteWholeNumber(tag);
            m_Buffer.WriteChar(EQ);
            var start = m_Buffer.Pos;
            m_Buffer.WriteLocalDateOnly(value);
            m_Buffer.WriteChar(Delimiter);
            m_Tags.Store(start, m_Buffer.Pos - start - 1, tag);
        }

        public void WriteMonthYear(int tag, MonthYear value)
        {
            m_Buffer.WriteWholeNumber(tag);
            m_Buffer.WriteChar(EQ);
            var start = m_Buffer.Pos;
            m_Buffer.WriteMonthYear(value);
            m_Buffer.WriteChar(Delimiter);
            m_Tags.Store(start, m_Buffer.Pos - start - 1, tag);
        }

        public void WriteNumber(int tag, double value)
        {
            m_Buffer.WriteWholeNumber(tag);
            m_Buffer.WriteChar(EQ);
            var start = m_Buffer.Pos;
            m_Buffer.WriteNumber(value);
            m_Buffer.WriteChar(Delimiter);
            m_Tags.Store(start, m_Buffer.Pos - start - 1, tag);


        }

        public void WriteString(int tag, string value)
        {
            m_Buffer.WriteWholeNumber(tag);
            m_Buffer.WriteChar(EQ);
            var start = m_Buffer.Pos;
            m_Buffer.WriteString(value);
            m_Buffer.WriteChar(Delimiter);
            m_Tags.Store(start, m_Buffer.Pos - start - 1, tag);

        }

        public void WriteUtcDateOnly(int tag, DateOnly value)
        {
            m_Buffer.WriteWholeNumber(tag);
            m_Buffer.WriteChar(EQ);
            var start = m_Buffer.Pos;
            m_Buffer.WriteUtcDateOnly(value);
            m_Buffer.WriteChar(Delimiter);
            m_Tags.Store(start, m_Buffer.Pos - start - 1, tag);
        }

        public void WriteUtcTimeStamp(int tag, DateTime value)
        {
            m_Buffer.WriteWholeNumber(tag);
            m_Buffer.WriteChar(EQ);
            var start = m_Buffer.Pos;
            m_Buffer.WriteUtcTimeStamp(value);
            m_Buffer.WriteChar(Delimiter);
            m_Tags.Store(start, m_Buffer.Pos - start - 1, tag);
        }

        public void WriteWholeNumber(int tag, int value)
        {
            m_Buffer.WriteWholeNumber(tag);
            m_Buffer.WriteChar(EQ);
            var start = m_Buffer.Pos;
            m_Buffer.WriteWholeNumber(value);
            m_Buffer.WriteChar(Delimiter);
            m_Tags.Store(start, m_Buffer.Pos - start - 1, tag);
        }

        public void WriteTimeOnly(int tag, TimeOnly value)
        {
            m_Buffer.WriteWholeNumber(tag);
            m_Buffer.WriteChar(EQ);
            var start = m_Buffer.Pos;
            m_Buffer.WriteTimeOnly(value);
            m_Buffer.WriteChar(Delimiter);
            m_Tags.Store(start, m_Buffer.Pos - start - 1, tag);
        }
    }
}
