using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class FillsGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1362, Offset = 0, Required = false)]
		public ExecutionReportNoFills[]? NoFills {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoFills is not null && NoFills.Length != 0)
			{
				writer.WriteWholeNumber(1362, NoFills.Length);
				for (int i = 0; i < NoFills.Length; i++)
				{
					((IFixEncoder)NoFills[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoFills") is IMessageView viewNoFills)
			{
				var count = viewNoFills.GroupCount();
				NoFills = new ExecutionReportNoFills[count];
				for (int i = 0; i < count; i++)
				{
					NoFills[i] = new();
					((IFixParser)NoFills[i]).Parse(viewNoFills.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoFills":
					value = NoFills;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoFills = null;
		}
	}
}
