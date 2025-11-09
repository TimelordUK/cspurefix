using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44.Components;

namespace PureFix.Types.FIX44.Components
{
	public sealed partial class RelSymDerivSecGrp : IFixComponent
	{
		public sealed partial class NoRelatedSym : IFixGroup
		{
			[Component(Offset = 0, Required = false)]
			public Instrument? Instrument {get; set;}
			
			[TagDetails(Tag = 15, Type = TagType.String, Offset = 1, Required = false)]
			public string? Currency {get; set;}
			
			[TagDetails(Tag = 827, Type = TagType.Int, Offset = 2, Required = false)]
			public int? ExpirationCycle {get; set;}
			
			[Component(Offset = 3, Required = false)]
			public InstrumentExtension? InstrumentExtension {get; set;}
			
			[Component(Offset = 4, Required = false)]
			public InstrmtLegGrp? InstrmtLegGrp {get; set;}
			
			[TagDetails(Tag = 336, Type = TagType.String, Offset = 5, Required = false)]
			public string? TradingSessionID {get; set;}
			
			[TagDetails(Tag = 625, Type = TagType.String, Offset = 6, Required = false)]
			public string? TradingSessionSubID {get; set;}
			
			[TagDetails(Tag = 58, Type = TagType.String, Offset = 7, Required = false)]
			public string? Text {get; set;}
			
			[TagDetails(Tag = 354, Type = TagType.Length, Offset = 8, Required = false)]
			public int? EncodedTextLen {get; set;}
			
			[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 9, Required = false)]
			public byte[]? EncodedText {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
				if (Currency is not null) writer.WriteString(15, Currency);
				if (ExpirationCycle is not null) writer.WriteWholeNumber(827, ExpirationCycle.Value);
				if (InstrumentExtension is not null) ((IFixEncoder)InstrumentExtension).Encode(writer);
				if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
				if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
				if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
				if (Text is not null) writer.WriteString(58, Text);
				if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
				if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				if (view.GetView("Instrument") is IMessageView viewInstrument)
				{
					Instrument = new();
					((IFixParser)Instrument).Parse(viewInstrument);
				}
				Currency = view.GetString(15);
				ExpirationCycle = view.GetInt32(827);
				if (view.GetView("InstrumentExtension") is IMessageView viewInstrumentExtension)
				{
					InstrumentExtension = new();
					((IFixParser)InstrumentExtension).Parse(viewInstrumentExtension);
				}
				if (view.GetView("InstrmtLegGrp") is IMessageView viewInstrmtLegGrp)
				{
					InstrmtLegGrp = new();
					((IFixParser)InstrmtLegGrp).Parse(viewInstrmtLegGrp);
				}
				TradingSessionID = view.GetString(336);
				TradingSessionSubID = view.GetString(625);
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
					{
						value = Instrument;
						break;
					}
					case "Currency":
					{
						value = Currency;
						break;
					}
					case "ExpirationCycle":
					{
						value = ExpirationCycle;
						break;
					}
					case "InstrumentExtension":
					{
						value = InstrumentExtension;
						break;
					}
					case "InstrmtLegGrp":
					{
						value = InstrmtLegGrp;
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
				Currency = null;
				ExpirationCycle = null;
				((IFixReset?)InstrumentExtension)?.Reset();
				((IFixReset?)InstrmtLegGrp)?.Reset();
				TradingSessionID = null;
				TradingSessionSubID = null;
				Text = null;
				EncodedTextLen = null;
				EncodedText = null;
			}
		}
		[Group(NoOfTag = 146, Offset = 0, Required = false)]
		public NoRelatedSym[]? RelatedSym {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (RelatedSym is not null && RelatedSym.Length != 0)
			{
				writer.WriteWholeNumber(146, RelatedSym.Length);
				for (int i = 0; i < RelatedSym.Length; i++)
				{
					((IFixEncoder)RelatedSym[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
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
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRelatedSym":
				{
					value = RelatedSym;
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
			RelatedSym = null;
		}
	}
}
