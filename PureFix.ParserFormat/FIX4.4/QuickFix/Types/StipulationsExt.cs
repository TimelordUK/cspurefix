using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class StipulationsExt
	{
		public static void Parse(this Stipulations instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoStipulations = view.GetView("NoStipulations");
			if (groupViewNoStipulations is null) return;
			
			var countNoStipulations = groupViewNoStipulations.GroupCount();
			instance.NoStipulations = new StipulationsNoStipulations[countNoStipulations];
			for (var i = 0; i < countNoStipulations; ++i)
			{
				instance.NoStipulations[i] = new();
				instance.NoStipulations[i].Parse(groupViewNoStipulations[i]);
			}
		}
	}
}
