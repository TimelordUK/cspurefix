using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class MarketDataIncrementalRefreshNoMDEntriesExt
	{
		public static void Parse(this MarketDataIncrementalRefreshNoMDEntries instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.MDUpdateAction = view.GetString(279);
			instance.DeleteReason = view.GetString(285);
			instance.MDEntryType = view.GetString(269);
			instance.MDEntryID = view.GetString(278);
			instance.MDEntryRefID = view.GetString(280);
			if (view.GetView("Instrument") is MsgView groupViewInstrument)
			{
				instance.Instrument = new Instrument();
				instance.Instrument!.Parse(groupViewInstrument);
			}
			instance.FinancialStatus = view.GetString(291);
			instance.CorporateAction = view.GetString(292);
			instance.MDEntryPx = view.GetDouble(270);
			instance.Currency = view.GetString(15);
			instance.MDEntrySize = view.GetDouble(271);
			instance.MDEntryDate = view.GetString(272);
			instance.MDEntryTime = view.GetTimeOnly(273);
			instance.TickDirection = view.GetString(274);
			instance.MDMkt = view.GetString(275);
			instance.TradingSessionID = view.GetString(336);
			instance.TradingSessionSubID = view.GetString(625);
			instance.QuoteCondition = view.GetString(276);
			instance.TradeCondition = view.GetString(277);
			instance.MDEntryOriginator = view.GetString(282);
			instance.LocationID = view.GetString(283);
			instance.DeskID = view.GetString(284);
			instance.OpenCloseSettleFlag = view.GetString(286);
			instance.TimeInForce = view.GetString(59);
			instance.ExpireDate = view.GetDateOnly(432);
			instance.ExpireTime = view.GetDateTime(126);
			instance.MinQty = view.GetDouble(110);
			instance.ExecInst = view.GetString(18);
			instance.SellerDays = view.GetInt32(287);
			instance.OrderID = view.GetString(37);
			instance.QuoteEntryID = view.GetString(299);
			instance.MDEntryBuyer = view.GetString(288);
			instance.MDEntrySeller = view.GetString(289);
			instance.NumberOfOrders = view.GetInt32(346);
			instance.MDEntryPositionNo = view.GetInt32(290);
			instance.Scope = view.GetString(546);
			instance.TotalVolumeTraded = view.GetDouble(387);
			instance.TotalVolumeTradedDate = view.GetString(449);
			instance.TotalVolumeTradedTime = view.GetTimeOnly(450);
			instance.NetChgPrevDay = view.GetDouble(451);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
		}
	}
}
