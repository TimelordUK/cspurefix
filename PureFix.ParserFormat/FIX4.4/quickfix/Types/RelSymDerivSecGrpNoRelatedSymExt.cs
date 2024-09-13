using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class RelSymDerivSecGrpNoRelatedSymExt
	{
		public static void Parse(this RelSymDerivSecGrpNoRelatedSym instance, MsgView? view)
		{
			instance.Instrument = new Instrument();
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.Currency = view?.GetString(15);
			instance.ExpirationCycle = view?.GetInt32(827);
			instance.InstrumentExtension = new InstrumentExtension();
			instance.InstrumentExtension?.Parse(view?.GetView("InstrumentExtension"));
			instance.InstrmtLegGrp = new InstrmtLegGrp();
			instance.InstrmtLegGrp?.Parse(view?.GetView("InstrmtLegGrp"));
			instance.TradingSessionID = view?.GetString(336);
			instance.TradingSessionSubID = view?.GetString(625);
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
		}
	}
}
