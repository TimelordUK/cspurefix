using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("AI", FixVersion.FIX43)]
	public static class QuoteStatusReportExt
	{
		public static void Parse(this QuoteStatusReport instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.QuoteStatusReqID = view.GetString(649);
			instance.QuoteReqID = view.GetString(131);
			instance.QuoteID = view.GetString(117);
			instance.QuoteType = view.GetInt32(537);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.Account = view.GetString(1);
			instance.AccountType = view.GetInt32(581);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			instance.BidPx = view.GetDouble(132);
			instance.OfferPx = view.GetDouble(133);
			instance.MktBidPx = view.GetDouble(645);
			instance.MktOfferPx = view.GetDouble(646);
			instance.MinBidSize = view.GetDouble(647);
			instance.BidSize = view.GetDouble(134);
			instance.MinOfferSize = view.GetDouble(648);
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
			instance.FutSettDate = view.GetDateOnly(64);
			instance.OrdType = view.GetString(40);
			instance.FutSettDate2 = view.GetDateOnly(193);
			instance.OrderQty2 = view.GetDouble(192);
			instance.BidForwardPoints2 = view.GetDouble(642);
			instance.OfferForwardPoints2 = view.GetDouble(643);
			instance.Currency = view.GetString(15);
			instance.SettlCurrBidFxRate = view.GetDouble(656);
			instance.SettlCurrOfferFxRate = view.GetDouble(657);
			instance.SettlCurrFxRateCalc = view.GetString(156);
			instance.Commission = view.GetDouble(12);
			instance.CommType = view.GetString(13);
			instance.CustOrderCapacity = view.GetInt32(582);
			instance.ExDestination = view.GetString(100);
			instance.QuoteStatus = view.GetInt32(297);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
