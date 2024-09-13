using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class StipulationsExt
	{
		public static void Parse(this Stipulations instance, MsgView? view)
		{
			if (view is null) return;
			
			var count = view.GroupCount();
			instance.NoStipulations = new StipulationsNoStipulations [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoStipulations[i] = new();
				instance.NoStipulations[i].Parse(view[i]);
			}
		}
	}
}
