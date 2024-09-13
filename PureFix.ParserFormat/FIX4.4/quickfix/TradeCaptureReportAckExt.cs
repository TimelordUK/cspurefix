using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AR", FixVersion.FIX44)]
	public static class TradeCaptureReportAckExt
	{
		public static void Parse(this TradeCaptureReportAck instance, MsgView? view)
		{
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.TradeReportID = view?.GetString(571);
			instance.TradeReportTransType = view?.GetInt32(487);
			instance.TradeReportType = view?.GetInt32(856);
			instance.TrdType = view?.GetInt32(828);
			instance.TrdSubType = view?.GetInt32(829);
			instance.SecondaryTrdType = view?.GetInt32(855);
			instance.TransferReason = view?.GetString(830);
			instance.ExecType = view?.GetString(150);
			instance.TradeReportRefID = view?.GetString(572);
			instance.SecondaryTradeReportRefID = view?.GetString(881);
			instance.TrdRptStatus = view?.GetInt32(939);
			instance.TradeReportRejectReason = view?.GetInt32(751);
			instance.SecondaryTradeReportID = view?.GetString(818);
			instance.SubscriptionRequestType = view?.GetString(263);
			instance.TradeLinkID = view?.GetString(820);
			instance.TrdMatchID = view?.GetString(880);
			instance.ExecID = view?.GetString(17);
			instance.SecondaryExecID = view?.GetString(527);
			instance.Instrument = new Instrument();
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.TransactTime = view?.GetDateTime(60);
			instance.TrdRegTimestamps = new TrdRegTimestamps();
			instance.TrdRegTimestamps?.Parse(view?.GetView("TrdRegTimestamps"));
			instance.ResponseTransportType = view?.GetInt32(725);
			instance.ResponseDestination = view?.GetString(726);
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.TrdInstrmtLegGrp = new TrdInstrmtLegGrp();
			instance.TrdInstrmtLegGrp?.Parse(view?.GetView("TrdInstrmtLegGrp"));
			instance.ClearingFeeIndicator = view?.GetString(635);
			instance.OrderCapacity = view?.GetString(528);
			instance.OrderRestrictions = view?.GetString(529);
			instance.CustOrderCapacity = view?.GetInt32(582);
			instance.Account = view?.GetString(1);
			instance.AcctIDSource = view?.GetInt32(660);
			instance.AccountType = view?.GetInt32(581);
			instance.PositionEffect = view?.GetString(77);
			instance.PreallocMethod = view?.GetString(591);
			instance.TrdAllocGrp = new TrdAllocGrp();
			instance.TrdAllocGrp?.Parse(view?.GetView("TrdAllocGrp"));
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
