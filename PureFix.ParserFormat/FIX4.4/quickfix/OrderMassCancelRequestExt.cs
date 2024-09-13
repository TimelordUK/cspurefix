using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("q", FixVersion.FIX44)]
	public static class OrderMassCancelRequestExt
	{
		public static void Parse(this OrderMassCancelRequest instance, MsgView? view)
		{
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.ClOrdID = view?.GetString(11);
			instance.SecondaryClOrdID = view?.GetString(526);
			instance.MassCancelRequestType = view?.GetString(530);
			instance.TradingSessionID = view?.GetString(336);
			instance.TradingSessionSubID = view?.GetString(625);
			instance.Instrument = new Instrument();
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.UnderlyingInstrument = new UnderlyingInstrument();
			instance.UnderlyingInstrument?.Parse(view?.GetView("UnderlyingInstrument"));
			instance.Side = view?.GetString(54);
			instance.TransactTime = view?.GetDateTime(60);
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
