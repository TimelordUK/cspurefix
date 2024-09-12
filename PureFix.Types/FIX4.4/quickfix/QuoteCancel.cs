using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class QuoteCancel : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(131, TagType.String)]
		public string? QuoteReqID { get; set; }
		
		[TagDetails(117, TagType.String)]
		public string? QuoteID { get; set; }
		
		[TagDetails(298, TagType.Int)]
		public int? QuoteCancelType { get; set; }
		
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
		
		[TagDetails(336, TagType.String)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(625, TagType.String)]
		public string? TradingSessionSubID { get; set; }
		
		[Component]
		public QuotCxlEntriesGrp? QuotCxlEntriesGrp { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
