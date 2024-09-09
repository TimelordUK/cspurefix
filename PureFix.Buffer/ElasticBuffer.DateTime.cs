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
    public partial class ElasticBuffer
    {
        // 10:39:01.621
        public DateTime? GetLocalTime(int st, int vend)
        {
            return GetTime(st, vend, DateTimeStyles.AssumeLocal);
        }

        public DateTime? GetUtcTime(int st, int vend)
        {
            return GetLocalTime(st, vend)?.ToUniversalTime();
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
            if (DateTime.TryParseExact(chars, "hh:mm:ss.fff".AsSpan(), null, style, out var d))
            {
                return d;
            }
            if (DateTime.TryParseExact(chars, "hh:mm:ss".AsSpan(), null, style, out var d1))
            {
                return d1;
            }
            return null;
        }
    }
}
