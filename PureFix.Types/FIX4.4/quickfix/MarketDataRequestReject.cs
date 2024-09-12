using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class MarketDataRequestReject : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? MDReqID { get; set; } // 262 STRING
		public string? MDReqRejReason { get; set; } // 281 CHAR
		public MDRjctGrp? MDRjctGrp { get; set; }
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
