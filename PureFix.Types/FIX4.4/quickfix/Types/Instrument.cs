using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class Instrument : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 55, Type = TagType.String, Offset = 0, Required = false)]
		public string? Symbol { get; set; }
		
		[TagDetails(Tag = 65, Type = TagType.String, Offset = 1, Required = false)]
		public string? SymbolSfx { get; set; }
		
		[TagDetails(Tag = 48, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecurityID { get; set; }
		
		[TagDetails(Tag = 22, Type = TagType.String, Offset = 3, Required = false)]
		public string? SecurityIDSource { get; set; }
		
		[Component(Offset = 4, Required = false)]
		public SecAltIDGrp? SecAltIDGrp { get; set; }
		
		[TagDetails(Tag = 460, Type = TagType.Int, Offset = 5, Required = false)]
		public int? Product { get; set; }
		
		[TagDetails(Tag = 461, Type = TagType.String, Offset = 6, Required = false)]
		public string? CFICode { get; set; }
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 7, Required = false)]
		public string? SecurityType { get; set; }
		
		[TagDetails(Tag = 762, Type = TagType.String, Offset = 8, Required = false)]
		public string? SecuritySubType { get; set; }
		
		[TagDetails(Tag = 200, Type = TagType.MonthYear, Offset = 9, Required = false)]
		public MonthYear? MaturityMonthYear { get; set; }
		
		[TagDetails(Tag = 541, Type = TagType.LocalDate, Offset = 10, Required = false)]
		public DateOnly? MaturityDate { get; set; }
		
		[TagDetails(Tag = 201, Type = TagType.Int, Offset = 11, Required = false)]
		public int? PutOrCall { get; set; }
		
		[TagDetails(Tag = 224, Type = TagType.LocalDate, Offset = 12, Required = false)]
		public DateOnly? CouponPaymentDate { get; set; }
		
		[TagDetails(Tag = 225, Type = TagType.LocalDate, Offset = 13, Required = false)]
		public DateOnly? IssueDate { get; set; }
		
		[TagDetails(Tag = 239, Type = TagType.String, Offset = 14, Required = false)]
		public string? RepoCollateralSecurityType { get; set; }
		
		[TagDetails(Tag = 226, Type = TagType.Int, Offset = 15, Required = false)]
		public int? RepurchaseTerm { get; set; }
		
		[TagDetails(Tag = 227, Type = TagType.Float, Offset = 16, Required = false)]
		public double? RepurchaseRate { get; set; }
		
		[TagDetails(Tag = 228, Type = TagType.Float, Offset = 17, Required = false)]
		public double? Factor { get; set; }
		
		[TagDetails(Tag = 255, Type = TagType.String, Offset = 18, Required = false)]
		public string? CreditRating { get; set; }
		
		[TagDetails(Tag = 543, Type = TagType.String, Offset = 19, Required = false)]
		public string? InstrRegistry { get; set; }
		
		[TagDetails(Tag = 470, Type = TagType.String, Offset = 20, Required = false)]
		public string? CountryOfIssue { get; set; }
		
		[TagDetails(Tag = 471, Type = TagType.String, Offset = 21, Required = false)]
		public string? StateOrProvinceOfIssue { get; set; }
		
		[TagDetails(Tag = 472, Type = TagType.String, Offset = 22, Required = false)]
		public string? LocaleOfIssue { get; set; }
		
		[TagDetails(Tag = 240, Type = TagType.LocalDate, Offset = 23, Required = false)]
		public DateOnly? RedemptionDate { get; set; }
		
		[TagDetails(Tag = 202, Type = TagType.Float, Offset = 24, Required = false)]
		public double? StrikePrice { get; set; }
		
		[TagDetails(Tag = 947, Type = TagType.String, Offset = 25, Required = false)]
		public string? StrikeCurrency { get; set; }
		
		[TagDetails(Tag = 206, Type = TagType.String, Offset = 26, Required = false)]
		public string? OptAttribute { get; set; }
		
		[TagDetails(Tag = 231, Type = TagType.Float, Offset = 27, Required = false)]
		public double? ContractMultiplier { get; set; }
		
		[TagDetails(Tag = 223, Type = TagType.Float, Offset = 28, Required = false)]
		public double? CouponRate { get; set; }
		
		[TagDetails(Tag = 207, Type = TagType.String, Offset = 29, Required = false)]
		public string? SecurityExchange { get; set; }
		
		[TagDetails(Tag = 106, Type = TagType.String, Offset = 30, Required = false)]
		public string? Issuer { get; set; }
		
		[TagDetails(Tag = 348, Type = TagType.Length, Offset = 31, Required = false, LinksToTag = 349)]
		public int? EncodedIssuerLen { get; set; }
		
		[TagDetails(Tag = 349, Type = TagType.RawData, Offset = 32, Required = false, LinksToTag = 348)]
		public byte[]? EncodedIssuer { get; set; }
		
		[TagDetails(Tag = 107, Type = TagType.String, Offset = 33, Required = false)]
		public string? SecurityDesc { get; set; }
		
		[TagDetails(Tag = 350, Type = TagType.Length, Offset = 34, Required = false, LinksToTag = 351)]
		public int? EncodedSecurityDescLen { get; set; }
		
		[TagDetails(Tag = 351, Type = TagType.RawData, Offset = 35, Required = false, LinksToTag = 350)]
		public byte[]? EncodedSecurityDesc { get; set; }
		
		[TagDetails(Tag = 691, Type = TagType.String, Offset = 36, Required = false)]
		public string? Pool { get; set; }
		
		[TagDetails(Tag = 667, Type = TagType.MonthYear, Offset = 37, Required = false)]
		public MonthYear? ContractSettlMonth { get; set; }
		
		[TagDetails(Tag = 875, Type = TagType.Int, Offset = 38, Required = false)]
		public int? CPProgram { get; set; }
		
		[TagDetails(Tag = 876, Type = TagType.String, Offset = 39, Required = false)]
		public string? CPRegType { get; set; }
		
		[Component(Offset = 40, Required = false)]
		public EvntGrp? EvntGrp { get; set; }
		
		[TagDetails(Tag = 873, Type = TagType.LocalDate, Offset = 41, Required = false)]
		public DateOnly? DatedDate { get; set; }
		
		[TagDetails(Tag = 874, Type = TagType.LocalDate, Offset = 42, Required = false)]
		public DateOnly? InterestAccrualDate { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Symbol is not null) writer.WriteString(55, Symbol);
			if (SymbolSfx is not null) writer.WriteString(65, SymbolSfx);
			if (SecurityID is not null) writer.WriteString(48, SecurityID);
			if (SecurityIDSource is not null) writer.WriteString(22, SecurityIDSource);
			if (SecAltIDGrp is not null) ((IFixEncoder)SecAltIDGrp).Encode(writer);
			if (Product is not null) writer.WriteWholeNumber(460, Product.Value);
			if (CFICode is not null) writer.WriteString(461, CFICode);
			if (SecurityType is not null) writer.WriteString(167, SecurityType);
			if (SecuritySubType is not null) writer.WriteString(762, SecuritySubType);
			if (MaturityMonthYear is not null) writer.WriteMonthYear(200, MaturityMonthYear.Value);
			if (MaturityDate is not null) writer.WriteLocalDateOnly(541, MaturityDate.Value);
			if (PutOrCall is not null) writer.WriteWholeNumber(201, PutOrCall.Value);
			if (CouponPaymentDate is not null) writer.WriteLocalDateOnly(224, CouponPaymentDate.Value);
			if (IssueDate is not null) writer.WriteLocalDateOnly(225, IssueDate.Value);
			if (RepoCollateralSecurityType is not null) writer.WriteString(239, RepoCollateralSecurityType);
			if (RepurchaseTerm is not null) writer.WriteWholeNumber(226, RepurchaseTerm.Value);
			if (RepurchaseRate is not null) writer.WriteNumber(227, RepurchaseRate.Value);
			if (Factor is not null) writer.WriteNumber(228, Factor.Value);
			if (CreditRating is not null) writer.WriteString(255, CreditRating);
			if (InstrRegistry is not null) writer.WriteString(543, InstrRegistry);
			if (CountryOfIssue is not null) writer.WriteString(470, CountryOfIssue);
			if (StateOrProvinceOfIssue is not null) writer.WriteString(471, StateOrProvinceOfIssue);
			if (LocaleOfIssue is not null) writer.WriteString(472, LocaleOfIssue);
			if (RedemptionDate is not null) writer.WriteLocalDateOnly(240, RedemptionDate.Value);
			if (StrikePrice is not null) writer.WriteNumber(202, StrikePrice.Value);
			if (StrikeCurrency is not null) writer.WriteString(947, StrikeCurrency);
			if (OptAttribute is not null) writer.WriteString(206, OptAttribute);
			if (ContractMultiplier is not null) writer.WriteNumber(231, ContractMultiplier.Value);
			if (CouponRate is not null) writer.WriteNumber(223, CouponRate.Value);
			if (SecurityExchange is not null) writer.WriteString(207, SecurityExchange);
			if (Issuer is not null) writer.WriteString(106, Issuer);
			if (EncodedIssuer is not null)
			{
				writer.WriteWholeNumber(348, EncodedIssuer.Length);
				writer.WriteBuffer(349, EncodedIssuer);
			}
			if (SecurityDesc is not null) writer.WriteString(107, SecurityDesc);
			if (EncodedSecurityDesc is not null)
			{
				writer.WriteWholeNumber(350, EncodedSecurityDesc.Length);
				writer.WriteBuffer(351, EncodedSecurityDesc);
			}
			if (Pool is not null) writer.WriteString(691, Pool);
			if (ContractSettlMonth is not null) writer.WriteMonthYear(667, ContractSettlMonth.Value);
			if (CPProgram is not null) writer.WriteWholeNumber(875, CPProgram.Value);
			if (CPRegType is not null) writer.WriteString(876, CPRegType);
			if (EvntGrp is not null) ((IFixEncoder)EvntGrp).Encode(writer);
			if (DatedDate is not null) writer.WriteLocalDateOnly(873, DatedDate.Value);
			if (InterestAccrualDate is not null) writer.WriteLocalDateOnly(874, InterestAccrualDate.Value);
		}
	}
}
