using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class LinesOfTextGrpExt
	{
		public static void Parse(this LinesOfTextGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var count = view.GroupCount();
			instance.NoLinesOfText = new LinesOfTextGrpNoLinesOfText [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoLinesOfText[i] = new();
				instance.NoLinesOfText[i].Parse(view[i]);
			}
		}
	}
}
