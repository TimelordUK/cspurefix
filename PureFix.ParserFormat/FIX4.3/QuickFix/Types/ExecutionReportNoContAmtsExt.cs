using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class ExecutionReportNoContAmtsExt
	{
		public static void Parse(this ExecutionReportNoContAmts instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.ContAmtType = view.GetInt32(519);
			instance.ContAmtValue = view.GetDouble(520);
			instance.ContAmtCurr = view.GetString(521);
		}
	}
}
