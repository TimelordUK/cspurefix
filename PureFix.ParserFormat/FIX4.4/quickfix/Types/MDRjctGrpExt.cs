using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class MDRjctGrpExt
	{
		public static void Parse(this MDRjctGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoAltMDSource = new MDRjctGrpNoAltMDSource [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoAltMDSource[i] = new();
				instance.NoAltMDSource[i].Parse(view?[i]);
			}
		}
	}
}
