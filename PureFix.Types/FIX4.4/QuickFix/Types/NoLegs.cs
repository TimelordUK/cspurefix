using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NoLegs : IFixGroup
	{
		[Component(Offset = 0, Required = false)]
		public InstrumentLegComponent? InstrumentLeg {get; set;}
		
		[TagDetails(Tag = 682, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegIOIQty {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public LegStipulationsComponent? LegStipulations {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (InstrumentLeg is not null) ((IFixEncoder)InstrumentLeg).Encode(writer);
			if (LegIOIQty is not null) writer.WriteString(682, LegIOIQty);
			if (LegStipulations is not null) ((IFixEncoder)LegStipulations).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("InstrumentLeg") is IMessageView viewInstrumentLeg)
			{
				InstrumentLeg = new();
				((IFixParser)InstrumentLeg).Parse(viewInstrumentLeg);
			}
			LegIOIQty = view.GetString(682);
			if (view.GetView("LegStipulations") is IMessageView viewLegStipulations)
			{
				LegStipulations = new();
				((IFixParser)LegStipulations).Parse(viewLegStipulations);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "InstrumentLeg":
					value = InstrumentLeg;
					break;
				case "LegIOIQty":
					value = LegIOIQty;
					break;
				case "LegStipulations":
					value = LegStipulations;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)InstrumentLeg)?.Reset();
			LegIOIQty = null;
			((IFixReset?)LegStipulations)?.Reset();
		}
	}
}
