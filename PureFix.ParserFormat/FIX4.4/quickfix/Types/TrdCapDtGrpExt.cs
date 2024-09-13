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
			
			var groupView = view.GetView("NoDates");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoDates = new TrdCapDtGrpNoDates[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoDates[i] = new();
				instance.NoDates[i].Parse(groupView[i]);
			}
		}
	}
}
