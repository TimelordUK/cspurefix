using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("J", FixVersion.FIX42)]
	public sealed partial class Allocation : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
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
		
		[Group(NoOfTag = 73, Offset = 6, Required = false)]
		public AllocationNoOrders[]? NoOrders {get; set;}
		
		[Group(NoOfTag = 124, Offset = 7, Required = false)]
		public AllocationNoExecs[]? NoExecs {get; set;}
		
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
		
		[TagDetails(Tag = 348, Type = TagType.Length, Offset = 23, Required = false, LinksToTag = 349)]
		public int? EncodedIssuerLen {get; set;}
		
		[TagDetails(Tag = 349, Type = TagType.RawData, Offset = 24, Required = false, LinksToTag = 348)]
		public byte[]? EncodedIssuer {get; set;}
		
		[TagDetails(Tag = 107, Type = TagType.String, Offset = 25, Required = false)]
		public string? SecurityDesc {get; set;}
		
		[TagDetails(Tag = 350, Type = TagType.Length, Offset = 26, Required = false, LinksToTag = 351)]
		public int? EncodedSecurityDescLen {get; set;}
		
		[TagDetails(Tag = 351, Type = TagType.RawData, Offset = 27, Required = false, LinksToTag = 350)]
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
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 42, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 43, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[TagDetails(Tag = 157, Type = TagType.Int, Offset = 44, Required = false)]
		public int? NumDaysInterest {get; set;}
		
		[TagDetails(Tag = 158, Type = TagType.Float, Offset = 45, Required = false)]
		public double? AccruedInterestRate {get; set;}
		
		[Group(NoOfTag = 78, Offset = 46, Required = false)]
		public AllocationNoAllocs[]? NoAllocs {get; set;}
		
		[Component(Offset = 47, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& AllocID is not null
				&& AllocTransType is not null
				&& Side is not null
				&& Symbol is not null
				&& Shares is not null
				&& AvgPx is not null
				&& TradeDate is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (AllocID is not null) writer.WriteString(70, AllocID);
			if (AllocTransType is not null) writer.WriteString(71, AllocTransType);
			if (RefAllocID is not null) writer.WriteString(72, RefAllocID);
			if (AllocLinkID is not null) writer.WriteString(196, AllocLinkID);
			if (AllocLinkType is not null) writer.WriteWholeNumber(197, AllocLinkType.Value);
			if (NoOrders is not null && NoOrders.Length != 0)
			{
				writer.WriteWholeNumber(73, NoOrders.Length);
				for (int i = 0; i < NoOrders.Length; i++)
				{
					((IFixEncoder)NoOrders[i]).Encode(writer);
				}
			}
			if (NoExecs is not null && NoExecs.Length != 0)
			{
				writer.WriteWholeNumber(124, NoExecs.Length);
				for (int i = 0; i < NoExecs.Length; i++)
				{
					((IFixEncoder)NoExecs[i]).Encode(writer);
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
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (NumDaysInterest is not null) writer.WriteWholeNumber(157, NumDaysInterest.Value);
			if (AccruedInterestRate is not null) writer.WriteNumber(158, AccruedInterestRate.Value);
			if (NoAllocs is not null && NoAllocs.Length != 0)
			{
				writer.WriteWholeNumber(78, NoAllocs.Length);
				for (int i = 0; i < NoAllocs.Length; i++)
				{
					((IFixEncoder)NoAllocs[i]).Encode(writer);
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
				NoOrders = new AllocationNoOrders[count];
				for (int i = 0; i < count; i++)
				{
					NoOrders[i] = new();
					((IFixParser)NoOrders[i]).Parse(viewNoOrders.GetGroupInstance(i));
				}
			}
			if (view.GetView("NoExecs") is IMessageView viewNoExecs)
			{
				var count = viewNoExecs.GroupCount();
				NoExecs = new AllocationNoExecs[count];
				for (int i = 0; i < count; i++)
				{
					NoExecs[i] = new();
					((IFixParser)NoExecs[i]).Parse(viewNoExecs.GetGroupInstance(i));
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
				NoAllocs = new AllocationNoAllocs[count];
				for (int i = 0; i < count; i++)
				{
					NoAllocs[i] = new();
					((IFixParser)NoAllocs[i]).Parse(viewNoAllocs.GetGroupInstance(i));
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
				case "AllocID":
					value = AllocID;
					break;
				case "AllocTransType":
					value = AllocTransType;
					break;
				case "RefAllocID":
					value = RefAllocID;
					break;
				case "AllocLinkID":
					value = AllocLinkID;
					break;
				case "AllocLinkType":
					value = AllocLinkType;
					break;
				case "NoOrders":
					value = NoOrders;
					break;
				case "NoExecs":
					value = NoExecs;
					break;
				case "Side":
					value = Side;
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
				case "Shares":
					value = Shares;
					break;
				case "LastMkt":
					value = LastMkt;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "AvgPx":
					value = AvgPx;
					break;
				case "Currency":
					value = Currency;
					break;
				case "AvgPrxPrecision":
					value = AvgPrxPrecision;
					break;
				case "TradeDate":
					value = TradeDate;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "SettlmntTyp":
					value = SettlmntTyp;
					break;
				case "FutSettDate":
					value = FutSettDate;
					break;
				case "GrossTradeAmt":
					value = GrossTradeAmt;
					break;
				case "NetMoney":
					value = NetMoney;
					break;
				case "OpenClose":
					value = OpenClose;
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
				case "NumDaysInterest":
					value = NumDaysInterest;
					break;
				case "AccruedInterestRate":
					value = AccruedInterestRate;
					break;
				case "NoAllocs":
					value = NoAllocs;
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
			AllocID = null;
			AllocTransType = null;
			RefAllocID = null;
			AllocLinkID = null;
			AllocLinkType = null;
			NoOrders = null;
			NoExecs = null;
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
			NoAllocs = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
