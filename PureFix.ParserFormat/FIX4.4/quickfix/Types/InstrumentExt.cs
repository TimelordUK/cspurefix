using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class InstrumentExt
	{
		public static void Parse(this Instrument instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.Symbol = view.GetString(55);
			instance.SymbolSfx = view.GetString(65);
			instance.SecurityID = view.GetString(48);
			instance.SecurityIDSource = view.GetString(22);
			if (view.GetView("SecAltIDGrp") is MsgView groupViewSecAltIDGrp)
			{
				instance.SecAltIDGrp = new SecAltIDGrp();
				instance.SecAltIDGrp!.Parse(groupViewSecAltIDGrp);
			}
			instance.Product = view.GetInt32(460);
			instance.CFICode = view.GetString(461);
			instance.SecurityType = view.GetString(167);
			instance.SecuritySubType = view.GetString(762);
			instance.MaturityMonthYear = view.GetString(200);
			instance.MaturityDate = view.GetDateOnly(541);
			instance.PutOrCall = view.GetInt32(201);
			instance.CouponPaymentDate = view.GetDateOnly(224);
			instance.IssueDate = view.GetDateOnly(225);
			instance.RepoCollateralSecurityType = view.GetString(239);
			instance.RepurchaseTerm = view.GetInt32(226);
			instance.RepurchaseRate = view.GetDouble(227);
			instance.Factor = view.GetDouble(228);
			instance.CreditRating = view.GetString(255);
			instance.InstrRegistry = view.GetString(543);
			instance.CountryOfIssue = view.GetString(470);
			instance.StateOrProvinceOfIssue = view.GetString(471);
			instance.LocaleOfIssue = view.GetString(472);
			instance.RedemptionDate = view.GetDateOnly(240);
			instance.StrikePrice = view.GetDouble(202);
			instance.StrikeCurrency = view.GetString(947);
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
			instance.Pool = view.GetString(691);
			instance.ContractSettlMonth = view.GetString(667);
			instance.CPProgram = view.GetInt32(875);
			instance.CPRegType = view.GetString(876);
			if (view.GetView("EvntGrp") is MsgView groupViewEvntGrp)
			{
				instance.EvntGrp = new EvntGrp();
				instance.EvntGrp!.Parse(groupViewEvntGrp);
			}
			instance.DatedDate = view.GetDateOnly(873);
			instance.InterestAccrualDate = view.GetDateOnly(874);
		}
	}
}
