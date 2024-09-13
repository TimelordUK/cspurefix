using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SecAltIDGrpExt
	{
		public static void Parse(this SecAltIDGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var count = view.GroupCount();
			instance.NoSecurityAltID = new SecAltIDGrpNoSecurityAltID [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoSecurityAltID[i] = new();
				instance.NoSecurityAltID[i].Parse(view[i]);
			}
		}
	}
}
