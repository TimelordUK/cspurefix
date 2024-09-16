using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public static class MassQuoteNoQuoteSetsNoQuoteEntriesExt
	{
		public static void Parse(this MassQuoteNoQuoteSetsNoQuoteEntries instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.QuoteEntryID = view.GetString(299);
			instance.Symbol = view.GetString(55);
			instance.SymbolSfx = view.GetString(65);
			instance.SecurityID = view.GetString(48);
			instance.IDSource = view.GetString(22);
			instance.SecurityType = view.GetString(167);
			instance.MaturityMonthYear = view.GetMonthYear(200);
			instance.MaturityDay = view.GetString(205);
			instance.PutOrCall = view.GetInt32(201);
			instance.StrikePrice = view.GetDouble(202);
			instance.OptAttribute = view.GetString(206);
			instance.ContractMultiplier = view.GetDouble(231);
			instance.CouponRate = view.GetDouble(223);
			instance.SecurityExchange = view.GetString(207);
			instance.Issuer = view.GetString(106);
			instance.EncodedIssuerLen = view.GetInt32(348);
			instance.EncodedIssuer = view.GetByteArray(349);
			instance.SecurityDesc = view.GetString(107);
			instance.EncodedSecurityDescLen = view.GetInt32(350);
			instance.EncodedSecurityDesc = view.GetByteArray(351);
			instance.BidPx = view.GetDouble(132);
			instance.OfferPx = view.GetDouble(133);
			instance.BidSize = view.GetDouble(134);
			instance.OfferSize = view.GetDouble(135);
			instance.ValidUntilTime = view.GetDateTime(62);
			instance.BidSpotRate = view.GetDouble(188);
			instance.OfferSpotRate = view.GetDouble(190);
			instance.BidForwardPoints = view.GetDouble(189);
			instance.OfferForwardPoints = view.GetDouble(191);
			instance.TransactTime = view.GetDateTime(60);
			instance.TradingSessionID = view.GetString(336);
			instance.FutSettDate = view.GetDateOnly(64);
			instance.OrdType = view.GetString(40);
			instance.FutSettDate2 = view.GetDateOnly(193);
			instance.OrderQty2 = view.GetDouble(192);
			instance.Currency = view.GetString(15);
		}
	}
}
