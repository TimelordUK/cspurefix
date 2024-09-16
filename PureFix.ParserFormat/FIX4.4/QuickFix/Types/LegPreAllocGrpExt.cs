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
			
			var groupViewNoLegAllocs = view.GetView("NoLegAllocs");
			if (groupViewNoLegAllocs is null) return;
			
			var countNoLegAllocs = groupViewNoLegAllocs.GroupCount();
			instance.NoLegAllocs = new LegPreAllocGrpNoLegAllocs[countNoLegAllocs];
			for (var i = 0; i < countNoLegAllocs; ++i)
			{
				instance.NoLegAllocs[i] = new();
				instance.NoLegAllocs[i].Parse(groupViewNoLegAllocs[i]);
			}
		}
	}
}
