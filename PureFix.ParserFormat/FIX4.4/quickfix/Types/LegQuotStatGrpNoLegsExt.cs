using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class LegQuotStatGrpNoLegsExt
	{
		public static void Parse(this LegQuotStatGrpNoLegs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.InstrumentLeg?.Parse(view.GetView("InstrumentLeg"));
			instance.LegQty = view.GetDouble(687);
			instance.LegSwapType = view.GetInt32(690);
			instance.LegSettlType = view.GetString(587);
			instance.LegSettlDate = view.GetDateTime(588);
			instance.LegStipulations?.Parse(view.GetView("LegStipulations"));
			instance.NestedParties?.Parse(view.GetView("NestedParties"));
		}
	}
}
