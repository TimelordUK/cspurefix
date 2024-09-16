using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("G", FixVersion.FIX42)]
	public static class OrderCancelReplaceRequestExt
	{
		public static void Parse(this OrderCancelReplaceRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.OrderID = view.GetString(37);
			instance.ClientID = view.GetString(109);
			instance.ExecBroker = view.GetString(76);
			instance.OrigClOrdID = view.GetString(41);
			instance.ClOrdID = view.GetString(11);
			instance.ListID = view.GetString(66);
			instance.Account = view.GetString(1);
			var groupViewNoAllocs = view.GetView("NoAllocs");
			if (groupViewNoAllocs is null) return;
			
			var countNoAllocs = groupViewNoAllocs.GroupCount();
			instance.NoAllocs = new OrderCancelReplaceRequestNoAllocs[countNoAllocs];
			for (var i = 0; i < countNoAllocs; ++i)
			{
				instance.NoAllocs[i] = new();
				instance.NoAllocs[i].Parse(groupViewNoAllocs[i]);
			}
			instance.SettlmntTyp = view.GetString(63);
			instance.FutSettDate = view.GetDateOnly(64);
			instance.HandlInst = view.GetString(21);
			instance.ExecInst = view.GetString(18);
			instance.MinQty = view.GetDouble(110);
			instance.MaxFloor = view.GetDouble(111);
			instance.ExDestination = view.GetString(100);
			var groupViewNoTradingSessions = view.GetView("NoTradingSessions");
			if (groupViewNoTradingSessions is null) return;
			
			var countNoTradingSessions = groupViewNoTradingSessions.GroupCount();
			instance.NoTradingSessions = new OrderCancelReplaceRequestNoTradingSessions[countNoTradingSessions];
			for (var i = 0; i < countNoTradingSessions; ++i)
			{
				instance.NoTradingSessions[i] = new();
				instance.NoTradingSessions[i].Parse(groupViewNoTradingSessions[i]);
			}
			instance.Symbol = view.GetString(55);
			instance.SymbolSfx = view.GetString(65);
			instance.SecurityID = view.GetString(48);
			instance.IDSource = view.GetString(22);
			instance.SecurityType = view.GetString(167);
			instance.MaturityMonthYear = view.GetMonthYear(200);
			instance.MaturityDay = view.GetString(205);
			instance.PutOrCall = view.GetInt32(201);
			instance.StrikePrice = view.GetDouble(202);
			instance.OptAttribute = view.GetString(206);
			instance.ContractMultiplier = view.GetDouble(231);
			instance.CouponRate = view.GetDouble(223);
			instance.SecurityExchange = view.GetString(207);
			instance.Issuer = view.GetString(106);
			instance.EncodedIssuerLen = view.GetInt32(348);
			instance.EncodedIssuer = view.GetByteArray(349);
			instance.SecurityDesc = view.GetString(107);
			instance.EncodedSecurityDescLen = view.GetInt32(350);
			instance.EncodedSecurityDesc = view.GetByteArray(351);
			instance.Side = view.GetString(54);
			instance.TransactTime = view.GetDateTime(60);
			instance.OrderQty = view.GetDouble(38);
			instance.CashOrderQty = view.GetDouble(152);
			instance.OrdType = view.GetString(40);
			instance.Price = view.GetDouble(44);
			instance.StopPx = view.GetDouble(99);
			instance.PegDifference = view.GetDouble(211);
			instance.DiscretionInst = view.GetString(388);
			instance.DiscretionOffset = view.GetDouble(389);
			instance.ComplianceID = view.GetString(376);
			instance.SolicitedFlag = view.GetBool(377);
			instance.Currency = view.GetString(15);
			instance.TimeInForce = view.GetString(59);
			instance.EffectiveTime = view.GetDateTime(168);
			instance.ExpireDate = view.GetDateOnly(432);
			instance.ExpireTime = view.GetDateTime(126);
			instance.GTBookingInst = view.GetInt32(427);
			instance.Commission = view.GetDouble(12);
			instance.CommType = view.GetString(13);
			instance.Rule80A = view.GetString(47);
			instance.ForexReq = view.GetBool(121);
			instance.SettlCurrency = view.GetString(120);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.FutSettDate2 = view.GetDateOnly(193);
			instance.OrderQty2 = view.GetDouble(192);
			instance.OpenClose = view.GetString(77);
			instance.CoveredOrUncovered = view.GetInt32(203);
			instance.CustomerOrFirm = view.GetInt32(204);
			instance.MaxShow = view.GetDouble(210);
			instance.LocateReqd = view.GetBool(114);
			instance.ClearingFirm = view.GetString(439);
			instance.ClearingAccount = view.GetString(440);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
