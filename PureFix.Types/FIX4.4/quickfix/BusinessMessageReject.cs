using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public class BusinessMessageReject : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public int? RefSeqNum { get; set; } // 45 SEQNUM
		public string? RefMsgType { get; set; } // 372 STRING
		public string? BusinessRejectRefID { get; set; } // 379 STRING
		public int? BusinessRejectReason { get; set; } // 380 INT
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
