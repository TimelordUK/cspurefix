using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class BidCompReqGrpNoBidComponentsExt
	{
		public static void Parse(this BidCompReqGrpNoBidComponents instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.ListID = view.GetString(66);
			instance.Side = view.GetString(54);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.NetGrossInd = view.GetInt32(430);
			instance.SettlType = view.GetString(63);
			instance.SettlDate = view.GetDateOnly(64);
			instance.Account = view.GetString(1);
			instance.AcctIDSource = view.GetInt32(660);
		}
	}
}
