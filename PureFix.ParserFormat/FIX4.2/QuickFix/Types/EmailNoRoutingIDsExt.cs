using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public static class EmailNoRoutingIDsExt
	{
		public static void Parse(this EmailNoRoutingIDs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.RoutingType = view.GetInt32(216);
			instance.RoutingID = view.GetString(217);
		}
	}
}
