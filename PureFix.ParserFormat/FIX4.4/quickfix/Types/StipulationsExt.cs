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
			var count = view?.GroupCount() ?? 0;
			instance.NoStipulations = new StipulationsNoStipulations [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoStipulations[i] = new();
				instance.NoStipulations[i].Parse(view?[i]);
			}
		}
	}
}
