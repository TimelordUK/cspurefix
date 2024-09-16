using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class DerivativeSecurityListNoRelatedSymExt
	{
		public static void Parse(this DerivativeSecurityListNoRelatedSym instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			instance.Currency = view.GetString(15);
			var groupViewNoLegs = view.GetView("NoLegs");
			if (groupViewNoLegs is null) return;
			
			var countNoLegs = groupViewNoLegs.GroupCount();
			instance.NoLegs = new DerivativeSecurityListNoRelatedSymNoLegs[countNoLegs];
			for (var i = 0; i < countNoLegs; ++i)
			{
				instance.NoLegs[i] = new();
				instance.NoLegs[i].Parse(groupViewNoLegs[i]);
			}
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
		}
	}
}
