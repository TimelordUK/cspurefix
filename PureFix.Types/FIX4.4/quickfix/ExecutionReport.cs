using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("8", FixVersion.FIX44)]
	public sealed class ExecutionReport : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 2)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 3)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 527, Type = TagType.String, Offset = 4)]
		public string? SecondaryExecID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 5)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 41, Type = TagType.String, Offset = 6)]
		public string? OrigClOrdID { get; set; }
		
		[TagDetails(Tag = 583, Type = TagType.String, Offset = 7)]
		public string? ClOrdLinkID { get; set; }
		
		[TagDetails(Tag = 693, Type = TagType.String, Offset = 8)]
		public string? QuoteRespID { get; set; }
		
		[TagDetails(Tag = 790, Type = TagType.String, Offset = 9)]
		public string? OrdStatusReqID { get; set; }
		
		[TagDetails(Tag = 584, Type = TagType.String, Offset = 10)]
		public string? MassStatusReqID { get; set; }
		
		[TagDetails(Tag = 911, Type = TagType.Int, Offset = 11)]
		public int? TotNumReports { get; set; }
		
		[TagDetails(Tag = 912, Type = TagType.Boolean, Offset = 12)]
		public bool? LastRptRequested { get; set; }
		
		[Component(Offset = 13)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 14)]
		public DateTime? TradeOriginationDate { get; set; }
		
		[Component(Offset = 15)]
		public ContraGrp? ContraGrp { get; set; }
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 16)]
		public string? ListID { get; set; }
		
		[TagDetails(Tag = 548, Type = TagType.String, Offset = 17)]
		public string? CrossID { get; set; }
		
		[TagDetails(Tag = 551, Type = TagType.String, Offset = 18)]
		public string? OrigCrossID { get; set; }
		
		[TagDetails(Tag = 549, Type = TagType.Int, Offset = 19)]
		public int? CrossType { get; set; }
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 20)]
		public string? ExecID { get; set; }
		
		[TagDetails(Tag = 19, Type = TagType.String, Offset = 21)]
		public string? ExecRefID { get; set; }
		
		[TagDetails(Tag = 150, Type = TagType.String, Offset = 22)]
		public string? ExecType { get; set; }
		
		[TagDetails(Tag = 39, Type = TagType.String, Offset = 23)]
		public string? OrdStatus { get; set; }
		
		[TagDetails(Tag = 636, Type = TagType.Boolean, Offset = 24)]
		public bool? WorkingIndicator { get; set; }
		
		[TagDetails(Tag = 103, Type = TagType.Int, Offset = 25)]
		public int? OrdRejReason { get; set; }
		
		[TagDetails(Tag = 378, Type = TagType.Int, Offset = 26)]
		public int? ExecRestatementReason { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 27)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 28)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 29)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 589, Type = TagType.String, Offset = 30)]
		public string? DayBookingInst { get; set; }
		
		[TagDetails(Tag = 590, Type = TagType.String, Offset = 31)]
		public string? BookingUnit { get; set; }
		
		[TagDetails(Tag = 591, Type = TagType.String, Offset = 32)]
		public string? PreallocMethod { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 33)]
		public string? SettlType { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 34)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(Tag = 544, Type = TagType.String, Offset = 35)]
		public string? CashMargin { get; set; }
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 36)]
		public string? ClearingFeeIndicator { get; set; }
		
		[Component(Offset = 37)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 38)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 39)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 40)]
		public string? Side { get; set; }
		
		[Component(Offset = 41)]
		public Stipulations? Stipulations { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 42)]
		public int? QtyType { get; set; }
		
		[Component(Offset = 43)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 44)]
		public string? OrdType { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 45)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 46)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 99, Type = TagType.Float, Offset = 47)]
		public double? StopPx { get; set; }
		
		[Component(Offset = 48)]
		public PegInstructions? PegInstructions { get; set; }
		
		[Component(Offset = 49)]
		public DiscretionInstructions? DiscretionInstructions { get; set; }
		
		[TagDetails(Tag = 839, Type = TagType.Float, Offset = 50)]
		public double? PeggedPrice { get; set; }
		
		[TagDetails(Tag = 845, Type = TagType.Float, Offset = 51)]
		public double? DiscretionPrice { get; set; }
		
		[TagDetails(Tag = 847, Type = TagType.Int, Offset = 52)]
		public int? TargetStrategy { get; set; }
		
		[TagDetails(Tag = 848, Type = TagType.String, Offset = 53)]
		public string? TargetStrategyParameters { get; set; }
		
		[TagDetails(Tag = 849, Type = TagType.Float, Offset = 54)]
		public double? ParticipationRate { get; set; }
		
		[TagDetails(Tag = 850, Type = TagType.Float, Offset = 55)]
		public double? TargetStrategyPerformance { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 56)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 57)]
		public string? ComplianceID { get; set; }
		
		[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 58)]
		public bool? SolicitedFlag { get; set; }
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 59)]
		public string? TimeInForce { get; set; }
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 60)]
		public DateTime? EffectiveTime { get; set; }
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 61)]
		public DateTime? ExpireDate { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 62)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 63)]
		public string? ExecInst { get; set; }
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 64)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 65)]
		public string? OrderRestrictions { get; set; }
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 66)]
		public int? CustOrderCapacity { get; set; }
		
		[TagDetails(Tag = 32, Type = TagType.Float, Offset = 67)]
		public double? LastQty { get; set; }
		
		[TagDetails(Tag = 652, Type = TagType.Float, Offset = 68)]
		public double? UnderlyingLastQty { get; set; }
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 69)]
		public double? LastPx { get; set; }
		
		[TagDetails(Tag = 651, Type = TagType.Float, Offset = 70)]
		public double? UnderlyingLastPx { get; set; }
		
		[TagDetails(Tag = 669, Type = TagType.Float, Offset = 71)]
		public double? LastParPx { get; set; }
		
		[TagDetails(Tag = 194, Type = TagType.Float, Offset = 72)]
		public double? LastSpotRate { get; set; }
		
		[TagDetails(Tag = 195, Type = TagType.Float, Offset = 73)]
		public double? LastForwardPoints { get; set; }
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 74)]
		public string? LastMkt { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 75)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 76)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 943, Type = TagType.String, Offset = 77)]
		public string? TimeBracket { get; set; }
		
		[TagDetails(Tag = 29, Type = TagType.String, Offset = 78)]
		public string? LastCapacity { get; set; }
		
		[TagDetails(Tag = 151, Type = TagType.Float, Offset = 79)]
		public double? LeavesQty { get; set; }
		
		[TagDetails(Tag = 14, Type = TagType.Float, Offset = 80)]
		public double? CumQty { get; set; }
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 81)]
		public double? AvgPx { get; set; }
		
		[TagDetails(Tag = 424, Type = TagType.Float, Offset = 82)]
		public double? DayOrderQty { get; set; }
		
		[TagDetails(Tag = 425, Type = TagType.Float, Offset = 83)]
		public double? DayCumQty { get; set; }
		
		[TagDetails(Tag = 426, Type = TagType.Float, Offset = 84)]
		public double? DayAvgPx { get; set; }
		
		[TagDetails(Tag = 427, Type = TagType.Int, Offset = 85)]
		public int? GTBookingInst { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 86)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 87)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 113, Type = TagType.Boolean, Offset = 88)]
		public bool? ReportToExch { get; set; }
		
		[Component(Offset = 89)]
		public CommissionData? CommissionData { get; set; }
		
		[Component(Offset = 90)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component(Offset = 91)]
		public YieldData? YieldData { get; set; }
		
		[TagDetails(Tag = 381, Type = TagType.Float, Offset = 92)]
		public double? GrossTradeAmt { get; set; }
		
		[TagDetails(Tag = 157, Type = TagType.Int, Offset = 93)]
		public int? NumDaysInterest { get; set; }
		
		[TagDetails(Tag = 230, Type = TagType.LocalDate, Offset = 94)]
		public DateTime? ExDate { get; set; }
		
		[TagDetails(Tag = 158, Type = TagType.Float, Offset = 95)]
		public double? AccruedInterestRate { get; set; }
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 96)]
		public double? AccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 738, Type = TagType.Float, Offset = 97)]
		public double? InterestAtMaturity { get; set; }
		
		[TagDetails(Tag = 920, Type = TagType.Float, Offset = 98)]
		public double? EndAccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 921, Type = TagType.Float, Offset = 99)]
		public double? StartCash { get; set; }
		
		[TagDetails(Tag = 922, Type = TagType.Float, Offset = 100)]
		public double? EndCash { get; set; }
		
		[TagDetails(Tag = 258, Type = TagType.Boolean, Offset = 101)]
		public bool? TradedFlatSwitch { get; set; }
		
		[TagDetails(Tag = 259, Type = TagType.LocalDate, Offset = 102)]
		public DateTime? BasisFeatureDate { get; set; }
		
		[TagDetails(Tag = 260, Type = TagType.Float, Offset = 103)]
		public double? BasisFeaturePrice { get; set; }
		
		[TagDetails(Tag = 238, Type = TagType.Float, Offset = 104)]
		public double? Concession { get; set; }
		
		[TagDetails(Tag = 237, Type = TagType.Float, Offset = 105)]
		public double? TotalTakedown { get; set; }
		
		[TagDetails(Tag = 118, Type = TagType.Float, Offset = 106)]
		public double? NetMoney { get; set; }
		
		[TagDetails(Tag = 119, Type = TagType.Float, Offset = 107)]
		public double? SettlCurrAmt { get; set; }
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 108)]
		public string? SettlCurrency { get; set; }
		
		[TagDetails(Tag = 155, Type = TagType.Float, Offset = 109)]
		public double? SettlCurrFxRate { get; set; }
		
		[TagDetails(Tag = 156, Type = TagType.String, Offset = 110)]
		public string? SettlCurrFxRateCalc { get; set; }
		
		[TagDetails(Tag = 21, Type = TagType.String, Offset = 111)]
		public string? HandlInst { get; set; }
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 112)]
		public double? MinQty { get; set; }
		
		[TagDetails(Tag = 111, Type = TagType.Float, Offset = 113)]
		public double? MaxFloor { get; set; }
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 114)]
		public string? PositionEffect { get; set; }
		
		[TagDetails(Tag = 210, Type = TagType.Float, Offset = 115)]
		public double? MaxShow { get; set; }
		
		[TagDetails(Tag = 775, Type = TagType.Int, Offset = 116)]
		public int? BookingType { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 117)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 118)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 119)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 120)]
		public DateTime? SettlDate2 { get; set; }
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 121)]
		public double? OrderQty2 { get; set; }
		
		[TagDetails(Tag = 641, Type = TagType.Float, Offset = 122)]
		public double? LastForwardPoints2 { get; set; }
		
		[TagDetails(Tag = 442, Type = TagType.String, Offset = 123)]
		public string? MultiLegReportingType { get; set; }
		
		[TagDetails(Tag = 480, Type = TagType.String, Offset = 124)]
		public string? CancellationRights { get; set; }
		
		[TagDetails(Tag = 481, Type = TagType.String, Offset = 125)]
		public string? MoneyLaunderingStatus { get; set; }
		
		[TagDetails(Tag = 513, Type = TagType.String, Offset = 126)]
		public string? RegistID { get; set; }
		
		[TagDetails(Tag = 494, Type = TagType.String, Offset = 127)]
		public string? Designation { get; set; }
		
		[TagDetails(Tag = 483, Type = TagType.UtcTimestamp, Offset = 128)]
		public DateTime? TransBkdTime { get; set; }
		
		[TagDetails(Tag = 515, Type = TagType.UtcTimestamp, Offset = 129)]
		public DateTime? ExecValuationPoint { get; set; }
		
		[TagDetails(Tag = 484, Type = TagType.String, Offset = 130)]
		public string? ExecPriceType { get; set; }
		
		[TagDetails(Tag = 485, Type = TagType.Float, Offset = 131)]
		public double? ExecPriceAdjustment { get; set; }
		
		[TagDetails(Tag = 638, Type = TagType.Int, Offset = 132)]
		public int? PriorityIndicator { get; set; }
		
		[TagDetails(Tag = 639, Type = TagType.Float, Offset = 133)]
		public double? PriceImprovement { get; set; }
		
		[TagDetails(Tag = 851, Type = TagType.Int, Offset = 134)]
		public int? LastLiquidityInd { get; set; }
		
		[Component(Offset = 135)]
		public ContAmtGrp? ContAmtGrp { get; set; }
		
		[Component(Offset = 136)]
		public InstrmtLegExecGrp? InstrmtLegExecGrp { get; set; }
		
		[TagDetails(Tag = 797, Type = TagType.Boolean, Offset = 137)]
		public bool? CopyMsgIndicator { get; set; }
		
		[Component(Offset = 138)]
		public MiscFeesGrp? MiscFeesGrp { get; set; }
		
		[Component(Offset = 139)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
