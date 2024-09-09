using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using PureFix.Buffer.Ascii;

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
        // 10:39:01.621
        public DateTime? GetLocalTime(int st, int vend)
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
            var format = "HH:mm:ss.fff".AsSpan();
            CheckGrowBuffer(format.Length);
            var span = _buffer.AsSpan()[Pos..format.Length];
            dateTime.TryFormat(span, out var written, format);
            Pos += written;
            return Pos;
        }

        private DateTime? GetTime(int st, int vend, DateTimeStyles style)
        {
            var span = new ReadOnlySpan<byte>(_buffer, st, vend - st + 1);
            Span<char> chars = stackalloc char[span.Length];
            var j = 0;
            foreach (var c in span)
            {
                chars[j++] = (char)c;
            }
            if (DateTime.TryParseExact(chars, "HH:mm:ss.fff".AsSpan(), null, style, out var d))
            {
                return d;
            }
            if (DateTime.TryParseExact(chars, "HH:mm:ss".AsSpan(), null, style, out var d1))
            {
                return d1;
            }
            return null;
        }
    }
}
