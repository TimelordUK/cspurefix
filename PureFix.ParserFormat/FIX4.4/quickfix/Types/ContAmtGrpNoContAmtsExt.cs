using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class ContAmtGrpNoContAmtsExt
	{
		public static void Parse(this ContAmtGrpNoContAmts instance, MsgView? view)
		{
			instance.ContAmtType = view?.GetInt32(519);
			instance.ContAmtValue = view?.GetDouble(520);
			instance.ContAmtCurr = view?.GetString(521);
		}
	}
}
