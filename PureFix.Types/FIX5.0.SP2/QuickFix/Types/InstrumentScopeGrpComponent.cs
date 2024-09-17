using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class InstrumentScopeGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1656, Offset = 0, Required = false)]
		public NoInstrumentScopes[]? NoInstrumentScopes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoInstrumentScopes is not null && NoInstrumentScopes.Length != 0)
			{
				writer.WriteWholeNumber(1656, NoInstrumentScopes.Length);
				for (int i = 0; i < NoInstrumentScopes.Length; i++)
				{
					((IFixEncoder)NoInstrumentScopes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoInstrumentScopes") is IMessageView viewNoInstrumentScopes)
			{
				var count = viewNoInstrumentScopes.GroupCount();
				NoInstrumentScopes = new NoInstrumentScopes[count];
				for (int i = 0; i < count; i++)
				{
					NoInstrumentScopes[i] = new();
					((IFixParser)NoInstrumentScopes[i]).Parse(viewNoInstrumentScopes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoInstrumentScopes":
					value = NoInstrumentScopes;
					break;
				default: return false;
			}
			return true;
		}
	}
}
