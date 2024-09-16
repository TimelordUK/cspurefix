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
			
			var groupViewNoHops = view.GetView("NoHops");
			if (groupViewNoHops is null) return;
			
			var countNoHops = groupViewNoHops.GroupCount();
			instance.NoHops = new HopNoHops[countNoHops];
			for (var i = 0; i < countNoHops; ++i)
			{
				instance.NoHops[i] = new();
				instance.NoHops[i].Parse(groupViewNoHops[i]);
			}
		}
	}
}
