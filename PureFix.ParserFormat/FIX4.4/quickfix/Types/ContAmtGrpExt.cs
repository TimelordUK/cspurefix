using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class ContAmtGrpExt
	{
		public static void Parse(this ContAmtGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoContAmts = new ContAmtGrpNoContAmts [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoContAmts[i] = new();
				instance.NoContAmts[i].Parse(view?[i]);
			}
		}
	}
}
