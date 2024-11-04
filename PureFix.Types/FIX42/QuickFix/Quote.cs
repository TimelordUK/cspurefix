using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("S", FixVersion.FIX42)]
	public sealed partial class Quote : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 131, Type = TagType.String, Offset = 1, Required = false)]
		public string? QuoteReqID {get; set;}
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 2, Required = true)]
		public string? QuoteID {get; set;}
		
		[TagDetails(Tag = 301, Type = TagType.Int, Offset = 3, Required = false)]
		public int? QuoteResponseLevel {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 4, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 55, Type = TagType.String, Offset = 5, Required = true)]
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
		
		[TagDetails(Tag = 132, Type = TagType.Float, Offset = 24, Required = false)]
		public double? BidPx {get; set;}
		
		[TagDetails(Tag = 133, Type = TagType.Float, Offset = 25, Required = false)]
		public double? OfferPx {get; set;}
		
		[TagDetails(Tag = 134, Type = TagType.Float, Offset = 26, Required = false)]
		public double? BidSize {get; set;}
		
		[TagDetails(Tag = 135, Type = TagType.Float, Offset = 27, Required = false)]
		public double? OfferSize {get; set;}
		
		[TagDetails(Tag = 62, Type = TagType.UtcTimestamp, Offset = 28, Required = false)]
		public DateTime? ValidUntilTime {get; set;}
		
		[TagDetails(Tag = 188, Type = TagType.Float, Offset = 29, Required = false)]
		public double? BidSpotRate {get; set;}
		
		[TagDetails(Tag = 190, Type = TagType.Float, Offset = 30, Required = false)]
		public double? OfferSpotRate {get; set;}
		
		[TagDetails(Tag = 189, Type = TagType.Float, Offset = 31, Required = false)]
		public double? BidForwardPoints {get; set;}
		
		[TagDetails(Tag = 191, Type = TagType.Float, Offset = 32, Required = false)]
		public double? OfferForwardPoints {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 33, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 34, Required = false)]
		public DateOnly? FutSettDate {get; set;}
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 35, Required = false)]
		public string? OrdType {get; set;}
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 36, Required = false)]
		public DateOnly? FutSettDate2 {get; set;}
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 37, Required = false)]
		public double? OrderQty2 {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 38, Required = false)]
		public string? Currency {get; set;}
		
		[Component(Offset = 39, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& QuoteID is not null
				&& Symbol is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (QuoteReqID is not null) writer.WriteString(131, QuoteReqID);
			if (QuoteID is not null) writer.WriteString(117, QuoteID);
			if (QuoteResponseLevel is not null) writer.WriteWholeNumber(301, QuoteResponseLevel.Value);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
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
			if (BidPx is not null) writer.WriteNumber(132, BidPx.Value);
			if (OfferPx is not null) writer.WriteNumber(133, OfferPx.Value);
			if (BidSize is not null) writer.WriteNumber(134, BidSize.Value);
			if (OfferSize is not null) writer.WriteNumber(135, OfferSize.Value);
			if (ValidUntilTime is not null) writer.WriteUtcTimeStamp(62, ValidUntilTime.Value);
			if (BidSpotRate is not null) writer.WriteNumber(188, BidSpotRate.Value);
			if (OfferSpotRate is not null) writer.WriteNumber(190, OfferSpotRate.Value);
			if (BidForwardPoints is not null) writer.WriteNumber(189, BidForwardPoints.Value);
			if (OfferForwardPoints is not null) writer.WriteNumber(191, OfferForwardPoints.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (FutSettDate is not null) writer.WriteLocalDateOnly(64, FutSettDate.Value);
			if (OrdType is not null) writer.WriteString(40, OrdType);
			if (FutSettDate2 is not null) writer.WriteLocalDateOnly(193, FutSettDate2.Value);
			if (OrderQty2 is not null) writer.WriteNumber(192, OrderQty2.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
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
			QuoteReqID = view.GetString(131);
			QuoteID = view.GetString(117);
			QuoteResponseLevel = view.GetInt32(301);
			TradingSessionID = view.GetString(336);
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
			BidPx = view.GetDouble(132);
			OfferPx = view.GetDouble(133);
			BidSize = view.GetDouble(134);
			OfferSize = view.GetDouble(135);
			ValidUntilTime = view.GetDateTime(62);
			BidSpotRate = view.GetDouble(188);
			OfferSpotRate = view.GetDouble(190);
			BidForwardPoints = view.GetDouble(189);
			OfferForwardPoints = view.GetDouble(191);
			TransactTime = view.GetDateTime(60);
			FutSettDate = view.GetDateOnly(64);
			OrdType = view.GetString(40);
			FutSettDate2 = view.GetDateOnly(193);
			OrderQty2 = view.GetDouble(192);
			Currency = view.GetString(15);
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
				case "QuoteReqID":
					value = QuoteReqID;
					break;
				case "QuoteID":
					value = QuoteID;
					break;
				case "QuoteResponseLevel":
					value = QuoteResponseLevel;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
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
				case "BidPx":
					value = BidPx;
					break;
				case "OfferPx":
					value = OfferPx;
					break;
				case "BidSize":
					value = BidSize;
					break;
				case "OfferSize":
					value = OfferSize;
					break;
				case "ValidUntilTime":
					value = ValidUntilTime;
					break;
				case "BidSpotRate":
					value = BidSpotRate;
					break;
				case "OfferSpotRate":
					value = OfferSpotRate;
					break;
				case "BidForwardPoints":
					value = BidForwardPoints;
					break;
				case "OfferForwardPoints":
					value = OfferForwardPoints;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "FutSettDate":
					value = FutSettDate;
					break;
				case "OrdType":
					value = OrdType;
					break;
				case "FutSettDate2":
					value = FutSettDate2;
					break;
				case "OrderQty2":
					value = OrderQty2;
					break;
				case "Currency":
					value = Currency;
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
			QuoteReqID = null;
			QuoteID = null;
			QuoteResponseLevel = null;
			TradingSessionID = null;
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
			BidPx = null;
			OfferPx = null;
			BidSize = null;
			OfferSize = null;
			ValidUntilTime = null;
			BidSpotRate = null;
			OfferSpotRate = null;
			BidForwardPoints = null;
			OfferForwardPoints = null;
			TransactTime = null;
			FutSettDate = null;
			OrdType = null;
			FutSettDate2 = null;
			OrderQty2 = null;
			Currency = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
