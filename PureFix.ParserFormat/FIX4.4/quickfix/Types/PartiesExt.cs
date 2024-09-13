using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class PartiesExt
	{
		public static void Parse(this Parties instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoPartyIDs = new PartiesNoPartyIDs [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoPartyIDs[i] = new();
				instance.NoPartyIDs[i].Parse(view?[i]);
			}
		}
	}
}
