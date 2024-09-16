using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class InstrmtStrkPxGrpExt
	{
		public static void Parse(this InstrmtStrkPxGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoStrikes = view.GetView("NoStrikes");
			if (groupViewNoStrikes is null) return;
			
			var countNoStrikes = groupViewNoStrikes.GroupCount();
			instance.NoStrikes = new InstrmtStrkPxGrpNoStrikes[countNoStrikes];
			for (var i = 0; i < countNoStrikes; ++i)
			{
				instance.NoStrikes[i] = new();
				instance.NoStrikes[i].Parse(groupViewNoStrikes[i]);
			}
		}
	}
}
