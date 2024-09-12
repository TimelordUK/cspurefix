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
		[TagDetails(311, TagType.String)]
		public string? UnderlyingSymbol { get; set; }
		
		[TagDetails(312, TagType.String)]
		public string? UnderlyingSymbolSfx { get; set; }
		
		[TagDetails(309, TagType.String)]
		public string? UnderlyingSecurityID { get; set; }
		
		[TagDetails(305, TagType.String)]
		public string? UnderlyingSecurityIDSource { get; set; }
		
		[Component]
		public UndSecAltIDGrp? UndSecAltIDGrp { get; set; }
		
		[TagDetails(462, TagType.Int)]
		public int? UnderlyingProduct { get; set; }
		
		[TagDetails(463, TagType.String)]
		public string? UnderlyingCFICode { get; set; }
		
		[TagDetails(310, TagType.String)]
		public string? UnderlyingSecurityType { get; set; }
		
		[TagDetails(763, TagType.String)]
		public string? UnderlyingSecuritySubType { get; set; }
		
		[TagDetails(313, TagType.String)]
		public string? UnderlyingMaturityMonthYear { get; set; }
		
		[TagDetails(542, TagType.LocalDate)]
		public DateTime? UnderlyingMaturityDate { get; set; }
		
		[TagDetails(315, TagType.Int)]
		public int? UnderlyingPutOrCall { get; set; }
		
		[TagDetails(241, TagType.LocalDate)]
		public DateTime? UnderlyingCouponPaymentDate { get; set; }
		
		[TagDetails(242, TagType.LocalDate)]
		public DateTime? UnderlyingIssueDate { get; set; }
		
		[TagDetails(243, TagType.String)]
		public string? UnderlyingRepoCollateralSecurityType { get; set; }
		
		[TagDetails(244, TagType.Int)]
		public int? UnderlyingRepurchaseTerm { get; set; }
		
		[TagDetails(245, TagType.Float)]
		public double? UnderlyingRepurchaseRate { get; set; }
		
		[TagDetails(246, TagType.Float)]
		public double? UnderlyingFactor { get; set; }
		
		[TagDetails(256, TagType.String)]
		public string? UnderlyingCreditRating { get; set; }
		
		[TagDetails(595, TagType.String)]
		public string? UnderlyingInstrRegistry { get; set; }
		
		[TagDetails(592, TagType.String)]
		public string? UnderlyingCountryOfIssue { get; set; }
		
		[TagDetails(593, TagType.String)]
		public string? UnderlyingStateOrProvinceOfIssue { get; set; }
		
		[TagDetails(594, TagType.String)]
		public string? UnderlyingLocaleOfIssue { get; set; }
		
		[TagDetails(247, TagType.LocalDate)]
		public DateTime? UnderlyingRedemptionDate { get; set; }
		
		[TagDetails(316, TagType.Float)]
		public double? UnderlyingStrikePrice { get; set; }
		
		[TagDetails(941, TagType.String)]
		public string? UnderlyingStrikeCurrency { get; set; }
		
		[TagDetails(317, TagType.String)]
		public string? UnderlyingOptAttribute { get; set; }
		
		[TagDetails(436, TagType.Float)]
		public double? UnderlyingContractMultiplier { get; set; }
		
		[TagDetails(435, TagType.Float)]
		public double? UnderlyingCouponRate { get; set; }
		
		[TagDetails(308, TagType.String)]
		public string? UnderlyingSecurityExchange { get; set; }
		
		[TagDetails(306, TagType.String)]
		public string? UnderlyingIssuer { get; set; }
		
		[TagDetails(362, TagType.Length)]
		public int? EncodedUnderlyingIssuerLen { get; set; }
		
		[TagDetails(363, TagType.RawData)]
		public byte[]? EncodedUnderlyingIssuer { get; set; }
		
		[TagDetails(307, TagType.String)]
		public string? UnderlyingSecurityDesc { get; set; }
		
		[TagDetails(364, TagType.Length)]
		public int? EncodedUnderlyingSecurityDescLen { get; set; }
		
		[TagDetails(365, TagType.RawData)]
		public byte[]? EncodedUnderlyingSecurityDesc { get; set; }
		
		[TagDetails(877, TagType.String)]
		public string? UnderlyingCPProgram { get; set; }
		
		[TagDetails(878, TagType.String)]
		public string? UnderlyingCPRegType { get; set; }
		
		[TagDetails(318, TagType.String)]
		public string? UnderlyingCurrency { get; set; }
		
		[TagDetails(879, TagType.Float)]
		public double? UnderlyingQty { get; set; }
		
		[TagDetails(810, TagType.Float)]
		public double? UnderlyingPx { get; set; }
		
		[TagDetails(882, TagType.Float)]
		public double? UnderlyingDirtyPrice { get; set; }
		
		[TagDetails(883, TagType.Float)]
		public double? UnderlyingEndPrice { get; set; }
		
		[TagDetails(884, TagType.Float)]
		public double? UnderlyingStartValue { get; set; }
		
		[TagDetails(885, TagType.Float)]
		public double? UnderlyingCurrentValue { get; set; }
		
		[TagDetails(886, TagType.Float)]
		public double? UnderlyingEndValue { get; set; }
		
		[Component]
		public UnderlyingStipulations? UnderlyingStipulations { get; set; }
		
	}
}
