using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class LegStipulationsExt
	{
		public static void Parse(this LegStipulations instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoLegStipulations");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoLegStipulations = new LegStipulationsNoLegStipulations[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoLegStipulations[i] = new();
				instance.NoLegStipulations[i].Parse(groupView[i]);
			}
		}
	}
}
