using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class TrdCollGrpExt
	{
		public static void Parse(this TrdCollGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoTrades = view.GetView("NoTrades");
			if (groupViewNoTrades is null) return;
			
			var countNoTrades = groupViewNoTrades.GroupCount();
			instance.NoTrades = new TrdCollGrpNoTrades[countNoTrades];
			for (var i = 0; i < countNoTrades; ++i)
			{
				instance.NoTrades[i] = new();
				instance.NoTrades[i].Parse(groupViewNoTrades[i]);
			}
		}
	}
}
