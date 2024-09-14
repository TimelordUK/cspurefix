using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AE", FixVersion.FIX44)]
	public static class TradeCaptureReportExt
	{
		public static void Parse(this TradeCaptureReport instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.TradeReportID = view.GetString(571);
			instance.TradeReportTransType = view.GetInt32(487);
			instance.TradeReportType = view.GetInt32(856);
			instance.TradeRequestID = view.GetString(568);
			instance.TrdType = view.GetInt32(828);
			instance.TrdSubType = view.GetInt32(829);
			instance.SecondaryTrdType = view.GetInt32(855);
			instance.TransferReason = view.GetString(830);
			instance.ExecType = view.GetString(150);
			instance.TotNumTradeReports = view.GetInt32(748);
			instance.LastRptRequested = view.GetBool(912);
			instance.UnsolicitedIndicator = view.GetBool(325);
			instance.SubscriptionRequestType = view.GetString(263);
			instance.TradeReportRefID = view.GetString(572);
			instance.SecondaryTradeReportRefID = view.GetString(881);
			instance.SecondaryTradeReportID = view.GetString(818);
			instance.TradeLinkID = view.GetString(820);
			instance.TrdMatchID = view.GetString(880);
			instance.ExecID = view.GetString(17);
			instance.OrdStatus = view.GetString(39);
			instance.SecondaryExecID = view.GetString(527);
			instance.ExecRestatementReason = view.GetInt32(378);
			instance.PreviouslyReported = view.GetBool(570);
			instance.PriceType = view.GetInt32(423);
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
			if (view.GetView("OrderQtyData") is MsgView groupViewOrderQtyData)
			{
				instance.OrderQtyData = new OrderQtyData();
				instance.OrderQtyData!.Parse(groupViewOrderQtyData);
			}
			instance.QtyType = view.GetInt32(854);
			if (view.GetView("YieldData") is MsgView groupViewYieldData)
			{
				instance.YieldData = new YieldData();
				instance.YieldData!.Parse(groupViewYieldData);
			}
			if (view.GetView("UndInstrmtGrp") is MsgView groupViewUndInstrmtGrp)
			{
				instance.UndInstrmtGrp = new UndInstrmtGrp();
				instance.UndInstrmtGrp!.Parse(groupViewUndInstrmtGrp);
			}
			instance.UnderlyingTradingSessionID = view.GetString(822);
			instance.UnderlyingTradingSessionSubID = view.GetString(823);
			instance.LastQty = view.GetDouble(32);
			instance.LastPx = view.GetDouble(31);
			instance.LastParPx = view.GetDouble(669);
			instance.LastSpotRate = view.GetDouble(194);
			instance.LastForwardPoints = view.GetDouble(195);
			instance.LastMkt = view.GetString(30);
			instance.TradeDate = view.GetDateOnly(75);
			instance.ClearingBusinessDate = view.GetDateOnly(715);
			instance.AvgPx = view.GetDouble(6);
			if (view.GetView("SpreadOrBenchmarkCurveData") is MsgView groupViewSpreadOrBenchmarkCurveData)
			{
				instance.SpreadOrBenchmarkCurveData = new SpreadOrBenchmarkCurveData();
				instance.SpreadOrBenchmarkCurveData!.Parse(groupViewSpreadOrBenchmarkCurveData);
			}
			instance.AvgPxIndicator = view.GetInt32(819);
			if (view.GetView("PositionAmountData") is MsgView groupViewPositionAmountData)
			{
				instance.PositionAmountData = new PositionAmountData();
				instance.PositionAmountData!.Parse(groupViewPositionAmountData);
			}
			instance.MultiLegReportingType = view.GetString(442);
			instance.TradeLegRefID = view.GetString(824);
			if (view.GetView("TrdInstrmtLegGrp") is MsgView groupViewTrdInstrmtLegGrp)
			{
				instance.TrdInstrmtLegGrp = new TrdInstrmtLegGrp();
				instance.TrdInstrmtLegGrp!.Parse(groupViewTrdInstrmtLegGrp);
			}
			instance.TransactTime = view.GetDateTime(60);
			if (view.GetView("TrdRegTimestamps") is MsgView groupViewTrdRegTimestamps)
			{
				instance.TrdRegTimestamps = new TrdRegTimestamps();
				instance.TrdRegTimestamps!.Parse(groupViewTrdRegTimestamps);
			}
			instance.SettlType = view.GetString(63);
			instance.SettlDate = view.GetDateOnly(64);
			instance.MatchStatus = view.GetString(573);
			instance.MatchType = view.GetString(574);
			if (view.GetView("TrdCapRptSideGrp") is MsgView groupViewTrdCapRptSideGrp)
			{
				instance.TrdCapRptSideGrp = new TrdCapRptSideGrp();
				instance.TrdCapRptSideGrp!.Parse(groupViewTrdCapRptSideGrp);
			}
			instance.CopyMsgIndicator = view.GetBool(797);
			instance.PublishTrdIndicator = view.GetBool(852);
			instance.ShortSaleReason = view.GetInt32(853);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
