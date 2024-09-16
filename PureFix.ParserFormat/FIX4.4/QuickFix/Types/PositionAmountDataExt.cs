using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class PositionAmountDataExt
	{
		public static void Parse(this PositionAmountData instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoPosAmt = view.GetView("NoPosAmt");
			if (groupViewNoPosAmt is null) return;
			
			var countNoPosAmt = groupViewNoPosAmt.GroupCount();
			instance.NoPosAmt = new PositionAmountDataNoPosAmt[countNoPosAmt];
			for (var i = 0; i < countNoPosAmt; ++i)
			{
				instance.NoPosAmt[i] = new();
				instance.NoPosAmt[i].Parse(groupViewNoPosAmt[i]);
			}
		}
	}
}
