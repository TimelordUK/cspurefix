using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class AllocationReportAck : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(755, TagType.String)]
		public string? AllocReportID { get; set; }
		
		[TagDetails(70, TagType.String)]
		public string? AllocID { get; set; }
		
		[Component]
		public Parties? Parties { get; set; }
		
		[TagDetails(793, TagType.String)]
		public string? SecondaryAllocID { get; set; }
		
		[TagDetails(75, TagType.LocalDate)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(87, TagType.Int)]
		public int? AllocStatus { get; set; }
		
		[TagDetails(88, TagType.Int)]
		public int? AllocRejCode { get; set; }
		
		[TagDetails(794, TagType.Int)]
		public int? AllocReportType { get; set; }
		
		[TagDetails(808, TagType.Int)]
		public int? AllocIntermedReqType { get; set; }
		
		[TagDetails(573, TagType.String)]
		public string? MatchStatus { get; set; }
		
		[TagDetails(460, TagType.Int)]
		public int? Product { get; set; }
		
		[TagDetails(167, TagType.String)]
		public string? SecurityType { get; set; }
		
		[TagDetails(58, TagType.String)]
		public string? Text { get; set; }
		
		[TagDetails(354, TagType.Length)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(355, TagType.RawData)]
		public byte[]? EncodedText { get; set; }
		
		[Component]
		public AllocAckGrp? AllocAckGrp { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
		
	}
}
