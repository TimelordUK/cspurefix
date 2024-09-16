using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SettlInstGrpExt
	{
		public static void Parse(this SettlInstGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoSettlInst = view.GetView("NoSettlInst");
			if (groupViewNoSettlInst is null) return;
			
			var countNoSettlInst = groupViewNoSettlInst.GroupCount();
			instance.NoSettlInst = new SettlInstGrpNoSettlInst[countNoSettlInst];
			for (var i = 0; i < countNoSettlInst; ++i)
			{
				instance.NoSettlInst[i] = new();
				instance.NoSettlInst[i].Parse(groupViewNoSettlInst[i]);
			}
		}
	}
}
