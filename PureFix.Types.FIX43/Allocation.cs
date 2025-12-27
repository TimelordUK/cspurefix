using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX43.Components;

namespace PureFix.Types.FIX43
{
	[MessageType("J", FixVersion.FIX43)]
	public sealed partial class Allocation : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 1, Required = true)]
		public string? AllocID {get; set;}
		
		[TagDetails(Tag = 71, Type = TagType.String, Offset = 2, Required = true)]
		public string? AllocTransType {get; set;}
		
		[TagDetails(Tag = 626, Type = TagType.Int, Offset = 3, Required = true)]
		public int? AllocType {get; set;}
		
		[TagDetails(Tag = 72, Type = TagType.String, Offset = 4, Required = false)]
		public string? RefAllocID {get; set;}
		
		[TagDetails(Tag = 196, Type = TagType.String, Offset = 5, Required = false)]
		public string? AllocLinkID {get; set;}
		
		[TagDetails(Tag = 197, Type = TagType.Int, Offset = 6, Required = false)]
		public int? AllocLinkType {get; set;}
		
		[TagDetails(Tag = 466, Type = TagType.String, Offset = 7, Required = false)]
		public string? BookingRefID {get; set;}
		
		public sealed partial class NoOrders : IFixGroup
		{
			[TagDetails(Tag = 11, Type = TagType.String, Offset = 0, Required = false)]
			public string? ClOrdID {get; set;}
			
			[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = false)]
			public string? OrderID {get; set;}
			
			[TagDetails(Tag = 198, Type = TagType.String, Offset = 2, Required = false)]
			public string? SecondaryOrderID {get; set;}
			
			[TagDetails(Tag = 526, Type = TagType.String, Offset = 3, Required = false)]
			public string? SecondaryClOrdID {get; set;}
			
			[TagDetails(Tag = 66, Type = TagType.String, Offset = 4, Required = false)]
			public string? ListID {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
				if (OrderID is not null) writer.WriteString(37, OrderID);
				if (SecondaryOrderID is not null) writer.WriteString(198, SecondaryOrderID);
				if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
				if (ListID is not null) writer.WriteString(66, ListID);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				ClOrdID = view.GetString(11);
				OrderID = view.GetString(37);
				SecondaryOrderID = view.GetString(198);
				SecondaryClOrdID = view.GetString(526);
				ListID = view.GetString(66);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "ClOrdID":
					{
						value = ClOrdID;
						break;
					}
					case "OrderID":
					{
						value = OrderID;
						break;
					}
					case "SecondaryOrderID":
					{
						value = SecondaryOrderID;
						break;
					}
					case "SecondaryClOrdID":
					{
						value = SecondaryClOrdID;
						break;
					}
					case "ListID":
					{
						value = ListID;
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
				ClOrdID = null;
				OrderID = null;
				SecondaryOrderID = null;
				SecondaryClOrdID = null;
				ListID = null;
			}
		}
		[Group(NoOfTag = 73, Offset = 8, Required = false)]
		public NoOrders[]? Orders {get; set;}
		
		public sealed partial class NoExecs : IFixGroup
		{
			[TagDetails(Tag = 32, Type = TagType.Float, Offset = 0, Required = false)]
			public double? LastQty {get; set;}
			
			[TagDetails(Tag = 17, Type = TagType.String, Offset = 1, Required = false)]
			public string? ExecID {get; set;}
			
			[TagDetails(Tag = 527, Type = TagType.String, Offset = 2, Required = false)]
			public string? SecondaryExecID {get; set;}
			
			[TagDetails(Tag = 31, Type = TagType.Float, Offset = 3, Required = false)]
			public double? LastPx {get; set;}
			
			[TagDetails(Tag = 29, Type = TagType.String, Offset = 4, Required = false)]
			public string? LastCapacity {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (LastQty is not null) writer.WriteNumber(32, LastQty.Value);
				if (ExecID is not null) writer.WriteString(17, ExecID);
				if (SecondaryExecID is not null) writer.WriteString(527, SecondaryExecID);
				if (LastPx is not null) writer.WriteNumber(31, LastPx.Value);
				if (LastCapacity is not null) writer.WriteString(29, LastCapacity);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				LastQty = view.GetDouble(32);
				ExecID = view.GetString(17);
				SecondaryExecID = view.GetString(527);
				LastPx = view.GetDouble(31);
				LastCapacity = view.GetString(29);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "LastQty":
					{
						value = LastQty;
						break;
					}
					case "ExecID":
					{
						value = ExecID;
						break;
					}
					case "SecondaryExecID":
					{
						value = SecondaryExecID;
						break;
					}
					case "LastPx":
					{
						value = LastPx;
						break;
					}
					case "LastCapacity":
					{
						value = LastCapacity;
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
				LastQty = null;
				ExecID = null;
				SecondaryExecID = null;
				LastPx = null;
				LastCapacity = null;
			}
		}
		[Group(NoOfTag = 124, Offset = 9, Required = false)]
		public NoExecs[]? Execs {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 10, Required = true)]
		public string? Side {get; set;}
		
		[Component(Offset = 11, Required = true)]
		public Instrument? Instrument {get; set;}
		
		[TagDetails(Tag = 53, Type = TagType.Float, Offset = 12, Required = true)]
		public double? Quantity {get; set;}
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 13, Required = false)]
		public string? LastMkt {get; set;}
		
		[TagDetails(Tag = 229, Type = TagType.String, Offset = 14, Required = false)]
		public string? TradeOriginationDate {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 15, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 16, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 17, Required = false)]
		public int? PriceType {get; set;}
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 18, Required = true)]
		public double? AvgPx {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 19, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 74, Type = TagType.Int, Offset = 20, Required = false)]
		public int? AvgPrxPrecision {get; set;}
		
		[Component(Offset = 21, Required = false)]
		public Parties? Parties {get; set;}
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 22, Required = true)]
		public DateOnly? TradeDate {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 23, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 24, Required = false)]
		public string? SettlmntTyp {get; set;}
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 25, Required = false)]
		public DateOnly? FutSettDate {get; set;}
		
		[TagDetails(Tag = 381, Type = TagType.Float, Offset = 26, Required = false)]
		public double? GrossTradeAmt {get; set;}
		
		[TagDetails(Tag = 238, Type = TagType.Float, Offset = 27, Required = false)]
		public double? Concession {get; set;}
		
		[TagDetails(Tag = 237, Type = TagType.Float, Offset = 28, Required = false)]
		public double? TotalTakedown {get; set;}
		
		[TagDetails(Tag = 118, Type = TagType.Float, Offset = 29, Required = false)]
		public double? NetMoney {get; set;}
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 30, Required = false)]
		public string? PositionEffect {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 31, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 32, Required = false)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 33, Required = false)]
		public byte[]? EncodedText {get; set;}
		
		[TagDetails(Tag = 157, Type = TagType.Int, Offset = 34, Required = false)]
		public int? NumDaysInterest {get; set;}
		
		[TagDetails(Tag = 158, Type = TagType.Float, Offset = 35, Required = false)]
		public double? AccruedInterestRate {get; set;}
		
		[TagDetails(Tag = 540, Type = TagType.Float, Offset = 36, Required = false)]
		public double? TotalAccruedInterestAmt {get; set;}
		
		[TagDetails(Tag = 650, Type = TagType.Boolean, Offset = 37, Required = false)]
		public bool? LegalConfirm {get; set;}
		
		public sealed partial class NoAllocs : IFixGroup
		{
			[TagDetails(Tag = 79, Type = TagType.String, Offset = 0, Required = false)]
			public string? AllocAccount {get; set;}
			
			[TagDetails(Tag = 366, Type = TagType.Float, Offset = 1, Required = false)]
			public double? AllocPrice {get; set;}
			
			[TagDetails(Tag = 80, Type = TagType.Float, Offset = 2, Required = false)]
			public double? AllocQty {get; set;}
			
			[TagDetails(Tag = 467, Type = TagType.String, Offset = 3, Required = false)]
			public string? IndividualAllocID {get; set;}
			
			[TagDetails(Tag = 81, Type = TagType.String, Offset = 4, Required = false)]
			public string? ProcessCode {get; set;}
			
			[Component(Offset = 5, Required = false)]
			public NestedParties? NestedParties {get; set;}
			
			[TagDetails(Tag = 208, Type = TagType.Boolean, Offset = 6, Required = false)]
			public bool? NotifyBrokerOfCredit {get; set;}
			
			[TagDetails(Tag = 209, Type = TagType.Int, Offset = 7, Required = false)]
			public int? AllocHandlInst {get; set;}
			
			[TagDetails(Tag = 161, Type = TagType.String, Offset = 8, Required = false)]
			public string? AllocText {get; set;}
			
			[TagDetails(Tag = 360, Type = TagType.Length, Offset = 9, Required = false)]
			public int? EncodedAllocTextLen {get; set;}
			
			[TagDetails(Tag = 361, Type = TagType.RawData, Offset = 10, Required = false)]
			public byte[]? EncodedAllocText {get; set;}
			
			[Component(Offset = 11, Required = false)]
			public CommissionData? CommissionData {get; set;}
			
			[TagDetails(Tag = 153, Type = TagType.Float, Offset = 12, Required = false)]
			public double? AllocAvgPx {get; set;}
			
			[TagDetails(Tag = 154, Type = TagType.Float, Offset = 13, Required = false)]
			public double? AllocNetMoney {get; set;}
			
			[TagDetails(Tag = 119, Type = TagType.Float, Offset = 14, Required = false)]
			public double? SettlCurrAmt {get; set;}
			
			[TagDetails(Tag = 120, Type = TagType.String, Offset = 15, Required = false)]
			public string? SettlCurrency {get; set;}
			
			[TagDetails(Tag = 155, Type = TagType.Float, Offset = 16, Required = false)]
			public double? SettlCurrFxRate {get; set;}
			
			[TagDetails(Tag = 156, Type = TagType.String, Offset = 17, Required = false)]
			public string? SettlCurrFxRateCalc {get; set;}
			
			[TagDetails(Tag = 159, Type = TagType.Float, Offset = 18, Required = false)]
			public double? AccruedInterestAmt {get; set;}
			
			[TagDetails(Tag = 160, Type = TagType.String, Offset = 19, Required = false)]
			public string? SettlInstMode {get; set;}
			
			public sealed partial class NoMiscFees : IFixGroup
			{
				[TagDetails(Tag = 137, Type = TagType.Float, Offset = 0, Required = false)]
				public double? MiscFeeAmt {get; set;}
				
				[TagDetails(Tag = 138, Type = TagType.String, Offset = 1, Required = false)]
				public string? MiscFeeCurr {get; set;}
				
				[TagDetails(Tag = 139, Type = TagType.String, Offset = 2, Required = false)]
				public string? MiscFeeType {get; set;}
				
				
				bool IFixValidator.IsValid(in FixValidatorConfig config)
				{
					return true;
				}
				
				void IFixEncoder.Encode(IFixWriter writer)
				{
					if (MiscFeeAmt is not null) writer.WriteNumber(137, MiscFeeAmt.Value);
					if (MiscFeeCurr is not null) writer.WriteString(138, MiscFeeCurr);
					if (MiscFeeType is not null) writer.WriteString(139, MiscFeeType);
				}
				
				void IFixParser.Parse(IMessageView? view)
				{
					if (view is null) return;
					
					MiscFeeAmt = view.GetDouble(137);
					MiscFeeCurr = view.GetString(138);
					MiscFeeType = view.GetString(139);
				}
				
				bool IFixLookup.TryGetByTag(string name, out object? value)
				{
					value = null;
					switch (name)
					{
						case "MiscFeeAmt":
						{
							value = MiscFeeAmt;
							break;
						}
						case "MiscFeeCurr":
						{
							value = MiscFeeCurr;
							break;
						}
						case "MiscFeeType":
						{
							value = MiscFeeType;
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
					MiscFeeAmt = null;
					MiscFeeCurr = null;
					MiscFeeType = null;
				}
			}
			[Group(NoOfTag = 136, Offset = 20, Required = false)]
			public NoMiscFees[]? MiscFees {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (AllocAccount is not null) writer.WriteString(79, AllocAccount);
				if (AllocPrice is not null) writer.WriteNumber(366, AllocPrice.Value);
				if (AllocQty is not null) writer.WriteNumber(80, AllocQty.Value);
				if (IndividualAllocID is not null) writer.WriteString(467, IndividualAllocID);
				if (ProcessCode is not null) writer.WriteString(81, ProcessCode);
				if (NestedParties is not null) ((IFixEncoder)NestedParties).Encode(writer);
				if (NotifyBrokerOfCredit is not null) writer.WriteBoolean(208, NotifyBrokerOfCredit.Value);
				if (AllocHandlInst is not null) writer.WriteWholeNumber(209, AllocHandlInst.Value);
				if (AllocText is not null) writer.WriteString(161, AllocText);
				if (EncodedAllocTextLen is not null) writer.WriteWholeNumber(360, EncodedAllocTextLen.Value);
				if (EncodedAllocText is not null) writer.WriteBuffer(361, EncodedAllocText);
				if (CommissionData is not null) ((IFixEncoder)CommissionData).Encode(writer);
				if (AllocAvgPx is not null) writer.WriteNumber(153, AllocAvgPx.Value);
				if (AllocNetMoney is not null) writer.WriteNumber(154, AllocNetMoney.Value);
				if (SettlCurrAmt is not null) writer.WriteNumber(119, SettlCurrAmt.Value);
				if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
				if (SettlCurrFxRate is not null) writer.WriteNumber(155, SettlCurrFxRate.Value);
				if (SettlCurrFxRateCalc is not null) writer.WriteString(156, SettlCurrFxRateCalc);
				if (AccruedInterestAmt is not null) writer.WriteNumber(159, AccruedInterestAmt.Value);
				if (SettlInstMode is not null) writer.WriteString(160, SettlInstMode);
				if (MiscFees is not null && MiscFees.Length != 0)
				{
					writer.WriteWholeNumber(136, MiscFees.Length);
					for (int i = 0; i < MiscFees.Length; i++)
					{
						((IFixEncoder)MiscFees[i]).Encode(writer);
					}
				}
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				AllocAccount = view.GetString(79);
				AllocPrice = view.GetDouble(366);
				AllocQty = view.GetDouble(80);
				IndividualAllocID = view.GetString(467);
				ProcessCode = view.GetString(81);
				if (view.GetView("NestedParties") is IMessageView viewNestedParties)
				{
					NestedParties = new();
					((IFixParser)NestedParties).Parse(viewNestedParties);
				}
				NotifyBrokerOfCredit = view.GetBool(208);
				AllocHandlInst = view.GetInt32(209);
				AllocText = view.GetString(161);
				EncodedAllocTextLen = view.GetInt32(360);
				EncodedAllocText = view.GetByteArray(361);
				if (view.GetView("CommissionData") is IMessageView viewCommissionData)
				{
					CommissionData = new();
					((IFixParser)CommissionData).Parse(viewCommissionData);
				}
				AllocAvgPx = view.GetDouble(153);
				AllocNetMoney = view.GetDouble(154);
				SettlCurrAmt = view.GetDouble(119);
				SettlCurrency = view.GetString(120);
				SettlCurrFxRate = view.GetDouble(155);
				SettlCurrFxRateCalc = view.GetString(156);
				AccruedInterestAmt = view.GetDouble(159);
				SettlInstMode = view.GetString(160);
				if (view.GetView("NoMiscFees") is IMessageView viewNoMiscFees)
				{
					var count = viewNoMiscFees.GroupCount();
					MiscFees = new NoMiscFees[count];
					for (int i = 0; i < count; i++)
					{
						MiscFees[i] = new();
						((IFixParser)MiscFees[i]).Parse(viewNoMiscFees.GetGroupInstance(i));
					}
				}
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
					case "AllocPrice":
					{
						value = AllocPrice;
						break;
					}
					case "AllocQty":
					{
						value = AllocQty;
						break;
					}
					case "IndividualAllocID":
					{
						value = IndividualAllocID;
						break;
					}
					case "ProcessCode":
					{
						value = ProcessCode;
						break;
					}
					case "NestedParties":
					{
						value = NestedParties;
						break;
					}
					case "NotifyBrokerOfCredit":
					{
						value = NotifyBrokerOfCredit;
						break;
					}
					case "AllocHandlInst":
					{
						value = AllocHandlInst;
						break;
					}
					case "AllocText":
					{
						value = AllocText;
						break;
					}
					case "EncodedAllocTextLen":
					{
						value = EncodedAllocTextLen;
						break;
					}
					case "EncodedAllocText":
					{
						value = EncodedAllocText;
						break;
					}
					case "CommissionData":
					{
						value = CommissionData;
						break;
					}
					case "AllocAvgPx":
					{
						value = AllocAvgPx;
						break;
					}
					case "AllocNetMoney":
					{
						value = AllocNetMoney;
						break;
					}
					case "SettlCurrAmt":
					{
						value = SettlCurrAmt;
						break;
					}
					case "SettlCurrency":
					{
						value = SettlCurrency;
						break;
					}
					case "SettlCurrFxRate":
					{
						value = SettlCurrFxRate;
						break;
					}
					case "SettlCurrFxRateCalc":
					{
						value = SettlCurrFxRateCalc;
						break;
					}
					case "AccruedInterestAmt":
					{
						value = AccruedInterestAmt;
						break;
					}
					case "SettlInstMode":
					{
						value = SettlInstMode;
						break;
					}
					case "NoMiscFees":
					{
						value = MiscFees;
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
				AllocPrice = null;
				AllocQty = null;
				IndividualAllocID = null;
				ProcessCode = null;
				((IFixReset?)NestedParties)?.Reset();
				NotifyBrokerOfCredit = null;
				AllocHandlInst = null;
				AllocText = null;
				EncodedAllocTextLen = null;
				EncodedAllocText = null;
				((IFixReset?)CommissionData)?.Reset();
				AllocAvgPx = null;
				AllocNetMoney = null;
				SettlCurrAmt = null;
				SettlCurrency = null;
				SettlCurrFxRate = null;
				SettlCurrFxRateCalc = null;
				AccruedInterestAmt = null;
				SettlInstMode = null;
				MiscFees = null;
			}
		}
		[Group(NoOfTag = 78, Offset = 38, Required = false)]
		public NoAllocs[]? Allocs {get; set;}
		
		[Component(Offset = 39, Required = true)]
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
			if (AllocID is not null) writer.WriteString(70, AllocID);
			if (AllocTransType is not null) writer.WriteString(71, AllocTransType);
			if (AllocType is not null) writer.WriteWholeNumber(626, AllocType.Value);
			if (RefAllocID is not null) writer.WriteString(72, RefAllocID);
			if (AllocLinkID is not null) writer.WriteString(196, AllocLinkID);
			if (AllocLinkType is not null) writer.WriteWholeNumber(197, AllocLinkType.Value);
			if (BookingRefID is not null) writer.WriteString(466, BookingRefID);
			if (Orders is not null && Orders.Length != 0)
			{
				writer.WriteWholeNumber(73, Orders.Length);
				for (int i = 0; i < Orders.Length; i++)
				{
					((IFixEncoder)Orders[i]).Encode(writer);
				}
			}
			if (Execs is not null && Execs.Length != 0)
			{
				writer.WriteWholeNumber(124, Execs.Length);
				for (int i = 0; i < Execs.Length; i++)
				{
					((IFixEncoder)Execs[i]).Encode(writer);
				}
			}
			if (Side is not null) writer.WriteString(54, Side);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (Quantity is not null) writer.WriteNumber(53, Quantity.Value);
			if (LastMkt is not null) writer.WriteString(30, LastMkt);
			if (TradeOriginationDate is not null) writer.WriteString(229, TradeOriginationDate);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (AvgPx is not null) writer.WriteNumber(6, AvgPx.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (AvgPrxPrecision is not null) writer.WriteWholeNumber(74, AvgPrxPrecision.Value);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (SettlmntTyp is not null) writer.WriteString(63, SettlmntTyp);
			if (FutSettDate is not null) writer.WriteLocalDateOnly(64, FutSettDate.Value);
			if (GrossTradeAmt is not null) writer.WriteNumber(381, GrossTradeAmt.Value);
			if (Concession is not null) writer.WriteNumber(238, Concession.Value);
			if (TotalTakedown is not null) writer.WriteNumber(237, TotalTakedown.Value);
			if (NetMoney is not null) writer.WriteNumber(118, NetMoney.Value);
			if (PositionEffect is not null) writer.WriteString(77, PositionEffect);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
			if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
			if (NumDaysInterest is not null) writer.WriteWholeNumber(157, NumDaysInterest.Value);
			if (AccruedInterestRate is not null) writer.WriteNumber(158, AccruedInterestRate.Value);
			if (TotalAccruedInterestAmt is not null) writer.WriteNumber(540, TotalAccruedInterestAmt.Value);
			if (LegalConfirm is not null) writer.WriteBoolean(650, LegalConfirm.Value);
			if (Allocs is not null && Allocs.Length != 0)
			{
				writer.WriteWholeNumber(78, Allocs.Length);
				for (int i = 0; i < Allocs.Length; i++)
				{
					((IFixEncoder)Allocs[i]).Encode(writer);
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
			AllocID = view.GetString(70);
			AllocTransType = view.GetString(71);
			AllocType = view.GetInt32(626);
			RefAllocID = view.GetString(72);
			AllocLinkID = view.GetString(196);
			AllocLinkType = view.GetInt32(197);
			BookingRefID = view.GetString(466);
			if (view.GetView("NoOrders") is IMessageView viewNoOrders)
			{
				var count = viewNoOrders.GroupCount();
				Orders = new NoOrders[count];
				for (int i = 0; i < count; i++)
				{
					Orders[i] = new();
					((IFixParser)Orders[i]).Parse(viewNoOrders.GetGroupInstance(i));
				}
			}
			if (view.GetView("NoExecs") is IMessageView viewNoExecs)
			{
				var count = viewNoExecs.GroupCount();
				Execs = new NoExecs[count];
				for (int i = 0; i < count; i++)
				{
					Execs[i] = new();
					((IFixParser)Execs[i]).Parse(viewNoExecs.GetGroupInstance(i));
				}
			}
			Side = view.GetString(54);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			Quantity = view.GetDouble(53);
			LastMkt = view.GetString(30);
			TradeOriginationDate = view.GetString(229);
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
			PriceType = view.GetInt32(423);
			AvgPx = view.GetDouble(6);
			Currency = view.GetString(15);
			AvgPrxPrecision = view.GetInt32(74);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				Parties = new();
				((IFixParser)Parties).Parse(viewParties);
			}
			TradeDate = view.GetDateOnly(75);
			TransactTime = view.GetDateTime(60);
			SettlmntTyp = view.GetString(63);
			FutSettDate = view.GetDateOnly(64);
			GrossTradeAmt = view.GetDouble(381);
			Concession = view.GetDouble(238);
			TotalTakedown = view.GetDouble(237);
			NetMoney = view.GetDouble(118);
			PositionEffect = view.GetString(77);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			NumDaysInterest = view.GetInt32(157);
			AccruedInterestRate = view.GetDouble(158);
			TotalAccruedInterestAmt = view.GetDouble(540);
			LegalConfirm = view.GetBool(650);
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
				case "AllocID":
				{
					value = AllocID;
					break;
				}
				case "AllocTransType":
				{
					value = AllocTransType;
					break;
				}
				case "AllocType":
				{
					value = AllocType;
					break;
				}
				case "RefAllocID":
				{
					value = RefAllocID;
					break;
				}
				case "AllocLinkID":
				{
					value = AllocLinkID;
					break;
				}
				case "AllocLinkType":
				{
					value = AllocLinkType;
					break;
				}
				case "BookingRefID":
				{
					value = BookingRefID;
					break;
				}
				case "NoOrders":
				{
					value = Orders;
					break;
				}
				case "NoExecs":
				{
					value = Execs;
					break;
				}
				case "Side":
				{
					value = Side;
					break;
				}
				case "Instrument":
				{
					value = Instrument;
					break;
				}
				case "Quantity":
				{
					value = Quantity;
					break;
				}
				case "LastMkt":
				{
					value = LastMkt;
					break;
				}
				case "TradeOriginationDate":
				{
					value = TradeOriginationDate;
					break;
				}
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
				case "PriceType":
				{
					value = PriceType;
					break;
				}
				case "AvgPx":
				{
					value = AvgPx;
					break;
				}
				case "Currency":
				{
					value = Currency;
					break;
				}
				case "AvgPrxPrecision":
				{
					value = AvgPrxPrecision;
					break;
				}
				case "Parties":
				{
					value = Parties;
					break;
				}
				case "TradeDate":
				{
					value = TradeDate;
					break;
				}
				case "TransactTime":
				{
					value = TransactTime;
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
				case "GrossTradeAmt":
				{
					value = GrossTradeAmt;
					break;
				}
				case "Concession":
				{
					value = Concession;
					break;
				}
				case "TotalTakedown":
				{
					value = TotalTakedown;
					break;
				}
				case "NetMoney":
				{
					value = NetMoney;
					break;
				}
				case "PositionEffect":
				{
					value = PositionEffect;
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
				case "NumDaysInterest":
				{
					value = NumDaysInterest;
					break;
				}
				case "AccruedInterestRate":
				{
					value = AccruedInterestRate;
					break;
				}
				case "TotalAccruedInterestAmt":
				{
					value = TotalAccruedInterestAmt;
					break;
				}
				case "LegalConfirm":
				{
					value = LegalConfirm;
					break;
				}
				case "NoAllocs":
				{
					value = Allocs;
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
			AllocID = null;
			AllocTransType = null;
			AllocType = null;
			RefAllocID = null;
			AllocLinkID = null;
			AllocLinkType = null;
			BookingRefID = null;
			Orders = null;
			Execs = null;
			Side = null;
			((IFixReset?)Instrument)?.Reset();
			Quantity = null;
			LastMkt = null;
			TradeOriginationDate = null;
			TradingSessionID = null;
			TradingSessionSubID = null;
			PriceType = null;
			AvgPx = null;
			Currency = null;
			AvgPrxPrecision = null;
			((IFixReset?)Parties)?.Reset();
			TradeDate = null;
			TransactTime = null;
			SettlmntTyp = null;
			FutSettDate = null;
			GrossTradeAmt = null;
			Concession = null;
			TotalTakedown = null;
			NetMoney = null;
			PositionEffect = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			NumDaysInterest = null;
			AccruedInterestRate = null;
			TotalAccruedInterestAmt = null;
			LegalConfirm = null;
			Allocs = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
