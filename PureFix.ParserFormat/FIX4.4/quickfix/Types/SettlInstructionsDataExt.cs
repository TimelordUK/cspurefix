using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SettlInstructionsDataExt
	{
		public static void Parse(this SettlInstructionsData instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.SettlDeliveryType = view.GetInt32(172);
			instance.StandInstDbType = view.GetInt32(169);
			instance.StandInstDbName = view.GetString(170);
			instance.StandInstDbID = view.GetString(171);
			instance.DlvyInstGrp?.Parse(view.GetView("DlvyInstGrp"));
		}
	}
}
