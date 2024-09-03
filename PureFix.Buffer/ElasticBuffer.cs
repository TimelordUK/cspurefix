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
            this.writeChar(v ? AsciiChars.Y : AsciiChars.N);
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
            CheckGrowBuffer(1);
            _buffer[_ptr++] = c;
            return _ptr;
        }

        public int WriteString(string s)
        {
            var begin = _ptr;
            this.CheckGrowBuffer(s.Length);
            _ptr += buffer.write(s, begin, s.length, 'ascii')
            return this.ptr
        }
}
}

