using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class AffectedOrdGrpExt
	{
		public static void Parse(this AffectedOrdGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoAffectedOrders = view.GetView("NoAffectedOrders");
			if (groupViewNoAffectedOrders is null) return;
			
			var countNoAffectedOrders = groupViewNoAffectedOrders.GroupCount();
			instance.NoAffectedOrders = new AffectedOrdGrpNoAffectedOrders[countNoAffectedOrders];
			for (var i = 0; i < countNoAffectedOrders; ++i)
			{
				instance.NoAffectedOrders[i] = new();
				instance.NoAffectedOrders[i].Parse(groupViewNoAffectedOrders[i]);
			}
		}
	}
}
