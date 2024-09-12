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
		[TagDetails(600)]
		public string? LegSymbol { get; set; } // STRING
		
		[TagDetails(601)]
		public string? LegSymbolSfx { get; set; } // STRING
		
		[TagDetails(602)]
		public string? LegSecurityID { get; set; } // STRING
		
		[TagDetails(603)]
		public string? LegSecurityIDSource { get; set; } // STRING
		
		public LegSecAltIDGrp? LegSecAltIDGrp { get; set; }
		[TagDetails(607)]
		public int? LegProduct { get; set; } // INT
		
		[TagDetails(608)]
		public string? LegCFICode { get; set; } // STRING
		
		[TagDetails(609)]
		public string? LegSecurityType { get; set; } // STRING
		
		[TagDetails(764)]
		public string? LegSecuritySubType { get; set; } // STRING
		
		[TagDetails(610)]
		public string? LegMaturityMonthYear { get; set; } // MONTHYEAR
		
		[TagDetails(611)]
		public DateTime? LegMaturityDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(248)]
		public DateTime? LegCouponPaymentDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(249)]
		public DateTime? LegIssueDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(250)]
		public string? LegRepoCollateralSecurityType { get; set; } // STRING
		
		[TagDetails(251)]
		public int? LegRepurchaseTerm { get; set; } // INT
		
		[TagDetails(252)]
		public double? LegRepurchaseRate { get; set; } // PERCENTAGE
		
		[TagDetails(253)]
		public double? LegFactor { get; set; } // FLOAT
		
		[TagDetails(257)]
		public string? LegCreditRating { get; set; } // STRING
		
		[TagDetails(599)]
		public string? LegInstrRegistry { get; set; } // STRING
		
		[TagDetails(596)]
		public string? LegCountryOfIssue { get; set; } // COUNTRY
		
		[TagDetails(597)]
		public string? LegStateOrProvinceOfIssue { get; set; } // STRING
		
		[TagDetails(598)]
		public string? LegLocaleOfIssue { get; set; } // STRING
		
		[TagDetails(254)]
		public DateTime? LegRedemptionDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(612)]
		public double? LegStrikePrice { get; set; } // PRICE
		
		[TagDetails(942)]
		public string? LegStrikeCurrency { get; set; } // CURRENCY
		
		[TagDetails(613)]
		public string? LegOptAttribute { get; set; } // CHAR
		
		[TagDetails(614)]
		public double? LegContractMultiplier { get; set; } // FLOAT
		
		[TagDetails(615)]
		public double? LegCouponRate { get; set; } // PERCENTAGE
		
		[TagDetails(616)]
		public string? LegSecurityExchange { get; set; } // EXCHANGE
		
		[TagDetails(617)]
		public string? LegIssuer { get; set; } // STRING
		
		[TagDetails(618)]
		public int? EncodedLegIssuerLen { get; set; } // LENGTH
		
		[TagDetails(619)]
		public byte[]? EncodedLegIssuer { get; set; } // DATA
		
		[TagDetails(620)]
		public string? LegSecurityDesc { get; set; } // STRING
		
		[TagDetails(621)]
		public int? EncodedLegSecurityDescLen { get; set; } // LENGTH
		
		[TagDetails(622)]
		public byte[]? EncodedLegSecurityDesc { get; set; } // DATA
		
		[TagDetails(623)]
		public double? LegRatioQty { get; set; } // FLOAT
		
		[TagDetails(624)]
		public string? LegSide { get; set; } // CHAR
		
		[TagDetails(556)]
		public string? LegCurrency { get; set; } // CURRENCY
		
		[TagDetails(740)]
		public string? LegPool { get; set; } // STRING
		
		[TagDetails(739)]
		public DateTime? LegDatedDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(955)]
		public string? LegContractSettlMonth { get; set; } // MONTHYEAR
		
		[TagDetails(956)]
		public DateTime? LegInterestAccrualDate { get; set; } // LOCALMKTDATE
		
	}
}
