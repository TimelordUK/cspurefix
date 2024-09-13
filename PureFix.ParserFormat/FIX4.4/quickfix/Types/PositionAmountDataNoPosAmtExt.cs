using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class PositionAmountDataNoPosAmtExt
	{
		public static void Parse(this PositionAmountDataNoPosAmt instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.PosAmtType = view.GetString(707);
			instance.PosAmt = view.GetDouble(708);
		}
	}
}
