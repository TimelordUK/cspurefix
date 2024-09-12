using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("i", FixVersion.FIX44)]
	public sealed class MassQuote : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 131, Type = TagType.String, Offset = 1)]
		public string? QuoteReqID { get; set; }
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 2)]
		public string? QuoteID { get; set; }
		
		[TagDetails(Tag = 537, Type = TagType.Int, Offset = 3)]
		public int? QuoteType { get; set; }
		
		[TagDetails(Tag = 301, Type = TagType.Int, Offset = 4)]
		public int? QuoteResponseLevel { get; set; }
		
		[Component(Offset = 5)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 6)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 7)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 8)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 293, Type = TagType.Float, Offset = 9)]
		public double? DefBidSize { get; set; }
		
		[TagDetails(Tag = 294, Type = TagType.Float, Offset = 10)]
		public double? DefOfferSize { get; set; }
		
		[Component(Offset = 11)]
		public QuotSetGrp? QuotSetGrp { get; set; }
		
		[Component(Offset = 12)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
