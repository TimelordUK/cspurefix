using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class BidDescReqGrpExt
	{
		public static void Parse(this BidDescReqGrp instance, MsgView? view)
		{
			if (view is null) return;
			
			var groupViewNoBidDescriptors = view.GetView("NoBidDescriptors");
			if (groupViewNoBidDescriptors is null) return;
			
			var countNoBidDescriptors = groupViewNoBidDescriptors.GroupCount();
			instance.NoBidDescriptors = new BidDescReqGrpNoBidDescriptors[countNoBidDescriptors];
			for (var i = 0; i < countNoBidDescriptors; ++i)
			{
				instance.NoBidDescriptors[i] = new();
				instance.NoBidDescriptors[i].Parse(groupViewNoBidDescriptors[i]);
			}
		}
	}
}
