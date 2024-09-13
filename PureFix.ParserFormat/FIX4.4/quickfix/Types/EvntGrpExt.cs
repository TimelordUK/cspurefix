using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class EvntGrpExt
	{
		public static void Parse(this EvntGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var count = view.GroupCount();
			instance.NoEvents = new EvntGrpNoEvents [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoEvents[i] = new();
				instance.NoEvents[i].Parse(view[i]);
			}
		}
	}
}
