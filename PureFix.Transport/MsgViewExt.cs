using PureFix.Buffer.Ascii;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport
{
    public static class MsgViewExt
    {
        public static int? HeartBtInt(this MsgView view)
        {
            return view.GetInt32((int)MsgTag.HeartBtInt);
        }

        public static string? SenderCompID(this MsgView view)
        {
            return view.GetString((int)MsgTag.SenderCompID);
        }

        public static string? Username(this MsgView view)
        {
            return view.GetString((int)MsgTag.Username);
        }

        public static string? Password(this MsgView view)
        {
            return view.GetString((int)MsgTag.Password);
        }

        public static int? MsgSeqNum(this MsgView view)
        {
            return view.GetInt32((int)MsgTag.MsgSeqNum);
        }

        public static int? BeginSeqNo(this MsgView view)
        {
            return view.GetInt32((int)MsgTag.BeginSeqNo);
        }

        public static int? EndSeqNo(this MsgView view)
        {
            return view.GetInt32((int)MsgTag.EndSeqNo);
        }

        public static string? MsgType(this MsgView view)
        {
            return view.GetString((int)MsgTag.MsgType);
        }

        public static DateTime? SendingTime(this MsgView view)
        {
            return view.GetDateTime((int)MsgTag.SendingTime);
        }
    }
}
