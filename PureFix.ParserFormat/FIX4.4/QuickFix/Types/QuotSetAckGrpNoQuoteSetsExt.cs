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
			if (view.GetView("UnderlyingInstrument") is MsgView groupViewUnderlyingInstrument)
			{
				instance.UnderlyingInstrument = new UnderlyingInstrument();
				instance.UnderlyingInstrument!.Parse(groupViewUnderlyingInstrument);
			}
			instance.TotNoQuoteEntries = view.GetInt32(304);
			instance.LastFragment = view.GetBool(893);
			if (view.GetView("QuotEntryAckGrp") is MsgView groupViewQuotEntryAckGrp)
			{
				instance.QuotEntryAckGrp = new QuotEntryAckGrp();
				instance.QuotEntryAckGrp!.Parse(groupViewQuotEntryAckGrp);
			}
		}
	}
}
