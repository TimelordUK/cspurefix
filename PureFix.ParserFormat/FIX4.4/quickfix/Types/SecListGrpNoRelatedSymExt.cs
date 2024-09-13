using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SecListGrpNoRelatedSymExt
	{
		public static void Parse(this SecListGrpNoRelatedSym instance, MsgView? view)
		{
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.InstrumentExtension?.Parse(view?.GetView("InstrumentExtension"));
			instance.FinancingDetails?.Parse(view?.GetView("FinancingDetails"));
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.Currency = view?.GetString(15);
			instance.Stipulations?.Parse(view?.GetView("Stipulations"));
			instance.InstrmtLegSecListGrp?.Parse(view?.GetView("InstrmtLegSecListGrp"));
			instance.SpreadOrBenchmarkCurveData?.Parse(view?.GetView("SpreadOrBenchmarkCurveData"));
			instance.YieldData?.Parse(view?.GetView("YieldData"));
			instance.RoundLot = view?.GetDouble(561);
			instance.MinTradeVol = view?.GetDouble(562);
			instance.TradingSessionID = view?.GetString(336);
			instance.TradingSessionSubID = view?.GetString(625);
			instance.ExpirationCycle = view?.GetInt32(827);
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
		}
	}
}
