using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class LegSecAltIDGrpExt
	{
		public static void Parse(this LegSecAltIDGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoLegSecurityAltID");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoLegSecurityAltID = new LegSecAltIDGrpNoLegSecurityAltID[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoLegSecurityAltID[i] = new();
				instance.NoLegSecurityAltID[i].Parse(groupView[i]);
			}
		}
	}
}
