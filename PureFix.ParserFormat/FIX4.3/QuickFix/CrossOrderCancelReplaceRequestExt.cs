using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("t", FixVersion.FIX43)]
	public static class CrossOrderCancelReplaceRequestExt
	{
		public static void Parse(this CrossOrderCancelReplaceRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.OrderID = view.GetString(37);
			instance.CrossID = view.GetString(548);
			instance.OrigCrossID = view.GetString(551);
			instance.CrossType = view.GetInt32(549);
			instance.CrossPrioritization = view.GetInt32(550);
			var groupViewNoSides = view.GetView("NoSides");
			if (groupViewNoSides is null) return;
			
			var countNoSides = groupViewNoSides.GroupCount();
			instance.NoSides = new CrossOrderCancelReplaceRequestNoSides[countNoSides];
			for (var i = 0; i < countNoSides; ++i)
			{
				instance.NoSides[i] = new();
				instance.NoSides[i].Parse(groupViewNoSides[i]);
			}
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
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
			instance.NoTradingSessions = new CrossOrderCancelReplaceRequestNoTradingSessions[countNoTradingSessions];
			for (var i = 0; i < countNoTradingSessions; ++i)
			{
				instance.NoTradingSessions[i] = new();
				instance.NoTradingSessions[i].Parse(groupViewNoTradingSessions[i]);
			}
			instance.ProcessCode = view.GetString(81);
			instance.PrevClosePx = view.GetDouble(140);
			instance.LocateReqd = view.GetBool(114);
			instance.TransactTime = view.GetDateTime(60);
			if (view.GetView("Stipulations") is MsgView groupViewStipulations)
			{
				instance.Stipulations = new Stipulations();
				instance.Stipulations!.Parse(groupViewStipulations);
			}
			instance.OrdType = view.GetString(40);
			instance.PriceType = view.GetInt32(423);
			instance.Price = view.GetDouble(44);
			instance.StopPx = view.GetDouble(99);
			if (view.GetView("SpreadOrBenchmarkCurveData") is MsgView groupViewSpreadOrBenchmarkCurveData)
			{
				instance.SpreadOrBenchmarkCurveData = new SpreadOrBenchmarkCurveData();
				instance.SpreadOrBenchmarkCurveData!.Parse(groupViewSpreadOrBenchmarkCurveData);
			}
			if (view.GetView("YieldData") is MsgView groupViewYieldData)
			{
				instance.YieldData = new YieldData();
				instance.YieldData!.Parse(groupViewYieldData);
			}
			instance.Currency = view.GetString(15);
			instance.ComplianceID = view.GetString(376);
			instance.IOIid = view.GetString(23);
			instance.QuoteID = view.GetString(117);
			instance.TimeInForce = view.GetString(59);
			instance.EffectiveTime = view.GetDateTime(168);
			instance.ExpireDate = view.GetDateOnly(432);
			instance.ExpireTime = view.GetDateTime(126);
			instance.GTBookingInst = view.GetInt32(427);
			instance.MaxShow = view.GetDouble(210);
			instance.PegDifference = view.GetDouble(211);
			instance.DiscretionInst = view.GetString(388);
			instance.DiscretionOffset = view.GetDouble(389);
			instance.CancellationRights = view.GetString(480);
			instance.MoneyLaunderingStatus = view.GetString(481);
			instance.RegistID = view.GetString(513);
			instance.Designation = view.GetString(494);
			instance.AccruedInterestRate = view.GetDouble(158);
			instance.AccruedInterestAmt = view.GetDouble(159);
			instance.NetMoney = view.GetDouble(118);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
