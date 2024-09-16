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
			if (view is null) return;
			
			var groupViewNoPartySubIDs = view.GetView("NoPartySubIDs");
			if (groupViewNoPartySubIDs is null) return;
			
			var countNoPartySubIDs = groupViewNoPartySubIDs.GroupCount();
			instance.NoPartySubIDs = new PtysSubGrpNoPartySubIDs[countNoPartySubIDs];
			for (var i = 0; i < countNoPartySubIDs; ++i)
			{
				instance.NoPartySubIDs[i] = new();
				instance.NoPartySubIDs[i].Parse(groupViewNoPartySubIDs[i]);
			}
		}
	}
}
