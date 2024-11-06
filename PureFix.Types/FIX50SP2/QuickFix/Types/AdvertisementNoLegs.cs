using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class AdvertisementNoLegs : IFixGroup
	{
		[Component(Offset = 0, Required = false)]
		public InstrumentLegComponent? InstrumentLeg {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public LegFinancingDetailsComponent? LegFinancingDetails {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (InstrumentLeg is not null) ((IFixEncoder)InstrumentLeg).Encode(writer);
			if (LegFinancingDetails is not null) ((IFixEncoder)LegFinancingDetails).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("InstrumentLeg") is IMessageView viewInstrumentLeg)
			{
				InstrumentLeg = new();
				((IFixParser)InstrumentLeg).Parse(viewInstrumentLeg);
			}
			if (view.GetView("LegFinancingDetails") is IMessageView viewLegFinancingDetails)
			{
				LegFinancingDetails = new();
				((IFixParser)LegFinancingDetails).Parse(viewLegFinancingDetails);
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
				case "LegFinancingDetails":
					value = LegFinancingDetails;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)InstrumentLeg)?.Reset();
			((IFixReset?)LegFinancingDetails)?.Reset();
		}
	}
}
