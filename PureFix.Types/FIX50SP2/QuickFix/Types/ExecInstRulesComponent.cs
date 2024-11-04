using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ExecInstRulesComponent : IFixComponent
	{
		[Group(NoOfTag = 1232, Offset = 0, Required = false)]
		public NoExecInstRules[]? NoExecInstRules {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoExecInstRules is not null && NoExecInstRules.Length != 0)
			{
				writer.WriteWholeNumber(1232, NoExecInstRules.Length);
				for (int i = 0; i < NoExecInstRules.Length; i++)
				{
					((IFixEncoder)NoExecInstRules[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoExecInstRules") is IMessageView viewNoExecInstRules)
			{
				var count = viewNoExecInstRules.GroupCount();
				NoExecInstRules = new NoExecInstRules[count];
				for (int i = 0; i < count; i++)
				{
					NoExecInstRules[i] = new();
					((IFixParser)NoExecInstRules[i]).Parse(viewNoExecInstRules.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoExecInstRules":
					value = NoExecInstRules;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoExecInstRules = null;
		}
	}
}
