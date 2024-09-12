using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("k", FixVersion.FIX44)]
	public sealed class BidRequest : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 390, Type = TagType.String, Offset = 1)]
		public string? BidID { get; set; }
		
		[TagDetails(Tag = 391, Type = TagType.String, Offset = 2)]
		public string? ClientBidID { get; set; }
		
		[TagDetails(Tag = 374, Type = TagType.String, Offset = 3)]
		public string? BidRequestTransType { get; set; }
		
		[TagDetails(Tag = 392, Type = TagType.String, Offset = 4)]
		public string? ListName { get; set; }
		
		[TagDetails(Tag = 393, Type = TagType.Int, Offset = 5)]
		public int? TotNoRelatedSym { get; set; }
		
		[TagDetails(Tag = 394, Type = TagType.Int, Offset = 6)]
		public int? BidType { get; set; }
		
		[TagDetails(Tag = 395, Type = TagType.Int, Offset = 7)]
		public int? NumTickets { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 8)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 396, Type = TagType.Float, Offset = 9)]
		public double? SideValue1 { get; set; }
		
		[TagDetails(Tag = 397, Type = TagType.Float, Offset = 10)]
		public double? SideValue2 { get; set; }
		
		[Component(Offset = 11)]
		public BidDescReqGrp? BidDescReqGrp { get; set; }
		
		[Component(Offset = 12)]
		public BidCompReqGrp? BidCompReqGrp { get; set; }
		
		[TagDetails(Tag = 409, Type = TagType.Int, Offset = 13)]
		public int? LiquidityIndType { get; set; }
		
		[TagDetails(Tag = 410, Type = TagType.Float, Offset = 14)]
		public double? WtAverageLiquidity { get; set; }
		
		[TagDetails(Tag = 411, Type = TagType.Boolean, Offset = 15)]
		public bool? ExchangeForPhysical { get; set; }
		
		[TagDetails(Tag = 412, Type = TagType.Float, Offset = 16)]
		public double? OutMainCntryUIndex { get; set; }
		
		[TagDetails(Tag = 413, Type = TagType.Float, Offset = 17)]
		public double? CrossPercent { get; set; }
		
		[TagDetails(Tag = 414, Type = TagType.Int, Offset = 18)]
		public int? ProgRptReqs { get; set; }
		
		[TagDetails(Tag = 415, Type = TagType.Int, Offset = 19)]
		public int? ProgPeriodInterval { get; set; }
		
		[TagDetails(Tag = 416, Type = TagType.Int, Offset = 20)]
		public int? IncTaxInd { get; set; }
		
		[TagDetails(Tag = 121, Type = TagType.Boolean, Offset = 21)]
		public bool? ForexReq { get; set; }
		
		[TagDetails(Tag = 417, Type = TagType.Int, Offset = 22)]
		public int? NumBidders { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 23)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(Tag = 418, Type = TagType.String, Offset = 24)]
		public string? BidTradeType { get; set; }
		
		[TagDetails(Tag = 419, Type = TagType.String, Offset = 25)]
		public string? BasisPxType { get; set; }
		
		[TagDetails(Tag = 443, Type = TagType.UtcTimestamp, Offset = 26)]
		public DateTime? StrikeTime { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 27)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 28)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 29)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 30)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
