using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class LegSecAltIDGrpNoLegSecurityAltIDExt
	{
		public static void Parse(this LegSecAltIDGrpNoLegSecurityAltID instance, MsgView? view)
		{
			instance.LegSecurityAltID = view?.GetString(605);
			instance.LegSecurityAltIDSource = view?.GetString(606);
		}
	}
}
