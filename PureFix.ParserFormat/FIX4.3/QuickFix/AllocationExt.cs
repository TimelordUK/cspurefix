using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("J", FixVersion.FIX43)]
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
			instance.AllocType = view.GetInt32(626);
			instance.RefAllocID = view.GetString(72);
			instance.AllocLinkID = view.GetString(196);
			instance.AllocLinkType = view.GetInt32(197);
			instance.BookingRefID = view.GetString(466);
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
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			instance.Quantity = view.GetDouble(53);
			instance.LastMkt = view.GetString(30);
			instance.TradeOriginationDate = view.GetString(229);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.PriceType = view.GetInt32(423);
			instance.AvgPx = view.GetDouble(6);
			instance.Currency = view.GetString(15);
			instance.AvgPrxPrecision = view.GetInt32(74);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.TradeDate = view.GetDateOnly(75);
			instance.TransactTime = view.GetDateTime(60);
			instance.SettlmntTyp = view.GetString(63);
			instance.FutSettDate = view.GetDateOnly(64);
			instance.GrossTradeAmt = view.GetDouble(381);
			instance.Concession = view.GetDouble(238);
			instance.TotalTakedown = view.GetDouble(237);
			instance.NetMoney = view.GetDouble(118);
			instance.PositionEffect = view.GetString(77);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.NumDaysInterest = view.GetInt32(157);
			instance.AccruedInterestRate = view.GetDouble(158);
			instance.TotalAccruedInterestAmt = view.GetDouble(540);
			instance.LegalConfirm = view.GetBool(650);
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
