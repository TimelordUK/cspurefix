using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("n", FixVersion.FIX44)]
	public static class XMLnonFIXExt
	{
		public static void Parse(this XMLnonFIX instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
