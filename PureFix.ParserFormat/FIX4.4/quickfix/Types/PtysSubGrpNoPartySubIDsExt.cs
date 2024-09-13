using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class PtysSubGrpNoPartySubIDsExt
	{
		public static void Parse(this PtysSubGrpNoPartySubIDs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.PartySubID = view.GetString(523);
			instance.PartySubIDType = view.GetInt32(803);
		}
	}
}
