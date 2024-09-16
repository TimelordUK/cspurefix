using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("h", FixVersion.FIX42)]
	public static class TradingSessionStatusExt
	{
		public static void Parse(this TradingSessionStatus instance, MsgView? view)
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
			instance.UnsolicitedIndicator = view.GetBool(325);
			instance.TradSesStatus = view.GetInt32(340);
			instance.TradSesStartTime = view.GetDateTime(341);
			instance.TradSesOpenTime = view.GetDateTime(342);
			instance.TradSesPreCloseTime = view.GetDateTime(343);
			instance.TradSesCloseTime = view.GetDateTime(344);
			instance.TradSesEndTime = view.GetDateTime(345);
			instance.TotalVolumeTraded = view.GetDouble(387);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
