using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class YieldDataExt
	{
		public static void Parse(this YieldData instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.YieldType = view.GetString(235);
			instance.Yield = view.GetDouble(236);
		}
	}
}
