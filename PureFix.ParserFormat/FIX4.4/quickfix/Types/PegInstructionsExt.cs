using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class PegInstructionsExt
	{
		public static void Parse(this PegInstructions instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.PegOffsetValue = view.GetDouble(211);
			instance.PegMoveType = view.GetInt32(835);
			instance.PegOffsetType = view.GetInt32(836);
			instance.PegLimitType = view.GetInt32(837);
			instance.PegRoundDirection = view.GetInt32(838);
			instance.PegScope = view.GetInt32(840);
		}
	}
}
