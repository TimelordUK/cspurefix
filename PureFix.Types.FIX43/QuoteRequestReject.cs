using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX43.Components;

namespace PureFix.Types.FIX43
{
	[MessageType("AG", FixVersion.FIX43)]
	public sealed partial class QuoteRequestReject : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 131, Type = TagType.String, Offset = 1, Required = true)]
		public string? QuoteReqID {get; set;}
		
		[TagDetails(Tag = 644, Type = TagType.String, Offset = 2, Required = false)]
		public string? RFQReqID {get; set;}
		
		[TagDetails(Tag = 658, Type = TagType.Int, Offset = 3, Required = true)]
		public int? QuoteRequestRejectReason {get; set;}
		
		public sealed partial class NoRelatedSym : IFixGroup
		{
			[Component(Offset = 0, Required = true)]
			public Instrument? Instrument {get; set;}
			
			[TagDetails(Tag = 140, Type = TagType.Float, Offset = 1, Required = false)]
			public double? PrevClosePx {get; set;}
			
			[TagDetails(Tag = 303, Type = TagType.Int, Offset = 2, Required = false)]
			public int? QuoteRequestType {get; set;}
			
			[TagDetails(Tag = 537, Type = TagType.Int, Offset = 3, Required = false)]
			public int? QuoteType {get; set;}
			
			[TagDetails(Tag = 336, Type = TagType.String, Offset = 4, Required = false)]
			public string? TradingSessionID {get; set;}
			
			[TagDetails(Tag = 625, Type = TagType.String, Offset = 5, Required = false)]
			public string? TradingSessionSubID {get; set;}
			
			[TagDetails(Tag = 229, Type = TagType.String, Offset = 6, Required = false)]
			public string? TradeOriginationDate {get; set;}
			
			[Component(Offset = 7, Required = false)]
			public Stipulations? Stipulations {get; set;}
			
			[TagDetails(Tag = 54, Type = TagType.String, Offset = 8, Required = false)]
			public string? Side {get; set;}
			
			[TagDetails(Tag = 465, Type = TagType.Int, Offset = 9, Required = false)]
			public int? QuantityType {get; set;}
			
			[TagDetails(Tag = 38, Type = TagType.Float, Offset = 10, Required = false)]
			public double? OrderQty {get; set;}
			
			[TagDetails(Tag = 152, Type = TagType.Float, Offset = 11, Required = false)]
			public double? CashOrderQty {get; set;}
			
			[TagDetails(Tag = 63, Type = TagType.String, Offset = 12, Required = false)]
			public string? SettlmntTyp {get; set;}
			
			[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 13, Required = false)]
			public DateOnly? FutSettDate {get; set;}
			
			[TagDetails(Tag = 40, Type = TagType.String, Offset = 14, Required = false)]
			public string? OrdType {get; set;}
			
			[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 15, Required = false)]
			public DateOnly? FutSettDate2 {get; set;}
			
			[TagDetails(Tag = 192, Type = TagType.Float, Offset = 16, Required = false)]
			public double? OrderQty2 {get; set;}
			
			[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 17, Required = false)]
			public DateTime? ExpireTime {get; set;}
			
			[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 18, Required = false)]
			public DateTime? TransactTime {get; set;}
			
			[TagDetails(Tag = 15, Type = TagType.String, Offset = 19, Required = false)]
			public string? Currency {get; set;}
			
			[Component(Offset = 20, Required = false)]
			public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData {get; set;}
			
			[TagDetails(Tag = 423, Type = TagType.Int, Offset = 21, Required = false)]
			public int? PriceType {get; set;}
			
			[TagDetails(Tag = 44, Type = TagType.Float, Offset = 22, Required = false)]
			public double? Price {get; set;}
			
			[TagDetails(Tag = 640, Type = TagType.Float, Offset = 23, Required = false)]
			public double? Price2 {get; set;}
			
			[Component(Offset = 24, Required = false)]
			public YieldData? YieldData {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
				if (PrevClosePx is not null) writer.WriteNumber(140, PrevClosePx.Value);
				if (QuoteRequestType is not null) writer.WriteWholeNumber(303, QuoteRequestType.Value);
				if (QuoteType is not null) writer.WriteWholeNumber(537, QuoteType.Value);
				if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
				if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
				if (TradeOriginationDate is not null) writer.WriteString(229, TradeOriginationDate);
				if (Stipulations is not null) ((IFixEncoder)Stipulations).Encode(writer);
				if (Side is not null) writer.WriteString(54, Side);
				if (QuantityType is not null) writer.WriteWholeNumber(465, QuantityType.Value);
				if (OrderQty is not null) writer.WriteNumber(38, OrderQty.Value);
				if (CashOrderQty is not null) writer.WriteNumber(152, CashOrderQty.Value);
				if (SettlmntTyp is not null) writer.WriteString(63, SettlmntTyp);
				if (FutSettDate is not null) writer.WriteLocalDateOnly(64, FutSettDate.Value);
				if (OrdType is not null) writer.WriteString(40, OrdType);
				if (FutSettDate2 is not null) writer.WriteLocalDateOnly(193, FutSettDate2.Value);
				if (OrderQty2 is not null) writer.WriteNumber(192, OrderQty2.Value);
				if (ExpireTime is not null) writer.WriteUtcTimeStamp(126, ExpireTime.Value);
				if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
				if (Currency is not null) writer.WriteString(15, Currency);
				if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
				if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
				if (Price is not null) writer.WriteNumber(44, Price.Value);
				if (Price2 is not null) writer.WriteNumber(640, Price2.Value);
				if (YieldData is not null) ((IFixEncoder)YieldData).Encode(writer);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				if (view.GetView("Instrument") is IMessageView viewInstrument)
				{
					Instrument = new();
					((IFixParser)Instrument).Parse(viewInstrument);
				}
				PrevClosePx = view.GetDouble(140);
				QuoteRequestType = view.GetInt32(303);
				QuoteType = view.GetInt32(537);
				TradingSessionID = view.GetString(336);
				TradingSessionSubID = view.GetString(625);
				TradeOriginationDate = view.GetString(229);
				if (view.GetView("Stipulations") is IMessageView viewStipulations)
				{
					Stipulations = new();
					((IFixParser)Stipulations).Parse(viewStipulations);
				}
				Side = view.GetString(54);
				QuantityType = view.GetInt32(465);
				OrderQty = view.GetDouble(38);
				CashOrderQty = view.GetDouble(152);
				SettlmntTyp = view.GetString(63);
				FutSettDate = view.GetDateOnly(64);
				OrdType = view.GetString(40);
				FutSettDate2 = view.GetDateOnly(193);
				OrderQty2 = view.GetDouble(192);
				ExpireTime = view.GetDateTime(126);
				TransactTime = view.GetDateTime(60);
				Currency = view.GetString(15);
				if (view.GetView("SpreadOrBenchmarkCurveData") is IMessageView viewSpreadOrBenchmarkCurveData)
				{
					SpreadOrBenchmarkCurveData = new();
					((IFixParser)SpreadOrBenchmarkCurveData).Parse(viewSpreadOrBenchmarkCurveData);
				}
				PriceType = view.GetInt32(423);
				Price = view.GetDouble(44);
				Price2 = view.GetDouble(640);
				if (view.GetView("YieldData") is IMessageView viewYieldData)
				{
					YieldData = new();
					((IFixParser)YieldData).Parse(viewYieldData);
				}
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "Instrument":
					{
						value = Instrument;
						break;
					}
					case "PrevClosePx":
					{
						value = PrevClosePx;
						break;
					}
					case "QuoteRequestType":
					{
						value = QuoteRequestType;
						break;
					}
					case "QuoteType":
					{
						value = QuoteType;
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
					case "TradeOriginationDate":
					{
						value = TradeOriginationDate;
						break;
					}
					case "Stipulations":
					{
						value = Stipulations;
						break;
					}
					case "Side":
					{
						value = Side;
						break;
					}
					case "QuantityType":
					{
						value = QuantityType;
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
					case "OrdType":
					{
						value = OrdType;
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
					case "ExpireTime":
					{
						value = ExpireTime;
						break;
					}
					case "TransactTime":
					{
						value = TransactTime;
						break;
					}
					case "Currency":
					{
						value = Currency;
						break;
					}
					case "SpreadOrBenchmarkCurveData":
					{
						value = SpreadOrBenchmarkCurveData;
						break;
					}
					case "PriceType":
					{
						value = PriceType;
						break;
					}
					case "Price":
					{
						value = Price;
						break;
					}
					case "Price2":
					{
						value = Price2;
						break;
					}
					case "YieldData":
					{
						value = YieldData;
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
				((IFixReset?)Instrument)?.Reset();
				PrevClosePx = null;
				QuoteRequestType = null;
				QuoteType = null;
				TradingSessionID = null;
				TradingSessionSubID = null;
				TradeOriginationDate = null;
				((IFixReset?)Stipulations)?.Reset();
				Side = null;
				QuantityType = null;
				OrderQty = null;
				CashOrderQty = null;
				SettlmntTyp = null;
				FutSettDate = null;
				OrdType = null;
				FutSettDate2 = null;
				OrderQty2 = null;
				ExpireTime = null;
				TransactTime = null;
				Currency = null;
				((IFixReset?)SpreadOrBenchmarkCurveData)?.Reset();
				PriceType = null;
				Price = null;
				Price2 = null;
				((IFixReset?)YieldData)?.Reset();
			}
		}
		[Group(NoOfTag = 146, Offset = 4, Required = true)]
		public NoRelatedSym[]? RelatedSym {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 5, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 6, Required = false)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 7, Required = false)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 8, Required = true)]
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
			if (QuoteReqID is not null) writer.WriteString(131, QuoteReqID);
			if (RFQReqID is not null) writer.WriteString(644, RFQReqID);
			if (QuoteRequestRejectReason is not null) writer.WriteWholeNumber(658, QuoteRequestRejectReason.Value);
			if (RelatedSym is not null && RelatedSym.Length != 0)
			{
				writer.WriteWholeNumber(146, RelatedSym.Length);
				for (int i = 0; i < RelatedSym.Length; i++)
				{
					((IFixEncoder)RelatedSym[i]).Encode(writer);
				}
			}
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
			if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
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
			RFQReqID = view.GetString(644);
			QuoteRequestRejectReason = view.GetInt32(658);
			if (view.GetView("NoRelatedSym") is IMessageView viewNoRelatedSym)
			{
				var count = viewNoRelatedSym.GroupCount();
				RelatedSym = new NoRelatedSym[count];
				for (int i = 0; i < count; i++)
				{
					RelatedSym[i] = new();
					((IFixParser)RelatedSym[i]).Parse(viewNoRelatedSym.GetGroupInstance(i));
				}
			}
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
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
				case "QuoteReqID":
				{
					value = QuoteReqID;
					break;
				}
				case "RFQReqID":
				{
					value = RFQReqID;
					break;
				}
				case "QuoteRequestRejectReason":
				{
					value = QuoteRequestRejectReason;
					break;
				}
				case "NoRelatedSym":
				{
					value = RelatedSym;
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
			QuoteReqID = null;
			RFQReqID = null;
			QuoteRequestRejectReason = null;
			RelatedSym = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
