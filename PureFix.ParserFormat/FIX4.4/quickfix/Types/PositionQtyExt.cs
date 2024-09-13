using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class PositionQtyExt
	{
		public static void Parse(this PositionQty instance, MsgView? view)
		{
			if (view is null) return;
			
			var count = view.GroupCount();
			instance.NoPositions = new PositionQtyNoPositions [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoPositions[i] = new();
				instance.NoPositions[i].Parse(view[i]);
			}
		}
	}
}
