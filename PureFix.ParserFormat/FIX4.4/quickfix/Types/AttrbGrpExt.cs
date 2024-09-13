using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class AttrbGrpExt
	{
		public static void Parse(this AttrbGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoInstrAttrib = new AttrbGrpNoInstrAttrib [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoInstrAttrib[i] = new();
				instance.NoInstrAttrib[i].Parse(view?[i]);
			}
		}
	}
}
