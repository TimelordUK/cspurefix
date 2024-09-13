using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class YieldDataExt
	{
		public static void Parse(this YieldData instance, MsgView? view)
		{
			instance.YieldType = view?.GetString(235);
			instance.Yield = view?.GetDouble(236);
			instance.YieldCalcDate = view?.GetDateTime(701);
			instance.YieldRedemptionDate = view?.GetDateTime(696);
			instance.YieldRedemptionPrice = view?.GetDouble(697);
			instance.YieldRedemptionPriceType = view?.GetInt32(698);
		}
	}
}
