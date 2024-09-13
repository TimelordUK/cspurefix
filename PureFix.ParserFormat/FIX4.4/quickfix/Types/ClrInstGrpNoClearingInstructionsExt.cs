using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class ClrInstGrpNoClearingInstructionsExt
	{
		public static void Parse(this ClrInstGrpNoClearingInstructions instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.ClearingInstruction = view.GetInt32(577);
		}
	}
}
