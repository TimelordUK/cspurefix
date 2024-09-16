using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class MDReqGrpNoMDEntryTypesExt
	{
		public static void Parse(this MDReqGrpNoMDEntryTypes instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.MDEntryType = view.GetString(269);
		}
	}
}
