using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SideCrossOrdModGrpExt
	{
		public static void Parse(this SideCrossOrdModGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoSides");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoSides = new SideCrossOrdModGrpNoSides[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoSides[i] = new();
				instance.NoSides[i].Parse(groupView[i]);
			}
		}
	}
}
