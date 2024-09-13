using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class QuotSetGrpNoQuoteSetsExt
	{
		public static void Parse(this QuotSetGrpNoQuoteSets instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.QuoteSetID = view.GetString(302);
			instance.UnderlyingInstrument = new UnderlyingInstrument();
			instance.UnderlyingInstrument?.Parse(view.GetView("UnderlyingInstrument"));
			instance.QuoteSetValidUntilTime = view.GetDateTime(367);
			instance.TotNoQuoteEntries = view.GetInt32(304);
			instance.LastFragment = view.GetBool(893);
			instance.QuotEntryGrp = new QuotEntryGrp();
			instance.QuotEntryGrp?.Parse(view.GetView("QuotEntryGrp"));
		}
	}
}
