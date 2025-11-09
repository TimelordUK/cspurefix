using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44.Components;

namespace PureFix.Types.FIX44
{
	[MessageType("D", FixVersion.FIX44)]
	public sealed partial class NewOrderSingle : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 1, Required = true)]
		public string? ClOrdID {get; set;}
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryClOrdID {get; set;}
		
		[TagDetails(Tag = 583, Type = TagType.String, Offset = 3, Required = false)]
		public string? ClOrdLinkID {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public Parties? Parties {get; set;}
		
		[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 5, Required = false)]
		public DateOnly? TradeOriginationDate {get; set;}
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 6, Required = false)]
		public DateOnly? TradeDate {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 7, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 8, Required = false)]
		public int? AcctIDSource {get; set;}
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 9, Required = false)]
		public int? AccountType {get; set;}
		
		[TagDetails(Tag = 589, Type = TagType.String, Offset = 10, Required = false)]
		public string? DayBookingInst {get; set;}
		
		[TagDetails(Tag = 590, Type = TagType.String, Offset = 11, Required = false)]
		public string? BookingUnit {get; set;}
		
		[TagDetails(Tag = 591, Type = TagType.String, Offset = 12, Required = false)]
		public string? PreallocMethod {get; set;}
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 13, Required = false)]
		public string? AllocID {get; set;}
		
		[Component(Offset = 14, Required = false)]
		public PreAllocGrp? PreAllocGrp {get; set;}
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 15, Required = false)]
		public string? SettlType {get; set;}
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 16, Required = false)]
		public DateOnly? SettlDate {get; set;}
		
		[TagDetails(Tag = 544, Type = TagType.String, Offset = 17, Required = false)]
		public string? CashMargin {get; set;}
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 18, Required = false)]
		public string? ClearingFeeIndicator {get; set;}
		
		[TagDetails(Tag = 21, Type = TagType.String, Offset = 19, Required = false)]
		public string? HandlInst {get; set;}
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 20, Required = false)]
		public string? ExecInst {get; set;}
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 21, Required = false)]
		public double? MinQty {get; set;}
		
		[TagDetails(Tag = 111, Type = TagType.Float, Offset = 22, Required = false)]
		public double? MaxFloor {get; set;}
		
		[TagDetails(Tag = 100, Type = TagType.String, Offset = 23, Required = false)]
		public string? ExDestination {get; set;}
		
		[Component(Offset = 24, Required = false)]
		public TrdgSesGrp? TrdgSesGrp {get; set;}
		
		[TagDetails(Tag = 81, Type = TagType.String, Offset = 25, Required = false)]
		public string? ProcessCode {get; set;}
		
		[Component(Offset = 26, Required = true)]
		public Instrument? Instrument {get; set;}
		
		[Component(Offset = 27, Required = false)]
		public FinancingDetails? FinancingDetails {get; set;}
		
		[Component(Offset = 28, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp {get; set;}
		
		[TagDetails(Tag = 140, Type = TagType.Float, Offset = 29, Required = false)]
		public double? PrevClosePx {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 30, Required = true)]
		public string? Side {get; set;}
		
		[TagDetails(Tag = 114, Type = TagType.Boolean, Offset = 31, Required = false)]
		public bool? LocateReqd {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 32, Required = true)]
		public DateTime? TransactTime {get; set;}
		
		[Component(Offset = 33, Required = false)]
		public Stipulations? Stipulations {get; set;}
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 34, Required = false)]
		public int? QtyType {get; set;}
		
		[Component(Offset = 35, Required = true)]
		public OrderQtyData? OrderQtyData {get; set;}
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 36, Required = true)]
		public string? OrdType {get; set;}
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 37, Required = false)]
		public int? PriceType {get; set;}
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 38, Required = false)]
		public double? Price {get; set;}
		
		[TagDetails(Tag = 99, Type = TagType.Float, Offset = 39, Required = false)]
		public double? StopPx {get; set;}
		
		[Component(Offset = 40, Required = false)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData {get; set;}
		
		[Component(Offset = 41, Required = false)]
		public YieldData? YieldData {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 42, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 43, Required = false)]
		public string? ComplianceID {get; set;}
		
		[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 44, Required = false)]
		public bool? SolicitedFlag {get; set;}
		
		[TagDetails(Tag = 23, Type = TagType.String, Offset = 45, Required = false)]
		public string? IOIID {get; set;}
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 46, Required = false)]
		public string? QuoteID {get; set;}
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 47, Required = false)]
		public string? TimeInForce {get; set;}
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 48, Required = false)]
		public DateTime? EffectiveTime {get; set;}
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 49, Required = false)]
		public DateOnly? ExpireDate {get; set;}
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 50, Required = false)]
		public DateTime? ExpireTime {get; set;}
		
		[TagDetails(Tag = 427, Type = TagType.Int, Offset = 51, Required = false)]
		public int? GTBookingInst {get; set;}
		
		[Component(Offset = 52, Required = false)]
		public CommissionData? CommissionData {get; set;}
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 53, Required = false)]
		public string? OrderCapacity {get; set;}
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 54, Required = false)]
		public string? OrderRestrictions {get; set;}
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 55, Required = false)]
		public int? CustOrderCapacity {get; set;}
		
		[TagDetails(Tag = 121, Type = TagType.Boolean, Offset = 56, Required = false)]
		public bool? ForexReq {get; set;}
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 57, Required = false)]
		public string? SettlCurrency {get; set;}
		
		[TagDetails(Tag = 775, Type = TagType.Int, Offset = 58, Required = false)]
		public int? BookingType {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 59, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 60, Required = false)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 61, Required = false)]
		public byte[]? EncodedText {get; set;}
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 62, Required = false)]
		public DateOnly? SettlDate2 {get; set;}
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 63, Required = false)]
		public double? OrderQty2 {get; set;}
		
		[TagDetails(Tag = 640, Type = TagType.Float, Offset = 64, Required = false)]
		public double? Price2 {get; set;}
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 65, Required = false)]
		public string? PositionEffect {get; set;}
		
		[TagDetails(Tag = 203, Type = TagType.Int, Offset = 66, Required = false)]
		public int? CoveredOrUncovered {get; set;}
		
		[TagDetails(Tag = 210, Type = TagType.Float, Offset = 67, Required = false)]
		public double? MaxShow {get; set;}
		
		[Component(Offset = 68, Required = false)]
		public PegInstructions? PegInstructions {get; set;}
		
		[Component(Offset = 69, Required = false)]
		public DiscretionInstructions? DiscretionInstructions {get; set;}
		
		[TagDetails(Tag = 847, Type = TagType.Int, Offset = 70, Required = false)]
		public int? TargetStrategy {get; set;}
		
		[TagDetails(Tag = 848, Type = TagType.String, Offset = 71, Required = false)]
		public string? TargetStrategyParameters {get; set;}
		
		[TagDetails(Tag = 849, Type = TagType.Float, Offset = 72, Required = false)]
		public double? ParticipationRate {get; set;}
		
		[TagDetails(Tag = 480, Type = TagType.String, Offset = 73, Required = false)]
		public string? CancellationRights {get; set;}
		
		[TagDetails(Tag = 481, Type = TagType.String, Offset = 74, Required = false)]
		public string? MoneyLaunderingStatus {get; set;}
		
		[TagDetails(Tag = 513, Type = TagType.String, Offset = 75, Required = false)]
		public string? RegistID {get; set;}
		
		[TagDetails(Tag = 494, Type = TagType.String, Offset = 76, Required = false)]
		public string? Designation {get; set;}
		
		[Component(Offset = 77, Required = true)]
		public StandardTrailer? StandardTrailer {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return (!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config))) && (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
			if (ClOrdLinkID is not null) writer.WriteString(583, ClOrdLinkID);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (TradeOriginationDate is not null) writer.WriteLocalDateOnly(229, TradeOriginationDate.Value);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (DayBookingInst is not null) writer.WriteString(589, DayBookingInst);
			if (BookingUnit is not null) writer.WriteString(590, BookingUnit);
			if (PreallocMethod is not null) writer.WriteString(591, PreallocMethod);
			if (AllocID is not null) writer.WriteString(70, AllocID);
			if (PreAllocGrp is not null) ((IFixEncoder)PreAllocGrp).Encode(writer);
			if (SettlType is not null) writer.WriteString(63, SettlType);
			if (SettlDate is not null) writer.WriteLocalDateOnly(64, SettlDate.Value);
			if (CashMargin is not null) writer.WriteString(544, CashMargin);
			if (ClearingFeeIndicator is not null) writer.WriteString(635, ClearingFeeIndicator);
			if (HandlInst is not null) writer.WriteString(21, HandlInst);
			if (ExecInst is not null) writer.WriteString(18, ExecInst);
			if (MinQty is not null) writer.WriteNumber(110, MinQty.Value);
			if (MaxFloor is not null) writer.WriteNumber(111, MaxFloor.Value);
			if (ExDestination is not null) writer.WriteString(100, ExDestination);
			if (TrdgSesGrp is not null) ((IFixEncoder)TrdgSesGrp).Encode(writer);
			if (ProcessCode is not null) writer.WriteString(81, ProcessCode);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (PrevClosePx is not null) writer.WriteNumber(140, PrevClosePx.Value);
			if (Side is not null) writer.WriteString(54, Side);
			if (LocateReqd is not null) writer.WriteBoolean(114, LocateReqd.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (Stipulations is not null) ((IFixEncoder)Stipulations).Encode(writer);
			if (QtyType is not null) writer.WriteWholeNumber(854, QtyType.Value);
			if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
			if (OrdType is not null) writer.WriteString(40, OrdType);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (StopPx is not null) writer.WriteNumber(99, StopPx.Value);
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (YieldData is not null) ((IFixEncoder)YieldData).Encode(writer);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (ComplianceID is not null) writer.WriteString(376, ComplianceID);
			if (SolicitedFlag is not null) writer.WriteBoolean(377, SolicitedFlag.Value);
			if (IOIID is not null) writer.WriteString(23, IOIID);
			if (QuoteID is not null) writer.WriteString(117, QuoteID);
			if (TimeInForce is not null) writer.WriteString(59, TimeInForce);
			if (EffectiveTime is not null) writer.WriteUtcTimeStamp(168, EffectiveTime.Value);
			if (ExpireDate is not null) writer.WriteLocalDateOnly(432, ExpireDate.Value);
			if (ExpireTime is not null) writer.WriteUtcTimeStamp(126, ExpireTime.Value);
			if (GTBookingInst is not null) writer.WriteWholeNumber(427, GTBookingInst.Value);
			if (CommissionData is not null) ((IFixEncoder)CommissionData).Encode(writer);
			if (OrderCapacity is not null) writer.WriteString(528, OrderCapacity);
			if (OrderRestrictions is not null) writer.WriteString(529, OrderRestrictions);
			if (CustOrderCapacity is not null) writer.WriteWholeNumber(582, CustOrderCapacity.Value);
			if (ForexReq is not null) writer.WriteBoolean(121, ForexReq.Value);
			if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
			if (BookingType is not null) writer.WriteWholeNumber(775, BookingType.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
			if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
			if (SettlDate2 is not null) writer.WriteLocalDateOnly(193, SettlDate2.Value);
			if (OrderQty2 is not null) writer.WriteNumber(192, OrderQty2.Value);
			if (Price2 is not null) writer.WriteNumber(640, Price2.Value);
			if (PositionEffect is not null) writer.WriteString(77, PositionEffect);
			if (CoveredOrUncovered is not null) writer.WriteWholeNumber(203, CoveredOrUncovered.Value);
			if (MaxShow is not null) writer.WriteNumber(210, MaxShow.Value);
			if (PegInstructions is not null) ((IFixEncoder)PegInstructions).Encode(writer);
			if (DiscretionInstructions is not null) ((IFixEncoder)DiscretionInstructions).Encode(writer);
			if (TargetStrategy is not null) writer.WriteWholeNumber(847, TargetStrategy.Value);
			if (TargetStrategyParameters is not null) writer.WriteString(848, TargetStrategyParameters);
			if (ParticipationRate is not null) writer.WriteNumber(849, ParticipationRate.Value);
			if (CancellationRights is not null) writer.WriteString(480, CancellationRights);
			if (MoneyLaunderingStatus is not null) writer.WriteString(481, MoneyLaunderingStatus);
			if (RegistID is not null) writer.WriteString(513, RegistID);
			if (Designation is not null) writer.WriteString(494, Designation);
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
			ClOrdID = view.GetString(11);
			SecondaryClOrdID = view.GetString(526);
			ClOrdLinkID = view.GetString(583);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				Parties = new();
				((IFixParser)Parties).Parse(viewParties);
			}
			TradeOriginationDate = view.GetDateOnly(229);
			TradeDate = view.GetDateOnly(75);
			Account = view.GetString(1);
			AcctIDSource = view.GetInt32(660);
			AccountType = view.GetInt32(581);
			DayBookingInst = view.GetString(589);
			BookingUnit = view.GetString(590);
			PreallocMethod = view.GetString(591);
			AllocID = view.GetString(70);
			if (view.GetView("PreAllocGrp") is IMessageView viewPreAllocGrp)
			{
				PreAllocGrp = new();
				((IFixParser)PreAllocGrp).Parse(viewPreAllocGrp);
			}
			SettlType = view.GetString(63);
			SettlDate = view.GetDateOnly(64);
			CashMargin = view.GetString(544);
			ClearingFeeIndicator = view.GetString(635);
			HandlInst = view.GetString(21);
			ExecInst = view.GetString(18);
			MinQty = view.GetDouble(110);
			MaxFloor = view.GetDouble(111);
			ExDestination = view.GetString(100);
			if (view.GetView("TrdgSesGrp") is IMessageView viewTrdgSesGrp)
			{
				TrdgSesGrp = new();
				((IFixParser)TrdgSesGrp).Parse(viewTrdgSesGrp);
			}
			ProcessCode = view.GetString(81);
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
			PrevClosePx = view.GetDouble(140);
			Side = view.GetString(54);
			LocateReqd = view.GetBool(114);
			TransactTime = view.GetDateTime(60);
			if (view.GetView("Stipulations") is IMessageView viewStipulations)
			{
				Stipulations = new();
				((IFixParser)Stipulations).Parse(viewStipulations);
			}
			QtyType = view.GetInt32(854);
			if (view.GetView("OrderQtyData") is IMessageView viewOrderQtyData)
			{
				OrderQtyData = new();
				((IFixParser)OrderQtyData).Parse(viewOrderQtyData);
			}
			OrdType = view.GetString(40);
			PriceType = view.GetInt32(423);
			Price = view.GetDouble(44);
			StopPx = view.GetDouble(99);
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
			Currency = view.GetString(15);
			ComplianceID = view.GetString(376);
			SolicitedFlag = view.GetBool(377);
			IOIID = view.GetString(23);
			QuoteID = view.GetString(117);
			TimeInForce = view.GetString(59);
			EffectiveTime = view.GetDateTime(168);
			ExpireDate = view.GetDateOnly(432);
			ExpireTime = view.GetDateTime(126);
			GTBookingInst = view.GetInt32(427);
			if (view.GetView("CommissionData") is IMessageView viewCommissionData)
			{
				CommissionData = new();
				((IFixParser)CommissionData).Parse(viewCommissionData);
			}
			OrderCapacity = view.GetString(528);
			OrderRestrictions = view.GetString(529);
			CustOrderCapacity = view.GetInt32(582);
			ForexReq = view.GetBool(121);
			SettlCurrency = view.GetString(120);
			BookingType = view.GetInt32(775);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			SettlDate2 = view.GetDateOnly(193);
			OrderQty2 = view.GetDouble(192);
			Price2 = view.GetDouble(640);
			PositionEffect = view.GetString(77);
			CoveredOrUncovered = view.GetInt32(203);
			MaxShow = view.GetDouble(210);
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
			TargetStrategy = view.GetInt32(847);
			TargetStrategyParameters = view.GetString(848);
			ParticipationRate = view.GetDouble(849);
			CancellationRights = view.GetString(480);
			MoneyLaunderingStatus = view.GetString(481);
			RegistID = view.GetString(513);
			Designation = view.GetString(494);
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
				{
					value = StandardHeader;
					break;
				}
				case "ClOrdID":
				{
					value = ClOrdID;
					break;
				}
				case "SecondaryClOrdID":
				{
					value = SecondaryClOrdID;
					break;
				}
				case "ClOrdLinkID":
				{
					value = ClOrdLinkID;
					break;
				}
				case "Parties":
				{
					value = Parties;
					break;
				}
				case "TradeOriginationDate":
				{
					value = TradeOriginationDate;
					break;
				}
				case "TradeDate":
				{
					value = TradeDate;
					break;
				}
				case "Account":
				{
					value = Account;
					break;
				}
				case "AcctIDSource":
				{
					value = AcctIDSource;
					break;
				}
				case "AccountType":
				{
					value = AccountType;
					break;
				}
				case "DayBookingInst":
				{
					value = DayBookingInst;
					break;
				}
				case "BookingUnit":
				{
					value = BookingUnit;
					break;
				}
				case "PreallocMethod":
				{
					value = PreallocMethod;
					break;
				}
				case "AllocID":
				{
					value = AllocID;
					break;
				}
				case "PreAllocGrp":
				{
					value = PreAllocGrp;
					break;
				}
				case "SettlType":
				{
					value = SettlType;
					break;
				}
				case "SettlDate":
				{
					value = SettlDate;
					break;
				}
				case "CashMargin":
				{
					value = CashMargin;
					break;
				}
				case "ClearingFeeIndicator":
				{
					value = ClearingFeeIndicator;
					break;
				}
				case "HandlInst":
				{
					value = HandlInst;
					break;
				}
				case "ExecInst":
				{
					value = ExecInst;
					break;
				}
				case "MinQty":
				{
					value = MinQty;
					break;
				}
				case "MaxFloor":
				{
					value = MaxFloor;
					break;
				}
				case "ExDestination":
				{
					value = ExDestination;
					break;
				}
				case "TrdgSesGrp":
				{
					value = TrdgSesGrp;
					break;
				}
				case "ProcessCode":
				{
					value = ProcessCode;
					break;
				}
				case "Instrument":
				{
					value = Instrument;
					break;
				}
				case "FinancingDetails":
				{
					value = FinancingDetails;
					break;
				}
				case "UndInstrmtGrp":
				{
					value = UndInstrmtGrp;
					break;
				}
				case "PrevClosePx":
				{
					value = PrevClosePx;
					break;
				}
				case "Side":
				{
					value = Side;
					break;
				}
				case "LocateReqd":
				{
					value = LocateReqd;
					break;
				}
				case "TransactTime":
				{
					value = TransactTime;
					break;
				}
				case "Stipulations":
				{
					value = Stipulations;
					break;
				}
				case "QtyType":
				{
					value = QtyType;
					break;
				}
				case "OrderQtyData":
				{
					value = OrderQtyData;
					break;
				}
				case "OrdType":
				{
					value = OrdType;
					break;
				}
				case "PriceType":
				{
					value = PriceType;
					break;
				}
				case "Price":
				{
					value = Price;
					break;
				}
				case "StopPx":
				{
					value = StopPx;
					break;
				}
				case "SpreadOrBenchmarkCurveData":
				{
					value = SpreadOrBenchmarkCurveData;
					break;
				}
				case "YieldData":
				{
					value = YieldData;
					break;
				}
				case "Currency":
				{
					value = Currency;
					break;
				}
				case "ComplianceID":
				{
					value = ComplianceID;
					break;
				}
				case "SolicitedFlag":
				{
					value = SolicitedFlag;
					break;
				}
				case "IOIID":
				{
					value = IOIID;
					break;
				}
				case "QuoteID":
				{
					value = QuoteID;
					break;
				}
				case "TimeInForce":
				{
					value = TimeInForce;
					break;
				}
				case "EffectiveTime":
				{
					value = EffectiveTime;
					break;
				}
				case "ExpireDate":
				{
					value = ExpireDate;
					break;
				}
				case "ExpireTime":
				{
					value = ExpireTime;
					break;
				}
				case "GTBookingInst":
				{
					value = GTBookingInst;
					break;
				}
				case "CommissionData":
				{
					value = CommissionData;
					break;
				}
				case "OrderCapacity":
				{
					value = OrderCapacity;
					break;
				}
				case "OrderRestrictions":
				{
					value = OrderRestrictions;
					break;
				}
				case "CustOrderCapacity":
				{
					value = CustOrderCapacity;
					break;
				}
				case "ForexReq":
				{
					value = ForexReq;
					break;
				}
				case "SettlCurrency":
				{
					value = SettlCurrency;
					break;
				}
				case "BookingType":
				{
					value = BookingType;
					break;
				}
				case "Text":
				{
					value = Text;
					break;
				}
				case "EncodedTextLen":
				{
					value = EncodedTextLen;
					break;
				}
				case "EncodedText":
				{
					value = EncodedText;
					break;
				}
				case "SettlDate2":
				{
					value = SettlDate2;
					break;
				}
				case "OrderQty2":
				{
					value = OrderQty2;
					break;
				}
				case "Price2":
				{
					value = Price2;
					break;
				}
				case "PositionEffect":
				{
					value = PositionEffect;
					break;
				}
				case "CoveredOrUncovered":
				{
					value = CoveredOrUncovered;
					break;
				}
				case "MaxShow":
				{
					value = MaxShow;
					break;
				}
				case "PegInstructions":
				{
					value = PegInstructions;
					break;
				}
				case "DiscretionInstructions":
				{
					value = DiscretionInstructions;
					break;
				}
				case "TargetStrategy":
				{
					value = TargetStrategy;
					break;
				}
				case "TargetStrategyParameters":
				{
					value = TargetStrategyParameters;
					break;
				}
				case "ParticipationRate":
				{
					value = ParticipationRate;
					break;
				}
				case "CancellationRights":
				{
					value = CancellationRights;
					break;
				}
				case "MoneyLaunderingStatus":
				{
					value = MoneyLaunderingStatus;
					break;
				}
				case "RegistID":
				{
					value = RegistID;
					break;
				}
				case "Designation":
				{
					value = Designation;
					break;
				}
				case "StandardTrailer":
				{
					value = StandardTrailer;
					break;
				}
				default:
				{
					return false;
				}
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			ClOrdID = null;
			SecondaryClOrdID = null;
			ClOrdLinkID = null;
			((IFixReset?)Parties)?.Reset();
			TradeOriginationDate = null;
			TradeDate = null;
			Account = null;
			AcctIDSource = null;
			AccountType = null;
			DayBookingInst = null;
			BookingUnit = null;
			PreallocMethod = null;
			AllocID = null;
			((IFixReset?)PreAllocGrp)?.Reset();
			SettlType = null;
			SettlDate = null;
			CashMargin = null;
			ClearingFeeIndicator = null;
			HandlInst = null;
			ExecInst = null;
			MinQty = null;
			MaxFloor = null;
			ExDestination = null;
			((IFixReset?)TrdgSesGrp)?.Reset();
			ProcessCode = null;
			((IFixReset?)Instrument)?.Reset();
			((IFixReset?)FinancingDetails)?.Reset();
			((IFixReset?)UndInstrmtGrp)?.Reset();
			PrevClosePx = null;
			Side = null;
			LocateReqd = null;
			TransactTime = null;
			((IFixReset?)Stipulations)?.Reset();
			QtyType = null;
			((IFixReset?)OrderQtyData)?.Reset();
			OrdType = null;
			PriceType = null;
			Price = null;
			StopPx = null;
			((IFixReset?)SpreadOrBenchmarkCurveData)?.Reset();
			((IFixReset?)YieldData)?.Reset();
			Currency = null;
			ComplianceID = null;
			SolicitedFlag = null;
			IOIID = null;
			QuoteID = null;
			TimeInForce = null;
			EffectiveTime = null;
			ExpireDate = null;
			ExpireTime = null;
			GTBookingInst = null;
			((IFixReset?)CommissionData)?.Reset();
			OrderCapacity = null;
			OrderRestrictions = null;
			CustOrderCapacity = null;
			ForexReq = null;
			SettlCurrency = null;
			BookingType = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			SettlDate2 = null;
			OrderQty2 = null;
			Price2 = null;
			PositionEffect = null;
			CoveredOrUncovered = null;
			MaxShow = null;
			((IFixReset?)PegInstructions)?.Reset();
			((IFixReset?)DiscretionInstructions)?.Reset();
			TargetStrategy = null;
			TargetStrategyParameters = null;
			ParticipationRate = null;
			CancellationRights = null;
			MoneyLaunderingStatus = null;
			RegistID = null;
			Designation = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
