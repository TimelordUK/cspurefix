using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AY", FixVersion.FIX44)]
	public static class CollateralAssignmentExt
	{
		public static void Parse(this CollateralAssignment instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.CollAsgnID = view.GetString(902);
			instance.CollReqID = view.GetString(894);
			instance.CollAsgnReason = view.GetInt32(895);
			instance.CollAsgnTransType = view.GetInt32(903);
			instance.CollAsgnRefID = view.GetString(907);
			instance.TransactTime = view.GetDateTime(60);
			instance.ExpireTime = view.GetDateTime(126);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.Account = view.GetString(1);
			instance.AccountType = view.GetInt32(581);
			instance.ClOrdID = view.GetString(11);
			instance.OrderID = view.GetString(37);
			instance.SecondaryOrderID = view.GetString(198);
			instance.SecondaryClOrdID = view.GetString(526);
			if (view.GetView("ExecCollGrp") is MsgView groupViewExecCollGrp)
			{
				instance.ExecCollGrp = new ExecCollGrp();
				instance.ExecCollGrp!.Parse(groupViewExecCollGrp);
			}
			if (view.GetView("TrdCollGrp") is MsgView groupViewTrdCollGrp)
			{
				instance.TrdCollGrp = new TrdCollGrp();
				instance.TrdCollGrp!.Parse(groupViewTrdCollGrp);
			}
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			if (view.GetView("FinancingDetails") is MsgView groupViewFinancingDetails)
			{
				instance.FinancingDetails = new FinancingDetails();
				instance.FinancingDetails!.Parse(groupViewFinancingDetails);
			}
			instance.SettlDate = view.GetDateTime(64);
			instance.Quantity = view.GetDouble(53);
			instance.QtyType = view.GetInt32(854);
			instance.Currency = view.GetString(15);
			if (view.GetView("InstrmtLegGrp") is MsgView groupViewInstrmtLegGrp)
			{
				instance.InstrmtLegGrp = new InstrmtLegGrp();
				instance.InstrmtLegGrp!.Parse(groupViewInstrmtLegGrp);
			}
			if (view.GetView("UndInstrmtCollGrp") is MsgView groupViewUndInstrmtCollGrp)
			{
				instance.UndInstrmtCollGrp = new UndInstrmtCollGrp();
				instance.UndInstrmtCollGrp!.Parse(groupViewUndInstrmtCollGrp);
			}
			instance.MarginExcess = view.GetDouble(899);
			instance.TotalNetValue = view.GetDouble(900);
			instance.CashOutstanding = view.GetDouble(901);
			if (view.GetView("TrdRegTimestamps") is MsgView groupViewTrdRegTimestamps)
			{
				instance.TrdRegTimestamps = new TrdRegTimestamps();
				instance.TrdRegTimestamps!.Parse(groupViewTrdRegTimestamps);
			}
			instance.Side = view.GetString(54);
			if (view.GetView("MiscFeesGrp") is MsgView groupViewMiscFeesGrp)
			{
				instance.MiscFeesGrp = new MiscFeesGrp();
				instance.MiscFeesGrp!.Parse(groupViewMiscFeesGrp);
			}
			instance.Price = view.GetDouble(44);
			instance.PriceType = view.GetInt32(423);
			instance.AccruedInterestAmt = view.GetDouble(159);
			instance.EndAccruedInterestAmt = view.GetDouble(920);
			instance.StartCash = view.GetDouble(921);
			instance.EndCash = view.GetDouble(922);
			if (view.GetView("SpreadOrBenchmarkCurveData") is MsgView groupViewSpreadOrBenchmarkCurveData)
			{
				instance.SpreadOrBenchmarkCurveData = new SpreadOrBenchmarkCurveData();
				instance.SpreadOrBenchmarkCurveData!.Parse(groupViewSpreadOrBenchmarkCurveData);
			}
			if (view.GetView("Stipulations") is MsgView groupViewStipulations)
			{
				instance.Stipulations = new Stipulations();
				instance.Stipulations!.Parse(groupViewStipulations);
			}
			if (view.GetView("SettlInstructionsData") is MsgView groupViewSettlInstructionsData)
			{
				instance.SettlInstructionsData = new SettlInstructionsData();
				instance.SettlInstructionsData!.Parse(groupViewSettlInstructionsData);
			}
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.SettlSessID = view.GetString(716);
			instance.SettlSessSubID = view.GetString(717);
			instance.ClearingBusinessDate = view.GetDateTime(715);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
