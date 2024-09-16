using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class NewOrderMultilegNoTradingSessionsExt
	{
		public static void Parse(this NewOrderMultilegNoTradingSessions instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
		}
	}
}
