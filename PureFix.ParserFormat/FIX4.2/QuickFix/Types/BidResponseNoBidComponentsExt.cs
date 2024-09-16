using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public static class BidResponseNoBidComponentsExt
	{
		public static void Parse(this BidResponseNoBidComponents instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.Commission = view.GetDouble(12);
			instance.CommType = view.GetString(13);
			instance.ListID = view.GetString(66);
			instance.Country = view.GetString(421);
			instance.Side = view.GetString(54);
			instance.Price = view.GetDouble(44);
			instance.PriceType = view.GetInt32(423);
			instance.FairValue = view.GetDouble(406);
			instance.NetGrossInd = view.GetInt32(430);
			instance.SettlmntTyp = view.GetString(63);
			instance.FutSettDate = view.GetDateOnly(64);
			instance.TradingSessionID = view.GetString(336);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
		}
	}
}
