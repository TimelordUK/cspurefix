using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class BidRequestNoBidComponentsExt
	{
		public static void Parse(this BidRequestNoBidComponents instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.ListID = view.GetString(66);
			instance.Side = view.GetString(54);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.NetGrossInd = view.GetInt32(430);
			instance.SettlmntTyp = view.GetString(63);
			instance.FutSettDate = view.GetDateOnly(64);
			instance.Account = view.GetString(1);
		}
	}
}
