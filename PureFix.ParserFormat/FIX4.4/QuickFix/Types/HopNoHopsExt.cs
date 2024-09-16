using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class HopNoHopsExt
	{
		public static void Parse(this HopNoHops instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.HopCompID = view.GetString(628);
			instance.HopSendingTime = view.GetDateTime(629);
			instance.HopRefID = view.GetInt32(630);
		}
	}
}
