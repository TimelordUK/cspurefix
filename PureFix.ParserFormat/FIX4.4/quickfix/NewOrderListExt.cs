using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("E", FixVersion.FIX44)]
	public static class NewOrderListExt
	{
		public static void Parse(this NewOrderList instance, MsgView? view)
		{
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.ListID = view?.GetString(66);
			instance.BidID = view?.GetString(390);
			instance.ClientBidID = view?.GetString(391);
			instance.ProgRptReqs = view?.GetInt32(414);
			instance.BidType = view?.GetInt32(394);
			instance.ProgPeriodInterval = view?.GetInt32(415);
			instance.CancellationRights = view?.GetString(480);
			instance.MoneyLaunderingStatus = view?.GetString(481);
			instance.RegistID = view?.GetString(513);
			instance.ListExecInstType = view?.GetString(433);
			instance.ListExecInst = view?.GetString(69);
			instance.EncodedListExecInstLen = view?.GetInt32(352);
			instance.EncodedListExecInst = view?.GetByteArray(353);
			instance.AllowableOneSidednessPct = view?.GetDouble(765);
			instance.AllowableOneSidednessValue = view?.GetDouble(766);
			instance.AllowableOneSidednessCurr = view?.GetString(767);
			instance.TotNoOrders = view?.GetInt32(68);
			instance.LastFragment = view?.GetBool(893);
			instance.ListOrdGrp?.Parse(view?.GetView("ListOrdGrp"));
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
