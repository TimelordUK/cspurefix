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
		[TagDetails(Tag = 311, Type = TagType.String, Offset = 0)]
		public string? UnderlyingSymbol { get; set; }
		
		[TagDetails(Tag = 312, Type = TagType.String, Offset = 1)]
		public string? UnderlyingSymbolSfx { get; set; }
		
		[TagDetails(Tag = 309, Type = TagType.String, Offset = 2)]
		public string? UnderlyingSecurityID { get; set; }
		
		[TagDetails(Tag = 305, Type = TagType.String, Offset = 3)]
		public string? UnderlyingSecurityIDSource { get; set; }
		
		[Component(Offset = 4)]
		public UndSecAltIDGrp? UndSecAltIDGrp { get; set; }
		
		[TagDetails(Tag = 462, Type = TagType.Int, Offset = 5)]
		public int? UnderlyingProduct { get; set; }
		
		[TagDetails(Tag = 463, Type = TagType.String, Offset = 6)]
		public string? UnderlyingCFICode { get; set; }
		
		[TagDetails(Tag = 310, Type = TagType.String, Offset = 7)]
		public string? UnderlyingSecurityType { get; set; }
		
		[TagDetails(Tag = 763, Type = TagType.String, Offset = 8)]
		public string? UnderlyingSecuritySubType { get; set; }
		
		[TagDetails(Tag = 313, Type = TagType.String, Offset = 9)]
		public string? UnderlyingMaturityMonthYear { get; set; }
		
		[TagDetails(Tag = 542, Type = TagType.LocalDate, Offset = 10)]
		public DateTime? UnderlyingMaturityDate { get; set; }
		
		[TagDetails(Tag = 315, Type = TagType.Int, Offset = 11)]
		public int? UnderlyingPutOrCall { get; set; }
		
		[TagDetails(Tag = 241, Type = TagType.LocalDate, Offset = 12)]
		public DateTime? UnderlyingCouponPaymentDate { get; set; }
		
		[TagDetails(Tag = 242, Type = TagType.LocalDate, Offset = 13)]
		public DateTime? UnderlyingIssueDate { get; set; }
		
		[TagDetails(Tag = 243, Type = TagType.String, Offset = 14)]
		public string? UnderlyingRepoCollateralSecurityType { get; set; }
		
		[TagDetails(Tag = 244, Type = TagType.Int, Offset = 15)]
		public int? UnderlyingRepurchaseTerm { get; set; }
		
		[TagDetails(Tag = 245, Type = TagType.Float, Offset = 16)]
		public double? UnderlyingRepurchaseRate { get; set; }
		
		[TagDetails(Tag = 246, Type = TagType.Float, Offset = 17)]
		public double? UnderlyingFactor { get; set; }
		
		[TagDetails(Tag = 256, Type = TagType.String, Offset = 18)]
		public string? UnderlyingCreditRating { get; set; }
		
		[TagDetails(Tag = 595, Type = TagType.String, Offset = 19)]
		public string? UnderlyingInstrRegistry { get; set; }
		
		[TagDetails(Tag = 592, Type = TagType.String, Offset = 20)]
		public string? UnderlyingCountryOfIssue { get; set; }
		
		[TagDetails(Tag = 593, Type = TagType.String, Offset = 21)]
		public string? UnderlyingStateOrProvinceOfIssue { get; set; }
		
		[TagDetails(Tag = 594, Type = TagType.String, Offset = 22)]
		public string? UnderlyingLocaleOfIssue { get; set; }
		
		[TagDetails(Tag = 247, Type = TagType.LocalDate, Offset = 23)]
		public DateTime? UnderlyingRedemptionDate { get; set; }
		
		[TagDetails(Tag = 316, Type = TagType.Float, Offset = 24)]
		public double? UnderlyingStrikePrice { get; set; }
		
		[TagDetails(Tag = 941, Type = TagType.String, Offset = 25)]
		public string? UnderlyingStrikeCurrency { get; set; }
		
		[TagDetails(Tag = 317, Type = TagType.String, Offset = 26)]
		public string? UnderlyingOptAttribute { get; set; }
		
		[TagDetails(Tag = 436, Type = TagType.Float, Offset = 27)]
		public double? UnderlyingContractMultiplier { get; set; }
		
		[TagDetails(Tag = 435, Type = TagType.Float, Offset = 28)]
		public double? UnderlyingCouponRate { get; set; }
		
		[TagDetails(Tag = 308, Type = TagType.String, Offset = 29)]
		public string? UnderlyingSecurityExchange { get; set; }
		
		[TagDetails(Tag = 306, Type = TagType.String, Offset = 30)]
		public string? UnderlyingIssuer { get; set; }
		
		[TagDetails(Tag = 362, Type = TagType.Length, Offset = 31)]
		public int? EncodedUnderlyingIssuerLen { get; set; }
		
		[TagDetails(Tag = 363, Type = TagType.RawData, Offset = 32)]
		public byte[]? EncodedUnderlyingIssuer { get; set; }
		
		[TagDetails(Tag = 307, Type = TagType.String, Offset = 33)]
		public string? UnderlyingSecurityDesc { get; set; }
		
		[TagDetails(Tag = 364, Type = TagType.Length, Offset = 34)]
		public int? EncodedUnderlyingSecurityDescLen { get; set; }
		
		[TagDetails(Tag = 365, Type = TagType.RawData, Offset = 35)]
		public byte[]? EncodedUnderlyingSecurityDesc { get; set; }
		
		[TagDetails(Tag = 877, Type = TagType.String, Offset = 36)]
		public string? UnderlyingCPProgram { get; set; }
		
		[TagDetails(Tag = 878, Type = TagType.String, Offset = 37)]
		public string? UnderlyingCPRegType { get; set; }
		
		[TagDetails(Tag = 318, Type = TagType.String, Offset = 38)]
		public string? UnderlyingCurrency { get; set; }
		
		[TagDetails(Tag = 879, Type = TagType.Float, Offset = 39)]
		public double? UnderlyingQty { get; set; }
		
		[TagDetails(Tag = 810, Type = TagType.Float, Offset = 40)]
		public double? UnderlyingPx { get; set; }
		
		[TagDetails(Tag = 882, Type = TagType.Float, Offset = 41)]
		public double? UnderlyingDirtyPrice { get; set; }
		
		[TagDetails(Tag = 883, Type = TagType.Float, Offset = 42)]
		public double? UnderlyingEndPrice { get; set; }
		
		[TagDetails(Tag = 884, Type = TagType.Float, Offset = 43)]
		public double? UnderlyingStartValue { get; set; }
		
		[TagDetails(Tag = 885, Type = TagType.Float, Offset = 44)]
		public double? UnderlyingCurrentValue { get; set; }
		
		[TagDetails(Tag = 886, Type = TagType.Float, Offset = 45)]
		public double? UnderlyingEndValue { get; set; }
		
		[Component(Offset = 46)]
		public UnderlyingStipulations? UnderlyingStipulations { get; set; }
		
	}
}
