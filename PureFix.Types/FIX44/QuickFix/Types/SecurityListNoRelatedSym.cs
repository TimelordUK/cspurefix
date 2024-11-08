using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SecurityListNoRelatedSym : IFixGroup
	{
		[Component(Offset = 0, Required = false)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public InstrumentExtensionComponent? InstrumentExtension {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public FinancingDetailsComponent? FinancingDetails {get; set;}
		
		[Component(Offset = 3, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 4, Required = false)]
		public string? Currency {get; set;}
		
		[Component(Offset = 5, Required = false)]
		public StipulationsComponent? Stipulations {get; set;}
		
		[Component(Offset = 6, Required = false)]
		public InstrmtLegSecListGrpComponent? InstrmtLegSecListGrp {get; set;}
		
		[Component(Offset = 7, Required = false)]
		public SpreadOrBenchmarkCurveDataComponent? SpreadOrBenchmarkCurveData {get; set;}
		
		[Component(Offset = 8, Required = false)]
		public YieldDataComponent? YieldData {get; set;}
		
		[TagDetails(Tag = 561, Type = TagType.Float, Offset = 9, Required = false)]
		public double? RoundLot {get; set;}
		
		[TagDetails(Tag = 562, Type = TagType.Float, Offset = 10, Required = false)]
		public double? MinTradeVol {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 11, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 12, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		[TagDetails(Tag = 827, Type = TagType.Int, Offset = 13, Required = false)]
		public int? ExpirationCycle {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 14, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 15, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 16, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (InstrumentExtension is not null) ((IFixEncoder)InstrumentExtension).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (Stipulations is not null) ((IFixEncoder)Stipulations).Encode(writer);
			if (InstrmtLegSecListGrp is not null) ((IFixEncoder)InstrmtLegSecListGrp).Encode(writer);
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (YieldData is not null) ((IFixEncoder)YieldData).Encode(writer);
			if (RoundLot is not null) writer.WriteNumber(561, RoundLot.Value);
			if (MinTradeVol is not null) writer.WriteNumber(562, MinTradeVol.Value);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (ExpirationCycle is not null) writer.WriteWholeNumber(827, ExpirationCycle.Value);
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
			if (view.GetView("FinancingDetails") is IMessageView viewFinancingDetails)
			{
				FinancingDetails = new();
				((IFixParser)FinancingDetails).Parse(viewFinancingDetails);
			}
			if (view.GetView("UndInstrmtGrp") is IMessageView viewUndInstrmtGrp)
			{
				UndInstrmtGrp = new();
				((IFixParser)UndInstrmtGrp).Parse(viewUndInstrmtGrp);
			}
			Currency = view.GetString(15);
			if (view.GetView("Stipulations") is IMessageView viewStipulations)
			{
				Stipulations = new();
				((IFixParser)Stipulations).Parse(viewStipulations);
			}
			if (view.GetView("InstrmtLegSecListGrp") is IMessageView viewInstrmtLegSecListGrp)
			{
				InstrmtLegSecListGrp = new();
				((IFixParser)InstrmtLegSecListGrp).Parse(viewInstrmtLegSecListGrp);
			}
			if (view.GetView("SpreadOrBenchmarkCurveData") is IMessageView viewSpreadOrBenchmarkCurveData)
			{
				SpreadOrBenchmarkCurveData = new();
				((IFixParser)SpreadOrBenchmarkCurveData).Parse(viewSpreadOrBenchmarkCurveData);
			}
			if (view.GetView("YieldData") is IMessageView viewYieldData)
			{
				YieldData = new();
				((IFixParser)YieldData).Parse(viewYieldData);
			}
			RoundLot = view.GetDouble(561);
			MinTradeVol = view.GetDouble(562);
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
			ExpirationCycle = view.GetInt32(827);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "Instrument":
					value = Instrument;
					break;
				case "InstrumentExtension":
					value = InstrumentExtension;
					break;
				case "FinancingDetails":
					value = FinancingDetails;
					break;
				case "UndInstrmtGrp":
					value = UndInstrmtGrp;
					break;
				case "Currency":
					value = Currency;
					break;
				case "Stipulations":
					value = Stipulations;
					break;
				case "InstrmtLegSecListGrp":
					value = InstrmtLegSecListGrp;
					break;
				case "SpreadOrBenchmarkCurveData":
					value = SpreadOrBenchmarkCurveData;
					break;
				case "YieldData":
					value = YieldData;
					break;
				case "RoundLot":
					value = RoundLot;
					break;
				case "MinTradeVol":
					value = MinTradeVol;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "TradingSessionSubID":
					value = TradingSessionSubID;
					break;
				case "ExpirationCycle":
					value = ExpirationCycle;
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
			((IFixReset?)Instrument)?.Reset();
			((IFixReset?)InstrumentExtension)?.Reset();
			((IFixReset?)FinancingDetails)?.Reset();
			((IFixReset?)UndInstrmtGrp)?.Reset();
			Currency = null;
			((IFixReset?)Stipulations)?.Reset();
			((IFixReset?)InstrmtLegSecListGrp)?.Reset();
			((IFixReset?)SpreadOrBenchmarkCurveData)?.Reset();
			((IFixReset?)YieldData)?.Reset();
			RoundLot = null;
			MinTradeVol = null;
			TradingSessionID = null;
			TradingSessionSubID = null;
			ExpirationCycle = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
		}
	}
}
