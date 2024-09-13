using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class ExecAllocGrpNoExecsExt
	{
		public static void Parse(this ExecAllocGrpNoExecs instance, MsgView? view)
		{
			instance.LastQty = view?.GetDouble(32);
			instance.ExecID = view?.GetString(17);
			instance.SecondaryExecID = view?.GetString(527);
			instance.LastPx = view?.GetDouble(31);
			instance.LastParPx = view?.GetDouble(669);
			instance.LastCapacity = view?.GetString(29);
		}
	}
}
