using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class TradeCaptureReportTrdCapRptSideGrpNoSides : IFixGroup
	{
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 0, Required = true)]
		public string? Side {get; set;}
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = true)]
		public string? OrderID {get; set;}
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryOrderID {get; set;}
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 3, Required = false)]
		public string? ClOrdID {get; set;}
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 4, Required = false)]
		public string? SecondaryClOrdID {get; set;}
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 5, Required = false)]
		public string? ListID {get; set;}
		
		[Component(Offset = 6, Required = false)]
		public PartiesComponent? Parties {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 7, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 8, Required = false)]
		public int? AcctIDSource {get; set;}
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 9, Required = false)]
		public int? AccountType {get; set;}
		
		[TagDetails(Tag = 81, Type = TagType.String, Offset = 10, Required = false)]
		public string? ProcessCode {get; set;}
		
		[TagDetails(Tag = 575, Type = TagType.Boolean, Offset = 11, Required = false)]
		public bool? OddLot {get; set;}
		
		[Component(Offset = 12, Required = false)]
		public ClrInstGrpComponent? ClrInstGrp {get; set;}
		
		[TagDetails(Tag = 578, Type = TagType.String, Offset = 13, Required = false)]
		public string? TradeInputSource {get; set;}
		
		[TagDetails(Tag = 579, Type = TagType.String, Offset = 14, Required = false)]
		public string? TradeInputDevice {get; set;}
		
		[TagDetails(Tag = 821, Type = TagType.String, Offset = 15, Required = false)]
		public string? OrderInputDevice {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 16, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 17, Required = false)]
		public string? ComplianceID {get; set;}
		
		[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 18, Required = false)]
		public bool? SolicitedFlag {get; set;}
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 19, Required = false)]
		public string? OrderCapacity {get; set;}
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 20, Required = false)]
		public string? OrderRestrictions {get; set;}
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 21, Required = false)]
		public int? CustOrderCapacity {get; set;}
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 22, Required = false)]
		public string? OrdType {get; set;}
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 23, Required = false)]
		public string? ExecInst {get; set;}
		
		[TagDetails(Tag = 483, Type = TagType.UtcTimestamp, Offset = 24, Required = false)]
		public DateTime? TransBkdTime {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 25, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 26, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		[TagDetails(Tag = 943, Type = TagType.String, Offset = 27, Required = false)]
		public string? TimeBracket {get; set;}
		
		[Component(Offset = 28, Required = false)]
		public CommissionDataComponent? CommissionData {get; set;}
		
		[TagDetails(Tag = 381, Type = TagType.Float, Offset = 29, Required = false)]
		public double? GrossTradeAmt {get; set;}
		
		[TagDetails(Tag = 157, Type = TagType.Int, Offset = 30, Required = false)]
		public int? NumDaysInterest {get; set;}
		
		[TagDetails(Tag = 230, Type = TagType.LocalDate, Offset = 31, Required = false)]
		public DateOnly? ExDate {get; set;}
		
		[TagDetails(Tag = 158, Type = TagType.Float, Offset = 32, Required = false)]
		public double? AccruedInterestRate {get; set;}
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 33, Required = false)]
		public double? AccruedInterestAmt {get; set;}
		
		[TagDetails(Tag = 738, Type = TagType.Float, Offset = 34, Required = false)]
		public double? InterestAtMaturity {get; set;}
		
		[TagDetails(Tag = 920, Type = TagType.Float, Offset = 35, Required = false)]
		public double? EndAccruedInterestAmt {get; set;}
		
		[TagDetails(Tag = 921, Type = TagType.Float, Offset = 36, Required = false)]
		public double? StartCash {get; set;}
		
		[TagDetails(Tag = 922, Type = TagType.Float, Offset = 37, Required = false)]
		public double? EndCash {get; set;}
		
		[TagDetails(Tag = 238, Type = TagType.Float, Offset = 38, Required = false)]
		public double? Concession {get; set;}
		
		[TagDetails(Tag = 237, Type = TagType.Float, Offset = 39, Required = false)]
		public double? TotalTakedown {get; set;}
		
		[TagDetails(Tag = 118, Type = TagType.Float, Offset = 40, Required = false)]
		public double? NetMoney {get; set;}
		
		[TagDetails(Tag = 119, Type = TagType.Float, Offset = 41, Required = false)]
		public double? SettlCurrAmt {get; set;}
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 42, Required = false)]
		public string? SettlCurrency {get; set;}
		
		[TagDetails(Tag = 155, Type = TagType.Float, Offset = 43, Required = false)]
		public double? SettlCurrFxRate {get; set;}
		
		[TagDetails(Tag = 156, Type = TagType.String, Offset = 44, Required = false)]
		public string? SettlCurrFxRateCalc {get; set;}
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 45, Required = false)]
		public string? PositionEffect {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 46, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 47, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 48, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[TagDetails(Tag = 752, Type = TagType.Int, Offset = 49, Required = false)]
		public int? SideMultiLegReportingType {get; set;}
		
		[Component(Offset = 50, Required = false)]
		public ContAmtGrpComponent? ContAmtGrp {get; set;}
		
		[Component(Offset = 51, Required = false)]
		public StipulationsComponent? Stipulations {get; set;}
		
		[Component(Offset = 52, Required = false)]
		public MiscFeesGrpComponent? MiscFeesGrp {get; set;}
		
		[TagDetails(Tag = 825, Type = TagType.String, Offset = 53, Required = false)]
		public string? ExchangeRule {get; set;}
		
		[TagDetails(Tag = 826, Type = TagType.Int, Offset = 54, Required = false)]
		public int? TradeAllocIndicator {get; set;}
		
		[TagDetails(Tag = 591, Type = TagType.String, Offset = 55, Required = false)]
		public string? PreallocMethod {get; set;}
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 56, Required = false)]
		public string? AllocID {get; set;}
		
		[Component(Offset = 57, Required = false)]
		public TrdAllocGrpComponent? TrdAllocGrp {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				Side is not null
				&& OrderID is not null;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Side is not null) writer.WriteString(54, Side);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (SecondaryOrderID is not null) writer.WriteString(198, SecondaryOrderID);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
			if (ListID is not null) writer.WriteString(66, ListID);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (ProcessCode is not null) writer.WriteString(81, ProcessCode);
			if (OddLot is not null) writer.WriteBoolean(575, OddLot.Value);
			if (ClrInstGrp is not null) ((IFixEncoder)ClrInstGrp).Encode(writer);
			if (TradeInputSource is not null) writer.WriteString(578, TradeInputSource);
			if (TradeInputDevice is not null) writer.WriteString(579, TradeInputDevice);
			if (OrderInputDevice is not null) writer.WriteString(821, OrderInputDevice);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (ComplianceID is not null) writer.WriteString(376, ComplianceID);
			if (SolicitedFlag is not null) writer.WriteBoolean(377, SolicitedFlag.Value);
			if (OrderCapacity is not null) writer.WriteString(528, OrderCapacity);
			if (OrderRestrictions is not null) writer.WriteString(529, OrderRestrictions);
			if (CustOrderCapacity is not null) writer.WriteWholeNumber(582, CustOrderCapacity.Value);
			if (OrdType is not null) writer.WriteString(40, OrdType);
			if (ExecInst is not null) writer.WriteString(18, ExecInst);
			if (TransBkdTime is not null) writer.WriteUtcTimeStamp(483, TransBkdTime.Value);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (TimeBracket is not null) writer.WriteString(943, TimeBracket);
			if (CommissionData is not null) ((IFixEncoder)CommissionData).Encode(writer);
			if (GrossTradeAmt is not null) writer.WriteNumber(381, GrossTradeAmt.Value);
			if (NumDaysInterest is not null) writer.WriteWholeNumber(157, NumDaysInterest.Value);
			if (ExDate is not null) writer.WriteLocalDateOnly(230, ExDate.Value);
			if (AccruedInterestRate is not null) writer.WriteNumber(158, AccruedInterestRate.Value);
			if (AccruedInterestAmt is not null) writer.WriteNumber(159, AccruedInterestAmt.Value);
			if (InterestAtMaturity is not null) writer.WriteNumber(738, InterestAtMaturity.Value);
			if (EndAccruedInterestAmt is not null) writer.WriteNumber(920, EndAccruedInterestAmt.Value);
			if (StartCash is not null) writer.WriteNumber(921, StartCash.Value);
			if (EndCash is not null) writer.WriteNumber(922, EndCash.Value);
			if (Concession is not null) writer.WriteNumber(238, Concession.Value);
			if (TotalTakedown is not null) writer.WriteNumber(237, TotalTakedown.Value);
			if (NetMoney is not null) writer.WriteNumber(118, NetMoney.Value);
			if (SettlCurrAmt is not null) writer.WriteNumber(119, SettlCurrAmt.Value);
			if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
			if (SettlCurrFxRate is not null) writer.WriteNumber(155, SettlCurrFxRate.Value);
			if (SettlCurrFxRateCalc is not null) writer.WriteString(156, SettlCurrFxRateCalc);
			if (PositionEffect is not null) writer.WriteString(77, PositionEffect);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (SideMultiLegReportingType is not null) writer.WriteWholeNumber(752, SideMultiLegReportingType.Value);
			if (ContAmtGrp is not null) ((IFixEncoder)ContAmtGrp).Encode(writer);
			if (Stipulations is not null) ((IFixEncoder)Stipulations).Encode(writer);
			if (MiscFeesGrp is not null) ((IFixEncoder)MiscFeesGrp).Encode(writer);
			if (ExchangeRule is not null) writer.WriteString(825, ExchangeRule);
			if (TradeAllocIndicator is not null) writer.WriteWholeNumber(826, TradeAllocIndicator.Value);
			if (PreallocMethod is not null) writer.WriteString(591, PreallocMethod);
			if (AllocID is not null) writer.WriteString(70, AllocID);
			if (TrdAllocGrp is not null) ((IFixEncoder)TrdAllocGrp).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			Side = view.GetString(54);
			OrderID = view.GetString(37);
			SecondaryOrderID = view.GetString(198);
			ClOrdID = view.GetString(11);
			SecondaryClOrdID = view.GetString(526);
			ListID = view.GetString(66);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				Parties = new();
				((IFixParser)Parties).Parse(viewParties);
			}
			Account = view.GetString(1);
			AcctIDSource = view.GetInt32(660);
			AccountType = view.GetInt32(581);
			ProcessCode = view.GetString(81);
			OddLot = view.GetBool(575);
			if (view.GetView("ClrInstGrp") is IMessageView viewClrInstGrp)
			{
				ClrInstGrp = new();
				((IFixParser)ClrInstGrp).Parse(viewClrInstGrp);
			}
			TradeInputSource = view.GetString(578);
			TradeInputDevice = view.GetString(579);
			OrderInputDevice = view.GetString(821);
			Currency = view.GetString(15);
			ComplianceID = view.GetString(376);
			SolicitedFlag = view.GetBool(377);
			OrderCapacity = view.GetString(528);
			OrderRestrictions = view.GetString(529);
			CustOrderCapacity = view.GetInt32(582);
			OrdType = view.GetString(40);
			ExecInst = view.GetString(18);
			TransBkdTime = view.GetDateTime(483);
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
			TimeBracket = view.GetString(943);
			if (view.GetView("CommissionData") is IMessageView viewCommissionData)
			{
				CommissionData = new();
				((IFixParser)CommissionData).Parse(viewCommissionData);
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
			Concession = view.GetDouble(238);
			TotalTakedown = view.GetDouble(237);
			NetMoney = view.GetDouble(118);
			SettlCurrAmt = view.GetDouble(119);
			SettlCurrency = view.GetString(120);
			SettlCurrFxRate = view.GetDouble(155);
			SettlCurrFxRateCalc = view.GetString(156);
			PositionEffect = view.GetString(77);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			SideMultiLegReportingType = view.GetInt32(752);
			if (view.GetView("ContAmtGrp") is IMessageView viewContAmtGrp)
			{
				ContAmtGrp = new();
				((IFixParser)ContAmtGrp).Parse(viewContAmtGrp);
			}
			if (view.GetView("Stipulations") is IMessageView viewStipulations)
			{
				Stipulations = new();
				((IFixParser)Stipulations).Parse(viewStipulations);
			}
			if (view.GetView("MiscFeesGrp") is IMessageView viewMiscFeesGrp)
			{
				MiscFeesGrp = new();
				((IFixParser)MiscFeesGrp).Parse(viewMiscFeesGrp);
			}
			ExchangeRule = view.GetString(825);
			TradeAllocIndicator = view.GetInt32(826);
			PreallocMethod = view.GetString(591);
			AllocID = view.GetString(70);
			if (view.GetView("TrdAllocGrp") is IMessageView viewTrdAllocGrp)
			{
				TrdAllocGrp = new();
				((IFixParser)TrdAllocGrp).Parse(viewTrdAllocGrp);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "Side":
					value = Side;
					break;
				case "OrderID":
					value = OrderID;
					break;
				case "SecondaryOrderID":
					value = SecondaryOrderID;
					break;
				case "ClOrdID":
					value = ClOrdID;
					break;
				case "SecondaryClOrdID":
					value = SecondaryClOrdID;
					break;
				case "ListID":
					value = ListID;
					break;
				case "Parties":
					value = Parties;
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
				case "ProcessCode":
					value = ProcessCode;
					break;
				case "OddLot":
					value = OddLot;
					break;
				case "ClrInstGrp":
					value = ClrInstGrp;
					break;
				case "TradeInputSource":
					value = TradeInputSource;
					break;
				case "TradeInputDevice":
					value = TradeInputDevice;
					break;
				case "OrderInputDevice":
					value = OrderInputDevice;
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
				case "OrderCapacity":
					value = OrderCapacity;
					break;
				case "OrderRestrictions":
					value = OrderRestrictions;
					break;
				case "CustOrderCapacity":
					value = CustOrderCapacity;
					break;
				case "OrdType":
					value = OrdType;
					break;
				case "ExecInst":
					value = ExecInst;
					break;
				case "TransBkdTime":
					value = TransBkdTime;
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
				case "CommissionData":
					value = CommissionData;
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
				case "PositionEffect":
					value = PositionEffect;
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
				case "SideMultiLegReportingType":
					value = SideMultiLegReportingType;
					break;
				case "ContAmtGrp":
					value = ContAmtGrp;
					break;
				case "Stipulations":
					value = Stipulations;
					break;
				case "MiscFeesGrp":
					value = MiscFeesGrp;
					break;
				case "ExchangeRule":
					value = ExchangeRule;
					break;
				case "TradeAllocIndicator":
					value = TradeAllocIndicator;
					break;
				case "PreallocMethod":
					value = PreallocMethod;
					break;
				case "AllocID":
					value = AllocID;
					break;
				case "TrdAllocGrp":
					value = TrdAllocGrp;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			Side = null;
			OrderID = null;
			SecondaryOrderID = null;
			ClOrdID = null;
			SecondaryClOrdID = null;
			ListID = null;
			((IFixReset?)Parties)?.Reset();
			Account = null;
			AcctIDSource = null;
			AccountType = null;
			ProcessCode = null;
			OddLot = null;
			((IFixReset?)ClrInstGrp)?.Reset();
			TradeInputSource = null;
			TradeInputDevice = null;
			OrderInputDevice = null;
			Currency = null;
			ComplianceID = null;
			SolicitedFlag = null;
			OrderCapacity = null;
			OrderRestrictions = null;
			CustOrderCapacity = null;
			OrdType = null;
			ExecInst = null;
			TransBkdTime = null;
			TradingSessionID = null;
			TradingSessionSubID = null;
			TimeBracket = null;
			((IFixReset?)CommissionData)?.Reset();
			GrossTradeAmt = null;
			NumDaysInterest = null;
			ExDate = null;
			AccruedInterestRate = null;
			AccruedInterestAmt = null;
			InterestAtMaturity = null;
			EndAccruedInterestAmt = null;
			StartCash = null;
			EndCash = null;
			Concession = null;
			TotalTakedown = null;
			NetMoney = null;
			SettlCurrAmt = null;
			SettlCurrency = null;
			SettlCurrFxRate = null;
			SettlCurrFxRateCalc = null;
			PositionEffect = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			SideMultiLegReportingType = null;
			((IFixReset?)ContAmtGrp)?.Reset();
			((IFixReset?)Stipulations)?.Reset();
			((IFixReset?)MiscFeesGrp)?.Reset();
			ExchangeRule = null;
			TradeAllocIndicator = null;
			PreallocMethod = null;
			AllocID = null;
			((IFixReset?)TrdAllocGrp)?.Reset();
		}
	}
}
