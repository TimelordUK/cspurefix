using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
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
			var groupViewNoSecurityAltID = view.GetView("NoSecurityAltID");
			if (groupViewNoSecurityAltID is null) return;
			
			var countNoSecurityAltID = groupViewNoSecurityAltID.GroupCount();
			instance.NoSecurityAltID = new InstrumentNoSecurityAltID[countNoSecurityAltID];
			for (var i = 0; i < countNoSecurityAltID; ++i)
			{
				instance.NoSecurityAltID[i] = new();
				instance.NoSecurityAltID[i].Parse(groupViewNoSecurityAltID[i]);
			}
			instance.Product = view.GetInt32(460);
			instance.CFICode = view.GetString(461);
			instance.SecurityType = view.GetString(167);
			instance.MaturityMonthYear = view.GetMonthYear(200);
			instance.MaturityDate = view.GetDateOnly(541);
			instance.CouponPaymentDate = view.GetString(224);
			instance.IssueDate = view.GetString(225);
			instance.RepoCollateralSecurityType = view.GetString(239);
			instance.RepurchaseTerm = view.GetInt32(226);
			instance.RepurchaseRate = view.GetDouble(227);
			instance.Factor = view.GetDouble(228);
			instance.CreditRating = view.GetString(255);
			instance.InstrRegistry = view.GetString(543);
			instance.CountryOfIssue = view.GetString(470);
			instance.StateOrProvinceOfIssue = view.GetString(471);
			instance.LocaleOfIssue = view.GetString(472);
			instance.RedemptionDate = view.GetString(240);
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
		}
	}
}
