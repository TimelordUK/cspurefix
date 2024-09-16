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
			
			var groupViewNoMDEntries = view.GetView("NoMDEntries");
			if (groupViewNoMDEntries is null) return;
			
			var countNoMDEntries = groupViewNoMDEntries.GroupCount();
			instance.NoMDEntries = new MDIncGrpNoMDEntries[countNoMDEntries];
			for (var i = 0; i < countNoMDEntries; ++i)
			{
				instance.NoMDEntries[i] = new();
				instance.NoMDEntries[i].Parse(groupViewNoMDEntries[i]);
			}
		}
	}
}
