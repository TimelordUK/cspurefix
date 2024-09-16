using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("C", FixVersion.FIX43)]
	public static class EmailExt
	{
		public static void Parse(this Email instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.EmailThreadID = view.GetString(164);
			instance.EmailType = view.GetString(94);
			instance.OrigTime = view.GetDateTime(42);
			instance.Subject = view.GetString(147);
			instance.EncodedSubjectLen = view.GetInt32(356);
			instance.EncodedSubject = view.GetByteArray(357);
			var groupViewNoRoutingIDs = view.GetView("NoRoutingIDs");
			if (groupViewNoRoutingIDs is null) return;
			
			var countNoRoutingIDs = groupViewNoRoutingIDs.GroupCount();
			instance.NoRoutingIDs = new EmailNoRoutingIDs[countNoRoutingIDs];
			for (var i = 0; i < countNoRoutingIDs; ++i)
			{
				instance.NoRoutingIDs[i] = new();
				instance.NoRoutingIDs[i].Parse(groupViewNoRoutingIDs[i]);
			}
			var groupViewNoRelatedSym = view.GetView("NoRelatedSym");
			if (groupViewNoRelatedSym is null) return;
			
			var countNoRelatedSym = groupViewNoRelatedSym.GroupCount();
			instance.NoRelatedSym = new EmailNoRelatedSym[countNoRelatedSym];
			for (var i = 0; i < countNoRelatedSym; ++i)
			{
				instance.NoRelatedSym[i] = new();
				instance.NoRelatedSym[i].Parse(groupViewNoRelatedSym[i]);
			}
			instance.OrderID = view.GetString(37);
			instance.ClOrdID = view.GetString(11);
			var groupViewLinesOfText = view.GetView("LinesOfText");
			if (groupViewLinesOfText is null) return;
			
			var countLinesOfText = groupViewLinesOfText.GroupCount();
			instance.LinesOfText = new EmailLinesOfText[countLinesOfText];
			for (var i = 0; i < countLinesOfText; ++i)
			{
				instance.LinesOfText[i] = new();
				instance.LinesOfText[i].Parse(groupViewLinesOfText[i]);
			}
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
