using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SettlInstGrpExt
	{
		public static void Parse(this SettlInstGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoSettlInst = new SettlInstGrpNoSettlInst [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoSettlInst[i] = new();
				instance.NoSettlInst[i].Parse(view?[i]);
			}
		}
	}
}
