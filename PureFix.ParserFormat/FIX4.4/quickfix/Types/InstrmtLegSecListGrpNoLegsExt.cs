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
			instance.InstrumentLeg?.Parse(view?.GetView("InstrumentLeg"));
			instance.LegSwapType = view?.GetInt32(690);
			instance.LegSettlType = view?.GetString(587);
			instance.LegStipulations?.Parse(view?.GetView("LegStipulations"));
			instance.LegBenchmarkCurveData?.Parse(view?.GetView("LegBenchmarkCurveData"));
		}
	}
}
