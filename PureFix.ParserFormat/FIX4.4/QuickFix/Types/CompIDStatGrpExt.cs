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
			
			var groupViewNoCompIDs = view.GetView("NoCompIDs");
			if (groupViewNoCompIDs is null) return;
			
			var countNoCompIDs = groupViewNoCompIDs.GroupCount();
			instance.NoCompIDs = new CompIDStatGrpNoCompIDs[countNoCompIDs];
			for (var i = 0; i < countNoCompIDs; ++i)
			{
				instance.NoCompIDs[i] = new();
				instance.NoCompIDs[i].Parse(groupViewNoCompIDs[i]);
			}
		}
	}
}
