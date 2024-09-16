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
			
			var groupViewNoPositions = view.GetView("NoPositions");
			if (groupViewNoPositions is null) return;
			
			var countNoPositions = groupViewNoPositions.GroupCount();
			instance.NoPositions = new PositionQtyNoPositions[countNoPositions];
			for (var i = 0; i < countNoPositions; ++i)
			{
				instance.NoPositions[i] = new();
				instance.NoPositions[i].Parse(groupViewNoPositions[i]);
			}
		}
	}
}
