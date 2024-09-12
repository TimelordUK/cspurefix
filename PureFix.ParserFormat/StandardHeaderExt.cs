using System.Diagnostics.CodeAnalysis;
using System.Text;
using PureFix.Buffer.Ascii;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.ParserFormat
{
    /*
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
     */


    public static class StandardHeaderExt
    {
        public static void Parse(this StandardHeader instance, MsgView? view)
        {
            instance.BeginString = view?.GetString(8);
            instance.BodyLength = view?.GetInt32(9);
            instance.MsgType = view?.GetString(35);
            instance.SenderCompID = view?.GetString(49);
            instance.TargetCompID = view?.GetString(56);
            instance.OnBehalfOfCompID = view?.GetString(115);
            instance.DeliverToCompID = view?.GetString(128);
            instance.SecureDataLen = view?.GetInt32(91);
            instance.MsgSeqNum = view?.GetInt32(34);
            instance.SenderSubID = view?.GetString(50);
            instance.SenderLocationID = view?.GetString(142);
            instance.TargetSubID = view?.GetString(57);
            instance.TargetLocationID = view?.GetString(143);
            instance.OnBehalfOfSubID = view?.GetString(116);
            instance.OnBehalfOfLocationID = view?.GetString(144);
            instance.DeliverToSubID = view?.GetString(129);
            instance.DeliverToLocationID = view?.GetString(145);
            instance.PossDupFlag = view?.GetBool(43);
            instance.PossResend = view?.GetBool(97);
            instance.SendingTime = view?.GetDateTime(52);
            instance.OrigSendingTime = view?.GetDateTime(122);
            instance.XmlDataLen = view?.GetInt32(212);
            instance.XmlData = view?.GetByteArray(213);
            instance.MessageEncoding = view?.GetString(347);
            instance.LastMsgSeqNumProcessed = view?.GetInt32(369);
            instance.Hop?.Parse(view?.GetView("Hop"));
        }
    }
}
