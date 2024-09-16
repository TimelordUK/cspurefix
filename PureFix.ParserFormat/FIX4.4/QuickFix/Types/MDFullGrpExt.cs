using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class MDFullGrpExt
	{
		public static void Parse(this MDFullGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoMDEntries = view.GetView("NoMDEntries");
			if (groupViewNoMDEntries is null) return;
			
			var countNoMDEntries = groupViewNoMDEntries.GroupCount();
			instance.NoMDEntries = new MDFullGrpNoMDEntries[countNoMDEntries];
			for (var i = 0; i < countNoMDEntries; ++i)
			{
				instance.NoMDEntries[i] = new();
				instance.NoMDEntries[i].Parse(groupViewNoMDEntries[i]);
			}
		}
	}
}
