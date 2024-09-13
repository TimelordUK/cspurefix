using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AP", FixVersion.FIX44)]
	public static class PositionReportExt
	{
		public static void Parse(this PositionReport instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.PosMaintRptID = view.GetString(721);
			instance.PosReqID = view.GetString(710);
			instance.PosReqType = view.GetInt32(724);
			instance.SubscriptionRequestType = view.GetString(263);
			instance.TotalNumPosReports = view.GetInt32(727);
			instance.UnsolicitedIndicator = view.GetBool(325);
			instance.PosReqResult = view.GetInt32(728);
			instance.ClearingBusinessDate = view.GetDateTime(715);
			instance.SettlSessID = view.GetString(716);
			instance.SettlSessSubID = view.GetString(717);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.Parties = new Parties();
			instance.Parties?.Parse(view.GetView("Parties"));
			instance.Account = view.GetString(1);
			instance.AcctIDSource = view.GetInt32(660);
			instance.AccountType = view.GetInt32(581);
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			instance.Instrument = new Instrument();
			instance.Instrument?.Parse(view.GetView("Instrument"));
			instance.Currency = view.GetString(15);
			instance.SettlPrice = view.GetDouble(730);
			instance.SettlPriceType = view.GetInt32(731);
			instance.PriorSettlPrice = view.GetDouble(734);
			if (view.GetView("InstrmtLegGrp") is MsgView groupViewInstrmtLegGrp)
			{
				instance.InstrmtLegGrp = new InstrmtLegGrp();
				instance.InstrmtLegGrp!.Parse(groupViewInstrmtLegGrp);
			}
			instance.InstrmtLegGrp = new InstrmtLegGrp();
			instance.InstrmtLegGrp?.Parse(view.GetView("InstrmtLegGrp"));
			if (view.GetView("PosUndInstrmtGrp") is MsgView groupViewPosUndInstrmtGrp)
			{
				instance.PosUndInstrmtGrp = new PosUndInstrmtGrp();
				instance.PosUndInstrmtGrp!.Parse(groupViewPosUndInstrmtGrp);
			}
			instance.PosUndInstrmtGrp = new PosUndInstrmtGrp();
			instance.PosUndInstrmtGrp?.Parse(view.GetView("PosUndInstrmtGrp"));
			if (view.GetView("PositionQty") is MsgView groupViewPositionQty)
			{
				instance.PositionQty = new PositionQty();
				instance.PositionQty!.Parse(groupViewPositionQty);
			}
			instance.PositionQty = new PositionQty();
			instance.PositionQty?.Parse(view.GetView("PositionQty"));
			if (view.GetView("PositionAmountData") is MsgView groupViewPositionAmountData)
			{
				instance.PositionAmountData = new PositionAmountData();
				instance.PositionAmountData!.Parse(groupViewPositionAmountData);
			}
			instance.PositionAmountData = new PositionAmountData();
			instance.PositionAmountData?.Parse(view.GetView("PositionAmountData"));
			instance.RegistStatus = view.GetString(506);
			instance.DeliveryDate = view.GetDateTime(743);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
