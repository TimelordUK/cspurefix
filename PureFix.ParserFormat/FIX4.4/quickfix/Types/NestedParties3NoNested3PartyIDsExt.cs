using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class NestedParties3NoNested3PartyIDsExt
	{
		public static void Parse(this NestedParties3NoNested3PartyIDs instance, MsgView? view)
		{
			instance.Nested3PartyID = view?.GetString(949);
			instance.Nested3PartyIDSource = view?.GetString(950);
			instance.Nested3PartyRole = view?.GetInt32(951);
			instance.NstdPtys3SubGrp = new NstdPtys3SubGrp();
			instance.NstdPtys3SubGrp?.Parse(view?.GetView("NstdPtys3SubGrp"));
		}
	}
}
