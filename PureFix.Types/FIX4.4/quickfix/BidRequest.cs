using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("k", FixVersion.FIX44)]
	public sealed class BidRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 390, Type = TagType.String, Offset = 1, Required = false)]
		public string? BidID { get; set; }
		
		[TagDetails(Tag = 391, Type = TagType.String, Offset = 2, Required = true)]
		public string? ClientBidID { get; set; }
		
		[TagDetails(Tag = 374, Type = TagType.String, Offset = 3, Required = true)]
		public string? BidRequestTransType { get; set; }
		
		[TagDetails(Tag = 392, Type = TagType.String, Offset = 4, Required = false)]
		public string? ListName { get; set; }
		
		[TagDetails(Tag = 393, Type = TagType.Int, Offset = 5, Required = true)]
		public int? TotNoRelatedSym { get; set; }
		
		[TagDetails(Tag = 394, Type = TagType.Int, Offset = 6, Required = true)]
		public int? BidType { get; set; }
		
		[TagDetails(Tag = 395, Type = TagType.Int, Offset = 7, Required = false)]
		public int? NumTickets { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 8, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 396, Type = TagType.Float, Offset = 9, Required = false)]
		public double? SideValue1 { get; set; }
		
		[TagDetails(Tag = 397, Type = TagType.Float, Offset = 10, Required = false)]
		public double? SideValue2 { get; set; }
		
		[Component(Offset = 11, Required = false)]
		public BidDescReqGrp? BidDescReqGrp { get; set; }
		
		[Component(Offset = 12, Required = false)]
		public BidCompReqGrp? BidCompReqGrp { get; set; }
		
		[TagDetails(Tag = 409, Type = TagType.Int, Offset = 13, Required = false)]
		public int? LiquidityIndType { get; set; }
		
		[TagDetails(Tag = 410, Type = TagType.Float, Offset = 14, Required = false)]
		public double? WtAverageLiquidity { get; set; }
		
		[TagDetails(Tag = 411, Type = TagType.Boolean, Offset = 15, Required = false)]
		public bool? ExchangeForPhysical { get; set; }
		
		[TagDetails(Tag = 412, Type = TagType.Float, Offset = 16, Required = false)]
		public double? OutMainCntryUIndex { get; set; }
		
		[TagDetails(Tag = 413, Type = TagType.Float, Offset = 17, Required = false)]
		public double? CrossPercent { get; set; }
		
		[TagDetails(Tag = 414, Type = TagType.Int, Offset = 18, Required = false)]
		public int? ProgRptReqs { get; set; }
		
		[TagDetails(Tag = 415, Type = TagType.Int, Offset = 19, Required = false)]
		public int? ProgPeriodInterval { get; set; }
		
		[TagDetails(Tag = 416, Type = TagType.Int, Offset = 20, Required = false)]
		public int? IncTaxInd { get; set; }
		
		[TagDetails(Tag = 121, Type = TagType.Boolean, Offset = 21, Required = false)]
		public bool? ForexReq { get; set; }
		
		[TagDetails(Tag = 417, Type = TagType.Int, Offset = 22, Required = false)]
		public int? NumBidders { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 23, Required = false)]
		public DateOnly? TradeDate { get; set; }
		
		[TagDetails(Tag = 418, Type = TagType.String, Offset = 24, Required = true)]
		public string? BidTradeType { get; set; }
		
		[TagDetails(Tag = 419, Type = TagType.String, Offset = 25, Required = true)]
		public string? BasisPxType { get; set; }
		
		[TagDetails(Tag = 443, Type = TagType.UtcTimestamp, Offset = 26, Required = false)]
		public DateTime? StrikeTime { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 27, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 28, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 29, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 30, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
