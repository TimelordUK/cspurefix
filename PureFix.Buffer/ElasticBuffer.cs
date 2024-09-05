using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public int WriteBuffer(Memory<byte> v)
        {
            var span = v.Span;
            foreach (var c in span)
            {
                _buffer[_ptr++] = c;
            }

            return _ptr;
        }

        public int WriteWholeNumber(int n)
        {
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

        private static double PrecisionRound(double n, int precision)
        {
            var factor = Math.Pow(10, precision);
            return Math.Round(n * factor) / factor;
        }

        public int WriteNumber(double v, int places = 13)
        {
            var rounded = Math.Floor(v);
            var fraction = PrecisionRound(v - rounded, places);
            if (fraction == 0)
            {
                // integer
                return WriteWholeNumber((int)rounded);
            }
            else
            {
                // decimal with fraction turn to string
                return WriteString($"{v}");
            }
        }

        public double? GetFloat(int start, int vend)
        {
            long n = 0;
            var digits = 0;
            var dotPosition = 0;
            var sign = 1;

            switch (_buffer[start])
            {
                case AsciiChars.Minus:
                    {
                        sign = -1;
                        start++;
                        break;
                    }

                case AsciiChars.Plus:
                    {
                        start++;
                        break;
                    }
            }
            var len = vend - start;
            long i = (long)Math.Pow(10, len - 1);
            for (var j = start; j <= vend; ++j)
            {
                var p = _buffer[j];
                if (p >= AsciiChars.Zero && p <= AsciiChars.Nine)
                {
                    var d = p - AsciiChars.Zero;
                    ++digits;
                    n += d * i;
                    i /= 10;
                }
                else if (p == AsciiChars.Dot)
                {
                    if (dotPosition > 0)
                    {
                        return null;
                    }
                    dotPosition = j - start;
                }
                else if (digits > 0)
                {
                    return null;
                }
            }
            var power = dotPosition == 0 ? 0 : len - dotPosition;
            var raised = dotPosition == 0 ? 10 : Math.Pow(10, -1 * power);
            var round = dotPosition == 0 ? 1 : Math.Pow(10, power);
            var val = n * raised * sign;
            return Math.Round(val * round) / round;
        }
    }
}
