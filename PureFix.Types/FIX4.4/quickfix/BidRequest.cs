using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("k")]
	public sealed class BidRequest : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(390, TagType.String)]
		public string? BidID { get; set; }
		
		[TagDetails(391, TagType.String)]
		public string? ClientBidID { get; set; }
		
		[TagDetails(374, TagType.String)]
		public string? BidRequestTransType { get; set; }
		
		[TagDetails(392, TagType.String)]
		public string? ListName { get; set; }
		
		[TagDetails(393, TagType.Int)]
		public int? TotNoRelatedSym { get; set; }
		
		[TagDetails(394, TagType.Int)]
		public int? BidType { get; set; }
		
		[TagDetails(395, TagType.Int)]
		public int? NumTickets { get; set; }
		
		[TagDetails(15, TagType.String)]
		public string? Currency { get; set; }
		
		[TagDetails(396, TagType.Float)]
		public double? SideValue1 { get; set; }
		
		[TagDetails(397, TagType.Float)]
		public double? SideValue2 { get; set; }
		
		[Component]
		public BidDescReqGrp? BidDescReqGrp { get; set; }
		
		[Component]
		public BidCompReqGrp? BidCompReqGrp { get; set; }
		
		[TagDetails(409, TagType.Int)]
		public int? LiquidityIndType { get; set; }
		
		[TagDetails(410, TagType.Float)]
		public double? WtAverageLiquidity { get; set; }
		
		[TagDetails(411, TagType.Boolean)]
		public bool? ExchangeForPhysical { get; set; }
		
		[TagDetails(412, TagType.Float)]
		public double? OutMainCntryUIndex { get; set; }
		
		[TagDetails(413, TagType.Float)]
		public double? CrossPercent { get; set; }
		
		[TagDetails(414, TagType.Int)]
		public int? ProgRptReqs { get; set; }
		
		[TagDetails(415, TagType.Int)]
		public int? ProgPeriodInterval { get; set; }
		
		[TagDetails(416, TagType.Int)]
		public int? IncTaxInd { get; set; }
		
		[TagDetails(121, TagType.Boolean)]
		public bool? ForexReq { get; set; }
		
		[TagDetails(417, TagType.Int)]
		public int? NumBidders { get; set; }
		
		[TagDetails(75, TagType.LocalDate)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(418, TagType.String)]
		public string? BidTradeType { get; set; }
		
		[TagDetails(419, TagType.String)]
		public string? BasisPxType { get; set; }
		
		[TagDetails(443, TagType.UtcTimestamp)]
		public DateTime? StrikeTime { get; set; }
		
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
