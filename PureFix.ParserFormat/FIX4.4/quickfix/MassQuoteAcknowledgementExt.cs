using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("b", FixVersion.FIX44)]
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
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
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
			instance.Parties = new Parties();
			instance.Parties?.Parse(view.GetView("Parties"));
			instance.Account = view.GetString(1);
			instance.AcctIDSource = view.GetInt32(660);
			instance.AccountType = view.GetInt32(581);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			if (view.GetView("QuotSetAckGrp") is MsgView groupViewQuotSetAckGrp)
			{
				instance.QuotSetAckGrp = new QuotSetAckGrp();
				instance.QuotSetAckGrp!.Parse(groupViewQuotSetAckGrp);
			}
			instance.QuotSetAckGrp = new QuotSetAckGrp();
			instance.QuotSetAckGrp?.Parse(view.GetView("QuotSetAckGrp"));
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
