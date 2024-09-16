using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("i", FixVersion.FIX42)]
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
			instance.QuoteResponseLevel = view.GetInt32(301);
			instance.DefBidSize = view.GetDouble(293);
			instance.DefOfferSize = view.GetDouble(294);
			var groupViewNoQuoteSets = view.GetView("NoQuoteSets");
			if (groupViewNoQuoteSets is null) return;
			
			var countNoQuoteSets = groupViewNoQuoteSets.GroupCount();
			instance.NoQuoteSets = new MassQuoteNoQuoteSets[countNoQuoteSets];
			for (var i = 0; i < countNoQuoteSets; ++i)
			{
				instance.NoQuoteSets[i] = new();
				instance.NoQuoteSets[i].Parse(groupViewNoQuoteSets[i]);
			}
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
