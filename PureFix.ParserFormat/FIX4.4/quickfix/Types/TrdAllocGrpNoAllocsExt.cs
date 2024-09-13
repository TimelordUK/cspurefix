using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class TrdAllocGrpNoAllocsExt
	{
		public static void Parse(this TrdAllocGrpNoAllocs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.AllocAccount = view.GetString(79);
			instance.AllocAcctIDSource = view.GetInt32(661);
			instance.AllocSettlCurrency = view.GetString(736);
			instance.IndividualAllocID = view.GetString(467);
			if (view.GetView("NestedParties2") is MsgView groupViewNestedParties2)
			{
				instance.NestedParties2 = new NestedParties2();
				instance.NestedParties2!.Parse(groupViewNestedParties2);
			}
			instance.NestedParties2 = new NestedParties2();
			instance.NestedParties2?.Parse(view.GetView("NestedParties2"));
			instance.AllocQty = view.GetDouble(80);
		}
	}
}
