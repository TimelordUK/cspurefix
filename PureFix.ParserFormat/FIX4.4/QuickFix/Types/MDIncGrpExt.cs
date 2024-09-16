using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class MDIncGrpExt
	{
		public static void Parse(this MDIncGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoMDEntries");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoMDEntries = new MDIncGrpNoMDEntries[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoMDEntries[i] = new();
				instance.NoMDEntries[i].Parse(groupView[i]);
			}
		}
	}
}
