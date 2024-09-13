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
			var count = view?.GroupCount() ?? 0;
			instance.NoQuoteQualifiers = new QuotQualGrpNoQuoteQualifiers [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoQuoteQualifiers[i] = new();
				instance.NoQuoteQualifiers[i].Parse(view?[i]);
			}
		}
	}
}
