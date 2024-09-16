using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class TrdCapDtGrpExt
	{
		public static void Parse(this TrdCapDtGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoDates = view.GetView("NoDates");
			if (groupViewNoDates is null) return;
			
			var countNoDates = groupViewNoDates.GroupCount();
			instance.NoDates = new TrdCapDtGrpNoDates[countNoDates];
			for (var i = 0; i < countNoDates; ++i)
			{
				instance.NoDates[i] = new();
				instance.NoDates[i].Parse(groupViewNoDates[i]);
			}
		}
	}
}
