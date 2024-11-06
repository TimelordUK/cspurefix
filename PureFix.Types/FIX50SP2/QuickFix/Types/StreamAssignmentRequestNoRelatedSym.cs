using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class StreamAssignmentRequestNoRelatedSym : IFixGroup
	{
		[Component(Offset = 0, Required = false)]
		public InstrumentComponent? Instrument {get; set;}
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 1, Required = false)]
		public string? SettlType {get; set;}
		
		[TagDetails(Tag = 271, Type = TagType.Float, Offset = 2, Required = false)]
		public double? MDEntrySize {get; set;}
		
		[TagDetails(Tag = 1500, Type = TagType.String, Offset = 3, Required = false)]
		public string? MDStreamID {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (SettlType is not null) writer.WriteString(63, SettlType);
			if (MDEntrySize is not null) writer.WriteNumber(271, MDEntrySize.Value);
			if (MDStreamID is not null) writer.WriteString(1500, MDStreamID);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			SettlType = view.GetString(63);
			MDEntrySize = view.GetDouble(271);
			MDStreamID = view.GetString(1500);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "Instrument":
					value = Instrument;
					break;
				case "SettlType":
					value = SettlType;
					break;
				case "MDEntrySize":
					value = MDEntrySize;
					break;
				case "MDStreamID":
					value = MDStreamID;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)Instrument)?.Reset();
			SettlType = null;
			MDEntrySize = null;
			MDStreamID = null;
		}
	}
}
