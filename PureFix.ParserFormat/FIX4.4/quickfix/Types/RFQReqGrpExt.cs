using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class RFQReqGrpExt
	{
		public static void Parse(this RFQReqGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoRelatedSym = new RFQReqGrpNoRelatedSym [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoRelatedSym[i] = new();
				instance.NoRelatedSym[i].Parse(view?[i]);
			}
		}
	}
}
