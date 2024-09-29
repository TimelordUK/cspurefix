using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class InstrumentScopeSecurityAltIDGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1540, Offset = 0, Required = false)]
		public NoInstrumentScopeSecurityAltID[]? NoInstrumentScopeSecurityAltID {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoInstrumentScopeSecurityAltID is not null && NoInstrumentScopeSecurityAltID.Length != 0)
			{
				writer.WriteWholeNumber(1540, NoInstrumentScopeSecurityAltID.Length);
				for (int i = 0; i < NoInstrumentScopeSecurityAltID.Length; i++)
				{
					((IFixEncoder)NoInstrumentScopeSecurityAltID[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoInstrumentScopeSecurityAltID") is IMessageView viewNoInstrumentScopeSecurityAltID)
			{
				var count = viewNoInstrumentScopeSecurityAltID.GroupCount();
				NoInstrumentScopeSecurityAltID = new NoInstrumentScopeSecurityAltID[count];
				for (int i = 0; i < count; i++)
				{
					NoInstrumentScopeSecurityAltID[i] = new();
					((IFixParser)NoInstrumentScopeSecurityAltID[i]).Parse(viewNoInstrumentScopeSecurityAltID.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoInstrumentScopeSecurityAltID":
					value = NoInstrumentScopeSecurityAltID;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoInstrumentScopeSecurityAltID = null;
		}
	}
}
