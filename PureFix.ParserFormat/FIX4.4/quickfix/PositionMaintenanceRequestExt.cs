using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AL", FixVersion.FIX44)]
	public static class PositionMaintenanceRequestExt
	{
		public static void Parse(this PositionMaintenanceRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.PosReqID = view.GetString(710);
			instance.PosTransType = view.GetInt32(709);
			instance.PosMaintAction = view.GetInt32(712);
			instance.OrigPosReqRefID = view.GetString(713);
			instance.PosMaintRptRefID = view.GetString(714);
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
			if (view.GetView("InstrmtLegGrp") is MsgView groupViewInstrmtLegGrp)
			{
				instance.InstrmtLegGrp = new InstrmtLegGrp();
				instance.InstrmtLegGrp!.Parse(groupViewInstrmtLegGrp);
			}
			instance.InstrmtLegGrp = new InstrmtLegGrp();
			instance.InstrmtLegGrp?.Parse(view.GetView("InstrmtLegGrp"));
			if (view.GetView("UndInstrmtGrp") is MsgView groupViewUndInstrmtGrp)
			{
				instance.UndInstrmtGrp = new UndInstrmtGrp();
				instance.UndInstrmtGrp!.Parse(groupViewUndInstrmtGrp);
			}
			instance.UndInstrmtGrp = new UndInstrmtGrp();
			instance.UndInstrmtGrp?.Parse(view.GetView("UndInstrmtGrp"));
			if (view.GetView("TrdgSesGrp") is MsgView groupViewTrdgSesGrp)
			{
				instance.TrdgSesGrp = new TrdgSesGrp();
				instance.TrdgSesGrp!.Parse(groupViewTrdgSesGrp);
			}
			instance.TrdgSesGrp = new TrdgSesGrp();
			instance.TrdgSesGrp?.Parse(view.GetView("TrdgSesGrp"));
			instance.TransactTime = view.GetDateTime(60);
			if (view.GetView("PositionQty") is MsgView groupViewPositionQty)
			{
				instance.PositionQty = new PositionQty();
				instance.PositionQty!.Parse(groupViewPositionQty);
			}
			instance.PositionQty = new PositionQty();
			instance.PositionQty?.Parse(view.GetView("PositionQty"));
			instance.AdjustmentType = view.GetInt32(718);
			instance.ContraryInstructionIndicator = view.GetBool(719);
			instance.PriorSpreadIndicator = view.GetBool(720);
			instance.ThresholdAmount = view.GetDouble(834);
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
