using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class ClrInstGrpExt
	{
		public static void Parse(this ClrInstGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoClearingInstructions = new ClrInstGrpNoClearingInstructions [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoClearingInstructions[i] = new();
				instance.NoClearingInstructions[i].Parse(view?[i]);
			}
		}
	}
}
