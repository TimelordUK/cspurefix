using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("f", FixVersion.FIX42)]
	public sealed partial class SecurityStatus : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 324, Type = TagType.String, Offset = 1, Required = false)]
		public string? SecurityStatusReqID {get; set;}
		
		[TagDetails(Tag = 55, Type = TagType.String, Offset = 2, Required = true)]
		public string? Symbol {get; set;}
		
		[TagDetails(Tag = 65, Type = TagType.String, Offset = 3, Required = false)]
		public string? SymbolSfx {get; set;}
		
		[TagDetails(Tag = 48, Type = TagType.String, Offset = 4, Required = false)]
		public string? SecurityID {get; set;}
		
		[TagDetails(Tag = 22, Type = TagType.String, Offset = 5, Required = false)]
		public string? IDSource {get; set;}
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 6, Required = false)]
		public string? SecurityType {get; set;}
		
		[TagDetails(Tag = 200, Type = TagType.MonthYear, Offset = 7, Required = false)]
		public MonthYear? MaturityMonthYear {get; set;}
		
		[TagDetails(Tag = 205, Type = TagType.String, Offset = 8, Required = false)]
		public string? MaturityDay {get; set;}
		
		[TagDetails(Tag = 201, Type = TagType.Int, Offset = 9, Required = false)]
		public int? PutOrCall {get; set;}
		
		[TagDetails(Tag = 202, Type = TagType.Float, Offset = 10, Required = false)]
		public double? StrikePrice {get; set;}
		
		[TagDetails(Tag = 206, Type = TagType.String, Offset = 11, Required = false)]
		public string? OptAttribute {get; set;}
		
		[TagDetails(Tag = 231, Type = TagType.Float, Offset = 12, Required = false)]
		public double? ContractMultiplier {get; set;}
		
		[TagDetails(Tag = 223, Type = TagType.Float, Offset = 13, Required = false)]
		public double? CouponRate {get; set;}
		
		[TagDetails(Tag = 207, Type = TagType.String, Offset = 14, Required = false)]
		public string? SecurityExchange {get; set;}
		
		[TagDetails(Tag = 106, Type = TagType.String, Offset = 15, Required = false)]
		public string? Issuer {get; set;}
		
		[TagDetails(Tag = 348, Type = TagType.Length, Offset = 16, Required = false, LinksToTag = 349)]
		public int? EncodedIssuerLen {get; set;}
		
		[TagDetails(Tag = 349, Type = TagType.RawData, Offset = 17, Required = false, LinksToTag = 348)]
		public byte[]? EncodedIssuer {get; set;}
		
		[TagDetails(Tag = 107, Type = TagType.String, Offset = 18, Required = false)]
		public string? SecurityDesc {get; set;}
		
		[TagDetails(Tag = 350, Type = TagType.Length, Offset = 19, Required = false, LinksToTag = 351)]
		public int? EncodedSecurityDescLen {get; set;}
		
		[TagDetails(Tag = 351, Type = TagType.RawData, Offset = 20, Required = false, LinksToTag = 350)]
		public byte[]? EncodedSecurityDesc {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 21, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 22, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 325, Type = TagType.Boolean, Offset = 23, Required = false)]
		public bool? UnsolicitedIndicator {get; set;}
		
		[TagDetails(Tag = 326, Type = TagType.Int, Offset = 24, Required = false)]
		public int? SecurityTradingStatus {get; set;}
		
		[TagDetails(Tag = 291, Type = TagType.String, Offset = 25, Required = false)]
		public string? FinancialStatus {get; set;}
		
		[TagDetails(Tag = 292, Type = TagType.String, Offset = 26, Required = false)]
		public string? CorporateAction {get; set;}
		
		[TagDetails(Tag = 327, Type = TagType.String, Offset = 27, Required = false)]
		public string? HaltReasonChar {get; set;}
		
		[TagDetails(Tag = 328, Type = TagType.Boolean, Offset = 28, Required = false)]
		public bool? InViewOfCommon {get; set;}
		
		[TagDetails(Tag = 329, Type = TagType.Boolean, Offset = 29, Required = false)]
		public bool? DueToRelated {get; set;}
		
		[TagDetails(Tag = 330, Type = TagType.Float, Offset = 30, Required = false)]
		public double? BuyVolume {get; set;}
		
		[TagDetails(Tag = 331, Type = TagType.Float, Offset = 31, Required = false)]
		public double? SellVolume {get; set;}
		
		[TagDetails(Tag = 332, Type = TagType.Float, Offset = 32, Required = false)]
		public double? HighPx {get; set;}
		
		[TagDetails(Tag = 333, Type = TagType.Float, Offset = 33, Required = false)]
		public double? LowPx {get; set;}
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 34, Required = false)]
		public double? LastPx {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 35, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 334, Type = TagType.Int, Offset = 36, Required = false)]
		public int? Adjustment {get; set;}
		
		[Component(Offset = 37, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& Symbol is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (SecurityStatusReqID is not null) writer.WriteString(324, SecurityStatusReqID);
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
			if (Currency is not null) writer.WriteString(15, Currency);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (UnsolicitedIndicator is not null) writer.WriteBoolean(325, UnsolicitedIndicator.Value);
			if (SecurityTradingStatus is not null) writer.WriteWholeNumber(326, SecurityTradingStatus.Value);
			if (FinancialStatus is not null) writer.WriteString(291, FinancialStatus);
			if (CorporateAction is not null) writer.WriteString(292, CorporateAction);
			if (HaltReasonChar is not null) writer.WriteString(327, HaltReasonChar);
			if (InViewOfCommon is not null) writer.WriteBoolean(328, InViewOfCommon.Value);
			if (DueToRelated is not null) writer.WriteBoolean(329, DueToRelated.Value);
			if (BuyVolume is not null) writer.WriteNumber(330, BuyVolume.Value);
			if (SellVolume is not null) writer.WriteNumber(331, SellVolume.Value);
			if (HighPx is not null) writer.WriteNumber(332, HighPx.Value);
			if (LowPx is not null) writer.WriteNumber(333, LowPx.Value);
			if (LastPx is not null) writer.WriteNumber(31, LastPx.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (Adjustment is not null) writer.WriteWholeNumber(334, Adjustment.Value);
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
			SecurityStatusReqID = view.GetString(324);
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
			Currency = view.GetString(15);
			TradingSessionID = view.GetString(336);
			UnsolicitedIndicator = view.GetBool(325);
			SecurityTradingStatus = view.GetInt32(326);
			FinancialStatus = view.GetString(291);
			CorporateAction = view.GetString(292);
			HaltReasonChar = view.GetString(327);
			InViewOfCommon = view.GetBool(328);
			DueToRelated = view.GetBool(329);
			BuyVolume = view.GetDouble(330);
			SellVolume = view.GetDouble(331);
			HighPx = view.GetDouble(332);
			LowPx = view.GetDouble(333);
			LastPx = view.GetDouble(31);
			TransactTime = view.GetDateTime(60);
			Adjustment = view.GetInt32(334);
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
				case "SecurityStatusReqID":
					value = SecurityStatusReqID;
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
				case "Currency":
					value = Currency;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "UnsolicitedIndicator":
					value = UnsolicitedIndicator;
					break;
				case "SecurityTradingStatus":
					value = SecurityTradingStatus;
					break;
				case "FinancialStatus":
					value = FinancialStatus;
					break;
				case "CorporateAction":
					value = CorporateAction;
					break;
				case "HaltReasonChar":
					value = HaltReasonChar;
					break;
				case "InViewOfCommon":
					value = InViewOfCommon;
					break;
				case "DueToRelated":
					value = DueToRelated;
					break;
				case "BuyVolume":
					value = BuyVolume;
					break;
				case "SellVolume":
					value = SellVolume;
					break;
				case "HighPx":
					value = HighPx;
					break;
				case "LowPx":
					value = LowPx;
					break;
				case "LastPx":
					value = LastPx;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "Adjustment":
					value = Adjustment;
					break;
				case "StandardTrailer":
					value = StandardTrailer;
					break;
				default: return false;
			}
			return true;
		}
	}
}
