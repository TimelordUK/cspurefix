using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AK", FixVersion.FIX44)]
	public static class ConfirmationExt
	{
		public static void Parse(this Confirmation instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.ConfirmID = view.GetString(664);
			instance.ConfirmRefID = view.GetString(772);
			instance.ConfirmReqID = view.GetString(859);
			instance.ConfirmTransType = view.GetInt32(666);
			instance.ConfirmType = view.GetInt32(773);
			instance.CopyMsgIndicator = view.GetBool(797);
			instance.LegalConfirm = view.GetBool(650);
			instance.ConfirmStatus = view.GetInt32(665);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			if (view.GetView("OrdAllocGrp") is MsgView groupViewOrdAllocGrp)
			{
				instance.OrdAllocGrp = new OrdAllocGrp();
				instance.OrdAllocGrp!.Parse(groupViewOrdAllocGrp);
			}
			instance.AllocID = view.GetString(70);
			instance.SecondaryAllocID = view.GetString(793);
			instance.IndividualAllocID = view.GetString(467);
			instance.TransactTime = view.GetDateTime(60);
			instance.TradeDate = view.GetDateOnly(75);
			if (view.GetView("TrdRegTimestamps") is MsgView groupViewTrdRegTimestamps)
			{
				instance.TrdRegTimestamps = new TrdRegTimestamps();
				instance.TrdRegTimestamps!.Parse(groupViewTrdRegTimestamps);
			}
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			if (view.GetView("InstrumentExtension") is MsgView groupViewInstrumentExtension)
			{
				instance.InstrumentExtension = new InstrumentExtension();
				instance.InstrumentExtension!.Parse(groupViewInstrumentExtension);
			}
			if (view.GetView("FinancingDetails") is MsgView groupViewFinancingDetails)
			{
				instance.FinancingDetails = new FinancingDetails();
				instance.FinancingDetails!.Parse(groupViewFinancingDetails);
			}
			if (view.GetView("UndInstrmtGrp") is MsgView groupViewUndInstrmtGrp)
			{
				instance.UndInstrmtGrp = new UndInstrmtGrp();
				instance.UndInstrmtGrp!.Parse(groupViewUndInstrmtGrp);
			}
			if (view.GetView("InstrmtLegGrp") is MsgView groupViewInstrmtLegGrp)
			{
				instance.InstrmtLegGrp = new InstrmtLegGrp();
				instance.InstrmtLegGrp!.Parse(groupViewInstrmtLegGrp);
			}
			if (view.GetView("YieldData") is MsgView groupViewYieldData)
			{
				instance.YieldData = new YieldData();
				instance.YieldData!.Parse(groupViewYieldData);
			}
			instance.AllocQty = view.GetDouble(80);
			instance.QtyType = view.GetInt32(854);
			instance.Side = view.GetString(54);
			instance.Currency = view.GetString(15);
			instance.LastMkt = view.GetString(30);
			if (view.GetView("CpctyConfGrp") is MsgView groupViewCpctyConfGrp)
			{
				instance.CpctyConfGrp = new CpctyConfGrp();
				instance.CpctyConfGrp!.Parse(groupViewCpctyConfGrp);
			}
			instance.AllocAccount = view.GetString(79);
			instance.AllocAcctIDSource = view.GetInt32(661);
			instance.AllocAccountType = view.GetInt32(798);
			instance.AvgPx = view.GetDouble(6);
			instance.AvgPxPrecision = view.GetInt32(74);
			instance.PriceType = view.GetInt32(423);
			instance.AvgParPx = view.GetDouble(860);
			if (view.GetView("SpreadOrBenchmarkCurveData") is MsgView groupViewSpreadOrBenchmarkCurveData)
			{
				instance.SpreadOrBenchmarkCurveData = new SpreadOrBenchmarkCurveData();
				instance.SpreadOrBenchmarkCurveData!.Parse(groupViewSpreadOrBenchmarkCurveData);
			}
			instance.ReportedPx = view.GetDouble(861);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.ProcessCode = view.GetString(81);
			instance.GrossTradeAmt = view.GetDouble(381);
			instance.NumDaysInterest = view.GetInt32(157);
			instance.ExDate = view.GetDateOnly(230);
			instance.AccruedInterestRate = view.GetDouble(158);
			instance.AccruedInterestAmt = view.GetDouble(159);
			instance.InterestAtMaturity = view.GetDouble(738);
			instance.EndAccruedInterestAmt = view.GetDouble(920);
			instance.StartCash = view.GetDouble(921);
			instance.EndCash = view.GetDouble(922);
			instance.Concession = view.GetDouble(238);
			instance.TotalTakedown = view.GetDouble(237);
			instance.NetMoney = view.GetDouble(118);
			instance.MaturityNetMoney = view.GetDouble(890);
			instance.SettlCurrAmt = view.GetDouble(119);
			instance.SettlCurrency = view.GetString(120);
			instance.SettlCurrFxRate = view.GetDouble(155);
			instance.SettlCurrFxRateCalc = view.GetString(156);
			instance.SettlType = view.GetString(63);
			instance.SettlDate = view.GetDateOnly(64);
			if (view.GetView("SettlInstructionsData") is MsgView groupViewSettlInstructionsData)
			{
				instance.SettlInstructionsData = new SettlInstructionsData();
				instance.SettlInstructionsData!.Parse(groupViewSettlInstructionsData);
			}
			if (view.GetView("CommissionData") is MsgView groupViewCommissionData)
			{
				instance.CommissionData = new CommissionData();
				instance.CommissionData!.Parse(groupViewCommissionData);
			}
			instance.SharedCommission = view.GetDouble(858);
			if (view.GetView("Stipulations") is MsgView groupViewStipulations)
			{
				instance.Stipulations = new Stipulations();
				instance.Stipulations!.Parse(groupViewStipulations);
			}
			if (view.GetView("MiscFeesGrp") is MsgView groupViewMiscFeesGrp)
			{
				instance.MiscFeesGrp = new MiscFeesGrp();
				instance.MiscFeesGrp!.Parse(groupViewMiscFeesGrp);
			}
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
