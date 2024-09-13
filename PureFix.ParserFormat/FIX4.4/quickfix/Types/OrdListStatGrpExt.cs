using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class OrdListStatGrpExt
	{
		public static void Parse(this OrdListStatGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoOrders = new OrdListStatGrpNoOrders [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoOrders[i] = new();
				instance.NoOrders[i].Parse(view?[i]);
			}
		}
	}
}
