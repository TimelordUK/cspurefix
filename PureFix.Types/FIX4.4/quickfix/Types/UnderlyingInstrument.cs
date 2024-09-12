using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class UnderlyingInstrument
	{
		public string? UnderlyingSymbol { get; set; } // 311 STRING
		public string? UnderlyingSymbolSfx { get; set; } // 312 STRING
		public string? UnderlyingSecurityID { get; set; } // 309 STRING
		public string? UnderlyingSecurityIDSource { get; set; } // 305 STRING
		public UndSecAltIDGrp? UndSecAltIDGrp { get; set; }
		public int? UnderlyingProduct { get; set; } // 462 INT
		public string? UnderlyingCFICode { get; set; } // 463 STRING
		public string? UnderlyingSecurityType { get; set; } // 310 STRING
		public string? UnderlyingSecuritySubType { get; set; } // 763 STRING
		public string? UnderlyingMaturityMonthYear { get; set; } // 313 MONTHYEAR
		public DateTime? UnderlyingMaturityDate { get; set; } // 542 LOCALMKTDATE
		public int? UnderlyingPutOrCall { get; set; } // 315 INT
		public DateTime? UnderlyingCouponPaymentDate { get; set; } // 241 LOCALMKTDATE
		public DateTime? UnderlyingIssueDate { get; set; } // 242 LOCALMKTDATE
		public string? UnderlyingRepoCollateralSecurityType { get; set; } // 243 STRING
		public int? UnderlyingRepurchaseTerm { get; set; } // 244 INT
		public double? UnderlyingRepurchaseRate { get; set; } // 245 PERCENTAGE
		public double? UnderlyingFactor { get; set; } // 246 FLOAT
		public string? UnderlyingCreditRating { get; set; } // 256 STRING
		public string? UnderlyingInstrRegistry { get; set; } // 595 STRING
		public string? UnderlyingCountryOfIssue { get; set; } // 592 COUNTRY
		public string? UnderlyingStateOrProvinceOfIssue { get; set; } // 593 STRING
		public string? UnderlyingLocaleOfIssue { get; set; } // 594 STRING
		public DateTime? UnderlyingRedemptionDate { get; set; } // 247 LOCALMKTDATE
		public double? UnderlyingStrikePrice { get; set; } // 316 PRICE
		public string? UnderlyingStrikeCurrency { get; set; } // 941 CURRENCY
		public string? UnderlyingOptAttribute { get; set; } // 317 CHAR
		public double? UnderlyingContractMultiplier { get; set; } // 436 FLOAT
		public double? UnderlyingCouponRate { get; set; } // 435 PERCENTAGE
		public string? UnderlyingSecurityExchange { get; set; } // 308 EXCHANGE
		public string? UnderlyingIssuer { get; set; } // 306 STRING
		public int? EncodedUnderlyingIssuerLen { get; set; } // 362 LENGTH
		public byte[]? EncodedUnderlyingIssuer { get; set; } // 363 DATA
		public string? UnderlyingSecurityDesc { get; set; } // 307 STRING
		public int? EncodedUnderlyingSecurityDescLen { get; set; } // 364 LENGTH
		public byte[]? EncodedUnderlyingSecurityDesc { get; set; } // 365 DATA
		public string? UnderlyingCPProgram { get; set; } // 877 STRING
		public string? UnderlyingCPRegType { get; set; } // 878 STRING
		public string? UnderlyingCurrency { get; set; } // 318 CURRENCY
		public double? UnderlyingQty { get; set; } // 879 QTY
		public double? UnderlyingPx { get; set; } // 810 PRICE
		public double? UnderlyingDirtyPrice { get; set; } // 882 PRICE
		public double? UnderlyingEndPrice { get; set; } // 883 PRICE
		public double? UnderlyingStartValue { get; set; } // 884 AMT
		public double? UnderlyingCurrentValue { get; set; } // 885 AMT
		public double? UnderlyingEndValue { get; set; } // 886 AMT
		public UnderlyingStipulations? UnderlyingStipulations { get; set; }
	}
}
