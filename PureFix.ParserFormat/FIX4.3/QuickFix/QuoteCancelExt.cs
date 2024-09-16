using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("Z", FixVersion.FIX43)]
	public static class QuoteCancelExt
	{
		public static void Parse(this QuoteCancel instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.QuoteReqID = view.GetString(131);
			instance.QuoteID = view.GetString(117);
			instance.QuoteCancelType = view.GetInt32(298);
			instance.QuoteResponseLevel = view.GetInt32(301);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.Account = view.GetString(1);
			instance.AccountType = view.GetInt32(581);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			var groupViewNoQuoteEntries = view.GetView("NoQuoteEntries");
			if (groupViewNoQuoteEntries is null) return;
			
			var countNoQuoteEntries = groupViewNoQuoteEntries.GroupCount();
			instance.NoQuoteEntries = new QuoteCancelNoQuoteEntries[countNoQuoteEntries];
			for (var i = 0; i < countNoQuoteEntries; ++i)
			{
				instance.NoQuoteEntries[i] = new();
				instance.NoQuoteEntries[i].Parse(groupViewNoQuoteEntries[i]);
			}
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
