using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("V", FixVersion.FIX44)]
	public static class MarketDataRequestExt
	{
		public static void Parse(this MarketDataRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.MDReqID = view.GetString(262);
			instance.SubscriptionRequestType = view.GetString(263);
			instance.MarketDepth = view.GetInt32(264);
			instance.MDUpdateType = view.GetInt32(265);
			instance.AggregatedBook = view.GetBool(266);
			instance.OpenCloseSettlFlag = view.GetString(286);
			instance.Scope = view.GetString(546);
			instance.MDImplicitDelete = view.GetBool(547);
			instance.MDReqGrp?.Parse(view.GetView("MDReqGrp"));
			instance.InstrmtMDReqGrp?.Parse(view.GetView("InstrmtMDReqGrp"));
			instance.TrdgSesGrp?.Parse(view.GetView("TrdgSesGrp"));
			instance.ApplQueueAction = view.GetInt32(815);
			instance.ApplQueueMax = view.GetInt32(812);
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
