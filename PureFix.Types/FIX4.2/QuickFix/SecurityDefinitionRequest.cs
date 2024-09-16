using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("c", FixVersion.FIX42)]
	public sealed partial class SecurityDefinitionRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 320, Type = TagType.String, Offset = 1, Required = true)]
		public string? SecurityReqID {get; set;}
		
		[TagDetails(Tag = 321, Type = TagType.Int, Offset = 2, Required = true)]
		public int? SecurityRequestType {get; set;}
		
		[TagDetails(Tag = 55, Type = TagType.String, Offset = 3, Required = false)]
		public string? Symbol {get; set;}
		
		[TagDetails(Tag = 65, Type = TagType.String, Offset = 4, Required = false)]
		public string? SymbolSfx {get; set;}
		
		[TagDetails(Tag = 48, Type = TagType.String, Offset = 5, Required = false)]
		public string? SecurityID {get; set;}
		
		[TagDetails(Tag = 22, Type = TagType.String, Offset = 6, Required = false)]
		public string? IDSource {get; set;}
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 7, Required = false)]
		public string? SecurityType {get; set;}
		
		[TagDetails(Tag = 200, Type = TagType.MonthYear, Offset = 8, Required = false)]
		public MonthYear? MaturityMonthYear {get; set;}
		
		[TagDetails(Tag = 205, Type = TagType.String, Offset = 9, Required = false)]
		public string? MaturityDay {get; set;}
		
		[TagDetails(Tag = 201, Type = TagType.Int, Offset = 10, Required = false)]
		public int? PutOrCall {get; set;}
		
		[TagDetails(Tag = 202, Type = TagType.Float, Offset = 11, Required = false)]
		public double? StrikePrice {get; set;}
		
		[TagDetails(Tag = 206, Type = TagType.String, Offset = 12, Required = false)]
		public string? OptAttribute {get; set;}
		
		[TagDetails(Tag = 231, Type = TagType.Float, Offset = 13, Required = false)]
		public double? ContractMultiplier {get; set;}
		
		[TagDetails(Tag = 223, Type = TagType.Float, Offset = 14, Required = false)]
		public double? CouponRate {get; set;}
		
		[TagDetails(Tag = 207, Type = TagType.String, Offset = 15, Required = false)]
		public string? SecurityExchange {get; set;}
		
		[TagDetails(Tag = 106, Type = TagType.String, Offset = 16, Required = false)]
		public string? Issuer {get; set;}
		
		[TagDetails(Tag = 348, Type = TagType.Length, Offset = 17, Required = false, LinksToTag = 349)]
		public int? EncodedIssuerLen {get; set;}
		
		[TagDetails(Tag = 349, Type = TagType.RawData, Offset = 18, Required = false, LinksToTag = 348)]
		public byte[]? EncodedIssuer {get; set;}
		
		[TagDetails(Tag = 107, Type = TagType.String, Offset = 19, Required = false)]
		public string? SecurityDesc {get; set;}
		
		[TagDetails(Tag = 350, Type = TagType.Length, Offset = 20, Required = false, LinksToTag = 351)]
		public int? EncodedSecurityDescLen {get; set;}
		
		[TagDetails(Tag = 351, Type = TagType.RawData, Offset = 21, Required = false, LinksToTag = 350)]
		public byte[]? EncodedSecurityDesc {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 22, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 23, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 24, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 25, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 26, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[Group(NoOfTag = 146, Offset = 27, Required = false)]
		public NoRelatedSym[]? NoRelatedSym {get; set;}
		
		[Component(Offset = 28, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& SecurityReqID is not null
				&& SecurityRequestType is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (SecurityReqID is not null) writer.WriteString(320, SecurityReqID);
			if (SecurityRequestType is not null) writer.WriteWholeNumber(321, SecurityRequestType.Value);
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
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (NoRelatedSym is not null && NoRelatedSym.Length != 0)
			{
				writer.WriteWholeNumber(146, NoRelatedSym.Length);
				for (int i = 0; i < NoRelatedSym.Length; i++)
				{
					((IFixEncoder)NoRelatedSym[i]).Encode(writer);
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
			SecurityReqID = view.GetString(320);
			SecurityRequestType = view.GetInt32(321);
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
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			TradingSessionID = view.GetString(336);
			if (view.GetView("NoRelatedSym") is IMessageView viewNoRelatedSym)
			{
				var count = viewNoRelatedSym.GroupCount();
				NoRelatedSym = new NoRelatedSym[count];
				for (int i = 0; i < count; i++)
				{
					NoRelatedSym[i] = new();
					((IFixParser)NoRelatedSym[i]).Parse(viewNoRelatedSym.GetGroupInstance(i));
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
					value = StandardHeader;
					break;
				case "SecurityReqID":
					value = SecurityReqID;
					break;
				case "SecurityRequestType":
					value = SecurityRequestType;
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
				case "Text":
					value = Text;
					break;
				case "EncodedTextLen":
					value = EncodedTextLen;
					break;
				case "EncodedText":
					value = EncodedText;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "NoRelatedSym":
					value = NoRelatedSym;
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
