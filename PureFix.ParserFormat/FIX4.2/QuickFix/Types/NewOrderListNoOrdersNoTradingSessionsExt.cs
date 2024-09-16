using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public static class NewOrderListNoOrdersNoTradingSessionsExt
	{
		public static void Parse(this NewOrderListNoOrdersNoTradingSessions instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.TradingSessionID = view.GetString(336);
		}
	}
}
