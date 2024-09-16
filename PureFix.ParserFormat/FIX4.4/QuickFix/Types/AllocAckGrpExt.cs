using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class AllocAckGrpExt
	{
		public static void Parse(this AllocAckGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoAllocs = view.GetView("NoAllocs");
			if (groupViewNoAllocs is null) return;
			
			var countNoAllocs = groupViewNoAllocs.GroupCount();
			instance.NoAllocs = new AllocAckGrpNoAllocs[countNoAllocs];
			for (var i = 0; i < countNoAllocs; ++i)
			{
				instance.NoAllocs[i] = new();
				instance.NoAllocs[i].Parse(groupViewNoAllocs[i]);
			}
		}
	}
}
