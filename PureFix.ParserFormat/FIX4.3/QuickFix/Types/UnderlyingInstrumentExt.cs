using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class UnderlyingInstrumentExt
	{
		public static void Parse(this UnderlyingInstrument instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.UnderlyingSymbol = view.GetString(311);
			instance.UnderlyingSymbolSfx = view.GetString(312);
			instance.UnderlyingSecurityID = view.GetString(309);
			instance.UnderlyingSecurityIDSource = view.GetString(305);
			var groupViewNoUnderlyingSecurityAltID = view.GetView("NoUnderlyingSecurityAltID");
			if (groupViewNoUnderlyingSecurityAltID is null) return;
			
			var countNoUnderlyingSecurityAltID = groupViewNoUnderlyingSecurityAltID.GroupCount();
			instance.NoUnderlyingSecurityAltID = new UnderlyingInstrumentNoUnderlyingSecurityAltID[countNoUnderlyingSecurityAltID];
			for (var i = 0; i < countNoUnderlyingSecurityAltID; ++i)
			{
				instance.NoUnderlyingSecurityAltID[i] = new();
				instance.NoUnderlyingSecurityAltID[i].Parse(groupViewNoUnderlyingSecurityAltID[i]);
			}
			instance.UnderlyingProduct = view.GetInt32(462);
			instance.UnderlyingCFICode = view.GetString(463);
			instance.UnderlyingSecurityType = view.GetString(310);
			instance.UnderlyingMaturityMonthYear = view.GetMonthYear(313);
			instance.UnderlyingMaturityDate = view.GetDateOnly(542);
			instance.UnderlyingPutOrCall = view.GetInt32(315);
			instance.UnderlyingCouponPaymentDate = view.GetString(241);
			instance.UnderlyingIssueDate = view.GetString(242);
			instance.UnderlyingRepoCollateralSecurityType = view.GetString(243);
			instance.UnderlyingRepurchaseTerm = view.GetInt32(244);
			instance.UnderlyingRepurchaseRate = view.GetDouble(245);
			instance.UnderlyingFactor = view.GetDouble(246);
			instance.UnderlyingCreditRating = view.GetString(256);
			instance.UnderlyingInstrRegistry = view.GetString(595);
			instance.UnderlyingCountryOfIssue = view.GetString(592);
			instance.UnderlyingStateOrProvinceOfIssue = view.GetString(593);
			instance.UnderlyingLocaleOfIssue = view.GetString(594);
			instance.UnderlyingRedemptionDate = view.GetString(247);
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
		}
	}
}
