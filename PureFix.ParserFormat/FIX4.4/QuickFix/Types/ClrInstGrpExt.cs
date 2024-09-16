using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class ClrInstGrpExt
	{
		public static void Parse(this ClrInstGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoClearingInstructions = view.GetView("NoClearingInstructions");
			if (groupViewNoClearingInstructions is null) return;
			
			var countNoClearingInstructions = groupViewNoClearingInstructions.GroupCount();
			instance.NoClearingInstructions = new ClrInstGrpNoClearingInstructions[countNoClearingInstructions];
			for (var i = 0; i < countNoClearingInstructions; ++i)
			{
				instance.NoClearingInstructions[i] = new();
				instance.NoClearingInstructions[i].Parse(groupViewNoClearingInstructions[i]);
			}
		}
	}
}
