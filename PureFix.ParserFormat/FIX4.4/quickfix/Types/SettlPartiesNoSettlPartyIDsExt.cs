using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SettlPartiesNoSettlPartyIDsExt
	{
		public static void Parse(this SettlPartiesNoSettlPartyIDs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.SettlPartyID = view.GetString(782);
			instance.SettlPartyIDSource = view.GetString(783);
			instance.SettlPartyRole = view.GetInt32(784);
			if (view.GetView("SettlPtysSubGrp") is MsgView groupViewSettlPtysSubGrp)
			{
				instance.SettlPtysSubGrp = new SettlPtysSubGrp();
				instance.SettlPtysSubGrp!.Parse(groupViewSettlPtysSubGrp);
			}
			instance.SettlPtysSubGrp = new SettlPtysSubGrp();
			instance.SettlPtysSubGrp?.Parse(view.GetView("SettlPtysSubGrp"));
		}
	}
}
