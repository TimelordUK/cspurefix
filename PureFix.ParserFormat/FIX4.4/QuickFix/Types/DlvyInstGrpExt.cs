using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class DlvyInstGrpExt
	{
		public static void Parse(this DlvyInstGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoDlvyInst = view.GetView("NoDlvyInst");
			if (groupViewNoDlvyInst is null) return;
			
			var countNoDlvyInst = groupViewNoDlvyInst.GroupCount();
			instance.NoDlvyInst = new DlvyInstGrpNoDlvyInst[countNoDlvyInst];
			for (var i = 0; i < countNoDlvyInst; ++i)
			{
				instance.NoDlvyInst[i] = new();
				instance.NoDlvyInst[i].Parse(groupViewNoDlvyInst[i]);
			}
		}
	}
}
