using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class NstdPtys3SubGrpExt
	{
		public static void Parse(this NstdPtys3SubGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var count = view.GroupCount();
			instance.NoNested3PartySubIDs = new NstdPtys3SubGrpNoNested3PartySubIDs [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoNested3PartySubIDs[i] = new();
				instance.NoNested3PartySubIDs[i].Parse(view[i]);
			}
		}
	}
}
