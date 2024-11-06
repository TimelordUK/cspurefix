using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TradingSessionRulesGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1309, Offset = 0, Required = false)]
		public SecurityDefinitionNoTradingSessionRules[]? NoTradingSessionRules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoTradingSessionRules is not null && NoTradingSessionRules.Length != 0)
			{
				writer.WriteWholeNumber(1309, NoTradingSessionRules.Length);
				for (int i = 0; i < NoTradingSessionRules.Length; i++)
				{
					((IFixEncoder)NoTradingSessionRules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoTradingSessionRules") is IMessageView viewNoTradingSessionRules)
			{
				var count = viewNoTradingSessionRules.GroupCount();
				NoTradingSessionRules = new SecurityDefinitionNoTradingSessionRules[count];
				for (int i = 0; i < count; i++)
				{
					NoTradingSessionRules[i] = new();
					((IFixParser)NoTradingSessionRules[i]).Parse(viewNoTradingSessionRules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoTradingSessionRules":
					value = NoTradingSessionRules;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoTradingSessionRules = null;
		}
	}
}
