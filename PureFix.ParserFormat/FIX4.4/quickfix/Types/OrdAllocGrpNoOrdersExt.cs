using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class OrdAllocGrpNoOrdersExt
	{
		public static void Parse(this OrdAllocGrpNoOrders instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.ClOrdID = view.GetString(11);
			instance.OrderID = view.GetString(37);
			instance.SecondaryOrderID = view.GetString(198);
			instance.SecondaryClOrdID = view.GetString(526);
			instance.ListID = view.GetString(66);
			instance.NestedParties2 = new NestedParties2();
			instance.NestedParties2?.Parse(view.GetView("NestedParties2"));
			instance.OrderQty = view.GetDouble(38);
			instance.OrderAvgPx = view.GetDouble(799);
			instance.OrderBookingQty = view.GetDouble(800);
		}
	}
}
