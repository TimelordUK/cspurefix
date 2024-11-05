using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class RFQRequestRFQReqGrpNoRelatedSym : IFixGroup
	{
		[Component(Offset = 0, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public InstrmtLegGrpComponent? InstrmtLegGrp {get; set;}
		
		[TagDetails(Tag = 140, Type = TagType.Float, Offset = 3, Required = false)]
		public double? PrevClosePx {get; set;}
		
		[TagDetails(Tag = 303, Type = TagType.Int, Offset = 4, Required = false)]
		public int? QuoteRequestType {get; set;}
		
		[TagDetails(Tag = 537, Type = TagType.Int, Offset = 5, Required = false)]
		public int? QuoteType {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 6, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 7, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				Instrument is not null && ((IFixValidator)Instrument).IsValid(in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (PrevClosePx is not null) writer.WriteNumber(140, PrevClosePx.Value);
			if (QuoteRequestType is not null) writer.WriteWholeNumber(303, QuoteRequestType.Value);
			if (QuoteType is not null) writer.WriteWholeNumber(537, QuoteType.Value);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			if (view.GetView("UndInstrmtGrp") is IMessageView viewUndInstrmtGrp)
			{
				UndInstrmtGrp = new();
				((IFixParser)UndInstrmtGrp).Parse(viewUndInstrmtGrp);
			}
			if (view.GetView("InstrmtLegGrp") is IMessageView viewInstrmtLegGrp)
			{
				InstrmtLegGrp = new();
				((IFixParser)InstrmtLegGrp).Parse(viewInstrmtLegGrp);
			}
			PrevClosePx = view.GetDouble(140);
			QuoteRequestType = view.GetInt32(303);
			QuoteType = view.GetInt32(537);
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "Instrument":
					value = Instrument;
					break;
				case "UndInstrmtGrp":
					value = UndInstrmtGrp;
					break;
				case "InstrmtLegGrp":
					value = InstrmtLegGrp;
					break;
				case "PrevClosePx":
					value = PrevClosePx;
					break;
				case "QuoteRequestType":
					value = QuoteRequestType;
					break;
				case "QuoteType":
					value = QuoteType;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "TradingSessionSubID":
					value = TradingSessionSubID;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)Instrument)?.Reset();
			((IFixReset?)UndInstrmtGrp)?.Reset();
			((IFixReset?)InstrmtLegGrp)?.Reset();
			PrevClosePx = null;
			QuoteRequestType = null;
			QuoteType = null;
			TradingSessionID = null;
			TradingSessionSubID = null;
		}
	}
}
