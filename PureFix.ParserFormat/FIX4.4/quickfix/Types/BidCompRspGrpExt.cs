using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class BidCompRspGrpExt
	{
		public static void Parse(this BidCompRspGrp instance, MsgView? view)
		{
			var count = view?.GroupCount() ?? 0;
			instance.NoBidComponents = new BidCompRspGrpNoBidComponents [count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoBidComponents[i] = new();
				instance.NoBidComponents[i].Parse(view?[i]);
			}
		}
	}
}
