using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AS", FixVersion.FIX44)]
	public static class AllocationReportExt
	{
		public static void Parse(this AllocationReport instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.AllocReportID = view.GetString(755);
			instance.AllocID = view.GetString(70);
			instance.AllocTransType = view.GetString(71);
			instance.AllocReportRefID = view.GetString(795);
			instance.AllocCancReplaceReason = view.GetInt32(796);
			instance.SecondaryAllocID = view.GetString(793);
			instance.AllocReportType = view.GetInt32(794);
			instance.AllocStatus = view.GetInt32(87);
			instance.AllocRejCode = view.GetInt32(88);
			instance.RefAllocID = view.GetString(72);
			instance.AllocIntermedReqType = view.GetInt32(808);
			instance.AllocLinkID = view.GetString(196);
			instance.AllocLinkType = view.GetInt32(197);
			instance.BookingRefID = view.GetString(466);
			instance.AllocNoOrdersType = view.GetInt32(857);
			if (view.GetView("OrdAllocGrp") is MsgView groupViewOrdAllocGrp)
			{
				instance.OrdAllocGrp = new OrdAllocGrp();
				instance.OrdAllocGrp!.Parse(groupViewOrdAllocGrp);
			}
			if (view.GetView("ExecAllocGrp") is MsgView groupViewExecAllocGrp)
			{
				instance.ExecAllocGrp = new ExecAllocGrp();
				instance.ExecAllocGrp!.Parse(groupViewExecAllocGrp);
			}
			instance.PreviouslyReported = view.GetBool(570);
			instance.ReversalIndicator = view.GetBool(700);
			instance.MatchType = view.GetString(574);
			instance.Side = view.GetString(54);
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
			instance.Quantity = view.GetDouble(53);
			instance.QtyType = view.GetInt32(854);
			instance.LastMkt = view.GetString(30);
			instance.TradeOriginationDate = view.GetDateTime(229);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.PriceType = view.GetInt32(423);
			instance.AvgPx = view.GetDouble(6);
			instance.AvgParPx = view.GetDouble(860);
			if (view.GetView("SpreadOrBenchmarkCurveData") is MsgView groupViewSpreadOrBenchmarkCurveData)
			{
				instance.SpreadOrBenchmarkCurveData = new SpreadOrBenchmarkCurveData();
				instance.SpreadOrBenchmarkCurveData!.Parse(groupViewSpreadOrBenchmarkCurveData);
			}
			instance.Currency = view.GetString(15);
			instance.AvgPxPrecision = view.GetInt32(74);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.TradeDate = view.GetDateTime(75);
			instance.TransactTime = view.GetDateTime(60);
			instance.SettlType = view.GetString(63);
			instance.SettlDate = view.GetDateTime(64);
			instance.BookingType = view.GetInt32(775);
			instance.GrossTradeAmt = view.GetDouble(381);
			instance.Concession = view.GetDouble(238);
			instance.TotalTakedown = view.GetDouble(237);
			instance.NetMoney = view.GetDouble(118);
			instance.PositionEffect = view.GetString(77);
			instance.AutoAcceptIndicator = view.GetBool(754);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.NumDaysInterest = view.GetInt32(157);
			instance.AccruedInterestRate = view.GetDouble(158);
			instance.AccruedInterestAmt = view.GetDouble(159);
			instance.TotalAccruedInterestAmt = view.GetDouble(540);
			instance.InterestAtMaturity = view.GetDouble(738);
			instance.EndAccruedInterestAmt = view.GetDouble(920);
			instance.StartCash = view.GetDouble(921);
			instance.EndCash = view.GetDouble(922);
			instance.LegalConfirm = view.GetBool(650);
			if (view.GetView("Stipulations") is MsgView groupViewStipulations)
			{
				instance.Stipulations = new Stipulations();
				instance.Stipulations!.Parse(groupViewStipulations);
			}
			if (view.GetView("YieldData") is MsgView groupViewYieldData)
			{
				instance.YieldData = new YieldData();
				instance.YieldData!.Parse(groupViewYieldData);
			}
			instance.TotNoAllocs = view.GetInt32(892);
			instance.LastFragment = view.GetBool(893);
			if (view.GetView("AllocGrp") is MsgView groupViewAllocGrp)
			{
				instance.AllocGrp = new AllocGrp();
				instance.AllocGrp!.Parse(groupViewAllocGrp);
			}
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
