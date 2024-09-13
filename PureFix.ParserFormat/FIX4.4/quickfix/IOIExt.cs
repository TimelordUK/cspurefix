using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("6", FixVersion.FIX44)]
	public static class IOIExt
	{
		public static void Parse(this IOI instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.IOIID = view.GetString(23);
			instance.IOITransType = view.GetString(28);
			instance.IOIRefID = view.GetString(26);
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
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
			instance.Side = view.GetString(54);
			instance.QtyType = view.GetInt32(854);
			if (view.GetView("OrderQtyData") is MsgView groupViewOrderQtyData)
			{
				instance.OrderQtyData = new OrderQtyData();
				instance.OrderQtyData!.Parse(groupViewOrderQtyData);
			}
			instance.IOIQty = view.GetString(27);
			instance.Currency = view.GetString(15);
			if (view.GetView("Stipulations") is MsgView groupViewStipulations)
			{
				instance.Stipulations = new Stipulations();
				instance.Stipulations!.Parse(groupViewStipulations);
			}
			if (view.GetView("InstrmtLegIOIGrp") is MsgView groupViewInstrmtLegIOIGrp)
			{
				instance.InstrmtLegIOIGrp = new InstrmtLegIOIGrp();
				instance.InstrmtLegIOIGrp!.Parse(groupViewInstrmtLegIOIGrp);
			}
			instance.PriceType = view.GetInt32(423);
			instance.Price = view.GetDouble(44);
			instance.ValidUntilTime = view.GetDateTime(62);
			instance.IOIQltyInd = view.GetString(25);
			instance.IOINaturalFlag = view.GetBool(130);
			if (view.GetView("IOIQualGrp") is MsgView groupViewIOIQualGrp)
			{
				instance.IOIQualGrp = new IOIQualGrp();
				instance.IOIQualGrp!.Parse(groupViewIOIQualGrp);
			}
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.TransactTime = view.GetDateTime(60);
			instance.URLLink = view.GetString(149);
			if (view.GetView("RoutingGrp") is MsgView groupViewRoutingGrp)
			{
				instance.RoutingGrp = new RoutingGrp();
				instance.RoutingGrp!.Parse(groupViewRoutingGrp);
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
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
