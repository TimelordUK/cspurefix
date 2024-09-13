using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class InstrmtLegIOIGrpNoLegsExt
	{
		public static void Parse(this InstrmtLegIOIGrpNoLegs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.InstrumentLeg?.Parse(view.GetView("InstrumentLeg"));
			instance.LegIOIQty = view.GetString(682);
			instance.LegStipulations?.Parse(view.GetView("LegStipulations"));
		}
	}
}
