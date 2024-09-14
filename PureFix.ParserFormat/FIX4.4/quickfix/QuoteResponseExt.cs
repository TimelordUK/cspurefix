using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AJ", FixVersion.FIX44)]
	public static class QuoteResponseExt
	{
		public static void Parse(this QuoteResponse instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.QuoteRespID = view.GetString(693);
			instance.QuoteID = view.GetString(117);
			instance.QuoteRespType = view.GetInt32(694);
			instance.ClOrdID = view.GetString(11);
			instance.OrderCapacity = view.GetString(528);
			instance.IOIID = view.GetString(23);
			instance.QuoteType = view.GetInt32(537);
			if (view.GetView("QuotQualGrp") is MsgView groupViewQuotQualGrp)
			{
				instance.QuotQualGrp = new QuotQualGrp();
				instance.QuotQualGrp!.Parse(groupViewQuotQualGrp);
			}
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			if (view.GetView("FinancingDetails") is MsgView groupViewFinancingDetails)
			{
				instance.FinancingDetails = new FinancingDetails();
				instance.FinancingDetails!.Parse(groupViewFinancingDetails);
			}
			if (view.GetView("UndInstrmtGrp") is MsgView groupViewUndInstrmtGrp)
			{
				instance.UndInstrmtGrp = new UndInstrmtGrp();
				instance.UndInstrmtGrp!.Parse(groupViewUndInstrmtGrp);
			}
			instance.Side = view.GetString(54);
			if (view.GetView("OrderQtyData") is MsgView groupViewOrderQtyData)
			{
				instance.OrderQtyData = new OrderQtyData();
				instance.OrderQtyData!.Parse(groupViewOrderQtyData);
			}
			instance.SettlType = view.GetString(63);
			instance.SettlDate = view.GetDateOnly(64);
			instance.SettlDate2 = view.GetDateOnly(193);
			instance.OrderQty2 = view.GetDouble(192);
			instance.Currency = view.GetString(15);
			if (view.GetView("Stipulations") is MsgView groupViewStipulations)
			{
				instance.Stipulations = new Stipulations();
				instance.Stipulations!.Parse(groupViewStipulations);
			}
			instance.Account = view.GetString(1);
			instance.AcctIDSource = view.GetInt32(660);
			instance.AccountType = view.GetInt32(581);
			if (view.GetView("LegQuotGrp") is MsgView groupViewLegQuotGrp)
			{
				instance.LegQuotGrp = new LegQuotGrp();
				instance.LegQuotGrp!.Parse(groupViewLegQuotGrp);
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
			instance.OrdType = view.GetString(40);
			instance.BidForwardPoints2 = view.GetDouble(642);
			instance.OfferForwardPoints2 = view.GetDouble(643);
			instance.SettlCurrBidFxRate = view.GetDouble(656);
			instance.SettlCurrOfferFxRate = view.GetDouble(657);
			instance.SettlCurrFxRateCalc = view.GetString(156);
			instance.Commission = view.GetDouble(12);
			instance.CommType = view.GetString(13);
			instance.CustOrderCapacity = view.GetInt32(582);
			instance.ExDestination = view.GetString(100);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.Price = view.GetDouble(44);
			instance.PriceType = view.GetInt32(423);
			if (view.GetView("SpreadOrBenchmarkCurveData") is MsgView groupViewSpreadOrBenchmarkCurveData)
			{
				instance.SpreadOrBenchmarkCurveData = new SpreadOrBenchmarkCurveData();
				instance.SpreadOrBenchmarkCurveData!.Parse(groupViewSpreadOrBenchmarkCurveData);
			}
			if (view.GetView("YieldData") is MsgView groupViewYieldData)
			{
				instance.YieldData = new YieldData();
				instance.YieldData!.Parse(groupViewYieldData);
			}
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
