using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AQ", FixVersion.FIX44)]
	public static class TradeCaptureReportRequestAckExt
	{
		public static void Parse(this TradeCaptureReportRequestAck instance, MsgView? view)
		{
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.TradeRequestID = view?.GetString(568);
			instance.TradeRequestType = view?.GetInt32(569);
			instance.SubscriptionRequestType = view?.GetString(263);
			instance.TotNumTradeReports = view?.GetInt32(748);
			instance.TradeRequestResult = view?.GetInt32(749);
			instance.TradeRequestStatus = view?.GetInt32(750);
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.InstrmtLegGrp?.Parse(view?.GetView("InstrmtLegGrp"));
			instance.MultiLegReportingType = view?.GetString(442);
			instance.ResponseTransportType = view?.GetInt32(725);
			instance.ResponseDestination = view?.GetString(726);
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
