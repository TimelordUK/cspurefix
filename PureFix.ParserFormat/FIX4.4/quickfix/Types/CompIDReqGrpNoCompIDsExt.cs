using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class CompIDReqGrpNoCompIDsExt
	{
		public static void Parse(this CompIDReqGrpNoCompIDs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.RefCompID = view.GetString(930);
			instance.RefSubID = view.GetString(931);
			instance.LocationID = view.GetString(283);
			instance.DeskID = view.GetString(284);
		}
	}
}
