using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class InstrumentExtensionExt
	{
		public static void Parse(this InstrumentExtension instance, MsgView? view)
		{
			instance.DeliveryForm = view?.GetInt32(668);
			instance.PctAtRisk = view?.GetDouble(869);
			instance.AttrbGrp = new AttrbGrp();
			instance.AttrbGrp?.Parse(view?.GetView("AttrbGrp"));
		}
	}
}
