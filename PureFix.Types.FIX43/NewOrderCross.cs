using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX43.Components;

namespace PureFix.Types.FIX43
{
	[MessageType("s", FixVersion.FIX43)]
	public sealed partial class NewOrderCross : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 548, Type = TagType.String, Offset = 1, Required = true)]
		public string? CrossID {get; set;}
		
		[TagDetails(Tag = 549, Type = TagType.Int, Offset = 2, Required = true)]
		public int? CrossType {get; set;}
		
		[TagDetails(Tag = 550, Type = TagType.Int, Offset = 3, Required = true)]
		public int? CrossPrioritization {get; set;}
		
		public sealed partial class NoSides : IFixGroup
		{
			[TagDetails(Tag = 54, Type = TagType.String, Offset = 0, Required = true)]
			public string? Side {get; set;}
			
			[TagDetails(Tag = 11, Type = TagType.String, Offset = 1, Required = true)]
			public string? ClOrdID {get; set;}
			
			[TagDetails(Tag = 526, Type = TagType.String, Offset = 2, Required = false)]
			public string? SecondaryClOrdID {get; set;}
			
			[TagDetails(Tag = 583, Type = TagType.String, Offset = 3, Required = false)]
			public string? ClOrdLinkID {get; set;}
			
			[Component(Offset = 4, Required = false)]
			public Parties? Parties {get; set;}
			
			[TagDetails(Tag = 229, Type = TagType.String, Offset = 5, Required = false)]
			public string? TradeOriginationDate {get; set;}
			
			[TagDetails(Tag = 1, Type = TagType.String, Offset = 6, Required = false)]
			public string? Account {get; set;}
			
			[TagDetails(Tag = 581, Type = TagType.Int, Offset = 7, Required = false)]
			public int? AccountType {get; set;}
			
			[TagDetails(Tag = 589, Type = TagType.String, Offset = 8, Required = false)]
			public string? DayBookingInst {get; set;}
			
			[TagDetails(Tag = 590, Type = TagType.String, Offset = 9, Required = false)]
			public string? BookingUnit {get; set;}
			
			[TagDetails(Tag = 591, Type = TagType.String, Offset = 10, Required = false)]
			public string? PreallocMethod {get; set;}
			
			public sealed partial class NoAllocs : IFixGroup
			{
				[TagDetails(Tag = 79, Type = TagType.String, Offset = 0, Required = false)]
				public string? AllocAccount {get; set;}
				
				[TagDetails(Tag = 467, Type = TagType.String, Offset = 1, Required = false)]
				public string? IndividualAllocID {get; set;}
				
				[Component(Offset = 2, Required = false)]
				public NestedParties? NestedParties {get; set;}
				
				[TagDetails(Tag = 80, Type = TagType.Float, Offset = 3, Required = false)]
				public double? AllocQty {get; set;}
				
				
				bool IFixValidator.IsValid(in FixValidatorConfig config)
				{
					return true;
				}
				
				void IFixEncoder.Encode(IFixWriter writer)
				{
					if (AllocAccount is not null) writer.WriteString(79, AllocAccount);
					if (IndividualAllocID is not null) writer.WriteString(467, IndividualAllocID);
					if (NestedParties is not null) ((IFixEncoder)NestedParties).Encode(writer);
					if (AllocQty is not null) writer.WriteNumber(80, AllocQty.Value);
				}
				
				void IFixParser.Parse(IMessageView? view)
				{
					if (view is null) return;
					
					AllocAccount = view.GetString(79);
					IndividualAllocID = view.GetString(467);
					if (view.GetView("NestedParties") is IMessageView viewNestedParties)
					{
						NestedParties = new();
						((IFixParser)NestedParties).Parse(viewNestedParties);
					}
					AllocQty = view.GetDouble(80);
				}
				
				bool IFixLookup.TryGetByTag(string name, out object? value)
				{
					value = null;
					switch (name)
					{
						case "AllocAccount":
						{
							value = AllocAccount;
							break;
						}
						case "IndividualAllocID":
						{
							value = IndividualAllocID;
							break;
						}
						case "NestedParties":
						{
							value = NestedParties;
							break;
						}
						case "AllocQty":
						{
							value = AllocQty;
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
					AllocAccount = null;
					IndividualAllocID = null;
					((IFixReset?)NestedParties)?.Reset();
					AllocQty = null;
				}
			}
			[Group(NoOfTag = 78, Offset = 11, Required = false)]
			public NoAllocs[]? Allocs {get; set;}
			
			[TagDetails(Tag = 465, Type = TagType.Int, Offset = 12, Required = false)]
			public int? QuantityType {get; set;}
			
			[Component(Offset = 13, Required = true)]
			public OrderQtyData? OrderQtyData {get; set;}
			
			[Component(Offset = 14, Required = false)]
			public CommissionData? CommissionData {get; set;}
			
			[TagDetails(Tag = 528, Type = TagType.String, Offset = 15, Required = false)]
			public string? OrderCapacity {get; set;}
			
			[TagDetails(Tag = 529, Type = TagType.String, Offset = 16, Required = false)]
			public string? OrderRestrictions {get; set;}
			
			[TagDetails(Tag = 582, Type = TagType.Int, Offset = 17, Required = false)]
			public int? CustOrderCapacity {get; set;}
			
			[TagDetails(Tag = 121, Type = TagType.Boolean, Offset = 18, Required = false)]
			public bool? ForexReq {get; set;}
			
			[TagDetails(Tag = 120, Type = TagType.String, Offset = 19, Required = false)]
			public string? SettlCurrency {get; set;}
			
			[TagDetails(Tag = 58, Type = TagType.String, Offset = 20, Required = false)]
			public string? Text {get; set;}
			
			[TagDetails(Tag = 354, Type = TagType.Length, Offset = 21, Required = false)]
			public int? EncodedTextLen {get; set;}
			
			[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 22, Required = false)]
			public byte[]? EncodedText {get; set;}
			
			[TagDetails(Tag = 77, Type = TagType.String, Offset = 23, Required = false)]
			public string? PositionEffect {get; set;}
			
			[TagDetails(Tag = 203, Type = TagType.Int, Offset = 24, Required = false)]
			public int? CoveredOrUncovered {get; set;}
			
			[TagDetails(Tag = 544, Type = TagType.String, Offset = 25, Required = false)]
			public string? CashMargin {get; set;}
			
			[TagDetails(Tag = 635, Type = TagType.String, Offset = 26, Required = false)]
			public string? ClearingFeeIndicator {get; set;}
			
			[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 27, Required = false)]
			public bool? SolicitedFlag {get; set;}
			
			[TagDetails(Tag = 659, Type = TagType.String, Offset = 28, Required = false)]
			public string? SideComplianceID {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (Side is not null) writer.WriteString(54, Side);
				if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
				if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
				if (ClOrdLinkID is not null) writer.WriteString(583, ClOrdLinkID);
				if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
				if (TradeOriginationDate is not null) writer.WriteString(229, TradeOriginationDate);
				if (Account is not null) writer.WriteString(1, Account);
				if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
				if (DayBookingInst is not null) writer.WriteString(589, DayBookingInst);
				if (BookingUnit is not null) writer.WriteString(590, BookingUnit);
				if (PreallocMethod is not null) writer.WriteString(591, PreallocMethod);
				if (Allocs is not null && Allocs.Length != 0)
				{
					writer.WriteWholeNumber(78, Allocs.Length);
					for (int i = 0; i < Allocs.Length; i++)
					{
						((IFixEncoder)Allocs[i]).Encode(writer);
					}
				}
				if (QuantityType is not null) writer.WriteWholeNumber(465, QuantityType.Value);
				if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
				if (CommissionData is not null) ((IFixEncoder)CommissionData).Encode(writer);
				if (OrderCapacity is not null) writer.WriteString(528, OrderCapacity);
				if (OrderRestrictions is not null) writer.WriteString(529, OrderRestrictions);
				if (CustOrderCapacity is not null) writer.WriteWholeNumber(582, CustOrderCapacity.Value);
				if (ForexReq is not null) writer.WriteBoolean(121, ForexReq.Value);
				if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
				if (Text is not null) writer.WriteString(58, Text);
				if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
				if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
				if (PositionEffect is not null) writer.WriteString(77, PositionEffect);
				if (CoveredOrUncovered is not null) writer.WriteWholeNumber(203, CoveredOrUncovered.Value);
				if (CashMargin is not null) writer.WriteString(544, CashMargin);
				if (ClearingFeeIndicator is not null) writer.WriteString(635, ClearingFeeIndicator);
				if (SolicitedFlag is not null) writer.WriteBoolean(377, SolicitedFlag.Value);
				if (SideComplianceID is not null) writer.WriteString(659, SideComplianceID);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				Side = view.GetString(54);
				ClOrdID = view.GetString(11);
				SecondaryClOrdID = view.GetString(526);
				ClOrdLinkID = view.GetString(583);
				if (view.GetView("Parties") is IMessageView viewParties)
				{
					Parties = new();
					((IFixParser)Parties).Parse(viewParties);
				}
				TradeOriginationDate = view.GetString(229);
				Account = view.GetString(1);
				AccountType = view.GetInt32(581);
				DayBookingInst = view.GetString(589);
				BookingUnit = view.GetString(590);
				PreallocMethod = view.GetString(591);
				if (view.GetView("NoAllocs") is IMessageView viewNoAllocs)
				{
					var count = viewNoAllocs.GroupCount();
					Allocs = new NoAllocs[count];
					for (int i = 0; i < count; i++)
					{
						Allocs[i] = new();
						((IFixParser)Allocs[i]).Parse(viewNoAllocs.GetGroupInstance(i));
					}
				}
				QuantityType = view.GetInt32(465);
				if (view.GetView("OrderQtyData") is IMessageView viewOrderQtyData)
				{
					OrderQtyData = new();
					((IFixParser)OrderQtyData).Parse(viewOrderQtyData);
				}
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
				CashMargin = view.GetString(544);
				ClearingFeeIndicator = view.GetString(635);
				SolicitedFlag = view.GetBool(377);
				SideComplianceID = view.GetString(659);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "Side":
					{
						value = Side;
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
					case "Account":
					{
						value = Account;
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
					case "NoAllocs":
					{
						value = Allocs;
						break;
					}
					case "QuantityType":
					{
						value = QuantityType;
						break;
					}
					case "OrderQtyData":
					{
						value = OrderQtyData;
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
					case "SolicitedFlag":
					{
						value = SolicitedFlag;
						break;
					}
					case "SideComplianceID":
					{
						value = SideComplianceID;
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
				Side = null;
				ClOrdID = null;
				SecondaryClOrdID = null;
				ClOrdLinkID = null;
				((IFixReset?)Parties)?.Reset();
				TradeOriginationDate = null;
				Account = null;
				AccountType = null;
				DayBookingInst = null;
				BookingUnit = null;
				PreallocMethod = null;
				Allocs = null;
				QuantityType = null;
				((IFixReset?)OrderQtyData)?.Reset();
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
				CashMargin = null;
				ClearingFeeIndicator = null;
				SolicitedFlag = null;
				SideComplianceID = null;
			}
		}
		[Group(NoOfTag = 552, Offset = 4, Required = true)]
		public NoSides[]? Sides {get; set;}
		
		[Component(Offset = 5, Required = true)]
		public Instrument? Instrument {get; set;}
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 6, Required = false)]
		public string? SettlmntTyp {get; set;}
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 7, Required = false)]
		public DateOnly? FutSettDate {get; set;}
		
		[TagDetails(Tag = 21, Type = TagType.String, Offset = 8, Required = true)]
		public string? HandlInst {get; set;}
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 9, Required = false)]
		public string? ExecInst {get; set;}
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 10, Required = false)]
		public double? MinQty {get; set;}
		
		[TagDetails(Tag = 111, Type = TagType.Float, Offset = 11, Required = false)]
		public double? MaxFloor {get; set;}
		
		[TagDetails(Tag = 100, Type = TagType.String, Offset = 12, Required = false)]
		public string? ExDestination {get; set;}
		
		public sealed partial class NoTradingSessions : IFixGroup
		{
			[TagDetails(Tag = 336, Type = TagType.String, Offset = 0, Required = false)]
			public string? TradingSessionID {get; set;}
			
			[TagDetails(Tag = 625, Type = TagType.String, Offset = 1, Required = false)]
			public string? TradingSessionSubID {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
				if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				TradingSessionID = view.GetString(336);
				TradingSessionSubID = view.GetString(625);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "TradingSessionID":
					{
						value = TradingSessionID;
						break;
					}
					case "TradingSessionSubID":
					{
						value = TradingSessionSubID;
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
				TradingSessionID = null;
				TradingSessionSubID = null;
			}
		}
		[Group(NoOfTag = 386, Offset = 13, Required = false)]
		public NoTradingSessions[]? TradingSessions {get; set;}
		
		[TagDetails(Tag = 81, Type = TagType.String, Offset = 14, Required = false)]
		public string? ProcessCode {get; set;}
		
		[TagDetails(Tag = 140, Type = TagType.Float, Offset = 15, Required = false)]
		public double? PrevClosePx {get; set;}
		
		[TagDetails(Tag = 114, Type = TagType.Boolean, Offset = 16, Required = false)]
		public bool? LocateReqd {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 17, Required = true)]
		public DateTime? TransactTime {get; set;}
		
		[Component(Offset = 18, Required = false)]
		public Stipulations? Stipulations {get; set;}
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 19, Required = true)]
		public string? OrdType {get; set;}
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 20, Required = false)]
		public int? PriceType {get; set;}
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 21, Required = false)]
		public double? Price {get; set;}
		
		[TagDetails(Tag = 99, Type = TagType.Float, Offset = 22, Required = false)]
		public double? StopPx {get; set;}
		
		[Component(Offset = 23, Required = false)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData {get; set;}
		
		[Component(Offset = 24, Required = false)]
		public YieldData? YieldData {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 25, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 26, Required = false)]
		public string? ComplianceID {get; set;}
		
		[TagDetails(Tag = 23, Type = TagType.String, Offset = 27, Required = false)]
		public string? IOIid {get; set;}
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 28, Required = false)]
		public string? QuoteID {get; set;}
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 29, Required = false)]
		public string? TimeInForce {get; set;}
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 30, Required = false)]
		public DateTime? EffectiveTime {get; set;}
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 31, Required = false)]
		public DateOnly? ExpireDate {get; set;}
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 32, Required = false)]
		public DateTime? ExpireTime {get; set;}
		
		[TagDetails(Tag = 427, Type = TagType.Int, Offset = 33, Required = false)]
		public int? GTBookingInst {get; set;}
		
		[TagDetails(Tag = 210, Type = TagType.Float, Offset = 34, Required = false)]
		public double? MaxShow {get; set;}
		
		[TagDetails(Tag = 211, Type = TagType.Float, Offset = 35, Required = false)]
		public double? PegDifference {get; set;}
		
		[TagDetails(Tag = 388, Type = TagType.String, Offset = 36, Required = false)]
		public string? DiscretionInst {get; set;}
		
		[TagDetails(Tag = 389, Type = TagType.Float, Offset = 37, Required = false)]
		public double? DiscretionOffset {get; set;}
		
		[TagDetails(Tag = 480, Type = TagType.String, Offset = 38, Required = false)]
		public string? CancellationRights {get; set;}
		
		[TagDetails(Tag = 481, Type = TagType.String, Offset = 39, Required = false)]
		public string? MoneyLaunderingStatus {get; set;}
		
		[TagDetails(Tag = 513, Type = TagType.String, Offset = 40, Required = false)]
		public string? RegistID {get; set;}
		
		[TagDetails(Tag = 494, Type = TagType.String, Offset = 41, Required = false)]
		public string? Designation {get; set;}
		
		[TagDetails(Tag = 158, Type = TagType.Float, Offset = 42, Required = false)]
		public double? AccruedInterestRate {get; set;}
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 43, Required = false)]
		public double? AccruedInterestAmt {get; set;}
		
		[TagDetails(Tag = 118, Type = TagType.Float, Offset = 44, Required = false)]
		public double? NetMoney {get; set;}
		
		[Component(Offset = 45, Required = true)]
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
			if (CrossID is not null) writer.WriteString(548, CrossID);
			if (CrossType is not null) writer.WriteWholeNumber(549, CrossType.Value);
			if (CrossPrioritization is not null) writer.WriteWholeNumber(550, CrossPrioritization.Value);
			if (Sides is not null && Sides.Length != 0)
			{
				writer.WriteWholeNumber(552, Sides.Length);
				for (int i = 0; i < Sides.Length; i++)
				{
					((IFixEncoder)Sides[i]).Encode(writer);
				}
			}
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (SettlmntTyp is not null) writer.WriteString(63, SettlmntTyp);
			if (FutSettDate is not null) writer.WriteLocalDateOnly(64, FutSettDate.Value);
			if (HandlInst is not null) writer.WriteString(21, HandlInst);
			if (ExecInst is not null) writer.WriteString(18, ExecInst);
			if (MinQty is not null) writer.WriteNumber(110, MinQty.Value);
			if (MaxFloor is not null) writer.WriteNumber(111, MaxFloor.Value);
			if (ExDestination is not null) writer.WriteString(100, ExDestination);
			if (TradingSessions is not null && TradingSessions.Length != 0)
			{
				writer.WriteWholeNumber(386, TradingSessions.Length);
				for (int i = 0; i < TradingSessions.Length; i++)
				{
					((IFixEncoder)TradingSessions[i]).Encode(writer);
				}
			}
			if (ProcessCode is not null) writer.WriteString(81, ProcessCode);
			if (PrevClosePx is not null) writer.WriteNumber(140, PrevClosePx.Value);
			if (LocateReqd is not null) writer.WriteBoolean(114, LocateReqd.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (Stipulations is not null) ((IFixEncoder)Stipulations).Encode(writer);
			if (OrdType is not null) writer.WriteString(40, OrdType);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (StopPx is not null) writer.WriteNumber(99, StopPx.Value);
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (YieldData is not null) ((IFixEncoder)YieldData).Encode(writer);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (ComplianceID is not null) writer.WriteString(376, ComplianceID);
			if (IOIid is not null) writer.WriteString(23, IOIid);
			if (QuoteID is not null) writer.WriteString(117, QuoteID);
			if (TimeInForce is not null) writer.WriteString(59, TimeInForce);
			if (EffectiveTime is not null) writer.WriteUtcTimeStamp(168, EffectiveTime.Value);
			if (ExpireDate is not null) writer.WriteLocalDateOnly(432, ExpireDate.Value);
			if (ExpireTime is not null) writer.WriteUtcTimeStamp(126, ExpireTime.Value);
			if (GTBookingInst is not null) writer.WriteWholeNumber(427, GTBookingInst.Value);
			if (MaxShow is not null) writer.WriteNumber(210, MaxShow.Value);
			if (PegDifference is not null) writer.WriteNumber(211, PegDifference.Value);
			if (DiscretionInst is not null) writer.WriteString(388, DiscretionInst);
			if (DiscretionOffset is not null) writer.WriteNumber(389, DiscretionOffset.Value);
			if (CancellationRights is not null) writer.WriteString(480, CancellationRights);
			if (MoneyLaunderingStatus is not null) writer.WriteString(481, MoneyLaunderingStatus);
			if (RegistID is not null) writer.WriteString(513, RegistID);
			if (Designation is not null) writer.WriteString(494, Designation);
			if (AccruedInterestRate is not null) writer.WriteNumber(158, AccruedInterestRate.Value);
			if (AccruedInterestAmt is not null) writer.WriteNumber(159, AccruedInterestAmt.Value);
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
			CrossID = view.GetString(548);
			CrossType = view.GetInt32(549);
			CrossPrioritization = view.GetInt32(550);
			if (view.GetView("NoSides") is IMessageView viewNoSides)
			{
				var count = viewNoSides.GroupCount();
				Sides = new NoSides[count];
				for (int i = 0; i < count; i++)
				{
					Sides[i] = new();
					((IFixParser)Sides[i]).Parse(viewNoSides.GetGroupInstance(i));
				}
			}
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			SettlmntTyp = view.GetString(63);
			FutSettDate = view.GetDateOnly(64);
			HandlInst = view.GetString(21);
			ExecInst = view.GetString(18);
			MinQty = view.GetDouble(110);
			MaxFloor = view.GetDouble(111);
			ExDestination = view.GetString(100);
			if (view.GetView("NoTradingSessions") is IMessageView viewNoTradingSessions)
			{
				var count = viewNoTradingSessions.GroupCount();
				TradingSessions = new NoTradingSessions[count];
				for (int i = 0; i < count; i++)
				{
					TradingSessions[i] = new();
					((IFixParser)TradingSessions[i]).Parse(viewNoTradingSessions.GetGroupInstance(i));
				}
			}
			ProcessCode = view.GetString(81);
			PrevClosePx = view.GetDouble(140);
			LocateReqd = view.GetBool(114);
			TransactTime = view.GetDateTime(60);
			if (view.GetView("Stipulations") is IMessageView viewStipulations)
			{
				Stipulations = new();
				((IFixParser)Stipulations).Parse(viewStipulations);
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
			IOIid = view.GetString(23);
			QuoteID = view.GetString(117);
			TimeInForce = view.GetString(59);
			EffectiveTime = view.GetDateTime(168);
			ExpireDate = view.GetDateOnly(432);
			ExpireTime = view.GetDateTime(126);
			GTBookingInst = view.GetInt32(427);
			MaxShow = view.GetDouble(210);
			PegDifference = view.GetDouble(211);
			DiscretionInst = view.GetString(388);
			DiscretionOffset = view.GetDouble(389);
			CancellationRights = view.GetString(480);
			MoneyLaunderingStatus = view.GetString(481);
			RegistID = view.GetString(513);
			Designation = view.GetString(494);
			AccruedInterestRate = view.GetDouble(158);
			AccruedInterestAmt = view.GetDouble(159);
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
				{
					value = StandardHeader;
					break;
				}
				case "CrossID":
				{
					value = CrossID;
					break;
				}
				case "CrossType":
				{
					value = CrossType;
					break;
				}
				case "CrossPrioritization":
				{
					value = CrossPrioritization;
					break;
				}
				case "NoSides":
				{
					value = Sides;
					break;
				}
				case "Instrument":
				{
					value = Instrument;
					break;
				}
				case "SettlmntTyp":
				{
					value = SettlmntTyp;
					break;
				}
				case "FutSettDate":
				{
					value = FutSettDate;
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
				case "NoTradingSessions":
				{
					value = TradingSessions;
					break;
				}
				case "ProcessCode":
				{
					value = ProcessCode;
					break;
				}
				case "PrevClosePx":
				{
					value = PrevClosePx;
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
				case "IOIid":
				{
					value = IOIid;
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
				case "MaxShow":
				{
					value = MaxShow;
					break;
				}
				case "PegDifference":
				{
					value = PegDifference;
					break;
				}
				case "DiscretionInst":
				{
					value = DiscretionInst;
					break;
				}
				case "DiscretionOffset":
				{
					value = DiscretionOffset;
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
				case "AccruedInterestRate":
				{
					value = AccruedInterestRate;
					break;
				}
				case "AccruedInterestAmt":
				{
					value = AccruedInterestAmt;
					break;
				}
				case "NetMoney":
				{
					value = NetMoney;
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
			CrossID = null;
			CrossType = null;
			CrossPrioritization = null;
			Sides = null;
			((IFixReset?)Instrument)?.Reset();
			SettlmntTyp = null;
			FutSettDate = null;
			HandlInst = null;
			ExecInst = null;
			MinQty = null;
			MaxFloor = null;
			ExDestination = null;
			TradingSessions = null;
			ProcessCode = null;
			PrevClosePx = null;
			LocateReqd = null;
			TransactTime = null;
			((IFixReset?)Stipulations)?.Reset();
			OrdType = null;
			PriceType = null;
			Price = null;
			StopPx = null;
			((IFixReset?)SpreadOrBenchmarkCurveData)?.Reset();
			((IFixReset?)YieldData)?.Reset();
			Currency = null;
			ComplianceID = null;
			IOIid = null;
			QuoteID = null;
			TimeInForce = null;
			EffectiveTime = null;
			ExpireDate = null;
			ExpireTime = null;
			GTBookingInst = null;
			MaxShow = null;
			PegDifference = null;
			DiscretionInst = null;
			DiscretionOffset = null;
			CancellationRights = null;
			MoneyLaunderingStatus = null;
			RegistID = null;
			Designation = null;
			AccruedInterestRate = null;
			AccruedInterestAmt = null;
			NetMoney = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
