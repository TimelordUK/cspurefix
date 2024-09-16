using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("d", FixVersion.FIX42)]
	public static class SecurityDefinitionExt
	{
		public static void Parse(this SecurityDefinition instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.SecurityReqID = view.GetString(320);
			instance.SecurityResponseID = view.GetString(322);
			instance.SecurityResponseType = view.GetInt32(323);
			instance.TotalNumSecurities = view.GetInt32(393);
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
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			var groupViewNoRelatedSym = view.GetView("NoRelatedSym");
			if (groupViewNoRelatedSym is null) return;
			
			var countNoRelatedSym = groupViewNoRelatedSym.GroupCount();
			instance.NoRelatedSym = new SecurityDefinitionNoRelatedSym[countNoRelatedSym];
			for (var i = 0; i < countNoRelatedSym; ++i)
			{
				instance.NoRelatedSym[i] = new();
				instance.NoRelatedSym[i].Parse(groupViewNoRelatedSym[i]);
			}
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
