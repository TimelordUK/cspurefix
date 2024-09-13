using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class NstdPtys3SubGrpNoNested3PartySubIDsExt
	{
		public static void Parse(this NstdPtys3SubGrpNoNested3PartySubIDs instance, MsgView? view)
		{
			instance.Nested3PartySubID = view?.GetString(953);
			instance.Nested3PartySubIDType = view?.GetInt32(954);
		}
	}
}
