using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class UndInstrmtGrpExt
	{
		public static void Parse(this UndInstrmtGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var count = view.GroupCount();
			instance.NoUnderlyings = new UndInstrmtGrpNoUnderlyings [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoUnderlyings[i] = new();
				instance.NoUnderlyings[i].Parse(view[i]);
			}
		}
	}
}
