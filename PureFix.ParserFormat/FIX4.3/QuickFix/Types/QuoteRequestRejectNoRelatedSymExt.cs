using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class QuoteRequestRejectNoRelatedSymExt
	{
		public static void Parse(this QuoteRequestRejectNoRelatedSym instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			instance.PrevClosePx = view.GetDouble(140);
			instance.QuoteRequestType = view.GetInt32(303);
			instance.QuoteType = view.GetInt32(537);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.TradeOriginationDate = view.GetString(229);
			if (view.GetView("Stipulations") is MsgView groupViewStipulations)
			{
				instance.Stipulations = new Stipulations();
				instance.Stipulations!.Parse(groupViewStipulations);
			}
			instance.Side = view.GetString(54);
			instance.QuantityType = view.GetInt32(465);
			instance.OrderQty = view.GetDouble(38);
			instance.CashOrderQty = view.GetDouble(152);
			instance.SettlmntTyp = view.GetString(63);
			instance.FutSettDate = view.GetDateOnly(64);
			instance.OrdType = view.GetString(40);
			instance.FutSettDate2 = view.GetDateOnly(193);
			instance.OrderQty2 = view.GetDouble(192);
			instance.ExpireTime = view.GetDateTime(126);
			instance.TransactTime = view.GetDateTime(60);
			instance.Currency = view.GetString(15);
			if (view.GetView("SpreadOrBenchmarkCurveData") is MsgView groupViewSpreadOrBenchmarkCurveData)
			{
				instance.SpreadOrBenchmarkCurveData = new SpreadOrBenchmarkCurveData();
				instance.SpreadOrBenchmarkCurveData!.Parse(groupViewSpreadOrBenchmarkCurveData);
			}
			instance.PriceType = view.GetInt32(423);
			instance.Price = view.GetDouble(44);
			instance.Price2 = view.GetDouble(640);
			if (view.GetView("YieldData") is MsgView groupViewYieldData)
			{
				instance.YieldData = new YieldData();
				instance.YieldData!.Parse(groupViewYieldData);
			}
		}
	}
}
