using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("b", FixVersion.FIX43)]
	public static class MassQuoteAcknowledgementExt
	{
		public static void Parse(this MassQuoteAcknowledgement instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.QuoteReqID = view.GetString(131);
			instance.QuoteID = view.GetString(117);
			instance.QuoteStatus = view.GetInt32(297);
			instance.QuoteRejectReason = view.GetInt32(300);
			instance.QuoteResponseLevel = view.GetInt32(301);
			instance.QuoteType = view.GetInt32(537);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.Account = view.GetString(1);
			instance.AccountType = view.GetInt32(581);
			instance.Text = view.GetString(58);
			var groupViewNoQuoteSets = view.GetView("NoQuoteSets");
			if (groupViewNoQuoteSets is null) return;
			
			var countNoQuoteSets = groupViewNoQuoteSets.GroupCount();
			instance.NoQuoteSets = new MassQuoteAcknowledgementNoQuoteSets[countNoQuoteSets];
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
