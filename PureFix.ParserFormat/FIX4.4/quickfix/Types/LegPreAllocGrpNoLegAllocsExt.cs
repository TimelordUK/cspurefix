using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class LegPreAllocGrpNoLegAllocsExt
	{
		public static void Parse(this LegPreAllocGrpNoLegAllocs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.LegAllocAccount = view.GetString(671);
			instance.LegIndividualAllocID = view.GetString(672);
			instance.NestedParties2?.Parse(view.GetView("NestedParties2"));
			instance.LegAllocQty = view.GetDouble(673);
			instance.LegAllocAcctIDSource = view.GetString(674);
			instance.LegSettlCurrency = view.GetString(675);
		}
	}
}
