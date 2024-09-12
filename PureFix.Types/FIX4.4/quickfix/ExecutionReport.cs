using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("8")]
	public sealed class ExecutionReport : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(37, TagType.String)]
		public string? OrderID { get; set; }
		
		[TagDetails(198, TagType.String)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(526, TagType.String)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(527, TagType.String)]
		public string? SecondaryExecID { get; set; }
		
		[TagDetails(11, TagType.String)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(41, TagType.String)]
		public string? OrigClOrdID { get; set; }
		
		[TagDetails(583, TagType.String)]
		public string? ClOrdLinkID { get; set; }
		
		[TagDetails(693, TagType.String)]
		public string? QuoteRespID { get; set; }
		
		[TagDetails(790, TagType.String)]
		public string? OrdStatusReqID { get; set; }
		
		[TagDetails(584, TagType.String)]
		public string? MassStatusReqID { get; set; }
		
		[TagDetails(911, TagType.Int)]
		public int? TotNumReports { get; set; }
		
		[TagDetails(912, TagType.Boolean)]
		public bool? LastRptRequested { get; set; }
		
		[Component]
		public Parties? Parties { get; set; }
		
		[TagDetails(229, TagType.LocalDate)]
		public DateTime? TradeOriginationDate { get; set; }
		
		[Component]
		public ContraGrp? ContraGrp { get; set; }
		
		[TagDetails(66, TagType.String)]
		public string? ListID { get; set; }
		
		[TagDetails(548, TagType.String)]
		public string? CrossID { get; set; }
		
		[TagDetails(551, TagType.String)]
		public string? OrigCrossID { get; set; }
		
		[TagDetails(549, TagType.Int)]
		public int? CrossType { get; set; }
		
		[TagDetails(17, TagType.String)]
		public string? ExecID { get; set; }
		
		[TagDetails(19, TagType.String)]
		public string? ExecRefID { get; set; }
		
		[TagDetails(150, TagType.String)]
		public string? ExecType { get; set; }
		
		[TagDetails(39, TagType.String)]
		public string? OrdStatus { get; set; }
		
		[TagDetails(636, TagType.Boolean)]
		public bool? WorkingIndicator { get; set; }
		
		[TagDetails(103, TagType.Int)]
		public int? OrdRejReason { get; set; }
		
		[TagDetails(378, TagType.Int)]
		public int? ExecRestatementReason { get; set; }
		
		[TagDetails(1, TagType.String)]
		public string? Account { get; set; }
		
		[TagDetails(660, TagType.Int)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(581, TagType.Int)]
		public int? AccountType { get; set; }
		
		[TagDetails(589, TagType.String)]
		public string? DayBookingInst { get; set; }
		
		[TagDetails(590, TagType.String)]
		public string? BookingUnit { get; set; }
		
		[TagDetails(591, TagType.String)]
		public string? PreallocMethod { get; set; }
		
		[TagDetails(63, TagType.String)]
		public string? SettlType { get; set; }
		
		[TagDetails(64, TagType.LocalDate)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(544, TagType.String)]
		public string? CashMargin { get; set; }
		
		[TagDetails(635, TagType.String)]
		public string? ClearingFeeIndicator { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[Component]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(54, TagType.String)]
		public string? Side { get; set; }
		
		[Component]
		public Stipulations? Stipulations { get; set; }
		
		[TagDetails(854, TagType.Int)]
		public int? QtyType { get; set; }
		
		[Component]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(40, TagType.String)]
		public string? OrdType { get; set; }
		
		[TagDetails(423, TagType.Int)]
		public int? PriceType { get; set; }
		
		[TagDetails(44, TagType.Float)]
		public double? Price { get; set; }
		
		[TagDetails(99, TagType.Float)]
		public double? StopPx { get; set; }
		
		[Component]
		public PegInstructions? PegInstructions { get; set; }
		
		[Component]
		public DiscretionInstructions? DiscretionInstructions { get; set; }
		
		[TagDetails(839, TagType.Float)]
		public double? PeggedPrice { get; set; }
		
		[TagDetails(845, TagType.Float)]
		public double? DiscretionPrice { get; set; }
		
		[TagDetails(847, TagType.Int)]
		public int? TargetStrategy { get; set; }
		
		[TagDetails(848, TagType.String)]
		public string? TargetStrategyParameters { get; set; }
		
		[TagDetails(849, TagType.Float)]
		public double? ParticipationRate { get; set; }
		
		[TagDetails(850, TagType.Float)]
		public double? TargetStrategyPerformance { get; set; }
		
		[TagDetails(15, TagType.String)]
		public string? Currency { get; set; }
		
		[TagDetails(376, TagType.String)]
		public string? ComplianceID { get; set; }
		
		[TagDetails(377, TagType.Boolean)]
		public bool? SolicitedFlag { get; set; }
		
		[TagDetails(59, TagType.String)]
		public string? TimeInForce { get; set; }
		
		[TagDetails(168, TagType.UtcTimestamp)]
		public DateTime? EffectiveTime { get; set; }
		
		[TagDetails(432, TagType.LocalDate)]
		public DateTime? ExpireDate { get; set; }
		
		[TagDetails(126, TagType.UtcTimestamp)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(18, TagType.String)]
		public string? ExecInst { get; set; }
		
		[TagDetails(528, TagType.String)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(529, TagType.String)]
		public string? OrderRestrictions { get; set; }
		
		[TagDetails(582, TagType.Int)]
		public int? CustOrderCapacity { get; set; }
		
		[TagDetails(32, TagType.Float)]
		public double? LastQty { get; set; }
		
		[TagDetails(652, TagType.Float)]
		public double? UnderlyingLastQty { get; set; }
		
		[TagDetails(31, TagType.Float)]
		public double? LastPx { get; set; }
		
		[TagDetails(651, TagType.Float)]
		public double? UnderlyingLastPx { get; set; }
		
		[TagDetails(669, TagType.Float)]
		public double? LastParPx { get; set; }
		
		[TagDetails(194, TagType.Float)]
		public double? LastSpotRate { get; set; }
		
		[TagDetails(195, TagType.Float)]
		public double? LastForwardPoints { get; set; }
		
		[TagDetails(30, TagType.String)]
		public string? LastMkt { get; set; }
		
		[TagDetails(336, TagType.String)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(625, TagType.String)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(943, TagType.String)]
		public string? TimeBracket { get; set; }
		
		[TagDetails(29, TagType.String)]
		public string? LastCapacity { get; set; }
		
		[TagDetails(151, TagType.Float)]
		public double? LeavesQty { get; set; }
		
		[TagDetails(14, TagType.Float)]
		public double? CumQty { get; set; }
		
		[TagDetails(6, TagType.Float)]
		public double? AvgPx { get; set; }
		
		[TagDetails(424, TagType.Float)]
		public double? DayOrderQty { get; set; }
		
		[TagDetails(425, TagType.Float)]
		public double? DayCumQty { get; set; }
		
		[TagDetails(426, TagType.Float)]
		public double? DayAvgPx { get; set; }
		
		[TagDetails(427, TagType.Int)]
		public int? GTBookingInst { get; set; }
		
		[TagDetails(75, TagType.LocalDate)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(113, TagType.Boolean)]
		public bool? ReportToExch { get; set; }
		
		[Component]
		public CommissionData? CommissionData { get; set; }
		
		[Component]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component]
		public YieldData? YieldData { get; set; }
		
		[TagDetails(381, TagType.Float)]
		public double? GrossTradeAmt { get; set; }
		
		[TagDetails(157, TagType.Int)]
		public int? NumDaysInterest { get; set; }
		
		[TagDetails(230, TagType.LocalDate)]
		public DateTime? ExDate { get; set; }
		
		[TagDetails(158, TagType.Float)]
		public double? AccruedInterestRate { get; set; }
		
		[TagDetails(159, TagType.Float)]
		public double? AccruedInterestAmt { get; set; }
		
		[TagDetails(738, TagType.Float)]
		public double? InterestAtMaturity { get; set; }
		
		[TagDetails(920, TagType.Float)]
		public double? EndAccruedInterestAmt { get; set; }
		
		[TagDetails(921, TagType.Float)]
		public double? StartCash { get; set; }
		
		[TagDetails(922, TagType.Float)]
		public double? EndCash { get; set; }
		
		[TagDetails(258, TagType.Boolean)]
		public bool? TradedFlatSwitch { get; set; }
		
		[TagDetails(259, TagType.LocalDate)]
		public DateTime? BasisFeatureDate { get; set; }
		
		[TagDetails(260, TagType.Float)]
		public double? BasisFeaturePrice { get; set; }
		
		[TagDetails(238, TagType.Float)]
		public double? Concession { get; set; }
		
		[TagDetails(237, TagType.Float)]
		public double? TotalTakedown { get; set; }
		
		[TagDetails(118, TagType.Float)]
		public double? NetMoney { get; set; }
		
		[TagDetails(119, TagType.Float)]
		public double? SettlCurrAmt { get; set; }
		
		[TagDetails(120, TagType.String)]
		public string? SettlCurrency { get; set; }
		
		[TagDetails(155, TagType.Float)]
		public double? SettlCurrFxRate { get; set; }
		
		[TagDetails(156, TagType.String)]
		public string? SettlCurrFxRateCalc { get; set; }
		
		[TagDetails(21, TagType.String)]
		public string? HandlInst { get; set; }
		
		[TagDetails(110, TagType.Float)]
		public double? MinQty { get; set; }
		
		[TagDetails(111, TagType.Float)]
		public double? MaxFloor { get; set; }
		
		[TagDetails(77, TagType.String)]
		public string? PositionEffect { get; set; }
		
		[TagDetails(210, TagType.Float)]
		public double? MaxShow { get; set; }
		
		[TagDetails(775, TagType.Int)]
		public int? BookingType { get; set; }
		
		[TagDetails(58, TagType.String)]
		public string? Text { get; set; }
		
		[TagDetails(354, TagType.Length)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(355, TagType.RawData)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(193, TagType.LocalDate)]
		public DateTime? SettlDate2 { get; set; }
		
		[TagDetails(192, TagType.Float)]
		public double? OrderQty2 { get; set; }
		
		[TagDetails(641, TagType.Float)]
		public double? LastForwardPoints2 { get; set; }
		
		[TagDetails(442, TagType.String)]
		public string? MultiLegReportingType { get; set; }
		
		[TagDetails(480, TagType.String)]
		public string? CancellationRights { get; set; }
		
		[TagDetails(481, TagType.String)]
		public string? MoneyLaunderingStatus { get; set; }
		
		[TagDetails(513, TagType.String)]
		public string? RegistID { get; set; }
		
		[TagDetails(494, TagType.String)]
		public string? Designation { get; set; }
		
		[TagDetails(483, TagType.UtcTimestamp)]
		public DateTime? TransBkdTime { get; set; }
		
		[TagDetails(515, TagType.UtcTimestamp)]
		public DateTime? ExecValuationPoint { get; set; }
		
		[TagDetails(484, TagType.String)]
		public string? ExecPriceType { get; set; }
		
		[TagDetails(485, TagType.Float)]
		public double? ExecPriceAdjustment { get; set; }
		
		[TagDetails(638, TagType.Int)]
		public int? PriorityIndicator { get; set; }
		
		[TagDetails(639, TagType.Float)]
		public double? PriceImprovement { get; set; }
		
		[TagDetails(851, TagType.Int)]
		public int? LastLiquidityInd { get; set; }
		
		[Component]
		public ContAmtGrp? ContAmtGrp { get; set; }
		
		[Component]
		public InstrmtLegExecGrp? InstrmtLegExecGrp { get; set; }
		
		[TagDetails(797, TagType.Boolean)]
		public bool? CopyMsgIndicator { get; set; }
		
		[Component]
		public MiscFeesGrp? MiscFeesGrp { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
