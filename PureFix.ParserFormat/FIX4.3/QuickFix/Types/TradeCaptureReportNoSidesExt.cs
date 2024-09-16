using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class TradeCaptureReportNoSidesExt
	{
		public static void Parse(this TradeCaptureReportNoSides instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.Side = view.GetString(54);
			instance.OrderID = view.GetString(37);
			instance.SecondaryOrderID = view.GetString(198);
			instance.ClOrdID = view.GetString(11);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.Account = view.GetString(1);
			instance.AccountType = view.GetInt32(581);
			instance.ProcessCode = view.GetString(81);
			instance.OddLot = view.GetBool(575);
			var groupViewNoClearingInstructions = view.GetView("NoClearingInstructions");
			if (groupViewNoClearingInstructions is null) return;
			
			var countNoClearingInstructions = groupViewNoClearingInstructions.GroupCount();
			instance.NoClearingInstructions = new TradeCaptureReportNoSidesNoClearingInstructions[countNoClearingInstructions];
			for (var i = 0; i < countNoClearingInstructions; ++i)
			{
				instance.NoClearingInstructions[i] = new();
				instance.NoClearingInstructions[i].Parse(groupViewNoClearingInstructions[i]);
			}
			instance.ClearingFeeIndicator = view.GetString(635);
			instance.TradeInputSource = view.GetString(578);
			instance.TradeInputDevice = view.GetString(579);
			instance.Currency = view.GetString(15);
			instance.ComplianceID = view.GetString(376);
			instance.SolicitedFlag = view.GetBool(377);
			instance.OrderCapacity = view.GetString(528);
			instance.OrderRestrictions = view.GetString(529);
			instance.CustOrderCapacity = view.GetInt32(582);
			instance.TransBkdTime = view.GetDateTime(483);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			if (view.GetView("CommissionData") is MsgView groupViewCommissionData)
			{
				instance.CommissionData = new CommissionData();
				instance.CommissionData!.Parse(groupViewCommissionData);
			}
			instance.GrossTradeAmt = view.GetDouble(381);
			instance.NumDaysInterest = view.GetInt32(157);
			instance.ExDate = view.GetString(230);
			instance.AccruedInterestRate = view.GetDouble(158);
			instance.AccruedInterestAmt = view.GetDouble(159);
			instance.Concession = view.GetDouble(238);
			instance.TotalTakedown = view.GetDouble(237);
			instance.NetMoney = view.GetDouble(118);
			instance.SettlCurrAmt = view.GetDouble(119);
			instance.SettlCurrency = view.GetString(120);
			instance.SettlCurrFxRate = view.GetDouble(155);
			instance.SettlCurrFxRateCalc = view.GetString(156);
			instance.PositionEffect = view.GetString(77);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.MultiLegReportingType = view.GetString(442);
			var groupViewNoContAmts = view.GetView("NoContAmts");
			if (groupViewNoContAmts is null) return;
			
			var countNoContAmts = groupViewNoContAmts.GroupCount();
			instance.NoContAmts = new TradeCaptureReportNoSidesNoContAmts[countNoContAmts];
			for (var i = 0; i < countNoContAmts; ++i)
			{
				instance.NoContAmts[i] = new();
				instance.NoContAmts[i].Parse(groupViewNoContAmts[i]);
			}
			var groupViewNoMiscFees = view.GetView("NoMiscFees");
			if (groupViewNoMiscFees is null) return;
			
			var countNoMiscFees = groupViewNoMiscFees.GroupCount();
			instance.NoMiscFees = new TradeCaptureReportNoSidesNoMiscFees[countNoMiscFees];
			for (var i = 0; i < countNoMiscFees; ++i)
			{
				instance.NoMiscFees[i] = new();
				instance.NoMiscFees[i].Parse(groupViewNoMiscFees[i]);
			}
		}
	}
}
