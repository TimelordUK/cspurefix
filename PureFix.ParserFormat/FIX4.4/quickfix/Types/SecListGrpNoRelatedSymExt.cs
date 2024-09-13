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
			instance.Instrument = new Instrument();
			instance.Instrument?.Parse(view.GetView("Instrument"));
			if (view.GetView("InstrumentExtension") is MsgView groupViewInstrumentExtension)
			{
				instance.InstrumentExtension = new InstrumentExtension();
				instance.InstrumentExtension!.Parse(groupViewInstrumentExtension);
			}
			instance.InstrumentExtension = new InstrumentExtension();
			instance.InstrumentExtension?.Parse(view.GetView("InstrumentExtension"));
			if (view.GetView("FinancingDetails") is MsgView groupViewFinancingDetails)
			{
				instance.FinancingDetails = new FinancingDetails();
				instance.FinancingDetails!.Parse(groupViewFinancingDetails);
			}
			instance.FinancingDetails = new FinancingDetails();
			instance.FinancingDetails?.Parse(view.GetView("FinancingDetails"));
			if (view.GetView("UndInstrmtGrp") is MsgView groupViewUndInstrmtGrp)
			{
				instance.UndInstrmtGrp = new UndInstrmtGrp();
				instance.UndInstrmtGrp!.Parse(groupViewUndInstrmtGrp);
			}
			instance.UndInstrmtGrp = new UndInstrmtGrp();
			instance.UndInstrmtGrp?.Parse(view.GetView("UndInstrmtGrp"));
			instance.Currency = view.GetString(15);
			if (view.GetView("Stipulations") is MsgView groupViewStipulations)
			{
				instance.Stipulations = new Stipulations();
				instance.Stipulations!.Parse(groupViewStipulations);
			}
			instance.Stipulations = new Stipulations();
			instance.Stipulations?.Parse(view.GetView("Stipulations"));
			if (view.GetView("InstrmtLegSecListGrp") is MsgView groupViewInstrmtLegSecListGrp)
			{
				instance.InstrmtLegSecListGrp = new InstrmtLegSecListGrp();
				instance.InstrmtLegSecListGrp!.Parse(groupViewInstrmtLegSecListGrp);
			}
			instance.InstrmtLegSecListGrp = new InstrmtLegSecListGrp();
			instance.InstrmtLegSecListGrp?.Parse(view.GetView("InstrmtLegSecListGrp"));
			if (view.GetView("SpreadOrBenchmarkCurveData") is MsgView groupViewSpreadOrBenchmarkCurveData)
			{
				instance.SpreadOrBenchmarkCurveData = new SpreadOrBenchmarkCurveData();
				instance.SpreadOrBenchmarkCurveData!.Parse(groupViewSpreadOrBenchmarkCurveData);
			}
			instance.SpreadOrBenchmarkCurveData = new SpreadOrBenchmarkCurveData();
			instance.SpreadOrBenchmarkCurveData?.Parse(view.GetView("SpreadOrBenchmarkCurveData"));
			if (view.GetView("YieldData") is MsgView groupViewYieldData)
			{
				instance.YieldData = new YieldData();
				instance.YieldData!.Parse(groupViewYieldData);
			}
			instance.YieldData = new YieldData();
			instance.YieldData?.Parse(view.GetView("YieldData"));
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
