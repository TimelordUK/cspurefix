using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("w", FixVersion.FIX44)]
	public static class SecurityTypesExt
	{
		public static void Parse(this SecurityTypes instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.SecurityReqID = view.GetString(320);
			instance.SecurityResponseID = view.GetString(322);
			instance.SecurityResponseType = view.GetInt32(323);
			instance.TotNoSecurityTypes = view.GetInt32(557);
			instance.LastFragment = view.GetBool(893);
			if (view.GetView("SecTypesGrp") is MsgView groupViewSecTypesGrp)
			{
				instance.SecTypesGrp = new SecTypesGrp();
				instance.SecTypesGrp!.Parse(groupViewSecTypesGrp);
			}
			instance.SecTypesGrp = new SecTypesGrp();
			instance.SecTypesGrp?.Parse(view.GetView("SecTypesGrp"));
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.SubscriptionRequestType = view.GetString(263);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
