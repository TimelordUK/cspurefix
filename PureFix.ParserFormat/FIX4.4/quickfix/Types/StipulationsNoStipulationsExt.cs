using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class StipulationsNoStipulationsExt
	{
		public static void Parse(this StipulationsNoStipulations instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.StipulationType = view.GetString(233);
			instance.StipulationValue = view.GetString(234);
		}
	}
}
