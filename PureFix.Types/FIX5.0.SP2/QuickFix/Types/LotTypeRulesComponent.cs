using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LotTypeRulesComponent : IFixComponent
	{
		[Group(NoOfTag = 1234, Offset = 0, Required = false)]
		public NoLotTypeRules[]? NoLotTypeRules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLotTypeRules is not null && NoLotTypeRules.Length != 0)
			{
				writer.WriteWholeNumber(1234, NoLotTypeRules.Length);
				for (int i = 0; i < NoLotTypeRules.Length; i++)
				{
					((IFixEncoder)NoLotTypeRules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLotTypeRules") is IMessageView viewNoLotTypeRules)
			{
				var count = viewNoLotTypeRules.GroupCount();
				NoLotTypeRules = new NoLotTypeRules[count];
				for (int i = 0; i < count; i++)
				{
					NoLotTypeRules[i] = new();
					((IFixParser)NoLotTypeRules[i]).Parse(viewNoLotTypeRules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLotTypeRules":
					value = NoLotTypeRules;
					break;
				default: return false;
			}
			return true;
		}
	}
}
