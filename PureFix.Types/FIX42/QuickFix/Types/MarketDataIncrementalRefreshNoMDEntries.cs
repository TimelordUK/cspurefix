using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public sealed partial class MarketDataIncrementalRefreshNoMDEntries : IFixGroup
	{
		[TagDetails(Tag = 279, Type = TagType.String, Offset = 0, Required = true)]
		public string? MDUpdateAction {get; set;}
		
		[TagDetails(Tag = 285, Type = TagType.String, Offset = 1, Required = false)]
		public string? DeleteReason {get; set;}
		
		[TagDetails(Tag = 269, Type = TagType.String, Offset = 2, Required = false)]
		public string? MDEntryType {get; set;}
		
		[TagDetails(Tag = 278, Type = TagType.String, Offset = 3, Required = false)]
		public string? MDEntryID {get; set;}
		
		[TagDetails(Tag = 280, Type = TagType.String, Offset = 4, Required = false)]
		public string? MDEntryRefID {get; set;}
		
		[TagDetails(Tag = 55, Type = TagType.String, Offset = 5, Required = false)]
		public string? Symbol {get; set;}
		
		[TagDetails(Tag = 65, Type = TagType.String, Offset = 6, Required = false)]
		public string? SymbolSfx {get; set;}
		
		[TagDetails(Tag = 48, Type = TagType.String, Offset = 7, Required = false)]
		public string? SecurityID {get; set;}
		
		[TagDetails(Tag = 22, Type = TagType.String, Offset = 8, Required = false)]
		public string? IDSource {get; set;}
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 9, Required = false)]
		public string? SecurityType {get; set;}
		
		[TagDetails(Tag = 200, Type = TagType.MonthYear, Offset = 10, Required = false)]
		public MonthYear? MaturityMonthYear {get; set;}
		
		[TagDetails(Tag = 205, Type = TagType.String, Offset = 11, Required = false)]
		public string? MaturityDay {get; set;}
		
		[TagDetails(Tag = 201, Type = TagType.Int, Offset = 12, Required = false)]
		public int? PutOrCall {get; set;}
		
		[TagDetails(Tag = 202, Type = TagType.Float, Offset = 13, Required = false)]
		public double? StrikePrice {get; set;}
		
		[TagDetails(Tag = 206, Type = TagType.String, Offset = 14, Required = false)]
		public string? OptAttribute {get; set;}
		
		[TagDetails(Tag = 231, Type = TagType.Float, Offset = 15, Required = false)]
		public double? ContractMultiplier {get; set;}
		
		[TagDetails(Tag = 223, Type = TagType.Float, Offset = 16, Required = false)]
		public double? CouponRate {get; set;}
		
		[TagDetails(Tag = 207, Type = TagType.String, Offset = 17, Required = false)]
		public string? SecurityExchange {get; set;}
		
		[TagDetails(Tag = 106, Type = TagType.String, Offset = 18, Required = false)]
		public string? Issuer {get; set;}
		
		[TagDetails(Tag = 348, Type = TagType.Length, Offset = 19, Required = false, LinksToTag = 349)]
		public int? EncodedIssuerLen {get; set;}
		
		[TagDetails(Tag = 349, Type = TagType.RawData, Offset = 20, Required = false, LinksToTag = 348)]
		public byte[]? EncodedIssuer {get; set;}
		
		[TagDetails(Tag = 107, Type = TagType.String, Offset = 21, Required = false)]
		public string? SecurityDesc {get; set;}
		
		[TagDetails(Tag = 350, Type = TagType.Length, Offset = 22, Required = false, LinksToTag = 351)]
		public int? EncodedSecurityDescLen {get; set;}
		
		[TagDetails(Tag = 351, Type = TagType.RawData, Offset = 23, Required = false, LinksToTag = 350)]
		public byte[]? EncodedSecurityDesc {get; set;}
		
		[TagDetails(Tag = 291, Type = TagType.String, Offset = 24, Required = false)]
		public string? FinancialStatus {get; set;}
		
		[TagDetails(Tag = 292, Type = TagType.String, Offset = 25, Required = false)]
		public string? CorporateAction {get; set;}
		
		[TagDetails(Tag = 270, Type = TagType.Float, Offset = 26, Required = false)]
		public double? MDEntryPx {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 27, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 271, Type = TagType.Float, Offset = 28, Required = false)]
		public double? MDEntrySize {get; set;}
		
		[TagDetails(Tag = 272, Type = TagType.String, Offset = 29, Required = false)]
		public string? MDEntryDate {get; set;}
		
		[TagDetails(Tag = 273, Type = TagType.UtcTimeOnly, Offset = 30, Required = false)]
		public TimeOnly? MDEntryTime {get; set;}
		
		[TagDetails(Tag = 274, Type = TagType.String, Offset = 31, Required = false)]
		public string? TickDirection {get; set;}
		
		[TagDetails(Tag = 275, Type = TagType.String, Offset = 32, Required = false)]
		public string? MDMkt {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 33, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 276, Type = TagType.String, Offset = 34, Required = false)]
		public string? QuoteCondition {get; set;}
		
		[TagDetails(Tag = 277, Type = TagType.String, Offset = 35, Required = false)]
		public string? TradeCondition {get; set;}
		
		[TagDetails(Tag = 282, Type = TagType.String, Offset = 36, Required = false)]
		public string? MDEntryOriginator {get; set;}
		
		[TagDetails(Tag = 283, Type = TagType.String, Offset = 37, Required = false)]
		public string? LocationID {get; set;}
		
		[TagDetails(Tag = 284, Type = TagType.String, Offset = 38, Required = false)]
		public string? DeskID {get; set;}
		
		[TagDetails(Tag = 286, Type = TagType.String, Offset = 39, Required = false)]
		public string? OpenCloseSettleFlag {get; set;}
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 40, Required = false)]
		public string? TimeInForce {get; set;}
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 41, Required = false)]
		public DateOnly? ExpireDate {get; set;}
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 42, Required = false)]
		public DateTime? ExpireTime {get; set;}
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 43, Required = false)]
		public double? MinQty {get; set;}
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 44, Required = false)]
		public string? ExecInst {get; set;}
		
		[TagDetails(Tag = 287, Type = TagType.Int, Offset = 45, Required = false)]
		public int? SellerDays {get; set;}
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 46, Required = false)]
		public string? OrderID {get; set;}
		
		[TagDetails(Tag = 299, Type = TagType.String, Offset = 47, Required = false)]
		public string? QuoteEntryID {get; set;}
		
		[TagDetails(Tag = 288, Type = TagType.String, Offset = 48, Required = false)]
		public string? MDEntryBuyer {get; set;}
		
		[TagDetails(Tag = 289, Type = TagType.String, Offset = 49, Required = false)]
		public string? MDEntrySeller {get; set;}
		
		[TagDetails(Tag = 346, Type = TagType.Int, Offset = 50, Required = false)]
		public int? NumberOfOrders {get; set;}
		
		[TagDetails(Tag = 290, Type = TagType.Int, Offset = 51, Required = false)]
		public int? MDEntryPositionNo {get; set;}
		
		[TagDetails(Tag = 387, Type = TagType.Float, Offset = 52, Required = false)]
		public double? TotalVolumeTraded {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 53, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 54, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 55, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				MDUpdateAction is not null;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (MDUpdateAction is not null) writer.WriteString(279, MDUpdateAction);
			if (DeleteReason is not null) writer.WriteString(285, DeleteReason);
			if (MDEntryType is not null) writer.WriteString(269, MDEntryType);
			if (MDEntryID is not null) writer.WriteString(278, MDEntryID);
			if (MDEntryRefID is not null) writer.WriteString(280, MDEntryRefID);
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
			if (EncodedIssuer is not null)
			{
				writer.WriteWholeNumber(348, EncodedIssuer.Length);
				writer.WriteBuffer(349, EncodedIssuer);
			}
			if (SecurityDesc is not null) writer.WriteString(107, SecurityDesc);
			if (EncodedSecurityDesc is not null)
			{
				writer.WriteWholeNumber(350, EncodedSecurityDesc.Length);
				writer.WriteBuffer(351, EncodedSecurityDesc);
			}
			if (FinancialStatus is not null) writer.WriteString(291, FinancialStatus);
			if (CorporateAction is not null) writer.WriteString(292, CorporateAction);
			if (MDEntryPx is not null) writer.WriteNumber(270, MDEntryPx.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (MDEntrySize is not null) writer.WriteNumber(271, MDEntrySize.Value);
			if (MDEntryDate is not null) writer.WriteString(272, MDEntryDate);
			if (MDEntryTime is not null) writer.WriteTimeOnly(273, MDEntryTime.Value);
			if (TickDirection is not null) writer.WriteString(274, TickDirection);
			if (MDMkt is not null) writer.WriteString(275, MDMkt);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (QuoteCondition is not null) writer.WriteString(276, QuoteCondition);
			if (TradeCondition is not null) writer.WriteString(277, TradeCondition);
			if (MDEntryOriginator is not null) writer.WriteString(282, MDEntryOriginator);
			if (LocationID is not null) writer.WriteString(283, LocationID);
			if (DeskID is not null) writer.WriteString(284, DeskID);
			if (OpenCloseSettleFlag is not null) writer.WriteString(286, OpenCloseSettleFlag);
			if (TimeInForce is not null) writer.WriteString(59, TimeInForce);
			if (ExpireDate is not null) writer.WriteLocalDateOnly(432, ExpireDate.Value);
			if (ExpireTime is not null) writer.WriteUtcTimeStamp(126, ExpireTime.Value);
			if (MinQty is not null) writer.WriteNumber(110, MinQty.Value);
			if (ExecInst is not null) writer.WriteString(18, ExecInst);
			if (SellerDays is not null) writer.WriteWholeNumber(287, SellerDays.Value);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (QuoteEntryID is not null) writer.WriteString(299, QuoteEntryID);
			if (MDEntryBuyer is not null) writer.WriteString(288, MDEntryBuyer);
			if (MDEntrySeller is not null) writer.WriteString(289, MDEntrySeller);
			if (NumberOfOrders is not null) writer.WriteWholeNumber(346, NumberOfOrders.Value);
			if (MDEntryPositionNo is not null) writer.WriteWholeNumber(290, MDEntryPositionNo.Value);
			if (TotalVolumeTraded is not null) writer.WriteNumber(387, TotalVolumeTraded.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			MDUpdateAction = view.GetString(279);
			DeleteReason = view.GetString(285);
			MDEntryType = view.GetString(269);
			MDEntryID = view.GetString(278);
			MDEntryRefID = view.GetString(280);
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
			FinancialStatus = view.GetString(291);
			CorporateAction = view.GetString(292);
			MDEntryPx = view.GetDouble(270);
			Currency = view.GetString(15);
			MDEntrySize = view.GetDouble(271);
			MDEntryDate = view.GetString(272);
			MDEntryTime = view.GetTimeOnly(273);
			TickDirection = view.GetString(274);
			MDMkt = view.GetString(275);
			TradingSessionID = view.GetString(336);
			QuoteCondition = view.GetString(276);
			TradeCondition = view.GetString(277);
			MDEntryOriginator = view.GetString(282);
			LocationID = view.GetString(283);
			DeskID = view.GetString(284);
			OpenCloseSettleFlag = view.GetString(286);
			TimeInForce = view.GetString(59);
			ExpireDate = view.GetDateOnly(432);
			ExpireTime = view.GetDateTime(126);
			MinQty = view.GetDouble(110);
			ExecInst = view.GetString(18);
			SellerDays = view.GetInt32(287);
			OrderID = view.GetString(37);
			QuoteEntryID = view.GetString(299);
			MDEntryBuyer = view.GetString(288);
			MDEntrySeller = view.GetString(289);
			NumberOfOrders = view.GetInt32(346);
			MDEntryPositionNo = view.GetInt32(290);
			TotalVolumeTraded = view.GetDouble(387);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "MDUpdateAction":
					value = MDUpdateAction;
					break;
				case "DeleteReason":
					value = DeleteReason;
					break;
				case "MDEntryType":
					value = MDEntryType;
					break;
				case "MDEntryID":
					value = MDEntryID;
					break;
				case "MDEntryRefID":
					value = MDEntryRefID;
					break;
				case "Symbol":
					value = Symbol;
					break;
				case "SymbolSfx":
					value = SymbolSfx;
					break;
				case "SecurityID":
					value = SecurityID;
					break;
				case "IDSource":
					value = IDSource;
					break;
				case "SecurityType":
					value = SecurityType;
					break;
				case "MaturityMonthYear":
					value = MaturityMonthYear;
					break;
				case "MaturityDay":
					value = MaturityDay;
					break;
				case "PutOrCall":
					value = PutOrCall;
					break;
				case "StrikePrice":
					value = StrikePrice;
					break;
				case "OptAttribute":
					value = OptAttribute;
					break;
				case "ContractMultiplier":
					value = ContractMultiplier;
					break;
				case "CouponRate":
					value = CouponRate;
					break;
				case "SecurityExchange":
					value = SecurityExchange;
					break;
				case "Issuer":
					value = Issuer;
					break;
				case "EncodedIssuerLen":
					value = EncodedIssuerLen;
					break;
				case "EncodedIssuer":
					value = EncodedIssuer;
					break;
				case "SecurityDesc":
					value = SecurityDesc;
					break;
				case "EncodedSecurityDescLen":
					value = EncodedSecurityDescLen;
					break;
				case "EncodedSecurityDesc":
					value = EncodedSecurityDesc;
					break;
				case "FinancialStatus":
					value = FinancialStatus;
					break;
				case "CorporateAction":
					value = CorporateAction;
					break;
				case "MDEntryPx":
					value = MDEntryPx;
					break;
				case "Currency":
					value = Currency;
					break;
				case "MDEntrySize":
					value = MDEntrySize;
					break;
				case "MDEntryDate":
					value = MDEntryDate;
					break;
				case "MDEntryTime":
					value = MDEntryTime;
					break;
				case "TickDirection":
					value = TickDirection;
					break;
				case "MDMkt":
					value = MDMkt;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "QuoteCondition":
					value = QuoteCondition;
					break;
				case "TradeCondition":
					value = TradeCondition;
					break;
				case "MDEntryOriginator":
					value = MDEntryOriginator;
					break;
				case "LocationID":
					value = LocationID;
					break;
				case "DeskID":
					value = DeskID;
					break;
				case "OpenCloseSettleFlag":
					value = OpenCloseSettleFlag;
					break;
				case "TimeInForce":
					value = TimeInForce;
					break;
				case "ExpireDate":
					value = ExpireDate;
					break;
				case "ExpireTime":
					value = ExpireTime;
					break;
				case "MinQty":
					value = MinQty;
					break;
				case "ExecInst":
					value = ExecInst;
					break;
				case "SellerDays":
					value = SellerDays;
					break;
				case "OrderID":
					value = OrderID;
					break;
				case "QuoteEntryID":
					value = QuoteEntryID;
					break;
				case "MDEntryBuyer":
					value = MDEntryBuyer;
					break;
				case "MDEntrySeller":
					value = MDEntrySeller;
					break;
				case "NumberOfOrders":
					value = NumberOfOrders;
					break;
				case "MDEntryPositionNo":
					value = MDEntryPositionNo;
					break;
				case "TotalVolumeTraded":
					value = TotalVolumeTraded;
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
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			MDUpdateAction = null;
			DeleteReason = null;
			MDEntryType = null;
			MDEntryID = null;
			MDEntryRefID = null;
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
			FinancialStatus = null;
			CorporateAction = null;
			MDEntryPx = null;
			Currency = null;
			MDEntrySize = null;
			MDEntryDate = null;
			MDEntryTime = null;
			TickDirection = null;
			MDMkt = null;
			TradingSessionID = null;
			QuoteCondition = null;
			TradeCondition = null;
			MDEntryOriginator = null;
			LocationID = null;
			DeskID = null;
			OpenCloseSettleFlag = null;
			TimeInForce = null;
			ExpireDate = null;
			ExpireTime = null;
			MinQty = null;
			ExecInst = null;
			SellerDays = null;
			OrderID = null;
			QuoteEntryID = null;
			MDEntryBuyer = null;
			MDEntrySeller = null;
			NumberOfOrders = null;
			MDEntryPositionNo = null;
			TotalVolumeTraded = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
		}
	}
}
