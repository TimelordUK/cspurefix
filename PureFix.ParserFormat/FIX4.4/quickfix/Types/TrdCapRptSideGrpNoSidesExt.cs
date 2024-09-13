using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class TrdCapRptSideGrpNoSidesExt
	{
		public static void Parse(this TrdCapRptSideGrpNoSides instance, MsgView? view)
		{
			instance.Side = view?.GetString(54);
			instance.OrderID = view?.GetString(37);
			instance.SecondaryOrderID = view?.GetString(198);
			instance.ClOrdID = view?.GetString(11);
			instance.SecondaryClOrdID = view?.GetString(526);
			instance.ListID = view?.GetString(66);
			instance.Parties?.Parse(view?.GetView("Parties"));
			instance.Account = view?.GetString(1);
			instance.AcctIDSource = view?.GetInt32(660);
			instance.AccountType = view?.GetInt32(581);
			instance.ProcessCode = view?.GetString(81);
			instance.OddLot = view?.GetBool(575);
			instance.ClrInstGrp?.Parse(view?.GetView("ClrInstGrp"));
			instance.TradeInputSource = view?.GetString(578);
			instance.TradeInputDevice = view?.GetString(579);
			instance.OrderInputDevice = view?.GetString(821);
			instance.Currency = view?.GetString(15);
			instance.ComplianceID = view?.GetString(376);
			instance.SolicitedFlag = view?.GetBool(377);
			instance.OrderCapacity = view?.GetString(528);
			instance.OrderRestrictions = view?.GetString(529);
			instance.CustOrderCapacity = view?.GetInt32(582);
			instance.OrdType = view?.GetString(40);
			instance.ExecInst = view?.GetString(18);
			instance.TransBkdTime = view?.GetDateTime(483);
			instance.TradingSessionID = view?.GetString(336);
			instance.TradingSessionSubID = view?.GetString(625);
			instance.TimeBracket = view?.GetString(943);
			instance.CommissionData?.Parse(view?.GetView("CommissionData"));
			instance.GrossTradeAmt = view?.GetDouble(381);
			instance.NumDaysInterest = view?.GetInt32(157);
			instance.ExDate = view?.GetDateTime(230);
			instance.AccruedInterestRate = view?.GetDouble(158);
			instance.AccruedInterestAmt = view?.GetDouble(159);
			instance.InterestAtMaturity = view?.GetDouble(738);
			instance.EndAccruedInterestAmt = view?.GetDouble(920);
			instance.StartCash = view?.GetDouble(921);
			instance.EndCash = view?.GetDouble(922);
			instance.Concession = view?.GetDouble(238);
			instance.TotalTakedown = view?.GetDouble(237);
			instance.NetMoney = view?.GetDouble(118);
			instance.SettlCurrAmt = view?.GetDouble(119);
			instance.SettlCurrency = view?.GetString(120);
			instance.SettlCurrFxRate = view?.GetDouble(155);
			instance.SettlCurrFxRateCalc = view?.GetString(156);
			instance.PositionEffect = view?.GetString(77);
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.SideMultiLegReportingType = view?.GetInt32(752);
			instance.ContAmtGrp?.Parse(view?.GetView("ContAmtGrp"));
			instance.Stipulations?.Parse(view?.GetView("Stipulations"));
			instance.MiscFeesGrp?.Parse(view?.GetView("MiscFeesGrp"));
			instance.ExchangeRule = view?.GetString(825);
			instance.TradeAllocIndicator = view?.GetInt32(826);
			instance.PreallocMethod = view?.GetString(591);
			instance.AllocID = view?.GetString(70);
			instance.TrdAllocGrp?.Parse(view?.GetView("TrdAllocGrp"));
		}
	}
}
