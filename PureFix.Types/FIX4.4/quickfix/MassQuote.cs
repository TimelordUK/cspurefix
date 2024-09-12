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
		[TagDetails(131)]
		public string? QuoteReqID { get; set; } // STRING
		
		[TagDetails(117)]
		public string? QuoteID { get; set; } // STRING
		
		[TagDetails(537)]
		public int? QuoteType { get; set; } // INT
		
		[TagDetails(301)]
		public int? QuoteResponseLevel { get; set; } // INT
		
		public Parties? Parties { get; set; }
		[TagDetails(1)]
		public string? Account { get; set; } // STRING
		
		[TagDetails(660)]
		public int? AcctIDSource { get; set; } // INT
		
		[TagDetails(581)]
		public int? AccountType { get; set; } // INT
		
		[TagDetails(293)]
		public double? DefBidSize { get; set; } // QTY
		
		[TagDetails(294)]
		public double? DefOfferSize { get; set; } // QTY
		
		public QuotSetGrp? QuotSetGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
