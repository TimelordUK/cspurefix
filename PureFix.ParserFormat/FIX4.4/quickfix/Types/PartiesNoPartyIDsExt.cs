using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class PartiesNoPartyIDsExt
	{
		public static void Parse(this PartiesNoPartyIDs instance, MsgView? view)
		{
			instance.PartyID = view?.GetString(448);
			instance.PartyIDSource = view?.GetString(447);
			instance.PartyRole = view?.GetInt32(452);
			instance.PtysSubGrp?.Parse(view?.GetView("PtysSubGrp"));
		}
	}
}
