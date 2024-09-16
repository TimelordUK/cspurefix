using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public static class AllocationNoExecsExt
	{
		public static void Parse(this AllocationNoExecs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.LastShares = view.GetDouble(32);
			instance.ExecID = view.GetString(17);
			instance.LastPx = view.GetDouble(31);
			instance.LastCapacity = view.GetString(29);
		}
	}
}
