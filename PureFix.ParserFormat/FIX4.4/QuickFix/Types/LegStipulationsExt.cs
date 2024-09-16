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
			
			var groupViewNoLegStipulations = view.GetView("NoLegStipulations");
			if (groupViewNoLegStipulations is null) return;
			
			var countNoLegStipulations = groupViewNoLegStipulations.GroupCount();
			instance.NoLegStipulations = new LegStipulationsNoLegStipulations[countNoLegStipulations];
			for (var i = 0; i < countNoLegStipulations; ++i)
			{
				instance.NoLegStipulations[i] = new();
				instance.NoLegStipulations[i].Parse(groupViewNoLegStipulations[i]);
			}
		}
	}
}
