using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("e", FixVersion.FIX44)]
	public static class SecurityStatusRequestExt
	{
		public static void Parse(this SecurityStatusRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.SecurityStatusReqID = view.GetString(324);
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			if (view.GetView("InstrumentExtension") is MsgView groupViewInstrumentExtension)
			{
				instance.InstrumentExtension = new InstrumentExtension();
				instance.InstrumentExtension!.Parse(groupViewInstrumentExtension);
			}
			if (view.GetView("UndInstrmtGrp") is MsgView groupViewUndInstrmtGrp)
			{
				instance.UndInstrmtGrp = new UndInstrmtGrp();
				instance.UndInstrmtGrp!.Parse(groupViewUndInstrmtGrp);
			}
			if (view.GetView("InstrmtLegGrp") is MsgView groupViewInstrmtLegGrp)
			{
				instance.InstrmtLegGrp = new InstrmtLegGrp();
				instance.InstrmtLegGrp!.Parse(groupViewInstrmtLegGrp);
			}
			instance.Currency = view.GetString(15);
			instance.SubscriptionRequestType = view.GetString(263);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
