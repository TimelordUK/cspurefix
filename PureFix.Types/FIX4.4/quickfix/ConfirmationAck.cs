using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AU", FixVersion.FIX44)]
	public sealed class ConfirmationAck : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 664, Type = TagType.String, Offset = 1, Required = true)]
		public string? ConfirmID { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 2, Required = true)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 3, Required = true)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 940, Type = TagType.Int, Offset = 4, Required = true)]
		public int? AffirmStatus { get; set; }
		
		[TagDetails(Tag = 774, Type = TagType.Int, Offset = 5, Required = false)]
		public int? ConfirmRejReason { get; set; }
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 6, Required = false)]
		public string? MatchStatus { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 7, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 8, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 9, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 10, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		public override string? MsgType => StandardHeader?.MsgType;
		public override int? BodyLength => StandardHeader?.BodyLength;
	}
}
