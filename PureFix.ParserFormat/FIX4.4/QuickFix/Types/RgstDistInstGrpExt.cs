using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class RgstDistInstGrpExt
	{
		public static void Parse(this RgstDistInstGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoDistribInsts");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoDistribInsts = new RgstDistInstGrpNoDistribInsts[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoDistribInsts[i] = new();
				instance.NoDistribInsts[i].Parse(groupView[i]);
			}
		}
	}
}
