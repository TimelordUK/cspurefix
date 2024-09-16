using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("T", FixVersion.FIX43)]
	public static class SettlementInstructionsExt
	{
		public static void Parse(this SettlementInstructions instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.SettlInstID = view.GetString(162);
			instance.SettlInstTransType = view.GetString(163);
			instance.SettlInstRefID = view.GetString(214);
			instance.SettlInstMode = view.GetString(160);
			instance.SettlInstSource = view.GetString(165);
			instance.AllocAccount = view.GetString(79);
			instance.IndividualAllocID = view.GetString(467);
			instance.ClOrdID = view.GetString(11);
			instance.TradeDate = view.GetDateOnly(75);
			instance.AllocID = view.GetString(70);
			instance.LastMkt = view.GetString(30);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.Side = view.GetString(54);
			instance.SecurityType = view.GetString(167);
			instance.EffectiveTime = view.GetDateTime(168);
			instance.TransactTime = view.GetDateTime(60);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.StandInstDbType = view.GetInt32(169);
			instance.StandInstDbName = view.GetString(170);
			instance.StandInstDbID = view.GetString(171);
			instance.SettlDeliveryType = view.GetInt32(172);
			instance.SettlDepositoryCode = view.GetString(173);
			instance.SettlBrkrCode = view.GetString(174);
			instance.SettlInstCode = view.GetString(175);
			instance.SecuritySettlAgentName = view.GetString(176);
			instance.SecuritySettlAgentCode = view.GetString(177);
			instance.SecuritySettlAgentAcctNum = view.GetString(178);
			instance.SecuritySettlAgentAcctName = view.GetString(179);
			instance.SecuritySettlAgentContactName = view.GetString(180);
			instance.SecuritySettlAgentContactPhone = view.GetString(181);
			instance.CashSettlAgentName = view.GetString(182);
			instance.CashSettlAgentCode = view.GetString(183);
			instance.CashSettlAgentAcctNum = view.GetString(184);
			instance.CashSettlAgentAcctName = view.GetString(185);
			instance.CashSettlAgentContactName = view.GetString(186);
			instance.CashSettlAgentContactPhone = view.GetString(187);
			instance.PaymentMethod = view.GetInt32(492);
			instance.PaymentRef = view.GetString(476);
			instance.CardHolderName = view.GetString(488);
			instance.CardNumber = view.GetString(489);
			instance.CardStartDate = view.GetDateOnly(503);
			instance.CardExpDate = view.GetDateOnly(490);
			instance.CardIssNo = view.GetString(491);
			instance.PaymentDate = view.GetDateOnly(504);
			instance.PaymentRemitterID = view.GetString(505);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
