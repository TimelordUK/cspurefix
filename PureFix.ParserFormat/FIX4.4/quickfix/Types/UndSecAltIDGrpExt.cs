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
			var count = view?.GroupCount() ?? 0;
			instance.NoUnderlyingSecurityAltID = new UndSecAltIDGrpNoUnderlyingSecurityAltID [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoUnderlyingSecurityAltID[i] = new();
				instance.NoUnderlyingSecurityAltID[i].Parse(view?[i]);
			}
		}
	}
}
