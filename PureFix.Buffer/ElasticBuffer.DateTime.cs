using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer
{
    /*
     * case "utctimestamp":
       {
           return TagType.UtcTimestamp;
       }

       case "localmktdate":
       {
           return TagType.LocalDate;
       }

       case "utcdateonly":
       {
           return TagType.UtcDateOnly;
       }

       case "utctimeonly":
       {
           return TagType.UtcTimeOnly;
       }
     */
    public partial class ElasticBuffer
    {
        public static class TimeFormats
        {
            public static readonly string Timestamp = "yyyyMMdd-HH:mm:ss.fff";
            public static readonly string Date = "yyyyMMdd";
            public static readonly string TimeMs = "HH:mm:ss.fff";
            public static readonly string Time = "HH:mm:ss";
        }
        
        // 10:39:01.621
        public DateTime? GetLocalTimeOnly(int st, int vend)
        {
            var v = GetTime(st, vend, DateTimeStyles.AssumeLocal);
            if (v == null) return v;
            return v.Value.ToLocalTime();
        }

        // case "utctimeonly":
        public DateTime? GetUtcTimeOnly(int st, int vend)
        {
            var v = GetTime(st, vend, DateTimeStyles.AssumeUniversal);
            if (v == null) return v;
            return v.Value.ToUniversalTime();
        }

        public int WriteUtcTimeOnly(DateTime dateTime)
        {
            return WriteLocalTimeOnly(dateTime.ToUniversalTime());
        }

        public int WriteLocalTimeOnly(DateTime dateTime)
        {
            var format = TimeFormats.TimeMs;
            CheckGrowBuffer(format.Length);
            var span = _buffer.AsSpan()[Pos..format.Length];
            dateTime.TryFormat(span, out var written, format);
            Pos += written;
            return Pos;
        }

        // 20180610-10:39:01.621
        public int WriteUtcTimeStamp(DateTime dateTime)
        {
            return WriteFormat(dateTime.ToUniversalTime(), TimeFormats.Timestamp);
        }

        public int WriteUtcDateOnly(DateTime dateTime)
        {
            return WriteFormat(dateTime.ToUniversalTime(), TimeFormats.Date);
        }

        public int WriteLocalDateOnly(DateTime dateTime)
        {
            return WriteFormat(dateTime, TimeFormats.Date);
        }

        public DateTime? GetUtcDateOnly(int st, int vend)
        {
            return GetDateTimeWithFormat(st, vend, TimeFormats.Date, DateTimeStyles.AssumeUniversal);
        }

        public DateTime? GetLocalDateOnly(int st, int vend)
        {
            return GetDateTimeWithFormat(st, vend, TimeFormats.Date, DateTimeStyles.AssumeLocal);
        }

        private int WriteFormat(DateTime dateTime, string format)
        {
            CheckGrowBuffer(format.Length);
            var span = _buffer.AsSpan()[Pos..format.Length];
            dateTime.TryFormat(span, out var written, format);
            Pos += written;
            return Pos;
        }

        public DateTime? GetUtcTimeStamp(int st, int vend)
        {
            return GetDateTimeWithFormat(st, vend, TimeFormats.Timestamp, DateTimeStyles.AssumeUniversal);
        }

        private DateTime? GetDateTimeWithFormat(int st, int vend, string format, DateTimeStyles style)
        {
            var span = new ReadOnlySpan<byte>(_buffer, st, vend - st + 1);
            Span<char> chars = stackalloc char[span.Length];
            span.CopyByteSpanToCharSpan(chars);

            if (DateTime.TryParseExact(chars, format, null, style, out var d))
            {
                return d;
            }
            return null;
        }

        private DateTime? GetTime(int st, int vend, DateTimeStyles style)
        {
            var span = new ReadOnlySpan<byte>(_buffer, st, vend - st + 1);
            Span<char> chars = stackalloc char[span.Length];
            span.CopyByteSpanToCharSpan(chars);

            
            if (DateTime.TryParseExact(chars, TimeFormats.TimeMs, null, style, out var d))
            {
                return d;
            }
            if (DateTime.TryParseExact(chars, TimeFormats.Time, null, style, out var d1))
            {
                return d1;
            }
            return null;
        }
    }
}
