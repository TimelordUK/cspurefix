using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class ContraGrpNoContraBrokersExt
	{
		public static void Parse(this ContraGrpNoContraBrokers instance, MsgView? view)
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
