using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class PtysSubGrpExt
	{
		public static void Parse(this PtysSubGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoPartySubIDs = new PtysSubGrpNoPartySubIDs [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoPartySubIDs[i] = new();
				instance.NoPartySubIDs[i].Parse(view?[i]);
			}
		}
	}
}
