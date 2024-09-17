using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PureFix.Types;
namespace PureFix.Types
{
    public partial class ElasticBuffer
    {
        private const byte AsciiZero = (byte)'0';
        private const byte AsciiNine = (byte)'9';
        private const byte AsciiY = (byte)'Y';
        private const byte AsciiN = (byte)'N';

        private static readonly ArrayPool<byte> _pool = ArrayPool<byte>.Create(128 * 1024, 50);

        private byte[] _buffer;
        public int Pos { get; private set; }
        private readonly int _returnTo;

        public ElasticBuffer(int size = 1 * 1024) 
        {
            _buffer = _pool.Rent(size);
            _returnTo = _buffer.Length;
        }

        public ElasticBuffer(ElasticBuffer rhs) : this()
        {
            var length = rhs.Pos;
            _buffer = _pool.Rent(length);
            rhs._buffer[..rhs.Pos].CopyTo(_buffer, length);
            _returnTo = _buffer.Length;
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
            
            var span = new Span<byte>(_buffer, 0, ptr);
            var length = span.Length;
            
            for (var i = 0; i < length; i++)
            {
                total += span[i];
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
            if (ptr >= 0 && ptr <= _buffer.Length)
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
            WriteChar(v ? AsciiY : AsciiN);
            return Pos;
        }

        public int SwitchChar(byte c)
        {
            _buffer[Pos - 1] = c;
            return Pos;
        }

        public int SaveChar(byte c)
        {
            CheckGrowBuffer(1);
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
            var length = s.Length;
            
            for (var i = 0; i < length; i++)
            {
                var c = s[i];
                _buffer[Pos++] = (byte)c;
            }
            
            return Pos;
        }

        public int WriteMonthYear(MonthYear monthYear)
        {
            var length = monthYear.Length;
            CheckGrowBuffer(length);

            var destination = new Span<byte>(_buffer, Pos, length);
            monthYear.CopyTo(destination);

            Pos += length;

            return Pos;
        }

        public int WriteBuffer(Memory<byte> v)
        {
            var span = v.Span;
            var length = span.Length;
            CheckGrowBuffer(length);
            
            for (var i = 0; i < length; i++)
            {
                var b = span[i];
                _buffer[Pos++] = b;
            }
            return Pos;
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
            var span = new Span<byte>(_buffer, Pos, reserve);
            n.TryFormat(span, out var written);
            Pos += written;
            return Pos;
        }

        public bool Reset()
        {
            Pos = 0;
            var reducing = _buffer.Length > _returnTo;
            if (reducing)
            {
                _pool.Return(_buffer);
                _buffer = _pool.Rent(_returnTo);
            }
            return reducing;
        }

        public long GetWholeNumber(int st, int vend)
        {
            // Fix numbers don't have any fancy formatting other than a sign prefix
            // so we should probably avoid doing (int/long).Parse as it supports too many formats
            var encoding = new ReadOnlySpan<byte>(_buffer, st, vend - st + 1);
            var offset = 0;
            var length = encoding.Length;

            long multiplier = 1;
            if (encoding[0] == (byte)'+')
            {
                multiplier = 1;
                offset++;
            }
            else if (encoding[0] == (byte)'-')
            {
                multiplier = -1;
                offset++;
            }

            long value = 0;

            while(offset < length)
            {
                var b = encoding[offset];
                if (b >= AsciiZero && b <= AsciiNine)
                {
                    value = (value * 10) + (b - AsciiZero);
                }
                else
                {
                    throw new FormatException("invalid digit in number");
                }

                offset++;
            }

            value *= multiplier;
            
            return value;
        }

        public string GetString(int start, int end)
        {
            var length = end - start;
            if (length == 0) return string.Empty;

            var str = string.Create(length, (_buffer, start, length), static (span, state) =>
            {
                for (var i = 0; i < state.length; i++)
                {
                    span[i] = (char)(state._buffer[state.start + i]);
                }
            });

            return str;
        }

        public int CurrentSize()
        {
            return _buffer.Length;
        }

        public override string ToString()
        {
            var slice = new ReadOnlySpan<byte>(_buffer, 0, Pos);
            var str = Encoding.UTF8.GetString(slice);
            return str;
        }

        public void CheckGrowBuffer(int required)
        {
            var len = _buffer.Length;
            if (_buffer.Length - Pos >= required)
            {
                return;
            }

            while (len - Pos < required)
            {
                len *= 2;
            }

            var newBuffer = _pool.Rent(len);
            Array.Copy(_buffer, newBuffer, _buffer.Length);

            _buffer = newBuffer;
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

        public MonthYear? GetMonthYear(int st, int vend)
        {
            var ros = new ReadOnlySpan<byte>(_buffer, st, vend - st + 1);
            if (MonthYear.TryParse(ros, out var monthYear))
            {
                return monthYear;
            }

            return null;
        }

        public double? GetFloat(int st, int vend)
        {
            var ros = new ReadOnlySpan<byte>(_buffer, st, vend - st + 1);
            if (double.TryParse(ros, out var v))
            {
                return v;
            }

            return null;
        }

        public decimal? GetDecimal(int st, int vend)
        {
            var ros = new ReadOnlySpan<byte>(_buffer, st, vend - st + 1);
            if (decimal.TryParse(ros, out var v))
            {
                return v;
            }

            return null;
        }

        public Memory<byte> GetBuffer(int st, int vend)
        {
            var span = new ReadOnlySpan<byte>(_buffer, st, vend - st + 1);
            return span.ToArray();
        }

        public bool GetBoolean(int start)
        {
            return _buffer[start] == AsciiY;
        }

        public ElasticBuffer Clone()
        {
            return new ElasticBuffer(this);
        }
    }
}
