using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class UndInstrmtCollGrpExt
	{
		public static void Parse(this UndInstrmtCollGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoUnderlyings");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoUnderlyings = new UndInstrmtCollGrpNoUnderlyings[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoUnderlyings[i] = new();
				instance.NoUnderlyings[i].Parse(groupView[i]);
			}
		}
	}
}
