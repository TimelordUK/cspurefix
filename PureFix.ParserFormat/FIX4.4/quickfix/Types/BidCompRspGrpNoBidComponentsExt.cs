using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class BidCompRspGrpNoBidComponentsExt
	{
		public static void Parse(this BidCompRspGrpNoBidComponents instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("CommissionData") is MsgView groupViewCommissionData)
			{
				instance.CommissionData = new CommissionData();
				instance.CommissionData!.Parse(groupViewCommissionData);
			}
			instance.ListID = view.GetString(66);
			instance.Country = view.GetString(421);
			instance.Side = view.GetString(54);
			instance.Price = view.GetDouble(44);
			instance.PriceType = view.GetInt32(423);
			instance.FairValue = view.GetDouble(406);
			instance.NetGrossInd = view.GetInt32(430);
			instance.SettlType = view.GetString(63);
			instance.SettlDate = view.GetDateTime(64);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
		}
	}
}
