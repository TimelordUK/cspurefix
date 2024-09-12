using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class StandardHeader
	{
		public string? BeginString { get; set; } // 8 STRING
		public int? BodyLength { get; set; } // 9 LENGTH
		public string? MsgType { get; set; } // 35 STRING
		public string? SenderCompID { get; set; } // 49 STRING
		public string? TargetCompID { get; set; } // 56 STRING
		public string? OnBehalfOfCompID { get; set; } // 115 STRING
		public string? DeliverToCompID { get; set; } // 128 STRING
		public int? SecureDataLen { get; set; } // 90 LENGTH
		public byte[]? SecureData { get; set; } // 91 DATA
		public int? MsgSeqNum { get; set; } // 34 SEQNUM
		public string? SenderSubID { get; set; } // 50 STRING
		public string? SenderLocationID { get; set; } // 142 STRING
		public string? TargetSubID { get; set; } // 57 STRING
		public string? TargetLocationID { get; set; } // 143 STRING
		public string? OnBehalfOfSubID { get; set; } // 116 STRING
		public string? OnBehalfOfLocationID { get; set; } // 144 STRING
		public string? DeliverToSubID { get; set; } // 129 STRING
		public string? DeliverToLocationID { get; set; } // 145 STRING
		public bool? PossDupFlag { get; set; } // 43 BOOLEAN
		public bool? PossResend { get; set; } // 97 BOOLEAN
		public DateTime? SendingTime { get; set; } // 52 UTCTIMESTAMP
		public DateTime? OrigSendingTime { get; set; } // 122 UTCTIMESTAMP
		public int? XmlDataLen { get; set; } // 212 LENGTH
		public byte[]? XmlData { get; set; } // 213 DATA
		public string? MessageEncoding { get; set; } // 347 STRING
		public int? LastMsgSeqNumProcessed { get; set; } // 369 SEQNUM
		public Hop? Hop { get; set; }
	}
}
