using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AU", FixVersion.FIX44)]
	public static class ConfirmationAckExt
	{
		public static void Parse(this ConfirmationAck instance, MsgView? view)
		{
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.ConfirmID = view?.GetString(664);
			instance.TradeDate = view?.GetDateTime(75);
			instance.TransactTime = view?.GetDateTime(60);
			instance.AffirmStatus = view?.GetInt32(940);
			instance.ConfirmRejReason = view?.GetInt32(774);
			instance.MatchStatus = view?.GetString(573);
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
