using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class MiscFeesGrpNoMiscFeesExt
	{
		public static void Parse(this MiscFeesGrpNoMiscFees instance, MsgView? view)
		{
			instance.MiscFeeAmt = view?.GetDouble(137);
			instance.MiscFeeCurr = view?.GetString(138);
			instance.MiscFeeType = view?.GetString(139);
			instance.MiscFeeBasis = view?.GetInt32(891);
		}
	}
}
