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
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.TradeReportID = view?.GetString(571);
			instance.TradeReportTransType = view?.GetInt32(487);
			instance.TradeReportType = view?.GetInt32(856);
			instance.TradeRequestID = view?.GetString(568);
			instance.TrdType = view?.GetInt32(828);
			instance.TrdSubType = view?.GetInt32(829);
			instance.SecondaryTrdType = view?.GetInt32(855);
			instance.TransferReason = view?.GetString(830);
			instance.ExecType = view?.GetString(150);
			instance.TotNumTradeReports = view?.GetInt32(748);
			instance.LastRptRequested = view?.GetBool(912);
			instance.UnsolicitedIndicator = view?.GetBool(325);
			instance.SubscriptionRequestType = view?.GetString(263);
			instance.TradeReportRefID = view?.GetString(572);
			instance.SecondaryTradeReportRefID = view?.GetString(881);
			instance.SecondaryTradeReportID = view?.GetString(818);
			instance.TradeLinkID = view?.GetString(820);
			instance.TrdMatchID = view?.GetString(880);
			instance.ExecID = view?.GetString(17);
			instance.OrdStatus = view?.GetString(39);
			instance.SecondaryExecID = view?.GetString(527);
			instance.ExecRestatementReason = view?.GetInt32(378);
			instance.PreviouslyReported = view?.GetBool(570);
			instance.PriceType = view?.GetInt32(423);
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.FinancingDetails?.Parse(view?.GetView("FinancingDetails"));
			instance.OrderQtyData?.Parse(view?.GetView("OrderQtyData"));
			instance.QtyType = view?.GetInt32(854);
			instance.YieldData?.Parse(view?.GetView("YieldData"));
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.UnderlyingTradingSessionID = view?.GetString(822);
			instance.UnderlyingTradingSessionSubID = view?.GetString(823);
			instance.LastQty = view?.GetDouble(32);
			instance.LastPx = view?.GetDouble(31);
			instance.LastParPx = view?.GetDouble(669);
			instance.LastSpotRate = view?.GetDouble(194);
			instance.LastForwardPoints = view?.GetDouble(195);
			instance.LastMkt = view?.GetString(30);
			instance.TradeDate = view?.GetDateTime(75);
			instance.ClearingBusinessDate = view?.GetDateTime(715);
			instance.AvgPx = view?.GetDouble(6);
			instance.SpreadOrBenchmarkCurveData?.Parse(view?.GetView("SpreadOrBenchmarkCurveData"));
			instance.AvgPxIndicator = view?.GetInt32(819);
			instance.PositionAmountData?.Parse(view?.GetView("PositionAmountData"));
			instance.MultiLegReportingType = view?.GetString(442);
			instance.TradeLegRefID = view?.GetString(824);
			instance.TrdInstrmtLegGrp?.Parse(view?.GetView("TrdInstrmtLegGrp"));
			instance.TransactTime = view?.GetDateTime(60);
			instance.TrdRegTimestamps?.Parse(view?.GetView("TrdRegTimestamps"));
			instance.SettlType = view?.GetString(63);
			instance.SettlDate = view?.GetDateTime(64);
			instance.MatchStatus = view?.GetString(573);
			instance.MatchType = view?.GetString(574);
			instance.TrdCapRptSideGrp?.Parse(view?.GetView("TrdCapRptSideGrp"));
			instance.CopyMsgIndicator = view?.GetBool(797);
			instance.PublishTrdIndicator = view?.GetBool(852);
			instance.ShortSaleReason = view?.GetInt32(853);
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
