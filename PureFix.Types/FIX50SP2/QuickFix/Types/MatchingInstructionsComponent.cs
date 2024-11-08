using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class MatchingInstructionsComponent : IFixComponent
	{
		[Group(NoOfTag = 1624, Offset = 0, Required = false)]
		public ExecutionReportNoMatchInst[]? NoMatchInst {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMatchInst is not null && NoMatchInst.Length != 0)
			{
				writer.WriteWholeNumber(1624, NoMatchInst.Length);
				for (int i = 0; i < NoMatchInst.Length; i++)
				{
					((IFixEncoder)NoMatchInst[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoMatchInst") is IMessageView viewNoMatchInst)
			{
				var count = viewNoMatchInst.GroupCount();
				NoMatchInst = new ExecutionReportNoMatchInst[count];
				for (int i = 0; i < count; i++)
				{
					NoMatchInst[i] = new();
					((IFixParser)NoMatchInst[i]).Parse(viewNoMatchInst.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoMatchInst":
					value = NoMatchInst;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoMatchInst = null;
		}
	}
}
