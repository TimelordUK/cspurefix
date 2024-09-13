using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class ListOrdGrpExt
	{
		public static void Parse(this ListOrdGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var count = view.GroupCount();
			instance.NoOrders = new ListOrdGrpNoOrders [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoOrders[i] = new();
				instance.NoOrders[i].Parse(view[i]);
			}
		}
	}
}
