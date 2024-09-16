using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class OrderMassCancelReportNoAffectedOrdersExt
	{
		public static void Parse(this OrderMassCancelReportNoAffectedOrders instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.OrigClOrdID = view.GetString(41);
			instance.AffectedOrderID = view.GetString(535);
			instance.AffectedSecondaryOrderID = view.GetString(536);
		}
	}
}
