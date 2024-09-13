using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class QuotCxlEntriesGrpNoQuoteEntriesExt
	{
		public static void Parse(this QuotCxlEntriesGrpNoQuoteEntries instance, MsgView? view)
		{
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.FinancingDetails?.Parse(view?.GetView("FinancingDetails"));
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.InstrmtLegGrp?.Parse(view?.GetView("InstrmtLegGrp"));
		}
	}
}
