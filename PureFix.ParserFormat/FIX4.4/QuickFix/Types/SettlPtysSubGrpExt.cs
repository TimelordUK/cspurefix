using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SettlPtysSubGrpExt
	{
		public static void Parse(this SettlPtysSubGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoSettlPartySubIDs = view.GetView("NoSettlPartySubIDs");
			if (groupViewNoSettlPartySubIDs is null) return;
			
			var countNoSettlPartySubIDs = groupViewNoSettlPartySubIDs.GroupCount();
			instance.NoSettlPartySubIDs = new SettlPtysSubGrpNoSettlPartySubIDs[countNoSettlPartySubIDs];
			for (var i = 0; i < countNoSettlPartySubIDs; ++i)
			{
				instance.NoSettlPartySubIDs[i] = new();
				instance.NoSettlPartySubIDs[i].Parse(groupViewNoSettlPartySubIDs[i]);
			}
		}
	}
}
