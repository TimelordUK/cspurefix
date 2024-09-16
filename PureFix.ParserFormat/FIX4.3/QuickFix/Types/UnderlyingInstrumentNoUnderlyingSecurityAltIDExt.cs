using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class UnderlyingInstrumentNoUnderlyingSecurityAltIDExt
	{
		public static void Parse(this UnderlyingInstrumentNoUnderlyingSecurityAltID instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.UnderlyingSecurityAltID = view.GetString(458);
			instance.UnderlyingSecurityAltIDSource = view.GetString(459);
		}
	}
}
