using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SecTypesGrpExt
	{
		public static void Parse(this SecTypesGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoSecurityTypes = new SecTypesGrpNoSecurityTypes [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoSecurityTypes[i] = new();
				instance.NoSecurityTypes[i].Parse(view?[i]);
			}
		}
	}
}
