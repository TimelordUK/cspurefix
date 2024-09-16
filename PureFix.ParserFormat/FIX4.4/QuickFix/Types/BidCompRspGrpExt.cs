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
			if (view is null) return;
			
			var groupViewNoBidComponents = view.GetView("NoBidComponents");
			if (groupViewNoBidComponents is null) return;
			
			var countNoBidComponents = groupViewNoBidComponents.GroupCount();
			instance.NoBidComponents = new BidCompRspGrpNoBidComponents[countNoBidComponents];
			for (var i = 0; i < countNoBidComponents; ++i)
			{
				instance.NoBidComponents[i] = new();
				instance.NoBidComponents[i].Parse(groupViewNoBidComponents[i]);
			}
		}
	}
}
