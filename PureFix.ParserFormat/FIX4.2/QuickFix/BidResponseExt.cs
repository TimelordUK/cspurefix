using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("l", FixVersion.FIX42)]
	public static class BidResponseExt
	{
		public static void Parse(this BidResponse instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.BidID = view.GetString(390);
			instance.ClientBidID = view.GetString(391);
			var groupViewNoBidComponents = view.GetView("NoBidComponents");
			if (groupViewNoBidComponents is null) return;
			
			var countNoBidComponents = groupViewNoBidComponents.GroupCount();
			instance.NoBidComponents = new BidResponseNoBidComponents[countNoBidComponents];
			for (var i = 0; i < countNoBidComponents; ++i)
			{
				instance.NoBidComponents[i] = new();
				instance.NoBidComponents[i].Parse(groupViewNoBidComponents[i]);
			}
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
