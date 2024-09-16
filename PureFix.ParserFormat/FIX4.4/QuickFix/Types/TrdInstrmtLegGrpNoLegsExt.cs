using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class TrdInstrmtLegGrpNoLegsExt
	{
		public static void Parse(this TrdInstrmtLegGrpNoLegs instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("InstrumentLeg") is MsgView groupViewInstrumentLeg)
			{
				instance.InstrumentLeg = new InstrumentLeg();
				instance.InstrumentLeg!.Parse(groupViewInstrumentLeg);
			}
			instance.LegQty = view.GetDouble(687);
			instance.LegSwapType = view.GetInt32(690);
			if (view.GetView("LegStipulations") is MsgView groupViewLegStipulations)
			{
				instance.LegStipulations = new LegStipulations();
				instance.LegStipulations!.Parse(groupViewLegStipulations);
			}
			instance.LegPositionEffect = view.GetString(564);
			instance.LegCoveredOrUncovered = view.GetInt32(565);
			if (view.GetView("NestedParties") is MsgView groupViewNestedParties)
			{
				instance.NestedParties = new NestedParties();
				instance.NestedParties!.Parse(groupViewNestedParties);
			}
			instance.LegRefID = view.GetString(654);
			instance.LegPrice = view.GetDouble(566);
			instance.LegSettlType = view.GetString(587);
			instance.LegSettlDate = view.GetDateOnly(588);
			instance.LegLastPx = view.GetDouble(637);
		}
	}
}
