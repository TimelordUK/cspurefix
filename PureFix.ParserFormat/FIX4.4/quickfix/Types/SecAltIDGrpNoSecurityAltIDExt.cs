using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SecAltIDGrpNoSecurityAltIDExt
	{
		public static void Parse(this SecAltIDGrpNoSecurityAltID instance, MsgView? view)
		{
			instance.SecurityAltID = view?.GetString(455);
			instance.SecurityAltIDSource = view?.GetString(456);
		}
	}
}
