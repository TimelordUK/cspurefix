using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class TrdRegTimestampsExt
	{
		public static void Parse(this TrdRegTimestamps instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoTrdRegTimestamps = new TrdRegTimestampsNoTrdRegTimestamps [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoTrdRegTimestamps[i] = new();
				instance.NoTrdRegTimestamps[i].Parse(view?[i]);
			}
		}
	}
}
