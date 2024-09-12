using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class MassQuoteAcknowledgement : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? QuoteReqID { get; set; } // 131 STRING
		public string? QuoteID { get; set; } // 117 STRING
		public int? QuoteStatus { get; set; } // 297 INT
		public int? QuoteRejectReason { get; set; } // 300 INT
		public int? QuoteResponseLevel { get; set; } // 301 INT
		public int? QuoteType { get; set; } // 537 INT
		public Parties? Parties { get; set; }
		public string? Account { get; set; } // 1 STRING
		public int? AcctIDSource { get; set; } // 660 INT
		public int? AccountType { get; set; } // 581 INT
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public QuotSetAckGrp? QuotSetAckGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
