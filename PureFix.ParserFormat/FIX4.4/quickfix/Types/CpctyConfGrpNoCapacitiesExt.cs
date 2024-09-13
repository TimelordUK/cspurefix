using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class CpctyConfGrpNoCapacitiesExt
	{
		public static void Parse(this CpctyConfGrpNoCapacities instance, MsgView? view)
		{
			instance.OrderCapacity = view?.GetString(528);
			instance.OrderRestrictions = view?.GetString(529);
			instance.OrderCapacityQty = view?.GetDouble(863);
		}
	}
}
