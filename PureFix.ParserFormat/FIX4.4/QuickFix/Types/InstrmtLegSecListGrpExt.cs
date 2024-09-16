using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class InstrmtLegSecListGrpExt
	{
		public static void Parse(this InstrmtLegSecListGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoLegs");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoLegs = new InstrmtLegSecListGrpNoLegs[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoLegs[i] = new();
				instance.NoLegs[i].Parse(groupView[i]);
			}
		}
	}
}
