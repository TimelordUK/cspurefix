using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public static class ListStatusNoOrdersExt
	{
		public static void Parse(this ListStatusNoOrders instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.ClOrdID = view.GetString(11);
			instance.CumQty = view.GetDouble(14);
			instance.OrdStatus = view.GetString(39);
			instance.LeavesQty = view.GetDouble(151);
			instance.CxlQty = view.GetDouble(84);
			instance.AvgPx = view.GetDouble(6);
			instance.OrdRejReason = view.GetInt32(103);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
		}
	}
}
