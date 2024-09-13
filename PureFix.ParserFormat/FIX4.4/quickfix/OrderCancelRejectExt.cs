using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("9", FixVersion.FIX44)]
	public static class OrderCancelRejectExt
	{
		public static void Parse(this OrderCancelReject instance, MsgView? view)
		{
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.OrderID = view?.GetString(37);
			instance.SecondaryOrderID = view?.GetString(198);
			instance.SecondaryClOrdID = view?.GetString(526);
			instance.ClOrdID = view?.GetString(11);
			instance.ClOrdLinkID = view?.GetString(583);
			instance.OrigClOrdID = view?.GetString(41);
			instance.OrdStatus = view?.GetString(39);
			instance.WorkingIndicator = view?.GetBool(636);
			instance.OrigOrdModTime = view?.GetDateTime(586);
			instance.ListID = view?.GetString(66);
			instance.Account = view?.GetString(1);
			instance.AcctIDSource = view?.GetInt32(660);
			instance.AccountType = view?.GetInt32(581);
			instance.TradeOriginationDate = view?.GetDateTime(229);
			instance.TradeDate = view?.GetDateTime(75);
			instance.TransactTime = view?.GetDateTime(60);
			instance.CxlRejResponseTo = view?.GetString(434);
			instance.CxlRejReason = view?.GetInt32(102);
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
