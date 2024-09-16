using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PriceRangeRuleGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2550, Offset = 0, Required = false)]
		public NoPriceRangeRules[]? NoPriceRangeRules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPriceRangeRules is not null && NoPriceRangeRules.Length != 0)
			{
				writer.WriteWholeNumber(2550, NoPriceRangeRules.Length);
				for (int i = 0; i < NoPriceRangeRules.Length; i++)
				{
					((IFixEncoder)NoPriceRangeRules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPriceRangeRules") is IMessageView viewNoPriceRangeRules)
			{
				var count = viewNoPriceRangeRules.GroupCount();
				NoPriceRangeRules = new NoPriceRangeRules[count];
				for (int i = 0; i < count; i++)
				{
					NoPriceRangeRules[i] = new();
					((IFixParser)NoPriceRangeRules[i]).Parse(viewNoPriceRangeRules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPriceRangeRules":
					value = NoPriceRangeRules;
					break;
				default: return false;
			}
			return true;
		}
	}
}
