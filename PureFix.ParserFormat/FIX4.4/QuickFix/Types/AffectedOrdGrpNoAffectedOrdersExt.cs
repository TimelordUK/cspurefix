using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class AffectedOrdGrpNoAffectedOrdersExt
	{
		public static void Parse(this AffectedOrdGrpNoAffectedOrders instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.OrigClOrdID = view.GetString(41);
			instance.AffectedOrderID = view.GetString(535);
			instance.AffectedSecondaryOrderID = view.GetString(536);
		}
	}
}
