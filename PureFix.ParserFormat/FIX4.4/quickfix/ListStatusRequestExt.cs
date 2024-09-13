using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("M", FixVersion.FIX44)]
	public static class ListStatusRequestExt
	{
		public static void Parse(this ListStatusRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.ListID = view.GetString(66);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
