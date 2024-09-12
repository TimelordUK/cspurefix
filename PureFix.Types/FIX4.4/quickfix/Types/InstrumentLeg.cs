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
		[TagDetails(Tag = 600, Type = TagType.String, Offset = 0)]
		public string? LegSymbol { get; set; }
		
		[TagDetails(Tag = 601, Type = TagType.String, Offset = 1)]
		public string? LegSymbolSfx { get; set; }
		
		[TagDetails(Tag = 602, Type = TagType.String, Offset = 2)]
		public string? LegSecurityID { get; set; }
		
		[TagDetails(Tag = 603, Type = TagType.String, Offset = 3)]
		public string? LegSecurityIDSource { get; set; }
		
		[Component(Offset = 4)]
		public LegSecAltIDGrp? LegSecAltIDGrp { get; set; }
		
		[TagDetails(Tag = 607, Type = TagType.Int, Offset = 5)]
		public int? LegProduct { get; set; }
		
		[TagDetails(Tag = 608, Type = TagType.String, Offset = 6)]
		public string? LegCFICode { get; set; }
		
		[TagDetails(Tag = 609, Type = TagType.String, Offset = 7)]
		public string? LegSecurityType { get; set; }
		
		[TagDetails(Tag = 764, Type = TagType.String, Offset = 8)]
		public string? LegSecuritySubType { get; set; }
		
		[TagDetails(Tag = 610, Type = TagType.String, Offset = 9)]
		public string? LegMaturityMonthYear { get; set; }
		
		[TagDetails(Tag = 611, Type = TagType.LocalDate, Offset = 10)]
		public DateTime? LegMaturityDate { get; set; }
		
		[TagDetails(Tag = 248, Type = TagType.LocalDate, Offset = 11)]
		public DateTime? LegCouponPaymentDate { get; set; }
		
		[TagDetails(Tag = 249, Type = TagType.LocalDate, Offset = 12)]
		public DateTime? LegIssueDate { get; set; }
		
		[TagDetails(Tag = 250, Type = TagType.String, Offset = 13)]
		public string? LegRepoCollateralSecurityType { get; set; }
		
		[TagDetails(Tag = 251, Type = TagType.Int, Offset = 14)]
		public int? LegRepurchaseTerm { get; set; }
		
		[TagDetails(Tag = 252, Type = TagType.Float, Offset = 15)]
		public double? LegRepurchaseRate { get; set; }
		
		[TagDetails(Tag = 253, Type = TagType.Float, Offset = 16)]
		public double? LegFactor { get; set; }
		
		[TagDetails(Tag = 257, Type = TagType.String, Offset = 17)]
		public string? LegCreditRating { get; set; }
		
		[TagDetails(Tag = 599, Type = TagType.String, Offset = 18)]
		public string? LegInstrRegistry { get; set; }
		
		[TagDetails(Tag = 596, Type = TagType.String, Offset = 19)]
		public string? LegCountryOfIssue { get; set; }
		
		[TagDetails(Tag = 597, Type = TagType.String, Offset = 20)]
		public string? LegStateOrProvinceOfIssue { get; set; }
		
		[TagDetails(Tag = 598, Type = TagType.String, Offset = 21)]
		public string? LegLocaleOfIssue { get; set; }
		
		[TagDetails(Tag = 254, Type = TagType.LocalDate, Offset = 22)]
		public DateTime? LegRedemptionDate { get; set; }
		
		[TagDetails(Tag = 612, Type = TagType.Float, Offset = 23)]
		public double? LegStrikePrice { get; set; }
		
		[TagDetails(Tag = 942, Type = TagType.String, Offset = 24)]
		public string? LegStrikeCurrency { get; set; }
		
		[TagDetails(Tag = 613, Type = TagType.String, Offset = 25)]
		public string? LegOptAttribute { get; set; }
		
		[TagDetails(Tag = 614, Type = TagType.Float, Offset = 26)]
		public double? LegContractMultiplier { get; set; }
		
		[TagDetails(Tag = 615, Type = TagType.Float, Offset = 27)]
		public double? LegCouponRate { get; set; }
		
		[TagDetails(Tag = 616, Type = TagType.String, Offset = 28)]
		public string? LegSecurityExchange { get; set; }
		
		[TagDetails(Tag = 617, Type = TagType.String, Offset = 29)]
		public string? LegIssuer { get; set; }
		
		[TagDetails(Tag = 618, Type = TagType.Length, Offset = 30)]
		public int? EncodedLegIssuerLen { get; set; }
		
		[TagDetails(Tag = 619, Type = TagType.RawData, Offset = 31)]
		public byte[]? EncodedLegIssuer { get; set; }
		
		[TagDetails(Tag = 620, Type = TagType.String, Offset = 32)]
		public string? LegSecurityDesc { get; set; }
		
		[TagDetails(Tag = 621, Type = TagType.Length, Offset = 33)]
		public int? EncodedLegSecurityDescLen { get; set; }
		
		[TagDetails(Tag = 622, Type = TagType.RawData, Offset = 34)]
		public byte[]? EncodedLegSecurityDesc { get; set; }
		
		[TagDetails(Tag = 623, Type = TagType.Float, Offset = 35)]
		public double? LegRatioQty { get; set; }
		
		[TagDetails(Tag = 624, Type = TagType.String, Offset = 36)]
		public string? LegSide { get; set; }
		
		[TagDetails(Tag = 556, Type = TagType.String, Offset = 37)]
		public string? LegCurrency { get; set; }
		
		[TagDetails(Tag = 740, Type = TagType.String, Offset = 38)]
		public string? LegPool { get; set; }
		
		[TagDetails(Tag = 739, Type = TagType.LocalDate, Offset = 39)]
		public DateTime? LegDatedDate { get; set; }
		
		[TagDetails(Tag = 955, Type = TagType.String, Offset = 40)]
		public string? LegContractSettlMonth { get; set; }
		
		[TagDetails(Tag = 956, Type = TagType.LocalDate, Offset = 41)]
		public DateTime? LegInterestAccrualDate { get; set; }
		
	}
}
