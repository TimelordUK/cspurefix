using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("b", FixVersion.FIX44)]
	public sealed class MassQuoteAcknowledgement : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 131, Type = TagType.String, Offset = 1, Required = false)]
		public string? QuoteReqID { get; set; }
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 2, Required = false)]
		public string? QuoteID { get; set; }
		
		[TagDetails(Tag = 297, Type = TagType.Int, Offset = 3, Required = true)]
		public int? QuoteStatus { get; set; }
		
		[TagDetails(Tag = 300, Type = TagType.Int, Offset = 4, Required = false)]
		public int? QuoteRejectReason { get; set; }
		
		[TagDetails(Tag = 301, Type = TagType.Int, Offset = 5, Required = false)]
		public int? QuoteResponseLevel { get; set; }
		
		[TagDetails(Tag = 537, Type = TagType.Int, Offset = 6, Required = false)]
		public int? QuoteType { get; set; }
		
		[Component(Offset = 7, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 8, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 9, Required = false)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 10, Required = false)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 11, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 12, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 13, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 14, Required = false)]
		public QuotSetAckGrp? QuotSetAckGrp { get; set; }
		
		[Component(Offset = 15, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		public override string? MsgType => StandardHeader?.MsgType;
		public override int? BodyLength => StandardHeader?.BodyLength;
	}
}
