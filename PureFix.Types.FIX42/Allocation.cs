using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX42.Components;

namespace PureFix.Types.FIX42
{
	[MessageType("J", FixVersion.FIX42)]
	public sealed partial class Allocation : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 1, Required = true)]
		public string? AllocID {get; set;}
		
		[TagDetails(Tag = 71, Type = TagType.String, Offset = 2, Required = true)]
		public string? AllocTransType {get; set;}
		
		[TagDetails(Tag = 72, Type = TagType.String, Offset = 3, Required = false)]
		public string? RefAllocID {get; set;}
		
		[TagDetails(Tag = 196, Type = TagType.String, Offset = 4, Required = false)]
		public string? AllocLinkID {get; set;}
		
		[TagDetails(Tag = 197, Type = TagType.Int, Offset = 5, Required = false)]
		public int? AllocLinkType {get; set;}
		
		public sealed partial class NoOrders : IFixGroup
		{
			[TagDetails(Tag = 11, Type = TagType.String, Offset = 0, Required = false)]
			public string? ClOrdID {get; set;}
			
			[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = false)]
			public string? OrderID {get; set;}
			
			[TagDetails(Tag = 198, Type = TagType.String, Offset = 2, Required = false)]
			public string? SecondaryOrderID {get; set;}
			
			[TagDetails(Tag = 66, Type = TagType.String, Offset = 3, Required = false)]
			public string? ListID {get; set;}
			
			[TagDetails(Tag = 105, Type = TagType.String, Offset = 4, Required = false)]
			public string? WaveNo {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
				if (OrderID is not null) writer.WriteString(37, OrderID);
				if (SecondaryOrderID is not null) writer.WriteString(198, SecondaryOrderID);
				if (ListID is not null) writer.WriteString(66, ListID);
				if (WaveNo is not null) writer.WriteString(105, WaveNo);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				ClOrdID = view.GetString(11);
				OrderID = view.GetString(37);
				SecondaryOrderID = view.GetString(198);
				ListID = view.GetString(66);
				WaveNo = view.GetString(105);
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
					case "ListID":
					{
						value = ListID;
						break;
					}
					case "WaveNo":
					{
						value = WaveNo;
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
				ListID = null;
				WaveNo = null;
			}
		}
		[Group(NoOfTag = 73, Offset = 6, Required = false)]
		public NoOrders[]? Orders {get; set;}
		
		public sealed partial class NoExecs : IFixGroup
		{
			[TagDetails(Tag = 32, Type = TagType.Float, Offset = 0, Required = false)]
			public double? LastShares {get; set;}
			
			[TagDetails(Tag = 17, Type = TagType.String, Offset = 1, Required = false)]
			public string? ExecID {get; set;}
			
			[TagDetails(Tag = 31, Type = TagType.Float, Offset = 2, Required = false)]
			public double? LastPx {get; set;}
			
			[TagDetails(Tag = 29, Type = TagType.String, Offset = 3, Required = false)]
			public string? LastCapacity {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (LastShares is not null) writer.WriteNumber(32, LastShares.Value);
				if (ExecID is not null) writer.WriteString(17, ExecID);
				if (LastPx is not null) writer.WriteNumber(31, LastPx.Value);
				if (LastCapacity is not null) writer.WriteString(29, LastCapacity);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				LastShares = view.GetDouble(32);
				ExecID = view.GetString(17);
				LastPx = view.GetDouble(31);
				LastCapacity = view.GetString(29);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "LastShares":
					{
						value = LastShares;
						break;
					}
					case "ExecID":
					{
						value = ExecID;
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
				LastShares = null;
				ExecID = null;
				LastPx = null;
				LastCapacity = null;
			}
		}
		[Group(NoOfTag = 124, Offset = 7, Required = false)]
		public NoExecs[]? Execs {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 8, Required = true)]
		public string? Side {get; set;}
		
		[TagDetails(Tag = 55, Type = TagType.String, Offset = 9, Required = true)]
		public string? Symbol {get; set;}
		
		[TagDetails(Tag = 65, Type = TagType.String, Offset = 10, Required = false)]
		public string? SymbolSfx {get; set;}
		
		[TagDetails(Tag = 48, Type = TagType.String, Offset = 11, Required = false)]
		public string? SecurityID {get; set;}
		
		[TagDetails(Tag = 22, Type = TagType.String, Offset = 12, Required = false)]
		public string? IDSource {get; set;}
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 13, Required = false)]
		public string? SecurityType {get; set;}
		
		[TagDetails(Tag = 200, Type = TagType.MonthYear, Offset = 14, Required = false)]
		public MonthYear? MaturityMonthYear {get; set;}
		
		[TagDetails(Tag = 205, Type = TagType.String, Offset = 15, Required = false)]
		public string? MaturityDay {get; set;}
		
		[TagDetails(Tag = 201, Type = TagType.Int, Offset = 16, Required = false)]
		public int? PutOrCall {get; set;}
		
		[TagDetails(Tag = 202, Type = TagType.Float, Offset = 17, Required = false)]
		public double? StrikePrice {get; set;}
		
		[TagDetails(Tag = 206, Type = TagType.String, Offset = 18, Required = false)]
		public string? OptAttribute {get; set;}
		
		[TagDetails(Tag = 231, Type = TagType.Float, Offset = 19, Required = false)]
		public double? ContractMultiplier {get; set;}
		
		[TagDetails(Tag = 223, Type = TagType.Float, Offset = 20, Required = false)]
		public double? CouponRate {get; set;}
		
		[TagDetails(Tag = 207, Type = TagType.String, Offset = 21, Required = false)]
		public string? SecurityExchange {get; set;}
		
		[TagDetails(Tag = 106, Type = TagType.String, Offset = 22, Required = false)]
		public string? Issuer {get; set;}
		
		[TagDetails(Tag = 348, Type = TagType.Length, Offset = 23, Required = false)]
		public int? EncodedIssuerLen {get; set;}
		
		[TagDetails(Tag = 349, Type = TagType.RawData, Offset = 24, Required = false)]
		public byte[]? EncodedIssuer {get; set;}
		
		[TagDetails(Tag = 107, Type = TagType.String, Offset = 25, Required = false)]
		public string? SecurityDesc {get; set;}
		
		[TagDetails(Tag = 350, Type = TagType.Length, Offset = 26, Required = false)]
		public int? EncodedSecurityDescLen {get; set;}
		
		[TagDetails(Tag = 351, Type = TagType.RawData, Offset = 27, Required = false)]
		public byte[]? EncodedSecurityDesc {get; set;}
		
		[TagDetails(Tag = 53, Type = TagType.Float, Offset = 28, Required = true)]
		public double? Shares {get; set;}
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 29, Required = false)]
		public string? LastMkt {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 30, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 31, Required = true)]
		public double? AvgPx {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 32, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 74, Type = TagType.Int, Offset = 33, Required = false)]
		public int? AvgPrxPrecision {get; set;}
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 34, Required = true)]
		public DateOnly? TradeDate {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 35, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 36, Required = false)]
		public string? SettlmntTyp {get; set;}
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 37, Required = false)]
		public DateOnly? FutSettDate {get; set;}
		
		[TagDetails(Tag = 381, Type = TagType.Float, Offset = 38, Required = false)]
		public double? GrossTradeAmt {get; set;}
		
		[TagDetails(Tag = 118, Type = TagType.Float, Offset = 39, Required = false)]
		public double? NetMoney {get; set;}
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 40, Required = false)]
		public string? OpenClose {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 41, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 42, Required = false)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 43, Required = false)]
		public byte[]? EncodedText {get; set;}
		
		[TagDetails(Tag = 157, Type = TagType.Int, Offset = 44, Required = false)]
		public int? NumDaysInterest {get; set;}
		
		[TagDetails(Tag = 158, Type = TagType.Float, Offset = 45, Required = false)]
		public double? AccruedInterestRate {get; set;}
		
		public sealed partial class NoAllocs : IFixGroup
		{
			[TagDetails(Tag = 79, Type = TagType.String, Offset = 0, Required = false)]
			public string? AllocAccount {get; set;}
			
			[TagDetails(Tag = 366, Type = TagType.Float, Offset = 1, Required = false)]
			public double? AllocPrice {get; set;}
			
			[TagDetails(Tag = 80, Type = TagType.Float, Offset = 2, Required = true)]
			public double? AllocShares {get; set;}
			
			[TagDetails(Tag = 81, Type = TagType.String, Offset = 3, Required = false)]
			public string? ProcessCode {get; set;}
			
			[TagDetails(Tag = 92, Type = TagType.String, Offset = 4, Required = false)]
			public string? BrokerOfCredit {get; set;}
			
			[TagDetails(Tag = 208, Type = TagType.Boolean, Offset = 5, Required = false)]
			public bool? NotifyBrokerOfCredit {get; set;}
			
			[TagDetails(Tag = 209, Type = TagType.Int, Offset = 6, Required = false)]
			public int? AllocHandlInst {get; set;}
			
			[TagDetails(Tag = 161, Type = TagType.String, Offset = 7, Required = false)]
			public string? AllocText {get; set;}
			
			[TagDetails(Tag = 360, Type = TagType.Length, Offset = 8, Required = false)]
			public int? EncodedAllocTextLen {get; set;}
			
			[TagDetails(Tag = 361, Type = TagType.RawData, Offset = 9, Required = false)]
			public byte[]? EncodedAllocText {get; set;}
			
			[TagDetails(Tag = 76, Type = TagType.String, Offset = 10, Required = false)]
			public string? ExecBroker {get; set;}
			
			[TagDetails(Tag = 109, Type = TagType.String, Offset = 11, Required = false)]
			public string? ClientID {get; set;}
			
			[TagDetails(Tag = 12, Type = TagType.Float, Offset = 12, Required = false)]
			public double? Commission {get; set;}
			
			[TagDetails(Tag = 13, Type = TagType.String, Offset = 13, Required = false)]
			public string? CommType {get; set;}
			
			[TagDetails(Tag = 153, Type = TagType.Float, Offset = 14, Required = false)]
			public double? AllocAvgPx {get; set;}
			
			[TagDetails(Tag = 154, Type = TagType.Float, Offset = 15, Required = false)]
			public double? AllocNetMoney {get; set;}
			
			[TagDetails(Tag = 119, Type = TagType.Float, Offset = 16, Required = false)]
			public double? SettlCurrAmt {get; set;}
			
			[TagDetails(Tag = 120, Type = TagType.String, Offset = 17, Required = false)]
			public string? SettlCurrency {get; set;}
			
			[TagDetails(Tag = 155, Type = TagType.Float, Offset = 18, Required = false)]
			public double? SettlCurrFxRate {get; set;}
			
			[TagDetails(Tag = 156, Type = TagType.String, Offset = 19, Required = false)]
			public string? SettlCurrFxRateCalc {get; set;}
			
			[TagDetails(Tag = 159, Type = TagType.Float, Offset = 20, Required = false)]
			public double? AccruedInterestAmt {get; set;}
			
			[TagDetails(Tag = 160, Type = TagType.String, Offset = 21, Required = false)]
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
			[Group(NoOfTag = 136, Offset = 22, Required = false)]
			public NoMiscFees[]? MiscFees {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (AllocAccount is not null) writer.WriteString(79, AllocAccount);
				if (AllocPrice is not null) writer.WriteNumber(366, AllocPrice.Value);
				if (AllocShares is not null) writer.WriteNumber(80, AllocShares.Value);
				if (ProcessCode is not null) writer.WriteString(81, ProcessCode);
				if (BrokerOfCredit is not null) writer.WriteString(92, BrokerOfCredit);
				if (NotifyBrokerOfCredit is not null) writer.WriteBoolean(208, NotifyBrokerOfCredit.Value);
				if (AllocHandlInst is not null) writer.WriteWholeNumber(209, AllocHandlInst.Value);
				if (AllocText is not null) writer.WriteString(161, AllocText);
				if (EncodedAllocTextLen is not null) writer.WriteWholeNumber(360, EncodedAllocTextLen.Value);
				if (EncodedAllocText is not null) writer.WriteBuffer(361, EncodedAllocText);
				if (ExecBroker is not null) writer.WriteString(76, ExecBroker);
				if (ClientID is not null) writer.WriteString(109, ClientID);
				if (Commission is not null) writer.WriteNumber(12, Commission.Value);
				if (CommType is not null) writer.WriteString(13, CommType);
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
				AllocShares = view.GetDouble(80);
				ProcessCode = view.GetString(81);
				BrokerOfCredit = view.GetString(92);
				NotifyBrokerOfCredit = view.GetBool(208);
				AllocHandlInst = view.GetInt32(209);
				AllocText = view.GetString(161);
				EncodedAllocTextLen = view.GetInt32(360);
				EncodedAllocText = view.GetByteArray(361);
				ExecBroker = view.GetString(76);
				ClientID = view.GetString(109);
				Commission = view.GetDouble(12);
				CommType = view.GetString(13);
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
					case "AllocShares":
					{
						value = AllocShares;
						break;
					}
					case "ProcessCode":
					{
						value = ProcessCode;
						break;
					}
					case "BrokerOfCredit":
					{
						value = BrokerOfCredit;
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
					case "ExecBroker":
					{
						value = ExecBroker;
						break;
					}
					case "ClientID":
					{
						value = ClientID;
						break;
					}
					case "Commission":
					{
						value = Commission;
						break;
					}
					case "CommType":
					{
						value = CommType;
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
				AllocShares = null;
				ProcessCode = null;
				BrokerOfCredit = null;
				NotifyBrokerOfCredit = null;
				AllocHandlInst = null;
				AllocText = null;
				EncodedAllocTextLen = null;
				EncodedAllocText = null;
				ExecBroker = null;
				ClientID = null;
				Commission = null;
				CommType = null;
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
		[Group(NoOfTag = 78, Offset = 46, Required = false)]
		public NoAllocs[]? Allocs {get; set;}
		
		[Component(Offset = 47, Required = true)]
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
			if (RefAllocID is not null) writer.WriteString(72, RefAllocID);
			if (AllocLinkID is not null) writer.WriteString(196, AllocLinkID);
			if (AllocLinkType is not null) writer.WriteWholeNumber(197, AllocLinkType.Value);
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
			if (Symbol is not null) writer.WriteString(55, Symbol);
			if (SymbolSfx is not null) writer.WriteString(65, SymbolSfx);
			if (SecurityID is not null) writer.WriteString(48, SecurityID);
			if (IDSource is not null) writer.WriteString(22, IDSource);
			if (SecurityType is not null) writer.WriteString(167, SecurityType);
			if (MaturityMonthYear is not null) writer.WriteMonthYear(200, MaturityMonthYear.Value);
			if (MaturityDay is not null) writer.WriteString(205, MaturityDay);
			if (PutOrCall is not null) writer.WriteWholeNumber(201, PutOrCall.Value);
			if (StrikePrice is not null) writer.WriteNumber(202, StrikePrice.Value);
			if (OptAttribute is not null) writer.WriteString(206, OptAttribute);
			if (ContractMultiplier is not null) writer.WriteNumber(231, ContractMultiplier.Value);
			if (CouponRate is not null) writer.WriteNumber(223, CouponRate.Value);
			if (SecurityExchange is not null) writer.WriteString(207, SecurityExchange);
			if (Issuer is not null) writer.WriteString(106, Issuer);
			if (EncodedIssuerLen is not null) writer.WriteWholeNumber(348, EncodedIssuerLen.Value);
			if (EncodedIssuer is not null) writer.WriteBuffer(349, EncodedIssuer);
			if (SecurityDesc is not null) writer.WriteString(107, SecurityDesc);
			if (EncodedSecurityDescLen is not null) writer.WriteWholeNumber(350, EncodedSecurityDescLen.Value);
			if (EncodedSecurityDesc is not null) writer.WriteBuffer(351, EncodedSecurityDesc);
			if (Shares is not null) writer.WriteNumber(53, Shares.Value);
			if (LastMkt is not null) writer.WriteString(30, LastMkt);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (AvgPx is not null) writer.WriteNumber(6, AvgPx.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (AvgPrxPrecision is not null) writer.WriteWholeNumber(74, AvgPrxPrecision.Value);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (SettlmntTyp is not null) writer.WriteString(63, SettlmntTyp);
			if (FutSettDate is not null) writer.WriteLocalDateOnly(64, FutSettDate.Value);
			if (GrossTradeAmt is not null) writer.WriteNumber(381, GrossTradeAmt.Value);
			if (NetMoney is not null) writer.WriteNumber(118, NetMoney.Value);
			if (OpenClose is not null) writer.WriteString(77, OpenClose);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
			if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
			if (NumDaysInterest is not null) writer.WriteWholeNumber(157, NumDaysInterest.Value);
			if (AccruedInterestRate is not null) writer.WriteNumber(158, AccruedInterestRate.Value);
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
			RefAllocID = view.GetString(72);
			AllocLinkID = view.GetString(196);
			AllocLinkType = view.GetInt32(197);
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
			Symbol = view.GetString(55);
			SymbolSfx = view.GetString(65);
			SecurityID = view.GetString(48);
			IDSource = view.GetString(22);
			SecurityType = view.GetString(167);
			MaturityMonthYear = view.GetMonthYear(200);
			MaturityDay = view.GetString(205);
			PutOrCall = view.GetInt32(201);
			StrikePrice = view.GetDouble(202);
			OptAttribute = view.GetString(206);
			ContractMultiplier = view.GetDouble(231);
			CouponRate = view.GetDouble(223);
			SecurityExchange = view.GetString(207);
			Issuer = view.GetString(106);
			EncodedIssuerLen = view.GetInt32(348);
			EncodedIssuer = view.GetByteArray(349);
			SecurityDesc = view.GetString(107);
			EncodedSecurityDescLen = view.GetInt32(350);
			EncodedSecurityDesc = view.GetByteArray(351);
			Shares = view.GetDouble(53);
			LastMkt = view.GetString(30);
			TradingSessionID = view.GetString(336);
			AvgPx = view.GetDouble(6);
			Currency = view.GetString(15);
			AvgPrxPrecision = view.GetInt32(74);
			TradeDate = view.GetDateOnly(75);
			TransactTime = view.GetDateTime(60);
			SettlmntTyp = view.GetString(63);
			FutSettDate = view.GetDateOnly(64);
			GrossTradeAmt = view.GetDouble(381);
			NetMoney = view.GetDouble(118);
			OpenClose = view.GetString(77);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			NumDaysInterest = view.GetInt32(157);
			AccruedInterestRate = view.GetDouble(158);
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
				case "Symbol":
				{
					value = Symbol;
					break;
				}
				case "SymbolSfx":
				{
					value = SymbolSfx;
					break;
				}
				case "SecurityID":
				{
					value = SecurityID;
					break;
				}
				case "IDSource":
				{
					value = IDSource;
					break;
				}
				case "SecurityType":
				{
					value = SecurityType;
					break;
				}
				case "MaturityMonthYear":
				{
					value = MaturityMonthYear;
					break;
				}
				case "MaturityDay":
				{
					value = MaturityDay;
					break;
				}
				case "PutOrCall":
				{
					value = PutOrCall;
					break;
				}
				case "StrikePrice":
				{
					value = StrikePrice;
					break;
				}
				case "OptAttribute":
				{
					value = OptAttribute;
					break;
				}
				case "ContractMultiplier":
				{
					value = ContractMultiplier;
					break;
				}
				case "CouponRate":
				{
					value = CouponRate;
					break;
				}
				case "SecurityExchange":
				{
					value = SecurityExchange;
					break;
				}
				case "Issuer":
				{
					value = Issuer;
					break;
				}
				case "EncodedIssuerLen":
				{
					value = EncodedIssuerLen;
					break;
				}
				case "EncodedIssuer":
				{
					value = EncodedIssuer;
					break;
				}
				case "SecurityDesc":
				{
					value = SecurityDesc;
					break;
				}
				case "EncodedSecurityDescLen":
				{
					value = EncodedSecurityDescLen;
					break;
				}
				case "EncodedSecurityDesc":
				{
					value = EncodedSecurityDesc;
					break;
				}
				case "Shares":
				{
					value = Shares;
					break;
				}
				case "LastMkt":
				{
					value = LastMkt;
					break;
				}
				case "TradingSessionID":
				{
					value = TradingSessionID;
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
				case "NetMoney":
				{
					value = NetMoney;
					break;
				}
				case "OpenClose":
				{
					value = OpenClose;
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
			RefAllocID = null;
			AllocLinkID = null;
			AllocLinkType = null;
			Orders = null;
			Execs = null;
			Side = null;
			Symbol = null;
			SymbolSfx = null;
			SecurityID = null;
			IDSource = null;
			SecurityType = null;
			MaturityMonthYear = null;
			MaturityDay = null;
			PutOrCall = null;
			StrikePrice = null;
			OptAttribute = null;
			ContractMultiplier = null;
			CouponRate = null;
			SecurityExchange = null;
			Issuer = null;
			EncodedIssuerLen = null;
			EncodedIssuer = null;
			SecurityDesc = null;
			EncodedSecurityDescLen = null;
			EncodedSecurityDesc = null;
			Shares = null;
			LastMkt = null;
			TradingSessionID = null;
			AvgPx = null;
			Currency = null;
			AvgPrxPrecision = null;
			TradeDate = null;
			TransactTime = null;
			SettlmntTyp = null;
			FutSettDate = null;
			GrossTradeAmt = null;
			NetMoney = null;
			OpenClose = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			NumDaysInterest = null;
			AccruedInterestRate = null;
			Allocs = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
