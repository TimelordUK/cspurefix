using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public static class SecurityDefinitionRequestNoRelatedSymExt
	{
		public static void Parse(this SecurityDefinitionRequestNoRelatedSym instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.UnderlyingSymbol = view.GetString(311);
			instance.UnderlyingSymbolSfx = view.GetString(312);
			instance.UnderlyingSecurityID = view.GetString(309);
			instance.UnderlyingIDSource = view.GetString(305);
			instance.UnderlyingSecurityType = view.GetString(310);
			instance.UnderlyingMaturityMonthYear = view.GetMonthYear(313);
			instance.UnderlyingMaturityDay = view.GetString(314);
			instance.UnderlyingPutOrCall = view.GetInt32(315);
			instance.UnderlyingStrikePrice = view.GetDouble(316);
			instance.UnderlyingOptAttribute = view.GetString(317);
			instance.UnderlyingContractMultiplier = view.GetDouble(436);
			instance.UnderlyingCouponRate = view.GetDouble(435);
			instance.UnderlyingSecurityExchange = view.GetString(308);
			instance.UnderlyingIssuer = view.GetString(306);
			instance.EncodedUnderlyingIssuerLen = view.GetInt32(362);
			instance.EncodedUnderlyingIssuer = view.GetByteArray(363);
			instance.UnderlyingSecurityDesc = view.GetString(307);
			instance.EncodedUnderlyingSecurityDescLen = view.GetInt32(364);
			instance.EncodedUnderlyingSecurityDesc = view.GetByteArray(365);
			instance.RatioQty = view.GetDouble(319);
			instance.Side = view.GetString(54);
			instance.UnderlyingCurrency = view.GetString(318);
		}
	}
}
