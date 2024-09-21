using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("8", FixVersion.FIX43)]
	public sealed partial class ExecutionReport : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = true)]
		public string? OrderID {get; set;}
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryOrderID {get; set;}
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 3, Required = false)]
		public string? SecondaryClOrdID {get; set;}
		
		[TagDetails(Tag = 527, Type = TagType.String, Offset = 4, Required = false)]
		public string? SecondaryExecID {get; set;}
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 5, Required = false)]
		public string? ClOrdID {get; set;}
		
		[TagDetails(Tag = 41, Type = TagType.String, Offset = 6, Required = false)]
		public string? OrigClOrdID {get; set;}
		
		[TagDetails(Tag = 583, Type = TagType.String, Offset = 7, Required = false)]
		public string? ClOrdLinkID {get; set;}
		
		[Component(Offset = 8, Required = false)]
		public PartiesComponent? Parties {get; set;}
		
		[TagDetails(Tag = 229, Type = TagType.String, Offset = 9, Required = false)]
		public string? TradeOriginationDate {get; set;}
		
		[Group(NoOfTag = 382, Offset = 10, Required = false)]
		public NoContraBrokers[]? NoContraBrokers {get; set;}
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 11, Required = false)]
		public string? ListID {get; set;}
		
		[TagDetails(Tag = 548, Type = TagType.String, Offset = 12, Required = false)]
		public string? CrossID {get; set;}
		
		[TagDetails(Tag = 551, Type = TagType.String, Offset = 13, Required = false)]
		public string? OrigCrossID {get; set;}
		
		[TagDetails(Tag = 549, Type = TagType.Int, Offset = 14, Required = false)]
		public int? CrossType {get; set;}
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 15, Required = true)]
		public string? ExecID {get; set;}
		
		[TagDetails(Tag = 19, Type = TagType.String, Offset = 16, Required = false)]
		public string? ExecRefID {get; set;}
		
		[TagDetails(Tag = 150, Type = TagType.String, Offset = 17, Required = true)]
		public string? ExecType {get; set;}
		
		[TagDetails(Tag = 39, Type = TagType.String, Offset = 18, Required = true)]
		public string? OrdStatus {get; set;}
		
		[TagDetails(Tag = 636, Type = TagType.Boolean, Offset = 19, Required = false)]
		public bool? WorkingIndicator {get; set;}
		
		[TagDetails(Tag = 103, Type = TagType.Int, Offset = 20, Required = false)]
		public int? OrdRejReason {get; set;}
		
		[TagDetails(Tag = 378, Type = TagType.Int, Offset = 21, Required = false)]
		public int? ExecRestatementReason {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 22, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 23, Required = false)]
		public int? AccountType {get; set;}
		
		[TagDetails(Tag = 589, Type = TagType.String, Offset = 24, Required = false)]
		public string? DayBookingInst {get; set;}
		
		[TagDetails(Tag = 590, Type = TagType.String, Offset = 25, Required = false)]
		public string? BookingUnit {get; set;}
		
		[TagDetails(Tag = 591, Type = TagType.String, Offset = 26, Required = false)]
		public string? PreallocMethod {get; set;}
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 27, Required = false)]
		public string? SettlmntTyp {get; set;}
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 28, Required = false)]
		public DateOnly? FutSettDate {get; set;}
		
		[TagDetails(Tag = 544, Type = TagType.String, Offset = 29, Required = false)]
		public string? CashMargin {get; set;}
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 30, Required = false)]
		public string? ClearingFeeIndicator {get; set;}
		
		[Component(Offset = 31, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 32, Required = true)]
		public string? Side {get; set;}
		
		[Component(Offset = 33, Required = false)]
		public StipulationsComponent? Stipulations {get; set;}
		
		[TagDetails(Tag = 465, Type = TagType.Int, Offset = 34, Required = false)]
		public int? QuantityType {get; set;}
		
		[Component(Offset = 35, Required = true)]
		public OrderQtyDataComponent? OrderQtyData {get; set;}
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 36, Required = false)]
		public string? OrdType {get; set;}
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 37, Required = false)]
		public int? PriceType {get; set;}
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 38, Required = false)]
		public double? Price {get; set;}
		
		[TagDetails(Tag = 99, Type = TagType.Float, Offset = 39, Required = false)]
		public double? StopPx {get; set;}
		
		[TagDetails(Tag = 211, Type = TagType.Float, Offset = 40, Required = false)]
		public double? PegDifference {get; set;}
		
		[TagDetails(Tag = 388, Type = TagType.String, Offset = 41, Required = false)]
		public string? DiscretionInst {get; set;}
		
		[TagDetails(Tag = 389, Type = TagType.Float, Offset = 42, Required = false)]
		public double? DiscretionOffset {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 43, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 44, Required = false)]
		public string? ComplianceID {get; set;}
		
		[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 45, Required = false)]
		public bool? SolicitedFlag {get; set;}
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 46, Required = false)]
		public string? TimeInForce {get; set;}
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 47, Required = false)]
		public DateTime? EffectiveTime {get; set;}
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 48, Required = false)]
		public DateOnly? ExpireDate {get; set;}
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 49, Required = false)]
		public DateTime? ExpireTime {get; set;}
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 50, Required = false)]
		public string? ExecInst {get; set;}
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 51, Required = false)]
		public string? OrderCapacity {get; set;}
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 52, Required = false)]
		public string? OrderRestrictions {get; set;}
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 53, Required = false)]
		public int? CustOrderCapacity {get; set;}
		
		[TagDetails(Tag = 47, Type = TagType.String, Offset = 54, Required = false)]
		public string? Rule80A {get; set;}
		
		[TagDetails(Tag = 32, Type = TagType.Float, Offset = 55, Required = false)]
		public double? LastQty {get; set;}
		
		[TagDetails(Tag = 652, Type = TagType.Float, Offset = 56, Required = false)]
		public double? UnderlyingLastQty {get; set;}
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 57, Required = false)]
		public double? LastPx {get; set;}
		
		[TagDetails(Tag = 651, Type = TagType.Float, Offset = 58, Required = false)]
		public double? UnderlyingLastPx {get; set;}
		
		[TagDetails(Tag = 194, Type = TagType.Float, Offset = 59, Required = false)]
		public double? LastSpotRate {get; set;}
		
		[TagDetails(Tag = 195, Type = TagType.Float, Offset = 60, Required = false)]
		public double? LastForwardPoints {get; set;}
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 61, Required = false)]
		public string? LastMkt {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 62, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 63, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		[TagDetails(Tag = 29, Type = TagType.String, Offset = 64, Required = false)]
		public string? LastCapacity {get; set;}
		
		[TagDetails(Tag = 151, Type = TagType.Float, Offset = 65, Required = true)]
		public double? LeavesQty {get; set;}
		
		[TagDetails(Tag = 14, Type = TagType.Float, Offset = 66, Required = true)]
		public double? CumQty {get; set;}
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 67, Required = true)]
		public double? AvgPx {get; set;}
		
		[TagDetails(Tag = 424, Type = TagType.Float, Offset = 68, Required = false)]
		public double? DayOrderQty {get; set;}
		
		[TagDetails(Tag = 425, Type = TagType.Float, Offset = 69, Required = false)]
		public double? DayCumQty {get; set;}
		
		[TagDetails(Tag = 426, Type = TagType.Float, Offset = 70, Required = false)]
		public double? DayAvgPx {get; set;}
		
		[TagDetails(Tag = 427, Type = TagType.Int, Offset = 71, Required = false)]
		public int? GTBookingInst {get; set;}
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 72, Required = false)]
		public DateOnly? TradeDate {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 73, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 113, Type = TagType.Boolean, Offset = 74, Required = false)]
		public bool? ReportToExch {get; set;}
		
		[Component(Offset = 75, Required = false)]
		public CommissionDataComponent? CommissionData {get; set;}
		
		[Component(Offset = 76, Required = false)]
		public SpreadOrBenchmarkCurveDataComponent? SpreadOrBenchmarkCurveData {get; set;}
		
		[Component(Offset = 77, Required = false)]
		public YieldDataComponent? YieldData {get; set;}
		
		[TagDetails(Tag = 381, Type = TagType.Float, Offset = 78, Required = false)]
		public double? GrossTradeAmt {get; set;}
		
		[TagDetails(Tag = 157, Type = TagType.Int, Offset = 79, Required = false)]
		public int? NumDaysInterest {get; set;}
		
		[TagDetails(Tag = 230, Type = TagType.String, Offset = 80, Required = false)]
		public string? ExDate {get; set;}
		
		[TagDetails(Tag = 158, Type = TagType.Float, Offset = 81, Required = false)]
		public double? AccruedInterestRate {get; set;}
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 82, Required = false)]
		public double? AccruedInterestAmt {get; set;}
		
		[TagDetails(Tag = 258, Type = TagType.Boolean, Offset = 83, Required = false)]
		public bool? TradedFlatSwitch {get; set;}
		
		[TagDetails(Tag = 259, Type = TagType.String, Offset = 84, Required = false)]
		public string? BasisFeatureDate {get; set;}
		
		[TagDetails(Tag = 260, Type = TagType.Float, Offset = 85, Required = false)]
		public double? BasisFeaturePrice {get; set;}
		
		[TagDetails(Tag = 238, Type = TagType.Float, Offset = 86, Required = false)]
		public double? Concession {get; set;}
		
		[TagDetails(Tag = 237, Type = TagType.Float, Offset = 87, Required = false)]
		public double? TotalTakedown {get; set;}
		
		[TagDetails(Tag = 118, Type = TagType.Float, Offset = 88, Required = false)]
		public double? NetMoney {get; set;}
		
		[TagDetails(Tag = 119, Type = TagType.Float, Offset = 89, Required = false)]
		public double? SettlCurrAmt {get; set;}
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 90, Required = false)]
		public string? SettlCurrency {get; set;}
		
		[TagDetails(Tag = 155, Type = TagType.Float, Offset = 91, Required = false)]
		public double? SettlCurrFxRate {get; set;}
		
		[TagDetails(Tag = 156, Type = TagType.String, Offset = 92, Required = false)]
		public string? SettlCurrFxRateCalc {get; set;}
		
		[TagDetails(Tag = 21, Type = TagType.String, Offset = 93, Required = false)]
		public string? HandlInst {get; set;}
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 94, Required = false)]
		public double? MinQty {get; set;}
		
		[TagDetails(Tag = 111, Type = TagType.Float, Offset = 95, Required = false)]
		public double? MaxFloor {get; set;}
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 96, Required = false)]
		public string? PositionEffect {get; set;}
		
		[TagDetails(Tag = 210, Type = TagType.Float, Offset = 97, Required = false)]
		public double? MaxShow {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 98, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 99, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 100, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 101, Required = false)]
		public DateOnly? FutSettDate2 {get; set;}
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 102, Required = false)]
		public double? OrderQty2 {get; set;}
		
		[TagDetails(Tag = 641, Type = TagType.Float, Offset = 103, Required = false)]
		public double? LastForwardPoints2 {get; set;}
		
		[TagDetails(Tag = 442, Type = TagType.String, Offset = 104, Required = false)]
		public string? MultiLegReportingType {get; set;}
		
		[TagDetails(Tag = 480, Type = TagType.String, Offset = 105, Required = false)]
		public string? CancellationRights {get; set;}
		
		[TagDetails(Tag = 481, Type = TagType.String, Offset = 106, Required = false)]
		public string? MoneyLaunderingStatus {get; set;}
		
		[TagDetails(Tag = 513, Type = TagType.String, Offset = 107, Required = false)]
		public string? RegistID {get; set;}
		
		[TagDetails(Tag = 494, Type = TagType.String, Offset = 108, Required = false)]
		public string? Designation {get; set;}
		
		[TagDetails(Tag = 483, Type = TagType.UtcTimestamp, Offset = 109, Required = false)]
		public DateTime? TransBkdTime {get; set;}
		
		[TagDetails(Tag = 515, Type = TagType.UtcTimestamp, Offset = 110, Required = false)]
		public DateTime? ExecValuationPoint {get; set;}
		
		[TagDetails(Tag = 484, Type = TagType.String, Offset = 111, Required = false)]
		public string? ExecPriceType {get; set;}
		
		[TagDetails(Tag = 485, Type = TagType.Float, Offset = 112, Required = false)]
		public double? ExecPriceAdjustment {get; set;}
		
		[TagDetails(Tag = 638, Type = TagType.Int, Offset = 113, Required = false)]
		public int? PriorityIndicator {get; set;}
		
		[TagDetails(Tag = 639, Type = TagType.Float, Offset = 114, Required = false)]
		public double? PriceImprovement {get; set;}
		
		[Group(NoOfTag = 518, Offset = 115, Required = false)]
		public NoContAmts[]? NoContAmts {get; set;}
		
		[Group(NoOfTag = 555, Offset = 116, Required = false)]
		public NoLegs[]? NoLegs {get; set;}
		
		[Component(Offset = 117, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
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
				&& OrderQtyData is not null && ((IFixValidator)OrderQtyData).IsValid(in config)
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
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (TradeOriginationDate is not null) writer.WriteString(229, TradeOriginationDate);
			if (NoContraBrokers is not null && NoContraBrokers.Length != 0)
			{
				writer.WriteWholeNumber(382, NoContraBrokers.Length);
				for (int i = 0; i < NoContraBrokers.Length; i++)
				{
					((IFixEncoder)NoContraBrokers[i]).Encode(writer);
				}
			}
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
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (DayBookingInst is not null) writer.WriteString(589, DayBookingInst);
			if (BookingUnit is not null) writer.WriteString(590, BookingUnit);
			if (PreallocMethod is not null) writer.WriteString(591, PreallocMethod);
			if (SettlmntTyp is not null) writer.WriteString(63, SettlmntTyp);
			if (FutSettDate is not null) writer.WriteLocalDateOnly(64, FutSettDate.Value);
			if (CashMargin is not null) writer.WriteString(544, CashMargin);
			if (ClearingFeeIndicator is not null) writer.WriteString(635, ClearingFeeIndicator);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (Side is not null) writer.WriteString(54, Side);
			if (Stipulations is not null) ((IFixEncoder)Stipulations).Encode(writer);
			if (QuantityType is not null) writer.WriteWholeNumber(465, QuantityType.Value);
			if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
			if (OrdType is not null) writer.WriteString(40, OrdType);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (StopPx is not null) writer.WriteNumber(99, StopPx.Value);
			if (PegDifference is not null) writer.WriteNumber(211, PegDifference.Value);
			if (DiscretionInst is not null) writer.WriteString(388, DiscretionInst);
			if (DiscretionOffset is not null) writer.WriteNumber(389, DiscretionOffset.Value);
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
			if (Rule80A is not null) writer.WriteString(47, Rule80A);
			if (LastQty is not null) writer.WriteNumber(32, LastQty.Value);
			if (UnderlyingLastQty is not null) writer.WriteNumber(652, UnderlyingLastQty.Value);
			if (LastPx is not null) writer.WriteNumber(31, LastPx.Value);
			if (UnderlyingLastPx is not null) writer.WriteNumber(651, UnderlyingLastPx.Value);
			if (LastSpotRate is not null) writer.WriteNumber(194, LastSpotRate.Value);
			if (LastForwardPoints is not null) writer.WriteNumber(195, LastForwardPoints.Value);
			if (LastMkt is not null) writer.WriteString(30, LastMkt);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
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
			if (ExDate is not null) writer.WriteString(230, ExDate);
			if (AccruedInterestRate is not null) writer.WriteNumber(158, AccruedInterestRate.Value);
			if (AccruedInterestAmt is not null) writer.WriteNumber(159, AccruedInterestAmt.Value);
			if (TradedFlatSwitch is not null) writer.WriteBoolean(258, TradedFlatSwitch.Value);
			if (BasisFeatureDate is not null) writer.WriteString(259, BasisFeatureDate);
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
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (FutSettDate2 is not null) writer.WriteLocalDateOnly(193, FutSettDate2.Value);
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
			if (NoContAmts is not null && NoContAmts.Length != 0)
			{
				writer.WriteWholeNumber(518, NoContAmts.Length);
				for (int i = 0; i < NoContAmts.Length; i++)
				{
					((IFixEncoder)NoContAmts[i]).Encode(writer);
				}
			}
			if (NoLegs is not null && NoLegs.Length != 0)
			{
				writer.WriteWholeNumber(555, NoLegs.Length);
				for (int i = 0; i < NoLegs.Length; i++)
				{
					((IFixEncoder)NoLegs[i]).Encode(writer);
				}
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is IMessageView viewStandardHeader)
			{
				StandardHeader = new();
				((IFixParser)StandardHeader).Parse(viewStandardHeader);
			}
			OrderID = view.GetString(37);
			SecondaryOrderID = view.GetString(198);
			SecondaryClOrdID = view.GetString(526);
			SecondaryExecID = view.GetString(527);
			ClOrdID = view.GetString(11);
			OrigClOrdID = view.GetString(41);
			ClOrdLinkID = view.GetString(583);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				Parties = new();
				((IFixParser)Parties).Parse(viewParties);
			}
			TradeOriginationDate = view.GetString(229);
			if (view.GetView("NoContraBrokers") is IMessageView viewNoContraBrokers)
			{
				var count = viewNoContraBrokers.GroupCount();
				NoContraBrokers = new NoContraBrokers[count];
				for (int i = 0; i < count; i++)
				{
					NoContraBrokers[i] = new();
					((IFixParser)NoContraBrokers[i]).Parse(viewNoContraBrokers.GetGroupInstance(i));
				}
			}
			ListID = view.GetString(66);
			CrossID = view.GetString(548);
			OrigCrossID = view.GetString(551);
			CrossType = view.GetInt32(549);
			ExecID = view.GetString(17);
			ExecRefID = view.GetString(19);
			ExecType = view.GetString(150);
			OrdStatus = view.GetString(39);
			WorkingIndicator = view.GetBool(636);
			OrdRejReason = view.GetInt32(103);
			ExecRestatementReason = view.GetInt32(378);
			Account = view.GetString(1);
			AccountType = view.GetInt32(581);
			DayBookingInst = view.GetString(589);
			BookingUnit = view.GetString(590);
			PreallocMethod = view.GetString(591);
			SettlmntTyp = view.GetString(63);
			FutSettDate = view.GetDateOnly(64);
			CashMargin = view.GetString(544);
			ClearingFeeIndicator = view.GetString(635);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			Side = view.GetString(54);
			if (view.GetView("Stipulations") is IMessageView viewStipulations)
			{
				Stipulations = new();
				((IFixParser)Stipulations).Parse(viewStipulations);
			}
			QuantityType = view.GetInt32(465);
			if (view.GetView("OrderQtyData") is IMessageView viewOrderQtyData)
			{
				OrderQtyData = new();
				((IFixParser)OrderQtyData).Parse(viewOrderQtyData);
			}
			OrdType = view.GetString(40);
			PriceType = view.GetInt32(423);
			Price = view.GetDouble(44);
			StopPx = view.GetDouble(99);
			PegDifference = view.GetDouble(211);
			DiscretionInst = view.GetString(388);
			DiscretionOffset = view.GetDouble(389);
			Currency = view.GetString(15);
			ComplianceID = view.GetString(376);
			SolicitedFlag = view.GetBool(377);
			TimeInForce = view.GetString(59);
			EffectiveTime = view.GetDateTime(168);
			ExpireDate = view.GetDateOnly(432);
			ExpireTime = view.GetDateTime(126);
			ExecInst = view.GetString(18);
			OrderCapacity = view.GetString(528);
			OrderRestrictions = view.GetString(529);
			CustOrderCapacity = view.GetInt32(582);
			Rule80A = view.GetString(47);
			LastQty = view.GetDouble(32);
			UnderlyingLastQty = view.GetDouble(652);
			LastPx = view.GetDouble(31);
			UnderlyingLastPx = view.GetDouble(651);
			LastSpotRate = view.GetDouble(194);
			LastForwardPoints = view.GetDouble(195);
			LastMkt = view.GetString(30);
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
			LastCapacity = view.GetString(29);
			LeavesQty = view.GetDouble(151);
			CumQty = view.GetDouble(14);
			AvgPx = view.GetDouble(6);
			DayOrderQty = view.GetDouble(424);
			DayCumQty = view.GetDouble(425);
			DayAvgPx = view.GetDouble(426);
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
			ExDate = view.GetString(230);
			AccruedInterestRate = view.GetDouble(158);
			AccruedInterestAmt = view.GetDouble(159);
			TradedFlatSwitch = view.GetBool(258);
			BasisFeatureDate = view.GetString(259);
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
			MaxFloor = view.GetDouble(111);
			PositionEffect = view.GetString(77);
			MaxShow = view.GetDouble(210);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			FutSettDate2 = view.GetDateOnly(193);
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
			if (view.GetView("NoContAmts") is IMessageView viewNoContAmts)
			{
				var count = viewNoContAmts.GroupCount();
				NoContAmts = new NoContAmts[count];
				for (int i = 0; i < count; i++)
				{
					NoContAmts[i] = new();
					((IFixParser)NoContAmts[i]).Parse(viewNoContAmts.GetGroupInstance(i));
				}
			}
			if (view.GetView("NoLegs") is IMessageView viewNoLegs)
			{
				var count = viewNoLegs.GroupCount();
				NoLegs = new NoLegs[count];
				for (int i = 0; i < count; i++)
				{
					NoLegs[i] = new();
					((IFixParser)NoLegs[i]).Parse(viewNoLegs.GetGroupInstance(i));
				}
			}
			if (view.GetView("StandardTrailer") is IMessageView viewStandardTrailer)
			{
				StandardTrailer = new();
				((IFixParser)StandardTrailer).Parse(viewStandardTrailer);
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
				case "Parties":
					value = Parties;
					break;
				case "TradeOriginationDate":
					value = TradeOriginationDate;
					break;
				case "NoContraBrokers":
					value = NoContraBrokers;
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
				case "SettlmntTyp":
					value = SettlmntTyp;
					break;
				case "FutSettDate":
					value = FutSettDate;
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
				case "Side":
					value = Side;
					break;
				case "Stipulations":
					value = Stipulations;
					break;
				case "QuantityType":
					value = QuantityType;
					break;
				case "OrderQtyData":
					value = OrderQtyData;
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
				case "StopPx":
					value = StopPx;
					break;
				case "PegDifference":
					value = PegDifference;
					break;
				case "DiscretionInst":
					value = DiscretionInst;
					break;
				case "DiscretionOffset":
					value = DiscretionOffset;
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
				case "OrderCapacity":
					value = OrderCapacity;
					break;
				case "OrderRestrictions":
					value = OrderRestrictions;
					break;
				case "CustOrderCapacity":
					value = CustOrderCapacity;
					break;
				case "Rule80A":
					value = Rule80A;
					break;
				case "LastQty":
					value = LastQty;
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
				case "MaxFloor":
					value = MaxFloor;
					break;
				case "PositionEffect":
					value = PositionEffect;
					break;
				case "MaxShow":
					value = MaxShow;
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
				case "FutSettDate2":
					value = FutSettDate2;
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
				case "NoContAmts":
					value = NoContAmts;
					break;
				case "NoLegs":
					value = NoLegs;
					break;
				case "StandardTrailer":
					value = StandardTrailer;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			OrderID = null;
			SecondaryOrderID = null;
			SecondaryClOrdID = null;
			SecondaryExecID = null;
			ClOrdID = null;
			OrigClOrdID = null;
			ClOrdLinkID = null;
			((IFixReset?)Parties)?.Reset();
			TradeOriginationDate = null;
			NoContraBrokers = null;
			ListID = null;
			CrossID = null;
			OrigCrossID = null;
			CrossType = null;
			ExecID = null;
			ExecRefID = null;
			ExecType = null;
			OrdStatus = null;
			WorkingIndicator = null;
			OrdRejReason = null;
			ExecRestatementReason = null;
			Account = null;
			AccountType = null;
			DayBookingInst = null;
			BookingUnit = null;
			PreallocMethod = null;
			SettlmntTyp = null;
			FutSettDate = null;
			CashMargin = null;
			ClearingFeeIndicator = null;
			((IFixReset?)Instrument)?.Reset();
			Side = null;
			((IFixReset?)Stipulations)?.Reset();
			QuantityType = null;
			((IFixReset?)OrderQtyData)?.Reset();
			OrdType = null;
			PriceType = null;
			Price = null;
			StopPx = null;
			PegDifference = null;
			DiscretionInst = null;
			DiscretionOffset = null;
			Currency = null;
			ComplianceID = null;
			SolicitedFlag = null;
			TimeInForce = null;
			EffectiveTime = null;
			ExpireDate = null;
			ExpireTime = null;
			ExecInst = null;
			OrderCapacity = null;
			OrderRestrictions = null;
			CustOrderCapacity = null;
			Rule80A = null;
			LastQty = null;
			UnderlyingLastQty = null;
			LastPx = null;
			UnderlyingLastPx = null;
			LastSpotRate = null;
			LastForwardPoints = null;
			LastMkt = null;
			TradingSessionID = null;
			TradingSessionSubID = null;
			LastCapacity = null;
			LeavesQty = null;
			CumQty = null;
			AvgPx = null;
			DayOrderQty = null;
			DayCumQty = null;
			DayAvgPx = null;
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
			MaxFloor = null;
			PositionEffect = null;
			MaxShow = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			FutSettDate2 = null;
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
			NoContAmts = null;
			NoLegs = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
