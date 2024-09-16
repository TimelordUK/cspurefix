using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("d", FixVersion.FIX43)]
	public static class SecurityDefinitionExt
	{
		public static void Parse(this SecurityDefinition instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.SecurityReqID = view.GetString(320);
			instance.SecurityResponseID = view.GetString(322);
			instance.SecurityResponseType = view.GetInt32(323);
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			instance.Currency = view.GetString(15);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			var groupViewNoLegs = view.GetView("NoLegs");
			if (groupViewNoLegs is null) return;
			
			var countNoLegs = groupViewNoLegs.GroupCount();
			instance.NoLegs = new SecurityDefinitionNoLegs[countNoLegs];
			for (var i = 0; i < countNoLegs; ++i)
			{
				instance.NoLegs[i] = new();
				instance.NoLegs[i].Parse(groupViewNoLegs[i]);
			}
			instance.RoundLot = view.GetDouble(561);
			instance.MinTradeVol = view.GetDouble(562);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
