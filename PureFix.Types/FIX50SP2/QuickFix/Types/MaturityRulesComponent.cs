using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class MaturityRulesComponent : IFixComponent
	{
		[Group(NoOfTag = 1236, Offset = 0, Required = false)]
		public SecurityDefinitionNoMaturityRules[]? NoMaturityRules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMaturityRules is not null && NoMaturityRules.Length != 0)
			{
				writer.WriteWholeNumber(1236, NoMaturityRules.Length);
				for (int i = 0; i < NoMaturityRules.Length; i++)
				{
					((IFixEncoder)NoMaturityRules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoMaturityRules") is IMessageView viewNoMaturityRules)
			{
				var count = viewNoMaturityRules.GroupCount();
				NoMaturityRules = new SecurityDefinitionNoMaturityRules[count];
				for (int i = 0; i < count; i++)
				{
					NoMaturityRules[i] = new();
					((IFixParser)NoMaturityRules[i]).Parse(viewNoMaturityRules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoMaturityRules":
					value = NoMaturityRules;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoMaturityRules = null;
		}
	}
}
