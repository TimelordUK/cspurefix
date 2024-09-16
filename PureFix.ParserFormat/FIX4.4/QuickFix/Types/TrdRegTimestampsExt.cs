using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class TrdRegTimestampsExt
	{
		public static void Parse(this TrdRegTimestamps instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoTrdRegTimestamps = view.GetView("NoTrdRegTimestamps");
			if (groupViewNoTrdRegTimestamps is null) return;
			
			var countNoTrdRegTimestamps = groupViewNoTrdRegTimestamps.GroupCount();
			instance.NoTrdRegTimestamps = new TrdRegTimestampsNoTrdRegTimestamps[countNoTrdRegTimestamps];
			for (var i = 0; i < countNoTrdRegTimestamps; ++i)
			{
				instance.NoTrdRegTimestamps[i] = new();
				instance.NoTrdRegTimestamps[i].Parse(groupViewNoTrdRegTimestamps[i]);
			}
		}
	}
}
