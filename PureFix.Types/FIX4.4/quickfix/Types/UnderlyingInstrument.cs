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
		[TagDetails(311)]
		public string? UnderlyingSymbol { get; set; } // STRING
		
		[TagDetails(312)]
		public string? UnderlyingSymbolSfx { get; set; } // STRING
		
		[TagDetails(309)]
		public string? UnderlyingSecurityID { get; set; } // STRING
		
		[TagDetails(305)]
		public string? UnderlyingSecurityIDSource { get; set; } // STRING
		
		public UndSecAltIDGrp? UndSecAltIDGrp { get; set; }
		[TagDetails(462)]
		public int? UnderlyingProduct { get; set; } // INT
		
		[TagDetails(463)]
		public string? UnderlyingCFICode { get; set; } // STRING
		
		[TagDetails(310)]
		public string? UnderlyingSecurityType { get; set; } // STRING
		
		[TagDetails(763)]
		public string? UnderlyingSecuritySubType { get; set; } // STRING
		
		[TagDetails(313)]
		public string? UnderlyingMaturityMonthYear { get; set; } // MONTHYEAR
		
		[TagDetails(542)]
		public DateTime? UnderlyingMaturityDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(315)]
		public int? UnderlyingPutOrCall { get; set; } // INT
		
		[TagDetails(241)]
		public DateTime? UnderlyingCouponPaymentDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(242)]
		public DateTime? UnderlyingIssueDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(243)]
		public string? UnderlyingRepoCollateralSecurityType { get; set; } // STRING
		
		[TagDetails(244)]
		public int? UnderlyingRepurchaseTerm { get; set; } // INT
		
		[TagDetails(245)]
		public double? UnderlyingRepurchaseRate { get; set; } // PERCENTAGE
		
		[TagDetails(246)]
		public double? UnderlyingFactor { get; set; } // FLOAT
		
		[TagDetails(256)]
		public string? UnderlyingCreditRating { get; set; } // STRING
		
		[TagDetails(595)]
		public string? UnderlyingInstrRegistry { get; set; } // STRING
		
		[TagDetails(592)]
		public string? UnderlyingCountryOfIssue { get; set; } // COUNTRY
		
		[TagDetails(593)]
		public string? UnderlyingStateOrProvinceOfIssue { get; set; } // STRING
		
		[TagDetails(594)]
		public string? UnderlyingLocaleOfIssue { get; set; } // STRING
		
		[TagDetails(247)]
		public DateTime? UnderlyingRedemptionDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(316)]
		public double? UnderlyingStrikePrice { get; set; } // PRICE
		
		[TagDetails(941)]
		public string? UnderlyingStrikeCurrency { get; set; } // CURRENCY
		
		[TagDetails(317)]
		public string? UnderlyingOptAttribute { get; set; } // CHAR
		
		[TagDetails(436)]
		public double? UnderlyingContractMultiplier { get; set; } // FLOAT
		
		[TagDetails(435)]
		public double? UnderlyingCouponRate { get; set; } // PERCENTAGE
		
		[TagDetails(308)]
		public string? UnderlyingSecurityExchange { get; set; } // EXCHANGE
		
		[TagDetails(306)]
		public string? UnderlyingIssuer { get; set; } // STRING
		
		[TagDetails(362)]
		public int? EncodedUnderlyingIssuerLen { get; set; } // LENGTH
		
		[TagDetails(363)]
		public byte[]? EncodedUnderlyingIssuer { get; set; } // DATA
		
		[TagDetails(307)]
		public string? UnderlyingSecurityDesc { get; set; } // STRING
		
		[TagDetails(364)]
		public int? EncodedUnderlyingSecurityDescLen { get; set; } // LENGTH
		
		[TagDetails(365)]
		public byte[]? EncodedUnderlyingSecurityDesc { get; set; } // DATA
		
		[TagDetails(877)]
		public string? UnderlyingCPProgram { get; set; } // STRING
		
		[TagDetails(878)]
		public string? UnderlyingCPRegType { get; set; } // STRING
		
		[TagDetails(318)]
		public string? UnderlyingCurrency { get; set; } // CURRENCY
		
		[TagDetails(879)]
		public double? UnderlyingQty { get; set; } // QTY
		
		[TagDetails(810)]
		public double? UnderlyingPx { get; set; } // PRICE
		
		[TagDetails(882)]
		public double? UnderlyingDirtyPrice { get; set; } // PRICE
		
		[TagDetails(883)]
		public double? UnderlyingEndPrice { get; set; } // PRICE
		
		[TagDetails(884)]
		public double? UnderlyingStartValue { get; set; } // AMT
		
		[TagDetails(885)]
		public double? UnderlyingCurrentValue { get; set; } // AMT
		
		[TagDetails(886)]
		public double? UnderlyingEndValue { get; set; } // AMT
		
		public UnderlyingStipulations? UnderlyingStipulations { get; set; }
	}
}
