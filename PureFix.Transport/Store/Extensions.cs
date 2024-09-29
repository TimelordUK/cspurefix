using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Store
{
    public static class Extensions
    {
        public static void CopyFrom(this IStandardHeader lhs, IStandardHeader rhs)
        {
            lhs.BeginString = rhs.BeginString;
            lhs.BodyLength = rhs.BodyLength;
            lhs.TargetSubID = rhs.TargetSubID;
            lhs.TargetCompID = rhs.TargetCompID;
            lhs.SenderSubID = rhs.SenderSubID;
            lhs.SenderCompID = rhs.SenderCompID;
            lhs.OrigSendingTime = rhs.OrigSendingTime;
            lhs.MsgSeqNum = rhs.MsgSeqNum;
            lhs.PossDupFlag = rhs.PossDupFlag;
            lhs.SendingTime = rhs.SendingTime;
            lhs.MsgType = rhs.MsgType;
        }
    }
}
