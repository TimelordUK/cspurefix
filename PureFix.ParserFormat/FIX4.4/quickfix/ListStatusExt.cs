using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("N", FixVersion.FIX44)]
	public static class ListStatusExt
	{
		public static void Parse(this ListStatus instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.ListID = view.GetString(66);
			instance.ListStatusType = view.GetInt32(429);
			instance.NoRpts = view.GetInt32(82);
			instance.ListOrderStatus = view.GetInt32(431);
			instance.RptSeq = view.GetInt32(83);
			instance.ListStatusText = view.GetString(444);
			instance.EncodedListStatusTextLen = view.GetInt32(445);
			instance.EncodedListStatusText = view.GetByteArray(446);
			instance.TransactTime = view.GetDateTime(60);
			instance.TotNoOrders = view.GetInt32(68);
			instance.LastFragment = view.GetBool(893);
			if (view.GetView("OrdListStatGrp") is MsgView groupViewOrdListStatGrp)
			{
				instance.OrdListStatGrp = new OrdListStatGrp();
				instance.OrdListStatGrp!.Parse(groupViewOrdListStatGrp);
			}
			instance.OrdListStatGrp = new OrdListStatGrp();
			instance.OrdListStatGrp?.Parse(view.GetView("OrdListStatGrp"));
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
