using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class DlvyInstGrpExt
	{
		public static void Parse(this DlvyInstGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoDlvyInst = new DlvyInstGrpNoDlvyInst [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoDlvyInst[i] = new();
				instance.NoDlvyInst[i].Parse(view?[i]);
			}
		}
	}
}
