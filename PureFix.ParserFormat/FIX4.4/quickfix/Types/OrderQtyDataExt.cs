using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class OrderQtyDataExt
	{
		public static void Parse(this OrderQtyData instance, MsgView? view)
		{
			instance.OrderQty = view?.GetDouble(38);
			instance.CashOrderQty = view?.GetDouble(152);
			instance.OrderPercent = view?.GetDouble(516);
			instance.RoundingDirection = view?.GetString(468);
			instance.RoundingModulus = view?.GetDouble(469);
		}
	}
}
