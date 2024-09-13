using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("l", FixVersion.FIX44)]
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
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.BidID = view.GetString(390);
			instance.ClientBidID = view.GetString(391);
			if (view.GetView("BidCompRspGrp") is MsgView groupViewBidCompRspGrp)
			{
				instance.BidCompRspGrp = new BidCompRspGrp();
				instance.BidCompRspGrp!.Parse(groupViewBidCompRspGrp);
			}
			instance.BidCompRspGrp = new BidCompRspGrp();
			instance.BidCompRspGrp?.Parse(view.GetView("BidCompRspGrp"));
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
