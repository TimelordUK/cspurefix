using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AW", FixVersion.FIX44)]
	public static class AssignmentReportExt
	{
		public static void Parse(this AssignmentReport instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.AsgnRptID = view.GetString(833);
			instance.TotNumAssignmentReports = view.GetInt32(832);
			instance.LastRptRequested = view.GetBool(912);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.Account = view.GetString(1);
			instance.AccountType = view.GetInt32(581);
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
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
			if (view.GetView("PositionQty") is MsgView groupViewPositionQty)
			{
				instance.PositionQty = new PositionQty();
				instance.PositionQty!.Parse(groupViewPositionQty);
			}
			if (view.GetView("PositionAmountData") is MsgView groupViewPositionAmountData)
			{
				instance.PositionAmountData = new PositionAmountData();
				instance.PositionAmountData!.Parse(groupViewPositionAmountData);
			}
			instance.ThresholdAmount = view.GetDouble(834);
			instance.SettlPrice = view.GetDouble(730);
			instance.SettlPriceType = view.GetInt32(731);
			instance.UnderlyingSettlPrice = view.GetDouble(732);
			instance.ExpireDate = view.GetDateTime(432);
			instance.AssignmentMethod = view.GetString(744);
			instance.AssignmentUnit = view.GetDouble(745);
			instance.OpenInterest = view.GetDouble(746);
			instance.ExerciseMethod = view.GetString(747);
			instance.SettlSessID = view.GetString(716);
			instance.SettlSessSubID = view.GetString(717);
			instance.ClearingBusinessDate = view.GetDateTime(715);
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
