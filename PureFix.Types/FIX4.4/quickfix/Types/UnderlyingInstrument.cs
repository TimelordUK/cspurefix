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
		[TagDetails(Tag = 311, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingSymbol { get; set; }
		
		[TagDetails(Tag = 312, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingSymbolSfx { get; set; }
		
		[TagDetails(Tag = 309, Type = TagType.String, Offset = 2, Required = false)]
		public string? UnderlyingSecurityID { get; set; }
		
		[TagDetails(Tag = 305, Type = TagType.String, Offset = 3, Required = false)]
		public string? UnderlyingSecurityIDSource { get; set; }
		
		[Component(Offset = 4, Required = false)]
		public UndSecAltIDGrp? UndSecAltIDGrp { get; set; }
		
		[TagDetails(Tag = 462, Type = TagType.Int, Offset = 5, Required = false)]
		public int? UnderlyingProduct { get; set; }
		
		[TagDetails(Tag = 463, Type = TagType.String, Offset = 6, Required = false)]
		public string? UnderlyingCFICode { get; set; }
		
		[TagDetails(Tag = 310, Type = TagType.String, Offset = 7, Required = false)]
		public string? UnderlyingSecurityType { get; set; }
		
		[TagDetails(Tag = 763, Type = TagType.String, Offset = 8, Required = false)]
		public string? UnderlyingSecuritySubType { get; set; }
		
		[TagDetails(Tag = 313, Type = TagType.String, Offset = 9, Required = false)]
		public string? UnderlyingMaturityMonthYear { get; set; }
		
		[TagDetails(Tag = 542, Type = TagType.LocalDate, Offset = 10, Required = false)]
		public DateTime? UnderlyingMaturityDate { get; set; }
		
		[TagDetails(Tag = 315, Type = TagType.Int, Offset = 11, Required = false)]
		public int? UnderlyingPutOrCall { get; set; }
		
		[TagDetails(Tag = 241, Type = TagType.LocalDate, Offset = 12, Required = false)]
		public DateTime? UnderlyingCouponPaymentDate { get; set; }
		
		[TagDetails(Tag = 242, Type = TagType.LocalDate, Offset = 13, Required = false)]
		public DateTime? UnderlyingIssueDate { get; set; }
		
		[TagDetails(Tag = 243, Type = TagType.String, Offset = 14, Required = false)]
		public string? UnderlyingRepoCollateralSecurityType { get; set; }
		
		[TagDetails(Tag = 244, Type = TagType.Int, Offset = 15, Required = false)]
		public int? UnderlyingRepurchaseTerm { get; set; }
		
		[TagDetails(Tag = 245, Type = TagType.Float, Offset = 16, Required = false)]
		public double? UnderlyingRepurchaseRate { get; set; }
		
		[TagDetails(Tag = 246, Type = TagType.Float, Offset = 17, Required = false)]
		public double? UnderlyingFactor { get; set; }
		
		[TagDetails(Tag = 256, Type = TagType.String, Offset = 18, Required = false)]
		public string? UnderlyingCreditRating { get; set; }
		
		[TagDetails(Tag = 595, Type = TagType.String, Offset = 19, Required = false)]
		public string? UnderlyingInstrRegistry { get; set; }
		
		[TagDetails(Tag = 592, Type = TagType.String, Offset = 20, Required = false)]
		public string? UnderlyingCountryOfIssue { get; set; }
		
		[TagDetails(Tag = 593, Type = TagType.String, Offset = 21, Required = false)]
		public string? UnderlyingStateOrProvinceOfIssue { get; set; }
		
		[TagDetails(Tag = 594, Type = TagType.String, Offset = 22, Required = false)]
		public string? UnderlyingLocaleOfIssue { get; set; }
		
		[TagDetails(Tag = 247, Type = TagType.LocalDate, Offset = 23, Required = false)]
		public DateTime? UnderlyingRedemptionDate { get; set; }
		
		[TagDetails(Tag = 316, Type = TagType.Float, Offset = 24, Required = false)]
		public double? UnderlyingStrikePrice { get; set; }
		
		[TagDetails(Tag = 941, Type = TagType.String, Offset = 25, Required = false)]
		public string? UnderlyingStrikeCurrency { get; set; }
		
		[TagDetails(Tag = 317, Type = TagType.String, Offset = 26, Required = false)]
		public string? UnderlyingOptAttribute { get; set; }
		
		[TagDetails(Tag = 436, Type = TagType.Float, Offset = 27, Required = false)]
		public double? UnderlyingContractMultiplier { get; set; }
		
		[TagDetails(Tag = 435, Type = TagType.Float, Offset = 28, Required = false)]
		public double? UnderlyingCouponRate { get; set; }
		
		[TagDetails(Tag = 308, Type = TagType.String, Offset = 29, Required = false)]
		public string? UnderlyingSecurityExchange { get; set; }
		
		[TagDetails(Tag = 306, Type = TagType.String, Offset = 30, Required = false)]
		public string? UnderlyingIssuer { get; set; }
		
		[TagDetails(Tag = 362, Type = TagType.Length, Offset = 31, Required = false, LinksToTag = 363)]
		public int? EncodedUnderlyingIssuerLen { get; set; }
		
		[TagDetails(Tag = 363, Type = TagType.RawData, Offset = 32, Required = false, LinksToTag = 362)]
		public byte[]? EncodedUnderlyingIssuer { get; set; }
		
		[TagDetails(Tag = 307, Type = TagType.String, Offset = 33, Required = false)]
		public string? UnderlyingSecurityDesc { get; set; }
		
		[TagDetails(Tag = 364, Type = TagType.Length, Offset = 34, Required = false, LinksToTag = 365)]
		public int? EncodedUnderlyingSecurityDescLen { get; set; }
		
		[TagDetails(Tag = 365, Type = TagType.RawData, Offset = 35, Required = false, LinksToTag = 364)]
		public byte[]? EncodedUnderlyingSecurityDesc { get; set; }
		
		[TagDetails(Tag = 877, Type = TagType.String, Offset = 36, Required = false)]
		public string? UnderlyingCPProgram { get; set; }
		
		[TagDetails(Tag = 878, Type = TagType.String, Offset = 37, Required = false)]
		public string? UnderlyingCPRegType { get; set; }
		
		[TagDetails(Tag = 318, Type = TagType.String, Offset = 38, Required = false)]
		public string? UnderlyingCurrency { get; set; }
		
		[TagDetails(Tag = 879, Type = TagType.Float, Offset = 39, Required = false)]
		public double? UnderlyingQty { get; set; }
		
		[TagDetails(Tag = 810, Type = TagType.Float, Offset = 40, Required = false)]
		public double? UnderlyingPx { get; set; }
		
		[TagDetails(Tag = 882, Type = TagType.Float, Offset = 41, Required = false)]
		public double? UnderlyingDirtyPrice { get; set; }
		
		[TagDetails(Tag = 883, Type = TagType.Float, Offset = 42, Required = false)]
		public double? UnderlyingEndPrice { get; set; }
		
		[TagDetails(Tag = 884, Type = TagType.Float, Offset = 43, Required = false)]
		public double? UnderlyingStartValue { get; set; }
		
		[TagDetails(Tag = 885, Type = TagType.Float, Offset = 44, Required = false)]
		public double? UnderlyingCurrentValue { get; set; }
		
		[TagDetails(Tag = 886, Type = TagType.Float, Offset = 45, Required = false)]
		public double? UnderlyingEndValue { get; set; }
		
		[Component(Offset = 46, Required = false)]
		public UnderlyingStipulations? UnderlyingStipulations { get; set; }
		
	}
}
