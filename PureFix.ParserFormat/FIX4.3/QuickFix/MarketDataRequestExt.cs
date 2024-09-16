using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("V", FixVersion.FIX43)]
	public static class MarketDataRequestExt
	{
		public static void Parse(this MarketDataRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.MDReqID = view.GetString(262);
			instance.SubscriptionRequestType = view.GetString(263);
			instance.MarketDepth = view.GetInt32(264);
			instance.MDUpdateType = view.GetInt32(265);
			instance.AggregatedBook = view.GetBool(266);
			instance.OpenCloseSettleFlag = view.GetString(286);
			instance.Scope = view.GetString(546);
			instance.MDImplicitDelete = view.GetBool(547);
			var groupViewNoMDEntryTypes = view.GetView("NoMDEntryTypes");
			if (groupViewNoMDEntryTypes is null) return;
			
			var countNoMDEntryTypes = groupViewNoMDEntryTypes.GroupCount();
			instance.NoMDEntryTypes = new MarketDataRequestNoMDEntryTypes[countNoMDEntryTypes];
			for (var i = 0; i < countNoMDEntryTypes; ++i)
			{
				instance.NoMDEntryTypes[i] = new();
				instance.NoMDEntryTypes[i].Parse(groupViewNoMDEntryTypes[i]);
			}
			var groupViewNoRelatedSym = view.GetView("NoRelatedSym");
			if (groupViewNoRelatedSym is null) return;
			
			var countNoRelatedSym = groupViewNoRelatedSym.GroupCount();
			instance.NoRelatedSym = new MarketDataRequestNoRelatedSym[countNoRelatedSym];
			for (var i = 0; i < countNoRelatedSym; ++i)
			{
				instance.NoRelatedSym[i] = new();
				instance.NoRelatedSym[i].Parse(groupViewNoRelatedSym[i]);
			}
			var groupViewNoTradingSessions = view.GetView("NoTradingSessions");
			if (groupViewNoTradingSessions is null) return;
			
			var countNoTradingSessions = groupViewNoTradingSessions.GroupCount();
			instance.NoTradingSessions = new MarketDataRequestNoTradingSessions[countNoTradingSessions];
			for (var i = 0; i < countNoTradingSessions; ++i)
			{
				instance.NoTradingSessions[i] = new();
				instance.NoTradingSessions[i].Parse(groupViewNoTradingSessions[i]);
			}
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
