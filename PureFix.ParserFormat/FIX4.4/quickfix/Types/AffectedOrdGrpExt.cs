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
			
			var groupView = view.GetView("NoAffectedOrders");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoAffectedOrders = new AffectedOrdGrpNoAffectedOrders[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoAffectedOrders[i] = new();
				instance.NoAffectedOrders[i].Parse(groupView[i]);
			}
		}
	}
}
