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
			instance.OrdListStatGrp?.Parse(view.GetView("OrdListStatGrp"));
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
