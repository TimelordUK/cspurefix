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
			
			var groupViewNoMDEntryTypes = view.GetView("NoMDEntryTypes");
			if (groupViewNoMDEntryTypes is null) return;
			
			var countNoMDEntryTypes = groupViewNoMDEntryTypes.GroupCount();
			instance.NoMDEntryTypes = new MDReqGrpNoMDEntryTypes[countNoMDEntryTypes];
			for (var i = 0; i < countNoMDEntryTypes; ++i)
			{
				instance.NoMDEntryTypes[i] = new();
				instance.NoMDEntryTypes[i].Parse(groupViewNoMDEntryTypes[i]);
			}
		}
	}
}
