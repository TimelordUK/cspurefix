using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class InstrmtGrpExt
	{
		public static void Parse(this InstrmtGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var count = view.GroupCount();
			instance.NoRelatedSym = new InstrmtGrpNoRelatedSym [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoRelatedSym[i] = new();
				instance.NoRelatedSym[i].Parse(view[i]);
			}
		}
	}
}
