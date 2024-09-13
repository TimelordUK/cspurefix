using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class QuotReqGrpNoRelatedSymExt
	{
		public static void Parse(this QuotReqGrpNoRelatedSym instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			instance.Instrument = new Instrument();
			instance.Instrument?.Parse(view.GetView("Instrument"));
			if (view.GetView("FinancingDetails") is MsgView groupViewFinancingDetails)
			{
				instance.FinancingDetails = new FinancingDetails();
				instance.FinancingDetails!.Parse(groupViewFinancingDetails);
			}
			instance.FinancingDetails = new FinancingDetails();
			instance.FinancingDetails?.Parse(view.GetView("FinancingDetails"));
			if (view.GetView("UndInstrmtGrp") is MsgView groupViewUndInstrmtGrp)
			{
				instance.UndInstrmtGrp = new UndInstrmtGrp();
				instance.UndInstrmtGrp!.Parse(groupViewUndInstrmtGrp);
			}
			instance.UndInstrmtGrp = new UndInstrmtGrp();
			instance.UndInstrmtGrp?.Parse(view.GetView("UndInstrmtGrp"));
			instance.PrevClosePx = view.GetDouble(140);
			instance.QuoteRequestType = view.GetInt32(303);
			instance.QuoteType = view.GetInt32(537);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.TradeOriginationDate = view.GetDateTime(229);
			instance.Side = view.GetString(54);
			instance.QtyType = view.GetInt32(854);
			if (view.GetView("OrderQtyData") is MsgView groupViewOrderQtyData)
			{
				instance.OrderQtyData = new OrderQtyData();
				instance.OrderQtyData!.Parse(groupViewOrderQtyData);
			}
			instance.OrderQtyData = new OrderQtyData();
			instance.OrderQtyData?.Parse(view.GetView("OrderQtyData"));
			instance.SettlType = view.GetString(63);
			instance.SettlDate = view.GetDateTime(64);
			instance.SettlDate2 = view.GetDateTime(193);
			instance.OrderQty2 = view.GetDouble(192);
			instance.Currency = view.GetString(15);
			if (view.GetView("Stipulations") is MsgView groupViewStipulations)
			{
				instance.Stipulations = new Stipulations();
				instance.Stipulations!.Parse(groupViewStipulations);
			}
			instance.Stipulations = new Stipulations();
			instance.Stipulations?.Parse(view.GetView("Stipulations"));
			instance.Account = view.GetString(1);
			instance.AcctIDSource = view.GetInt32(660);
			instance.AccountType = view.GetInt32(581);
			if (view.GetView("QuotReqLegsGrp") is MsgView groupViewQuotReqLegsGrp)
			{
				instance.QuotReqLegsGrp = new QuotReqLegsGrp();
				instance.QuotReqLegsGrp!.Parse(groupViewQuotReqLegsGrp);
			}
			instance.QuotReqLegsGrp = new QuotReqLegsGrp();
			instance.QuotReqLegsGrp?.Parse(view.GetView("QuotReqLegsGrp"));
			if (view.GetView("QuotQualGrp") is MsgView groupViewQuotQualGrp)
			{
				instance.QuotQualGrp = new QuotQualGrp();
				instance.QuotQualGrp!.Parse(groupViewQuotQualGrp);
			}
			instance.QuotQualGrp = new QuotQualGrp();
			instance.QuotQualGrp?.Parse(view.GetView("QuotQualGrp"));
			instance.QuotePriceType = view.GetInt32(692);
			instance.OrdType = view.GetString(40);
			instance.ValidUntilTime = view.GetDateTime(62);
			instance.ExpireTime = view.GetDateTime(126);
			instance.TransactTime = view.GetDateTime(60);
			if (view.GetView("SpreadOrBenchmarkCurveData") is MsgView groupViewSpreadOrBenchmarkCurveData)
			{
				instance.SpreadOrBenchmarkCurveData = new SpreadOrBenchmarkCurveData();
				instance.SpreadOrBenchmarkCurveData!.Parse(groupViewSpreadOrBenchmarkCurveData);
			}
			instance.SpreadOrBenchmarkCurveData = new SpreadOrBenchmarkCurveData();
			instance.SpreadOrBenchmarkCurveData?.Parse(view.GetView("SpreadOrBenchmarkCurveData"));
			instance.PriceType = view.GetInt32(423);
			instance.Price = view.GetDouble(44);
			instance.Price2 = view.GetDouble(640);
			if (view.GetView("YieldData") is MsgView groupViewYieldData)
			{
				instance.YieldData = new YieldData();
				instance.YieldData!.Parse(groupViewYieldData);
			}
			instance.YieldData = new YieldData();
			instance.YieldData?.Parse(view.GetView("YieldData"));
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.Parties = new Parties();
			instance.Parties?.Parse(view.GetView("Parties"));
		}
	}
}
