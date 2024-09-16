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
			
			var groupViewNoUnderlyings = view.GetView("NoUnderlyings");
			if (groupViewNoUnderlyings is null) return;
			
			var countNoUnderlyings = groupViewNoUnderlyings.GroupCount();
			instance.NoUnderlyings = new UndInstrmtCollGrpNoUnderlyings[countNoUnderlyings];
			for (var i = 0; i < countNoUnderlyings; ++i)
			{
				instance.NoUnderlyings[i] = new();
				instance.NoUnderlyings[i].Parse(groupViewNoUnderlyings[i]);
			}
		}
	}
}
