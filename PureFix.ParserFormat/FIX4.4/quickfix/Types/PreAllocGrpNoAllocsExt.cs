using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class PreAllocGrpNoAllocsExt
	{
		public static void Parse(this PreAllocGrpNoAllocs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.AllocAccount = view.GetString(79);
			instance.AllocAcctIDSource = view.GetInt32(661);
			instance.AllocSettlCurrency = view.GetString(736);
			instance.IndividualAllocID = view.GetString(467);
			instance.NestedParties = new NestedParties();
			instance.NestedParties?.Parse(view.GetView("NestedParties"));
			instance.AllocQty = view.GetDouble(80);
		}
	}
}
