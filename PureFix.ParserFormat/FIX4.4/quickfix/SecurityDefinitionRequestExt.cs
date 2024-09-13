using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("c", FixVersion.FIX44)]
	public static class SecurityDefinitionRequestExt
	{
		public static void Parse(this SecurityDefinitionRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.SecurityReqID = view.GetString(320);
			instance.SecurityRequestType = view.GetInt32(321);
			instance.Instrument?.Parse(view.GetView("Instrument"));
			instance.InstrumentExtension?.Parse(view.GetView("InstrumentExtension"));
			instance.UndInstrmtGrp?.Parse(view.GetView("UndInstrmtGrp"));
			instance.Currency = view.GetString(15);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.InstrmtLegGrp?.Parse(view.GetView("InstrmtLegGrp"));
			instance.ExpirationCycle = view.GetInt32(827);
			instance.SubscriptionRequestType = view.GetString(263);
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
