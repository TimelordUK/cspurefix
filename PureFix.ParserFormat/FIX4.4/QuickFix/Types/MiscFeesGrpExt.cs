using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class MiscFeesGrpExt
	{
		public static void Parse(this MiscFeesGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoMiscFees = view.GetView("NoMiscFees");
			if (groupViewNoMiscFees is null) return;
			
			var countNoMiscFees = groupViewNoMiscFees.GroupCount();
			instance.NoMiscFees = new MiscFeesGrpNoMiscFees[countNoMiscFees];
			for (var i = 0; i < countNoMiscFees; ++i)
			{
				instance.NoMiscFees[i] = new();
				instance.NoMiscFees[i].Parse(groupViewNoMiscFees[i]);
			}
		}
	}
}
