using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public static class QuoteRequestNoRelatedSymExt
	{
		public static void Parse(this QuoteRequestNoRelatedSym instance, MsgView? view)
		{
			if (view is null) return;
			
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
			instance.PrevClosePx = view.GetDouble(140);
			instance.QuoteRequestType = view.GetInt32(303);
			instance.TradingSessionID = view.GetString(336);
			instance.Side = view.GetString(54);
			instance.OrderQty = view.GetDouble(38);
			instance.FutSettDate = view.GetDateOnly(64);
			instance.OrdType = view.GetString(40);
			instance.FutSettDate2 = view.GetDateOnly(193);
			instance.OrderQty2 = view.GetDouble(192);
			instance.ExpireTime = view.GetDateTime(126);
			instance.TransactTime = view.GetDateTime(60);
			instance.Currency = view.GetString(15);
		}
	}
}
