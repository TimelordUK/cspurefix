using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class PartiesExt
	{
		public static void Parse(this Parties instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoPartyIDs = view.GetView("NoPartyIDs");
			if (groupViewNoPartyIDs is null) return;
			
			var countNoPartyIDs = groupViewNoPartyIDs.GroupCount();
			instance.NoPartyIDs = new PartiesNoPartyIDs[countNoPartyIDs];
			for (var i = 0; i < countNoPartyIDs; ++i)
			{
				instance.NoPartyIDs[i] = new();
				instance.NoPartyIDs[i].Parse(groupViewNoPartyIDs[i]);
			}
		}
	}
}
