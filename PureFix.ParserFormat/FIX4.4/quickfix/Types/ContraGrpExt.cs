using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class ContraGrpExt
	{
		public static void Parse(this ContraGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoContraBrokers");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoContraBrokers = new ContraGrpNoContraBrokers[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoContraBrokers[i] = new();
				instance.NoContraBrokers[i].Parse(groupView[i]);
			}
		}
	}
}
