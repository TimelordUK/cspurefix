using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class NestedParties2NoNested2PartyIDsExt
	{
		public static void Parse(this NestedParties2NoNested2PartyIDs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.Nested2PartyID = view.GetString(757);
			instance.Nested2PartyIDSource = view.GetString(758);
			instance.Nested2PartyRole = view.GetInt32(759);
			instance.NstdPtys2SubGrp = new NstdPtys2SubGrp();
			instance.NstdPtys2SubGrp?.Parse(view.GetView("NstdPtys2SubGrp"));
		}
	}
}
