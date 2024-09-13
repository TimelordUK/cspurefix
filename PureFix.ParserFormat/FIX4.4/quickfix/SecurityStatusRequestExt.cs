using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("e", FixVersion.FIX44)]
	public static class SecurityStatusRequestExt
	{
		public static void Parse(this SecurityStatusRequest instance, MsgView? view)
		{
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.SecurityStatusReqID = view?.GetString(324);
			instance.Instrument = new Instrument();
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.InstrumentExtension = new InstrumentExtension();
			instance.InstrumentExtension?.Parse(view?.GetView("InstrumentExtension"));
			instance.UndInstrmtGrp = new UndInstrmtGrp();
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.InstrmtLegGrp = new InstrmtLegGrp();
			instance.InstrmtLegGrp?.Parse(view?.GetView("InstrmtLegGrp"));
			instance.Currency = view?.GetString(15);
			instance.SubscriptionRequestType = view?.GetString(263);
			instance.TradingSessionID = view?.GetString(336);
			instance.TradingSessionSubID = view?.GetString(625);
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
