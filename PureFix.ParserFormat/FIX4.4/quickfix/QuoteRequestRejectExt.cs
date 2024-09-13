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
			
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.QuoteReqID = view.GetString(131);
			instance.RFQReqID = view.GetString(644);
			instance.QuoteRequestRejectReason = view.GetInt32(658);
			instance.QuotReqRjctGrp = new QuotReqRjctGrp();
			instance.QuotReqRjctGrp?.Parse(view.GetView("QuotReqRjctGrp"));
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
