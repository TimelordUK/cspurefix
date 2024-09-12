using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class Reject : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public int? RefSeqNum { get; set; } // 45 SEQNUM
		public int? RefTagID { get; set; } // 371 INT
		public string? RefMsgType { get; set; } // 372 STRING
		public int? SessionRejectReason { get; set; } // 373 INT
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
