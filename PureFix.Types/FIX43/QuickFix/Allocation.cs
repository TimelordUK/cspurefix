using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("J", FixVersion.FIX43)]
	public sealed partial class Allocation : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
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
		
		[Group(NoOfTag = 73, Offset = 8, Required = false)]
		public NoOrders[]? NoOrders {get; set;}
		
		[Group(NoOfTag = 124, Offset = 9, Required = false)]
		public NoExecs[]? NoExecs {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 10, Required = true)]
		public string? Side {get; set;}
		
		[Component(Offset = 11, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
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
		public PartiesComponent? Parties {get; set;}
		
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
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 32, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 33, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[TagDetails(Tag = 157, Type = TagType.Int, Offset = 34, Required = false)]
		public int? NumDaysInterest {get; set;}
		
		[TagDetails(Tag = 158, Type = TagType.Float, Offset = 35, Required = false)]
		public double? AccruedInterestRate {get; set;}
		
		[TagDetails(Tag = 540, Type = TagType.Float, Offset = 36, Required = false)]
		public double? TotalAccruedInterestAmt {get; set;}
		
		[TagDetails(Tag = 650, Type = TagType.Boolean, Offset = 37, Required = false)]
		public bool? LegalConfirm {get; set;}
		
		[Group(NoOfTag = 78, Offset = 38, Required = false)]
		public NoAllocs[]? NoAllocs {get; set;}
		
		[Component(Offset = 39, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& AllocID is not null
				&& AllocTransType is not null
				&& AllocType is not null
				&& Side is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& Quantity is not null
				&& AvgPx is not null
				&& TradeDate is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
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
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (NumDaysInterest is not null) writer.WriteWholeNumber(157, NumDaysInterest.Value);
			if (AccruedInterestRate is not null) writer.WriteNumber(158, AccruedInterestRate.Value);
			if (TotalAccruedInterestAmt is not null) writer.WriteNumber(540, TotalAccruedInterestAmt.Value);
			if (LegalConfirm is not null) writer.WriteBoolean(650, LegalConfirm.Value);
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
			AllocType = view.GetInt32(626);
			RefAllocID = view.GetString(72);
			AllocLinkID = view.GetString(196);
			AllocLinkType = view.GetInt32(197);
			BookingRefID = view.GetString(466);
			if (view.GetView("NoOrders") is IMessageView viewNoOrders)
			{
				var count = viewNoOrders.GroupCount();
				NoOrders = new NoOrders[count];
				for (int i = 0; i < count; i++)
				{
					NoOrders[i] = new();
					((IFixParser)NoOrders[i]).Parse(viewNoOrders.GetGroupInstance(i));
				}
			}
			if (view.GetView("NoExecs") is IMessageView viewNoExecs)
			{
				var count = viewNoExecs.GroupCount();
				NoExecs = new NoExecs[count];
				for (int i = 0; i < count; i++)
				{
					NoExecs[i] = new();
					((IFixParser)NoExecs[i]).Parse(viewNoExecs.GetGroupInstance(i));
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
				NoAllocs = new NoAllocs[count];
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
				case "AllocType":
					value = AllocType;
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
				case "BookingRefID":
					value = BookingRefID;
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
				case "Instrument":
					value = Instrument;
					break;
				case "Quantity":
					value = Quantity;
					break;
				case "LastMkt":
					value = LastMkt;
					break;
				case "TradeOriginationDate":
					value = TradeOriginationDate;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "TradingSessionSubID":
					value = TradingSessionSubID;
					break;
				case "PriceType":
					value = PriceType;
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
				case "Parties":
					value = Parties;
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
				case "Concession":
					value = Concession;
					break;
				case "TotalTakedown":
					value = TotalTakedown;
					break;
				case "NetMoney":
					value = NetMoney;
					break;
				case "PositionEffect":
					value = PositionEffect;
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
				case "TotalAccruedInterestAmt":
					value = TotalAccruedInterestAmt;
					break;
				case "LegalConfirm":
					value = LegalConfirm;
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
			AllocType = null;
			RefAllocID = null;
			AllocLinkID = null;
			AllocLinkType = null;
			BookingRefID = null;
			NoOrders = null;
			NoExecs = null;
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
			NoAllocs = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
