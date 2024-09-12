using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AT", FixVersion.FIX44)]
	public sealed class AllocationReportAck : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 755, Type = TagType.String, Offset = 1)]
		public string? AllocReportID { get; set; }
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 2)]
		public string? AllocID { get; set; }
		
		[Component(Offset = 3)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 793, Type = TagType.String, Offset = 4)]
		public string? SecondaryAllocID { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 5)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 6)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 87, Type = TagType.Int, Offset = 7)]
		public int? AllocStatus { get; set; }
		
		[TagDetails(Tag = 88, Type = TagType.Int, Offset = 8)]
		public int? AllocRejCode { get; set; }
		
		[TagDetails(Tag = 794, Type = TagType.Int, Offset = 9)]
		public int? AllocReportType { get; set; }
		
		[TagDetails(Tag = 808, Type = TagType.Int, Offset = 10)]
		public int? AllocIntermedReqType { get; set; }
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 11)]
		public string? MatchStatus { get; set; }
		
		[TagDetails(Tag = 460, Type = TagType.Int, Offset = 12)]
		public int? Product { get; set; }
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 13)]
		public string? SecurityType { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 14)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 15)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 16)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 17)]
		public AllocAckGrp? AllocAckGrp { get; set; }
		
		[Component(Offset = 18)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
