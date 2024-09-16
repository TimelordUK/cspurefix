using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("X", FixVersion.FIX43)]
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
			var groupViewNoMDEntries = view.GetView("NoMDEntries");
			if (groupViewNoMDEntries is null) return;
			
			var countNoMDEntries = groupViewNoMDEntries.GroupCount();
			instance.NoMDEntries = new MarketDataIncrementalRefreshNoMDEntries[countNoMDEntries];
			for (var i = 0; i < countNoMDEntries; ++i)
			{
				instance.NoMDEntries[i] = new();
				instance.NoMDEntries[i].Parse(groupViewNoMDEntries[i]);
			}
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
