using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class HopExt
	{
		public static void Parse(this Hop instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoHops");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoHops = new HopNoHops[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoHops[i] = new();
				instance.NoHops[i].Parse(groupView[i]);
			}
		}
	}
}
