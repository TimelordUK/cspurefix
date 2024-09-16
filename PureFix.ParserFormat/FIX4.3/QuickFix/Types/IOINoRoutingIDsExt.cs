using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class IOINoRoutingIDsExt
	{
		public static void Parse(this IOINoRoutingIDs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.RoutingType = view.GetInt32(216);
			instance.RoutingID = view.GetString(217);
		}
	}
}
