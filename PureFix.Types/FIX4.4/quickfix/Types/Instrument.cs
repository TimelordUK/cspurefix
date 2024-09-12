using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class Instrument
	{
		[TagDetails(55)]
		public string? Symbol { get; set; } // STRING
		
		[TagDetails(65)]
		public string? SymbolSfx { get; set; } // STRING
		
		[TagDetails(48)]
		public string? SecurityID { get; set; } // STRING
		
		[TagDetails(22)]
		public string? SecurityIDSource { get; set; } // STRING
		
		public SecAltIDGrp? SecAltIDGrp { get; set; }
		[TagDetails(460)]
		public int? Product { get; set; } // INT
		
		[TagDetails(461)]
		public string? CFICode { get; set; } // STRING
		
		[TagDetails(167)]
		public string? SecurityType { get; set; } // STRING
		
		[TagDetails(762)]
		public string? SecuritySubType { get; set; } // STRING
		
		[TagDetails(200)]
		public string? MaturityMonthYear { get; set; } // MONTHYEAR
		
		[TagDetails(541)]
		public DateTime? MaturityDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(201)]
		public int? PutOrCall { get; set; } // INT
		
		[TagDetails(224)]
		public DateTime? CouponPaymentDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(225)]
		public DateTime? IssueDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(239)]
		public string? RepoCollateralSecurityType { get; set; } // STRING
		
		[TagDetails(226)]
		public int? RepurchaseTerm { get; set; } // INT
		
		[TagDetails(227)]
		public double? RepurchaseRate { get; set; } // PERCENTAGE
		
		[TagDetails(228)]
		public double? Factor { get; set; } // FLOAT
		
		[TagDetails(255)]
		public string? CreditRating { get; set; } // STRING
		
		[TagDetails(543)]
		public string? InstrRegistry { get; set; } // STRING
		
		[TagDetails(470)]
		public string? CountryOfIssue { get; set; } // COUNTRY
		
		[TagDetails(471)]
		public string? StateOrProvinceOfIssue { get; set; } // STRING
		
		[TagDetails(472)]
		public string? LocaleOfIssue { get; set; } // STRING
		
		[TagDetails(240)]
		public DateTime? RedemptionDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(202)]
		public double? StrikePrice { get; set; } // PRICE
		
		[TagDetails(947)]
		public string? StrikeCurrency { get; set; } // CURRENCY
		
		[TagDetails(206)]
		public string? OptAttribute { get; set; } // CHAR
		
		[TagDetails(231)]
		public double? ContractMultiplier { get; set; } // FLOAT
		
		[TagDetails(223)]
		public double? CouponRate { get; set; } // PERCENTAGE
		
		[TagDetails(207)]
		public string? SecurityExchange { get; set; } // EXCHANGE
		
		[TagDetails(106)]
		public string? Issuer { get; set; } // STRING
		
		[TagDetails(348)]
		public int? EncodedIssuerLen { get; set; } // LENGTH
		
		[TagDetails(349)]
		public byte[]? EncodedIssuer { get; set; } // DATA
		
		[TagDetails(107)]
		public string? SecurityDesc { get; set; } // STRING
		
		[TagDetails(350)]
		public int? EncodedSecurityDescLen { get; set; } // LENGTH
		
		[TagDetails(351)]
		public byte[]? EncodedSecurityDesc { get; set; } // DATA
		
		[TagDetails(691)]
		public string? Pool { get; set; } // STRING
		
		[TagDetails(667)]
		public string? ContractSettlMonth { get; set; } // MONTHYEAR
		
		[TagDetails(875)]
		public int? CPProgram { get; set; } // INT
		
		[TagDetails(876)]
		public string? CPRegType { get; set; } // STRING
		
		public EvntGrp? EvntGrp { get; set; }
		[TagDetails(873)]
		public DateTime? DatedDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(874)]
		public DateTime? InterestAccrualDate { get; set; } // LOCALMKTDATE
		
	}
}
