using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("8", FixVersion.FIX43)]
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
			instance.SecondaryClOrdID = view.GetString(526);
			instance.SecondaryExecID = view.GetString(527);
			instance.ClOrdID = view.GetString(11);
			instance.OrigClOrdID = view.GetString(41);
			instance.ClOrdLinkID = view.GetString(583);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.TradeOriginationDate = view.GetString(229);
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
			instance.CrossID = view.GetString(548);
			instance.OrigCrossID = view.GetString(551);
			instance.CrossType = view.GetInt32(549);
			instance.ExecID = view.GetString(17);
			instance.ExecRefID = view.GetString(19);
			instance.ExecType = view.GetString(150);
			instance.OrdStatus = view.GetString(39);
			instance.WorkingIndicator = view.GetBool(636);
			instance.OrdRejReason = view.GetInt32(103);
			instance.ExecRestatementReason = view.GetInt32(378);
			instance.Account = view.GetString(1);
			instance.AccountType = view.GetInt32(581);
			instance.DayBookingInst = view.GetString(589);
			instance.BookingUnit = view.GetString(590);
			instance.PreallocMethod = view.GetString(591);
			instance.SettlmntTyp = view.GetString(63);
			instance.FutSettDate = view.GetDateOnly(64);
			instance.CashMargin = view.GetString(544);
			instance.ClearingFeeIndicator = view.GetString(635);
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			instance.Side = view.GetString(54);
			if (view.GetView("Stipulations") is MsgView groupViewStipulations)
			{
				instance.Stipulations = new Stipulations();
				instance.Stipulations!.Parse(groupViewStipulations);
			}
			instance.QuantityType = view.GetInt32(465);
			if (view.GetView("OrderQtyData") is MsgView groupViewOrderQtyData)
			{
				instance.OrderQtyData = new OrderQtyData();
				instance.OrderQtyData!.Parse(groupViewOrderQtyData);
			}
			instance.OrdType = view.GetString(40);
			instance.PriceType = view.GetInt32(423);
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
			instance.OrderCapacity = view.GetString(528);
			instance.OrderRestrictions = view.GetString(529);
			instance.CustOrderCapacity = view.GetInt32(582);
			instance.Rule80A = view.GetString(47);
			instance.LastQty = view.GetDouble(32);
			instance.UnderlyingLastQty = view.GetDouble(652);
			instance.LastPx = view.GetDouble(31);
			instance.UnderlyingLastPx = view.GetDouble(651);
			instance.LastSpotRate = view.GetDouble(194);
			instance.LastForwardPoints = view.GetDouble(195);
			instance.LastMkt = view.GetString(30);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
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
			if (view.GetView("CommissionData") is MsgView groupViewCommissionData)
			{
				instance.CommissionData = new CommissionData();
				instance.CommissionData!.Parse(groupViewCommissionData);
			}
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
			instance.GrossTradeAmt = view.GetDouble(381);
			instance.NumDaysInterest = view.GetInt32(157);
			instance.ExDate = view.GetString(230);
			instance.AccruedInterestRate = view.GetDouble(158);
			instance.AccruedInterestAmt = view.GetDouble(159);
			instance.TradedFlatSwitch = view.GetBool(258);
			instance.BasisFeatureDate = view.GetString(259);
			instance.BasisFeaturePrice = view.GetDouble(260);
			instance.Concession = view.GetDouble(238);
			instance.TotalTakedown = view.GetDouble(237);
			instance.NetMoney = view.GetDouble(118);
			instance.SettlCurrAmt = view.GetDouble(119);
			instance.SettlCurrency = view.GetString(120);
			instance.SettlCurrFxRate = view.GetDouble(155);
			instance.SettlCurrFxRateCalc = view.GetString(156);
			instance.HandlInst = view.GetString(21);
			instance.MinQty = view.GetDouble(110);
			instance.MaxFloor = view.GetDouble(111);
			instance.PositionEffect = view.GetString(77);
			instance.MaxShow = view.GetDouble(210);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.FutSettDate2 = view.GetDateOnly(193);
			instance.OrderQty2 = view.GetDouble(192);
			instance.LastForwardPoints2 = view.GetDouble(641);
			instance.MultiLegReportingType = view.GetString(442);
			instance.CancellationRights = view.GetString(480);
			instance.MoneyLaunderingStatus = view.GetString(481);
			instance.RegistID = view.GetString(513);
			instance.Designation = view.GetString(494);
			instance.TransBkdTime = view.GetDateTime(483);
			instance.ExecValuationPoint = view.GetDateTime(515);
			instance.ExecPriceType = view.GetString(484);
			instance.ExecPriceAdjustment = view.GetDouble(485);
			instance.PriorityIndicator = view.GetInt32(638);
			instance.PriceImprovement = view.GetDouble(639);
			var groupViewNoContAmts = view.GetView("NoContAmts");
			if (groupViewNoContAmts is null) return;
			
			var countNoContAmts = groupViewNoContAmts.GroupCount();
			instance.NoContAmts = new ExecutionReportNoContAmts[countNoContAmts];
			for (var i = 0; i < countNoContAmts; ++i)
			{
				instance.NoContAmts[i] = new();
				instance.NoContAmts[i].Parse(groupViewNoContAmts[i]);
			}
			var groupViewNoLegs = view.GetView("NoLegs");
			if (groupViewNoLegs is null) return;
			
			var countNoLegs = groupViewNoLegs.GroupCount();
			instance.NoLegs = new ExecutionReportNoLegs[countNoLegs];
			for (var i = 0; i < countNoLegs; ++i)
			{
				instance.NoLegs[i] = new();
				instance.NoLegs[i].Parse(groupViewNoLegs[i]);
			}
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
