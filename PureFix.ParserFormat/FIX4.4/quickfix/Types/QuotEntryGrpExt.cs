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
			
			var count = view.GroupCount();
			instance.NoQuoteEntries = new QuotEntryGrpNoQuoteEntries [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoQuoteEntries[i] = new();
				instance.NoQuoteEntries[i].Parse(view[i]);
			}
		}
	}
}
