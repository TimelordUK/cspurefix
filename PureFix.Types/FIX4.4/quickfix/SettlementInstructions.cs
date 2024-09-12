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
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 777, Type = TagType.String, Offset = 1)]
		public string? SettlInstMsgID { get; set; }
		
		[TagDetails(Tag = 791, Type = TagType.String, Offset = 2)]
		public string? SettlInstReqID { get; set; }
		
		[TagDetails(Tag = 160, Type = TagType.String, Offset = 3)]
		public string? SettlInstMode { get; set; }
		
		[TagDetails(Tag = 792, Type = TagType.Int, Offset = 4)]
		public int? SettlInstReqRejCode { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 5)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 6)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 7)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 8)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 9)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 10)]
		public SettlInstGrp? SettlInstGrp { get; set; }
		
		[Component(Offset = 11)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
