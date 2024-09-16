using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class ExecutionReportNoContraBrokersExt
	{
		public static void Parse(this ExecutionReportNoContraBrokers instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.ContraBroker = view.GetString(375);
			instance.ContraTrader = view.GetString(337);
			instance.ContraTradeQty = view.GetDouble(437);
			instance.ContraTradeTime = view.GetDateTime(438);
			instance.ContraLegRefID = view.GetString(655);
		}
	}
}
