using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class InstrumentPartiesComponent : IFixComponent
	{
		[Group(NoOfTag = 1018, Offset = 0, Required = false)]
		public IOINoInstrumentParties[]? NoInstrumentParties {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoInstrumentParties is not null && NoInstrumentParties.Length != 0)
			{
				writer.WriteWholeNumber(1018, NoInstrumentParties.Length);
				for (int i = 0; i < NoInstrumentParties.Length; i++)
				{
					((IFixEncoder)NoInstrumentParties[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoInstrumentParties") is IMessageView viewNoInstrumentParties)
			{
				var count = viewNoInstrumentParties.GroupCount();
				NoInstrumentParties = new IOINoInstrumentParties[count];
				for (int i = 0; i < count; i++)
				{
					NoInstrumentParties[i] = new();
					((IFixParser)NoInstrumentParties[i]).Parse(viewNoInstrumentParties.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoInstrumentParties":
					value = NoInstrumentParties;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoInstrumentParties = null;
		}
	}
}
