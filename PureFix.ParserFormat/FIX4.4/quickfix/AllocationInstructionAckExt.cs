using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("P", FixVersion.FIX44)]
	public static class AllocationInstructionAckExt
	{
		public static void Parse(this AllocationInstructionAck instance, MsgView? view)
		{
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.AllocID = view?.GetString(70);
			instance.Parties?.Parse(view?.GetView("Parties"));
			instance.SecondaryAllocID = view?.GetString(793);
			instance.TradeDate = view?.GetDateTime(75);
			instance.TransactTime = view?.GetDateTime(60);
			instance.AllocStatus = view?.GetInt32(87);
			instance.AllocRejCode = view?.GetInt32(88);
			instance.AllocType = view?.GetInt32(626);
			instance.AllocIntermedReqType = view?.GetInt32(808);
			instance.MatchStatus = view?.GetString(573);
			instance.Product = view?.GetInt32(460);
			instance.SecurityType = view?.GetString(167);
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.AllocAckGrp?.Parse(view?.GetView("AllocAckGrp"));
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
