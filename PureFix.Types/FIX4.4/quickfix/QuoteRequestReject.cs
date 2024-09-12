using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class QuoteRequestReject : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? QuoteReqID { get; set; } // 131 STRING
		public string? RFQReqID { get; set; } // 644 STRING
		public int? QuoteRequestRejectReason { get; set; } // 658 INT
		public QuotReqRjctGrp? QuotReqRjctGrp { get; set; }
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
