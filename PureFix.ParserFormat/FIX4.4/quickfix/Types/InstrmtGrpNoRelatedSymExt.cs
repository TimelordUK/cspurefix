using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class InstrmtGrpNoRelatedSymExt
	{
		public static void Parse(this InstrmtGrpNoRelatedSym instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.Instrument?.Parse(view.GetView("Instrument"));
		}
	}
}
