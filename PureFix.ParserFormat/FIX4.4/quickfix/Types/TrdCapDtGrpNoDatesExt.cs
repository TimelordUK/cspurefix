using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class TrdCapDtGrpNoDatesExt
	{
		public static void Parse(this TrdCapDtGrpNoDates instance, MsgView? view)
		{
			instance.TradeDate = view?.GetDateTime(75);
			instance.TransactTime = view?.GetDateTime(60);
		}
	}
}
