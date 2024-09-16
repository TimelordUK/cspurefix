using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class NestedParties3Ext
	{
		public static void Parse(this NestedParties3 instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoNested3PartyIDs = view.GetView("NoNested3PartyIDs");
			if (groupViewNoNested3PartyIDs is null) return;
			
			var countNoNested3PartyIDs = groupViewNoNested3PartyIDs.GroupCount();
			instance.NoNested3PartyIDs = new NestedParties3NoNested3PartyIDs[countNoNested3PartyIDs];
			for (var i = 0; i < countNoNested3PartyIDs; ++i)
			{
				instance.NoNested3PartyIDs[i] = new();
				instance.NoNested3PartyIDs[i].Parse(groupViewNoNested3PartyIDs[i]);
			}
		}
	}
}
