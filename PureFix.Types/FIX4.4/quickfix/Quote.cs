using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("S")]
	public sealed class Quote : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(131, TagType.String)]
		public string? QuoteReqID { get; set; }
		
		[TagDetails(117, TagType.String)]
		public string? QuoteID { get; set; }
		
		[TagDetails(693, TagType.String)]
		public string? QuoteRespID { get; set; }
		
		[TagDetails(537, TagType.Int)]
		public int? QuoteType { get; set; }
		
		[Component]
		public QuotQualGrp? QuotQualGrp { get; set; }
		
		[TagDetails(301, TagType.Int)]
		public int? QuoteResponseLevel { get; set; }
		
		[Component]
		public Parties? Parties { get; set; }
		
		[TagDetails(336, TagType.String)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(625, TagType.String)]
		public string? TradingSessionSubID { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[Component]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(54, TagType.String)]
		public string? Side { get; set; }
		
		[Component]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(63, TagType.String)]
		public string? SettlType { get; set; }
		
		[TagDetails(64, TagType.LocalDate)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(193, TagType.LocalDate)]
		public DateTime? SettlDate2 { get; set; }
		
		[TagDetails(192, TagType.Float)]
		public double? OrderQty2 { get; set; }
		
		[TagDetails(15, TagType.String)]
		public string? Currency { get; set; }
		
		[Component]
		public Stipulations? Stipulations { get; set; }
		
		[TagDetails(1, TagType.String)]
		public string? Account { get; set; }
		
		[TagDetails(660, TagType.Int)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(581, TagType.Int)]
		public int? AccountType { get; set; }
		
		[Component]
		public LegQuotGrp? LegQuotGrp { get; set; }
		
		[TagDetails(132, TagType.Float)]
		public double? BidPx { get; set; }
		
		[TagDetails(133, TagType.Float)]
		public double? OfferPx { get; set; }
		
		[TagDetails(645, TagType.Float)]
		public double? MktBidPx { get; set; }
		
		[TagDetails(646, TagType.Float)]
		public double? MktOfferPx { get; set; }
		
		[TagDetails(647, TagType.Float)]
		public double? MinBidSize { get; set; }
		
		[TagDetails(134, TagType.Float)]
		public double? BidSize { get; set; }
		
		[TagDetails(648, TagType.Float)]
		public double? MinOfferSize { get; set; }
		
		[TagDetails(135, TagType.Float)]
		public double? OfferSize { get; set; }
		
		[TagDetails(62, TagType.UtcTimestamp)]
		public DateTime? ValidUntilTime { get; set; }
		
		[TagDetails(188, TagType.Float)]
		public double? BidSpotRate { get; set; }
		
		[TagDetails(190, TagType.Float)]
		public double? OfferSpotRate { get; set; }
		
		[TagDetails(189, TagType.Float)]
		public double? BidForwardPoints { get; set; }
		
		[TagDetails(191, TagType.Float)]
		public double? OfferForwardPoints { get; set; }
		
		[TagDetails(631, TagType.Float)]
		public double? MidPx { get; set; }
		
		[TagDetails(632, TagType.Float)]
		public double? BidYield { get; set; }
		
		[TagDetails(633, TagType.Float)]
		public double? MidYield { get; set; }
		
		[TagDetails(634, TagType.Float)]
		public double? OfferYield { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(40, TagType.String)]
		public string? OrdType { get; set; }
		
		[TagDetails(642, TagType.Float)]
		public double? BidForwardPoints2 { get; set; }
		
		[TagDetails(643, TagType.Float)]
		public double? OfferForwardPoints2 { get; set; }
		
		[TagDetails(656, TagType.Float)]
		public double? SettlCurrBidFxRate { get; set; }
		
		[TagDetails(657, TagType.Float)]
		public double? SettlCurrOfferFxRate { get; set; }
		
		[TagDetails(156, TagType.String)]
		public string? SettlCurrFxRateCalc { get; set; }
		
		[TagDetails(13, TagType.String)]
		public string? CommType { get; set; }
		
		[TagDetails(12, TagType.Float)]
		public double? Commission { get; set; }
		
		[TagDetails(582, TagType.Int)]
		public int? CustOrderCapacity { get; set; }
		
		[TagDetails(100, TagType.String)]
		public string? ExDestination { get; set; }
		
		[TagDetails(528, TagType.String)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(423, TagType.Int)]
		public int? PriceType { get; set; }
		
		[Component]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component]
		public YieldData? YieldData { get; set; }
		
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
