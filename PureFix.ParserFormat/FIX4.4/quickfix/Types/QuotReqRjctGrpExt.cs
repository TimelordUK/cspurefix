using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class QuotReqRjctGrpExt
	{
		public static void Parse(this QuotReqRjctGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoRelatedSym = new QuotReqRjctGrpNoRelatedSym [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoRelatedSym[i] = new();
				instance.NoRelatedSym[i].Parse(view?[i]);
			}
		}
	}
}
