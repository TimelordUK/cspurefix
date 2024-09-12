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
		[TagDetails(55, TagType.String)]
		public string? Symbol { get; set; }
		
		[TagDetails(65, TagType.String)]
		public string? SymbolSfx { get; set; }
		
		[TagDetails(48, TagType.String)]
		public string? SecurityID { get; set; }
		
		[TagDetails(22, TagType.String)]
		public string? SecurityIDSource { get; set; }
		
		[Component]
		public SecAltIDGrp? SecAltIDGrp { get; set; }
		
		[TagDetails(460, TagType.Int)]
		public int? Product { get; set; }
		
		[TagDetails(461, TagType.String)]
		public string? CFICode { get; set; }
		
		[TagDetails(167, TagType.String)]
		public string? SecurityType { get; set; }
		
		[TagDetails(762, TagType.String)]
		public string? SecuritySubType { get; set; }
		
		[TagDetails(200, TagType.String)]
		public string? MaturityMonthYear { get; set; }
		
		[TagDetails(541, TagType.LocalDate)]
		public DateTime? MaturityDate { get; set; }
		
		[TagDetails(201, TagType.Int)]
		public int? PutOrCall { get; set; }
		
		[TagDetails(224, TagType.LocalDate)]
		public DateTime? CouponPaymentDate { get; set; }
		
		[TagDetails(225, TagType.LocalDate)]
		public DateTime? IssueDate { get; set; }
		
		[TagDetails(239, TagType.String)]
		public string? RepoCollateralSecurityType { get; set; }
		
		[TagDetails(226, TagType.Int)]
		public int? RepurchaseTerm { get; set; }
		
		[TagDetails(227, TagType.Float)]
		public double? RepurchaseRate { get; set; }
		
		[TagDetails(228, TagType.Float)]
		public double? Factor { get; set; }
		
		[TagDetails(255, TagType.String)]
		public string? CreditRating { get; set; }
		
		[TagDetails(543, TagType.String)]
		public string? InstrRegistry { get; set; }
		
		[TagDetails(470, TagType.String)]
		public string? CountryOfIssue { get; set; }
		
		[TagDetails(471, TagType.String)]
		public string? StateOrProvinceOfIssue { get; set; }
		
		[TagDetails(472, TagType.String)]
		public string? LocaleOfIssue { get; set; }
		
		[TagDetails(240, TagType.LocalDate)]
		public DateTime? RedemptionDate { get; set; }
		
		[TagDetails(202, TagType.Float)]
		public double? StrikePrice { get; set; }
		
		[TagDetails(947, TagType.String)]
		public string? StrikeCurrency { get; set; }
		
		[TagDetails(206, TagType.String)]
		public string? OptAttribute { get; set; }
		
		[TagDetails(231, TagType.Float)]
		public double? ContractMultiplier { get; set; }
		
		[TagDetails(223, TagType.Float)]
		public double? CouponRate { get; set; }
		
		[TagDetails(207, TagType.String)]
		public string? SecurityExchange { get; set; }
		
		[TagDetails(106, TagType.String)]
		public string? Issuer { get; set; }
		
		[TagDetails(348, TagType.Length)]
		public int? EncodedIssuerLen { get; set; }
		
		[TagDetails(349, TagType.RawData)]
		public byte[]? EncodedIssuer { get; set; }
		
		[TagDetails(107, TagType.String)]
		public string? SecurityDesc { get; set; }
		
		[TagDetails(350, TagType.Length)]
		public int? EncodedSecurityDescLen { get; set; }
		
		[TagDetails(351, TagType.RawData)]
		public byte[]? EncodedSecurityDesc { get; set; }
		
		[TagDetails(691, TagType.String)]
		public string? Pool { get; set; }
		
		[TagDetails(667, TagType.String)]
		public string? ContractSettlMonth { get; set; }
		
		[TagDetails(875, TagType.Int)]
		public int? CPProgram { get; set; }
		
		[TagDetails(876, TagType.String)]
		public string? CPRegType { get; set; }
		
		[Component]
		public EvntGrp? EvntGrp { get; set; }
		
		[TagDetails(873, TagType.LocalDate)]
		public DateTime? DatedDate { get; set; }
		
		[TagDetails(874, TagType.LocalDate)]
		public DateTime? InterestAccrualDate { get; set; }
		
	}
}
