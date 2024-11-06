using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TickRulesComponent : IFixComponent
	{
		[Group(NoOfTag = 1205, Offset = 0, Required = false)]
		public SecurityDefinitionNoTickRules[]? NoTickRules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoTickRules is not null && NoTickRules.Length != 0)
			{
				writer.WriteWholeNumber(1205, NoTickRules.Length);
				for (int i = 0; i < NoTickRules.Length; i++)
				{
					((IFixEncoder)NoTickRules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoTickRules") is IMessageView viewNoTickRules)
			{
				var count = viewNoTickRules.GroupCount();
				NoTickRules = new SecurityDefinitionNoTickRules[count];
				for (int i = 0; i < count; i++)
				{
					NoTickRules[i] = new();
					((IFixParser)NoTickRules[i]).Parse(viewNoTickRules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoTickRules":
					value = NoTickRules;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoTickRules = null;
		}
	}
}
