using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class EvntGrpExt
	{
		public static void Parse(this EvntGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoEvents = view.GetView("NoEvents");
			if (groupViewNoEvents is null) return;
			
			var countNoEvents = groupViewNoEvents.GroupCount();
			instance.NoEvents = new EvntGrpNoEvents[countNoEvents];
			for (var i = 0; i < countNoEvents; ++i)
			{
				instance.NoEvents[i] = new();
				instance.NoEvents[i].Parse(groupViewNoEvents[i]);
			}
		}
	}
}
