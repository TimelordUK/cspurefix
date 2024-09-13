using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class UndSecAltIDGrpNoUnderlyingSecurityAltIDExt
	{
		public static void Parse(this UndSecAltIDGrpNoUnderlyingSecurityAltID instance, MsgView? view)
		{
			instance.UnderlyingSecurityAltID = view?.GetString(458);
			instance.UnderlyingSecurityAltIDSource = view?.GetString(459);
		}
	}
}
