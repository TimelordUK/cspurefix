using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class StandardHeaderNoHopsExt
	{
		public static void Parse(this StandardHeaderNoHops instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.HopCompID = view.GetString(628);
			instance.HopSendingTime = view.GetDateTime(629);
			instance.HopRefID = view.GetInt32(630);
		}
	}
}
