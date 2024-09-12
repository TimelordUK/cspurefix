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
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 664, Type = TagType.String, Offset = 1)]
		public string? ConfirmID { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 2)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 3)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 940, Type = TagType.Int, Offset = 4)]
		public int? AffirmStatus { get; set; }
		
		[TagDetails(Tag = 774, Type = TagType.Int, Offset = 5)]
		public int? ConfirmRejReason { get; set; }
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 6)]
		public string? MatchStatus { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 7)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 8)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 9)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 10)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
