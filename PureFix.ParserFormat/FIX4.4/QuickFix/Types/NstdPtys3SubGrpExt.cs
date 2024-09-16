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
			
			var groupViewNoNested3PartySubIDs = view.GetView("NoNested3PartySubIDs");
			if (groupViewNoNested3PartySubIDs is null) return;
			
			var countNoNested3PartySubIDs = groupViewNoNested3PartySubIDs.GroupCount();
			instance.NoNested3PartySubIDs = new NstdPtys3SubGrpNoNested3PartySubIDs[countNoNested3PartySubIDs];
			for (var i = 0; i < countNoNested3PartySubIDs; ++i)
			{
				instance.NoNested3PartySubIDs[i] = new();
				instance.NoNested3PartySubIDs[i].Parse(groupViewNoNested3PartySubIDs[i]);
			}
		}
	}
}
