using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SettlPtysSubGrpNoSettlPartySubIDsExt
	{
		public static void Parse(this SettlPtysSubGrpNoSettlPartySubIDs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.SettlPartySubID = view.GetString(785);
			instance.SettlPartySubIDType = view.GetInt32(786);
		}
	}
}
