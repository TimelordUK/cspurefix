using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class InstrmtLegGrpNoLegsExt
	{
		public static void Parse(this InstrmtLegGrpNoLegs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.InstrumentLeg = new InstrumentLeg();
			instance.InstrumentLeg?.Parse(view.GetView("InstrumentLeg"));
		}
	}
}
