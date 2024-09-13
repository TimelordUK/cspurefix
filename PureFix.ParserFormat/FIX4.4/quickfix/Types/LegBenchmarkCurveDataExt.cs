using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class LegBenchmarkCurveDataExt
	{
		public static void Parse(this LegBenchmarkCurveData instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.LegBenchmarkCurveCurrency = view.GetString(676);
			instance.LegBenchmarkCurveName = view.GetString(677);
			instance.LegBenchmarkCurvePoint = view.GetString(678);
			instance.LegBenchmarkPrice = view.GetDouble(679);
			instance.LegBenchmarkPriceType = view.GetInt32(680);
		}
	}
}
