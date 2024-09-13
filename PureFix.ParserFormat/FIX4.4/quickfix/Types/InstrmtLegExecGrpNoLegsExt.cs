using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class InstrmtLegExecGrpNoLegsExt
	{
		public static void Parse(this InstrmtLegExecGrpNoLegs instance, MsgView? view)
		{
			instance.InstrumentLeg?.Parse(view?.GetView("InstrumentLeg"));
			instance.LegQty = view?.GetDouble(687);
			instance.LegSwapType = view?.GetInt32(690);
			instance.LegStipulations?.Parse(view?.GetView("LegStipulations"));
			instance.LegPositionEffect = view?.GetString(564);
			instance.LegCoveredOrUncovered = view?.GetInt32(565);
			instance.NestedParties?.Parse(view?.GetView("NestedParties"));
			instance.LegRefID = view?.GetString(654);
			instance.LegPrice = view?.GetDouble(566);
			instance.LegSettlType = view?.GetString(587);
			instance.LegSettlDate = view?.GetDateTime(588);
			instance.LegLastPx = view?.GetDouble(637);
		}
	}
}
