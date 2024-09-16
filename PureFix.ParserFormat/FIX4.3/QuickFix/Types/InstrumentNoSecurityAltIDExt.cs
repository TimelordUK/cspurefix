using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class InstrumentNoSecurityAltIDExt
	{
		public static void Parse(this InstrumentNoSecurityAltID instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.SecurityAltID = view.GetString(455);
			instance.SecurityAltIDSource = view.GetString(456);
		}
	}
}
