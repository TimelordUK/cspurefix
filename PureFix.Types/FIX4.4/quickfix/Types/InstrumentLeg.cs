using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public class InstrumentLeg
	{
		public string? LegSymbol { get; set; } // 600 STRING
		public string? LegSymbolSfx { get; set; } // 601 STRING
		public string? LegSecurityID { get; set; } // 602 STRING
		public string? LegSecurityIDSource { get; set; } // 603 STRING
		public LegSecAltIDGrp? LegSecAltIDGrp { get; set; }
		public int? LegProduct { get; set; } // 607 INT
		public string? LegCFICode { get; set; } // 608 STRING
		public string? LegSecurityType { get; set; } // 609 STRING
		public string? LegSecuritySubType { get; set; } // 764 STRING
		public string? LegMaturityMonthYear { get; set; } // 610 MONTHYEAR
		public DateTime? LegMaturityDate { get; set; } // 611 LOCALMKTDATE
		public DateTime? LegCouponPaymentDate { get; set; } // 248 LOCALMKTDATE
		public DateTime? LegIssueDate { get; set; } // 249 LOCALMKTDATE
		public string? LegRepoCollateralSecurityType { get; set; } // 250 STRING
		public int? LegRepurchaseTerm { get; set; } // 251 INT
		public double? LegRepurchaseRate { get; set; } // 252 PERCENTAGE
		public double? LegFactor { get; set; } // 253 FLOAT
		public string? LegCreditRating { get; set; } // 257 STRING
		public string? LegInstrRegistry { get; set; } // 599 STRING
		public string? LegCountryOfIssue { get; set; } // 596 COUNTRY
		public string? LegStateOrProvinceOfIssue { get; set; } // 597 STRING
		public string? LegLocaleOfIssue { get; set; } // 598 STRING
		public DateTime? LegRedemptionDate { get; set; } // 254 LOCALMKTDATE
		public double? LegStrikePrice { get; set; } // 612 PRICE
		public string? LegStrikeCurrency { get; set; } // 942 CURRENCY
		public string? LegOptAttribute { get; set; } // 613 CHAR
		public double? LegContractMultiplier { get; set; } // 614 FLOAT
		public double? LegCouponRate { get; set; } // 615 PERCENTAGE
		public string? LegSecurityExchange { get; set; } // 616 EXCHANGE
		public string? LegIssuer { get; set; } // 617 STRING
		public int? EncodedLegIssuerLen { get; set; } // 618 LENGTH
		public byte[]? EncodedLegIssuer { get; set; } // 619 DATA
		public string? LegSecurityDesc { get; set; } // 620 STRING
		public int? EncodedLegSecurityDescLen { get; set; } // 621 LENGTH
		public byte[]? EncodedLegSecurityDesc { get; set; } // 622 DATA
		public double? LegRatioQty { get; set; } // 623 FLOAT
		public string? LegSide { get; set; } // 624 CHAR
		public string? LegCurrency { get; set; } // 556 CURRENCY
		public string? LegPool { get; set; } // 740 STRING
		public DateTime? LegDatedDate { get; set; } // 739 LOCALMKTDATE
		public string? LegContractSettlMonth { get; set; } // 955 MONTHYEAR
		public DateTime? LegInterestAccrualDate { get; set; } // 956 LOCALMKTDATE
	}
}
