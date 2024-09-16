using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class RFQRequestNoRelatedSymExt
	{
		public static void Parse(this RFQRequestNoRelatedSym instance, MsgView? view)
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
		}
	}
}
