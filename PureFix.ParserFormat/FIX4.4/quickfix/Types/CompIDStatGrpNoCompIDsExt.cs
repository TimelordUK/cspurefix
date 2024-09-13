using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class CompIDStatGrpNoCompIDsExt
	{
		public static void Parse(this CompIDStatGrpNoCompIDs instance, MsgView? view)
		{
			instance.RefCompID = view?.GetString(930);
			instance.RefSubID = view?.GetString(931);
			instance.LocationID = view?.GetString(283);
			instance.DeskID = view?.GetString(284);
			instance.StatusValue = view?.GetInt32(928);
			instance.StatusText = view?.GetString(929);
		}
	}
}
