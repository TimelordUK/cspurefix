using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("AD", FixVersion.FIX43)]
	public static class TradeCaptureReportRequestExt
	{
		public static void Parse(this TradeCaptureReportRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.TradeRequestID = view.GetString(568);
			instance.TradeRequestType = view.GetInt32(569);
			instance.SubscriptionRequestType = view.GetString(263);
			instance.ExecID = view.GetString(17);
			instance.OrderID = view.GetString(37);
			instance.ClOrdID = view.GetString(11);
			instance.MatchStatus = view.GetString(573);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			var groupViewNoDates = view.GetView("NoDates");
			if (groupViewNoDates is null) return;
			
			var countNoDates = groupViewNoDates.GroupCount();
			instance.NoDates = new TradeCaptureReportRequestNoDates[countNoDates];
			for (var i = 0; i < countNoDates; ++i)
			{
				instance.NoDates[i] = new();
				instance.NoDates[i].Parse(groupViewNoDates[i]);
			}
			instance.Side = view.GetString(54);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.TradeInputSource = view.GetString(578);
			instance.TradeInputDevice = view.GetString(579);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
