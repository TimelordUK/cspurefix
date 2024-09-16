using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class TrdRegTimestampsNoTrdRegTimestampsExt
	{
		public static void Parse(this TrdRegTimestampsNoTrdRegTimestamps instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.TrdRegTimestamp = view.GetDateTime(769);
			instance.TrdRegTimestampType = view.GetInt32(770);
			instance.TrdRegTimestampOrigin = view.GetString(771);
		}
	}
}
