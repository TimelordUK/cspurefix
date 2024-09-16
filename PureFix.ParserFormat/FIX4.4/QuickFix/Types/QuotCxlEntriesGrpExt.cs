using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class QuotCxlEntriesGrpExt
	{
		public static void Parse(this QuotCxlEntriesGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoQuoteEntries");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoQuoteEntries = new QuotCxlEntriesGrpNoQuoteEntries[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoQuoteEntries[i] = new();
				instance.NoQuoteEntries[i].Parse(groupView[i]);
			}
		}
	}
}
