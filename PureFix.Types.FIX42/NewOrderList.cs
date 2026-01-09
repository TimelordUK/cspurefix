using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX42.Components;

namespace PureFix.Types.FIX42
{
	[MessageType("E", FixVersion.FIX42)]
	public sealed partial class NewOrderList : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 1, Required = true)]
		public string? ListID {get; set;}
		
		[TagDetails(Tag = 390, Type = TagType.String, Offset = 2, Required = false)]
		public string? BidID {get; set;}
		
		[TagDetails(Tag = 391, Type = TagType.String, Offset = 3, Required = false)]
		public string? ClientBidID {get; set;}
		
		[TagDetails(Tag = 414, Type = TagType.Int, Offset = 4, Required = false)]
		public int? ProgRptReqs {get; set;}
		
		[TagDetails(Tag = 394, Type = TagType.Int, Offset = 5, Required = true)]
		public int? BidType {get; set;}
		
		[TagDetails(Tag = 415, Type = TagType.Int, Offset = 6, Required = false)]
		public int? ProgPeriodInterval {get; set;}
		
		[TagDetails(Tag = 433, Type = TagType.String, Offset = 7, Required = false)]
		public string? ListExecInstType {get; set;}
		
		[TagDetails(Tag = 69, Type = TagType.String, Offset = 8, Required = false)]
		public string? ListExecInst {get; set;}
		
		[TagDetails(Tag = 352, Type = TagType.Length, Offset = 9, Required = false)]
		public int? EncodedListExecInstLen {get; set;}
		
		[TagDetails(Tag = 353, Type = TagType.RawData, Offset = 10, Required = false)]
		public byte[]? EncodedListExecInst {get; set;}
		
		[TagDetails(Tag = 68, Type = TagType.Int, Offset = 11, Required = true)]
		public int? TotNoOrders {get; set;}
		
		public sealed partial class NoOrders : IFixGroup
		{
			[TagDetails(Tag = 11, Type = TagType.String, Offset = 0, Required = true)]
			public string? ClOrdID {get; set;}
			
			[TagDetails(Tag = 67, Type = TagType.Int, Offset = 1, Required = true)]
			public int? ListSeqNo {get; set;}
			
			[TagDetails(Tag = 160, Type = TagType.String, Offset = 2, Required = false)]
			public string? SettlInstMode {get; set;}
			
			[TagDetails(Tag = 109, Type = TagType.String, Offset = 3, Required = false)]
			public string? ClientID {get; set;}
			
			[TagDetails(Tag = 76, Type = TagType.String, Offset = 4, Required = false)]
			public string? ExecBroker {get; set;}
			
			[TagDetails(Tag = 1, Type = TagType.String, Offset = 5, Required = false)]
			public string? Account {get; set;}
			
			public sealed partial class NoAllocs : IFixGroup
			{
				[TagDetails(Tag = 79, Type = TagType.String, Offset = 0, Required = false)]
				public string? AllocAccount {get; set;}
				
				[TagDetails(Tag = 80, Type = TagType.Float, Offset = 1, Required = false)]
				public double? AllocShares {get; set;}
				
				
				bool IFixValidator.IsValid(in FixValidatorConfig config)
				{
					return true;
				}
				
				void IFixEncoder.Encode(IFixWriter writer)
				{
					if (AllocAccount is not null) writer.WriteString(79, AllocAccount);
					if (AllocShares is not null) writer.WriteNumber(80, AllocShares.Value);
				}
				
				void IFixParser.Parse(IMessageView? view)
				{
					if (view is null) return;
					
					AllocAccount = view.GetString(79);
					AllocShares = view.GetDouble(80);
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
						case "AllocShares":
						{
							value = AllocShares;
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
					AllocShares = null;
				}
			}
			[Group(NoOfTag = 78, Offset = 6, Required = false)]
			public NoAllocs[]? Allocs {get; set;}
			
			[TagDetails(Tag = 63, Type = TagType.String, Offset = 7, Required = false)]
			public string? SettlmntTyp {get; set;}
			
			[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 8, Required = false)]
			public DateOnly? FutSettDate {get; set;}
			
			[TagDetails(Tag = 21, Type = TagType.String, Offset = 9, Required = false)]
			public string? HandlInst {get; set;}
			
			[TagDetails(Tag = 18, Type = TagType.String, Offset = 10, Required = false)]
			public string? ExecInst {get; set;}
			
			[TagDetails(Tag = 110, Type = TagType.Float, Offset = 11, Required = false)]
			public double? MinQty {get; set;}
			
			[TagDetails(Tag = 111, Type = TagType.Float, Offset = 12, Required = false)]
			public double? MaxFloor {get; set;}
			
			[TagDetails(Tag = 100, Type = TagType.String, Offset = 13, Required = false)]
			public string? ExDestination {get; set;}
			
			public sealed partial class NoTradingSessions : IFixGroup
			{
				[TagDetails(Tag = 336, Type = TagType.String, Offset = 0, Required = false)]
				public string? TradingSessionID {get; set;}
				
				
				bool IFixValidator.IsValid(in FixValidatorConfig config)
				{
					return true;
				}
				
				void IFixEncoder.Encode(IFixWriter writer)
				{
					if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
				}
				
				void IFixParser.Parse(IMessageView? view)
				{
					if (view is null) return;
					
					TradingSessionID = view.GetString(336);
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
				}
			}
			[Group(NoOfTag = 386, Offset = 14, Required = false)]
			public NoTradingSessions[]? TradingSessions {get; set;}
			
			[TagDetails(Tag = 81, Type = TagType.String, Offset = 15, Required = false)]
			public string? ProcessCode {get; set;}
			
			[TagDetails(Tag = 55, Type = TagType.String, Offset = 16, Required = true)]
			public string? Symbol {get; set;}
			
			[TagDetails(Tag = 65, Type = TagType.String, Offset = 17, Required = false)]
			public string? SymbolSfx {get; set;}
			
			[TagDetails(Tag = 48, Type = TagType.String, Offset = 18, Required = false)]
			public string? SecurityID {get; set;}
			
			[TagDetails(Tag = 22, Type = TagType.String, Offset = 19, Required = false)]
			public string? IDSource {get; set;}
			
			[TagDetails(Tag = 167, Type = TagType.String, Offset = 20, Required = false)]
			public string? SecurityType {get; set;}
			
			[TagDetails(Tag = 200, Type = TagType.MonthYear, Offset = 21, Required = false)]
			public MonthYear? MaturityMonthYear {get; set;}
			
			[TagDetails(Tag = 205, Type = TagType.String, Offset = 22, Required = false)]
			public string? MaturityDay {get; set;}
			
			[TagDetails(Tag = 201, Type = TagType.Int, Offset = 23, Required = false)]
			public int? PutOrCall {get; set;}
			
			[TagDetails(Tag = 202, Type = TagType.Float, Offset = 24, Required = false)]
			public double? StrikePrice {get; set;}
			
			[TagDetails(Tag = 206, Type = TagType.String, Offset = 25, Required = false)]
			public string? OptAttribute {get; set;}
			
			[TagDetails(Tag = 231, Type = TagType.Float, Offset = 26, Required = false)]
			public double? ContractMultiplier {get; set;}
			
			[TagDetails(Tag = 223, Type = TagType.Float, Offset = 27, Required = false)]
			public double? CouponRate {get; set;}
			
			[TagDetails(Tag = 207, Type = TagType.String, Offset = 28, Required = false)]
			public string? SecurityExchange {get; set;}
			
			[TagDetails(Tag = 106, Type = TagType.String, Offset = 29, Required = false)]
			public string? Issuer {get; set;}
			
			[TagDetails(Tag = 348, Type = TagType.Length, Offset = 30, Required = false)]
			public int? EncodedIssuerLen {get; set;}
			
			[TagDetails(Tag = 349, Type = TagType.RawData, Offset = 31, Required = false)]
			public byte[]? EncodedIssuer {get; set;}
			
			[TagDetails(Tag = 107, Type = TagType.String, Offset = 32, Required = false)]
			public string? SecurityDesc {get; set;}
			
			[TagDetails(Tag = 350, Type = TagType.Length, Offset = 33, Required = false)]
			public int? EncodedSecurityDescLen {get; set;}
			
			[TagDetails(Tag = 351, Type = TagType.RawData, Offset = 34, Required = false)]
			public byte[]? EncodedSecurityDesc {get; set;}
			
			[TagDetails(Tag = 140, Type = TagType.Float, Offset = 35, Required = false)]
			public double? PrevClosePx {get; set;}
			
			[TagDetails(Tag = 54, Type = TagType.String, Offset = 36, Required = true)]
			public string? Side {get; set;}
			
			[TagDetails(Tag = 401, Type = TagType.Int, Offset = 37, Required = false)]
			public int? SideValueInd {get; set;}
			
			[TagDetails(Tag = 114, Type = TagType.Boolean, Offset = 38, Required = false)]
			public bool? LocateReqd {get; set;}
			
			[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 39, Required = false)]
			public DateTime? TransactTime {get; set;}
			
			[TagDetails(Tag = 38, Type = TagType.Float, Offset = 40, Required = false)]
			public double? OrderQty {get; set;}
			
			[TagDetails(Tag = 152, Type = TagType.Float, Offset = 41, Required = false)]
			public double? CashOrderQty {get; set;}
			
			[TagDetails(Tag = 40, Type = TagType.String, Offset = 42, Required = false)]
			public string? OrdType {get; set;}
			
			[TagDetails(Tag = 44, Type = TagType.Float, Offset = 43, Required = false)]
			public double? Price {get; set;}
			
			[TagDetails(Tag = 99, Type = TagType.Float, Offset = 44, Required = false)]
			public double? StopPx {get; set;}
			
			[TagDetails(Tag = 15, Type = TagType.String, Offset = 45, Required = false)]
			public string? Currency {get; set;}
			
			[TagDetails(Tag = 376, Type = TagType.String, Offset = 46, Required = false)]
			public string? ComplianceID {get; set;}
			
			[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 47, Required = false)]
			public bool? SolicitedFlag {get; set;}
			
			[TagDetails(Tag = 23, Type = TagType.String, Offset = 48, Required = false)]
			public string? IOIid {get; set;}
			
			[TagDetails(Tag = 117, Type = TagType.String, Offset = 49, Required = false)]
			public string? QuoteID {get; set;}
			
			[TagDetails(Tag = 59, Type = TagType.String, Offset = 50, Required = false)]
			public string? TimeInForce {get; set;}
			
			[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 51, Required = false)]
			public DateTime? EffectiveTime {get; set;}
			
			[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 52, Required = false)]
			public DateOnly? ExpireDate {get; set;}
			
			[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 53, Required = false)]
			public DateTime? ExpireTime {get; set;}
			
			[TagDetails(Tag = 427, Type = TagType.Int, Offset = 54, Required = false)]
			public int? GTBookingInst {get; set;}
			
			[TagDetails(Tag = 12, Type = TagType.Float, Offset = 55, Required = false)]
			public double? Commission {get; set;}
			
			[TagDetails(Tag = 13, Type = TagType.String, Offset = 56, Required = false)]
			public string? CommType {get; set;}
			
			[TagDetails(Tag = 47, Type = TagType.String, Offset = 57, Required = false)]
			public string? Rule80A {get; set;}
			
			[TagDetails(Tag = 121, Type = TagType.Boolean, Offset = 58, Required = false)]
			public bool? ForexReq {get; set;}
			
			[TagDetails(Tag = 120, Type = TagType.String, Offset = 59, Required = false)]
			public string? SettlCurrency {get; set;}
			
			[TagDetails(Tag = 58, Type = TagType.String, Offset = 60, Required = false)]
			public string? Text {get; set;}
			
			[TagDetails(Tag = 354, Type = TagType.Length, Offset = 61, Required = false)]
			public int? EncodedTextLen {get; set;}
			
			[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 62, Required = false)]
			public byte[]? EncodedText {get; set;}
			
			[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 63, Required = false)]
			public DateOnly? FutSettDate2 {get; set;}
			
			[TagDetails(Tag = 192, Type = TagType.Float, Offset = 64, Required = false)]
			public double? OrderQty2 {get; set;}
			
			[TagDetails(Tag = 77, Type = TagType.String, Offset = 65, Required = false)]
			public string? OpenClose {get; set;}
			
			[TagDetails(Tag = 203, Type = TagType.Int, Offset = 66, Required = false)]
			public int? CoveredOrUncovered {get; set;}
			
			[TagDetails(Tag = 204, Type = TagType.Int, Offset = 67, Required = false)]
			public int? CustomerOrFirm {get; set;}
			
			[TagDetails(Tag = 210, Type = TagType.Float, Offset = 68, Required = false)]
			public double? MaxShow {get; set;}
			
			[TagDetails(Tag = 211, Type = TagType.Float, Offset = 69, Required = false)]
			public double? PegDifference {get; set;}
			
			[TagDetails(Tag = 388, Type = TagType.String, Offset = 70, Required = false)]
			public string? DiscretionInst {get; set;}
			
			[TagDetails(Tag = 389, Type = TagType.Float, Offset = 71, Required = false)]
			public double? DiscretionOffset {get; set;}
			
			[TagDetails(Tag = 439, Type = TagType.String, Offset = 72, Required = false)]
			public string? ClearingFirm {get; set;}
			
			[TagDetails(Tag = 440, Type = TagType.String, Offset = 73, Required = false)]
			public string? ClearingAccount {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
				if (ListSeqNo is not null) writer.WriteWholeNumber(67, ListSeqNo.Value);
				if (SettlInstMode is not null) writer.WriteString(160, SettlInstMode);
				if (ClientID is not null) writer.WriteString(109, ClientID);
				if (ExecBroker is not null) writer.WriteString(76, ExecBroker);
				if (Account is not null) writer.WriteString(1, Account);
				if (Allocs is not null && Allocs.Length != 0)
				{
					writer.WriteWholeNumber(78, Allocs.Length);
					for (int i = 0; i < Allocs.Length; i++)
					{
						((IFixEncoder)Allocs[i]).Encode(writer);
					}
				}
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
				if (PrevClosePx is not null) writer.WriteNumber(140, PrevClosePx.Value);
				if (Side is not null) writer.WriteString(54, Side);
				if (SideValueInd is not null) writer.WriteWholeNumber(401, SideValueInd.Value);
				if (LocateReqd is not null) writer.WriteBoolean(114, LocateReqd.Value);
				if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
				if (OrderQty is not null) writer.WriteNumber(38, OrderQty.Value);
				if (CashOrderQty is not null) writer.WriteNumber(152, CashOrderQty.Value);
				if (OrdType is not null) writer.WriteString(40, OrdType);
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
				if (Commission is not null) writer.WriteNumber(12, Commission.Value);
				if (CommType is not null) writer.WriteString(13, CommType);
				if (Rule80A is not null) writer.WriteString(47, Rule80A);
				if (ForexReq is not null) writer.WriteBoolean(121, ForexReq.Value);
				if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
				if (Text is not null) writer.WriteString(58, Text);
				if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
				if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
				if (FutSettDate2 is not null) writer.WriteLocalDateOnly(193, FutSettDate2.Value);
				if (OrderQty2 is not null) writer.WriteNumber(192, OrderQty2.Value);
				if (OpenClose is not null) writer.WriteString(77, OpenClose);
				if (CoveredOrUncovered is not null) writer.WriteWholeNumber(203, CoveredOrUncovered.Value);
				if (CustomerOrFirm is not null) writer.WriteWholeNumber(204, CustomerOrFirm.Value);
				if (MaxShow is not null) writer.WriteNumber(210, MaxShow.Value);
				if (PegDifference is not null) writer.WriteNumber(211, PegDifference.Value);
				if (DiscretionInst is not null) writer.WriteString(388, DiscretionInst);
				if (DiscretionOffset is not null) writer.WriteNumber(389, DiscretionOffset.Value);
				if (ClearingFirm is not null) writer.WriteString(439, ClearingFirm);
				if (ClearingAccount is not null) writer.WriteString(440, ClearingAccount);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				ClOrdID = view.GetString(11);
				ListSeqNo = view.GetInt32(67);
				SettlInstMode = view.GetString(160);
				ClientID = view.GetString(109);
				ExecBroker = view.GetString(76);
				Account = view.GetString(1);
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
				PrevClosePx = view.GetDouble(140);
				Side = view.GetString(54);
				SideValueInd = view.GetInt32(401);
				LocateReqd = view.GetBool(114);
				TransactTime = view.GetDateTime(60);
				OrderQty = view.GetDouble(38);
				CashOrderQty = view.GetDouble(152);
				OrdType = view.GetString(40);
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
				Commission = view.GetDouble(12);
				CommType = view.GetString(13);
				Rule80A = view.GetString(47);
				ForexReq = view.GetBool(121);
				SettlCurrency = view.GetString(120);
				Text = view.GetString(58);
				EncodedTextLen = view.GetInt32(354);
				EncodedText = view.GetByteArray(355);
				FutSettDate2 = view.GetDateOnly(193);
				OrderQty2 = view.GetDouble(192);
				OpenClose = view.GetString(77);
				CoveredOrUncovered = view.GetInt32(203);
				CustomerOrFirm = view.GetInt32(204);
				MaxShow = view.GetDouble(210);
				PegDifference = view.GetDouble(211);
				DiscretionInst = view.GetString(388);
				DiscretionOffset = view.GetDouble(389);
				ClearingFirm = view.GetString(439);
				ClearingAccount = view.GetString(440);
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
					case "ListSeqNo":
					{
						value = ListSeqNo;
						break;
					}
					case "SettlInstMode":
					{
						value = SettlInstMode;
						break;
					}
					case "ClientID":
					{
						value = ClientID;
						break;
					}
					case "ExecBroker":
					{
						value = ExecBroker;
						break;
					}
					case "Account":
					{
						value = Account;
						break;
					}
					case "NoAllocs":
					{
						value = Allocs;
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
					case "SideValueInd":
					{
						value = SideValueInd;
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
					case "OrderQty":
					{
						value = OrderQty;
						break;
					}
					case "CashOrderQty":
					{
						value = CashOrderQty;
						break;
					}
					case "OrdType":
					{
						value = OrdType;
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
					case "Rule80A":
					{
						value = Rule80A;
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
					case "FutSettDate2":
					{
						value = FutSettDate2;
						break;
					}
					case "OrderQty2":
					{
						value = OrderQty2;
						break;
					}
					case "OpenClose":
					{
						value = OpenClose;
						break;
					}
					case "CoveredOrUncovered":
					{
						value = CoveredOrUncovered;
						break;
					}
					case "CustomerOrFirm":
					{
						value = CustomerOrFirm;
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
					case "ClearingFirm":
					{
						value = ClearingFirm;
						break;
					}
					case "ClearingAccount":
					{
						value = ClearingAccount;
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
				ListSeqNo = null;
				SettlInstMode = null;
				ClientID = null;
				ExecBroker = null;
				Account = null;
				Allocs = null;
				SettlmntTyp = null;
				FutSettDate = null;
				HandlInst = null;
				ExecInst = null;
				MinQty = null;
				MaxFloor = null;
				ExDestination = null;
				TradingSessions = null;
				ProcessCode = null;
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
				PrevClosePx = null;
				Side = null;
				SideValueInd = null;
				LocateReqd = null;
				TransactTime = null;
				OrderQty = null;
				CashOrderQty = null;
				OrdType = null;
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
				Commission = null;
				CommType = null;
				Rule80A = null;
				ForexReq = null;
				SettlCurrency = null;
				Text = null;
				EncodedTextLen = null;
				EncodedText = null;
				FutSettDate2 = null;
				OrderQty2 = null;
				OpenClose = null;
				CoveredOrUncovered = null;
				CustomerOrFirm = null;
				MaxShow = null;
				PegDifference = null;
				DiscretionInst = null;
				DiscretionOffset = null;
				ClearingFirm = null;
				ClearingAccount = null;
			}
		}
		[Group(NoOfTag = 73, Offset = 12, Required = true)]
		public NoOrders[]? Orders {get; set;}
		
		[Component(Offset = 13, Required = true)]
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
			if (ListID is not null) writer.WriteString(66, ListID);
			if (BidID is not null) writer.WriteString(390, BidID);
			if (ClientBidID is not null) writer.WriteString(391, ClientBidID);
			if (ProgRptReqs is not null) writer.WriteWholeNumber(414, ProgRptReqs.Value);
			if (BidType is not null) writer.WriteWholeNumber(394, BidType.Value);
			if (ProgPeriodInterval is not null) writer.WriteWholeNumber(415, ProgPeriodInterval.Value);
			if (ListExecInstType is not null) writer.WriteString(433, ListExecInstType);
			if (ListExecInst is not null) writer.WriteString(69, ListExecInst);
			if (EncodedListExecInstLen is not null) writer.WriteWholeNumber(352, EncodedListExecInstLen.Value);
			if (EncodedListExecInst is not null) writer.WriteBuffer(353, EncodedListExecInst);
			if (TotNoOrders is not null) writer.WriteWholeNumber(68, TotNoOrders.Value);
			if (Orders is not null && Orders.Length != 0)
			{
				writer.WriteWholeNumber(73, Orders.Length);
				for (int i = 0; i < Orders.Length; i++)
				{
					((IFixEncoder)Orders[i]).Encode(writer);
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
			ListID = view.GetString(66);
			BidID = view.GetString(390);
			ClientBidID = view.GetString(391);
			ProgRptReqs = view.GetInt32(414);
			BidType = view.GetInt32(394);
			ProgPeriodInterval = view.GetInt32(415);
			ListExecInstType = view.GetString(433);
			ListExecInst = view.GetString(69);
			EncodedListExecInstLen = view.GetInt32(352);
			EncodedListExecInst = view.GetByteArray(353);
			TotNoOrders = view.GetInt32(68);
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
				case "ListID":
				{
					value = ListID;
					break;
				}
				case "BidID":
				{
					value = BidID;
					break;
				}
				case "ClientBidID":
				{
					value = ClientBidID;
					break;
				}
				case "ProgRptReqs":
				{
					value = ProgRptReqs;
					break;
				}
				case "BidType":
				{
					value = BidType;
					break;
				}
				case "ProgPeriodInterval":
				{
					value = ProgPeriodInterval;
					break;
				}
				case "ListExecInstType":
				{
					value = ListExecInstType;
					break;
				}
				case "ListExecInst":
				{
					value = ListExecInst;
					break;
				}
				case "EncodedListExecInstLen":
				{
					value = EncodedListExecInstLen;
					break;
				}
				case "EncodedListExecInst":
				{
					value = EncodedListExecInst;
					break;
				}
				case "TotNoOrders":
				{
					value = TotNoOrders;
					break;
				}
				case "NoOrders":
				{
					value = Orders;
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
			ListID = null;
			BidID = null;
			ClientBidID = null;
			ProgRptReqs = null;
			BidType = null;
			ProgPeriodInterval = null;
			ListExecInstType = null;
			ListExecInst = null;
			EncodedListExecInstLen = null;
			EncodedListExecInst = null;
			TotNoOrders = null;
			Orders = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
