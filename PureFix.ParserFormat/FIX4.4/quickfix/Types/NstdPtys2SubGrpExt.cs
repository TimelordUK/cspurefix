using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class NstdPtys2SubGrpExt
	{
		public static void Parse(this NstdPtys2SubGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoNested2PartySubIDs = new NstdPtys2SubGrpNoNested2PartySubIDs [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoNested2PartySubIDs[i] = new();
				instance.NoNested2PartySubIDs[i].Parse(view?[i]);
			}
		}
	}
}
