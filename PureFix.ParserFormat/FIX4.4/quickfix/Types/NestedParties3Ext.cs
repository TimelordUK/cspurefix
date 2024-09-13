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
			
			var count = view.GroupCount();
			instance.NoNested3PartyIDs = new NestedParties3NoNested3PartyIDs [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoNested3PartyIDs[i] = new();
				instance.NoNested3PartyIDs[i].Parse(view[i]);
			}
		}
	}
}
