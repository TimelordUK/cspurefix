using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class NstdPtys2SubGrpNoNested2PartySubIDsExt
	{
		public static void Parse(this NstdPtys2SubGrpNoNested2PartySubIDs instance, MsgView? view)
		{
			instance.Nested2PartySubID = view?.GetString(760);
			instance.Nested2PartySubIDType = view?.GetInt32(807);
		}
	}
}
