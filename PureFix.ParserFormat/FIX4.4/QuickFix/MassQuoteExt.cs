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
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.QuoteReqID = view.GetString(131);
			instance.QuoteID = view.GetString(117);
			instance.QuoteType = view.GetInt32(537);
			instance.QuoteResponseLevel = view.GetInt32(301);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.Account = view.GetString(1);
			instance.AcctIDSource = view.GetInt32(660);
			instance.AccountType = view.GetInt32(581);
			instance.DefBidSize = view.GetDouble(293);
			instance.DefOfferSize = view.GetDouble(294);
			if (view.GetView("QuotSetGrp") is MsgView groupViewQuotSetGrp)
			{
				instance.QuotSetGrp = new QuotSetGrp();
				instance.QuotSetGrp!.Parse(groupViewQuotSetGrp);
			}
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
