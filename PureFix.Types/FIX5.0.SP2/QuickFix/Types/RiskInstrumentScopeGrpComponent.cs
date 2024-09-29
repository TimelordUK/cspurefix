using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class RiskInstrumentScopeGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1534, Offset = 0, Required = false)]
		public NoRiskInstrumentScopes[]? NoRiskInstrumentScopes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRiskInstrumentScopes is not null && NoRiskInstrumentScopes.Length != 0)
			{
				writer.WriteWholeNumber(1534, NoRiskInstrumentScopes.Length);
				for (int i = 0; i < NoRiskInstrumentScopes.Length; i++)
				{
					((IFixEncoder)NoRiskInstrumentScopes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRiskInstrumentScopes") is IMessageView viewNoRiskInstrumentScopes)
			{
				var count = viewNoRiskInstrumentScopes.GroupCount();
				NoRiskInstrumentScopes = new NoRiskInstrumentScopes[count];
				for (int i = 0; i < count; i++)
				{
					NoRiskInstrumentScopes[i] = new();
					((IFixParser)NoRiskInstrumentScopes[i]).Parse(viewNoRiskInstrumentScopes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRiskInstrumentScopes":
					value = NoRiskInstrumentScopes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoRiskInstrumentScopes = null;
		}
	}
}
