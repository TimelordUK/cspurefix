using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AM", FixVersion.FIX44)]
	public static class PositionMaintenanceReportExt
	{
		public static void Parse(this PositionMaintenanceReport instance, MsgView? view)
		{
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.PosMaintRptID = view?.GetString(721);
			instance.PosTransType = view?.GetInt32(709);
			instance.PosReqID = view?.GetString(710);
			instance.PosMaintAction = view?.GetInt32(712);
			instance.OrigPosReqRefID = view?.GetString(713);
			instance.PosMaintStatus = view?.GetInt32(722);
			instance.PosMaintResult = view?.GetInt32(723);
			instance.ClearingBusinessDate = view?.GetDateTime(715);
			instance.SettlSessID = view?.GetString(716);
			instance.SettlSessSubID = view?.GetString(717);
			instance.Parties?.Parse(view?.GetView("Parties"));
			instance.Account = view?.GetString(1);
			instance.AcctIDSource = view?.GetInt32(660);
			instance.AccountType = view?.GetInt32(581);
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.Currency = view?.GetString(15);
			instance.InstrmtLegGrp?.Parse(view?.GetView("InstrmtLegGrp"));
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.TrdgSesGrp?.Parse(view?.GetView("TrdgSesGrp"));
			instance.TransactTime = view?.GetDateTime(60);
			instance.PositionQty?.Parse(view?.GetView("PositionQty"));
			instance.PositionAmountData?.Parse(view?.GetView("PositionAmountData"));
			instance.AdjustmentType = view?.GetInt32(718);
			instance.ThresholdAmount = view?.GetDouble(834);
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
