using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer.Ascii
{
    public partial class MsgView : IMessageView
    {
        bool? IMessageView.GetBool(string name)
        {
            return this.GetBool(name);
        }

        bool? IMessageView.GetBool(int tag)
        {
            return this.GetBool(tag);
        }

        byte[]? IMessageView.GetByteArray(string name)
        {
            return this.GetByteArray(name);
        }

        byte[]? IMessageView.GetByteArray(int tag)
        {
            return this.GetByteArray(tag);
        }

        DateOnly? IMessageView.GetDateOnly(int tag)
        {
            return this.GetDateOnly(tag);
        }

        DateTime? IMessageView.GetDateTime(int tag)
        {
            return this.GetDateTime(tag);
        }

        decimal? IMessageView.GetDecimal(string name)
        {
            return this.GetDecimal(name);
        }

        decimal? IMessageView.GetDecimal(int tag)
        {
            return this.GetDecimal(tag);
        }

        double? IMessageView.GetDouble(int tag)
        {
            return this.GetDouble(tag);
        }

        double? IMessageView.GetDouble(string name)
        {
            return this.GetDouble(name);
        }

        IMessageView? IMessageView.GetGroupInstance(int i)
        {
            return this.GetGroupInstance(i);
        }

        int? IMessageView.GetInt32(string name)
        {
            return this.GetInt32(name);
        }

        int? IMessageView.GetInt32(int tag)
        {
            return this.GetInt32(tag);
        }

        Memory<byte>? IMessageView.GetMemory(int tag)
        {
            return this.GetMemory(tag);
        }

        MonthYear? IMessageView.GetMonthYear(int tag)
        {
            return this.GetMonthYear(tag);
        }

        string? IMessageView.GetString(int tag)
        {
            return this.GetString(tag);
        }

        string? IMessageView.GetString(string name)
        {
            return this.GetString(name);
        }

        TimeOnly? IMessageView.GetTimeOnly(int tag)
        {
            return this.GetTimeOnly(tag);
        }

        IMessageView? IMessageView.GetView(string name)
        {
            return this.GetView(name);
        }

        int IMessageView.GroupCount()
        {
            return this.GroupCount();
        }
    }
}
