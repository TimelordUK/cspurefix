using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class InstrmtLegIOIGrpExt
	{
		public static void Parse(this InstrmtLegIOIGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoLegs = view.GetView("NoLegs");
			if (groupViewNoLegs is null) return;
			
			var countNoLegs = groupViewNoLegs.GroupCount();
			instance.NoLegs = new InstrmtLegIOIGrpNoLegs[countNoLegs];
			for (var i = 0; i < countNoLegs; ++i)
			{
				instance.NoLegs[i] = new();
				instance.NoLegs[i].Parse(groupViewNoLegs[i]);
			}
		}
	}
}
