using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("Z", FixVersion.FIX44)]
	public static class QuoteCancelExt
	{
		public static void Parse(this QuoteCancel instance, MsgView? view)
		{
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.QuoteReqID = view?.GetString(131);
			instance.QuoteID = view?.GetString(117);
			instance.QuoteCancelType = view?.GetInt32(298);
			instance.QuoteResponseLevel = view?.GetInt32(301);
			instance.Parties?.Parse(view?.GetView("Parties"));
			instance.Account = view?.GetString(1);
			instance.AcctIDSource = view?.GetInt32(660);
			instance.AccountType = view?.GetInt32(581);
			instance.TradingSessionID = view?.GetString(336);
			instance.TradingSessionSubID = view?.GetString(625);
			instance.QuotCxlEntriesGrp?.Parse(view?.GetView("QuotCxlEntriesGrp"));
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
