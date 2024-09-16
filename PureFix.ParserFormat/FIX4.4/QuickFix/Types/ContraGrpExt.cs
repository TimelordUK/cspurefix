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
			
			var groupViewNoContraBrokers = view.GetView("NoContraBrokers");
			if (groupViewNoContraBrokers is null) return;
			
			var countNoContraBrokers = groupViewNoContraBrokers.GroupCount();
			instance.NoContraBrokers = new ContraGrpNoContraBrokers[countNoContraBrokers];
			for (var i = 0; i < countNoContraBrokers; ++i)
			{
				instance.NoContraBrokers[i] = new();
				instance.NoContraBrokers[i].Parse(groupViewNoContraBrokers[i]);
			}
		}
	}
}
