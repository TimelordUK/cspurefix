using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("AE", FixVersion.FIX43)]
	public static class TradeCaptureReportExt
	{
		public static void Parse(this TradeCaptureReport instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.TradeReportID = view.GetString(571);
			instance.TradeReportTransType = view.GetString(487);
			instance.TradeRequestID = view.GetString(568);
			instance.ExecType = view.GetString(150);
			instance.TradeReportRefID = view.GetString(572);
			instance.ExecID = view.GetString(17);
			instance.SecondaryExecID = view.GetString(527);
			instance.ExecRestatementReason = view.GetInt32(378);
			instance.PreviouslyReported = view.GetBool(570);
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			if (view.GetView("OrderQtyData") is MsgView groupViewOrderQtyData)
			{
				instance.OrderQtyData = new OrderQtyData();
				instance.OrderQtyData!.Parse(groupViewOrderQtyData);
			}
			instance.LastQty = view.GetDouble(32);
			instance.LastPx = view.GetDouble(31);
			instance.LastSpotRate = view.GetDouble(194);
			instance.LastForwardPoints = view.GetDouble(195);
			instance.LastMkt = view.GetString(30);
			instance.TradeDate = view.GetDateOnly(75);
			instance.TransactTime = view.GetDateTime(60);
			instance.SettlmntTyp = view.GetString(63);
			instance.FutSettDate = view.GetDateOnly(64);
			instance.MatchStatus = view.GetString(573);
			instance.MatchType = view.GetString(574);
			var groupViewNoSides = view.GetView("NoSides");
			if (groupViewNoSides is null) return;
			
			var countNoSides = groupViewNoSides.GroupCount();
			instance.NoSides = new TradeCaptureReportNoSides[countNoSides];
			for (var i = 0; i < countNoSides; ++i)
			{
				instance.NoSides[i] = new();
				instance.NoSides[i].Parse(groupViewNoSides[i]);
			}
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
