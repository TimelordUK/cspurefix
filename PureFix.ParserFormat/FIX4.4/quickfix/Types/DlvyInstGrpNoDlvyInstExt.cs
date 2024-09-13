using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class DlvyInstGrpNoDlvyInstExt
	{
		public static void Parse(this DlvyInstGrpNoDlvyInst instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.SettlInstSource = view.GetString(165);
			instance.DlvyInstType = view.GetString(787);
			if (view.GetView("SettlParties") is MsgView groupViewSettlParties)
			{
				instance.SettlParties = new SettlParties();
				instance.SettlParties!.Parse(groupViewSettlParties);
			}
			instance.SettlParties = new SettlParties();
			instance.SettlParties?.Parse(view.GetView("SettlParties"));
		}
	}
}
