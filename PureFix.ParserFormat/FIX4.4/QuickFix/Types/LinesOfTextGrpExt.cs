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
			
			var groupViewNoLinesOfText = view.GetView("NoLinesOfText");
			if (groupViewNoLinesOfText is null) return;
			
			var countNoLinesOfText = groupViewNoLinesOfText.GroupCount();
			instance.NoLinesOfText = new LinesOfTextGrpNoLinesOfText[countNoLinesOfText];
			for (var i = 0; i < countNoLinesOfText; ++i)
			{
				instance.NoLinesOfText[i] = new();
				instance.NoLinesOfText[i].Parse(groupViewNoLinesOfText[i]);
			}
		}
	}
}
