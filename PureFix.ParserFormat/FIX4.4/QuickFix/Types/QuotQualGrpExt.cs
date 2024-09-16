using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class QuotQualGrpExt
	{
		public static void Parse(this QuotQualGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoQuoteQualifiers = view.GetView("NoQuoteQualifiers");
			if (groupViewNoQuoteQualifiers is null) return;
			
			var countNoQuoteQualifiers = groupViewNoQuoteQualifiers.GroupCount();
			instance.NoQuoteQualifiers = new QuotQualGrpNoQuoteQualifiers[countNoQuoteQualifiers];
			for (var i = 0; i < countNoQuoteQualifiers; ++i)
			{
				instance.NoQuoteQualifiers[i] = new();
				instance.NoQuoteQualifiers[i].Parse(groupViewNoQuoteQualifiers[i]);
			}
		}
	}
}
