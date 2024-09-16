using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class DiscretionInstructionsExt
	{
		public static void Parse(this DiscretionInstructions instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.DiscretionInst = view.GetString(388);
			instance.DiscretionOffsetValue = view.GetDouble(389);
			instance.DiscretionMoveType = view.GetInt32(841);
			instance.DiscretionOffsetType = view.GetInt32(842);
			instance.DiscretionLimitType = view.GetInt32(843);
			instance.DiscretionRoundDirection = view.GetInt32(844);
			instance.DiscretionScope = view.GetInt32(846);
		}
	}
}
