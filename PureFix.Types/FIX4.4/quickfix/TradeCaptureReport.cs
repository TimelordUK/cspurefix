using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AE", FixVersion.FIX44)]
	public sealed class TradeCaptureReport : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 571, Type = TagType.String, Offset = 1)]
		public string? TradeReportID { get; set; }
		
		[TagDetails(Tag = 487, Type = TagType.Int, Offset = 2)]
		public int? TradeReportTransType { get; set; }
		
		[TagDetails(Tag = 856, Type = TagType.Int, Offset = 3)]
		public int? TradeReportType { get; set; }
		
		[TagDetails(Tag = 568, Type = TagType.String, Offset = 4)]
		public string? TradeRequestID { get; set; }
		
		[TagDetails(Tag = 828, Type = TagType.Int, Offset = 5)]
		public int? TrdType { get; set; }
		
		[TagDetails(Tag = 829, Type = TagType.Int, Offset = 6)]
		public int? TrdSubType { get; set; }
		
		[TagDetails(Tag = 855, Type = TagType.Int, Offset = 7)]
		public int? SecondaryTrdType { get; set; }
		
		[TagDetails(Tag = 830, Type = TagType.String, Offset = 8)]
		public string? TransferReason { get; set; }
		
		[TagDetails(Tag = 150, Type = TagType.String, Offset = 9)]
		public string? ExecType { get; set; }
		
		[TagDetails(Tag = 748, Type = TagType.Int, Offset = 10)]
		public int? TotNumTradeReports { get; set; }
		
		[TagDetails(Tag = 912, Type = TagType.Boolean, Offset = 11)]
		public bool? LastRptRequested { get; set; }
		
		[TagDetails(Tag = 325, Type = TagType.Boolean, Offset = 12)]
		public bool? UnsolicitedIndicator { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 13)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(Tag = 572, Type = TagType.String, Offset = 14)]
		public string? TradeReportRefID { get; set; }
		
		[TagDetails(Tag = 881, Type = TagType.String, Offset = 15)]
		public string? SecondaryTradeReportRefID { get; set; }
		
		[TagDetails(Tag = 818, Type = TagType.String, Offset = 16)]
		public string? SecondaryTradeReportID { get; set; }
		
		[TagDetails(Tag = 820, Type = TagType.String, Offset = 17)]
		public string? TradeLinkID { get; set; }
		
		[TagDetails(Tag = 880, Type = TagType.String, Offset = 18)]
		public string? TrdMatchID { get; set; }
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 19)]
		public string? ExecID { get; set; }
		
		[TagDetails(Tag = 39, Type = TagType.String, Offset = 20)]
		public string? OrdStatus { get; set; }
		
		[TagDetails(Tag = 527, Type = TagType.String, Offset = 21)]
		public string? SecondaryExecID { get; set; }
		
		[TagDetails(Tag = 378, Type = TagType.Int, Offset = 22)]
		public int? ExecRestatementReason { get; set; }
		
		[TagDetails(Tag = 570, Type = TagType.Boolean, Offset = 23)]
		public bool? PreviouslyReported { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 24)]
		public int? PriceType { get; set; }
		
		[Component(Offset = 25)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 26)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 27)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 28)]
		public int? QtyType { get; set; }
		
		[Component(Offset = 29)]
		public YieldData? YieldData { get; set; }
		
		[Component(Offset = 30)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 822, Type = TagType.String, Offset = 31)]
		public string? UnderlyingTradingSessionID { get; set; }
		
		[TagDetails(Tag = 823, Type = TagType.String, Offset = 32)]
		public string? UnderlyingTradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 32, Type = TagType.Float, Offset = 33)]
		public double? LastQty { get; set; }
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 34)]
		public double? LastPx { get; set; }
		
		[TagDetails(Tag = 669, Type = TagType.Float, Offset = 35)]
		public double? LastParPx { get; set; }
		
		[TagDetails(Tag = 194, Type = TagType.Float, Offset = 36)]
		public double? LastSpotRate { get; set; }
		
		[TagDetails(Tag = 195, Type = TagType.Float, Offset = 37)]
		public double? LastForwardPoints { get; set; }
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 38)]
		public string? LastMkt { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 39)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 40)]
		public DateTime? ClearingBusinessDate { get; set; }
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 41)]
		public double? AvgPx { get; set; }
		
		[Component(Offset = 42)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[TagDetails(Tag = 819, Type = TagType.Int, Offset = 43)]
		public int? AvgPxIndicator { get; set; }
		
		[Component(Offset = 44)]
		public PositionAmountData? PositionAmountData { get; set; }
		
		[TagDetails(Tag = 442, Type = TagType.String, Offset = 45)]
		public string? MultiLegReportingType { get; set; }
		
		[TagDetails(Tag = 824, Type = TagType.String, Offset = 46)]
		public string? TradeLegRefID { get; set; }
		
		[Component(Offset = 47)]
		public TrdInstrmtLegGrp? TrdInstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 48)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 49)]
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 50)]
		public string? SettlType { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 51)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 52)]
		public string? MatchStatus { get; set; }
		
		[TagDetails(Tag = 574, Type = TagType.String, Offset = 53)]
		public string? MatchType { get; set; }
		
		[Component(Offset = 54)]
		public TrdCapRptSideGrp? TrdCapRptSideGrp { get; set; }
		
		[TagDetails(Tag = 797, Type = TagType.Boolean, Offset = 55)]
		public bool? CopyMsgIndicator { get; set; }
		
		[TagDetails(Tag = 852, Type = TagType.Boolean, Offset = 56)]
		public bool? PublishTrdIndicator { get; set; }
		
		[TagDetails(Tag = 853, Type = TagType.Int, Offset = 57)]
		public int? ShortSaleReason { get; set; }
		
		[Component(Offset = 58)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
