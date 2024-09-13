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
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.FinancingDetails?.Parse(view?.GetView("FinancingDetails"));
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.PrevClosePx = view?.GetDouble(140);
			instance.QuoteRequestType = view?.GetInt32(303);
			instance.QuoteType = view?.GetInt32(537);
			instance.TradingSessionID = view?.GetString(336);
			instance.TradingSessionSubID = view?.GetString(625);
			instance.TradeOriginationDate = view?.GetDateTime(229);
			instance.Side = view?.GetString(54);
			instance.QtyType = view?.GetInt32(854);
			instance.OrderQtyData?.Parse(view?.GetView("OrderQtyData"));
			instance.SettlType = view?.GetString(63);
			instance.SettlDate = view?.GetDateTime(64);
			instance.SettlDate2 = view?.GetDateTime(193);
			instance.OrderQty2 = view?.GetDouble(192);
			instance.Currency = view?.GetString(15);
			instance.Stipulations?.Parse(view?.GetView("Stipulations"));
			instance.Account = view?.GetString(1);
			instance.AcctIDSource = view?.GetInt32(660);
			instance.AccountType = view?.GetInt32(581);
			instance.QuotReqLegsGrp?.Parse(view?.GetView("QuotReqLegsGrp"));
			instance.QuotQualGrp?.Parse(view?.GetView("QuotQualGrp"));
			instance.QuotePriceType = view?.GetInt32(692);
			instance.OrdType = view?.GetString(40);
			instance.ValidUntilTime = view?.GetDateTime(62);
			instance.ExpireTime = view?.GetDateTime(126);
			instance.TransactTime = view?.GetDateTime(60);
			instance.SpreadOrBenchmarkCurveData?.Parse(view?.GetView("SpreadOrBenchmarkCurveData"));
			instance.PriceType = view?.GetInt32(423);
			instance.Price = view?.GetDouble(44);
			instance.Price2 = view?.GetDouble(640);
			instance.YieldData?.Parse(view?.GetView("YieldData"));
			instance.Parties?.Parse(view?.GetView("Parties"));
		}
	}
}
