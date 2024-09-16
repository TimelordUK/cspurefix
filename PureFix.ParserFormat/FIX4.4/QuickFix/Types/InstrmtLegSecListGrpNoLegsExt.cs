using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class InstrmtLegSecListGrpNoLegsExt
	{
		public static void Parse(this InstrmtLegSecListGrpNoLegs instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("InstrumentLeg") is MsgView groupViewInstrumentLeg)
			{
				instance.InstrumentLeg = new InstrumentLeg();
				instance.InstrumentLeg!.Parse(groupViewInstrumentLeg);
			}
			instance.LegSwapType = view.GetInt32(690);
			instance.LegSettlType = view.GetString(587);
			if (view.GetView("LegStipulations") is MsgView groupViewLegStipulations)
			{
				instance.LegStipulations = new LegStipulations();
				instance.LegStipulations!.Parse(groupViewLegStipulations);
			}
			if (view.GetView("LegBenchmarkCurveData") is MsgView groupViewLegBenchmarkCurveData)
			{
				instance.LegBenchmarkCurveData = new LegBenchmarkCurveData();
				instance.LegBenchmarkCurveData!.Parse(groupViewLegBenchmarkCurveData);
			}
		}
	}
}
