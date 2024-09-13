using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class CompIDStatGrpExt
	{
		public static void Parse(this CompIDStatGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoCompIDs");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoCompIDs = new CompIDStatGrpNoCompIDs[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoCompIDs[i] = new();
				instance.NoCompIDs[i].Parse(groupView[i]);
			}
		}
	}
}
