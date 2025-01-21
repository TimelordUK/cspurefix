using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("ExecutionReport", FixVersion.FIX50SP2)]
	public sealed partial class ExecutionReport : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public ApplicationSequenceControlComponent? ApplicationSequenceControl {get; set;}
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 2, Required = true)]
		public string? OrderID {get; set;}
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 3, Required = false)]
		public string? SecondaryOrderID {get; set;}
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 4, Required = false)]
		public string? SecondaryClOrdID {get; set;}
		
		[TagDetails(Tag = 527, Type = TagType.String, Offset = 5, Required = false)]
		public string? SecondaryExecID {get; set;}
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 6, Required = false)]
		public string? ClOrdID {get; set;}
		
		[TagDetails(Tag = 41, Type = TagType.String, Offset = 7, Required = false)]
		public string? OrigClOrdID {get; set;}
		
		[TagDetails(Tag = 583, Type = TagType.String, Offset = 8, Required = false)]
		public string? ClOrdLinkID {get; set;}
		
		[TagDetails(Tag = 693, Type = TagType.String, Offset = 9, Required = false)]
		public string? QuoteRespID {get; set;}
		
		[TagDetails(Tag = 790, Type = TagType.String, Offset = 10, Required = false)]
		public string? OrdStatusReqID {get; set;}
		
		[TagDetails(Tag = 584, Type = TagType.String, Offset = 11, Required = false)]
		public string? MassStatusReqID {get; set;}
		
		[TagDetails(Tag = 961, Type = TagType.String, Offset = 12, Required = false)]
		public string? HostCrossID {get; set;}
		
		[TagDetails(Tag = 911, Type = TagType.Int, Offset = 13, Required = false)]
		public int? TotNumReports {get; set;}
		
		[TagDetails(Tag = 912, Type = TagType.Boolean, Offset = 14, Required = false)]
		public bool? LastRptRequested {get; set;}
		
		[Group(NoOfTag = 1012, Offset = 15, Required = false)]
		public ExecutionReportParties[]? Parties {get; set;}
		
		[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 16, Required = false)]
		public DateOnly? TradeOriginationDate {get; set;}
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 17, Required = false)]
		public string? ListID {get; set;}
		
		[TagDetails(Tag = 548, Type = TagType.String, Offset = 18, Required = false)]
		public string? CrossID {get; set;}
		
		[TagDetails(Tag = 551, Type = TagType.String, Offset = 19, Required = false)]
		public string? OrigCrossID {get; set;}
		
		[TagDetails(Tag = 549, Type = TagType.Int, Offset = 20, Required = false)]
		public int? CrossType {get; set;}
		
		[TagDetails(Tag = 880, Type = TagType.String, Offset = 21, Required = false)]
		public string? TrdMatchID {get; set;}
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 22, Required = true)]
		public string? ExecID {get; set;}
		
		[TagDetails(Tag = 19, Type = TagType.String, Offset = 23, Required = false)]
		public string? ExecRefID {get; set;}
		
		[TagDetails(Tag = 150, Type = TagType.String, Offset = 24, Required = true)]
		public string? ExecType {get; set;}
		
		[TagDetails(Tag = 39, Type = TagType.String, Offset = 25, Required = true)]
		public string? OrdStatus {get; set;}
		
		[TagDetails(Tag = 636, Type = TagType.Boolean, Offset = 26, Required = false)]
		public bool? WorkingIndicator {get; set;}
		
		[TagDetails(Tag = 103, Type = TagType.Int, Offset = 27, Required = false)]
		public int? OrdRejReason {get; set;}
		
		[TagDetails(Tag = 378, Type = TagType.Int, Offset = 28, Required = false)]
		public int? ExecRestatementReason {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 29, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 30, Required = false)]
		public int? AcctIDSource {get; set;}
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 31, Required = false)]
		public int? AccountType {get; set;}
		
		[TagDetails(Tag = 589, Type = TagType.String, Offset = 32, Required = false)]
		public string? DayBookingInst {get; set;}
		
		[TagDetails(Tag = 590, Type = TagType.String, Offset = 33, Required = false)]
		public string? BookingUnit {get; set;}
		
		[TagDetails(Tag = 591, Type = TagType.String, Offset = 34, Required = false)]
		public string? PreallocMethod {get; set;}
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 35, Required = false)]
		public string? AllocID {get; set;}
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 36, Required = false)]
		public string? SettlType {get; set;}
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 37, Required = false)]
		public DateOnly? SettlDate {get; set;}
		
		[TagDetails(Tag = 574, Type = TagType.String, Offset = 38, Required = false)]
		public string? MatchType {get; set;}
		
		[TagDetails(Tag = 1115, Type = TagType.String, Offset = 39, Required = false)]
		public string? OrderCategory {get; set;}
		
		[TagDetails(Tag = 544, Type = TagType.String, Offset = 40, Required = false)]
		public string? CashMargin {get; set;}
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 41, Required = false)]
		public string? ClearingFeeIndicator {get; set;}
		
		[Component(Offset = 42, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 43, Required = false)]
		public FinancingDetailsComponent? FinancingDetails {get; set;}
		
		[Component(Offset = 44, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 45, Required = true)]
		public string? Side {get; set;}
		
		[Group(NoOfTag = 1019, Offset = 46, Required = false)]
		public ExecutionReportStipulations[]? Stipulations {get; set;}
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 47, Required = false)]
		public int? QtyType {get; set;}
		
		[Component(Offset = 48, Required = false)]
		public OrderQtyDataComponent? OrderQtyData {get; set;}
		
		[TagDetails(Tag = 1093, Type = TagType.String, Offset = 49, Required = false)]
		public string? LotType {get; set;}
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 50, Required = false)]
		public string? OrdType {get; set;}
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 51, Required = false)]
		public int? PriceType {get; set;}
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 52, Required = false)]
		public double? Price {get; set;}
		
		[TagDetails(Tag = 1092, Type = TagType.String, Offset = 53, Required = false)]
		public string? PriceProtectionScope {get; set;}
		
		[TagDetails(Tag = 99, Type = TagType.Float, Offset = 54, Required = false)]
		public double? StopPx {get; set;}
		
		[Component(Offset = 55, Required = false)]
		public TriggeringInstructionComponent? TriggeringInstruction {get; set;}
		
		[Component(Offset = 56, Required = false)]
		public PegInstructionsComponent? PegInstructions {get; set;}
		
		[Component(Offset = 57, Required = false)]
		public DiscretionInstructionsComponent? DiscretionInstructions {get; set;}
		
		[TagDetails(Tag = 839, Type = TagType.Float, Offset = 58, Required = false)]
		public double? PeggedPrice {get; set;}
		
		[TagDetails(Tag = 1095, Type = TagType.Float, Offset = 59, Required = false)]
		public double? PeggedRefPrice {get; set;}
		
		[TagDetails(Tag = 845, Type = TagType.Float, Offset = 60, Required = false)]
		public double? DiscretionPrice {get; set;}
		
		[TagDetails(Tag = 847, Type = TagType.Int, Offset = 61, Required = false)]
		public int? TargetStrategy {get; set;}
		
		[TagDetails(Tag = 848, Type = TagType.String, Offset = 62, Required = false)]
		public string? TargetStrategyParameters {get; set;}
		
		[TagDetails(Tag = 849, Type = TagType.Float, Offset = 63, Required = false)]
		public double? ParticipationRate {get; set;}
		
		[TagDetails(Tag = 850, Type = TagType.Float, Offset = 64, Required = false)]
		public double? TargetStrategyPerformance {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 65, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 66, Required = false)]
		public string? ComplianceID {get; set;}
		
		[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 67, Required = false)]
		public bool? SolicitedFlag {get; set;}
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 68, Required = false)]
		public string? TimeInForce {get; set;}
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 69, Required = false)]
		public DateTime? EffectiveTime {get; set;}
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 70, Required = false)]
		public DateOnly? ExpireDate {get; set;}
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 71, Required = false)]
		public DateTime? ExpireTime {get; set;}
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 72, Required = false)]
		public string? ExecInst {get; set;}
		
		[TagDetails(Tag = 1057, Type = TagType.Boolean, Offset = 73, Required = false)]
		public bool? AggressorIndicator {get; set;}
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 74, Required = false)]
		public string? OrderCapacity {get; set;}
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 75, Required = false)]
		public string? OrderRestrictions {get; set;}
		
		[TagDetails(Tag = 1091, Type = TagType.Boolean, Offset = 76, Required = false)]
		public bool? PreTradeAnonymity {get; set;}
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 77, Required = false)]
		public int? CustOrderCapacity {get; set;}
		
		[TagDetails(Tag = 32, Type = TagType.Float, Offset = 78, Required = false)]
		public double? LastQty {get; set;}
		
		[TagDetails(Tag = 1056, Type = TagType.Float, Offset = 79, Required = false)]
		public double? CalculatedCcyLastQty {get; set;}
		
		[TagDetails(Tag = 1071, Type = TagType.Float, Offset = 80, Required = false)]
		public double? LastSwapPoints {get; set;}
		
		[TagDetails(Tag = 652, Type = TagType.Float, Offset = 81, Required = false)]
		public double? UnderlyingLastQty {get; set;}
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 82, Required = false)]
		public double? LastPx {get; set;}
		
		[TagDetails(Tag = 651, Type = TagType.Float, Offset = 83, Required = false)]
		public double? UnderlyingLastPx {get; set;}
		
		[TagDetails(Tag = 669, Type = TagType.Float, Offset = 84, Required = false)]
		public double? LastParPx {get; set;}
		
		[TagDetails(Tag = 194, Type = TagType.Float, Offset = 85, Required = false)]
		public double? LastSpotRate {get; set;}
		
		[TagDetails(Tag = 195, Type = TagType.Float, Offset = 86, Required = false)]
		public double? LastForwardPoints {get; set;}
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 87, Required = false)]
		public string? LastMkt {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 88, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 89, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		[TagDetails(Tag = 943, Type = TagType.String, Offset = 90, Required = false)]
		public string? TimeBracket {get; set;}
		
		[TagDetails(Tag = 29, Type = TagType.String, Offset = 91, Required = false)]
		public string? LastCapacity {get; set;}
		
		[TagDetails(Tag = 151, Type = TagType.Float, Offset = 92, Required = true)]
		public double? LeavesQty {get; set;}
		
		[TagDetails(Tag = 14, Type = TagType.Float, Offset = 93, Required = true)]
		public double? CumQty {get; set;}
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 94, Required = false)]
		public double? AvgPx {get; set;}
		
		[TagDetails(Tag = 424, Type = TagType.Float, Offset = 95, Required = false)]
		public double? DayOrderQty {get; set;}
		
		[TagDetails(Tag = 425, Type = TagType.Float, Offset = 96, Required = false)]
		public double? DayCumQty {get; set;}
		
		[TagDetails(Tag = 426, Type = TagType.Float, Offset = 97, Required = false)]
		public double? DayAvgPx {get; set;}
		
		[TagDetails(Tag = 1361, Type = TagType.Int, Offset = 98, Required = false)]
		public int? TotNoFills {get; set;}
		
		[TagDetails(Tag = 893, Type = TagType.Boolean, Offset = 99, Required = false)]
		public bool? LastFragment {get; set;}
		
		[TagDetails(Tag = 427, Type = TagType.Int, Offset = 100, Required = false)]
		public int? GTBookingInst {get; set;}
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 101, Required = false)]
		public DateOnly? TradeDate {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 102, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 113, Type = TagType.Boolean, Offset = 103, Required = false)]
		public bool? ReportToExch {get; set;}
		
		[Component(Offset = 104, Required = false)]
		public CommissionDataComponent? CommissionData {get; set;}
		
		[Component(Offset = 105, Required = false)]
		public SpreadOrBenchmarkCurveDataComponent? SpreadOrBenchmarkCurveData {get; set;}
		
		[Component(Offset = 106, Required = false)]
		public YieldDataComponent? YieldData {get; set;}
		
		[TagDetails(Tag = 381, Type = TagType.Float, Offset = 107, Required = false)]
		public double? GrossTradeAmt {get; set;}
		
		[TagDetails(Tag = 157, Type = TagType.Int, Offset = 108, Required = false)]
		public int? NumDaysInterest {get; set;}
		
		[TagDetails(Tag = 230, Type = TagType.LocalDate, Offset = 109, Required = false)]
		public DateOnly? ExDate {get; set;}
		
		[TagDetails(Tag = 158, Type = TagType.Float, Offset = 110, Required = false)]
		public double? AccruedInterestRate {get; set;}
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 111, Required = false)]
		public double? AccruedInterestAmt {get; set;}
		
		[TagDetails(Tag = 738, Type = TagType.Float, Offset = 112, Required = false)]
		public double? InterestAtMaturity {get; set;}
		
		[TagDetails(Tag = 920, Type = TagType.Float, Offset = 113, Required = false)]
		public double? EndAccruedInterestAmt {get; set;}
		
		[TagDetails(Tag = 921, Type = TagType.Float, Offset = 114, Required = false)]
		public double? StartCash {get; set;}
		
		[TagDetails(Tag = 922, Type = TagType.Float, Offset = 115, Required = false)]
		public double? EndCash {get; set;}
		
		[TagDetails(Tag = 258, Type = TagType.Boolean, Offset = 116, Required = false)]
		public bool? TradedFlatSwitch {get; set;}
		
		[TagDetails(Tag = 259, Type = TagType.LocalDate, Offset = 117, Required = false)]
		public DateOnly? BasisFeatureDate {get; set;}
		
		[TagDetails(Tag = 260, Type = TagType.Float, Offset = 118, Required = false)]
		public double? BasisFeaturePrice {get; set;}
		
		[TagDetails(Tag = 238, Type = TagType.Float, Offset = 119, Required = false)]
		public double? Concession {get; set;}
		
		[TagDetails(Tag = 237, Type = TagType.Float, Offset = 120, Required = false)]
		public double? TotalTakedown {get; set;}
		
		[TagDetails(Tag = 118, Type = TagType.Float, Offset = 121, Required = false)]
		public double? NetMoney {get; set;}
		
		[TagDetails(Tag = 119, Type = TagType.Float, Offset = 122, Required = false)]
		public double? SettlCurrAmt {get; set;}
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 123, Required = false)]
		public string? SettlCurrency {get; set;}
		
		[TagDetails(Tag = 155, Type = TagType.Float, Offset = 124, Required = false)]
		public double? SettlCurrFxRate {get; set;}
		
		[TagDetails(Tag = 156, Type = TagType.String, Offset = 125, Required = false)]
		public string? SettlCurrFxRateCalc {get; set;}
		
		[TagDetails(Tag = 21, Type = TagType.String, Offset = 126, Required = false)]
		public string? HandlInst {get; set;}
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 127, Required = false)]
		public double? MinQty {get; set;}
		
		[TagDetails(Tag = 1089, Type = TagType.Float, Offset = 128, Required = false)]
		public double? MatchIncrement {get; set;}
		
		[TagDetails(Tag = 1090, Type = TagType.Int, Offset = 129, Required = false)]
		public int? MaxPriceLevels {get; set;}
		
		[Component(Offset = 130, Required = false)]
		public DisplayInstructionComponent? DisplayInstruction {get; set;}
		
		[TagDetails(Tag = 111, Type = TagType.Float, Offset = 131, Required = false)]
		public double? MaxFloor {get; set;}
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 132, Required = false)]
		public string? PositionEffect {get; set;}
		
		[TagDetails(Tag = 210, Type = TagType.Float, Offset = 133, Required = false)]
		public double? MaxShow {get; set;}
		
		[TagDetails(Tag = 775, Type = TagType.Int, Offset = 134, Required = false)]
		public int? BookingType {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 135, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 136, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 137, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 138, Required = false)]
		public DateOnly? SettlDate2 {get; set;}
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 139, Required = false)]
		public double? OrderQty2 {get; set;}
		
		[TagDetails(Tag = 641, Type = TagType.Float, Offset = 140, Required = false)]
		public double? LastForwardPoints2 {get; set;}
		
		[TagDetails(Tag = 442, Type = TagType.String, Offset = 141, Required = false)]
		public string? MultiLegReportingType {get; set;}
		
		[TagDetails(Tag = 480, Type = TagType.String, Offset = 142, Required = false)]
		public string? CancellationRights {get; set;}
		
		[TagDetails(Tag = 481, Type = TagType.String, Offset = 143, Required = false)]
		public string? MoneyLaunderingStatus {get; set;}
		
		[TagDetails(Tag = 513, Type = TagType.String, Offset = 144, Required = false)]
		public string? RegistID {get; set;}
		
		[TagDetails(Tag = 494, Type = TagType.String, Offset = 145, Required = false)]
		public string? Designation {get; set;}
		
		[TagDetails(Tag = 483, Type = TagType.UtcTimestamp, Offset = 146, Required = false)]
		public DateTime? TransBkdTime {get; set;}
		
		[TagDetails(Tag = 515, Type = TagType.UtcTimestamp, Offset = 147, Required = false)]
		public DateTime? ExecValuationPoint {get; set;}
		
		[TagDetails(Tag = 484, Type = TagType.String, Offset = 148, Required = false)]
		public string? ExecPriceType {get; set;}
		
		[TagDetails(Tag = 485, Type = TagType.Float, Offset = 149, Required = false)]
		public double? ExecPriceAdjustment {get; set;}
		
		[TagDetails(Tag = 638, Type = TagType.Int, Offset = 150, Required = false)]
		public int? PriorityIndicator {get; set;}
		
		[TagDetails(Tag = 639, Type = TagType.Float, Offset = 151, Required = false)]
		public double? PriceImprovement {get; set;}
		
		[TagDetails(Tag = 851, Type = TagType.Int, Offset = 152, Required = false)]
		public int? LastLiquidityInd {get; set;}
		
		[TagDetails(Tag = 797, Type = TagType.Boolean, Offset = 153, Required = false)]
		public bool? CopyMsgIndicator {get; set;}
		
		[TagDetails(Tag = 1380, Type = TagType.Float, Offset = 154, Required = false)]
		public double? DividendYield {get; set;}
		
		[TagDetails(Tag = 1028, Type = TagType.Boolean, Offset = 155, Required = false)]
		public bool? ManualOrderIndicator {get; set;}
		
		[TagDetails(Tag = 1029, Type = TagType.Boolean, Offset = 156, Required = false)]
		public bool? CustDirectedOrder {get; set;}
		
		[TagDetails(Tag = 1030, Type = TagType.String, Offset = 157, Required = false)]
		public string? ReceivedDeptID {get; set;}
		
		[TagDetails(Tag = 1031, Type = TagType.String, Offset = 158, Required = false)]
		public string? CustOrderHandlingInst {get; set;}
		
		[TagDetails(Tag = 1032, Type = TagType.Int, Offset = 159, Required = false)]
		public int? OrderHandlingInstSource {get; set;}
		
		[Group(NoOfTag = 1020, Offset = 160, Required = false)]
		public ExecutionReportTrdRegTimestamps[]? TrdRegTimestamps {get; set;}
		
		[TagDetails(Tag = 1188, Type = TagType.Float, Offset = 161, Required = false)]
		public double? Volatility {get; set;}
		
		[TagDetails(Tag = 1189, Type = TagType.Float, Offset = 162, Required = false)]
		public double? TimeToExpiration {get; set;}
		
		[TagDetails(Tag = 1190, Type = TagType.Float, Offset = 163, Required = false)]
		public double? RiskFreeRate {get; set;}
		
		[TagDetails(Tag = 811, Type = TagType.Float, Offset = 164, Required = false)]
		public double? PriceDelta {get; set;}
		
		[Component(Offset = 165, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		[Group(NoOfTag = 1062, Offset = 166, Required = false)]
		public ExecutionReportRateSource[]? RateSource {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& OrderID is not null
				&& ExecID is not null
				&& ExecType is not null
				&& OrdStatus is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& Side is not null
				&& LeavesQty is not null
				&& CumQty is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (ApplicationSequenceControl is not null) ((IFixEncoder)ApplicationSequenceControl).Encode(writer);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (SecondaryOrderID is not null) writer.WriteString(198, SecondaryOrderID);
			if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
			if (SecondaryExecID is not null) writer.WriteString(527, SecondaryExecID);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (OrigClOrdID is not null) writer.WriteString(41, OrigClOrdID);
			if (ClOrdLinkID is not null) writer.WriteString(583, ClOrdLinkID);
			if (QuoteRespID is not null) writer.WriteString(693, QuoteRespID);
			if (OrdStatusReqID is not null) writer.WriteString(790, OrdStatusReqID);
			if (MassStatusReqID is not null) writer.WriteString(584, MassStatusReqID);
			if (HostCrossID is not null) writer.WriteString(961, HostCrossID);
			if (TotNumReports is not null) writer.WriteWholeNumber(911, TotNumReports.Value);
			if (LastRptRequested is not null) writer.WriteBoolean(912, LastRptRequested.Value);
			if (Parties is not null && Parties.Length != 0)
			{
				writer.WriteWholeNumber(1012, Parties.Length);
				for (int i = 0; i < Parties.Length; i++)
				{
					((IFixEncoder)Parties[i]).Encode(writer);
				}
			}
			if (TradeOriginationDate is not null) writer.WriteLocalDateOnly(229, TradeOriginationDate.Value);
			if (ListID is not null) writer.WriteString(66, ListID);
			if (CrossID is not null) writer.WriteString(548, CrossID);
			if (OrigCrossID is not null) writer.WriteString(551, OrigCrossID);
			if (CrossType is not null) writer.WriteWholeNumber(549, CrossType.Value);
			if (TrdMatchID is not null) writer.WriteString(880, TrdMatchID);
			if (ExecID is not null) writer.WriteString(17, ExecID);
			if (ExecRefID is not null) writer.WriteString(19, ExecRefID);
			if (ExecType is not null) writer.WriteString(150, ExecType);
			if (OrdStatus is not null) writer.WriteString(39, OrdStatus);
			if (WorkingIndicator is not null) writer.WriteBoolean(636, WorkingIndicator.Value);
			if (OrdRejReason is not null) writer.WriteWholeNumber(103, OrdRejReason.Value);
			if (ExecRestatementReason is not null) writer.WriteWholeNumber(378, ExecRestatementReason.Value);
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (DayBookingInst is not null) writer.WriteString(589, DayBookingInst);
			if (BookingUnit is not null) writer.WriteString(590, BookingUnit);
			if (PreallocMethod is not null) writer.WriteString(591, PreallocMethod);
			if (AllocID is not null) writer.WriteString(70, AllocID);
			if (SettlType is not null) writer.WriteString(63, SettlType);
			if (SettlDate is not null) writer.WriteLocalDateOnly(64, SettlDate.Value);
			if (MatchType is not null) writer.WriteString(574, MatchType);
			if (OrderCategory is not null) writer.WriteString(1115, OrderCategory);
			if (CashMargin is not null) writer.WriteString(544, CashMargin);
			if (ClearingFeeIndicator is not null) writer.WriteString(635, ClearingFeeIndicator);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (Side is not null) writer.WriteString(54, Side);
			if (Stipulations is not null && Stipulations.Length != 0)
			{
				writer.WriteWholeNumber(1019, Stipulations.Length);
				for (int i = 0; i < Stipulations.Length; i++)
				{
					((IFixEncoder)Stipulations[i]).Encode(writer);
				}
			}
			if (QtyType is not null) writer.WriteWholeNumber(854, QtyType.Value);
			if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
			if (LotType is not null) writer.WriteString(1093, LotType);
			if (OrdType is not null) writer.WriteString(40, OrdType);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (PriceProtectionScope is not null) writer.WriteString(1092, PriceProtectionScope);
			if (StopPx is not null) writer.WriteNumber(99, StopPx.Value);
			if (TriggeringInstruction is not null) ((IFixEncoder)TriggeringInstruction).Encode(writer);
			if (PegInstructions is not null) ((IFixEncoder)PegInstructions).Encode(writer);
			if (DiscretionInstructions is not null) ((IFixEncoder)DiscretionInstructions).Encode(writer);
			if (PeggedPrice is not null) writer.WriteNumber(839, PeggedPrice.Value);
			if (PeggedRefPrice is not null) writer.WriteNumber(1095, PeggedRefPrice.Value);
			if (DiscretionPrice is not null) writer.WriteNumber(845, DiscretionPrice.Value);
			if (TargetStrategy is not null) writer.WriteWholeNumber(847, TargetStrategy.Value);
			if (TargetStrategyParameters is not null) writer.WriteString(848, TargetStrategyParameters);
			if (ParticipationRate is not null) writer.WriteNumber(849, ParticipationRate.Value);
			if (TargetStrategyPerformance is not null) writer.WriteNumber(850, TargetStrategyPerformance.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (ComplianceID is not null) writer.WriteString(376, ComplianceID);
			if (SolicitedFlag is not null) writer.WriteBoolean(377, SolicitedFlag.Value);
			if (TimeInForce is not null) writer.WriteString(59, TimeInForce);
			if (EffectiveTime is not null) writer.WriteUtcTimeStamp(168, EffectiveTime.Value);
			if (ExpireDate is not null) writer.WriteLocalDateOnly(432, ExpireDate.Value);
			if (ExpireTime is not null) writer.WriteUtcTimeStamp(126, ExpireTime.Value);
			if (ExecInst is not null) writer.WriteString(18, ExecInst);
			if (AggressorIndicator is not null) writer.WriteBoolean(1057, AggressorIndicator.Value);
			if (OrderCapacity is not null) writer.WriteString(528, OrderCapacity);
			if (OrderRestrictions is not null) writer.WriteString(529, OrderRestrictions);
			if (PreTradeAnonymity is not null) writer.WriteBoolean(1091, PreTradeAnonymity.Value);
			if (CustOrderCapacity is not null) writer.WriteWholeNumber(582, CustOrderCapacity.Value);
			if (LastQty is not null) writer.WriteNumber(32, LastQty.Value);
			if (CalculatedCcyLastQty is not null) writer.WriteNumber(1056, CalculatedCcyLastQty.Value);
			if (LastSwapPoints is not null) writer.WriteNumber(1071, LastSwapPoints.Value);
			if (UnderlyingLastQty is not null) writer.WriteNumber(652, UnderlyingLastQty.Value);
			if (LastPx is not null) writer.WriteNumber(31, LastPx.Value);
			if (UnderlyingLastPx is not null) writer.WriteNumber(651, UnderlyingLastPx.Value);
			if (LastParPx is not null) writer.WriteNumber(669, LastParPx.Value);
			if (LastSpotRate is not null) writer.WriteNumber(194, LastSpotRate.Value);
			if (LastForwardPoints is not null) writer.WriteNumber(195, LastForwardPoints.Value);
			if (LastMkt is not null) writer.WriteString(30, LastMkt);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (TimeBracket is not null) writer.WriteString(943, TimeBracket);
			if (LastCapacity is not null) writer.WriteString(29, LastCapacity);
			if (LeavesQty is not null) writer.WriteNumber(151, LeavesQty.Value);
			if (CumQty is not null) writer.WriteNumber(14, CumQty.Value);
			if (AvgPx is not null) writer.WriteNumber(6, AvgPx.Value);
			if (DayOrderQty is not null) writer.WriteNumber(424, DayOrderQty.Value);
			if (DayCumQty is not null) writer.WriteNumber(425, DayCumQty.Value);
			if (DayAvgPx is not null) writer.WriteNumber(426, DayAvgPx.Value);
			if (TotNoFills is not null) writer.WriteWholeNumber(1361, TotNoFills.Value);
			if (LastFragment is not null) writer.WriteBoolean(893, LastFragment.Value);
			if (GTBookingInst is not null) writer.WriteWholeNumber(427, GTBookingInst.Value);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (ReportToExch is not null) writer.WriteBoolean(113, ReportToExch.Value);
			if (CommissionData is not null) ((IFixEncoder)CommissionData).Encode(writer);
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (YieldData is not null) ((IFixEncoder)YieldData).Encode(writer);
			if (GrossTradeAmt is not null) writer.WriteNumber(381, GrossTradeAmt.Value);
			if (NumDaysInterest is not null) writer.WriteWholeNumber(157, NumDaysInterest.Value);
			if (ExDate is not null) writer.WriteLocalDateOnly(230, ExDate.Value);
			if (AccruedInterestRate is not null) writer.WriteNumber(158, AccruedInterestRate.Value);
			if (AccruedInterestAmt is not null) writer.WriteNumber(159, AccruedInterestAmt.Value);
			if (InterestAtMaturity is not null) writer.WriteNumber(738, InterestAtMaturity.Value);
			if (EndAccruedInterestAmt is not null) writer.WriteNumber(920, EndAccruedInterestAmt.Value);
			if (StartCash is not null) writer.WriteNumber(921, StartCash.Value);
			if (EndCash is not null) writer.WriteNumber(922, EndCash.Value);
			if (TradedFlatSwitch is not null) writer.WriteBoolean(258, TradedFlatSwitch.Value);
			if (BasisFeatureDate is not null) writer.WriteLocalDateOnly(259, BasisFeatureDate.Value);
			if (BasisFeaturePrice is not null) writer.WriteNumber(260, BasisFeaturePrice.Value);
			if (Concession is not null) writer.WriteNumber(238, Concession.Value);
			if (TotalTakedown is not null) writer.WriteNumber(237, TotalTakedown.Value);
			if (NetMoney is not null) writer.WriteNumber(118, NetMoney.Value);
			if (SettlCurrAmt is not null) writer.WriteNumber(119, SettlCurrAmt.Value);
			if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
			if (SettlCurrFxRate is not null) writer.WriteNumber(155, SettlCurrFxRate.Value);
			if (SettlCurrFxRateCalc is not null) writer.WriteString(156, SettlCurrFxRateCalc);
			if (HandlInst is not null) writer.WriteString(21, HandlInst);
			if (MinQty is not null) writer.WriteNumber(110, MinQty.Value);
			if (MatchIncrement is not null) writer.WriteNumber(1089, MatchIncrement.Value);
			if (MaxPriceLevels is not null) writer.WriteWholeNumber(1090, MaxPriceLevels.Value);
			if (DisplayInstruction is not null) ((IFixEncoder)DisplayInstruction).Encode(writer);
			if (MaxFloor is not null) writer.WriteNumber(111, MaxFloor.Value);
			if (PositionEffect is not null) writer.WriteString(77, PositionEffect);
			if (MaxShow is not null) writer.WriteNumber(210, MaxShow.Value);
			if (BookingType is not null) writer.WriteWholeNumber(775, BookingType.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (SettlDate2 is not null) writer.WriteLocalDateOnly(193, SettlDate2.Value);
			if (OrderQty2 is not null) writer.WriteNumber(192, OrderQty2.Value);
			if (LastForwardPoints2 is not null) writer.WriteNumber(641, LastForwardPoints2.Value);
			if (MultiLegReportingType is not null) writer.WriteString(442, MultiLegReportingType);
			if (CancellationRights is not null) writer.WriteString(480, CancellationRights);
			if (MoneyLaunderingStatus is not null) writer.WriteString(481, MoneyLaunderingStatus);
			if (RegistID is not null) writer.WriteString(513, RegistID);
			if (Designation is not null) writer.WriteString(494, Designation);
			if (TransBkdTime is not null) writer.WriteUtcTimeStamp(483, TransBkdTime.Value);
			if (ExecValuationPoint is not null) writer.WriteUtcTimeStamp(515, ExecValuationPoint.Value);
			if (ExecPriceType is not null) writer.WriteString(484, ExecPriceType);
			if (ExecPriceAdjustment is not null) writer.WriteNumber(485, ExecPriceAdjustment.Value);
			if (PriorityIndicator is not null) writer.WriteWholeNumber(638, PriorityIndicator.Value);
			if (PriceImprovement is not null) writer.WriteNumber(639, PriceImprovement.Value);
			if (LastLiquidityInd is not null) writer.WriteWholeNumber(851, LastLiquidityInd.Value);
			if (CopyMsgIndicator is not null) writer.WriteBoolean(797, CopyMsgIndicator.Value);
			if (DividendYield is not null) writer.WriteNumber(1380, DividendYield.Value);
			if (ManualOrderIndicator is not null) writer.WriteBoolean(1028, ManualOrderIndicator.Value);
			if (CustDirectedOrder is not null) writer.WriteBoolean(1029, CustDirectedOrder.Value);
			if (ReceivedDeptID is not null) writer.WriteString(1030, ReceivedDeptID);
			if (CustOrderHandlingInst is not null) writer.WriteString(1031, CustOrderHandlingInst);
			if (OrderHandlingInstSource is not null) writer.WriteWholeNumber(1032, OrderHandlingInstSource.Value);
			if (TrdRegTimestamps is not null && TrdRegTimestamps.Length != 0)
			{
				writer.WriteWholeNumber(1020, TrdRegTimestamps.Length);
				for (int i = 0; i < TrdRegTimestamps.Length; i++)
				{
					((IFixEncoder)TrdRegTimestamps[i]).Encode(writer);
				}
			}
			if (Volatility is not null) writer.WriteNumber(1188, Volatility.Value);
			if (TimeToExpiration is not null) writer.WriteNumber(1189, TimeToExpiration.Value);
			if (RiskFreeRate is not null) writer.WriteNumber(1190, RiskFreeRate.Value);
			if (PriceDelta is not null) writer.WriteNumber(811, PriceDelta.Value);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
			if (RateSource is not null && RateSource.Length != 0)
			{
				writer.WriteWholeNumber(1062, RateSource.Length);
				for (int i = 0; i < RateSource.Length; i++)
				{
					((IFixEncoder)RateSource[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is IMessageView viewStandardHeader)
			{
				StandardHeader = new();
				((IFixParser)StandardHeader).Parse(viewStandardHeader);
			}
			if (view.GetView("ApplicationSequenceControl") is IMessageView viewApplicationSequenceControl)
			{
				ApplicationSequenceControl = new();
				((IFixParser)ApplicationSequenceControl).Parse(viewApplicationSequenceControl);
			}
			OrderID = view.GetString(37);
			SecondaryOrderID = view.GetString(198);
			SecondaryClOrdID = view.GetString(526);
			SecondaryExecID = view.GetString(527);
			ClOrdID = view.GetString(11);
			OrigClOrdID = view.GetString(41);
			ClOrdLinkID = view.GetString(583);
			QuoteRespID = view.GetString(693);
			OrdStatusReqID = view.GetString(790);
			MassStatusReqID = view.GetString(584);
			HostCrossID = view.GetString(961);
			TotNumReports = view.GetInt32(911);
			LastRptRequested = view.GetBool(912);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				var count = viewParties.GroupCount();
				Parties = new ExecutionReportParties[count];
				for (int i = 0; i < count; i++)
				{
					Parties[i] = new();
					((IFixParser)Parties[i]).Parse(viewParties.GetGroupInstance(i));
				}
			}
			TradeOriginationDate = view.GetDateOnly(229);
			ListID = view.GetString(66);
			CrossID = view.GetString(548);
			OrigCrossID = view.GetString(551);
			CrossType = view.GetInt32(549);
			TrdMatchID = view.GetString(880);
			ExecID = view.GetString(17);
			ExecRefID = view.GetString(19);
			ExecType = view.GetString(150);
			OrdStatus = view.GetString(39);
			WorkingIndicator = view.GetBool(636);
			OrdRejReason = view.GetInt32(103);
			ExecRestatementReason = view.GetInt32(378);
			Account = view.GetString(1);
			AcctIDSource = view.GetInt32(660);
			AccountType = view.GetInt32(581);
			DayBookingInst = view.GetString(589);
			BookingUnit = view.GetString(590);
			PreallocMethod = view.GetString(591);
			AllocID = view.GetString(70);
			SettlType = view.GetString(63);
			SettlDate = view.GetDateOnly(64);
			MatchType = view.GetString(574);
			OrderCategory = view.GetString(1115);
			CashMargin = view.GetString(544);
			ClearingFeeIndicator = view.GetString(635);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			if (view.GetView("FinancingDetails") is IMessageView viewFinancingDetails)
			{
				FinancingDetails = new();
				((IFixParser)FinancingDetails).Parse(viewFinancingDetails);
			}
			if (view.GetView("UndInstrmtGrp") is IMessageView viewUndInstrmtGrp)
			{
				UndInstrmtGrp = new();
				((IFixParser)UndInstrmtGrp).Parse(viewUndInstrmtGrp);
			}
			Side = view.GetString(54);
			if (view.GetView("Stipulations") is IMessageView viewStipulations)
			{
				var count = viewStipulations.GroupCount();
				Stipulations = new ExecutionReportStipulations[count];
				for (int i = 0; i < count; i++)
				{
					Stipulations[i] = new();
					((IFixParser)Stipulations[i]).Parse(viewStipulations.GetGroupInstance(i));
				}
			}
			QtyType = view.GetInt32(854);
			if (view.GetView("OrderQtyData") is IMessageView viewOrderQtyData)
			{
				OrderQtyData = new();
				((IFixParser)OrderQtyData).Parse(viewOrderQtyData);
			}
			LotType = view.GetString(1093);
			OrdType = view.GetString(40);
			PriceType = view.GetInt32(423);
			Price = view.GetDouble(44);
			PriceProtectionScope = view.GetString(1092);
			StopPx = view.GetDouble(99);
			if (view.GetView("TriggeringInstruction") is IMessageView viewTriggeringInstruction)
			{
				TriggeringInstruction = new();
				((IFixParser)TriggeringInstruction).Parse(viewTriggeringInstruction);
			}
			if (view.GetView("PegInstructions") is IMessageView viewPegInstructions)
			{
				PegInstructions = new();
				((IFixParser)PegInstructions).Parse(viewPegInstructions);
			}
			if (view.GetView("DiscretionInstructions") is IMessageView viewDiscretionInstructions)
			{
				DiscretionInstructions = new();
				((IFixParser)DiscretionInstructions).Parse(viewDiscretionInstructions);
			}
			PeggedPrice = view.GetDouble(839);
			PeggedRefPrice = view.GetDouble(1095);
			DiscretionPrice = view.GetDouble(845);
			TargetStrategy = view.GetInt32(847);
			TargetStrategyParameters = view.GetString(848);
			ParticipationRate = view.GetDouble(849);
			TargetStrategyPerformance = view.GetDouble(850);
			Currency = view.GetString(15);
			ComplianceID = view.GetString(376);
			SolicitedFlag = view.GetBool(377);
			TimeInForce = view.GetString(59);
			EffectiveTime = view.GetDateTime(168);
			ExpireDate = view.GetDateOnly(432);
			ExpireTime = view.GetDateTime(126);
			ExecInst = view.GetString(18);
			AggressorIndicator = view.GetBool(1057);
			OrderCapacity = view.GetString(528);
			OrderRestrictions = view.GetString(529);
			PreTradeAnonymity = view.GetBool(1091);
			CustOrderCapacity = view.GetInt32(582);
			LastQty = view.GetDouble(32);
			CalculatedCcyLastQty = view.GetDouble(1056);
			LastSwapPoints = view.GetDouble(1071);
			UnderlyingLastQty = view.GetDouble(652);
			LastPx = view.GetDouble(31);
			UnderlyingLastPx = view.GetDouble(651);
			LastParPx = view.GetDouble(669);
			LastSpotRate = view.GetDouble(194);
			LastForwardPoints = view.GetDouble(195);
			LastMkt = view.GetString(30);
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
			TimeBracket = view.GetString(943);
			LastCapacity = view.GetString(29);
			LeavesQty = view.GetDouble(151);
			CumQty = view.GetDouble(14);
			AvgPx = view.GetDouble(6);
			DayOrderQty = view.GetDouble(424);
			DayCumQty = view.GetDouble(425);
			DayAvgPx = view.GetDouble(426);
			TotNoFills = view.GetInt32(1361);
			LastFragment = view.GetBool(893);
			GTBookingInst = view.GetInt32(427);
			TradeDate = view.GetDateOnly(75);
			TransactTime = view.GetDateTime(60);
			ReportToExch = view.GetBool(113);
			if (view.GetView("CommissionData") is IMessageView viewCommissionData)
			{
				CommissionData = new();
				((IFixParser)CommissionData).Parse(viewCommissionData);
			}
			if (view.GetView("SpreadOrBenchmarkCurveData") is IMessageView viewSpreadOrBenchmarkCurveData)
			{
				SpreadOrBenchmarkCurveData = new();
				((IFixParser)SpreadOrBenchmarkCurveData).Parse(viewSpreadOrBenchmarkCurveData);
			}
			if (view.GetView("YieldData") is IMessageView viewYieldData)
			{
				YieldData = new();
				((IFixParser)YieldData).Parse(viewYieldData);
			}
			GrossTradeAmt = view.GetDouble(381);
			NumDaysInterest = view.GetInt32(157);
			ExDate = view.GetDateOnly(230);
			AccruedInterestRate = view.GetDouble(158);
			AccruedInterestAmt = view.GetDouble(159);
			InterestAtMaturity = view.GetDouble(738);
			EndAccruedInterestAmt = view.GetDouble(920);
			StartCash = view.GetDouble(921);
			EndCash = view.GetDouble(922);
			TradedFlatSwitch = view.GetBool(258);
			BasisFeatureDate = view.GetDateOnly(259);
			BasisFeaturePrice = view.GetDouble(260);
			Concession = view.GetDouble(238);
			TotalTakedown = view.GetDouble(237);
			NetMoney = view.GetDouble(118);
			SettlCurrAmt = view.GetDouble(119);
			SettlCurrency = view.GetString(120);
			SettlCurrFxRate = view.GetDouble(155);
			SettlCurrFxRateCalc = view.GetString(156);
			HandlInst = view.GetString(21);
			MinQty = view.GetDouble(110);
			MatchIncrement = view.GetDouble(1089);
			MaxPriceLevels = view.GetInt32(1090);
			if (view.GetView("DisplayInstruction") is IMessageView viewDisplayInstruction)
			{
				DisplayInstruction = new();
				((IFixParser)DisplayInstruction).Parse(viewDisplayInstruction);
			}
			MaxFloor = view.GetDouble(111);
			PositionEffect = view.GetString(77);
			MaxShow = view.GetDouble(210);
			BookingType = view.GetInt32(775);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			SettlDate2 = view.GetDateOnly(193);
			OrderQty2 = view.GetDouble(192);
			LastForwardPoints2 = view.GetDouble(641);
			MultiLegReportingType = view.GetString(442);
			CancellationRights = view.GetString(480);
			MoneyLaunderingStatus = view.GetString(481);
			RegistID = view.GetString(513);
			Designation = view.GetString(494);
			TransBkdTime = view.GetDateTime(483);
			ExecValuationPoint = view.GetDateTime(515);
			ExecPriceType = view.GetString(484);
			ExecPriceAdjustment = view.GetDouble(485);
			PriorityIndicator = view.GetInt32(638);
			PriceImprovement = view.GetDouble(639);
			LastLiquidityInd = view.GetInt32(851);
			CopyMsgIndicator = view.GetBool(797);
			DividendYield = view.GetDouble(1380);
			ManualOrderIndicator = view.GetBool(1028);
			CustDirectedOrder = view.GetBool(1029);
			ReceivedDeptID = view.GetString(1030);
			CustOrderHandlingInst = view.GetString(1031);
			OrderHandlingInstSource = view.GetInt32(1032);
			if (view.GetView("TrdRegTimestamps") is IMessageView viewTrdRegTimestamps)
			{
				var count = viewTrdRegTimestamps.GroupCount();
				TrdRegTimestamps = new ExecutionReportTrdRegTimestamps[count];
				for (int i = 0; i < count; i++)
				{
					TrdRegTimestamps[i] = new();
					((IFixParser)TrdRegTimestamps[i]).Parse(viewTrdRegTimestamps.GetGroupInstance(i));
				}
			}
			Volatility = view.GetDouble(1188);
			TimeToExpiration = view.GetDouble(1189);
			RiskFreeRate = view.GetDouble(1190);
			PriceDelta = view.GetDouble(811);
			if (view.GetView("StandardTrailer") is IMessageView viewStandardTrailer)
			{
				StandardTrailer = new();
				((IFixParser)StandardTrailer).Parse(viewStandardTrailer);
			}
			if (view.GetView("RateSource") is IMessageView viewRateSource)
			{
				var count = viewRateSource.GroupCount();
				RateSource = new ExecutionReportRateSource[count];
				for (int i = 0; i < count; i++)
				{
					RateSource[i] = new();
					((IFixParser)RateSource[i]).Parse(viewRateSource.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "StandardHeader":
					value = StandardHeader;
					break;
				case "ApplicationSequenceControl":
					value = ApplicationSequenceControl;
					break;
				case "OrderID":
					value = OrderID;
					break;
				case "SecondaryOrderID":
					value = SecondaryOrderID;
					break;
				case "SecondaryClOrdID":
					value = SecondaryClOrdID;
					break;
				case "SecondaryExecID":
					value = SecondaryExecID;
					break;
				case "ClOrdID":
					value = ClOrdID;
					break;
				case "OrigClOrdID":
					value = OrigClOrdID;
					break;
				case "ClOrdLinkID":
					value = ClOrdLinkID;
					break;
				case "QuoteRespID":
					value = QuoteRespID;
					break;
				case "OrdStatusReqID":
					value = OrdStatusReqID;
					break;
				case "MassStatusReqID":
					value = MassStatusReqID;
					break;
				case "HostCrossID":
					value = HostCrossID;
					break;
				case "TotNumReports":
					value = TotNumReports;
					break;
				case "LastRptRequested":
					value = LastRptRequested;
					break;
				case "Parties":
					value = Parties;
					break;
				case "TradeOriginationDate":
					value = TradeOriginationDate;
					break;
				case "ListID":
					value = ListID;
					break;
				case "CrossID":
					value = CrossID;
					break;
				case "OrigCrossID":
					value = OrigCrossID;
					break;
				case "CrossType":
					value = CrossType;
					break;
				case "TrdMatchID":
					value = TrdMatchID;
					break;
				case "ExecID":
					value = ExecID;
					break;
				case "ExecRefID":
					value = ExecRefID;
					break;
				case "ExecType":
					value = ExecType;
					break;
				case "OrdStatus":
					value = OrdStatus;
					break;
				case "WorkingIndicator":
					value = WorkingIndicator;
					break;
				case "OrdRejReason":
					value = OrdRejReason;
					break;
				case "ExecRestatementReason":
					value = ExecRestatementReason;
					break;
				case "Account":
					value = Account;
					break;
				case "AcctIDSource":
					value = AcctIDSource;
					break;
				case "AccountType":
					value = AccountType;
					break;
				case "DayBookingInst":
					value = DayBookingInst;
					break;
				case "BookingUnit":
					value = BookingUnit;
					break;
				case "PreallocMethod":
					value = PreallocMethod;
					break;
				case "AllocID":
					value = AllocID;
					break;
				case "SettlType":
					value = SettlType;
					break;
				case "SettlDate":
					value = SettlDate;
					break;
				case "MatchType":
					value = MatchType;
					break;
				case "OrderCategory":
					value = OrderCategory;
					break;
				case "CashMargin":
					value = CashMargin;
					break;
				case "ClearingFeeIndicator":
					value = ClearingFeeIndicator;
					break;
				case "Instrument":
					value = Instrument;
					break;
				case "FinancingDetails":
					value = FinancingDetails;
					break;
				case "UndInstrmtGrp":
					value = UndInstrmtGrp;
					break;
				case "Side":
					value = Side;
					break;
				case "Stipulations":
					value = Stipulations;
					break;
				case "QtyType":
					value = QtyType;
					break;
				case "OrderQtyData":
					value = OrderQtyData;
					break;
				case "LotType":
					value = LotType;
					break;
				case "OrdType":
					value = OrdType;
					break;
				case "PriceType":
					value = PriceType;
					break;
				case "Price":
					value = Price;
					break;
				case "PriceProtectionScope":
					value = PriceProtectionScope;
					break;
				case "StopPx":
					value = StopPx;
					break;
				case "TriggeringInstruction":
					value = TriggeringInstruction;
					break;
				case "PegInstructions":
					value = PegInstructions;
					break;
				case "DiscretionInstructions":
					value = DiscretionInstructions;
					break;
				case "PeggedPrice":
					value = PeggedPrice;
					break;
				case "PeggedRefPrice":
					value = PeggedRefPrice;
					break;
				case "DiscretionPrice":
					value = DiscretionPrice;
					break;
				case "TargetStrategy":
					value = TargetStrategy;
					break;
				case "TargetStrategyParameters":
					value = TargetStrategyParameters;
					break;
				case "ParticipationRate":
					value = ParticipationRate;
					break;
				case "TargetStrategyPerformance":
					value = TargetStrategyPerformance;
					break;
				case "Currency":
					value = Currency;
					break;
				case "ComplianceID":
					value = ComplianceID;
					break;
				case "SolicitedFlag":
					value = SolicitedFlag;
					break;
				case "TimeInForce":
					value = TimeInForce;
					break;
				case "EffectiveTime":
					value = EffectiveTime;
					break;
				case "ExpireDate":
					value = ExpireDate;
					break;
				case "ExpireTime":
					value = ExpireTime;
					break;
				case "ExecInst":
					value = ExecInst;
					break;
				case "AggressorIndicator":
					value = AggressorIndicator;
					break;
				case "OrderCapacity":
					value = OrderCapacity;
					break;
				case "OrderRestrictions":
					value = OrderRestrictions;
					break;
				case "PreTradeAnonymity":
					value = PreTradeAnonymity;
					break;
				case "CustOrderCapacity":
					value = CustOrderCapacity;
					break;
				case "LastQty":
					value = LastQty;
					break;
				case "CalculatedCcyLastQty":
					value = CalculatedCcyLastQty;
					break;
				case "LastSwapPoints":
					value = LastSwapPoints;
					break;
				case "UnderlyingLastQty":
					value = UnderlyingLastQty;
					break;
				case "LastPx":
					value = LastPx;
					break;
				case "UnderlyingLastPx":
					value = UnderlyingLastPx;
					break;
				case "LastParPx":
					value = LastParPx;
					break;
				case "LastSpotRate":
					value = LastSpotRate;
					break;
				case "LastForwardPoints":
					value = LastForwardPoints;
					break;
				case "LastMkt":
					value = LastMkt;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "TradingSessionSubID":
					value = TradingSessionSubID;
					break;
				case "TimeBracket":
					value = TimeBracket;
					break;
				case "LastCapacity":
					value = LastCapacity;
					break;
				case "LeavesQty":
					value = LeavesQty;
					break;
				case "CumQty":
					value = CumQty;
					break;
				case "AvgPx":
					value = AvgPx;
					break;
				case "DayOrderQty":
					value = DayOrderQty;
					break;
				case "DayCumQty":
					value = DayCumQty;
					break;
				case "DayAvgPx":
					value = DayAvgPx;
					break;
				case "TotNoFills":
					value = TotNoFills;
					break;
				case "LastFragment":
					value = LastFragment;
					break;
				case "GTBookingInst":
					value = GTBookingInst;
					break;
				case "TradeDate":
					value = TradeDate;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "ReportToExch":
					value = ReportToExch;
					break;
				case "CommissionData":
					value = CommissionData;
					break;
				case "SpreadOrBenchmarkCurveData":
					value = SpreadOrBenchmarkCurveData;
					break;
				case "YieldData":
					value = YieldData;
					break;
				case "GrossTradeAmt":
					value = GrossTradeAmt;
					break;
				case "NumDaysInterest":
					value = NumDaysInterest;
					break;
				case "ExDate":
					value = ExDate;
					break;
				case "AccruedInterestRate":
					value = AccruedInterestRate;
					break;
				case "AccruedInterestAmt":
					value = AccruedInterestAmt;
					break;
				case "InterestAtMaturity":
					value = InterestAtMaturity;
					break;
				case "EndAccruedInterestAmt":
					value = EndAccruedInterestAmt;
					break;
				case "StartCash":
					value = StartCash;
					break;
				case "EndCash":
					value = EndCash;
					break;
				case "TradedFlatSwitch":
					value = TradedFlatSwitch;
					break;
				case "BasisFeatureDate":
					value = BasisFeatureDate;
					break;
				case "BasisFeaturePrice":
					value = BasisFeaturePrice;
					break;
				case "Concession":
					value = Concession;
					break;
				case "TotalTakedown":
					value = TotalTakedown;
					break;
				case "NetMoney":
					value = NetMoney;
					break;
				case "SettlCurrAmt":
					value = SettlCurrAmt;
					break;
				case "SettlCurrency":
					value = SettlCurrency;
					break;
				case "SettlCurrFxRate":
					value = SettlCurrFxRate;
					break;
				case "SettlCurrFxRateCalc":
					value = SettlCurrFxRateCalc;
					break;
				case "HandlInst":
					value = HandlInst;
					break;
				case "MinQty":
					value = MinQty;
					break;
				case "MatchIncrement":
					value = MatchIncrement;
					break;
				case "MaxPriceLevels":
					value = MaxPriceLevels;
					break;
				case "DisplayInstruction":
					value = DisplayInstruction;
					break;
				case "MaxFloor":
					value = MaxFloor;
					break;
				case "PositionEffect":
					value = PositionEffect;
					break;
				case "MaxShow":
					value = MaxShow;
					break;
				case "BookingType":
					value = BookingType;
					break;
				case "Text":
					value = Text;
					break;
				case "EncodedTextLen":
					value = EncodedTextLen;
					break;
				case "EncodedText":
					value = EncodedText;
					break;
				case "SettlDate2":
					value = SettlDate2;
					break;
				case "OrderQty2":
					value = OrderQty2;
					break;
				case "LastForwardPoints2":
					value = LastForwardPoints2;
					break;
				case "MultiLegReportingType":
					value = MultiLegReportingType;
					break;
				case "CancellationRights":
					value = CancellationRights;
					break;
				case "MoneyLaunderingStatus":
					value = MoneyLaunderingStatus;
					break;
				case "RegistID":
					value = RegistID;
					break;
				case "Designation":
					value = Designation;
					break;
				case "TransBkdTime":
					value = TransBkdTime;
					break;
				case "ExecValuationPoint":
					value = ExecValuationPoint;
					break;
				case "ExecPriceType":
					value = ExecPriceType;
					break;
				case "ExecPriceAdjustment":
					value = ExecPriceAdjustment;
					break;
				case "PriorityIndicator":
					value = PriorityIndicator;
					break;
				case "PriceImprovement":
					value = PriceImprovement;
					break;
				case "LastLiquidityInd":
					value = LastLiquidityInd;
					break;
				case "CopyMsgIndicator":
					value = CopyMsgIndicator;
					break;
				case "DividendYield":
					value = DividendYield;
					break;
				case "ManualOrderIndicator":
					value = ManualOrderIndicator;
					break;
				case "CustDirectedOrder":
					value = CustDirectedOrder;
					break;
				case "ReceivedDeptID":
					value = ReceivedDeptID;
					break;
				case "CustOrderHandlingInst":
					value = CustOrderHandlingInst;
					break;
				case "OrderHandlingInstSource":
					value = OrderHandlingInstSource;
					break;
				case "TrdRegTimestamps":
					value = TrdRegTimestamps;
					break;
				case "Volatility":
					value = Volatility;
					break;
				case "TimeToExpiration":
					value = TimeToExpiration;
					break;
				case "RiskFreeRate":
					value = RiskFreeRate;
					break;
				case "PriceDelta":
					value = PriceDelta;
					break;
				case "StandardTrailer":
					value = StandardTrailer;
					break;
				case "RateSource":
					value = RateSource;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			((IFixReset?)ApplicationSequenceControl)?.Reset();
			OrderID = null;
			SecondaryOrderID = null;
			SecondaryClOrdID = null;
			SecondaryExecID = null;
			ClOrdID = null;
			OrigClOrdID = null;
			ClOrdLinkID = null;
			QuoteRespID = null;
			OrdStatusReqID = null;
			MassStatusReqID = null;
			HostCrossID = null;
			TotNumReports = null;
			LastRptRequested = null;
			Parties = null;
			TradeOriginationDate = null;
			ListID = null;
			CrossID = null;
			OrigCrossID = null;
			CrossType = null;
			TrdMatchID = null;
			ExecID = null;
			ExecRefID = null;
			ExecType = null;
			OrdStatus = null;
			WorkingIndicator = null;
			OrdRejReason = null;
			ExecRestatementReason = null;
			Account = null;
			AcctIDSource = null;
			AccountType = null;
			DayBookingInst = null;
			BookingUnit = null;
			PreallocMethod = null;
			AllocID = null;
			SettlType = null;
			SettlDate = null;
			MatchType = null;
			OrderCategory = null;
			CashMargin = null;
			ClearingFeeIndicator = null;
			((IFixReset?)Instrument)?.Reset();
			((IFixReset?)FinancingDetails)?.Reset();
			((IFixReset?)UndInstrmtGrp)?.Reset();
			Side = null;
			Stipulations = null;
			QtyType = null;
			((IFixReset?)OrderQtyData)?.Reset();
			LotType = null;
			OrdType = null;
			PriceType = null;
			Price = null;
			PriceProtectionScope = null;
			StopPx = null;
			((IFixReset?)TriggeringInstruction)?.Reset();
			((IFixReset?)PegInstructions)?.Reset();
			((IFixReset?)DiscretionInstructions)?.Reset();
			PeggedPrice = null;
			PeggedRefPrice = null;
			DiscretionPrice = null;
			TargetStrategy = null;
			TargetStrategyParameters = null;
			ParticipationRate = null;
			TargetStrategyPerformance = null;
			Currency = null;
			ComplianceID = null;
			SolicitedFlag = null;
			TimeInForce = null;
			EffectiveTime = null;
			ExpireDate = null;
			ExpireTime = null;
			ExecInst = null;
			AggressorIndicator = null;
			OrderCapacity = null;
			OrderRestrictions = null;
			PreTradeAnonymity = null;
			CustOrderCapacity = null;
			LastQty = null;
			CalculatedCcyLastQty = null;
			LastSwapPoints = null;
			UnderlyingLastQty = null;
			LastPx = null;
			UnderlyingLastPx = null;
			LastParPx = null;
			LastSpotRate = null;
			LastForwardPoints = null;
			LastMkt = null;
			TradingSessionID = null;
			TradingSessionSubID = null;
			TimeBracket = null;
			LastCapacity = null;
			LeavesQty = null;
			CumQty = null;
			AvgPx = null;
			DayOrderQty = null;
			DayCumQty = null;
			DayAvgPx = null;
			TotNoFills = null;
			LastFragment = null;
			GTBookingInst = null;
			TradeDate = null;
			TransactTime = null;
			ReportToExch = null;
			((IFixReset?)CommissionData)?.Reset();
			((IFixReset?)SpreadOrBenchmarkCurveData)?.Reset();
			((IFixReset?)YieldData)?.Reset();
			GrossTradeAmt = null;
			NumDaysInterest = null;
			ExDate = null;
			AccruedInterestRate = null;
			AccruedInterestAmt = null;
			InterestAtMaturity = null;
			EndAccruedInterestAmt = null;
			StartCash = null;
			EndCash = null;
			TradedFlatSwitch = null;
			BasisFeatureDate = null;
			BasisFeaturePrice = null;
			Concession = null;
			TotalTakedown = null;
			NetMoney = null;
			SettlCurrAmt = null;
			SettlCurrency = null;
			SettlCurrFxRate = null;
			SettlCurrFxRateCalc = null;
			HandlInst = null;
			MinQty = null;
			MatchIncrement = null;
			MaxPriceLevels = null;
			((IFixReset?)DisplayInstruction)?.Reset();
			MaxFloor = null;
			PositionEffect = null;
			MaxShow = null;
			BookingType = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			SettlDate2 = null;
			OrderQty2 = null;
			LastForwardPoints2 = null;
			MultiLegReportingType = null;
			CancellationRights = null;
			MoneyLaunderingStatus = null;
			RegistID = null;
			Designation = null;
			TransBkdTime = null;
			ExecValuationPoint = null;
			ExecPriceType = null;
			ExecPriceAdjustment = null;
			PriorityIndicator = null;
			PriceImprovement = null;
			LastLiquidityInd = null;
			CopyMsgIndicator = null;
			DividendYield = null;
			ManualOrderIndicator = null;
			CustDirectedOrder = null;
			ReceivedDeptID = null;
			CustOrderHandlingInst = null;
			OrderHandlingInstSource = null;
			TrdRegTimestamps = null;
			Volatility = null;
			TimeToExpiration = null;
			RiskFreeRate = null;
			PriceDelta = null;
			((IFixReset?)StandardTrailer)?.Reset();
			RateSource = null;
		}
	}
}
