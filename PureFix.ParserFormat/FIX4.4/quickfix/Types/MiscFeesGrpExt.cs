using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class MiscFeesGrpExt
	{
		public static void Parse(this MiscFeesGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var count = view.GroupCount();
			instance.NoMiscFees = new MiscFeesGrpNoMiscFees [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoMiscFees[i] = new();
				instance.NoMiscFees[i].Parse(view[i]);
			}
		}
	}
}
