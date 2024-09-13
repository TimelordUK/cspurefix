using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class RelSymDerivSecGrpExt
	{
		public static void Parse(this RelSymDerivSecGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoRelatedSym");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoRelatedSym = new RelSymDerivSecGrpNoRelatedSym[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoRelatedSym[i] = new();
				instance.NoRelatedSym[i].Parse(groupView[i]);
			}
		}
	}
}
