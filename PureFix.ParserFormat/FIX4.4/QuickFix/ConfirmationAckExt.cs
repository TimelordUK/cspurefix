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
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.ConfirmID = view.GetString(664);
			instance.TradeDate = view.GetDateOnly(75);
			instance.TransactTime = view.GetDateTime(60);
			instance.AffirmStatus = view.GetInt32(940);
			instance.ConfirmRejReason = view.GetInt32(774);
			instance.MatchStatus = view.GetString(573);
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
