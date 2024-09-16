using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class MassQuoteNoQuoteSetsNoQuoteEntriesExt
	{
		public static void Parse(this MassQuoteNoQuoteSetsNoQuoteEntries instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.QuoteEntryID = view.GetString(299);
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			instance.BidPx = view.GetDouble(132);
			instance.OfferPx = view.GetDouble(133);
			instance.BidSize = view.GetDouble(134);
			instance.OfferSize = view.GetDouble(135);
			instance.ValidUntilTime = view.GetDateTime(62);
			instance.BidSpotRate = view.GetDouble(188);
			instance.OfferSpotRate = view.GetDouble(190);
			instance.BidForwardPoints = view.GetDouble(189);
			instance.OfferForwardPoints = view.GetDouble(191);
			instance.MidPx = view.GetDouble(631);
			instance.BidYield = view.GetDouble(632);
			instance.MidYield = view.GetDouble(633);
			instance.OfferYield = view.GetDouble(634);
			instance.TransactTime = view.GetDateTime(60);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.FutSettDate = view.GetDateOnly(64);
			instance.OrdType = view.GetString(40);
			instance.FutSettDate2 = view.GetDateOnly(193);
			instance.OrderQty2 = view.GetDouble(192);
			instance.BidForwardPoints2 = view.GetDouble(642);
			instance.OfferForwardPoints2 = view.GetDouble(643);
			instance.Currency = view.GetString(15);
		}
	}
}
