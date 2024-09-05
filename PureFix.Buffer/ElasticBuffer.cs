using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Ascii;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PureFix.Buffer
{
    public class ElasticBuffer(int size = 6 * 1024, int returnTo = 6 * 1024)
    {
        private int _size = size;
        private int _returnTo = returnTo;
        private readonly List<byte> _buffer = Enumerable.Repeat((byte)0, size).ToList();
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
            CheckGrowBuffer(s.Length);
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
            var reserve = digits;
            var sign = Math.Sign(n);
            if (sign < 0)
            {
                reserve++;
            }
            CheckGrowBuffer(reserve);
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

        public int GetWholeNumber(int start, int vend)
        {
            var sign = 1;
            var raised = vend - start;
            switch (_buffer[start])
            {
                case AsciiChars.Minus:
                    {
                        --raised;
                        sign = -1;
                        ++start;
                        break;
                    }
                case AsciiChars.Plus:
                    {
                        --raised;
                        ++start;
                        break;
                    }
            }

            var i = (int)Math.Pow(10, raised);
            var num = 0;
            var scan = start;

            while (scan <= vend)
            {
                var p = _buffer[scan++];
                var d = p - AsciiChars.Zero;
                num += d * i;
                i /= 10;
            }

            return num * sign;
        }

        public string GetString(int start, int end)
        {
            var slice = _buffer.Slice(start, end - start);
            var str = Encoding.Default.GetString(slice.ToArray());
            return str;
        }

        public int CurrentSize()
        {
            return _buffer.Capacity;
        }

        public override string ToString()
        {
            return Encoding.UTF8.GetString(_buffer.ToArray(), 0, _ptr);
        }

        public void CheckGrowBuffer(int required)
        {
            if (_buffer.Capacity - _ptr >= required)
            {
                return;
            }

            while (_buffer.Capacity - _ptr < required)
            {
                _buffer.Capacity *= 2;
            }
            _buffer.AddRange(Enumerable.Repeat((byte)0, _buffer.Capacity - _buffer.Count));
        }
    }
}

    

