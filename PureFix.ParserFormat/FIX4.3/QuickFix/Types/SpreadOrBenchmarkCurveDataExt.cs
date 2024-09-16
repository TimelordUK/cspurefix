using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class SpreadOrBenchmarkCurveDataExt
	{
		public static void Parse(this SpreadOrBenchmarkCurveData instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.Spread = view.GetDouble(218);
			instance.BenchmarkCurveCurrency = view.GetString(220);
			instance.BenchmarkCurveName = view.GetString(221);
			instance.BenchmarkCurvePoint = view.GetString(222);
		}
	}
}
