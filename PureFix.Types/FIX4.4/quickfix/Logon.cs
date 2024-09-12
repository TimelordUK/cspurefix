using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public class Logon : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public int? EncryptMethod { get; set; } // 98 INT
		public int? HeartBtInt { get; set; } // 108 INT
		public int? RawDataLength { get; set; } // 95 LENGTH
		public byte[]? RawData { get; set; } // 96 DATA
		public bool? ResetSeqNumFlag { get; set; } // 141 BOOLEAN
		public int? NextExpectedMsgSeqNum { get; set; } // 789 SEQNUM
		public int? MaxMessageSize { get; set; } // 383 LENGTH
		public NoMsgTypes? NoMsgTypes { get; set; }
		public bool? TestMessageIndicator { get; set; } // 464 BOOLEAN
		public string? Username { get; set; } // 553 STRING
		public string? Password { get; set; } // 554 STRING
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
