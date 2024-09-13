using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("a", FixVersion.FIX44)]
	public static class QuoteStatusRequestExt
	{
		public static void Parse(this QuoteStatusRequest instance, MsgView? view)
		{
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.QuoteStatusReqID = view?.GetString(649);
			instance.QuoteID = view?.GetString(117);
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.FinancingDetails?.Parse(view?.GetView("FinancingDetails"));
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.InstrmtLegGrp?.Parse(view?.GetView("InstrmtLegGrp"));
			instance.Parties?.Parse(view?.GetView("Parties"));
			instance.Account = view?.GetString(1);
			instance.AcctIDSource = view?.GetInt32(660);
			instance.AccountType = view?.GetInt32(581);
			instance.TradingSessionID = view?.GetString(336);
			instance.TradingSessionSubID = view?.GetString(625);
			instance.SubscriptionRequestType = view?.GetString(263);
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
