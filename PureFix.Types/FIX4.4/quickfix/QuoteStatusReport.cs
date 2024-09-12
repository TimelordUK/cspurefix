using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AI", FixVersion.FIX44)]
	public sealed class QuoteStatusReport : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 649, Type = TagType.String, Offset = 1)]
		public string? QuoteStatusReqID { get; set; }
		
		[TagDetails(Tag = 131, Type = TagType.String, Offset = 2)]
		public string? QuoteReqID { get; set; }
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 3)]
		public string? QuoteID { get; set; }
		
		[TagDetails(Tag = 693, Type = TagType.String, Offset = 4)]
		public string? QuoteRespID { get; set; }
		
		[TagDetails(Tag = 537, Type = TagType.Int, Offset = 5)]
		public int? QuoteType { get; set; }
		
		[Component(Offset = 6)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 7)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 8)]
		public string? TradingSessionSubID { get; set; }
		
		[Component(Offset = 9)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 10)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 11)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 12)]
		public string? Side { get; set; }
		
		[Component(Offset = 13)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 14)]
		public string? SettlType { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 15)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 16)]
		public DateTime? SettlDate2 { get; set; }
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 17)]
		public double? OrderQty2 { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 18)]
		public string? Currency { get; set; }
		
		[Component(Offset = 19)]
		public Stipulations? Stipulations { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 20)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 21)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 22)]
		public int? AccountType { get; set; }
		
		[Component(Offset = 23)]
		public LegQuotStatGrp? LegQuotStatGrp { get; set; }
		
		[Component(Offset = 24)]
		public QuotQualGrp? QuotQualGrp { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 25)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 26)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 27)]
		public int? PriceType { get; set; }
		
		[Component(Offset = 28)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component(Offset = 29)]
		public YieldData? YieldData { get; set; }
		
		[TagDetails(Tag = 132, Type = TagType.Float, Offset = 30)]
		public double? BidPx { get; set; }
		
		[TagDetails(Tag = 133, Type = TagType.Float, Offset = 31)]
		public double? OfferPx { get; set; }
		
		[TagDetails(Tag = 645, Type = TagType.Float, Offset = 32)]
		public double? MktBidPx { get; set; }
		
		[TagDetails(Tag = 646, Type = TagType.Float, Offset = 33)]
		public double? MktOfferPx { get; set; }
		
		[TagDetails(Tag = 647, Type = TagType.Float, Offset = 34)]
		public double? MinBidSize { get; set; }
		
		[TagDetails(Tag = 134, Type = TagType.Float, Offset = 35)]
		public double? BidSize { get; set; }
		
		[TagDetails(Tag = 648, Type = TagType.Float, Offset = 36)]
		public double? MinOfferSize { get; set; }
		
		[TagDetails(Tag = 135, Type = TagType.Float, Offset = 37)]
		public double? OfferSize { get; set; }
		
		[TagDetails(Tag = 62, Type = TagType.UtcTimestamp, Offset = 38)]
		public DateTime? ValidUntilTime { get; set; }
		
		[TagDetails(Tag = 188, Type = TagType.Float, Offset = 39)]
		public double? BidSpotRate { get; set; }
		
		[TagDetails(Tag = 190, Type = TagType.Float, Offset = 40)]
		public double? OfferSpotRate { get; set; }
		
		[TagDetails(Tag = 189, Type = TagType.Float, Offset = 41)]
		public double? BidForwardPoints { get; set; }
		
		[TagDetails(Tag = 191, Type = TagType.Float, Offset = 42)]
		public double? OfferForwardPoints { get; set; }
		
		[TagDetails(Tag = 631, Type = TagType.Float, Offset = 43)]
		public double? MidPx { get; set; }
		
		[TagDetails(Tag = 632, Type = TagType.Float, Offset = 44)]
		public double? BidYield { get; set; }
		
		[TagDetails(Tag = 633, Type = TagType.Float, Offset = 45)]
		public double? MidYield { get; set; }
		
		[TagDetails(Tag = 634, Type = TagType.Float, Offset = 46)]
		public double? OfferYield { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 47)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 48)]
		public string? OrdType { get; set; }
		
		[TagDetails(Tag = 642, Type = TagType.Float, Offset = 49)]
		public double? BidForwardPoints2 { get; set; }
		
		[TagDetails(Tag = 643, Type = TagType.Float, Offset = 50)]
		public double? OfferForwardPoints2 { get; set; }
		
		[TagDetails(Tag = 656, Type = TagType.Float, Offset = 51)]
		public double? SettlCurrBidFxRate { get; set; }
		
		[TagDetails(Tag = 657, Type = TagType.Float, Offset = 52)]
		public double? SettlCurrOfferFxRate { get; set; }
		
		[TagDetails(Tag = 156, Type = TagType.String, Offset = 53)]
		public string? SettlCurrFxRateCalc { get; set; }
		
		[TagDetails(Tag = 13, Type = TagType.String, Offset = 54)]
		public string? CommType { get; set; }
		
		[TagDetails(Tag = 12, Type = TagType.Float, Offset = 55)]
		public double? Commission { get; set; }
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 56)]
		public int? CustOrderCapacity { get; set; }
		
		[TagDetails(Tag = 100, Type = TagType.String, Offset = 57)]
		public string? ExDestination { get; set; }
		
		[TagDetails(Tag = 297, Type = TagType.Int, Offset = 58)]
		public int? QuoteStatus { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 59)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 60)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 61)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 62)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
