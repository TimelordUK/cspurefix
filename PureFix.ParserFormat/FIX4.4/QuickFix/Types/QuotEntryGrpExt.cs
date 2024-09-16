using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class QuotEntryGrpExt
	{
		public static void Parse(this QuotEntryGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoQuoteEntries = view.GetView("NoQuoteEntries");
			if (groupViewNoQuoteEntries is null) return;
			
			var countNoQuoteEntries = groupViewNoQuoteEntries.GroupCount();
			instance.NoQuoteEntries = new QuotEntryGrpNoQuoteEntries[countNoQuoteEntries];
			for (var i = 0; i < countNoQuoteEntries; ++i)
			{
				instance.NoQuoteEntries[i] = new();
				instance.NoQuoteEntries[i].Parse(groupViewNoQuoteEntries[i]);
			}
		}
	}
}
