using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
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
			if (view.GetView("UndSecAltIDGrp") is MsgView groupViewUndSecAltIDGrp)
			{
				instance.UndSecAltIDGrp = new UndSecAltIDGrp();
				instance.UndSecAltIDGrp!.Parse(groupViewUndSecAltIDGrp);
			}
			instance.UnderlyingProduct = view.GetInt32(462);
			instance.UnderlyingCFICode = view.GetString(463);
			instance.UnderlyingSecurityType = view.GetString(310);
			instance.UnderlyingSecuritySubType = view.GetString(763);
			instance.UnderlyingMaturityMonthYear = view.GetMonthYear(313);
			instance.UnderlyingMaturityDate = view.GetDateOnly(542);
			instance.UnderlyingPutOrCall = view.GetInt32(315);
			instance.UnderlyingCouponPaymentDate = view.GetDateOnly(241);
			instance.UnderlyingIssueDate = view.GetDateOnly(242);
			instance.UnderlyingRepoCollateralSecurityType = view.GetString(243);
			instance.UnderlyingRepurchaseTerm = view.GetInt32(244);
			instance.UnderlyingRepurchaseRate = view.GetDouble(245);
			instance.UnderlyingFactor = view.GetDouble(246);
			instance.UnderlyingCreditRating = view.GetString(256);
			instance.UnderlyingInstrRegistry = view.GetString(595);
			instance.UnderlyingCountryOfIssue = view.GetString(592);
			instance.UnderlyingStateOrProvinceOfIssue = view.GetString(593);
			instance.UnderlyingLocaleOfIssue = view.GetString(594);
			instance.UnderlyingRedemptionDate = view.GetDateOnly(247);
			instance.UnderlyingStrikePrice = view.GetDouble(316);
			instance.UnderlyingStrikeCurrency = view.GetString(941);
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
			instance.UnderlyingCPProgram = view.GetString(877);
			instance.UnderlyingCPRegType = view.GetString(878);
			instance.UnderlyingCurrency = view.GetString(318);
			instance.UnderlyingQty = view.GetDouble(879);
			instance.UnderlyingPx = view.GetDouble(810);
			instance.UnderlyingDirtyPrice = view.GetDouble(882);
			instance.UnderlyingEndPrice = view.GetDouble(883);
			instance.UnderlyingStartValue = view.GetDouble(884);
			instance.UnderlyingCurrentValue = view.GetDouble(885);
			instance.UnderlyingEndValue = view.GetDouble(886);
			if (view.GetView("UnderlyingStipulations") is MsgView groupViewUnderlyingStipulations)
			{
				instance.UnderlyingStipulations = new UnderlyingStipulations();
				instance.UnderlyingStipulations!.Parse(groupViewUnderlyingStipulations);
			}
		}
	}
}
