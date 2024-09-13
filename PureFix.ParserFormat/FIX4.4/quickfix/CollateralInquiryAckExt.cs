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
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.CollInquiryID = view.GetString(909);
			instance.CollInquiryStatus = view.GetInt32(945);
			instance.CollInquiryResult = view.GetInt32(946);
			if (view.GetView("CollInqQualGrp") is MsgView groupViewCollInqQualGrp)
			{
				instance.CollInqQualGrp = new CollInqQualGrp();
				instance.CollInqQualGrp!.Parse(groupViewCollInqQualGrp);
			}
			instance.TotNumReports = view.GetInt32(911);
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
			if (view.GetView("UndInstrmtGrp") is MsgView groupViewUndInstrmtGrp)
			{
				instance.UndInstrmtGrp = new UndInstrmtGrp();
				instance.UndInstrmtGrp!.Parse(groupViewUndInstrmtGrp);
			}
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
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
