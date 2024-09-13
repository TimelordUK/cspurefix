using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class NestedPartiesNoNestedPartyIDsExt
	{
		public static void Parse(this NestedPartiesNoNestedPartyIDs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.NestedPartyID = view.GetString(524);
			instance.NestedPartyIDSource = view.GetString(525);
			instance.NestedPartyRole = view.GetInt32(538);
			instance.NstdPtysSubGrp = new NstdPtysSubGrp();
			instance.NstdPtysSubGrp?.Parse(view.GetView("NstdPtysSubGrp"));
		}
	}
}
