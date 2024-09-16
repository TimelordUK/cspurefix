using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("f", FixVersion.FIX42)]
	public static class SecurityStatusExt
	{
		public static void Parse(this SecurityStatus instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.SecurityStatusReqID = view.GetString(324);
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
			instance.Currency = view.GetString(15);
			instance.TradingSessionID = view.GetString(336);
			instance.UnsolicitedIndicator = view.GetBool(325);
			instance.SecurityTradingStatus = view.GetInt32(326);
			instance.FinancialStatus = view.GetString(291);
			instance.CorporateAction = view.GetString(292);
			instance.HaltReasonChar = view.GetString(327);
			instance.InViewOfCommon = view.GetBool(328);
			instance.DueToRelated = view.GetBool(329);
			instance.BuyVolume = view.GetDouble(330);
			instance.SellVolume = view.GetDouble(331);
			instance.HighPx = view.GetDouble(332);
			instance.LowPx = view.GetDouble(333);
			instance.LastPx = view.GetDouble(31);
			instance.TransactTime = view.GetDateTime(60);
			instance.Adjustment = view.GetInt32(334);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
