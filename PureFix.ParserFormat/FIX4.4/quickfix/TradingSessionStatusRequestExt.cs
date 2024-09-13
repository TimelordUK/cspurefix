using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("g", FixVersion.FIX44)]
	public static class TradingSessionStatusRequestExt
	{
		public static void Parse(this TradingSessionStatusRequest instance, MsgView? view)
		{
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.TradSesReqID = view?.GetString(335);
			instance.TradingSessionID = view?.GetString(336);
			instance.TradingSessionSubID = view?.GetString(625);
			instance.TradSesMethod = view?.GetInt32(338);
			instance.TradSesMode = view?.GetInt32(339);
			instance.SubscriptionRequestType = view?.GetString(263);
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
