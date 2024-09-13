using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class CompIDStatGrpExt
	{
		public static void Parse(this CompIDStatGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoCompIDs = new CompIDStatGrpNoCompIDs [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoCompIDs[i] = new();
				instance.NoCompIDs[i].Parse(view?[i]);
			}
		}
	}
}
