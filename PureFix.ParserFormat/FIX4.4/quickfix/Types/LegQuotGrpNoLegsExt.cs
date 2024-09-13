using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class LegQuotGrpNoLegsExt
	{
		public static void Parse(this LegQuotGrpNoLegs instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("InstrumentLeg") is MsgView groupViewInstrumentLeg)
			{
				instance.InstrumentLeg = new InstrumentLeg();
				instance.InstrumentLeg!.Parse(groupViewInstrumentLeg);
			}
			instance.InstrumentLeg = new InstrumentLeg();
			instance.InstrumentLeg?.Parse(view.GetView("InstrumentLeg"));
			instance.LegQty = view.GetDouble(687);
			instance.LegSwapType = view.GetInt32(690);
			instance.LegSettlType = view.GetString(587);
			instance.LegSettlDate = view.GetDateTime(588);
			if (view.GetView("LegStipulations") is MsgView groupViewLegStipulations)
			{
				instance.LegStipulations = new LegStipulations();
				instance.LegStipulations!.Parse(groupViewLegStipulations);
			}
			instance.LegStipulations = new LegStipulations();
			instance.LegStipulations?.Parse(view.GetView("LegStipulations"));
			if (view.GetView("NestedParties") is MsgView groupViewNestedParties)
			{
				instance.NestedParties = new NestedParties();
				instance.NestedParties!.Parse(groupViewNestedParties);
			}
			instance.NestedParties = new NestedParties();
			instance.NestedParties?.Parse(view.GetView("NestedParties"));
			instance.LegPriceType = view.GetInt32(686);
			instance.LegBidPx = view.GetDouble(681);
			instance.LegOfferPx = view.GetDouble(684);
			if (view.GetView("LegBenchmarkCurveData") is MsgView groupViewLegBenchmarkCurveData)
			{
				instance.LegBenchmarkCurveData = new LegBenchmarkCurveData();
				instance.LegBenchmarkCurveData!.Parse(groupViewLegBenchmarkCurveData);
			}
			instance.LegBenchmarkCurveData = new LegBenchmarkCurveData();
			instance.LegBenchmarkCurveData?.Parse(view.GetView("LegBenchmarkCurveData"));
		}
	}
}
