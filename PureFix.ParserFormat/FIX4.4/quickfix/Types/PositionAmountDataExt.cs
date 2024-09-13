using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class PositionAmountDataExt
	{
		public static void Parse(this PositionAmountData instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoPosAmt = new PositionAmountDataNoPosAmt [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoPosAmt[i] = new();
				instance.NoPosAmt[i].Parse(view?[i]);
			}
		}
	}
}
