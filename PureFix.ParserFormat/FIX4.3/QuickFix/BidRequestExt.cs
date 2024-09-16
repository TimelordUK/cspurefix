using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("k", FixVersion.FIX43)]
	public static class BidRequestExt
	{
		public static void Parse(this BidRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.BidID = view.GetString(390);
			instance.ClientBidID = view.GetString(391);
			instance.BidRequestTransType = view.GetString(374);
			instance.ListName = view.GetString(392);
			instance.TotalNumSecurities = view.GetInt32(393);
			instance.BidType = view.GetInt32(394);
			instance.NumTickets = view.GetInt32(395);
			instance.Currency = view.GetString(15);
			instance.SideValue1 = view.GetDouble(396);
			instance.SideValue2 = view.GetDouble(397);
			var groupViewNoBidDescriptors = view.GetView("NoBidDescriptors");
			if (groupViewNoBidDescriptors is null) return;
			
			var countNoBidDescriptors = groupViewNoBidDescriptors.GroupCount();
			instance.NoBidDescriptors = new BidRequestNoBidDescriptors[countNoBidDescriptors];
			for (var i = 0; i < countNoBidDescriptors; ++i)
			{
				instance.NoBidDescriptors[i] = new();
				instance.NoBidDescriptors[i].Parse(groupViewNoBidDescriptors[i]);
			}
			var groupViewNoBidComponents = view.GetView("NoBidComponents");
			if (groupViewNoBidComponents is null) return;
			
			var countNoBidComponents = groupViewNoBidComponents.GroupCount();
			instance.NoBidComponents = new BidRequestNoBidComponents[countNoBidComponents];
			for (var i = 0; i < countNoBidComponents; ++i)
			{
				instance.NoBidComponents[i] = new();
				instance.NoBidComponents[i].Parse(groupViewNoBidComponents[i]);
			}
			instance.LiquidityIndType = view.GetInt32(409);
			instance.WtAverageLiquidity = view.GetDouble(410);
			instance.ExchangeForPhysical = view.GetBool(411);
			instance.OutMainCntryUIndex = view.GetDouble(412);
			instance.CrossPercent = view.GetDouble(413);
			instance.ProgRptReqs = view.GetInt32(414);
			instance.ProgPeriodInterval = view.GetInt32(415);
			instance.IncTaxInd = view.GetInt32(416);
			instance.ForexReq = view.GetBool(121);
			instance.NumBidders = view.GetInt32(417);
			instance.TradeDate = view.GetDateOnly(75);
			instance.TradeType = view.GetString(418);
			instance.BasisPxType = view.GetString(419);
			instance.StrikeTime = view.GetDateTime(443);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
