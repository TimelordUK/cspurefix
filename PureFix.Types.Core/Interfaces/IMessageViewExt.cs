
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport
{
    public static class IMessageViewExt
    {
        public static int? HeartBtInt(this IMessageView view)
        {
            return view.GetInt32((int)MsgTag.HeartBtInt);
        }

        public static string? SenderCompID(this IMessageView view)
        {
            return view.GetString((int)MsgTag.SenderCompID);
        }

        public static string? Username(this IMessageView view)
        {
            return view.GetString((int)MsgTag.Username);
        }

        public static string? Password(this IMessageView view)
        {
            return view.GetString((int)MsgTag.Password);
        }

        public static int? MsgSeqNum(this IMessageView view)
        {
            return view.GetInt32((int)MsgTag.MsgSeqNum);
        }

        public static int? BeginSeqNo(this IMessageView view)
        {
            return view.GetInt32((int)MsgTag.BeginSeqNo);
        }

        public static int? EndSeqNo(this IMessageView view)
        {
            return view.GetInt32((int)MsgTag.EndSeqNo);
        }

        public static string? MsgType(this IMessageView view)
        {
            return view.GetString((int)MsgTag.MsgType);
        }

        public static DateTime? SendingTime(this IMessageView view)
        {
            return view.GetDateTime((int)MsgTag.SendingTime);
        }

        public static bool? ResetSeqNumFlag(this IMessageView view)
        {
            return view.GetBool((int)MsgTag.ResetSeqNumFlag);
        }
    }
}
