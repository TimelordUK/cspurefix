using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("E", FixVersion.FIX42)]
	public static class NewOrderListExt
	{
		public static void Parse(this NewOrderList instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.ListID = view.GetString(66);
			instance.BidID = view.GetString(390);
			instance.ClientBidID = view.GetString(391);
			instance.ProgRptReqs = view.GetInt32(414);
			instance.BidType = view.GetInt32(394);
			instance.ProgPeriodInterval = view.GetInt32(415);
			instance.ListExecInstType = view.GetString(433);
			instance.ListExecInst = view.GetString(69);
			instance.EncodedListExecInstLen = view.GetInt32(352);
			instance.EncodedListExecInst = view.GetByteArray(353);
			instance.TotNoOrders = view.GetInt32(68);
			var groupViewNoOrders = view.GetView("NoOrders");
			if (groupViewNoOrders is null) return;
			
			var countNoOrders = groupViewNoOrders.GroupCount();
			instance.NoOrders = new NewOrderListNoOrders[countNoOrders];
			for (var i = 0; i < countNoOrders; ++i)
			{
				instance.NoOrders[i] = new();
				instance.NoOrders[i].Parse(groupViewNoOrders[i]);
			}
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
