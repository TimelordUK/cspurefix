using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class InstrumentLeg
	{
		[TagDetails(600, TagType.String)]
		public string? LegSymbol { get; set; }
		
		[TagDetails(601, TagType.String)]
		public string? LegSymbolSfx { get; set; }
		
		[TagDetails(602, TagType.String)]
		public string? LegSecurityID { get; set; }
		
		[TagDetails(603, TagType.String)]
		public string? LegSecurityIDSource { get; set; }
		
		[Component]
		public LegSecAltIDGrp? LegSecAltIDGrp { get; set; }
		
		[TagDetails(607, TagType.Int)]
		public int? LegProduct { get; set; }
		
		[TagDetails(608, TagType.String)]
		public string? LegCFICode { get; set; }
		
		[TagDetails(609, TagType.String)]
		public string? LegSecurityType { get; set; }
		
		[TagDetails(764, TagType.String)]
		public string? LegSecuritySubType { get; set; }
		
		[TagDetails(610, TagType.String)]
		public string? LegMaturityMonthYear { get; set; }
		
		[TagDetails(611, TagType.LocalDate)]
		public DateTime? LegMaturityDate { get; set; }
		
		[TagDetails(248, TagType.LocalDate)]
		public DateTime? LegCouponPaymentDate { get; set; }
		
		[TagDetails(249, TagType.LocalDate)]
		public DateTime? LegIssueDate { get; set; }
		
		[TagDetails(250, TagType.String)]
		public string? LegRepoCollateralSecurityType { get; set; }
		
		[TagDetails(251, TagType.Int)]
		public int? LegRepurchaseTerm { get; set; }
		
		[TagDetails(252, TagType.Float)]
		public double? LegRepurchaseRate { get; set; }
		
		[TagDetails(253, TagType.Float)]
		public double? LegFactor { get; set; }
		
		[TagDetails(257, TagType.String)]
		public string? LegCreditRating { get; set; }
		
		[TagDetails(599, TagType.String)]
		public string? LegInstrRegistry { get; set; }
		
		[TagDetails(596, TagType.String)]
		public string? LegCountryOfIssue { get; set; }
		
		[TagDetails(597, TagType.String)]
		public string? LegStateOrProvinceOfIssue { get; set; }
		
		[TagDetails(598, TagType.String)]
		public string? LegLocaleOfIssue { get; set; }
		
		[TagDetails(254, TagType.LocalDate)]
		public DateTime? LegRedemptionDate { get; set; }
		
		[TagDetails(612, TagType.Float)]
		public double? LegStrikePrice { get; set; }
		
		[TagDetails(942, TagType.String)]
		public string? LegStrikeCurrency { get; set; }
		
		[TagDetails(613, TagType.String)]
		public string? LegOptAttribute { get; set; }
		
		[TagDetails(614, TagType.Float)]
		public double? LegContractMultiplier { get; set; }
		
		[TagDetails(615, TagType.Float)]
		public double? LegCouponRate { get; set; }
		
		[TagDetails(616, TagType.String)]
		public string? LegSecurityExchange { get; set; }
		
		[TagDetails(617, TagType.String)]
		public string? LegIssuer { get; set; }
		
		[TagDetails(618, TagType.Length)]
		public int? EncodedLegIssuerLen { get; set; }
		
		[TagDetails(619, TagType.RawData)]
		public byte[]? EncodedLegIssuer { get; set; }
		
		[TagDetails(620, TagType.String)]
		public string? LegSecurityDesc { get; set; }
		
		[TagDetails(621, TagType.Length)]
		public int? EncodedLegSecurityDescLen { get; set; }
		
		[TagDetails(622, TagType.RawData)]
		public byte[]? EncodedLegSecurityDesc { get; set; }
		
		[TagDetails(623, TagType.Float)]
		public double? LegRatioQty { get; set; }
		
		[TagDetails(624, TagType.String)]
		public string? LegSide { get; set; }
		
		[TagDetails(556, TagType.String)]
		public string? LegCurrency { get; set; }
		
		[TagDetails(740, TagType.String)]
		public string? LegPool { get; set; }
		
		[TagDetails(739, TagType.LocalDate)]
		public DateTime? LegDatedDate { get; set; }
		
		[TagDetails(955, TagType.String)]
		public string? LegContractSettlMonth { get; set; }
		
		[TagDetails(956, TagType.LocalDate)]
		public DateTime? LegInterestAccrualDate { get; set; }
		
	}
}
