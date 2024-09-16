using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("8", FixVersion.FIX42)]
	public static class ExecutionReportExt
	{
		public static void Parse(this ExecutionReport instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.OrderID = view.GetString(37);
			instance.SecondaryOrderID = view.GetString(198);
			instance.ClOrdID = view.GetString(11);
			instance.OrigClOrdID = view.GetString(41);
			instance.ClientID = view.GetString(109);
			instance.ExecBroker = view.GetString(76);
			var groupViewNoContraBrokers = view.GetView("NoContraBrokers");
			if (groupViewNoContraBrokers is null) return;
			
			var countNoContraBrokers = groupViewNoContraBrokers.GroupCount();
			instance.NoContraBrokers = new ExecutionReportNoContraBrokers[countNoContraBrokers];
			for (var i = 0; i < countNoContraBrokers; ++i)
			{
				instance.NoContraBrokers[i] = new();
				instance.NoContraBrokers[i].Parse(groupViewNoContraBrokers[i]);
			}
			instance.ListID = view.GetString(66);
			instance.ExecID = view.GetString(17);
			instance.ExecTransType = view.GetString(20);
			instance.ExecRefID = view.GetString(19);
			instance.ExecType = view.GetString(150);
			instance.OrdStatus = view.GetString(39);
			instance.OrdRejReason = view.GetInt32(103);
			instance.ExecRestatementReason = view.GetInt32(378);
			instance.Account = view.GetString(1);
			instance.SettlmntTyp = view.GetString(63);
			instance.FutSettDate = view.GetDateOnly(64);
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
			instance.OrderQty = view.GetDouble(38);
			instance.CashOrderQty = view.GetDouble(152);
			instance.OrdType = view.GetString(40);
			instance.Price = view.GetDouble(44);
			instance.StopPx = view.GetDouble(99);
			instance.PegDifference = view.GetDouble(211);
			instance.DiscretionInst = view.GetString(388);
			instance.DiscretionOffset = view.GetDouble(389);
			instance.Currency = view.GetString(15);
			instance.ComplianceID = view.GetString(376);
			instance.SolicitedFlag = view.GetBool(377);
			instance.TimeInForce = view.GetString(59);
			instance.EffectiveTime = view.GetDateTime(168);
			instance.ExpireDate = view.GetDateOnly(432);
			instance.ExpireTime = view.GetDateTime(126);
			instance.ExecInst = view.GetString(18);
			instance.Rule80A = view.GetString(47);
			instance.LastShares = view.GetDouble(32);
			instance.LastPx = view.GetDouble(31);
			instance.LastSpotRate = view.GetDouble(194);
			instance.LastForwardPoints = view.GetDouble(195);
			instance.LastMkt = view.GetString(30);
			instance.TradingSessionID = view.GetString(336);
			instance.LastCapacity = view.GetString(29);
			instance.LeavesQty = view.GetDouble(151);
			instance.CumQty = view.GetDouble(14);
			instance.AvgPx = view.GetDouble(6);
			instance.DayOrderQty = view.GetDouble(424);
			instance.DayCumQty = view.GetDouble(425);
			instance.DayAvgPx = view.GetDouble(426);
			instance.GTBookingInst = view.GetInt32(427);
			instance.TradeDate = view.GetDateOnly(75);
			instance.TransactTime = view.GetDateTime(60);
			instance.ReportToExch = view.GetBool(113);
			instance.Commission = view.GetDouble(12);
			instance.CommType = view.GetString(13);
			instance.GrossTradeAmt = view.GetDouble(381);
			instance.SettlCurrAmt = view.GetDouble(119);
			instance.SettlCurrency = view.GetString(120);
			instance.SettlCurrFxRate = view.GetDouble(155);
			instance.SettlCurrFxRateCalc = view.GetString(156);
			instance.HandlInst = view.GetString(21);
			instance.MinQty = view.GetDouble(110);
			instance.MaxFloor = view.GetDouble(111);
			instance.OpenClose = view.GetString(77);
			instance.MaxShow = view.GetDouble(210);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.FutSettDate2 = view.GetDateOnly(193);
			instance.OrderQty2 = view.GetDouble(192);
			instance.ClearingFirm = view.GetString(439);
			instance.ClearingAccount = view.GetString(440);
			instance.MultiLegReportingType = view.GetString(442);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
