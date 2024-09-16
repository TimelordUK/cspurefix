using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class AllocationNoExecsExt
	{
		public static void Parse(this AllocationNoExecs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.LastQty = view.GetDouble(32);
			instance.ExecID = view.GetString(17);
			instance.SecondaryExecID = view.GetString(527);
			instance.LastPx = view.GetDouble(31);
			instance.LastCapacity = view.GetString(29);
		}
	}
}
