using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo.Types
{
	public sealed partial class InstrmtLegGrpComponent : IFixComponent
	{
		[TagDetails(Tag = 555, Type = TagType.Int, Offset = 0, Required = false)]
		public int? NoLegs {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public InstrumentLegComponent? InstrumentLeg {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegs is not null) writer.WriteWholeNumber(555, NoLegs.Value);
			if (InstrumentLeg is not null) ((IFixEncoder)InstrumentLeg).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			NoLegs = view.GetInt32(555);
			if (view.GetView("InstrumentLeg") is IMessageView viewInstrumentLeg)
			{
				InstrumentLeg = new();
				((IFixParser)InstrumentLeg).Parse(viewInstrumentLeg);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegs":
					value = NoLegs;
					break;
				case "InstrumentLeg":
					value = InstrumentLeg;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegs = null;
			((IFixReset?)InstrumentLeg)?.Reset();
		}
	}
}
