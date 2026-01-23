using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.Core;

namespace PureFix.Types
{
    /// <summary>
    /// An implementation of a FIX writer that uses an elastic buffer for performance.
    /// Can be reused across multiple messages by calling Reset().
    /// </summary>
    public sealed class DefaultFixWriter : IFixWriter
    {
        private const byte EQ = (byte)'=';
        private ElasticBuffer _buffer = null!;
        private Tags _tags = null!;

        public byte Delimiter { get; private set; } = 1;

        /// <summary>
        /// Creates a new DefaultFixWriter with the specified buffer and tags.
        /// </summary>
        public DefaultFixWriter(ElasticBuffer buffer, Tags tags, byte delimiter = 1)
        {
            _buffer = buffer;
            _tags = tags;
            Delimiter = delimiter;
        }

        /// <summary>
        /// Creates an uninitialized writer. Must call Reset() before use.
        /// </summary>
        public DefaultFixWriter()
        {
        }

        /// <summary>
        /// Resets the writer to use new buffer and tags. Allows reuse without allocation.
        /// </summary>
        public void Reset(ElasticBuffer buffer, Tags tags, byte delimiter)
        {
            _buffer = buffer;
            _tags = tags;
            Delimiter = delimiter;
        }

        /// <summary>
        /// The tags that are populated whilst writing
        /// </summary>
        public Tags Tags => _tags;

        /// <summary>
        /// The buffer being written to
        /// </summary>
        public ElasticBuffer Buffer => _buffer;

        public void WriteBoolean(int tag, bool value)
        {
            _buffer.WriteWholeNumber(tag);
            _buffer.WriteChar(EQ);
            var start = _buffer.Pos;
            _buffer.WriteBoolean(value);
            _buffer.WriteChar(Delimiter);
            _tags.Store(start, _buffer.Pos - start - 1, tag);
        }

        public void WriteBuffer(int tag, byte[] value)
        {
            _buffer.WriteWholeNumber(tag);
            _buffer.WriteChar(EQ);
            var start = _buffer.Pos;
            _buffer.WriteBuffer(value);
            _buffer.WriteChar(Delimiter);
            _tags.Store(start, _buffer.Pos - start - 1, tag);
        }

        public void WriteLocalDateOnly(int tag, DateOnly value)
        {
            _buffer.WriteWholeNumber(tag);
            _buffer.WriteChar(EQ);
            var start = _buffer.Pos;
            _buffer.WriteLocalDateOnly(value);
            _buffer.WriteChar(Delimiter);
            _tags.Store(start, _buffer.Pos - start - 1, tag);
        }

        public void WriteMonthYear(int tag, MonthYear value)
        {
            _buffer.WriteWholeNumber(tag);
            _buffer.WriteChar(EQ);
            var start = _buffer.Pos;
            _buffer.WriteMonthYear(value);
            _buffer.WriteChar(Delimiter);
            _tags.Store(start, _buffer.Pos - start - 1, tag);
        }

        public void WriteNumber(int tag, double value)
        {
            _buffer.WriteWholeNumber(tag);
            _buffer.WriteChar(EQ);
            var start = _buffer.Pos;
            _buffer.WriteNumber(value);
            _buffer.WriteChar(Delimiter);
            _tags.Store(start, _buffer.Pos - start - 1, tag);


        }

        public void WriteString(int tag, string value)
        {
            _buffer.WriteWholeNumber(tag);
            _buffer.WriteChar(EQ);
            var start = _buffer.Pos;
            _buffer.WriteString(value);
            _buffer.WriteChar(Delimiter);
            _tags.Store(start, _buffer.Pos - start - 1, tag);

        }

        public void WriteUtcDateOnly(int tag, DateOnly value)
        {
            _buffer.WriteWholeNumber(tag);
            _buffer.WriteChar(EQ);
            var start = _buffer.Pos;
            _buffer.WriteUtcDateOnly(value);
            _buffer.WriteChar(Delimiter);
            _tags.Store(start, _buffer.Pos - start - 1, tag);
        }

        public void WriteUtcTimeStamp(int tag, DateTime value)
        {
            _buffer.WriteWholeNumber(tag);
            _buffer.WriteChar(EQ);
            var start = _buffer.Pos;
            _buffer.WriteUtcTimeStamp(value);
            _buffer.WriteChar(Delimiter);
            _tags.Store(start, _buffer.Pos - start - 1, tag);
        }

        public void WriteWholeNumber(int tag, int value)
        {
            _buffer.WriteWholeNumber(tag);
            _buffer.WriteChar(EQ);
            var start = _buffer.Pos;
            _buffer.WriteWholeNumber(value);
            _buffer.WriteChar(Delimiter);
            _tags.Store(start, _buffer.Pos - start - 1, tag);
        }

        public void WriteTimeOnly(int tag, TimeOnly value)
        {
            _buffer.WriteWholeNumber(tag);
            _buffer.WriteChar(EQ);
            var start = _buffer.Pos;
            _buffer.WriteTimeOnly(value);
            _buffer.WriteChar(Delimiter);
            _tags.Store(start, _buffer.Pos - start - 1, tag);
        }
    }
}
