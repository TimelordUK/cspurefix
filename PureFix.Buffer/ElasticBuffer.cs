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
using System.Xml.Linq;
using PureFix.Buffer.Ascii;

namespace PureFix.Buffer
{
    public partial class ElasticBuffer(int size = 6 * 1024, int returnTo = 6 * 1024) 
    {
        private byte[] _buffer = new byte[size];
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
            foreach (var t in span)
            {
                total += t;
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
            WriteChar(v ? AsciiChars.Y : AsciiChars.N);
            return Pos;
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
            var span = new Span<byte>(_buffer, Pos, s.Length);
            var i = 0;
            foreach (var c in s)
            {
                span[i++] = (byte)c;
            }
            Pos += s.Length;
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
                var span = new ReadOnlySpan<byte>(_buffer, 0, _returnTo);
                _buffer = span.ToArray();
            }
            return reducing;
        }

        public long GetWholeNumber(int st, int vend)
        {
            var ros = new ReadOnlySpan<byte>(_buffer, st, vend - st + 1);
            var v = long.Parse(ros);
            return v;
        }

        public string GetString(int start, int end)
        {
            var slice = new ReadOnlySpan<byte>(_buffer, start, (end - start));
            var str = Encoding.UTF8.GetString(slice);
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

            var grown = new byte[len];
            Array.Copy(_buffer, grown, _buffer.Length);
            _buffer = grown;
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
            var ros = new ReadOnlySpan<byte>(_buffer, st, vend - st + 1);
            var v = double.Parse(ros);
            return v;
        }

        public decimal? GetDecimal(int st, int vend)
        {
            var ros = new ReadOnlySpan<byte>(_buffer, st, vend - st + 1);
            var v = decimal.Parse(ros);
            return v;
        }

        public Memory<byte> GetBuffer(int st, int vend)
        {
            var span = new ReadOnlySpan<byte>(_buffer, st, vend - st + 1);
            return span.ToArray();
        }

        public bool GetBoolean(int start)
        {
            return _buffer[start] == AsciiChars.Y;
        }

        public ElasticBuffer Clone()
        {
            return new ElasticBuffer(this);
        }
    }
}
