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
			
			var groupView = view.GetView("NoSettlPartyIDs");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoSettlPartyIDs = new SettlPartiesNoSettlPartyIDs[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoSettlPartyIDs[i] = new();
				instance.NoSettlPartyIDs[i].Parse(groupView[i]);
			}
		}
	}
}
