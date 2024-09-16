using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("b", FixVersion.FIX42)]
	public static class QuoteAcknowledgementExt
	{
		public static void Parse(this QuoteAcknowledgement instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.QuoteReqID = view.GetString(131);
			instance.QuoteID = view.GetString(117);
			instance.QuoteAckStatus = view.GetInt32(297);
			instance.QuoteRejectReason = view.GetInt32(300);
			instance.QuoteResponseLevel = view.GetInt32(301);
			instance.TradingSessionID = view.GetString(336);
			instance.Text = view.GetString(58);
			var groupViewNoQuoteSets = view.GetView("NoQuoteSets");
			if (groupViewNoQuoteSets is null) return;
			
			var countNoQuoteSets = groupViewNoQuoteSets.GroupCount();
			instance.NoQuoteSets = new QuoteAcknowledgementNoQuoteSets[countNoQuoteSets];
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
