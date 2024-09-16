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
			
			var groupViewNoDistribInsts = view.GetView("NoDistribInsts");
			if (groupViewNoDistribInsts is null) return;
			
			var countNoDistribInsts = groupViewNoDistribInsts.GroupCount();
			instance.NoDistribInsts = new RgstDistInstGrpNoDistribInsts[countNoDistribInsts];
			for (var i = 0; i < countNoDistribInsts; ++i)
			{
				instance.NoDistribInsts[i] = new();
				instance.NoDistribInsts[i].Parse(groupViewNoDistribInsts[i]);
			}
		}
	}
}
