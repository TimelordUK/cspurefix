using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TimeInForceRulesComponent : IFixComponent
	{
		[Group(NoOfTag = 1239, Offset = 0, Required = false)]
		public SecurityDefinitionNoTimeInForceRules[]? NoTimeInForceRules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoTimeInForceRules is not null && NoTimeInForceRules.Length != 0)
			{
				writer.WriteWholeNumber(1239, NoTimeInForceRules.Length);
				for (int i = 0; i < NoTimeInForceRules.Length; i++)
				{
					((IFixEncoder)NoTimeInForceRules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoTimeInForceRules") is IMessageView viewNoTimeInForceRules)
			{
				var count = viewNoTimeInForceRules.GroupCount();
				NoTimeInForceRules = new SecurityDefinitionNoTimeInForceRules[count];
				for (int i = 0; i < count; i++)
				{
					NoTimeInForceRules[i] = new();
					((IFixParser)NoTimeInForceRules[i]).Parse(viewNoTimeInForceRules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoTimeInForceRules":
					value = NoTimeInForceRules;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoTimeInForceRules = null;
		}
	}
}
