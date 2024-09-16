using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("g", FixVersion.FIX42)]
	public static class TradingSessionStatusRequestExt
	{
		public static void Parse(this TradingSessionStatusRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.TradSesReqID = view.GetString(335);
			instance.TradingSessionID = view.GetString(336);
			instance.TradSesMethod = view.GetInt32(338);
			instance.TradSesMode = view.GetInt32(339);
			instance.SubscriptionRequestType = view.GetString(263);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
