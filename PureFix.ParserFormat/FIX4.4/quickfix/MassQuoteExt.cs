using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("i", FixVersion.FIX44)]
	public static class MassQuoteExt
	{
		public static void Parse(this MassQuote instance, MsgView? view)
		{
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.QuoteReqID = view?.GetString(131);
			instance.QuoteID = view?.GetString(117);
			instance.QuoteType = view?.GetInt32(537);
			instance.QuoteResponseLevel = view?.GetInt32(301);
			instance.Parties = new Parties();
			instance.Parties?.Parse(view?.GetView("Parties"));
			instance.Account = view?.GetString(1);
			instance.AcctIDSource = view?.GetInt32(660);
			instance.AccountType = view?.GetInt32(581);
			instance.DefBidSize = view?.GetDouble(293);
			instance.DefOfferSize = view?.GetDouble(294);
			instance.QuotSetGrp = new QuotSetGrp();
			instance.QuotSetGrp?.Parse(view?.GetView("QuotSetGrp"));
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
