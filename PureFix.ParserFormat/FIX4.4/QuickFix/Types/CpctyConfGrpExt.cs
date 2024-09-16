using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class CpctyConfGrpExt
	{
		public static void Parse(this CpctyConfGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoCapacities = view.GetView("NoCapacities");
			if (groupViewNoCapacities is null) return;
			
			var countNoCapacities = groupViewNoCapacities.GroupCount();
			instance.NoCapacities = new CpctyConfGrpNoCapacities[countNoCapacities];
			for (var i = 0; i < countNoCapacities; ++i)
			{
				instance.NoCapacities[i] = new();
				instance.NoCapacities[i].Parse(groupViewNoCapacities[i]);
			}
		}
	}
}
