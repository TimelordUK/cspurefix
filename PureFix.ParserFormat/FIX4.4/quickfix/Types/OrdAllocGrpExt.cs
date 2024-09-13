using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class OrdAllocGrpExt
	{
		public static void Parse(this OrdAllocGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoOrders = new OrdAllocGrpNoOrders [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoOrders[i] = new();
				instance.NoOrders[i].Parse(view?[i]);
			}
		}
	}
}
