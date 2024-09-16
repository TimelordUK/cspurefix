using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class MassQuoteAcknowledgementNoQuoteSetsExt
	{
		public static void Parse(this MassQuoteAcknowledgementNoQuoteSets instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.QuoteSetID = view.GetString(302);
			if (view.GetView("UnderlyingInstrument") is MsgView groupViewUnderlyingInstrument)
			{
				instance.UnderlyingInstrument = new UnderlyingInstrument();
				instance.UnderlyingInstrument!.Parse(groupViewUnderlyingInstrument);
			}
			instance.TotQuoteEntries = view.GetInt32(304);
			var groupViewNoQuoteEntries = view.GetView("NoQuoteEntries");
			if (groupViewNoQuoteEntries is null) return;
			
			var countNoQuoteEntries = groupViewNoQuoteEntries.GroupCount();
			instance.NoQuoteEntries = new MassQuoteAcknowledgementNoQuoteSetsNoQuoteEntries[countNoQuoteEntries];
			for (var i = 0; i < countNoQuoteEntries; ++i)
			{
				instance.NoQuoteEntries[i] = new();
				instance.NoQuoteEntries[i].Parse(groupViewNoQuoteEntries[i]);
			}
		}
	}
}
