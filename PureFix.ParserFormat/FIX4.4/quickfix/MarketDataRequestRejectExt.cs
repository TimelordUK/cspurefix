using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("Y", FixVersion.FIX44)]
	public static class MarketDataRequestRejectExt
	{
		public static void Parse(this MarketDataRequestReject instance, MsgView? view)
		{
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.MDReqID = view?.GetString(262);
			instance.MDReqRejReason = view?.GetString(281);
			instance.MDRjctGrp = new MDRjctGrp();
			instance.MDRjctGrp?.Parse(view?.GetView("MDRjctGrp"));
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
