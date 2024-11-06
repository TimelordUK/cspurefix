using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class StrikeRulesComponent : IFixComponent
	{
		[Group(NoOfTag = 1201, Offset = 0, Required = false)]
		public SecurityDefinitionNoStrikeRules[]? NoStrikeRules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStrikeRules is not null && NoStrikeRules.Length != 0)
			{
				writer.WriteWholeNumber(1201, NoStrikeRules.Length);
				for (int i = 0; i < NoStrikeRules.Length; i++)
				{
					((IFixEncoder)NoStrikeRules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoStrikeRules") is IMessageView viewNoStrikeRules)
			{
				var count = viewNoStrikeRules.GroupCount();
				NoStrikeRules = new SecurityDefinitionNoStrikeRules[count];
				for (int i = 0; i < count; i++)
				{
					NoStrikeRules[i] = new();
					((IFixParser)NoStrikeRules[i]).Parse(viewNoStrikeRules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoStrikeRules":
					value = NoStrikeRules;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoStrikeRules = null;
		}
	}
}
