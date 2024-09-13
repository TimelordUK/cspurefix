using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SideCrossOrdCxlGrpExt
	{
		public static void Parse(this SideCrossOrdCxlGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var count = view.GroupCount();
			instance.NoSides = new SideCrossOrdCxlGrpNoSides [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoSides[i] = new();
				instance.NoSides[i].Parse(view[i]);
			}
		}
	}
}
