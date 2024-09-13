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
			var count = view?.GroupCount() ?? 0;
			instance.NoNestedPartyIDs = new NestedPartiesNoNestedPartyIDs [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoNestedPartyIDs[i] = new();
				instance.NoNestedPartyIDs[i].Parse(view?[i]);
			}
		}
	}
}
