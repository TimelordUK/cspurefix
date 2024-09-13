using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AD", FixVersion.FIX44)]
	public static class TradeCaptureReportRequestExt
	{
		public static void Parse(this TradeCaptureReportRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.TradeRequestID = view.GetString(568);
			instance.TradeRequestType = view.GetInt32(569);
			instance.SubscriptionRequestType = view.GetString(263);
			instance.TradeReportID = view.GetString(571);
			instance.SecondaryTradeReportID = view.GetString(818);
			instance.ExecID = view.GetString(17);
			instance.ExecType = view.GetString(150);
			instance.OrderID = view.GetString(37);
			instance.ClOrdID = view.GetString(11);
			instance.MatchStatus = view.GetString(573);
			instance.TrdType = view.GetInt32(828);
			instance.TrdSubType = view.GetInt32(829);
			instance.TransferReason = view.GetString(830);
			instance.SecondaryTrdType = view.GetInt32(855);
			instance.TradeLinkID = view.GetString(820);
			instance.TrdMatchID = view.GetString(880);
			instance.Parties = new Parties();
			instance.Parties?.Parse(view.GetView("Parties"));
			instance.Instrument = new Instrument();
			instance.Instrument?.Parse(view.GetView("Instrument"));
			instance.InstrumentExtension = new InstrumentExtension();
			instance.InstrumentExtension?.Parse(view.GetView("InstrumentExtension"));
			instance.FinancingDetails = new FinancingDetails();
			instance.FinancingDetails?.Parse(view.GetView("FinancingDetails"));
			instance.UndInstrmtGrp = new UndInstrmtGrp();
			instance.UndInstrmtGrp?.Parse(view.GetView("UndInstrmtGrp"));
			instance.InstrmtLegGrp = new InstrmtLegGrp();
			instance.InstrmtLegGrp?.Parse(view.GetView("InstrmtLegGrp"));
			instance.TrdCapDtGrp = new TrdCapDtGrp();
			instance.TrdCapDtGrp?.Parse(view.GetView("TrdCapDtGrp"));
			instance.ClearingBusinessDate = view.GetDateTime(715);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.TimeBracket = view.GetString(943);
			instance.Side = view.GetString(54);
			instance.MultiLegReportingType = view.GetString(442);
			instance.TradeInputSource = view.GetString(578);
			instance.TradeInputDevice = view.GetString(579);
			instance.ResponseTransportType = view.GetInt32(725);
			instance.ResponseDestination = view.GetString(726);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
