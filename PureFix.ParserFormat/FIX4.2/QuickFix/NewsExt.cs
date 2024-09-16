using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("B", FixVersion.FIX42)]
	public static class NewsExt
	{
		public static void Parse(this News instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.OrigTime = view.GetDateTime(42);
			instance.Urgency = view.GetString(61);
			instance.Headline = view.GetString(148);
			instance.EncodedHeadlineLen = view.GetInt32(358);
			instance.EncodedHeadline = view.GetByteArray(359);
			var groupViewNoRoutingIDs = view.GetView("NoRoutingIDs");
			if (groupViewNoRoutingIDs is null) return;
			
			var countNoRoutingIDs = groupViewNoRoutingIDs.GroupCount();
			instance.NoRoutingIDs = new NewsNoRoutingIDs[countNoRoutingIDs];
			for (var i = 0; i < countNoRoutingIDs; ++i)
			{
				instance.NoRoutingIDs[i] = new();
				instance.NoRoutingIDs[i].Parse(groupViewNoRoutingIDs[i]);
			}
			var groupViewNoRelatedSym = view.GetView("NoRelatedSym");
			if (groupViewNoRelatedSym is null) return;
			
			var countNoRelatedSym = groupViewNoRelatedSym.GroupCount();
			instance.NoRelatedSym = new NewsNoRelatedSym[countNoRelatedSym];
			for (var i = 0; i < countNoRelatedSym; ++i)
			{
				instance.NoRelatedSym[i] = new();
				instance.NoRelatedSym[i].Parse(groupViewNoRelatedSym[i]);
			}
			var groupViewLinesOfText = view.GetView("LinesOfText");
			if (groupViewLinesOfText is null) return;
			
			var countLinesOfText = groupViewLinesOfText.GroupCount();
			instance.LinesOfText = new NewsLinesOfText[countLinesOfText];
			for (var i = 0; i < countLinesOfText; ++i)
			{
				instance.LinesOfText[i] = new();
				instance.LinesOfText[i].Parse(groupViewLinesOfText[i]);
			}
			instance.URLLink = view.GetString(149);
			instance.RawDataLength = view.GetInt32(95);
			instance.RawData = view.GetByteArray(96);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
