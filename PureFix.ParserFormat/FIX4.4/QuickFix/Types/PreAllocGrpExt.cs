using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class PreAllocGrpExt
	{
		public static void Parse(this PreAllocGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoAllocs");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoAllocs = new PreAllocGrpNoAllocs[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoAllocs[i] = new();
				instance.NoAllocs[i].Parse(groupView[i]);
			}
		}
	}
}
