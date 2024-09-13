using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class ExecAllocGrpExt
	{
		public static void Parse(this ExecAllocGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoExecs");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoExecs = new ExecAllocGrpNoExecs[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoExecs[i] = new();
				instance.NoExecs[i].Parse(groupView[i]);
			}
		}
	}
}
