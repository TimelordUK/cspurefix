using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class QuotQualGrpNoQuoteQualifiersExt
	{
		public static void Parse(this QuotQualGrpNoQuoteQualifiers instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.QuoteQualifier = view.GetString(695);
		}
	}
}
