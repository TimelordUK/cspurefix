using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class QuotSetAckGrpNoQuoteSetsExt
	{
		public static void Parse(this QuotSetAckGrpNoQuoteSets instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.QuoteSetID = view.GetString(302);
			instance.UnderlyingInstrument?.Parse(view.GetView("UnderlyingInstrument"));
			instance.TotNoQuoteEntries = view.GetInt32(304);
			instance.LastFragment = view.GetBool(893);
			instance.QuotEntryAckGrp?.Parse(view.GetView("QuotEntryAckGrp"));
		}
	}
}
