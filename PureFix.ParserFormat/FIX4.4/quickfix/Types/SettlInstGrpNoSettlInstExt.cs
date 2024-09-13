using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SettlInstGrpNoSettlInstExt
	{
		public static void Parse(this SettlInstGrpNoSettlInst instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.SettlInstID = view.GetString(162);
			instance.SettlInstTransType = view.GetString(163);
			instance.SettlInstRefID = view.GetString(214);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.Side = view.GetString(54);
			instance.Product = view.GetInt32(460);
			instance.SecurityType = view.GetString(167);
			instance.CFICode = view.GetString(461);
			instance.EffectiveTime = view.GetDateTime(168);
			instance.ExpireTime = view.GetDateTime(126);
			instance.LastUpdateTime = view.GetDateTime(779);
			if (view.GetView("SettlInstructionsData") is MsgView groupViewSettlInstructionsData)
			{
				instance.SettlInstructionsData = new SettlInstructionsData();
				instance.SettlInstructionsData!.Parse(groupViewSettlInstructionsData);
			}
			instance.PaymentMethod = view.GetInt32(492);
			instance.PaymentRef = view.GetString(476);
			instance.CardHolderName = view.GetString(488);
			instance.CardNumber = view.GetString(489);
			instance.CardStartDate = view.GetDateTime(503);
			instance.CardExpDate = view.GetDateTime(490);
			instance.CardIssNum = view.GetString(491);
			instance.PaymentDate = view.GetDateTime(504);
			instance.PaymentRemitterID = view.GetString(505);
		}
	}
}
