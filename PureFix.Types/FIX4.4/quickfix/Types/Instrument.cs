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
		public string? Symbol { get; set; } // 55 STRING
		public string? SymbolSfx { get; set; } // 65 STRING
		public string? SecurityID { get; set; } // 48 STRING
		public string? SecurityIDSource { get; set; } // 22 STRING
		public SecAltIDGrp? SecAltIDGrp { get; set; }
		public int? Product { get; set; } // 460 INT
		public string? CFICode { get; set; } // 461 STRING
		public string? SecurityType { get; set; } // 167 STRING
		public string? SecuritySubType { get; set; } // 762 STRING
		public string? MaturityMonthYear { get; set; } // 200 MONTHYEAR
		public DateTime? MaturityDate { get; set; } // 541 LOCALMKTDATE
		public int? PutOrCall { get; set; } // 201 INT
		public DateTime? CouponPaymentDate { get; set; } // 224 LOCALMKTDATE
		public DateTime? IssueDate { get; set; } // 225 LOCALMKTDATE
		public string? RepoCollateralSecurityType { get; set; } // 239 STRING
		public int? RepurchaseTerm { get; set; } // 226 INT
		public double? RepurchaseRate { get; set; } // 227 PERCENTAGE
		public double? Factor { get; set; } // 228 FLOAT
		public string? CreditRating { get; set; } // 255 STRING
		public string? InstrRegistry { get; set; } // 543 STRING
		public string? CountryOfIssue { get; set; } // 470 COUNTRY
		public string? StateOrProvinceOfIssue { get; set; } // 471 STRING
		public string? LocaleOfIssue { get; set; } // 472 STRING
		public DateTime? RedemptionDate { get; set; } // 240 LOCALMKTDATE
		public double? StrikePrice { get; set; } // 202 PRICE
		public string? StrikeCurrency { get; set; } // 947 CURRENCY
		public string? OptAttribute { get; set; } // 206 CHAR
		public double? ContractMultiplier { get; set; } // 231 FLOAT
		public double? CouponRate { get; set; } // 223 PERCENTAGE
		public string? SecurityExchange { get; set; } // 207 EXCHANGE
		public string? Issuer { get; set; } // 106 STRING
		public int? EncodedIssuerLen { get; set; } // 348 LENGTH
		public byte[]? EncodedIssuer { get; set; } // 349 DATA
		public string? SecurityDesc { get; set; } // 107 STRING
		public int? EncodedSecurityDescLen { get; set; } // 350 LENGTH
		public byte[]? EncodedSecurityDesc { get; set; } // 351 DATA
		public string? Pool { get; set; } // 691 STRING
		public string? ContractSettlMonth { get; set; } // 667 MONTHYEAR
		public int? CPProgram { get; set; } // 875 INT
		public string? CPRegType { get; set; } // 876 STRING
		public EvntGrp? EvntGrp { get; set; }
		public DateTime? DatedDate { get; set; } // 873 LOCALMKTDATE
		public DateTime? InterestAccrualDate { get; set; } // 874 LOCALMKTDATE
	}
}
