using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("z", FixVersion.FIX43)]
	public static class DerivativeSecurityListRequestExt
	{
		public static void Parse(this DerivativeSecurityListRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.SecurityReqID = view.GetString(320);
			instance.SecurityListRequestType = view.GetInt32(559);
			if (view.GetView("UnderlyingInstrument") is MsgView groupViewUnderlyingInstrument)
			{
				instance.UnderlyingInstrument = new UnderlyingInstrument();
				instance.UnderlyingInstrument!.Parse(groupViewUnderlyingInstrument);
			}
			instance.Currency = view.GetString(15);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.SubscriptionRequestType = view.GetString(263);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
