using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class NestedPartiesExt
	{
		public static void Parse(this NestedParties instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoNestedPartyIDs = view.GetView("NoNestedPartyIDs");
			if (groupViewNoNestedPartyIDs is null) return;
			
			var countNoNestedPartyIDs = groupViewNoNestedPartyIDs.GroupCount();
			instance.NoNestedPartyIDs = new NestedPartiesNoNestedPartyIDs[countNoNestedPartyIDs];
			for (var i = 0; i < countNoNestedPartyIDs; ++i)
			{
				instance.NoNestedPartyIDs[i] = new();
				instance.NoNestedPartyIDs[i].Parse(groupViewNoNestedPartyIDs[i]);
			}
		}
	}
}
