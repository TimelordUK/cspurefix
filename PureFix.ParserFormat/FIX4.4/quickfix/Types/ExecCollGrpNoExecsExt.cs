using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class ExecCollGrpNoExecsExt
	{
		public static void Parse(this ExecCollGrpNoExecs instance, MsgView? view)
		{
			instance.ExecID = view?.GetString(17);
		}
	}
}
