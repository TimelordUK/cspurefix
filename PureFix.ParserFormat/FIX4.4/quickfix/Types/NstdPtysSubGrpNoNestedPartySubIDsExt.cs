using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class NstdPtysSubGrpNoNestedPartySubIDsExt
	{
		public static void Parse(this NstdPtysSubGrpNoNestedPartySubIDs instance, MsgView? view)
		{
			instance.NestedPartySubID = view?.GetString(545);
			instance.NestedPartySubIDType = view?.GetInt32(805);
		}
	}
}
