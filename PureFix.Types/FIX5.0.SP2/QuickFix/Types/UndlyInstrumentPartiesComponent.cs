using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UndlyInstrumentPartiesComponent : IFixComponent
	{
		[Group(NoOfTag = 1058, Offset = 0, Required = false)]
		public NoUndlyInstrumentParties[]? NoUndlyInstrumentParties {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUndlyInstrumentParties is not null && NoUndlyInstrumentParties.Length != 0)
			{
				writer.WriteWholeNumber(1058, NoUndlyInstrumentParties.Length);
				for (int i = 0; i < NoUndlyInstrumentParties.Length; i++)
				{
					((IFixEncoder)NoUndlyInstrumentParties[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUndlyInstrumentParties") is IMessageView viewNoUndlyInstrumentParties)
			{
				var count = viewNoUndlyInstrumentParties.GroupCount();
				NoUndlyInstrumentParties = new NoUndlyInstrumentParties[count];
				for (int i = 0; i < count; i++)
				{
					NoUndlyInstrumentParties[i] = new();
					((IFixParser)NoUndlyInstrumentParties[i]).Parse(viewNoUndlyInstrumentParties.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUndlyInstrumentParties":
					value = NoUndlyInstrumentParties;
					break;
				default: return false;
			}
			return true;
		}
	}
}
