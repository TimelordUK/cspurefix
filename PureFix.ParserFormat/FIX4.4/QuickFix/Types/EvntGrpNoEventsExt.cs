using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class EvntGrpNoEventsExt
	{
		public static void Parse(this EvntGrpNoEvents instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.EventType = view.GetInt32(865);
			instance.EventDate = view.GetDateOnly(866);
			instance.EventPx = view.GetDouble(867);
			instance.EventText = view.GetString(868);
		}
	}
}
