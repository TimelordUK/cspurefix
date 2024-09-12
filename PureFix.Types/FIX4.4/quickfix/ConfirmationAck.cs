using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class ConfirmationAck : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(664, TagType.String)]
		public string? ConfirmID { get; set; }
		
		[TagDetails(75, TagType.LocalDate)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(940, TagType.Int)]
		public int? AffirmStatus { get; set; }
		
		[TagDetails(774, TagType.Int)]
		public int? ConfirmRejReason { get; set; }
		
		[TagDetails(573, TagType.String)]
		public string? MatchStatus { get; set; }
		
		[TagDetails(58, TagType.String)]
		public string? Text { get; set; }
		
		[TagDetails(354, TagType.Length)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(355, TagType.RawData)]
		public byte[]? EncodedText { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
