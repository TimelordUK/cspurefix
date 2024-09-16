using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class UndSecAltIDGrpExt
	{
		public static void Parse(this UndSecAltIDGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoUnderlyingSecurityAltID = view.GetView("NoUnderlyingSecurityAltID");
			if (groupViewNoUnderlyingSecurityAltID is null) return;
			
			var countNoUnderlyingSecurityAltID = groupViewNoUnderlyingSecurityAltID.GroupCount();
			instance.NoUnderlyingSecurityAltID = new UndSecAltIDGrpNoUnderlyingSecurityAltID[countNoUnderlyingSecurityAltID];
			for (var i = 0; i < countNoUnderlyingSecurityAltID; ++i)
			{
				instance.NoUnderlyingSecurityAltID[i] = new();
				instance.NoUnderlyingSecurityAltID[i].Parse(groupViewNoUnderlyingSecurityAltID[i]);
			}
		}
	}
}
