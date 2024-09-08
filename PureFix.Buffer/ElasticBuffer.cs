using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Ascii;

namespace PureFix.Buffer
{
    public class ElasticBuffer(int size = 6 * 1024, int returnTo = 6 * 1024) : IEquatable<ElasticBuffer>
    {
        private readonly List<byte> _buffer = Enumerable.Repeat((byte)0, size).ToList();
        public int Pos { get; private set; }
        private readonly int _returnTo = returnTo;

        public ElasticBuffer(ElasticBuffer rhs) : this()
        {
            _buffer = rhs._buffer[..rhs.Pos];
            _returnTo = rhs._returnTo;
            Pos = rhs.Pos;
        }

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

        public Memory<byte> Slice() {
            return _buffer.Slice(0, Pos).ToArray();
         }
  
        /*
    public copy() : Buffer {
    const m = Buffer.alloc(this.ptr)
    this.buffer.copy(m, 0, 0, this.ptr)
    return m
    }*/

        public int Checksum(int? p)
        {
            var ptr = p ?? Pos;
            var cks = Sum(ptr);
            return cks % 256;
        }

        public int Sum(int? p)
        {
            var total = 0;
            var ptr = p ?? Pos;
            ptr = Math.Min(ptr, Pos);
            for (var idx = 0; idx < ptr; idx++)
            {
                total += _buffer[idx];
            }
            return total;
        }

        public int GetPos()
        {
            return Pos;
        }

        public int SetPos(int ptr)
        {
            var prev = Pos;
            if (ptr >= 0 && ptr <= _buffer.Count)
            {
                Pos = ptr;
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
            return Pos;
        }

        public bool GetBoolean(int start)
        {
            var b = _buffer[start];
            return b == AsciiChars.Y;
        }

        public int SwitchChar(byte c)
        {
            _buffer[Pos - 1] = c;
            return Pos;
        }

        public int SaveChar(byte c)
        {
            _buffer[Pos++] = c;
            return Pos;
        }

        public int WriteChar(byte c)
        {
            CheckGrowBuffer(1);
            _buffer[Pos++] = c;
            return Pos;
        }

        public int WriteString(string s)
        {
            CheckGrowBuffer(s.Length);
            foreach (var c in s)
            {
                _buffer[Pos++] = (byte)c;
            }

            return Pos;
        }

        public int WriteBuffer(Memory<byte> v)
        {
            var span = v.Span;
            CheckGrowBuffer(span.Length);
            foreach (var c in span)
            {
                _buffer[Pos++] = c;
            }
            return Pos;
        }

        public Memory<byte> GetBuffer(int start, int end) {
            return _buffer.Slice(start, start + (end - start)).ToArray();
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
                _buffer[Pos++] = AsciiChars.Minus;
            }
            while (p >= 1)
            {
                var d = (byte)(v / p);
                v -= d * p;
                p /= 10;
                _buffer[Pos++] = (byte)(AsciiChars.Zero + d);
            }

            return Pos;
        }

        public bool Reset()
        {
            Pos = 0;
            var reducing = _buffer.Capacity > _returnTo;
            if (reducing)
            {
                CollectionsMarshal.SetCount(_buffer, _returnTo);
                _buffer.Capacity = _returnTo;
            }
            return reducing;
        }

        private (int start, int sign) GetSign(int start)
        {
            var sign = 1;
            switch (_buffer[start])
            {
                case AsciiChars.Minus:
                    {
                        sign = -1;
                        ++start;
                        break;
                    }
                case AsciiChars.Plus:
                    {
                        ++start;
                        break;
                    }
            }
            return (start, sign);
        }

        public long GetWholeNumber(int st, int vend)
        {
            var (start, sign) = GetSign(st);
            var num = 0;
            for (var j = start; j <= vend; ++j)
            {
                num *= 10;
                var v = _buffer[j];
                if (v is < AsciiChars.Zero or > AsciiChars.Nine)
                {
                    throw new InvalidDataException($"{v} is not a digit 0-9");
                }
                var d = v - AsciiChars.Zero;
                num += d;
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
            return Encoding.UTF8.GetString(_buffer.ToArray(), 0, Pos);
        }

        public void CheckGrowBuffer(int required)
        {
            if (_buffer.Capacity - Pos >= required)
            {
                return;
            }

            while (_buffer.Capacity - Pos < required)
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
                return WriteString($"{(decimal)v}");
            }
        }

        public double? GetFloat(int st, int vend)
        {
            long n = 0;
            var digits = 0;
            var dotPosition = 0;
            var (start, sign) = GetSign(st);
            var len = vend - start;

            for (var j = start; j <= vend; ++j)
            {
                var p = _buffer[j];
                switch (p)
                {
                    case >= AsciiChars.Zero and <= AsciiChars.Nine:
                        {
                            n = n * 10;
                            var d = p - AsciiChars.Zero;
                            ++digits;
                            n += d;
                            break;
                        }

                    case AsciiChars.Dot:
                        if (dotPosition > 0)
                        {
                            return null;
                        }
                        dotPosition = j - start;
                        break;

                    default:
                        if (digits > 0)
                        {
                            return null;
                        }
                        break;
                }
            }
            if (dotPosition == 0)
            {
                return n * sign;
            }
            var power = len - dotPosition;
            var raised = Math.Pow(10, -1 * power);
            var round = Math.Pow(10, power);
            var val = n * raised * sign;
            var rounded = Math.Round(val * round) / round;
            return rounded;
        }

        public ElasticBuffer Clone()
        {
            return new ElasticBuffer(this);
        }

        public bool Equals(ElasticBuffer? other)
        {
            if (!ReferenceEquals(other, null)) return false;
            return other != null && _buffer.SequenceEqual(other._buffer);
        }
    }
}
