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
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.MDReqID = view.GetString(262);
			if (view.GetView("MDIncGrp") is MsgView groupViewMDIncGrp)
			{
				instance.MDIncGrp = new MDIncGrp();
				instance.MDIncGrp!.Parse(groupViewMDIncGrp);
			}
			instance.ApplQueueDepth = view.GetInt32(813);
			instance.ApplQueueResolution = view.GetInt32(814);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
