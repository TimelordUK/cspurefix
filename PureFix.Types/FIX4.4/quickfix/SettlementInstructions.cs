using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("T", FixVersion.FIX44)]
	public sealed class SettlementInstructions : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(777, TagType.String)]
		public string? SettlInstMsgID { get; set; }
		
		[TagDetails(791, TagType.String)]
		public string? SettlInstReqID { get; set; }
		
		[TagDetails(160, TagType.String)]
		public string? SettlInstMode { get; set; }
		
		[TagDetails(792, TagType.Int)]
		public int? SettlInstReqRejCode { get; set; }
		
		[TagDetails(58, TagType.String)]
		public string? Text { get; set; }
		
		[TagDetails(354, TagType.Length)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(355, TagType.RawData)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(11, TagType.String)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[Component]
		public SettlInstGrp? SettlInstGrp { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
