using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("W", FixVersion.FIX43)]
	public static class MarketDataSnapshotFullRefreshExt
	{
		public static void Parse(this MarketDataSnapshotFullRefresh instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.MDReqID = view.GetString(262);
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			instance.FinancialStatus = view.GetString(291);
			instance.CorporateAction = view.GetString(292);
			instance.TotalVolumeTraded = view.GetDouble(387);
			instance.TotalVolumeTradedDate = view.GetString(449);
			instance.TotalVolumeTradedTime = view.GetTimeOnly(450);
			instance.NetChgPrevDay = view.GetDouble(451);
			var groupViewNoMDEntries = view.GetView("NoMDEntries");
			if (groupViewNoMDEntries is null) return;
			
			var countNoMDEntries = groupViewNoMDEntries.GroupCount();
			instance.NoMDEntries = new MarketDataSnapshotFullRefreshNoMDEntries[countNoMDEntries];
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
