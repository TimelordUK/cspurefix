using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class MDReqGrpExt
	{
		public static void Parse(this MDReqGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoMDEntryTypes");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoMDEntryTypes = new MDReqGrpNoMDEntryTypes[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoMDEntryTypes[i] = new();
				instance.NoMDEntryTypes[i].Parse(groupView[i]);
			}
		}
	}
}
