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
			
			var groupViewNoLegSecurityAltID = view.GetView("NoLegSecurityAltID");
			if (groupViewNoLegSecurityAltID is null) return;
			
			var countNoLegSecurityAltID = groupViewNoLegSecurityAltID.GroupCount();
			instance.NoLegSecurityAltID = new LegSecAltIDGrpNoLegSecurityAltID[countNoLegSecurityAltID];
			for (var i = 0; i < countNoLegSecurityAltID; ++i)
			{
				instance.NoLegSecurityAltID[i] = new();
				instance.NoLegSecurityAltID[i].Parse(groupViewNoLegSecurityAltID[i]);
			}
		}
	}
}
