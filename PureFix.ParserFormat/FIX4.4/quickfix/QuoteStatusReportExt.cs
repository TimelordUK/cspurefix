using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AI", FixVersion.FIX44)]
	public static class QuoteStatusReportExt
	{
		public static void Parse(this QuoteStatusReport instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.QuoteStatusReqID = view.GetString(649);
			instance.QuoteReqID = view.GetString(131);
			instance.QuoteID = view.GetString(117);
			instance.QuoteRespID = view.GetString(693);
			instance.QuoteType = view.GetInt32(537);
			instance.Parties = new Parties();
			instance.Parties?.Parse(view.GetView("Parties"));
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.Instrument = new Instrument();
			instance.Instrument?.Parse(view.GetView("Instrument"));
			instance.FinancingDetails = new FinancingDetails();
			instance.FinancingDetails?.Parse(view.GetView("FinancingDetails"));
			instance.UndInstrmtGrp = new UndInstrmtGrp();
			instance.UndInstrmtGrp?.Parse(view.GetView("UndInstrmtGrp"));
			instance.Side = view.GetString(54);
			instance.OrderQtyData = new OrderQtyData();
			instance.OrderQtyData?.Parse(view.GetView("OrderQtyData"));
			instance.SettlType = view.GetString(63);
			instance.SettlDate = view.GetDateTime(64);
			instance.SettlDate2 = view.GetDateTime(193);
			instance.OrderQty2 = view.GetDouble(192);
			instance.Currency = view.GetString(15);
			instance.Stipulations = new Stipulations();
			instance.Stipulations?.Parse(view.GetView("Stipulations"));
			instance.Account = view.GetString(1);
			instance.AcctIDSource = view.GetInt32(660);
			instance.AccountType = view.GetInt32(581);
			instance.LegQuotStatGrp = new LegQuotStatGrp();
			instance.LegQuotStatGrp?.Parse(view.GetView("LegQuotStatGrp"));
			instance.QuotQualGrp = new QuotQualGrp();
			instance.QuotQualGrp?.Parse(view.GetView("QuotQualGrp"));
			instance.ExpireTime = view.GetDateTime(126);
			instance.Price = view.GetDouble(44);
			instance.PriceType = view.GetInt32(423);
			instance.SpreadOrBenchmarkCurveData = new SpreadOrBenchmarkCurveData();
			instance.SpreadOrBenchmarkCurveData?.Parse(view.GetView("SpreadOrBenchmarkCurveData"));
			instance.YieldData = new YieldData();
			instance.YieldData?.Parse(view.GetView("YieldData"));
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
			instance.OrdType = view.GetString(40);
			instance.BidForwardPoints2 = view.GetDouble(642);
			instance.OfferForwardPoints2 = view.GetDouble(643);
			instance.SettlCurrBidFxRate = view.GetDouble(656);
			instance.SettlCurrOfferFxRate = view.GetDouble(657);
			instance.SettlCurrFxRateCalc = view.GetString(156);
			instance.CommType = view.GetString(13);
			instance.Commission = view.GetDouble(12);
			instance.CustOrderCapacity = view.GetInt32(582);
			instance.ExDestination = view.GetString(100);
			instance.QuoteStatus = view.GetInt32(297);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
