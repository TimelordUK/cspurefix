using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44.Components;

namespace PureFix.Types.FIX44
{
	[MessageType("d", FixVersion.FIX44)]
	public sealed partial class SecurityDefinition : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 320, Type = TagType.String, Offset = 1, Required = true)]
		public string? SecurityReqID {get; set;}
		
		[TagDetails(Tag = 322, Type = TagType.String, Offset = 2, Required = true)]
		public string? SecurityResponseID {get; set;}
		
		[TagDetails(Tag = 323, Type = TagType.Int, Offset = 3, Required = true)]
		public int? SecurityResponseType {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public Instrument? Instrument {get; set;}
		
		[Component(Offset = 5, Required = false)]
		public InstrumentExtension? InstrumentExtension {get; set;}
		
		[Component(Offset = 6, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 7, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 8, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 9, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 10, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 11, Required = false)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 12, Required = false)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 13, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp {get; set;}
		
		[TagDetails(Tag = 827, Type = TagType.Int, Offset = 14, Required = false)]
		public int? ExpirationCycle {get; set;}
		
		[TagDetails(Tag = 561, Type = TagType.Float, Offset = 15, Required = false)]
		public double? RoundLot {get; set;}
		
		[TagDetails(Tag = 562, Type = TagType.Float, Offset = 16, Required = false)]
		public double? MinTradeVol {get; set;}
		
		[Component(Offset = 17, Required = true)]
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
			if (SecurityReqID is not null) writer.WriteString(320, SecurityReqID);
			if (SecurityResponseID is not null) writer.WriteString(322, SecurityResponseID);
			if (SecurityResponseType is not null) writer.WriteWholeNumber(323, SecurityResponseType.Value);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (InstrumentExtension is not null) ((IFixEncoder)InstrumentExtension).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
			if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (ExpirationCycle is not null) writer.WriteWholeNumber(827, ExpirationCycle.Value);
			if (RoundLot is not null) writer.WriteNumber(561, RoundLot.Value);
			if (MinTradeVol is not null) writer.WriteNumber(562, MinTradeVol.Value);
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
			SecurityResponseID = view.GetString(322);
			SecurityResponseType = view.GetInt32(323);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			if (view.GetView("InstrumentExtension") is IMessageView viewInstrumentExtension)
			{
				InstrumentExtension = new();
				((IFixParser)InstrumentExtension).Parse(viewInstrumentExtension);
			}
			if (view.GetView("UndInstrmtGrp") is IMessageView viewUndInstrmtGrp)
			{
				UndInstrmtGrp = new();
				((IFixParser)UndInstrmtGrp).Parse(viewUndInstrmtGrp);
			}
			Currency = view.GetString(15);
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			if (view.GetView("InstrmtLegGrp") is IMessageView viewInstrmtLegGrp)
			{
				InstrmtLegGrp = new();
				((IFixParser)InstrmtLegGrp).Parse(viewInstrmtLegGrp);
			}
			ExpirationCycle = view.GetInt32(827);
			RoundLot = view.GetDouble(561);
			MinTradeVol = view.GetDouble(562);
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
				case "SecurityReqID":
				{
					value = SecurityReqID;
					break;
				}
				case "SecurityResponseID":
				{
					value = SecurityResponseID;
					break;
				}
				case "SecurityResponseType":
				{
					value = SecurityResponseType;
					break;
				}
				case "Instrument":
				{
					value = Instrument;
					break;
				}
				case "InstrumentExtension":
				{
					value = InstrumentExtension;
					break;
				}
				case "UndInstrmtGrp":
				{
					value = UndInstrmtGrp;
					break;
				}
				case "Currency":
				{
					value = Currency;
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
				case "InstrmtLegGrp":
				{
					value = InstrmtLegGrp;
					break;
				}
				case "ExpirationCycle":
				{
					value = ExpirationCycle;
					break;
				}
				case "RoundLot":
				{
					value = RoundLot;
					break;
				}
				case "MinTradeVol":
				{
					value = MinTradeVol;
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
			SecurityReqID = null;
			SecurityResponseID = null;
			SecurityResponseType = null;
			((IFixReset?)Instrument)?.Reset();
			((IFixReset?)InstrumentExtension)?.Reset();
			((IFixReset?)UndInstrmtGrp)?.Reset();
			Currency = null;
			TradingSessionID = null;
			TradingSessionSubID = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)InstrmtLegGrp)?.Reset();
			ExpirationCycle = null;
			RoundLot = null;
			MinTradeVol = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
