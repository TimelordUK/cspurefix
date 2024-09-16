using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public static class AllocationNoOrdersExt
	{
		public static void Parse(this AllocationNoOrders instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.ClOrdID = view.GetString(11);
			instance.OrderID = view.GetString(37);
			instance.SecondaryOrderID = view.GetString(198);
			instance.ListID = view.GetString(66);
			instance.WaveNo = view.GetString(105);
		}
	}
}
