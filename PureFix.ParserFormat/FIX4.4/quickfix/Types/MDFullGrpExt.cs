using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class MDFullGrpExt
	{
		public static void Parse(this MDFullGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoMDEntries = new MDFullGrpNoMDEntries [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoMDEntries[i] = new();
				instance.NoMDEntries[i].Parse(view?[i]);
			}
		}
	}
}
