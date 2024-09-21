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
    }
}
