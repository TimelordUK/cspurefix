using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("x", FixVersion.FIX44)]
	public static class SecurityListRequestExt
	{
		public static void Parse(this SecurityListRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.SecurityReqID = view.GetString(320);
			instance.SecurityListRequestType = view.GetInt32(559);
			instance.Instrument?.Parse(view.GetView("Instrument"));
			instance.InstrumentExtension?.Parse(view.GetView("InstrumentExtension"));
			instance.FinancingDetails?.Parse(view.GetView("FinancingDetails"));
			instance.UndInstrmtGrp?.Parse(view.GetView("UndInstrmtGrp"));
			instance.InstrmtLegGrp?.Parse(view.GetView("InstrmtLegGrp"));
			instance.Currency = view.GetString(15);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.SubscriptionRequestType = view.GetString(263);
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
