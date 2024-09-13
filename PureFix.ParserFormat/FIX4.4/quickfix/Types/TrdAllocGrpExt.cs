using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class TrdAllocGrpExt
	{
		public static void Parse(this TrdAllocGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoAllocs = new TrdAllocGrpNoAllocs [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoAllocs[i] = new();
				instance.NoAllocs[i].Parse(view?[i]);
			}
		}
	}
}
