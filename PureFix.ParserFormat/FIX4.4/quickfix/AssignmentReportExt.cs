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
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.AsgnRptID = view?.GetString(833);
			instance.TotNumAssignmentReports = view?.GetInt32(832);
			instance.LastRptRequested = view?.GetBool(912);
			instance.Parties?.Parse(view?.GetView("Parties"));
			instance.Account = view?.GetString(1);
			instance.AccountType = view?.GetInt32(581);
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.Currency = view?.GetString(15);
			instance.InstrmtLegGrp?.Parse(view?.GetView("InstrmtLegGrp"));
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.PositionQty?.Parse(view?.GetView("PositionQty"));
			instance.PositionAmountData?.Parse(view?.GetView("PositionAmountData"));
			instance.ThresholdAmount = view?.GetDouble(834);
			instance.SettlPrice = view?.GetDouble(730);
			instance.SettlPriceType = view?.GetInt32(731);
			instance.UnderlyingSettlPrice = view?.GetDouble(732);
			instance.ExpireDate = view?.GetDateTime(432);
			instance.AssignmentMethod = view?.GetString(744);
			instance.AssignmentUnit = view?.GetDouble(745);
			instance.OpenInterest = view?.GetDouble(746);
			instance.ExerciseMethod = view?.GetString(747);
			instance.SettlSessID = view?.GetString(716);
			instance.SettlSessSubID = view?.GetString(717);
			instance.ClearingBusinessDate = view?.GetDateTime(715);
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
