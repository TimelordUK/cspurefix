using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class OrdTypeRulesComponent : IFixComponent
	{
		[Group(NoOfTag = 1237, Offset = 0, Required = false)]
		public SecurityDefinitionNoOrdTypeRules[]? NoOrdTypeRules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoOrdTypeRules is not null && NoOrdTypeRules.Length != 0)
			{
				writer.WriteWholeNumber(1237, NoOrdTypeRules.Length);
				for (int i = 0; i < NoOrdTypeRules.Length; i++)
				{
					((IFixEncoder)NoOrdTypeRules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoOrdTypeRules") is IMessageView viewNoOrdTypeRules)
			{
				var count = viewNoOrdTypeRules.GroupCount();
				NoOrdTypeRules = new SecurityDefinitionNoOrdTypeRules[count];
				for (int i = 0; i < count; i++)
				{
					NoOrdTypeRules[i] = new();
					((IFixParser)NoOrdTypeRules[i]).Parse(viewNoOrdTypeRules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoOrdTypeRules":
					value = NoOrdTypeRules;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoOrdTypeRules = null;
		}
	}
}
