using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class BidCompReqGrpExt
	{
		public static void Parse(this BidCompReqGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupView = view.GetView("NoBidComponents");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoBidComponents = new BidCompReqGrpNoBidComponents[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoBidComponents[i] = new();
				instance.NoBidComponents[i].Parse(groupView[i]);
			}
		}
	}
}
