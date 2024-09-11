using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types.FIX4._4.quickfix.set
{
    public class StandardHeader
    {
        public string? BeginString { get; set; }  // [1] 8 (String)
        public int? BodyLength { get; set; } // [2] 9 (Length)
        public string? MsgType { get; set; } // [3] 35 (String)
        public string? SenderCompID { get; set; } // [4] 49 (String)
        public string? TargetCompID { get; set; } // [5] 56 (String)
        public string? OnBehalfOfCompID { get; set; } // [6] 115 (String)
        public string? DeliverToCompID { get; set; } // [7] 128 (String)
        public int? SecureDataLen { get; set; } // [8] 90 (Length)
        public byte[]? SecureData { get; set; } // [9] 91 (RawData)
        public int? MsgSeqNum { get; set; } // [10] 34 (Int)
        public string? SenderSubID { get; set; } // [11] 50 (String)
        public string? SenderLocationID { get; set; } // [12] 142 (String)
        public string? TargetSubID { get; set; } // [13] 57 (String)
        public string? TargetLocationID { get; set; } // [14] 143 (String)
        public string? OnBehalfOfSubID { get; set; } // [15] 116 (String)
        public string? OnBehalfOfLocationID { get; set; } // [16] 144 (String)
        public string? DeliverToSubID { get; set; } // [17] 129 (String)
        public string? DeliverToLocationID { get; set; } // [18] 145 (String)
        public bool? PossDupFlag { get; set; } // [19] 43 (Boolean)
        public bool? PossResend { get; set; } // [20] 97 (Boolean)
        public DateTime? SendingTime { get; set; } // [21] 52 (UtcTimestamp)
        public DateTime? OrigSendingTime { get; set; } // [22] 122 (UtcTimestamp)
        public int? XmlDataLen { get; set; } // [23] 212 (Length)
        public byte[]? XmlData { get; set; } // [24] 213 (RawData)
        public string? MessageEncoding { get; set; } // [25] 347 (String)
        public int? LastMsgSeqNumProcessed { get; set; } // [26] 369 (Int)
        public Hop? Hop { get; set; } // [27] NoHops.627, HopCompID.628 .. HopRefID.630// [26] 369 (Int)

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (BeginString != null)
            {
                sb.Append($"BeginString = {BeginString}, ");
            }
            if (BodyLength != null)
            {
                sb.Append($"BodyLength = {BodyLength}, ");
            }
            if (MsgType != null)
            {
                sb.Append($"MsgType = {MsgType}, ");
            }
            if (SenderCompID != null)
            {
                sb.Append($"SenderCompID = {SenderCompID}, ");
            }
            if (TargetCompID != null)
            {
                sb.Append($"TargetCompID  = {TargetCompID}, ");
            }
            if (MsgSeqNum != null)
            {
                sb.Append($"MsgSeqNum = {MsgSeqNum}, ");
            }
            if (TargetSubID != null)
            {
                sb.Append($"TargetSubID = {TargetSubID}, ");
            }
            if (SendingTime != null)
            {
                sb.Append($"SendingTime = {SendingTime}, ");
            }

            return sb.ToString();
        }
    }
}
