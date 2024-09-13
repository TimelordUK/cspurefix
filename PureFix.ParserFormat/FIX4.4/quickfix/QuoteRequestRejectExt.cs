using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AG", FixVersion.FIX44)]
	public static class QuoteRequestRejectExt
	{
		public static void Parse(this QuoteRequestReject instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.QuoteReqID = view.GetString(131);
			instance.RFQReqID = view.GetString(644);
			instance.QuoteRequestRejectReason = view.GetInt32(658);
			if (view.GetView("QuotReqRjctGrp") is MsgView groupViewQuotReqRjctGrp)
			{
				instance.QuotReqRjctGrp = new QuotReqRjctGrp();
				instance.QuotReqRjctGrp!.Parse(groupViewQuotReqRjctGrp);
			}
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
