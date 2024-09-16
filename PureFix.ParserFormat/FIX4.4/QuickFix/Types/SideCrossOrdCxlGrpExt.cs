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
			
			var groupViewNoSides = view.GetView("NoSides");
			if (groupViewNoSides is null) return;
			
			var countNoSides = groupViewNoSides.GroupCount();
			instance.NoSides = new SideCrossOrdCxlGrpNoSides[countNoSides];
			for (var i = 0; i < countNoSides; ++i)
			{
				instance.NoSides[i] = new();
				instance.NoSides[i].Parse(groupViewNoSides[i]);
			}
		}
	}
}
