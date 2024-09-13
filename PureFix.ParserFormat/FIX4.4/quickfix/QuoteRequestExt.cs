using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("R", FixVersion.FIX44)]
	public static class QuoteRequestExt
	{
		public static void Parse(this QuoteRequest instance, MsgView? view)
		{
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.QuoteReqID = view?.GetString(131);
			instance.RFQReqID = view?.GetString(644);
			instance.ClOrdID = view?.GetString(11);
			instance.OrderCapacity = view?.GetString(528);
			instance.QuotReqGrp = new QuotReqGrp();
			instance.QuotReqGrp?.Parse(view?.GetView("QuotReqGrp"));
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
