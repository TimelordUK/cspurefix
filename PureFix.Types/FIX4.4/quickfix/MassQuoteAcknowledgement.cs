using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("b")]
	public sealed class MassQuoteAcknowledgement : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(131, TagType.String)]
		public string? QuoteReqID { get; set; }
		
		[TagDetails(117, TagType.String)]
		public string? QuoteID { get; set; }
		
		[TagDetails(297, TagType.Int)]
		public int? QuoteStatus { get; set; }
		
		[TagDetails(300, TagType.Int)]
		public int? QuoteRejectReason { get; set; }
		
		[TagDetails(301, TagType.Int)]
		public int? QuoteResponseLevel { get; set; }
		
		[TagDetails(537, TagType.Int)]
		public int? QuoteType { get; set; }
		
		[Component]
		public Parties? Parties { get; set; }
		
		[TagDetails(1, TagType.String)]
		public string? Account { get; set; }
		
		[TagDetails(660, TagType.Int)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(581, TagType.Int)]
		public int? AccountType { get; set; }
		
		[TagDetails(58, TagType.String)]
		public string? Text { get; set; }
		
		[TagDetails(354, TagType.Length)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(355, TagType.RawData)]
		public byte[]? EncodedText { get; set; }
		
		[Component]
		public QuotSetAckGrp? QuotSetAckGrp { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
