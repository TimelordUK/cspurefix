using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("3", FixVersion.FIX44)]
	public static class RejectExt
	{
		public static void Parse(this Reject instance, MsgView? view)
		{
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.RefSeqNum = view?.GetInt32(45);
			instance.RefTagID = view?.GetInt32(371);
			instance.RefMsgType = view?.GetString(372);
			instance.SessionRejectReason = view?.GetInt32(373);
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
