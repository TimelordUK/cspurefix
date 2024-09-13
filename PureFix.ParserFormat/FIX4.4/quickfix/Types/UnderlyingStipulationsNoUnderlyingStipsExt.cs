using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class UnderlyingStipulationsNoUnderlyingStipsExt
	{
		public static void Parse(this UnderlyingStipulationsNoUnderlyingStips instance, MsgView? view)
		{
			instance.UnderlyingStipType = view?.GetString(888);
			instance.UnderlyingStipValue = view?.GetString(889);
		}
	}
}
