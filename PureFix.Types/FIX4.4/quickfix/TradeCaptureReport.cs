using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class TradeCaptureReport : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(571, TagType.String)]
		public string? TradeReportID { get; set; }
		
		[TagDetails(487, TagType.Int)]
		public int? TradeReportTransType { get; set; }
		
		[TagDetails(856, TagType.Int)]
		public int? TradeReportType { get; set; }
		
		[TagDetails(568, TagType.String)]
		public string? TradeRequestID { get; set; }
		
		[TagDetails(828, TagType.Int)]
		public int? TrdType { get; set; }
		
		[TagDetails(829, TagType.Int)]
		public int? TrdSubType { get; set; }
		
		[TagDetails(855, TagType.Int)]
		public int? SecondaryTrdType { get; set; }
		
		[TagDetails(830, TagType.String)]
		public string? TransferReason { get; set; }
		
		[TagDetails(150, TagType.String)]
		public string? ExecType { get; set; }
		
		[TagDetails(748, TagType.Int)]
		public int? TotNumTradeReports { get; set; }
		
		[TagDetails(912, TagType.Boolean)]
		public bool? LastRptRequested { get; set; }
		
		[TagDetails(325, TagType.Boolean)]
		public bool? UnsolicitedIndicator { get; set; }
		
		[TagDetails(263, TagType.String)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(572, TagType.String)]
		public string? TradeReportRefID { get; set; }
		
		[TagDetails(881, TagType.String)]
		public string? SecondaryTradeReportRefID { get; set; }
		
		[TagDetails(818, TagType.String)]
		public string? SecondaryTradeReportID { get; set; }
		
		[TagDetails(820, TagType.String)]
		public string? TradeLinkID { get; set; }
		
		[TagDetails(880, TagType.String)]
		public string? TrdMatchID { get; set; }
		
		[TagDetails(17, TagType.String)]
		public string? ExecID { get; set; }
		
		[TagDetails(39, TagType.String)]
		public string? OrdStatus { get; set; }
		
		[TagDetails(527, TagType.String)]
		public string? SecondaryExecID { get; set; }
		
		[TagDetails(378, TagType.Int)]
		public int? ExecRestatementReason { get; set; }
		
		[TagDetails(570, TagType.Boolean)]
		public bool? PreviouslyReported { get; set; }
		
		[TagDetails(423, TagType.Int)]
		public int? PriceType { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[Component]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(854, TagType.Int)]
		public int? QtyType { get; set; }
		
		[Component]
		public YieldData? YieldData { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(822, TagType.String)]
		public string? UnderlyingTradingSessionID { get; set; }
		
		[TagDetails(823, TagType.String)]
		public string? UnderlyingTradingSessionSubID { get; set; }
		
		[TagDetails(32, TagType.Float)]
		public double? LastQty { get; set; }
		
		[TagDetails(31, TagType.Float)]
		public double? LastPx { get; set; }
		
		[TagDetails(669, TagType.Float)]
		public double? LastParPx { get; set; }
		
		[TagDetails(194, TagType.Float)]
		public double? LastSpotRate { get; set; }
		
		[TagDetails(195, TagType.Float)]
		public double? LastForwardPoints { get; set; }
		
		[TagDetails(30, TagType.String)]
		public string? LastMkt { get; set; }
		
		[TagDetails(75, TagType.LocalDate)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(715, TagType.LocalDate)]
		public DateTime? ClearingBusinessDate { get; set; }
		
		[TagDetails(6, TagType.Float)]
		public double? AvgPx { get; set; }
		
		[Component]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[TagDetails(819, TagType.Int)]
		public int? AvgPxIndicator { get; set; }
		
		[Component]
		public PositionAmountData? PositionAmountData { get; set; }
		
		[TagDetails(442, TagType.String)]
		public string? MultiLegReportingType { get; set; }
		
		[TagDetails(824, TagType.String)]
		public string? TradeLegRefID { get; set; }
		
		[Component]
		public TrdInstrmtLegGrp? TrdInstrmtLegGrp { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[Component]
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		
		[TagDetails(63, TagType.String)]
		public string? SettlType { get; set; }
		
		[TagDetails(64, TagType.LocalDate)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(573, TagType.String)]
		public string? MatchStatus { get; set; }
		
		[TagDetails(574, TagType.String)]
		public string? MatchType { get; set; }
		
		[Component]
		public TrdCapRptSideGrp? TrdCapRptSideGrp { get; set; }
		
		[TagDetails(797, TagType.Boolean)]
		public bool? CopyMsgIndicator { get; set; }
		
		[TagDetails(852, TagType.Boolean)]
		public bool? PublishTrdIndicator { get; set; }
		
		[TagDetails(853, TagType.Int)]
		public int? ShortSaleReason { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
		
	}
}
