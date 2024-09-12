using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class MassQuote : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? QuoteReqID { get; set; } // 131 STRING
		public string? QuoteID { get; set; } // 117 STRING
		public int? QuoteType { get; set; } // 537 INT
		public int? QuoteResponseLevel { get; set; } // 301 INT
		public Parties? Parties { get; set; }
		public string? Account { get; set; } // 1 STRING
		public int? AcctIDSource { get; set; } // 660 INT
		public int? AccountType { get; set; } // 581 INT
		public double? DefBidSize { get; set; } // 293 QTY
		public double? DefOfferSize { get; set; } // 294 QTY
		public QuotSetGrp? QuotSetGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
