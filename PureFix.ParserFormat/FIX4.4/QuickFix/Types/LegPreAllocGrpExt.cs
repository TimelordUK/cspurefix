using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class LegPreAllocGrpExt
	{
		public static void Parse(this LegPreAllocGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoLegAllocs");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoLegAllocs = new LegPreAllocGrpNoLegAllocs[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoLegAllocs[i] = new();
				instance.NoLegAllocs[i].Parse(groupView[i]);
			}
		}
	}
}
