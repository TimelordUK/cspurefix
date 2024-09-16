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
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
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
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
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
			if (view.GetView("TrdCapDtGrp") is MsgView groupViewTrdCapDtGrp)
			{
				instance.TrdCapDtGrp = new TrdCapDtGrp();
				instance.TrdCapDtGrp!.Parse(groupViewTrdCapDtGrp);
			}
			instance.ClearingBusinessDate = view.GetDateOnly(715);
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
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
