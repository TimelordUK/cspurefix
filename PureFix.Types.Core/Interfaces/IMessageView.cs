using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public interface IMessageView
    {
        public int GroupCount();
        public string? GetString(int tag);
        public string? GetString(string name);
        public abstract DateTime? GetDateTime(int tag);
        public abstract TimeOnly? GetTimeOnly(int tag);
        public abstract DateOnly? GetDateOnly(int tag);
        public abstract int? GetInt32(string name);
        public abstract int? GetInt32(int tag);
        public abstract double? GetDouble(int tag);
        public abstract double? GetDouble(string name);
        public abstract bool? GetBool(string name);
        public abstract bool? GetBool(int tag);
        public abstract decimal? GetDecimal(string name);
        public abstract decimal? GetDecimal(int tag);
        public abstract byte[]? GetByteArray(string name);
        public abstract byte[]? GetByteArray(int tag);
        public abstract Memory<byte>? GetMemory(int tag);
        public abstract MonthYear? GetMonthYear(int tag);
        public IMessageView? GetGroupInstance(int i);
        public IMessageView? GetView(string name);
        public abstract string BufferString();
        public abstract int Checksum();

        // Zero-allocation span-based API
        public ReadOnlySpan<byte> GetSpan(int tag);
        public bool TryGetSpan(int tag, out ReadOnlySpan<byte> value);
        public bool IsTagEqual(int tag, ReadOnlySpan<byte> expected);
        public bool TagStartsWith(int tag, ReadOnlySpan<byte> prefix);
        public int MatchTag(int tag, ReadOnlySpan<byte> value0, ReadOnlySpan<byte> value1);
        public int MatchTag(int tag, ReadOnlySpan<byte> value0, ReadOnlySpan<byte> value1, ReadOnlySpan<byte> value2);
        public int MatchTag(int tag, ReadOnlySpan<byte> value0, ReadOnlySpan<byte> value1, ReadOnlySpan<byte> value2, ReadOnlySpan<byte> value3);

        // Try-pattern getters (avoid nullable boxing)
        public bool TryGetInt32(int tag, out int value);
        public bool TryGetInt64(int tag, out long value);
        public bool TryGetDouble(int tag, out double value);
        public bool TryGetDecimal(int tag, out decimal value);
        public bool TryGetBool(int tag, out bool value);
    }
}
