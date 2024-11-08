using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class SecurityDefinitionNoLegs : IFixGroup
	{
		[Component(Offset = 0, Required = false)]
		public InstrumentLegComponent? InstrumentLeg {get; set;}
		
		[TagDetails(Tag = 556, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegCurrency {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (InstrumentLeg is not null) ((IFixEncoder)InstrumentLeg).Encode(writer);
			if (LegCurrency is not null) writer.WriteString(556, LegCurrency);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("InstrumentLeg") is IMessageView viewInstrumentLeg)
			{
				InstrumentLeg = new();
				((IFixParser)InstrumentLeg).Parse(viewInstrumentLeg);
			}
			LegCurrency = view.GetString(556);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "InstrumentLeg":
					value = InstrumentLeg;
					break;
				case "LegCurrency":
					value = LegCurrency;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)InstrumentLeg)?.Reset();
			LegCurrency = null;
		}
	}
}
