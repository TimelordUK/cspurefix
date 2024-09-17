using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class MatchRulesComponent : IFixComponent
	{
		[Group(NoOfTag = 1235, Offset = 0, Required = false)]
		public NoMatchRules[]? NoMatchRules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMatchRules is not null && NoMatchRules.Length != 0)
			{
				writer.WriteWholeNumber(1235, NoMatchRules.Length);
				for (int i = 0; i < NoMatchRules.Length; i++)
				{
					((IFixEncoder)NoMatchRules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoMatchRules") is IMessageView viewNoMatchRules)
			{
				var count = viewNoMatchRules.GroupCount();
				NoMatchRules = new NoMatchRules[count];
				for (int i = 0; i < count; i++)
				{
					NoMatchRules[i] = new();
					((IFixParser)NoMatchRules[i]).Parse(viewNoMatchRules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoMatchRules":
					value = NoMatchRules;
					break;
				default: return false;
			}
			return true;
		}
	}
}
