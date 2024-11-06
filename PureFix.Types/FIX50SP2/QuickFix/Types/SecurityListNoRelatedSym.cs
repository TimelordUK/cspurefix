using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SecurityListNoRelatedSym : IFixGroup
	{
		[Component(Offset = 0, Required = false)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public InstrumentExtensionComponent? InstrumentExtension {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public SecurityClassificationGrpComponent? SecurityClassificationGrp {get; set;}
		
		[Component(Offset = 3, Required = false)]
		public FinancingDetailsComponent? FinancingDetails {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public SecurityTradingRulesComponent? SecurityTradingRules {get; set;}
		
		[Component(Offset = 5, Required = false)]
		public StrikeRulesComponent? StrikeRules {get; set;}
		
		[Component(Offset = 6, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 7, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 2897, Type = TagType.String, Offset = 8, Required = false)]
		public string? CurrencyCodeSource {get; set;}
		
		[Component(Offset = 9, Required = false)]
		public StipulationsComponent? Stipulations {get; set;}
		
		[Component(Offset = 10, Required = false)]
		public InstrmtLegSecListGrpComponent? InstrmtLegSecListGrp {get; set;}
		
		[Component(Offset = 11, Required = false)]
		public RelatedInstrumentGrpComponent? RelatedInstrumentGrp {get; set;}
		
		[Component(Offset = 12, Required = false)]
		public SpreadOrBenchmarkCurveDataComponent? SpreadOrBenchmarkCurveData {get; set;}
		
		[Component(Offset = 13, Required = false)]
		public YieldDataComponent? YieldData {get; set;}
		
		[Component(Offset = 14, Required = false)]
		public PriceMovementGrpComponent? PriceMovementGrp {get; set;}
		
		[TagDetails(Tag = 1504, Type = TagType.UtcTimestamp, Offset = 15, Required = false)]
		public DateTime? RelSymTransactTime {get; set;}
		
		[TagDetails(Tag = 1606, Type = TagType.Int, Offset = 16, Required = false)]
		public int? NumOfSimpleInstruments {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 17, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 18, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 19, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (InstrumentExtension is not null) ((IFixEncoder)InstrumentExtension).Encode(writer);
			if (SecurityClassificationGrp is not null) ((IFixEncoder)SecurityClassificationGrp).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (SecurityTradingRules is not null) ((IFixEncoder)SecurityTradingRules).Encode(writer);
			if (StrikeRules is not null) ((IFixEncoder)StrikeRules).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (CurrencyCodeSource is not null) writer.WriteString(2897, CurrencyCodeSource);
			if (Stipulations is not null) ((IFixEncoder)Stipulations).Encode(writer);
			if (InstrmtLegSecListGrp is not null) ((IFixEncoder)InstrmtLegSecListGrp).Encode(writer);
			if (RelatedInstrumentGrp is not null) ((IFixEncoder)RelatedInstrumentGrp).Encode(writer);
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (YieldData is not null) ((IFixEncoder)YieldData).Encode(writer);
			if (PriceMovementGrp is not null) ((IFixEncoder)PriceMovementGrp).Encode(writer);
			if (RelSymTransactTime is not null) writer.WriteUtcTimeStamp(1504, RelSymTransactTime.Value);
			if (NumOfSimpleInstruments is not null) writer.WriteWholeNumber(1606, NumOfSimpleInstruments.Value);
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
			if (view.GetView("SecurityClassificationGrp") is IMessageView viewSecurityClassificationGrp)
			{
				SecurityClassificationGrp = new();
				((IFixParser)SecurityClassificationGrp).Parse(viewSecurityClassificationGrp);
			}
			if (view.GetView("FinancingDetails") is IMessageView viewFinancingDetails)
			{
				FinancingDetails = new();
				((IFixParser)FinancingDetails).Parse(viewFinancingDetails);
			}
			if (view.GetView("SecurityTradingRules") is IMessageView viewSecurityTradingRules)
			{
				SecurityTradingRules = new();
				((IFixParser)SecurityTradingRules).Parse(viewSecurityTradingRules);
			}
			if (view.GetView("StrikeRules") is IMessageView viewStrikeRules)
			{
				StrikeRules = new();
				((IFixParser)StrikeRules).Parse(viewStrikeRules);
			}
			if (view.GetView("UndInstrmtGrp") is IMessageView viewUndInstrmtGrp)
			{
				UndInstrmtGrp = new();
				((IFixParser)UndInstrmtGrp).Parse(viewUndInstrmtGrp);
			}
			Currency = view.GetString(15);
			CurrencyCodeSource = view.GetString(2897);
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
			if (view.GetView("RelatedInstrumentGrp") is IMessageView viewRelatedInstrumentGrp)
			{
				RelatedInstrumentGrp = new();
				((IFixParser)RelatedInstrumentGrp).Parse(viewRelatedInstrumentGrp);
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
			if (view.GetView("PriceMovementGrp") is IMessageView viewPriceMovementGrp)
			{
				PriceMovementGrp = new();
				((IFixParser)PriceMovementGrp).Parse(viewPriceMovementGrp);
			}
			RelSymTransactTime = view.GetDateTime(1504);
			NumOfSimpleInstruments = view.GetInt32(1606);
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
				case "SecurityClassificationGrp":
					value = SecurityClassificationGrp;
					break;
				case "FinancingDetails":
					value = FinancingDetails;
					break;
				case "SecurityTradingRules":
					value = SecurityTradingRules;
					break;
				case "StrikeRules":
					value = StrikeRules;
					break;
				case "UndInstrmtGrp":
					value = UndInstrmtGrp;
					break;
				case "Currency":
					value = Currency;
					break;
				case "CurrencyCodeSource":
					value = CurrencyCodeSource;
					break;
				case "Stipulations":
					value = Stipulations;
					break;
				case "InstrmtLegSecListGrp":
					value = InstrmtLegSecListGrp;
					break;
				case "RelatedInstrumentGrp":
					value = RelatedInstrumentGrp;
					break;
				case "SpreadOrBenchmarkCurveData":
					value = SpreadOrBenchmarkCurveData;
					break;
				case "YieldData":
					value = YieldData;
					break;
				case "PriceMovementGrp":
					value = PriceMovementGrp;
					break;
				case "RelSymTransactTime":
					value = RelSymTransactTime;
					break;
				case "NumOfSimpleInstruments":
					value = NumOfSimpleInstruments;
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
			((IFixReset?)SecurityClassificationGrp)?.Reset();
			((IFixReset?)FinancingDetails)?.Reset();
			((IFixReset?)SecurityTradingRules)?.Reset();
			((IFixReset?)StrikeRules)?.Reset();
			((IFixReset?)UndInstrmtGrp)?.Reset();
			Currency = null;
			CurrencyCodeSource = null;
			((IFixReset?)Stipulations)?.Reset();
			((IFixReset?)InstrmtLegSecListGrp)?.Reset();
			((IFixReset?)RelatedInstrumentGrp)?.Reset();
			((IFixReset?)SpreadOrBenchmarkCurveData)?.Reset();
			((IFixReset?)YieldData)?.Reset();
			((IFixReset?)PriceMovementGrp)?.Reset();
			RelSymTransactTime = null;
			NumOfSimpleInstruments = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
		}
	}
}
