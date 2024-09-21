using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class QuoteSizeRuleGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2558, Offset = 0, Required = false)]
		public NoQuoteSizeRules[]? NoQuoteSizeRules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoQuoteSizeRules is not null && NoQuoteSizeRules.Length != 0)
			{
				writer.WriteWholeNumber(2558, NoQuoteSizeRules.Length);
				for (int i = 0; i < NoQuoteSizeRules.Length; i++)
				{
					((IFixEncoder)NoQuoteSizeRules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoQuoteSizeRules") is IMessageView viewNoQuoteSizeRules)
			{
				var count = viewNoQuoteSizeRules.GroupCount();
				NoQuoteSizeRules = new NoQuoteSizeRules[count];
				for (int i = 0; i < count; i++)
				{
					NoQuoteSizeRules[i] = new();
					((IFixParser)NoQuoteSizeRules[i]).Parse(viewNoQuoteSizeRules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoQuoteSizeRules":
					value = NoQuoteSizeRules;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoQuoteSizeRules = null;
		}
	}
}
