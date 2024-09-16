using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("8", FixVersion.FIX44)]
	public sealed partial class ExecutionReport : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = true)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 3, Required = false)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 527, Type = TagType.String, Offset = 4, Required = false)]
		public string? SecondaryExecID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 5, Required = false)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 41, Type = TagType.String, Offset = 6, Required = false)]
		public string? OrigClOrdID { get; set; }
		
		[TagDetails(Tag = 583, Type = TagType.String, Offset = 7, Required = false)]
		public string? ClOrdLinkID { get; set; }
		
		[TagDetails(Tag = 693, Type = TagType.String, Offset = 8, Required = false)]
		public string? QuoteRespID { get; set; }
		
		[TagDetails(Tag = 790, Type = TagType.String, Offset = 9, Required = false)]
		public string? OrdStatusReqID { get; set; }
		
		[TagDetails(Tag = 584, Type = TagType.String, Offset = 10, Required = false)]
		public string? MassStatusReqID { get; set; }
		
		[TagDetails(Tag = 911, Type = TagType.Int, Offset = 11, Required = false)]
		public int? TotNumReports { get; set; }
		
		[TagDetails(Tag = 912, Type = TagType.Boolean, Offset = 12, Required = false)]
		public bool? LastRptRequested { get; set; }
		
		[Component(Offset = 13, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 14, Required = false)]
		public DateOnly? TradeOriginationDate { get; set; }
		
		[Component(Offset = 15, Required = false)]
		public ContraGrp? ContraGrp { get; set; }
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 16, Required = false)]
		public string? ListID { get; set; }
		
		[TagDetails(Tag = 548, Type = TagType.String, Offset = 17, Required = false)]
		public string? CrossID { get; set; }
		
		[TagDetails(Tag = 551, Type = TagType.String, Offset = 18, Required = false)]
		public string? OrigCrossID { get; set; }
		
		[TagDetails(Tag = 549, Type = TagType.Int, Offset = 19, Required = false)]
		public int? CrossType { get; set; }
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 20, Required = true)]
		public string? ExecID { get; set; }
		
		[TagDetails(Tag = 19, Type = TagType.String, Offset = 21, Required = false)]
		public string? ExecRefID { get; set; }
		
		[TagDetails(Tag = 150, Type = TagType.String, Offset = 22, Required = true)]
		public string? ExecType { get; set; }
		
		[TagDetails(Tag = 39, Type = TagType.String, Offset = 23, Required = true)]
		public string? OrdStatus { get; set; }
		
		[TagDetails(Tag = 636, Type = TagType.Boolean, Offset = 24, Required = false)]
		public bool? WorkingIndicator { get; set; }
		
		[TagDetails(Tag = 103, Type = TagType.Int, Offset = 25, Required = false)]
		public int? OrdRejReason { get; set; }
		
		[TagDetails(Tag = 378, Type = TagType.Int, Offset = 26, Required = false)]
		public int? ExecRestatementReason { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 27, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 28, Required = false)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 29, Required = false)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 589, Type = TagType.String, Offset = 30, Required = false)]
		public string? DayBookingInst { get; set; }
		
		[TagDetails(Tag = 590, Type = TagType.String, Offset = 31, Required = false)]
		public string? BookingUnit { get; set; }
		
		[TagDetails(Tag = 591, Type = TagType.String, Offset = 32, Required = false)]
		public string? PreallocMethod { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 33, Required = false)]
		public string? SettlType { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 34, Required = false)]
		public DateOnly? SettlDate { get; set; }
		
		[TagDetails(Tag = 544, Type = TagType.String, Offset = 35, Required = false)]
		public string? CashMargin { get; set; }
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 36, Required = false)]
		public string? ClearingFeeIndicator { get; set; }
		
		[Component(Offset = 37, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 38, Required = false)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 39, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 40, Required = true)]
		public string? Side { get; set; }
		
		[Component(Offset = 41, Required = false)]
		public Stipulations? Stipulations { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 42, Required = false)]
		public int? QtyType { get; set; }
		
		[Component(Offset = 43, Required = false)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 44, Required = false)]
		public string? OrdType { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 45, Required = false)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 46, Required = false)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 99, Type = TagType.Float, Offset = 47, Required = false)]
		public double? StopPx { get; set; }
		
		[Component(Offset = 48, Required = false)]
		public PegInstructions? PegInstructions { get; set; }
		
		[Component(Offset = 49, Required = false)]
		public DiscretionInstructions? DiscretionInstructions { get; set; }
		
		[TagDetails(Tag = 839, Type = TagType.Float, Offset = 50, Required = false)]
		public double? PeggedPrice { get; set; }
		
		[TagDetails(Tag = 845, Type = TagType.Float, Offset = 51, Required = false)]
		public double? DiscretionPrice { get; set; }
		
		[TagDetails(Tag = 847, Type = TagType.Int, Offset = 52, Required = false)]
		public int? TargetStrategy { get; set; }
		
		[TagDetails(Tag = 848, Type = TagType.String, Offset = 53, Required = false)]
		public string? TargetStrategyParameters { get; set; }
		
		[TagDetails(Tag = 849, Type = TagType.Float, Offset = 54, Required = false)]
		public double? ParticipationRate { get; set; }
		
		[TagDetails(Tag = 850, Type = TagType.Float, Offset = 55, Required = false)]
		public double? TargetStrategyPerformance { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 56, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 57, Required = false)]
		public string? ComplianceID { get; set; }
		
		[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 58, Required = false)]
		public bool? SolicitedFlag { get; set; }
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 59, Required = false)]
		public string? TimeInForce { get; set; }
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 60, Required = false)]
		public DateTime? EffectiveTime { get; set; }
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 61, Required = false)]
		public DateOnly? ExpireDate { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 62, Required = false)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 63, Required = false)]
		public string? ExecInst { get; set; }
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 64, Required = false)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 65, Required = false)]
		public string? OrderRestrictions { get; set; }
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 66, Required = false)]
		public int? CustOrderCapacity { get; set; }
		
		[TagDetails(Tag = 32, Type = TagType.Float, Offset = 67, Required = false)]
		public double? LastQty { get; set; }
		
		[TagDetails(Tag = 652, Type = TagType.Float, Offset = 68, Required = false)]
		public double? UnderlyingLastQty { get; set; }
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 69, Required = false)]
		public double? LastPx { get; set; }
		
		[TagDetails(Tag = 651, Type = TagType.Float, Offset = 70, Required = false)]
		public double? UnderlyingLastPx { get; set; }
		
		[TagDetails(Tag = 669, Type = TagType.Float, Offset = 71, Required = false)]
		public double? LastParPx { get; set; }
		
		[TagDetails(Tag = 194, Type = TagType.Float, Offset = 72, Required = false)]
		public double? LastSpotRate { get; set; }
		
		[TagDetails(Tag = 195, Type = TagType.Float, Offset = 73, Required = false)]
		public double? LastForwardPoints { get; set; }
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 74, Required = false)]
		public string? LastMkt { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 75, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 76, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 943, Type = TagType.String, Offset = 77, Required = false)]
		public string? TimeBracket { get; set; }
		
		[TagDetails(Tag = 29, Type = TagType.String, Offset = 78, Required = false)]
		public string? LastCapacity { get; set; }
		
		[TagDetails(Tag = 151, Type = TagType.Float, Offset = 79, Required = true)]
		public double? LeavesQty { get; set; }
		
		[TagDetails(Tag = 14, Type = TagType.Float, Offset = 80, Required = true)]
		public double? CumQty { get; set; }
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 81, Required = true)]
		public double? AvgPx { get; set; }
		
		[TagDetails(Tag = 424, Type = TagType.Float, Offset = 82, Required = false)]
		public double? DayOrderQty { get; set; }
		
		[TagDetails(Tag = 425, Type = TagType.Float, Offset = 83, Required = false)]
		public double? DayCumQty { get; set; }
		
		[TagDetails(Tag = 426, Type = TagType.Float, Offset = 84, Required = false)]
		public double? DayAvgPx { get; set; }
		
		[TagDetails(Tag = 427, Type = TagType.Int, Offset = 85, Required = false)]
		public int? GTBookingInst { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 86, Required = false)]
		public DateOnly? TradeDate { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 87, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 113, Type = TagType.Boolean, Offset = 88, Required = false)]
		public bool? ReportToExch { get; set; }
		
		[Component(Offset = 89, Required = false)]
		public CommissionData? CommissionData { get; set; }
		
		[Component(Offset = 90, Required = false)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component(Offset = 91, Required = false)]
		public YieldData? YieldData { get; set; }
		
		[TagDetails(Tag = 381, Type = TagType.Float, Offset = 92, Required = false)]
		public double? GrossTradeAmt { get; set; }
		
		[TagDetails(Tag = 157, Type = TagType.Int, Offset = 93, Required = false)]
		public int? NumDaysInterest { get; set; }
		
		[TagDetails(Tag = 230, Type = TagType.LocalDate, Offset = 94, Required = false)]
		public DateOnly? ExDate { get; set; }
		
		[TagDetails(Tag = 158, Type = TagType.Float, Offset = 95, Required = false)]
		public double? AccruedInterestRate { get; set; }
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 96, Required = false)]
		public double? AccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 738, Type = TagType.Float, Offset = 97, Required = false)]
		public double? InterestAtMaturity { get; set; }
		
		[TagDetails(Tag = 920, Type = TagType.Float, Offset = 98, Required = false)]
		public double? EndAccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 921, Type = TagType.Float, Offset = 99, Required = false)]
		public double? StartCash { get; set; }
		
		[TagDetails(Tag = 922, Type = TagType.Float, Offset = 100, Required = false)]
		public double? EndCash { get; set; }
		
		[TagDetails(Tag = 258, Type = TagType.Boolean, Offset = 101, Required = false)]
		public bool? TradedFlatSwitch { get; set; }
		
		[TagDetails(Tag = 259, Type = TagType.LocalDate, Offset = 102, Required = false)]
		public DateOnly? BasisFeatureDate { get; set; }
		
		[TagDetails(Tag = 260, Type = TagType.Float, Offset = 103, Required = false)]
		public double? BasisFeaturePrice { get; set; }
		
		[TagDetails(Tag = 238, Type = TagType.Float, Offset = 104, Required = false)]
		public double? Concession { get; set; }
		
		[TagDetails(Tag = 237, Type = TagType.Float, Offset = 105, Required = false)]
		public double? TotalTakedown { get; set; }
		
		[TagDetails(Tag = 118, Type = TagType.Float, Offset = 106, Required = false)]
		public double? NetMoney { get; set; }
		
		[TagDetails(Tag = 119, Type = TagType.Float, Offset = 107, Required = false)]
		public double? SettlCurrAmt { get; set; }
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 108, Required = false)]
		public string? SettlCurrency { get; set; }
		
		[TagDetails(Tag = 155, Type = TagType.Float, Offset = 109, Required = false)]
		public double? SettlCurrFxRate { get; set; }
		
		[TagDetails(Tag = 156, Type = TagType.String, Offset = 110, Required = false)]
		public string? SettlCurrFxRateCalc { get; set; }
		
		[TagDetails(Tag = 21, Type = TagType.String, Offset = 111, Required = false)]
		public string? HandlInst { get; set; }
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 112, Required = false)]
		public double? MinQty { get; set; }
		
		[TagDetails(Tag = 111, Type = TagType.Float, Offset = 113, Required = false)]
		public double? MaxFloor { get; set; }
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 114, Required = false)]
		public string? PositionEffect { get; set; }
		
		[TagDetails(Tag = 210, Type = TagType.Float, Offset = 115, Required = false)]
		public double? MaxShow { get; set; }
		
		[TagDetails(Tag = 775, Type = TagType.Int, Offset = 116, Required = false)]
		public int? BookingType { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 117, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 118, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 119, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 120, Required = false)]
		public DateOnly? SettlDate2 { get; set; }
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 121, Required = false)]
		public double? OrderQty2 { get; set; }
		
		[TagDetails(Tag = 641, Type = TagType.Float, Offset = 122, Required = false)]
		public double? LastForwardPoints2 { get; set; }
		
		[TagDetails(Tag = 442, Type = TagType.String, Offset = 123, Required = false)]
		public string? MultiLegReportingType { get; set; }
		
		[TagDetails(Tag = 480, Type = TagType.String, Offset = 124, Required = false)]
		public string? CancellationRights { get; set; }
		
		[TagDetails(Tag = 481, Type = TagType.String, Offset = 125, Required = false)]
		public string? MoneyLaunderingStatus { get; set; }
		
		[TagDetails(Tag = 513, Type = TagType.String, Offset = 126, Required = false)]
		public string? RegistID { get; set; }
		
		[TagDetails(Tag = 494, Type = TagType.String, Offset = 127, Required = false)]
		public string? Designation { get; set; }
		
		[TagDetails(Tag = 483, Type = TagType.UtcTimestamp, Offset = 128, Required = false)]
		public DateTime? TransBkdTime { get; set; }
		
		[TagDetails(Tag = 515, Type = TagType.UtcTimestamp, Offset = 129, Required = false)]
		public DateTime? ExecValuationPoint { get; set; }
		
		[TagDetails(Tag = 484, Type = TagType.String, Offset = 130, Required = false)]
		public string? ExecPriceType { get; set; }
		
		[TagDetails(Tag = 485, Type = TagType.Float, Offset = 131, Required = false)]
		public double? ExecPriceAdjustment { get; set; }
		
		[TagDetails(Tag = 638, Type = TagType.Int, Offset = 132, Required = false)]
		public int? PriorityIndicator { get; set; }
		
		[TagDetails(Tag = 639, Type = TagType.Float, Offset = 133, Required = false)]
		public double? PriceImprovement { get; set; }
		
		[TagDetails(Tag = 851, Type = TagType.Int, Offset = 134, Required = false)]
		public int? LastLiquidityInd { get; set; }
		
		[Component(Offset = 135, Required = false)]
		public ContAmtGrp? ContAmtGrp { get; set; }
		
		[Component(Offset = 136, Required = false)]
		public InstrmtLegExecGrp? InstrmtLegExecGrp { get; set; }
		
		[TagDetails(Tag = 797, Type = TagType.Boolean, Offset = 137, Required = false)]
		public bool? CopyMsgIndicator { get; set; }
		
		[Component(Offset = 138, Required = false)]
		public MiscFeesGrp? MiscFeesGrp { get; set; }
		
		[Component(Offset = 139, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
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
				&& AvgPx is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
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
			if (TotNumReports is not null) writer.WriteWholeNumber(911, TotNumReports.Value);
			if (LastRptRequested is not null) writer.WriteBoolean(912, LastRptRequested.Value);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (TradeOriginationDate is not null) writer.WriteLocalDateOnly(229, TradeOriginationDate.Value);
			if (ContraGrp is not null) ((IFixEncoder)ContraGrp).Encode(writer);
			if (ListID is not null) writer.WriteString(66, ListID);
			if (CrossID is not null) writer.WriteString(548, CrossID);
			if (OrigCrossID is not null) writer.WriteString(551, OrigCrossID);
			if (CrossType is not null) writer.WriteWholeNumber(549, CrossType.Value);
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
			if (SettlType is not null) writer.WriteString(63, SettlType);
			if (SettlDate is not null) writer.WriteLocalDateOnly(64, SettlDate.Value);
			if (CashMargin is not null) writer.WriteString(544, CashMargin);
			if (ClearingFeeIndicator is not null) writer.WriteString(635, ClearingFeeIndicator);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (Side is not null) writer.WriteString(54, Side);
			if (Stipulations is not null) ((IFixEncoder)Stipulations).Encode(writer);
			if (QtyType is not null) writer.WriteWholeNumber(854, QtyType.Value);
			if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
			if (OrdType is not null) writer.WriteString(40, OrdType);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (StopPx is not null) writer.WriteNumber(99, StopPx.Value);
			if (PegInstructions is not null) ((IFixEncoder)PegInstructions).Encode(writer);
			if (DiscretionInstructions is not null) ((IFixEncoder)DiscretionInstructions).Encode(writer);
			if (PeggedPrice is not null) writer.WriteNumber(839, PeggedPrice.Value);
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
			if (OrderCapacity is not null) writer.WriteString(528, OrderCapacity);
			if (OrderRestrictions is not null) writer.WriteString(529, OrderRestrictions);
			if (CustOrderCapacity is not null) writer.WriteWholeNumber(582, CustOrderCapacity.Value);
			if (LastQty is not null) writer.WriteNumber(32, LastQty.Value);
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
			if (ContAmtGrp is not null) ((IFixEncoder)ContAmtGrp).Encode(writer);
			if (InstrmtLegExecGrp is not null) ((IFixEncoder)InstrmtLegExecGrp).Encode(writer);
			if (CopyMsgIndicator is not null) writer.WriteBoolean(797, CopyMsgIndicator.Value);
			if (MiscFeesGrp is not null) ((IFixEncoder)MiscFeesGrp).Encode(writer);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
