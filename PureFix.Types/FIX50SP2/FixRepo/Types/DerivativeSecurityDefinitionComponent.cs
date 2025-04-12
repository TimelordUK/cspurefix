using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo.Types
{
	public sealed partial class DerivativeSecurityDefinitionComponent : IFixComponent
	{
		[Component(Offset = 0, Required = false)]
		public DerivativeInstrumentComponent? DerivativeInstrument {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (DerivativeInstrument is not null) ((IFixEncoder)DerivativeInstrument).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("DerivativeInstrument") is IMessageView viewDerivativeInstrument)
			{
				DerivativeInstrument = new();
				((IFixParser)DerivativeInstrument).Parse(viewDerivativeInstrument);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "DerivativeInstrument":
					value = DerivativeInstrument;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)DerivativeInstrument)?.Reset();
		}
	}
}
