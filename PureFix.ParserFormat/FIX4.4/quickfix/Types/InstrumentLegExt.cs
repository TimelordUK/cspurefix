using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class InstrumentLegExt
	{
		public static void Parse(this InstrumentLeg instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.LegSymbol = view.GetString(600);
			instance.LegSymbolSfx = view.GetString(601);
			instance.LegSecurityID = view.GetString(602);
			instance.LegSecurityIDSource = view.GetString(603);
			if (view.GetView("LegSecAltIDGrp") is MsgView groupViewLegSecAltIDGrp)
			{
				instance.LegSecAltIDGrp = new LegSecAltIDGrp();
				instance.LegSecAltIDGrp!.Parse(groupViewLegSecAltIDGrp);
			}
			instance.LegProduct = view.GetInt32(607);
			instance.LegCFICode = view.GetString(608);
			instance.LegSecurityType = view.GetString(609);
			instance.LegSecuritySubType = view.GetString(764);
			instance.LegMaturityMonthYear = view.GetString(610);
			instance.LegMaturityDate = view.GetDateOnly(611);
			instance.LegCouponPaymentDate = view.GetDateOnly(248);
			instance.LegIssueDate = view.GetDateOnly(249);
			instance.LegRepoCollateralSecurityType = view.GetString(250);
			instance.LegRepurchaseTerm = view.GetInt32(251);
			instance.LegRepurchaseRate = view.GetDouble(252);
			instance.LegFactor = view.GetDouble(253);
			instance.LegCreditRating = view.GetString(257);
			instance.LegInstrRegistry = view.GetString(599);
			instance.LegCountryOfIssue = view.GetString(596);
			instance.LegStateOrProvinceOfIssue = view.GetString(597);
			instance.LegLocaleOfIssue = view.GetString(598);
			instance.LegRedemptionDate = view.GetDateOnly(254);
			instance.LegStrikePrice = view.GetDouble(612);
			instance.LegStrikeCurrency = view.GetString(942);
			instance.LegOptAttribute = view.GetString(613);
			instance.LegContractMultiplier = view.GetDouble(614);
			instance.LegCouponRate = view.GetDouble(615);
			instance.LegSecurityExchange = view.GetString(616);
			instance.LegIssuer = view.GetString(617);
			instance.EncodedLegIssuerLen = view.GetInt32(618);
			instance.EncodedLegIssuer = view.GetByteArray(619);
			instance.LegSecurityDesc = view.GetString(620);
			instance.EncodedLegSecurityDescLen = view.GetInt32(621);
			instance.EncodedLegSecurityDesc = view.GetByteArray(622);
			instance.LegRatioQty = view.GetDouble(623);
			instance.LegSide = view.GetString(624);
			instance.LegCurrency = view.GetString(556);
			instance.LegPool = view.GetString(740);
			instance.LegDatedDate = view.GetDateOnly(739);
			instance.LegContractSettlMonth = view.GetString(955);
			instance.LegInterestAccrualDate = view.GetDateOnly(956);
		}
	}
}
