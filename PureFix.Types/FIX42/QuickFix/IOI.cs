using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("6", FixVersion.FIX42)]
	public sealed partial class IOI : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 23, Type = TagType.String, Offset = 1, Required = true)]
		public string? IOIid {get; set;}
		
		[TagDetails(Tag = 28, Type = TagType.String, Offset = 2, Required = true)]
		public string? IOITransType {get; set;}
		
		[TagDetails(Tag = 26, Type = TagType.String, Offset = 3, Required = false)]
		public string? IOIRefID {get; set;}
		
		[TagDetails(Tag = 55, Type = TagType.String, Offset = 4, Required = true)]
		public string? Symbol {get; set;}
		
		[TagDetails(Tag = 65, Type = TagType.String, Offset = 5, Required = false)]
		public string? SymbolSfx {get; set;}
		
		[TagDetails(Tag = 48, Type = TagType.String, Offset = 6, Required = false)]
		public string? SecurityID {get; set;}
		
		[TagDetails(Tag = 22, Type = TagType.String, Offset = 7, Required = false)]
		public string? IDSource {get; set;}
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 8, Required = false)]
		public string? SecurityType {get; set;}
		
		[TagDetails(Tag = 200, Type = TagType.MonthYear, Offset = 9, Required = false)]
		public MonthYear? MaturityMonthYear {get; set;}
		
		[TagDetails(Tag = 205, Type = TagType.String, Offset = 10, Required = false)]
		public string? MaturityDay {get; set;}
		
		[TagDetails(Tag = 201, Type = TagType.Int, Offset = 11, Required = false)]
		public int? PutOrCall {get; set;}
		
		[TagDetails(Tag = 202, Type = TagType.Float, Offset = 12, Required = false)]
		public double? StrikePrice {get; set;}
		
		[TagDetails(Tag = 206, Type = TagType.String, Offset = 13, Required = false)]
		public string? OptAttribute {get; set;}
		
		[TagDetails(Tag = 231, Type = TagType.Float, Offset = 14, Required = false)]
		public double? ContractMultiplier {get; set;}
		
		[TagDetails(Tag = 223, Type = TagType.Float, Offset = 15, Required = false)]
		public double? CouponRate {get; set;}
		
		[TagDetails(Tag = 207, Type = TagType.String, Offset = 16, Required = false)]
		public string? SecurityExchange {get; set;}
		
		[TagDetails(Tag = 106, Type = TagType.String, Offset = 17, Required = false)]
		public string? Issuer {get; set;}
		
		[TagDetails(Tag = 348, Type = TagType.Length, Offset = 18, Required = false, LinksToTag = 349)]
		public int? EncodedIssuerLen {get; set;}
		
		[TagDetails(Tag = 349, Type = TagType.RawData, Offset = 19, Required = false, LinksToTag = 348)]
		public byte[]? EncodedIssuer {get; set;}
		
		[TagDetails(Tag = 107, Type = TagType.String, Offset = 20, Required = false)]
		public string? SecurityDesc {get; set;}
		
		[TagDetails(Tag = 350, Type = TagType.Length, Offset = 21, Required = false, LinksToTag = 351)]
		public int? EncodedSecurityDescLen {get; set;}
		
		[TagDetails(Tag = 351, Type = TagType.RawData, Offset = 22, Required = false, LinksToTag = 350)]
		public byte[]? EncodedSecurityDesc {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 23, Required = true)]
		public string? Side {get; set;}
		
		[TagDetails(Tag = 27, Type = TagType.String, Offset = 24, Required = true)]
		public string? IOIShares {get; set;}
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 25, Required = false)]
		public double? Price {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 26, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 62, Type = TagType.UtcTimestamp, Offset = 27, Required = false)]
		public DateTime? ValidUntilTime {get; set;}
		
		[TagDetails(Tag = 25, Type = TagType.String, Offset = 28, Required = false)]
		public string? IOIQltyInd {get; set;}
		
		[TagDetails(Tag = 130, Type = TagType.Boolean, Offset = 29, Required = false)]
		public bool? IOINaturalFlag {get; set;}
		
		[Group(NoOfTag = 199, Offset = 30, Required = false)]
		public IOINoIOIQualifiers[]? NoIOIQualifiers {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 31, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 32, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 33, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 34, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 149, Type = TagType.String, Offset = 35, Required = false)]
		public string? URLLink {get; set;}
		
		[Group(NoOfTag = 215, Offset = 36, Required = false)]
		public IOINoRoutingIDs[]? NoRoutingIDs {get; set;}
		
		[TagDetails(Tag = 218, Type = TagType.Float, Offset = 37, Required = false)]
		public double? SpreadToBenchmark {get; set;}
		
		[TagDetails(Tag = 219, Type = TagType.String, Offset = 38, Required = false)]
		public string? Benchmark {get; set;}
		
		[Component(Offset = 39, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& IOIid is not null
				&& IOITransType is not null
				&& Symbol is not null
				&& Side is not null
				&& IOIShares is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (IOIid is not null) writer.WriteString(23, IOIid);
			if (IOITransType is not null) writer.WriteString(28, IOITransType);
			if (IOIRefID is not null) writer.WriteString(26, IOIRefID);
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
			if (Side is not null) writer.WriteString(54, Side);
			if (IOIShares is not null) writer.WriteString(27, IOIShares);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (ValidUntilTime is not null) writer.WriteUtcTimeStamp(62, ValidUntilTime.Value);
			if (IOIQltyInd is not null) writer.WriteString(25, IOIQltyInd);
			if (IOINaturalFlag is not null) writer.WriteBoolean(130, IOINaturalFlag.Value);
			if (NoIOIQualifiers is not null && NoIOIQualifiers.Length != 0)
			{
				writer.WriteWholeNumber(199, NoIOIQualifiers.Length);
				for (int i = 0; i < NoIOIQualifiers.Length; i++)
				{
					((IFixEncoder)NoIOIQualifiers[i]).Encode(writer);
				}
			}
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (URLLink is not null) writer.WriteString(149, URLLink);
			if (NoRoutingIDs is not null && NoRoutingIDs.Length != 0)
			{
				writer.WriteWholeNumber(215, NoRoutingIDs.Length);
				for (int i = 0; i < NoRoutingIDs.Length; i++)
				{
					((IFixEncoder)NoRoutingIDs[i]).Encode(writer);
				}
			}
			if (SpreadToBenchmark is not null) writer.WriteNumber(218, SpreadToBenchmark.Value);
			if (Benchmark is not null) writer.WriteString(219, Benchmark);
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
			IOIid = view.GetString(23);
			IOITransType = view.GetString(28);
			IOIRefID = view.GetString(26);
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
			Side = view.GetString(54);
			IOIShares = view.GetString(27);
			Price = view.GetDouble(44);
			Currency = view.GetString(15);
			ValidUntilTime = view.GetDateTime(62);
			IOIQltyInd = view.GetString(25);
			IOINaturalFlag = view.GetBool(130);
			if (view.GetView("NoIOIQualifiers") is IMessageView viewNoIOIQualifiers)
			{
				var count = viewNoIOIQualifiers.GroupCount();
				NoIOIQualifiers = new IOINoIOIQualifiers[count];
				for (int i = 0; i < count; i++)
				{
					NoIOIQualifiers[i] = new();
					((IFixParser)NoIOIQualifiers[i]).Parse(viewNoIOIQualifiers.GetGroupInstance(i));
				}
			}
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			TransactTime = view.GetDateTime(60);
			URLLink = view.GetString(149);
			if (view.GetView("NoRoutingIDs") is IMessageView viewNoRoutingIDs)
			{
				var count = viewNoRoutingIDs.GroupCount();
				NoRoutingIDs = new IOINoRoutingIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoRoutingIDs[i] = new();
					((IFixParser)NoRoutingIDs[i]).Parse(viewNoRoutingIDs.GetGroupInstance(i));
				}
			}
			SpreadToBenchmark = view.GetDouble(218);
			Benchmark = view.GetString(219);
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
				case "IOIid":
					value = IOIid;
					break;
				case "IOITransType":
					value = IOITransType;
					break;
				case "IOIRefID":
					value = IOIRefID;
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
				case "Side":
					value = Side;
					break;
				case "IOIShares":
					value = IOIShares;
					break;
				case "Price":
					value = Price;
					break;
				case "Currency":
					value = Currency;
					break;
				case "ValidUntilTime":
					value = ValidUntilTime;
					break;
				case "IOIQltyInd":
					value = IOIQltyInd;
					break;
				case "IOINaturalFlag":
					value = IOINaturalFlag;
					break;
				case "NoIOIQualifiers":
					value = NoIOIQualifiers;
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
				case "TransactTime":
					value = TransactTime;
					break;
				case "URLLink":
					value = URLLink;
					break;
				case "NoRoutingIDs":
					value = NoRoutingIDs;
					break;
				case "SpreadToBenchmark":
					value = SpreadToBenchmark;
					break;
				case "Benchmark":
					value = Benchmark;
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
			IOIid = null;
			IOITransType = null;
			IOIRefID = null;
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
			Side = null;
			IOIShares = null;
			Price = null;
			Currency = null;
			ValidUntilTime = null;
			IOIQltyInd = null;
			IOINaturalFlag = null;
			NoIOIQualifiers = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			TransactTime = null;
			URLLink = null;
			NoRoutingIDs = null;
			SpreadToBenchmark = null;
			Benchmark = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
