using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class PositionQtyNoPositionsExt
	{
		public static void Parse(this PositionQtyNoPositions instance, MsgView? view)
		{
			instance.PosType = view?.GetString(703);
			instance.LongQty = view?.GetDouble(704);
			instance.ShortQty = view?.GetDouble(705);
			instance.PosQtyStatus = view?.GetInt32(706);
			instance.NestedParties?.Parse(view?.GetView("NestedParties"));
		}
	}
}
