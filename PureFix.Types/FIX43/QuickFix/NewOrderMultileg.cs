using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("AB", FixVersion.FIX43)]
	public sealed partial class NewOrderMultileg : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 1, Required = true)]
		public string? ClOrdID {get; set;}
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryClOrdID {get; set;}
		
		[TagDetails(Tag = 583, Type = TagType.String, Offset = 3, Required = false)]
		public string? ClOrdLinkID {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public PartiesComponent? Parties {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 5, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 6, Required = false)]
		public int? AccountType {get; set;}
		
		[TagDetails(Tag = 589, Type = TagType.String, Offset = 7, Required = false)]
		public string? DayBookingInst {get; set;}
		
		[TagDetails(Tag = 590, Type = TagType.String, Offset = 8, Required = false)]
		public string? BookingUnit {get; set;}
		
		[TagDetails(Tag = 591, Type = TagType.String, Offset = 9, Required = false)]
		public string? PreallocMethod {get; set;}
		
		[Group(NoOfTag = 78, Offset = 10, Required = false)]
		public NewOrderMultilegNoAllocs[]? NoAllocs {get; set;}
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 11, Required = false)]
		public string? SettlmntTyp {get; set;}
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 12, Required = false)]
		public DateOnly? FutSettDate {get; set;}
		
		[TagDetails(Tag = 544, Type = TagType.String, Offset = 13, Required = false)]
		public string? CashMargin {get; set;}
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 14, Required = false)]
		public string? ClearingFeeIndicator {get; set;}
		
		[TagDetails(Tag = 21, Type = TagType.String, Offset = 15, Required = true)]
		public string? HandlInst {get; set;}
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 16, Required = false)]
		public string? ExecInst {get; set;}
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 17, Required = false)]
		public double? MinQty {get; set;}
		
		[TagDetails(Tag = 111, Type = TagType.Float, Offset = 18, Required = false)]
		public double? MaxFloor {get; set;}
		
		[TagDetails(Tag = 100, Type = TagType.String, Offset = 19, Required = false)]
		public string? ExDestination {get; set;}
		
		[Group(NoOfTag = 386, Offset = 20, Required = false)]
		public NewOrderMultilegNoTradingSessions[]? NoTradingSessions {get; set;}
		
		[TagDetails(Tag = 81, Type = TagType.String, Offset = 21, Required = false)]
		public string? ProcessCode {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 22, Required = true)]
		public string? Side {get; set;}
		
		[Component(Offset = 23, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
		[TagDetails(Tag = 140, Type = TagType.Float, Offset = 24, Required = false)]
		public double? PrevClosePx {get; set;}
		
		[Group(NoOfTag = 555, Offset = 25, Required = true)]
		public NewOrderMultilegNoLegs[]? NoLegs {get; set;}
		
		[TagDetails(Tag = 114, Type = TagType.Boolean, Offset = 26, Required = false)]
		public bool? LocateReqd {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 27, Required = true)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 465, Type = TagType.Int, Offset = 28, Required = false)]
		public int? QuantityType {get; set;}
		
		[Component(Offset = 29, Required = true)]
		public OrderQtyDataComponent? OrderQtyData {get; set;}
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 30, Required = true)]
		public string? OrdType {get; set;}
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 31, Required = false)]
		public int? PriceType {get; set;}
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 32, Required = false)]
		public double? Price {get; set;}
		
		[TagDetails(Tag = 99, Type = TagType.Float, Offset = 33, Required = false)]
		public double? StopPx {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 34, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 35, Required = false)]
		public string? ComplianceID {get; set;}
		
		[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 36, Required = false)]
		public bool? SolicitedFlag {get; set;}
		
		[TagDetails(Tag = 23, Type = TagType.String, Offset = 37, Required = false)]
		public string? IOIid {get; set;}
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 38, Required = false)]
		public string? QuoteID {get; set;}
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 39, Required = false)]
		public string? TimeInForce {get; set;}
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 40, Required = false)]
		public DateTime? EffectiveTime {get; set;}
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 41, Required = false)]
		public DateOnly? ExpireDate {get; set;}
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 42, Required = false)]
		public DateTime? ExpireTime {get; set;}
		
		[TagDetails(Tag = 427, Type = TagType.Int, Offset = 43, Required = false)]
		public int? GTBookingInst {get; set;}
		
		[Component(Offset = 44, Required = false)]
		public CommissionDataComponent? CommissionData {get; set;}
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 45, Required = false)]
		public string? OrderCapacity {get; set;}
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 46, Required = false)]
		public string? OrderRestrictions {get; set;}
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 47, Required = false)]
		public int? CustOrderCapacity {get; set;}
		
		[TagDetails(Tag = 121, Type = TagType.Boolean, Offset = 48, Required = false)]
		public bool? ForexReq {get; set;}
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 49, Required = false)]
		public string? SettlCurrency {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 50, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 51, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 52, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 53, Required = false)]
		public string? PositionEffect {get; set;}
		
		[TagDetails(Tag = 203, Type = TagType.Int, Offset = 54, Required = false)]
		public int? CoveredOrUncovered {get; set;}
		
		[TagDetails(Tag = 210, Type = TagType.Float, Offset = 55, Required = false)]
		public double? MaxShow {get; set;}
		
		[TagDetails(Tag = 211, Type = TagType.Float, Offset = 56, Required = false)]
		public double? PegDifference {get; set;}
		
		[TagDetails(Tag = 388, Type = TagType.String, Offset = 57, Required = false)]
		public string? DiscretionInst {get; set;}
		
		[TagDetails(Tag = 389, Type = TagType.Float, Offset = 58, Required = false)]
		public double? DiscretionOffset {get; set;}
		
		[TagDetails(Tag = 480, Type = TagType.String, Offset = 59, Required = false)]
		public string? CancellationRights {get; set;}
		
		[TagDetails(Tag = 481, Type = TagType.String, Offset = 60, Required = false)]
		public string? MoneyLaunderingStatus {get; set;}
		
		[TagDetails(Tag = 513, Type = TagType.String, Offset = 61, Required = false)]
		public string? RegistID {get; set;}
		
		[TagDetails(Tag = 494, Type = TagType.String, Offset = 62, Required = false)]
		public string? Designation {get; set;}
		
		[TagDetails(Tag = 563, Type = TagType.Int, Offset = 63, Required = false)]
		public int? MultiLegRptTypeReq {get; set;}
		
		[TagDetails(Tag = 118, Type = TagType.Float, Offset = 64, Required = false)]
		public double? NetMoney {get; set;}
		
		[Component(Offset = 65, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& ClOrdID is not null
				&& HandlInst is not null
				&& Side is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& NoLegs is not null && FixValidator.IsValid(NoLegs, in config)
				&& TransactTime is not null
				&& OrderQtyData is not null && ((IFixValidator)OrderQtyData).IsValid(in config)
				&& OrdType is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
			if (ClOrdLinkID is not null) writer.WriteString(583, ClOrdLinkID);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Account is not null) writer.WriteString(1, Account);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (DayBookingInst is not null) writer.WriteString(589, DayBookingInst);
			if (BookingUnit is not null) writer.WriteString(590, BookingUnit);
			if (PreallocMethod is not null) writer.WriteString(591, PreallocMethod);
			if (NoAllocs is not null && NoAllocs.Length != 0)
			{
				writer.WriteWholeNumber(78, NoAllocs.Length);
				for (int i = 0; i < NoAllocs.Length; i++)
				{
					((IFixEncoder)NoAllocs[i]).Encode(writer);
				}
			}
			if (SettlmntTyp is not null) writer.WriteString(63, SettlmntTyp);
			if (FutSettDate is not null) writer.WriteLocalDateOnly(64, FutSettDate.Value);
			if (CashMargin is not null) writer.WriteString(544, CashMargin);
			if (ClearingFeeIndicator is not null) writer.WriteString(635, ClearingFeeIndicator);
			if (HandlInst is not null) writer.WriteString(21, HandlInst);
			if (ExecInst is not null) writer.WriteString(18, ExecInst);
			if (MinQty is not null) writer.WriteNumber(110, MinQty.Value);
			if (MaxFloor is not null) writer.WriteNumber(111, MaxFloor.Value);
			if (ExDestination is not null) writer.WriteString(100, ExDestination);
			if (NoTradingSessions is not null && NoTradingSessions.Length != 0)
			{
				writer.WriteWholeNumber(386, NoTradingSessions.Length);
				for (int i = 0; i < NoTradingSessions.Length; i++)
				{
					((IFixEncoder)NoTradingSessions[i]).Encode(writer);
				}
			}
			if (ProcessCode is not null) writer.WriteString(81, ProcessCode);
			if (Side is not null) writer.WriteString(54, Side);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (PrevClosePx is not null) writer.WriteNumber(140, PrevClosePx.Value);
			if (NoLegs is not null && NoLegs.Length != 0)
			{
				writer.WriteWholeNumber(555, NoLegs.Length);
				for (int i = 0; i < NoLegs.Length; i++)
				{
					((IFixEncoder)NoLegs[i]).Encode(writer);
				}
			}
			if (LocateReqd is not null) writer.WriteBoolean(114, LocateReqd.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (QuantityType is not null) writer.WriteWholeNumber(465, QuantityType.Value);
			if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
			if (OrdType is not null) writer.WriteString(40, OrdType);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (StopPx is not null) writer.WriteNumber(99, StopPx.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (ComplianceID is not null) writer.WriteString(376, ComplianceID);
			if (SolicitedFlag is not null) writer.WriteBoolean(377, SolicitedFlag.Value);
			if (IOIid is not null) writer.WriteString(23, IOIid);
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
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (PositionEffect is not null) writer.WriteString(77, PositionEffect);
			if (CoveredOrUncovered is not null) writer.WriteWholeNumber(203, CoveredOrUncovered.Value);
			if (MaxShow is not null) writer.WriteNumber(210, MaxShow.Value);
			if (PegDifference is not null) writer.WriteNumber(211, PegDifference.Value);
			if (DiscretionInst is not null) writer.WriteString(388, DiscretionInst);
			if (DiscretionOffset is not null) writer.WriteNumber(389, DiscretionOffset.Value);
			if (CancellationRights is not null) writer.WriteString(480, CancellationRights);
			if (MoneyLaunderingStatus is not null) writer.WriteString(481, MoneyLaunderingStatus);
			if (RegistID is not null) writer.WriteString(513, RegistID);
			if (Designation is not null) writer.WriteString(494, Designation);
			if (MultiLegRptTypeReq is not null) writer.WriteWholeNumber(563, MultiLegRptTypeReq.Value);
			if (NetMoney is not null) writer.WriteNumber(118, NetMoney.Value);
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
			Account = view.GetString(1);
			AccountType = view.GetInt32(581);
			DayBookingInst = view.GetString(589);
			BookingUnit = view.GetString(590);
			PreallocMethod = view.GetString(591);
			if (view.GetView("NoAllocs") is IMessageView viewNoAllocs)
			{
				var count = viewNoAllocs.GroupCount();
				NoAllocs = new NewOrderMultilegNoAllocs[count];
				for (int i = 0; i < count; i++)
				{
					NoAllocs[i] = new();
					((IFixParser)NoAllocs[i]).Parse(viewNoAllocs.GetGroupInstance(i));
				}
			}
			SettlmntTyp = view.GetString(63);
			FutSettDate = view.GetDateOnly(64);
			CashMargin = view.GetString(544);
			ClearingFeeIndicator = view.GetString(635);
			HandlInst = view.GetString(21);
			ExecInst = view.GetString(18);
			MinQty = view.GetDouble(110);
			MaxFloor = view.GetDouble(111);
			ExDestination = view.GetString(100);
			if (view.GetView("NoTradingSessions") is IMessageView viewNoTradingSessions)
			{
				var count = viewNoTradingSessions.GroupCount();
				NoTradingSessions = new NewOrderMultilegNoTradingSessions[count];
				for (int i = 0; i < count; i++)
				{
					NoTradingSessions[i] = new();
					((IFixParser)NoTradingSessions[i]).Parse(viewNoTradingSessions.GetGroupInstance(i));
				}
			}
			ProcessCode = view.GetString(81);
			Side = view.GetString(54);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			PrevClosePx = view.GetDouble(140);
			if (view.GetView("NoLegs") is IMessageView viewNoLegs)
			{
				var count = viewNoLegs.GroupCount();
				NoLegs = new NewOrderMultilegNoLegs[count];
				for (int i = 0; i < count; i++)
				{
					NoLegs[i] = new();
					((IFixParser)NoLegs[i]).Parse(viewNoLegs.GetGroupInstance(i));
				}
			}
			LocateReqd = view.GetBool(114);
			TransactTime = view.GetDateTime(60);
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
			Currency = view.GetString(15);
			ComplianceID = view.GetString(376);
			SolicitedFlag = view.GetBool(377);
			IOIid = view.GetString(23);
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
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			PositionEffect = view.GetString(77);
			CoveredOrUncovered = view.GetInt32(203);
			MaxShow = view.GetDouble(210);
			PegDifference = view.GetDouble(211);
			DiscretionInst = view.GetString(388);
			DiscretionOffset = view.GetDouble(389);
			CancellationRights = view.GetString(480);
			MoneyLaunderingStatus = view.GetString(481);
			RegistID = view.GetString(513);
			Designation = view.GetString(494);
			MultiLegRptTypeReq = view.GetInt32(563);
			NetMoney = view.GetDouble(118);
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
				case "ClOrdID":
					value = ClOrdID;
					break;
				case "SecondaryClOrdID":
					value = SecondaryClOrdID;
					break;
				case "ClOrdLinkID":
					value = ClOrdLinkID;
					break;
				case "Parties":
					value = Parties;
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
				case "NoAllocs":
					value = NoAllocs;
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
				case "HandlInst":
					value = HandlInst;
					break;
				case "ExecInst":
					value = ExecInst;
					break;
				case "MinQty":
					value = MinQty;
					break;
				case "MaxFloor":
					value = MaxFloor;
					break;
				case "ExDestination":
					value = ExDestination;
					break;
				case "NoTradingSessions":
					value = NoTradingSessions;
					break;
				case "ProcessCode":
					value = ProcessCode;
					break;
				case "Side":
					value = Side;
					break;
				case "Instrument":
					value = Instrument;
					break;
				case "PrevClosePx":
					value = PrevClosePx;
					break;
				case "NoLegs":
					value = NoLegs;
					break;
				case "LocateReqd":
					value = LocateReqd;
					break;
				case "TransactTime":
					value = TransactTime;
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
				case "Currency":
					value = Currency;
					break;
				case "ComplianceID":
					value = ComplianceID;
					break;
				case "SolicitedFlag":
					value = SolicitedFlag;
					break;
				case "IOIid":
					value = IOIid;
					break;
				case "QuoteID":
					value = QuoteID;
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
				case "GTBookingInst":
					value = GTBookingInst;
					break;
				case "CommissionData":
					value = CommissionData;
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
				case "ForexReq":
					value = ForexReq;
					break;
				case "SettlCurrency":
					value = SettlCurrency;
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
				case "PositionEffect":
					value = PositionEffect;
					break;
				case "CoveredOrUncovered":
					value = CoveredOrUncovered;
					break;
				case "MaxShow":
					value = MaxShow;
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
				case "MultiLegRptTypeReq":
					value = MultiLegRptTypeReq;
					break;
				case "NetMoney":
					value = NetMoney;
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
			ClOrdID = null;
			SecondaryClOrdID = null;
			ClOrdLinkID = null;
			((IFixReset?)Parties)?.Reset();
			Account = null;
			AccountType = null;
			DayBookingInst = null;
			BookingUnit = null;
			PreallocMethod = null;
			NoAllocs = null;
			SettlmntTyp = null;
			FutSettDate = null;
			CashMargin = null;
			ClearingFeeIndicator = null;
			HandlInst = null;
			ExecInst = null;
			MinQty = null;
			MaxFloor = null;
			ExDestination = null;
			NoTradingSessions = null;
			ProcessCode = null;
			Side = null;
			((IFixReset?)Instrument)?.Reset();
			PrevClosePx = null;
			NoLegs = null;
			LocateReqd = null;
			TransactTime = null;
			QuantityType = null;
			((IFixReset?)OrderQtyData)?.Reset();
			OrdType = null;
			PriceType = null;
			Price = null;
			StopPx = null;
			Currency = null;
			ComplianceID = null;
			SolicitedFlag = null;
			IOIid = null;
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
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			PositionEffect = null;
			CoveredOrUncovered = null;
			MaxShow = null;
			PegDifference = null;
			DiscretionInst = null;
			DiscretionOffset = null;
			CancellationRights = null;
			MoneyLaunderingStatus = null;
			RegistID = null;
			Designation = null;
			MultiLegRptTypeReq = null;
			NetMoney = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
