using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX43.Components;

namespace PureFix.Types.FIX43
{
	[MessageType("AE", FixVersion.FIX43)]
	public sealed partial class TradeCaptureReport : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 571, Type = TagType.String, Offset = 1, Required = true)]
		public string? TradeReportID {get; set;}
		
		[TagDetails(Tag = 487, Type = TagType.String, Offset = 2, Required = false)]
		public string? TradeReportTransType {get; set;}
		
		[TagDetails(Tag = 568, Type = TagType.String, Offset = 3, Required = false)]
		public string? TradeRequestID {get; set;}
		
		[TagDetails(Tag = 150, Type = TagType.String, Offset = 4, Required = true)]
		public string? ExecType {get; set;}
		
		[TagDetails(Tag = 572, Type = TagType.String, Offset = 5, Required = false)]
		public string? TradeReportRefID {get; set;}
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 6, Required = false)]
		public string? ExecID {get; set;}
		
		[TagDetails(Tag = 527, Type = TagType.String, Offset = 7, Required = false)]
		public string? SecondaryExecID {get; set;}
		
		[TagDetails(Tag = 378, Type = TagType.Int, Offset = 8, Required = false)]
		public int? ExecRestatementReason {get; set;}
		
		[TagDetails(Tag = 570, Type = TagType.Boolean, Offset = 9, Required = true)]
		public bool? PreviouslyReported {get; set;}
		
		[Component(Offset = 10, Required = true)]
		public Instrument? Instrument {get; set;}
		
		[Component(Offset = 11, Required = false)]
		public OrderQtyData? OrderQtyData {get; set;}
		
		[TagDetails(Tag = 32, Type = TagType.Float, Offset = 12, Required = true)]
		public double? LastQty {get; set;}
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 13, Required = true)]
		public double? LastPx {get; set;}
		
		[TagDetails(Tag = 194, Type = TagType.Float, Offset = 14, Required = false)]
		public double? LastSpotRate {get; set;}
		
		[TagDetails(Tag = 195, Type = TagType.Float, Offset = 15, Required = false)]
		public double? LastForwardPoints {get; set;}
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 16, Required = false)]
		public string? LastMkt {get; set;}
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 17, Required = true)]
		public DateOnly? TradeDate {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 18, Required = true)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 19, Required = false)]
		public string? SettlmntTyp {get; set;}
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 20, Required = false)]
		public DateOnly? FutSettDate {get; set;}
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 21, Required = false)]
		public string? MatchStatus {get; set;}
		
		[TagDetails(Tag = 574, Type = TagType.String, Offset = 22, Required = false)]
		public string? MatchType {get; set;}
		
		public sealed partial class NoSides : IFixGroup
		{
			[TagDetails(Tag = 54, Type = TagType.String, Offset = 0, Required = true)]
			public string? Side {get; set;}
			
			[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = true)]
			public string? OrderID {get; set;}
			
			[TagDetails(Tag = 198, Type = TagType.String, Offset = 2, Required = false)]
			public string? SecondaryOrderID {get; set;}
			
			[TagDetails(Tag = 11, Type = TagType.String, Offset = 3, Required = false)]
			public string? ClOrdID {get; set;}
			
			[Component(Offset = 4, Required = false)]
			public Parties? Parties {get; set;}
			
			[TagDetails(Tag = 1, Type = TagType.String, Offset = 5, Required = false)]
			public string? Account {get; set;}
			
			[TagDetails(Tag = 581, Type = TagType.Int, Offset = 6, Required = false)]
			public int? AccountType {get; set;}
			
			[TagDetails(Tag = 81, Type = TagType.String, Offset = 7, Required = false)]
			public string? ProcessCode {get; set;}
			
			[TagDetails(Tag = 575, Type = TagType.Boolean, Offset = 8, Required = false)]
			public bool? OddLot {get; set;}
			
			public sealed partial class NoClearingInstructions : IFixGroup
			{
				[TagDetails(Tag = 577, Type = TagType.Int, Offset = 0, Required = false)]
				public int? ClearingInstruction {get; set;}
				
				
				bool IFixValidator.IsValid(in FixValidatorConfig config)
				{
					return true;
				}
				
				void IFixEncoder.Encode(IFixWriter writer)
				{
					if (ClearingInstruction is not null) writer.WriteWholeNumber(577, ClearingInstruction.Value);
				}
				
				void IFixParser.Parse(IMessageView? view)
				{
					if (view is null) return;
					
					ClearingInstruction = view.GetInt32(577);
				}
				
				bool IFixLookup.TryGetByTag(string name, out object? value)
				{
					value = null;
					switch (name)
					{
						case "ClearingInstruction":
						{
							value = ClearingInstruction;
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
					ClearingInstruction = null;
				}
			}
			[Group(NoOfTag = 576, Offset = 9, Required = false)]
			public NoClearingInstructions[]? ClearingInstructions {get; set;}
			
			[TagDetails(Tag = 635, Type = TagType.String, Offset = 10, Required = false)]
			public string? ClearingFeeIndicator {get; set;}
			
			[TagDetails(Tag = 578, Type = TagType.String, Offset = 11, Required = false)]
			public string? TradeInputSource {get; set;}
			
			[TagDetails(Tag = 579, Type = TagType.String, Offset = 12, Required = false)]
			public string? TradeInputDevice {get; set;}
			
			[TagDetails(Tag = 15, Type = TagType.String, Offset = 13, Required = false)]
			public string? Currency {get; set;}
			
			[TagDetails(Tag = 376, Type = TagType.String, Offset = 14, Required = false)]
			public string? ComplianceID {get; set;}
			
			[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 15, Required = false)]
			public bool? SolicitedFlag {get; set;}
			
			[TagDetails(Tag = 528, Type = TagType.String, Offset = 16, Required = false)]
			public string? OrderCapacity {get; set;}
			
			[TagDetails(Tag = 529, Type = TagType.String, Offset = 17, Required = false)]
			public string? OrderRestrictions {get; set;}
			
			[TagDetails(Tag = 582, Type = TagType.Int, Offset = 18, Required = false)]
			public int? CustOrderCapacity {get; set;}
			
			[TagDetails(Tag = 483, Type = TagType.UtcTimestamp, Offset = 19, Required = false)]
			public DateTime? TransBkdTime {get; set;}
			
			[TagDetails(Tag = 336, Type = TagType.String, Offset = 20, Required = false)]
			public string? TradingSessionID {get; set;}
			
			[TagDetails(Tag = 625, Type = TagType.String, Offset = 21, Required = false)]
			public string? TradingSessionSubID {get; set;}
			
			[Component(Offset = 22, Required = false)]
			public CommissionData? CommissionData {get; set;}
			
			[TagDetails(Tag = 381, Type = TagType.Float, Offset = 23, Required = false)]
			public double? GrossTradeAmt {get; set;}
			
			[TagDetails(Tag = 157, Type = TagType.Int, Offset = 24, Required = false)]
			public int? NumDaysInterest {get; set;}
			
			[TagDetails(Tag = 230, Type = TagType.String, Offset = 25, Required = false)]
			public string? ExDate {get; set;}
			
			[TagDetails(Tag = 158, Type = TagType.Float, Offset = 26, Required = false)]
			public double? AccruedInterestRate {get; set;}
			
			[TagDetails(Tag = 159, Type = TagType.Float, Offset = 27, Required = false)]
			public double? AccruedInterestAmt {get; set;}
			
			[TagDetails(Tag = 238, Type = TagType.Float, Offset = 28, Required = false)]
			public double? Concession {get; set;}
			
			[TagDetails(Tag = 237, Type = TagType.Float, Offset = 29, Required = false)]
			public double? TotalTakedown {get; set;}
			
			[TagDetails(Tag = 118, Type = TagType.Float, Offset = 30, Required = false)]
			public double? NetMoney {get; set;}
			
			[TagDetails(Tag = 119, Type = TagType.Float, Offset = 31, Required = false)]
			public double? SettlCurrAmt {get; set;}
			
			[TagDetails(Tag = 120, Type = TagType.String, Offset = 32, Required = false)]
			public string? SettlCurrency {get; set;}
			
			[TagDetails(Tag = 155, Type = TagType.Float, Offset = 33, Required = false)]
			public double? SettlCurrFxRate {get; set;}
			
			[TagDetails(Tag = 156, Type = TagType.String, Offset = 34, Required = false)]
			public string? SettlCurrFxRateCalc {get; set;}
			
			[TagDetails(Tag = 77, Type = TagType.String, Offset = 35, Required = false)]
			public string? PositionEffect {get; set;}
			
			[TagDetails(Tag = 58, Type = TagType.String, Offset = 36, Required = false)]
			public string? Text {get; set;}
			
			[TagDetails(Tag = 354, Type = TagType.Length, Offset = 37, Required = false)]
			public int? EncodedTextLen {get; set;}
			
			[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 38, Required = false)]
			public byte[]? EncodedText {get; set;}
			
			[TagDetails(Tag = 442, Type = TagType.String, Offset = 39, Required = false)]
			public string? MultiLegReportingType {get; set;}
			
			public sealed partial class NoContAmts : IFixGroup
			{
				[TagDetails(Tag = 519, Type = TagType.Int, Offset = 0, Required = false)]
				public int? ContAmtType {get; set;}
				
				[TagDetails(Tag = 520, Type = TagType.Float, Offset = 1, Required = false)]
				public double? ContAmtValue {get; set;}
				
				[TagDetails(Tag = 521, Type = TagType.String, Offset = 2, Required = false)]
				public string? ContAmtCurr {get; set;}
				
				
				bool IFixValidator.IsValid(in FixValidatorConfig config)
				{
					return true;
				}
				
				void IFixEncoder.Encode(IFixWriter writer)
				{
					if (ContAmtType is not null) writer.WriteWholeNumber(519, ContAmtType.Value);
					if (ContAmtValue is not null) writer.WriteNumber(520, ContAmtValue.Value);
					if (ContAmtCurr is not null) writer.WriteString(521, ContAmtCurr);
				}
				
				void IFixParser.Parse(IMessageView? view)
				{
					if (view is null) return;
					
					ContAmtType = view.GetInt32(519);
					ContAmtValue = view.GetDouble(520);
					ContAmtCurr = view.GetString(521);
				}
				
				bool IFixLookup.TryGetByTag(string name, out object? value)
				{
					value = null;
					switch (name)
					{
						case "ContAmtType":
						{
							value = ContAmtType;
							break;
						}
						case "ContAmtValue":
						{
							value = ContAmtValue;
							break;
						}
						case "ContAmtCurr":
						{
							value = ContAmtCurr;
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
					ContAmtType = null;
					ContAmtValue = null;
					ContAmtCurr = null;
				}
			}
			[Group(NoOfTag = 518, Offset = 40, Required = false)]
			public NoContAmts[]? ContAmts {get; set;}
			
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
			[Group(NoOfTag = 136, Offset = 41, Required = false)]
			public NoMiscFees[]? MiscFees {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (Side is not null) writer.WriteString(54, Side);
				if (OrderID is not null) writer.WriteString(37, OrderID);
				if (SecondaryOrderID is not null) writer.WriteString(198, SecondaryOrderID);
				if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
				if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
				if (Account is not null) writer.WriteString(1, Account);
				if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
				if (ProcessCode is not null) writer.WriteString(81, ProcessCode);
				if (OddLot is not null) writer.WriteBoolean(575, OddLot.Value);
				if (ClearingInstructions is not null && ClearingInstructions.Length != 0)
				{
					writer.WriteWholeNumber(576, ClearingInstructions.Length);
					for (int i = 0; i < ClearingInstructions.Length; i++)
					{
						((IFixEncoder)ClearingInstructions[i]).Encode(writer);
					}
				}
				if (ClearingFeeIndicator is not null) writer.WriteString(635, ClearingFeeIndicator);
				if (TradeInputSource is not null) writer.WriteString(578, TradeInputSource);
				if (TradeInputDevice is not null) writer.WriteString(579, TradeInputDevice);
				if (Currency is not null) writer.WriteString(15, Currency);
				if (ComplianceID is not null) writer.WriteString(376, ComplianceID);
				if (SolicitedFlag is not null) writer.WriteBoolean(377, SolicitedFlag.Value);
				if (OrderCapacity is not null) writer.WriteString(528, OrderCapacity);
				if (OrderRestrictions is not null) writer.WriteString(529, OrderRestrictions);
				if (CustOrderCapacity is not null) writer.WriteWholeNumber(582, CustOrderCapacity.Value);
				if (TransBkdTime is not null) writer.WriteUtcTimeStamp(483, TransBkdTime.Value);
				if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
				if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
				if (CommissionData is not null) ((IFixEncoder)CommissionData).Encode(writer);
				if (GrossTradeAmt is not null) writer.WriteNumber(381, GrossTradeAmt.Value);
				if (NumDaysInterest is not null) writer.WriteWholeNumber(157, NumDaysInterest.Value);
				if (ExDate is not null) writer.WriteString(230, ExDate);
				if (AccruedInterestRate is not null) writer.WriteNumber(158, AccruedInterestRate.Value);
				if (AccruedInterestAmt is not null) writer.WriteNumber(159, AccruedInterestAmt.Value);
				if (Concession is not null) writer.WriteNumber(238, Concession.Value);
				if (TotalTakedown is not null) writer.WriteNumber(237, TotalTakedown.Value);
				if (NetMoney is not null) writer.WriteNumber(118, NetMoney.Value);
				if (SettlCurrAmt is not null) writer.WriteNumber(119, SettlCurrAmt.Value);
				if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
				if (SettlCurrFxRate is not null) writer.WriteNumber(155, SettlCurrFxRate.Value);
				if (SettlCurrFxRateCalc is not null) writer.WriteString(156, SettlCurrFxRateCalc);
				if (PositionEffect is not null) writer.WriteString(77, PositionEffect);
				if (Text is not null) writer.WriteString(58, Text);
				if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
				if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
				if (MultiLegReportingType is not null) writer.WriteString(442, MultiLegReportingType);
				if (ContAmts is not null && ContAmts.Length != 0)
				{
					writer.WriteWholeNumber(518, ContAmts.Length);
					for (int i = 0; i < ContAmts.Length; i++)
					{
						((IFixEncoder)ContAmts[i]).Encode(writer);
					}
				}
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
				
				Side = view.GetString(54);
				OrderID = view.GetString(37);
				SecondaryOrderID = view.GetString(198);
				ClOrdID = view.GetString(11);
				if (view.GetView("Parties") is IMessageView viewParties)
				{
					Parties = new();
					((IFixParser)Parties).Parse(viewParties);
				}
				Account = view.GetString(1);
				AccountType = view.GetInt32(581);
				ProcessCode = view.GetString(81);
				OddLot = view.GetBool(575);
				if (view.GetView("NoClearingInstructions") is IMessageView viewNoClearingInstructions)
				{
					var count = viewNoClearingInstructions.GroupCount();
					ClearingInstructions = new NoClearingInstructions[count];
					for (int i = 0; i < count; i++)
					{
						ClearingInstructions[i] = new();
						((IFixParser)ClearingInstructions[i]).Parse(viewNoClearingInstructions.GetGroupInstance(i));
					}
				}
				ClearingFeeIndicator = view.GetString(635);
				TradeInputSource = view.GetString(578);
				TradeInputDevice = view.GetString(579);
				Currency = view.GetString(15);
				ComplianceID = view.GetString(376);
				SolicitedFlag = view.GetBool(377);
				OrderCapacity = view.GetString(528);
				OrderRestrictions = view.GetString(529);
				CustOrderCapacity = view.GetInt32(582);
				TransBkdTime = view.GetDateTime(483);
				TradingSessionID = view.GetString(336);
				TradingSessionSubID = view.GetString(625);
				if (view.GetView("CommissionData") is IMessageView viewCommissionData)
				{
					CommissionData = new();
					((IFixParser)CommissionData).Parse(viewCommissionData);
				}
				GrossTradeAmt = view.GetDouble(381);
				NumDaysInterest = view.GetInt32(157);
				ExDate = view.GetString(230);
				AccruedInterestRate = view.GetDouble(158);
				AccruedInterestAmt = view.GetDouble(159);
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
				MultiLegReportingType = view.GetString(442);
				if (view.GetView("NoContAmts") is IMessageView viewNoContAmts)
				{
					var count = viewNoContAmts.GroupCount();
					ContAmts = new NoContAmts[count];
					for (int i = 0; i < count; i++)
					{
						ContAmts[i] = new();
						((IFixParser)ContAmts[i]).Parse(viewNoContAmts.GetGroupInstance(i));
					}
				}
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
					case "Side":
					{
						value = Side;
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
					case "ClOrdID":
					{
						value = ClOrdID;
						break;
					}
					case "Parties":
					{
						value = Parties;
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
					case "ProcessCode":
					{
						value = ProcessCode;
						break;
					}
					case "OddLot":
					{
						value = OddLot;
						break;
					}
					case "NoClearingInstructions":
					{
						value = ClearingInstructions;
						break;
					}
					case "ClearingFeeIndicator":
					{
						value = ClearingFeeIndicator;
						break;
					}
					case "TradeInputSource":
					{
						value = TradeInputSource;
						break;
					}
					case "TradeInputDevice":
					{
						value = TradeInputDevice;
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
					case "TransBkdTime":
					{
						value = TransBkdTime;
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
					case "CommissionData":
					{
						value = CommissionData;
						break;
					}
					case "GrossTradeAmt":
					{
						value = GrossTradeAmt;
						break;
					}
					case "NumDaysInterest":
					{
						value = NumDaysInterest;
						break;
					}
					case "ExDate":
					{
						value = ExDate;
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
					case "MultiLegReportingType":
					{
						value = MultiLegReportingType;
						break;
					}
					case "NoContAmts":
					{
						value = ContAmts;
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
				Side = null;
				OrderID = null;
				SecondaryOrderID = null;
				ClOrdID = null;
				((IFixReset?)Parties)?.Reset();
				Account = null;
				AccountType = null;
				ProcessCode = null;
				OddLot = null;
				ClearingInstructions = null;
				ClearingFeeIndicator = null;
				TradeInputSource = null;
				TradeInputDevice = null;
				Currency = null;
				ComplianceID = null;
				SolicitedFlag = null;
				OrderCapacity = null;
				OrderRestrictions = null;
				CustOrderCapacity = null;
				TransBkdTime = null;
				TradingSessionID = null;
				TradingSessionSubID = null;
				((IFixReset?)CommissionData)?.Reset();
				GrossTradeAmt = null;
				NumDaysInterest = null;
				ExDate = null;
				AccruedInterestRate = null;
				AccruedInterestAmt = null;
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
				MultiLegReportingType = null;
				ContAmts = null;
				MiscFees = null;
			}
		}
		[Group(NoOfTag = 552, Offset = 23, Required = true)]
		public NoSides[]? Sides {get; set;}
		
		[Component(Offset = 24, Required = true)]
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
			if (TradeReportID is not null) writer.WriteString(571, TradeReportID);
			if (TradeReportTransType is not null) writer.WriteString(487, TradeReportTransType);
			if (TradeRequestID is not null) writer.WriteString(568, TradeRequestID);
			if (ExecType is not null) writer.WriteString(150, ExecType);
			if (TradeReportRefID is not null) writer.WriteString(572, TradeReportRefID);
			if (ExecID is not null) writer.WriteString(17, ExecID);
			if (SecondaryExecID is not null) writer.WriteString(527, SecondaryExecID);
			if (ExecRestatementReason is not null) writer.WriteWholeNumber(378, ExecRestatementReason.Value);
			if (PreviouslyReported is not null) writer.WriteBoolean(570, PreviouslyReported.Value);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
			if (LastQty is not null) writer.WriteNumber(32, LastQty.Value);
			if (LastPx is not null) writer.WriteNumber(31, LastPx.Value);
			if (LastSpotRate is not null) writer.WriteNumber(194, LastSpotRate.Value);
			if (LastForwardPoints is not null) writer.WriteNumber(195, LastForwardPoints.Value);
			if (LastMkt is not null) writer.WriteString(30, LastMkt);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (SettlmntTyp is not null) writer.WriteString(63, SettlmntTyp);
			if (FutSettDate is not null) writer.WriteLocalDateOnly(64, FutSettDate.Value);
			if (MatchStatus is not null) writer.WriteString(573, MatchStatus);
			if (MatchType is not null) writer.WriteString(574, MatchType);
			if (Sides is not null && Sides.Length != 0)
			{
				writer.WriteWholeNumber(552, Sides.Length);
				for (int i = 0; i < Sides.Length; i++)
				{
					((IFixEncoder)Sides[i]).Encode(writer);
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
			TradeReportID = view.GetString(571);
			TradeReportTransType = view.GetString(487);
			TradeRequestID = view.GetString(568);
			ExecType = view.GetString(150);
			TradeReportRefID = view.GetString(572);
			ExecID = view.GetString(17);
			SecondaryExecID = view.GetString(527);
			ExecRestatementReason = view.GetInt32(378);
			PreviouslyReported = view.GetBool(570);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			if (view.GetView("OrderQtyData") is IMessageView viewOrderQtyData)
			{
				OrderQtyData = new();
				((IFixParser)OrderQtyData).Parse(viewOrderQtyData);
			}
			LastQty = view.GetDouble(32);
			LastPx = view.GetDouble(31);
			LastSpotRate = view.GetDouble(194);
			LastForwardPoints = view.GetDouble(195);
			LastMkt = view.GetString(30);
			TradeDate = view.GetDateOnly(75);
			TransactTime = view.GetDateTime(60);
			SettlmntTyp = view.GetString(63);
			FutSettDate = view.GetDateOnly(64);
			MatchStatus = view.GetString(573);
			MatchType = view.GetString(574);
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
				case "TradeReportID":
				{
					value = TradeReportID;
					break;
				}
				case "TradeReportTransType":
				{
					value = TradeReportTransType;
					break;
				}
				case "TradeRequestID":
				{
					value = TradeRequestID;
					break;
				}
				case "ExecType":
				{
					value = ExecType;
					break;
				}
				case "TradeReportRefID":
				{
					value = TradeReportRefID;
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
				case "ExecRestatementReason":
				{
					value = ExecRestatementReason;
					break;
				}
				case "PreviouslyReported":
				{
					value = PreviouslyReported;
					break;
				}
				case "Instrument":
				{
					value = Instrument;
					break;
				}
				case "OrderQtyData":
				{
					value = OrderQtyData;
					break;
				}
				case "LastQty":
				{
					value = LastQty;
					break;
				}
				case "LastPx":
				{
					value = LastPx;
					break;
				}
				case "LastSpotRate":
				{
					value = LastSpotRate;
					break;
				}
				case "LastForwardPoints":
				{
					value = LastForwardPoints;
					break;
				}
				case "LastMkt":
				{
					value = LastMkt;
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
				case "MatchStatus":
				{
					value = MatchStatus;
					break;
				}
				case "MatchType":
				{
					value = MatchType;
					break;
				}
				case "NoSides":
				{
					value = Sides;
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
			TradeReportID = null;
			TradeReportTransType = null;
			TradeRequestID = null;
			ExecType = null;
			TradeReportRefID = null;
			ExecID = null;
			SecondaryExecID = null;
			ExecRestatementReason = null;
			PreviouslyReported = null;
			((IFixReset?)Instrument)?.Reset();
			((IFixReset?)OrderQtyData)?.Reset();
			LastQty = null;
			LastPx = null;
			LastSpotRate = null;
			LastForwardPoints = null;
			LastMkt = null;
			TradeDate = null;
			TransactTime = null;
			SettlmntTyp = null;
			FutSettDate = null;
			MatchStatus = null;
			MatchType = null;
			Sides = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
