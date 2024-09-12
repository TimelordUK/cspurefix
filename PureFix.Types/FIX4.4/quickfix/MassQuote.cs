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
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(131, TagType.String)]
		public string? QuoteReqID { get; set; }
		
		[TagDetails(117, TagType.String)]
		public string? QuoteID { get; set; }
		
		[TagDetails(537, TagType.Int)]
		public int? QuoteType { get; set; }
		
		[TagDetails(301, TagType.Int)]
		public int? QuoteResponseLevel { get; set; }
		
		[Component]
		public Parties? Parties { get; set; }
		
		[TagDetails(1, TagType.String)]
		public string? Account { get; set; }
		
		[TagDetails(660, TagType.Int)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(581, TagType.Int)]
		public int? AccountType { get; set; }
		
		[TagDetails(293, TagType.Float)]
		public double? DefBidSize { get; set; }
		
		[TagDetails(294, TagType.Float)]
		public double? DefOfferSize { get; set; }
		
		[Component]
		public QuotSetGrp? QuotSetGrp { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
		
	}
}
