using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class AllocationNoAllocsNoMiscFeesExt
	{
		public static void Parse(this AllocationNoAllocsNoMiscFees instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.MiscFeeAmt = view.GetDouble(137);
			instance.MiscFeeCurr = view.GetString(138);
			instance.MiscFeeType = view.GetString(139);
		}
	}
}
