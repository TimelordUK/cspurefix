using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SettlPartiesExt
	{
		public static void Parse(this SettlParties instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoSettlPartyIDs = view.GetView("NoSettlPartyIDs");
			if (groupViewNoSettlPartyIDs is null) return;
			
			var countNoSettlPartyIDs = groupViewNoSettlPartyIDs.GroupCount();
			instance.NoSettlPartyIDs = new SettlPartiesNoSettlPartyIDs[countNoSettlPartyIDs];
			for (var i = 0; i < countNoSettlPartyIDs; ++i)
			{
				instance.NoSettlPartyIDs[i] = new();
				instance.NoSettlPartyIDs[i].Parse(groupViewNoSettlPartyIDs[i]);
			}
		}
	}
}
