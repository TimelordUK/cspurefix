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
			
			var groupView = view.GetView("NoTrades");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoTrades = new TrdCollGrpNoTrades[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoTrades[i] = new();
				instance.NoTrades[i].Parse(groupView[i]);
			}
		}
	}
}
