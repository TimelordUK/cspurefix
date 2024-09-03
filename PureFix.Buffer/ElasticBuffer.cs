using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Ascii;

namespace PureFix.Buffer
{
    public class ElasticBuffer(int size = 6 * 1024, int returnTo = 6 * 1024)
    {
        private int _size = size;
        private int _returnTo = returnTo;
        private readonly List<byte> _buffer = new(size);
        private int _ptr;

        private static int HowManyDigits(int v)
        {
            v = Math.Abs(v);
            var digits = 0;
            var w = v;
            while (w > 0)
            {
                ++digits;
                w /= 10;
            }

            return Math.Max(digits, 1);
        }

        public int GetPos()
        {
            return _ptr;
        }

        public int SetPos(int ptr)
        {
            var prev = _ptr;
            if (ptr >= 0 && ptr <= _buffer.Count)
            {
                _ptr = ptr;
            }

            return prev;
        }

        public byte Get(int pos)
        {
            return _buffer[pos];
        }

        public int WriteBoolean(bool v)
        {
            WriteChar(v ? AsciiChars.Y : AsciiChars.N);
            return _ptr;
        }

        public int SwitchChar(byte c)
        {
            _buffer[_ptr - 1] = c;
            return _ptr;
        }

        public int SaveChar(byte c)
        {
            _buffer[_ptr++] = c;
            return _ptr;
        }

        public int WriteChar(byte c)
        {
            _buffer[_ptr++] = c;
            return _ptr;
        }

        public int WriteString(string s)
        {
            foreach (var c in s)
            {
                _buffer[_ptr++] = (byte)c;
            }

            return _ptr;
        }

        public int WriteBuffer(Memory<byte> v) {
            var span = v.Span;
            foreach (var c in span)
            {
                _buffer[_ptr++] = c;
            }

            return _ptr;
        }

        public int WriteWholeNumber(int n) {
            var digits = HowManyDigits(n);
            var sign = Math.Sign(n);
            var p = (int)Math.Pow(10, digits - 1);
            var v = Math.Abs(n);
            
            if (sign < 0)
            {
                _buffer[_ptr++] = AsciiChars.Minus;
            }
            while (p >= 1)
            {
                var d = (byte)(v / p);
                v -= d * p;
                p /= 10;
                _buffer[_ptr++] = (byte)(AsciiChars.Zero + d);
            }

            return _ptr;
        }

        public bool Reset()
        {
            _ptr = 0;
            return true;
        }

        public int GetWholeNumber(int tagStartPos, int pos)
        {
            throw new NotImplementedException();
        }
    }
}

