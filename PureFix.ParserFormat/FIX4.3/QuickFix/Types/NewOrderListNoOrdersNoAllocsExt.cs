using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class NewOrderListNoOrdersNoAllocsExt
	{
		public static void Parse(this NewOrderListNoOrdersNoAllocs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.AllocAccount = view.GetString(79);
			instance.IndividualAllocID = view.GetString(467);
			if (view.GetView("NestedParties") is MsgView groupViewNestedParties)
			{
				instance.NestedParties = new NestedParties();
				instance.NestedParties!.Parse(groupViewNestedParties);
			}
			instance.AllocQty = view.GetDouble(80);
		}
	}
}
