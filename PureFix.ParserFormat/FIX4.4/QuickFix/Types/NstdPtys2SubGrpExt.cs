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
			if (view is null) return;
			
			var groupViewNoNested2PartySubIDs = view.GetView("NoNested2PartySubIDs");
			if (groupViewNoNested2PartySubIDs is null) return;
			
			var countNoNested2PartySubIDs = groupViewNoNested2PartySubIDs.GroupCount();
			instance.NoNested2PartySubIDs = new NstdPtys2SubGrpNoNested2PartySubIDs[countNoNested2PartySubIDs];
			for (var i = 0; i < countNoNested2PartySubIDs; ++i)
			{
				instance.NoNested2PartySubIDs[i] = new();
				instance.NoNested2PartySubIDs[i].Parse(groupViewNoNested2PartySubIDs[i]);
			}
		}
	}
}
