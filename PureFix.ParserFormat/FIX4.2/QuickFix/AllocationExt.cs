using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("J", FixVersion.FIX42)]
	public static class AllocationExt
	{
		public static void Parse(this Allocation instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.AllocID = view.GetString(70);
			instance.AllocTransType = view.GetString(71);
			instance.RefAllocID = view.GetString(72);
			instance.AllocLinkID = view.GetString(196);
			instance.AllocLinkType = view.GetInt32(197);
			var groupViewNoOrders = view.GetView("NoOrders");
			if (groupViewNoOrders is null) return;
			
			var countNoOrders = groupViewNoOrders.GroupCount();
			instance.NoOrders = new AllocationNoOrders[countNoOrders];
			for (var i = 0; i < countNoOrders; ++i)
			{
				instance.NoOrders[i] = new();
				instance.NoOrders[i].Parse(groupViewNoOrders[i]);
			}
			var groupViewNoExecs = view.GetView("NoExecs");
			if (groupViewNoExecs is null) return;
			
			var countNoExecs = groupViewNoExecs.GroupCount();
			instance.NoExecs = new AllocationNoExecs[countNoExecs];
			for (var i = 0; i < countNoExecs; ++i)
			{
				instance.NoExecs[i] = new();
				instance.NoExecs[i].Parse(groupViewNoExecs[i]);
			}
			instance.Side = view.GetString(54);
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
			instance.Shares = view.GetDouble(53);
			instance.LastMkt = view.GetString(30);
			instance.TradingSessionID = view.GetString(336);
			instance.AvgPx = view.GetDouble(6);
			instance.Currency = view.GetString(15);
			instance.AvgPrxPrecision = view.GetInt32(74);
			instance.TradeDate = view.GetDateOnly(75);
			instance.TransactTime = view.GetDateTime(60);
			instance.SettlmntTyp = view.GetString(63);
			instance.FutSettDate = view.GetDateOnly(64);
			instance.GrossTradeAmt = view.GetDouble(381);
			instance.NetMoney = view.GetDouble(118);
			instance.OpenClose = view.GetString(77);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.NumDaysInterest = view.GetInt32(157);
			instance.AccruedInterestRate = view.GetDouble(158);
			var groupViewNoAllocs = view.GetView("NoAllocs");
			if (groupViewNoAllocs is null) return;
			
			var countNoAllocs = groupViewNoAllocs.GroupCount();
			instance.NoAllocs = new AllocationNoAllocs[countNoAllocs];
			for (var i = 0; i < countNoAllocs; ++i)
			{
				instance.NoAllocs[i] = new();
				instance.NoAllocs[i].Parse(groupViewNoAllocs[i]);
			}
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
