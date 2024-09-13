using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class SecListGrpNoRelatedSymExt
	{
		public static void Parse(this SecListGrpNoRelatedSym instance, MsgView? view)
		{
			if (view is null) return;
			
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
			if (view.GetView("FinancingDetails") is MsgView groupViewFinancingDetails)
			{
				instance.FinancingDetails = new FinancingDetails();
				instance.FinancingDetails!.Parse(groupViewFinancingDetails);
			}
			if (view.GetView("UndInstrmtGrp") is MsgView groupViewUndInstrmtGrp)
			{
				instance.UndInstrmtGrp = new UndInstrmtGrp();
				instance.UndInstrmtGrp!.Parse(groupViewUndInstrmtGrp);
			}
			instance.Currency = view.GetString(15);
			if (view.GetView("Stipulations") is MsgView groupViewStipulations)
			{
				instance.Stipulations = new Stipulations();
				instance.Stipulations!.Parse(groupViewStipulations);
			}
			if (view.GetView("InstrmtLegSecListGrp") is MsgView groupViewInstrmtLegSecListGrp)
			{
				instance.InstrmtLegSecListGrp = new InstrmtLegSecListGrp();
				instance.InstrmtLegSecListGrp!.Parse(groupViewInstrmtLegSecListGrp);
			}
			if (view.GetView("SpreadOrBenchmarkCurveData") is MsgView groupViewSpreadOrBenchmarkCurveData)
			{
				instance.SpreadOrBenchmarkCurveData = new SpreadOrBenchmarkCurveData();
				instance.SpreadOrBenchmarkCurveData!.Parse(groupViewSpreadOrBenchmarkCurveData);
			}
			if (view.GetView("YieldData") is MsgView groupViewYieldData)
			{
				instance.YieldData = new YieldData();
				instance.YieldData!.Parse(groupViewYieldData);
			}
			instance.RoundLot = view.GetDouble(561);
			instance.MinTradeVol = view.GetDouble(562);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.ExpirationCycle = view.GetInt32(827);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
		}
	}
}
