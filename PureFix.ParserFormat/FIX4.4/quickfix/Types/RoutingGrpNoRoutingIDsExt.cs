using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class RoutingGrpNoRoutingIDsExt
	{
		public static void Parse(this RoutingGrpNoRoutingIDs instance, MsgView? view)
		{
			instance.RoutingType = view?.GetInt32(216);
			instance.RoutingID = view?.GetString(217);
		}
	}
}
