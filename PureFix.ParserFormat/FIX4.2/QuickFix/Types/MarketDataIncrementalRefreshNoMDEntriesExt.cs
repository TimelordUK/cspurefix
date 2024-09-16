using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix.Types
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
			instance.Symbol = view.GetString(55);
			instance.SymbolSfx = view.GetString(65);
			instance.SecurityID = view.GetString(48);
			instance.IDSource = view.GetString(22);
			instance.SecurityType = view.GetString(167);
			instance.MaturityMonthYear = view.GetMonthYear(200);
			instance.MaturityDay = view.GetString(205);
			instance.PutOrCall = view.GetInt32(201);
			instance.StrikePrice = view.GetDouble(202);
			instance.OptAttribute = view.GetString(206);
			instance.ContractMultiplier = view.GetDouble(231);
			instance.CouponRate = view.GetDouble(223);
			instance.SecurityExchange = view.GetString(207);
			instance.Issuer = view.GetString(106);
			instance.EncodedIssuerLen = view.GetInt32(348);
			instance.EncodedIssuer = view.GetByteArray(349);
			instance.SecurityDesc = view.GetString(107);
			instance.EncodedSecurityDescLen = view.GetInt32(350);
			instance.EncodedSecurityDesc = view.GetByteArray(351);
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
			instance.TotalVolumeTraded = view.GetDouble(387);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
		}
	}
}
