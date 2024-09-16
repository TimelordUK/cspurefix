using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("6", FixVersion.FIX43)]
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
			instance.IOIid = view.GetString(23);
			instance.IOITransType = view.GetString(28);
			instance.IOIRefID = view.GetString(26);
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			instance.Side = view.GetString(54);
			instance.QuantityType = view.GetInt32(465);
			instance.IOIQty = view.GetString(27);
			instance.PriceType = view.GetInt32(423);
			instance.Price = view.GetDouble(44);
			instance.Currency = view.GetString(15);
			instance.ValidUntilTime = view.GetDateTime(62);
			instance.IOIQltyInd = view.GetString(25);
			instance.IOINaturalFlag = view.GetBool(130);
			var groupViewNoIOIQualifiers = view.GetView("NoIOIQualifiers");
			if (groupViewNoIOIQualifiers is null) return;
			
			var countNoIOIQualifiers = groupViewNoIOIQualifiers.GroupCount();
			instance.NoIOIQualifiers = new IOINoIOIQualifiers[countNoIOIQualifiers];
			for (var i = 0; i < countNoIOIQualifiers; ++i)
			{
				instance.NoIOIQualifiers[i] = new();
				instance.NoIOIQualifiers[i].Parse(groupViewNoIOIQualifiers[i]);
			}
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.TransactTime = view.GetDateTime(60);
			instance.URLLink = view.GetString(149);
			var groupViewNoRoutingIDs = view.GetView("NoRoutingIDs");
			if (groupViewNoRoutingIDs is null) return;
			
			var countNoRoutingIDs = groupViewNoRoutingIDs.GroupCount();
			instance.NoRoutingIDs = new IOINoRoutingIDs[countNoRoutingIDs];
			for (var i = 0; i < countNoRoutingIDs; ++i)
			{
				instance.NoRoutingIDs[i] = new();
				instance.NoRoutingIDs[i].Parse(groupViewNoRoutingIDs[i]);
			}
			if (view.GetView("SpreadOrBenchmarkCurveData") is MsgView groupViewSpreadOrBenchmarkCurveData)
			{
				instance.SpreadOrBenchmarkCurveData = new SpreadOrBenchmarkCurveData();
				instance.SpreadOrBenchmarkCurveData!.Parse(groupViewSpreadOrBenchmarkCurveData);
			}
			instance.Benchmark = view.GetString(219);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
