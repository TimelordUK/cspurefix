using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class SecurityDefinitionRequestNoLegsExt
	{
		public static void Parse(this SecurityDefinitionRequestNoLegs instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("InstrumentLeg") is MsgView groupViewInstrumentLeg)
			{
				instance.InstrumentLeg = new InstrumentLeg();
				instance.InstrumentLeg!.Parse(groupViewInstrumentLeg);
			}
			instance.LegCurrency = view.GetString(556);
		}
	}
}
