using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AT", FixVersion.FIX44)]
	public sealed class AllocationReportAck : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 755, Type = TagType.String, Offset = 1, Required = true)]
		public string? AllocReportID { get; set; }
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 2, Required = true)]
		public string? AllocID { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 793, Type = TagType.String, Offset = 4, Required = false)]
		public string? SecondaryAllocID { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 5, Required = false)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 6, Required = true)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 87, Type = TagType.Int, Offset = 7, Required = true)]
		public int? AllocStatus { get; set; }
		
		[TagDetails(Tag = 88, Type = TagType.Int, Offset = 8, Required = false)]
		public int? AllocRejCode { get; set; }
		
		[TagDetails(Tag = 794, Type = TagType.Int, Offset = 9, Required = false)]
		public int? AllocReportType { get; set; }
		
		[TagDetails(Tag = 808, Type = TagType.Int, Offset = 10, Required = false)]
		public int? AllocIntermedReqType { get; set; }
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 11, Required = false)]
		public string? MatchStatus { get; set; }
		
		[TagDetails(Tag = 460, Type = TagType.Int, Offset = 12, Required = false)]
		public int? Product { get; set; }
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 13, Required = false)]
		public string? SecurityType { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 14, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 15, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 16, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 17, Required = false)]
		public AllocAckGrp? AllocAckGrp { get; set; }
		
		[Component(Offset = 18, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
