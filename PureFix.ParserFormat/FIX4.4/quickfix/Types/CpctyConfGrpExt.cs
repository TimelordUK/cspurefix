using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class CpctyConfGrpExt
	{
		public static void Parse(this CpctyConfGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoCapacities = new CpctyConfGrpNoCapacities [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoCapacities[i] = new();
				instance.NoCapacities[i].Parse(view?[i]);
			}
		}
	}
}
