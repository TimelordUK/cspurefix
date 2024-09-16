using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("R", FixVersion.FIX42)]
	public static class QuoteRequestExt
	{
		public static void Parse(this QuoteRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.QuoteReqID = view.GetString(131);
			var groupViewNoRelatedSym = view.GetView("NoRelatedSym");
			if (groupViewNoRelatedSym is null) return;
			
			var countNoRelatedSym = groupViewNoRelatedSym.GroupCount();
			instance.NoRelatedSym = new QuoteRequestNoRelatedSym[countNoRelatedSym];
			for (var i = 0; i < countNoRelatedSym; ++i)
			{
				instance.NoRelatedSym[i] = new();
				instance.NoRelatedSym[i].Parse(groupViewNoRelatedSym[i]);
			}
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
