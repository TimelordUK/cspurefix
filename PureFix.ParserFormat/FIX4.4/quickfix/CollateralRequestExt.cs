using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AX", FixVersion.FIX44)]
	public static class CollateralRequestExt
	{
		public static void Parse(this CollateralRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.CollReqID = view.GetString(894);
			instance.CollAsgnReason = view.GetInt32(895);
			instance.TransactTime = view.GetDateTime(60);
			instance.ExpireTime = view.GetDateTime(126);
			instance.Parties?.Parse(view.GetView("Parties"));
			instance.Account = view.GetString(1);
			instance.AccountType = view.GetInt32(581);
			instance.ClOrdID = view.GetString(11);
			instance.OrderID = view.GetString(37);
			instance.SecondaryOrderID = view.GetString(198);
			instance.SecondaryClOrdID = view.GetString(526);
			instance.ExecCollGrp?.Parse(view.GetView("ExecCollGrp"));
			instance.TrdCollGrp?.Parse(view.GetView("TrdCollGrp"));
			instance.Instrument?.Parse(view.GetView("Instrument"));
			instance.FinancingDetails?.Parse(view.GetView("FinancingDetails"));
			instance.SettlDate = view.GetDateTime(64);
			instance.Quantity = view.GetDouble(53);
			instance.QtyType = view.GetInt32(854);
			instance.Currency = view.GetString(15);
			instance.InstrmtLegGrp?.Parse(view.GetView("InstrmtLegGrp"));
			instance.UndInstrmtCollGrp?.Parse(view.GetView("UndInstrmtCollGrp"));
			instance.MarginExcess = view.GetDouble(899);
			instance.TotalNetValue = view.GetDouble(900);
			instance.CashOutstanding = view.GetDouble(901);
			instance.TrdRegTimestamps?.Parse(view.GetView("TrdRegTimestamps"));
			instance.Side = view.GetString(54);
			instance.MiscFeesGrp?.Parse(view.GetView("MiscFeesGrp"));
			instance.Price = view.GetDouble(44);
			instance.PriceType = view.GetInt32(423);
			instance.AccruedInterestAmt = view.GetDouble(159);
			instance.EndAccruedInterestAmt = view.GetDouble(920);
			instance.StartCash = view.GetDouble(921);
			instance.EndCash = view.GetDouble(922);
			instance.SpreadOrBenchmarkCurveData?.Parse(view.GetView("SpreadOrBenchmarkCurveData"));
			instance.Stipulations?.Parse(view.GetView("Stipulations"));
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.SettlSessID = view.GetString(716);
			instance.SettlSessSubID = view.GetString(717);
			instance.ClearingBusinessDate = view.GetDateTime(715);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
