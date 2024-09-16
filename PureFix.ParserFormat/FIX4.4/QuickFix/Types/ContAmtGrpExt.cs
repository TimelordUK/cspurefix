using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class ContAmtGrpExt
	{
		public static void Parse(this ContAmtGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoContAmts = view.GetView("NoContAmts");
			if (groupViewNoContAmts is null) return;
			
			var countNoContAmts = groupViewNoContAmts.GroupCount();
			instance.NoContAmts = new ContAmtGrpNoContAmts[countNoContAmts];
			for (var i = 0; i < countNoContAmts; ++i)
			{
				instance.NoContAmts[i] = new();
				instance.NoContAmts[i].Parse(groupViewNoContAmts[i]);
			}
		}
	}
}
