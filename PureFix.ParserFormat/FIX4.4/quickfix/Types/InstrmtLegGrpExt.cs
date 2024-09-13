using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class InstrmtLegGrpExt
	{
		public static void Parse(this InstrmtLegGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoLegs = new InstrmtLegGrpNoLegs [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoLegs[i] = new();
				instance.NoLegs[i].Parse(view?[i]);
			}
		}
	}
}
