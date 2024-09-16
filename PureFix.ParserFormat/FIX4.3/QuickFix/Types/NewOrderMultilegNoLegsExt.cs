using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class NewOrderMultilegNoLegsExt
	{
		public static void Parse(this NewOrderMultilegNoLegs instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("InstrumentLeg") is MsgView groupViewInstrumentLeg)
			{
				instance.InstrumentLeg = new InstrumentLeg();
				instance.InstrumentLeg!.Parse(groupViewInstrumentLeg);
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
			instance.LegSettlmntTyp = view.GetString(587);
			instance.LegFutSettDate = view.GetDateOnly(588);
		}
	}
}
