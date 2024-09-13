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
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.IOIID = view?.GetString(23);
			instance.IOITransType = view?.GetString(28);
			instance.IOIRefID = view?.GetString(26);
			instance.Instrument = new Instrument();
			instance.Instrument?.Parse(view?.GetView("Instrument"));
			instance.FinancingDetails = new FinancingDetails();
			instance.FinancingDetails?.Parse(view?.GetView("FinancingDetails"));
			instance.UndInstrmtGrp = new UndInstrmtGrp();
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.Side = view?.GetString(54);
			instance.QtyType = view?.GetInt32(854);
			instance.OrderQtyData = new OrderQtyData();
			instance.OrderQtyData?.Parse(view?.GetView("OrderQtyData"));
			instance.IOIQty = view?.GetString(27);
			instance.Currency = view?.GetString(15);
			instance.Stipulations = new Stipulations();
			instance.Stipulations?.Parse(view?.GetView("Stipulations"));
			instance.InstrmtLegIOIGrp = new InstrmtLegIOIGrp();
			instance.InstrmtLegIOIGrp?.Parse(view?.GetView("InstrmtLegIOIGrp"));
			instance.PriceType = view?.GetInt32(423);
			instance.Price = view?.GetDouble(44);
			instance.ValidUntilTime = view?.GetDateTime(62);
			instance.IOIQltyInd = view?.GetString(25);
			instance.IOINaturalFlag = view?.GetBool(130);
			instance.IOIQualGrp = new IOIQualGrp();
			instance.IOIQualGrp?.Parse(view?.GetView("IOIQualGrp"));
			instance.Text = view?.GetString(58);
			instance.EncodedTextLen = view?.GetInt32(354);
			instance.EncodedText = view?.GetByteArray(355);
			instance.TransactTime = view?.GetDateTime(60);
			instance.URLLink = view?.GetString(149);
			instance.RoutingGrp = new RoutingGrp();
			instance.RoutingGrp?.Parse(view?.GetView("RoutingGrp"));
			instance.SpreadOrBenchmarkCurveData = new SpreadOrBenchmarkCurveData();
			instance.SpreadOrBenchmarkCurveData?.Parse(view?.GetView("SpreadOrBenchmarkCurveData"));
			instance.YieldData = new YieldData();
			instance.YieldData?.Parse(view?.GetView("YieldData"));
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
