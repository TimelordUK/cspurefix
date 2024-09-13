using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("T", FixVersion.FIX44)]
	public static class SettlementInstructionsExt
	{
		public static void Parse(this SettlementInstructions instance, MsgView? view)
		{
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.SettlInstMsgID = view?.GetString(777);
			instance.SettlInstReqID = view?.GetString(791);
			instance.SettlInstMode = view?.GetString(160);
			instance.SettlInstReqRejCode = view?.GetInt32(792);
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.ClOrdID = view?.GetString(11);
			instance.TransactTime = view?.GetDateTime(60);
			instance.SettlInstGrp = new SettlInstGrp();
			instance.SettlInstGrp?.Parse(view?.GetView("SettlInstGrp"));
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
