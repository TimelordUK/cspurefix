using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BG", FixVersion.FIX44)]
	public static class CollateralInquiryAckExt
	{
		public static void Parse(this CollateralInquiryAck instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.CollInquiryID = view.GetString(909);
			instance.CollInquiryStatus = view.GetInt32(945);
			instance.CollInquiryResult = view.GetInt32(946);
			instance.CollInqQualGrp?.Parse(view.GetView("CollInqQualGrp"));
			instance.TotNumReports = view.GetInt32(911);
			instance.Parties?.Parse(view.GetView("Parties"));
			instance.Account = view.GetString(1);
			instance.AccountType = view.GetInt32(581);
			instance.ClOrdID = view.GetString(11);
			instance.OrderID = view.GetString(37);
			instance.SecondaryOrderID = view.GetString(198);
			instance.SecondaryClOrdID = view.GetString(526);
			instance.ExecCollGrp?.Parse(view.GetView("ExecCollGrp"));
			instance.TrdCollGrp?.Parse(view.GetView("TrdCollGrp"));
			instance.Instrument?.Parse(view.GetView("Instrument"));
			instance.FinancingDetails?.Parse(view.GetView("FinancingDetails"));
			instance.SettlDate = view.GetDateTime(64);
			instance.Quantity = view.GetDouble(53);
			instance.QtyType = view.GetInt32(854);
			instance.Currency = view.GetString(15);
			instance.InstrmtLegGrp?.Parse(view.GetView("InstrmtLegGrp"));
			instance.UndInstrmtGrp?.Parse(view.GetView("UndInstrmtGrp"));
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.SettlSessID = view.GetString(716);
			instance.SettlSessSubID = view.GetString(717);
			instance.ClearingBusinessDate = view.GetDateTime(715);
			instance.ResponseTransportType = view.GetInt32(725);
			instance.ResponseDestination = view.GetString(726);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
