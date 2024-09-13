using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class NestedParties2Ext
	{
		public static void Parse(this NestedParties2 instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoNested2PartyIDs");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoNested2PartyIDs = new NestedParties2NoNested2PartyIDs[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoNested2PartyIDs[i] = new();
				instance.NoNested2PartyIDs[i].Parse(groupView[i]);
			}
		}
	}
}
