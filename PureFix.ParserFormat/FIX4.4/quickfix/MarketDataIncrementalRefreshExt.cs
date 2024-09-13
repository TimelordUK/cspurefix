using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("X", FixVersion.FIX44)]
	public static class MarketDataIncrementalRefreshExt
	{
		public static void Parse(this MarketDataIncrementalRefresh instance, MsgView? view)
		{
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.MDReqID = view?.GetString(262);
			instance.MDIncGrp = new MDIncGrp();
			instance.MDIncGrp?.Parse(view?.GetView("MDIncGrp"));
			instance.ApplQueueDepth = view?.GetInt32(813);
			instance.ApplQueueResolution = view?.GetInt32(814);
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
