using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class InstrumentLegNoLegSecurityAltIDExt
	{
		public static void Parse(this InstrumentLegNoLegSecurityAltID instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.LegSecurityAltID = view.GetString(605);
			instance.LegSecurityAltIDSource = view.GetString(606);
		}
	}
}
