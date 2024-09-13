using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("7", FixVersion.FIX44)]
	public static class AdvertisementExt
	{
		public static void Parse(this Advertisement instance, MsgView? view)
		{
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.AdvId = view?.GetString(2);
			instance.AdvTransType = view?.GetString(5);
			instance.AdvRefID = view?.GetString(3);
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.InstrmtLegGrp?.Parse(view?.GetView("InstrmtLegGrp"));
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.AdvSide = view?.GetString(4);
			instance.Quantity = view?.GetDouble(53);
			instance.QtyType = view?.GetInt32(854);
			instance.Price = view?.GetDouble(44);
			instance.Currency = view?.GetString(15);
			instance.TradeDate = view?.GetDateTime(75);
			instance.TransactTime = view?.GetDateTime(60);
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.URLLink = view?.GetString(149);
			instance.LastMkt = view?.GetString(30);
			instance.TradingSessionID = view?.GetString(336);
			instance.TradingSessionSubID = view?.GetString(625);
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
