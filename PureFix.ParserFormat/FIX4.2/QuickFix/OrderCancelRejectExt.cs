using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("9", FixVersion.FIX42)]
	public static class OrderCancelRejectExt
	{
		public static void Parse(this OrderCancelReject instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.OrderID = view.GetString(37);
			instance.SecondaryOrderID = view.GetString(198);
			instance.ClOrdID = view.GetString(11);
			instance.OrigClOrdID = view.GetString(41);
			instance.OrdStatus = view.GetString(39);
			instance.ClientID = view.GetString(109);
			instance.ExecBroker = view.GetString(76);
			instance.ListID = view.GetString(66);
			instance.Account = view.GetString(1);
			instance.TransactTime = view.GetDateTime(60);
			instance.CxlRejResponseTo = view.GetString(434);
			instance.CxlRejReason = view.GetInt32(102);
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
