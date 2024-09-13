using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class RFQReqGrpNoRelatedSymExt
	{
		public static void Parse(this RFQReqGrpNoRelatedSym instance, MsgView? view)
		{
			instance.Instrument = new Instrument();
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.UndInstrmtGrp = new UndInstrmtGrp();
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.InstrmtLegGrp = new InstrmtLegGrp();
			instance.InstrmtLegGrp?.Parse(view?.GetView("InstrmtLegGrp"));
			instance.PrevClosePx = view?.GetDouble(140);
			instance.QuoteRequestType = view?.GetInt32(303);
			instance.QuoteType = view?.GetInt32(537);
			instance.TradingSessionID = view?.GetString(336);
			instance.TradingSessionSubID = view?.GetString(625);
		}
	}
}
