using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class TradeCaptureReportNoSidesNoClearingInstructionsExt
	{
		public static void Parse(this TradeCaptureReportNoSidesNoClearingInstructions instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.ClearingInstruction = view.GetInt32(577);
		}
	}
}
