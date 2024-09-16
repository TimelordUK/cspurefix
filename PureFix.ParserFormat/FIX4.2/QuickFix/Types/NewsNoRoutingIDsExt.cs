using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public static class NewsNoRoutingIDsExt
	{
		public static void Parse(this NewsNoRoutingIDs instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.RoutingType = view.GetInt32(216);
			instance.RoutingID = view.GetString(217);
		}
	}
}
