using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Ascii;

namespace PureFix.Buffer
{
    public partial class ElasticBuffer
    {
        public char[] GetChars(int st, int vend)
        {
            var span = new ReadOnlySpan<byte>(_buffer, st, vend - st + 1);
            var chars = new char[span.Length];
            var itr = span.GetEnumerator();
            var j = 0;
            while (itr.MoveNext())
            {
                chars[j++] = (char)itr.Current;
            }
            return chars;
        }

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
            var chars = GetChars(st, vend);
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
