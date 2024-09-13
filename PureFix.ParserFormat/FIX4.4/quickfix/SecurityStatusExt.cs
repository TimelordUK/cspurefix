using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("f", FixVersion.FIX44)]
	public static class SecurityStatusExt
	{
		public static void Parse(this SecurityStatus instance, MsgView? view)
		{
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.SecurityStatusReqID = view?.GetString(324);
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.InstrumentExtension?.Parse(view?.GetView("InstrumentExtension"));
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.InstrmtLegGrp?.Parse(view?.GetView("InstrmtLegGrp"));
			instance.Currency = view?.GetString(15);
			instance.TradingSessionID = view?.GetString(336);
			instance.TradingSessionSubID = view?.GetString(625);
			instance.UnsolicitedIndicator = view?.GetBool(325);
			instance.SecurityTradingStatus = view?.GetInt32(326);
			instance.FinancialStatus = view?.GetString(291);
			instance.CorporateAction = view?.GetString(292);
			instance.HaltReasonChar = view?.GetString(327);
			instance.InViewOfCommon = view?.GetBool(328);
			instance.DueToRelated = view?.GetBool(329);
			instance.BuyVolume = view?.GetDouble(330);
			instance.SellVolume = view?.GetDouble(331);
			instance.HighPx = view?.GetDouble(332);
			instance.LowPx = view?.GetDouble(333);
			instance.LastPx = view?.GetDouble(31);
			instance.TransactTime = view?.GetDateTime(60);
			instance.Adjustment = view?.GetInt32(334);
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
