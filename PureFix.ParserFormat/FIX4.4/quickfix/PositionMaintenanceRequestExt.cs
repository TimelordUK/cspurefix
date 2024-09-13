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
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.PosReqID = view?.GetString(710);
			instance.PosTransType = view?.GetInt32(709);
			instance.PosMaintAction = view?.GetInt32(712);
			instance.OrigPosReqRefID = view?.GetString(713);
			instance.PosMaintRptRefID = view?.GetString(714);
			instance.ClearingBusinessDate = view?.GetDateTime(715);
			instance.SettlSessID = view?.GetString(716);
			instance.SettlSessSubID = view?.GetString(717);
			instance.Parties = new Parties();
			instance.Parties?.Parse(view?.GetView("Parties"));
			instance.Account = view?.GetString(1);
			instance.AcctIDSource = view?.GetInt32(660);
			instance.AccountType = view?.GetInt32(581);
			instance.Instrument = new Instrument();
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.Currency = view?.GetString(15);
			instance.InstrmtLegGrp = new InstrmtLegGrp();
			instance.InstrmtLegGrp?.Parse(view?.GetView("InstrmtLegGrp"));
			instance.UndInstrmtGrp = new UndInstrmtGrp();
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.TrdgSesGrp = new TrdgSesGrp();
			instance.TrdgSesGrp?.Parse(view?.GetView("TrdgSesGrp"));
			instance.TransactTime = view?.GetDateTime(60);
			instance.PositionQty = new PositionQty();
			instance.PositionQty?.Parse(view?.GetView("PositionQty"));
			instance.AdjustmentType = view?.GetInt32(718);
			instance.ContraryInstructionIndicator = view?.GetBool(719);
			instance.PriorSpreadIndicator = view?.GetBool(720);
			instance.ThresholdAmount = view?.GetDouble(834);
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
