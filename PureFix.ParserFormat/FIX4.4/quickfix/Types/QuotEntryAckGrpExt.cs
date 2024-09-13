using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class QuotEntryAckGrpExt
	{
		public static void Parse(this QuotEntryAckGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoQuoteEntries");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoQuoteEntries = new QuotEntryAckGrpNoQuoteEntries[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoQuoteEntries[i] = new();
				instance.NoQuoteEntries[i].Parse(groupView[i]);
			}
		}
	}
}
