using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class RoutingGrpExt
	{
		public static void Parse(this RoutingGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var count = view.GroupCount();
			instance.NoRoutingIDs = new RoutingGrpNoRoutingIDs [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoRoutingIDs[i] = new();
				instance.NoRoutingIDs[i].Parse(view[i]);
			}
		}
	}
}
