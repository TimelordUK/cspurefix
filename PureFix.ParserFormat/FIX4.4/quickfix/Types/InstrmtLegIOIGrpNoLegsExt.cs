using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class InstrmtLegIOIGrpNoLegsExt
	{
		public static void Parse(this InstrmtLegIOIGrpNoLegs instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("InstrumentLeg") is MsgView groupViewInstrumentLeg)
			{
				instance.InstrumentLeg = new InstrumentLeg();
				instance.InstrumentLeg!.Parse(groupViewInstrumentLeg);
			}
			instance.InstrumentLeg = new InstrumentLeg();
			instance.InstrumentLeg?.Parse(view.GetView("InstrumentLeg"));
			instance.LegIOIQty = view.GetString(682);
			if (view.GetView("LegStipulations") is MsgView groupViewLegStipulations)
			{
				instance.LegStipulations = new LegStipulations();
				instance.LegStipulations!.Parse(groupViewLegStipulations);
			}
			instance.LegStipulations = new LegStipulations();
			instance.LegStipulations?.Parse(view.GetView("LegStipulations"));
		}
	}
}
