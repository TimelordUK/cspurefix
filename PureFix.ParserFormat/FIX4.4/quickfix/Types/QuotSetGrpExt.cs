using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class QuotSetGrpExt
	{
		public static void Parse(this QuotSetGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoQuoteSets = new QuotSetGrpNoQuoteSets [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoQuoteSets[i] = new();
				instance.NoQuoteSets[i].Parse(view?[i]);
			}
		}
	}
}
