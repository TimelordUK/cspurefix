using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class InstrmtStrkPxGrpExt
	{
		public static void Parse(this InstrmtStrkPxGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var count = view.GroupCount();
			instance.NoStrikes = new InstrmtStrkPxGrpNoStrikes [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoStrikes[i] = new();
				instance.NoStrikes[i].Parse(view[i]);
			}
		}
	}
}
