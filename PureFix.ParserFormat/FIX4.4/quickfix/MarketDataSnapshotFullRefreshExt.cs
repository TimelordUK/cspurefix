using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("W", FixVersion.FIX44)]
	public static class MarketDataSnapshotFullRefreshExt
	{
		public static void Parse(this MarketDataSnapshotFullRefresh instance, MsgView? view)
		{
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.MDReqID = view?.GetString(262);
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.InstrmtLegGrp?.Parse(view?.GetView("InstrmtLegGrp"));
			instance.FinancialStatus = view?.GetString(291);
			instance.CorporateAction = view?.GetString(292);
			instance.NetChgPrevDay = view?.GetDouble(451);
			instance.MDFullGrp?.Parse(view?.GetView("MDFullGrp"));
			instance.ApplQueueDepth = view?.GetInt32(813);
			instance.ApplQueueResolution = view?.GetInt32(814);
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
