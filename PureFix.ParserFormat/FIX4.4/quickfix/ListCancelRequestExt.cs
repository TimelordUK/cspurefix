using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("K", FixVersion.FIX44)]
	public static class ListCancelRequestExt
	{
		public static void Parse(this ListCancelRequest instance, MsgView? view)
		{
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.ListID = view?.GetString(66);
			instance.TransactTime = view?.GetDateTime(60);
			instance.TradeOriginationDate = view?.GetDateTime(229);
			instance.TradeDate = view?.GetDateTime(75);
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
