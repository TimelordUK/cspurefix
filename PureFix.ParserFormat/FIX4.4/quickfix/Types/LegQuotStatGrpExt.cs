using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class LegQuotStatGrpExt
	{
		public static void Parse(this LegQuotStatGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var count = view.GroupCount();
			instance.NoLegs = new LegQuotStatGrpNoLegs [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoLegs[i] = new();
				instance.NoLegs[i].Parse(view[i]);
			}
		}
	}
}
