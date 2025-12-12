using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44UnitTest.Components;

namespace PureFix.Types.FIX44UnitTest.Components
{
	public sealed partial class Instrument : IFixComponent
	{
		[TagDetails(Tag = 55, Type = TagType.String, Offset = 0, Required = false)]
		public string? Symbol {get; set;}
		
		[TagDetails(Tag = 65, Type = TagType.String, Offset = 1, Required = false)]
		public string? SymbolSfx {get; set;}
		
		[TagDetails(Tag = 48, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecurityID {get; set;}
		
		[TagDetails(Tag = 22, Type = TagType.String, Offset = 3, Required = false)]
		public string? SecurityIDSource {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public SecAltIDGrp? SecAltIDGrp {get; set;}
		
		[TagDetails(Tag = 460, Type = TagType.Int, Offset = 5, Required = false)]
		public int? Product {get; set;}
		
		[TagDetails(Tag = 461, Type = TagType.String, Offset = 6, Required = false)]
		public string? CFICode {get; set;}
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 7, Required = false)]
		public string? SecurityType {get; set;}
		
		[TagDetails(Tag = 762, Type = TagType.String, Offset = 8, Required = false)]
		public string? SecuritySubType {get; set;}
		
		[TagDetails(Tag = 200, Type = TagType.MonthYear, Offset = 9, Required = false)]
		public MonthYear? MaturityMonthYear {get; set;}
		
		[TagDetails(Tag = 541, Type = TagType.LocalDate, Offset = 10, Required = false)]
		public DateOnly? MaturityDate {get; set;}
		
		[TagDetails(Tag = 201, Type = TagType.Int, Offset = 11, Required = false)]
		public int? PutOrCall {get; set;}
		
		[TagDetails(Tag = 224, Type = TagType.LocalDate, Offset = 12, Required = false)]
		public DateOnly? CouponPaymentDate {get; set;}
		
		[TagDetails(Tag = 225, Type = TagType.LocalDate, Offset = 13, Required = false)]
		public DateOnly? IssueDate {get; set;}
		
		[TagDetails(Tag = 239, Type = TagType.String, Offset = 14, Required = false)]
		public string? RepoCollateralSecurityType {get; set;}
		
		[TagDetails(Tag = 226, Type = TagType.Int, Offset = 15, Required = false)]
		public int? RepurchaseTerm {get; set;}
		
		[TagDetails(Tag = 227, Type = TagType.Float, Offset = 16, Required = false)]
		public double? RepurchaseRate {get; set;}
		
		[TagDetails(Tag = 228, Type = TagType.Float, Offset = 17, Required = false)]
		public double? Factor {get; set;}
		
		[TagDetails(Tag = 255, Type = TagType.String, Offset = 18, Required = false)]
		public string? CreditRating {get; set;}
		
		[TagDetails(Tag = 543, Type = TagType.String, Offset = 19, Required = false)]
		public string? InstrRegistry {get; set;}
		
		[TagDetails(Tag = 470, Type = TagType.String, Offset = 20, Required = false)]
		public string? CountryOfIssue {get; set;}
		
		[TagDetails(Tag = 471, Type = TagType.String, Offset = 21, Required = false)]
		public string? StateOrProvinceOfIssue {get; set;}
		
		[TagDetails(Tag = 472, Type = TagType.String, Offset = 22, Required = false)]
		public string? LocaleOfIssue {get; set;}
		
		[TagDetails(Tag = 240, Type = TagType.LocalDate, Offset = 23, Required = false)]
		public DateOnly? RedemptionDate {get; set;}
		
		[TagDetails(Tag = 202, Type = TagType.Float, Offset = 24, Required = false)]
		public double? StrikePrice {get; set;}
		
		[TagDetails(Tag = 947, Type = TagType.String, Offset = 25, Required = false)]
		public string? StrikeCurrency {get; set;}
		
		[TagDetails(Tag = 206, Type = TagType.String, Offset = 26, Required = false)]
		public string? OptAttribute {get; set;}
		
		[TagDetails(Tag = 231, Type = TagType.Float, Offset = 27, Required = false)]
		public double? ContractMultiplier {get; set;}
		
		[TagDetails(Tag = 223, Type = TagType.Float, Offset = 28, Required = false)]
		public double? CouponRate {get; set;}
		
		[TagDetails(Tag = 207, Type = TagType.String, Offset = 29, Required = false)]
		public string? SecurityExchange {get; set;}
		
		[TagDetails(Tag = 106, Type = TagType.String, Offset = 30, Required = false)]
		public string? Issuer {get; set;}
		
		[TagDetails(Tag = 348, Type = TagType.Length, Offset = 31, Required = false)]
		public int? EncodedIssuerLen {get; set;}
		
		[TagDetails(Tag = 349, Type = TagType.RawData, Offset = 32, Required = false)]
		public byte[]? EncodedIssuer {get; set;}
		
		[TagDetails(Tag = 107, Type = TagType.String, Offset = 33, Required = false)]
		public string? SecurityDesc {get; set;}
		
		[TagDetails(Tag = 350, Type = TagType.Length, Offset = 34, Required = false)]
		public int? EncodedSecurityDescLen {get; set;}
		
		[TagDetails(Tag = 351, Type = TagType.RawData, Offset = 35, Required = false)]
		public byte[]? EncodedSecurityDesc {get; set;}
		
		[TagDetails(Tag = 691, Type = TagType.String, Offset = 36, Required = false)]
		public string? Pool {get; set;}
		
		[TagDetails(Tag = 667, Type = TagType.MonthYear, Offset = 37, Required = false)]
		public MonthYear? ContractSettlMonth {get; set;}
		
		[TagDetails(Tag = 875, Type = TagType.Int, Offset = 38, Required = false)]
		public int? CPProgram {get; set;}
		
		[TagDetails(Tag = 876, Type = TagType.String, Offset = 39, Required = false)]
		public string? CPRegType {get; set;}
		
		[Component(Offset = 40, Required = false)]
		public EvntGrp? EvntGrp {get; set;}
		
		[TagDetails(Tag = 873, Type = TagType.LocalDate, Offset = 41, Required = false)]
		public DateOnly? DatedDate {get; set;}
		
		[TagDetails(Tag = 874, Type = TagType.LocalDate, Offset = 42, Required = false)]
		public DateOnly? InterestAccrualDate {get; set;}
		
		
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
			if (EncodedIssuerLen is not null) writer.WriteWholeNumber(348, EncodedIssuerLen.Value);
			if (EncodedIssuer is not null) writer.WriteBuffer(349, EncodedIssuer);
			if (SecurityDesc is not null) writer.WriteString(107, SecurityDesc);
			if (EncodedSecurityDescLen is not null) writer.WriteWholeNumber(350, EncodedSecurityDescLen.Value);
			if (EncodedSecurityDesc is not null) writer.WriteBuffer(351, EncodedSecurityDesc);
			if (Pool is not null) writer.WriteString(691, Pool);
			if (ContractSettlMonth is not null) writer.WriteMonthYear(667, ContractSettlMonth.Value);
			if (CPProgram is not null) writer.WriteWholeNumber(875, CPProgram.Value);
			if (CPRegType is not null) writer.WriteString(876, CPRegType);
			if (EvntGrp is not null) ((IFixEncoder)EvntGrp).Encode(writer);
			if (DatedDate is not null) writer.WriteLocalDateOnly(873, DatedDate.Value);
			if (InterestAccrualDate is not null) writer.WriteLocalDateOnly(874, InterestAccrualDate.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			Symbol = view.GetString(55);
			SymbolSfx = view.GetString(65);
			SecurityID = view.GetString(48);
			SecurityIDSource = view.GetString(22);
			if (view.GetView("SecAltIDGrp") is IMessageView viewSecAltIDGrp)
			{
				SecAltIDGrp = new();
				((IFixParser)SecAltIDGrp).Parse(viewSecAltIDGrp);
			}
			Product = view.GetInt32(460);
			CFICode = view.GetString(461);
			SecurityType = view.GetString(167);
			SecuritySubType = view.GetString(762);
			MaturityMonthYear = view.GetMonthYear(200);
			MaturityDate = view.GetDateOnly(541);
			PutOrCall = view.GetInt32(201);
			CouponPaymentDate = view.GetDateOnly(224);
			IssueDate = view.GetDateOnly(225);
			RepoCollateralSecurityType = view.GetString(239);
			RepurchaseTerm = view.GetInt32(226);
			RepurchaseRate = view.GetDouble(227);
			Factor = view.GetDouble(228);
			CreditRating = view.GetString(255);
			InstrRegistry = view.GetString(543);
			CountryOfIssue = view.GetString(470);
			StateOrProvinceOfIssue = view.GetString(471);
			LocaleOfIssue = view.GetString(472);
			RedemptionDate = view.GetDateOnly(240);
			StrikePrice = view.GetDouble(202);
			StrikeCurrency = view.GetString(947);
			OptAttribute = view.GetString(206);
			ContractMultiplier = view.GetDouble(231);
			CouponRate = view.GetDouble(223);
			SecurityExchange = view.GetString(207);
			Issuer = view.GetString(106);
			EncodedIssuerLen = view.GetInt32(348);
			EncodedIssuer = view.GetByteArray(349);
			SecurityDesc = view.GetString(107);
			EncodedSecurityDescLen = view.GetInt32(350);
			EncodedSecurityDesc = view.GetByteArray(351);
			Pool = view.GetString(691);
			ContractSettlMonth = view.GetMonthYear(667);
			CPProgram = view.GetInt32(875);
			CPRegType = view.GetString(876);
			if (view.GetView("EvntGrp") is IMessageView viewEvntGrp)
			{
				EvntGrp = new();
				((IFixParser)EvntGrp).Parse(viewEvntGrp);
			}
			DatedDate = view.GetDateOnly(873);
			InterestAccrualDate = view.GetDateOnly(874);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "Symbol":
				{
					value = Symbol;
					break;
				}
				case "SymbolSfx":
				{
					value = SymbolSfx;
					break;
				}
				case "SecurityID":
				{
					value = SecurityID;
					break;
				}
				case "SecurityIDSource":
				{
					value = SecurityIDSource;
					break;
				}
				case "SecAltIDGrp":
				{
					value = SecAltIDGrp;
					break;
				}
				case "Product":
				{
					value = Product;
					break;
				}
				case "CFICode":
				{
					value = CFICode;
					break;
				}
				case "SecurityType":
				{
					value = SecurityType;
					break;
				}
				case "SecuritySubType":
				{
					value = SecuritySubType;
					break;
				}
				case "MaturityMonthYear":
				{
					value = MaturityMonthYear;
					break;
				}
				case "MaturityDate":
				{
					value = MaturityDate;
					break;
				}
				case "PutOrCall":
				{
					value = PutOrCall;
					break;
				}
				case "CouponPaymentDate":
				{
					value = CouponPaymentDate;
					break;
				}
				case "IssueDate":
				{
					value = IssueDate;
					break;
				}
				case "RepoCollateralSecurityType":
				{
					value = RepoCollateralSecurityType;
					break;
				}
				case "RepurchaseTerm":
				{
					value = RepurchaseTerm;
					break;
				}
				case "RepurchaseRate":
				{
					value = RepurchaseRate;
					break;
				}
				case "Factor":
				{
					value = Factor;
					break;
				}
				case "CreditRating":
				{
					value = CreditRating;
					break;
				}
				case "InstrRegistry":
				{
					value = InstrRegistry;
					break;
				}
				case "CountryOfIssue":
				{
					value = CountryOfIssue;
					break;
				}
				case "StateOrProvinceOfIssue":
				{
					value = StateOrProvinceOfIssue;
					break;
				}
				case "LocaleOfIssue":
				{
					value = LocaleOfIssue;
					break;
				}
				case "RedemptionDate":
				{
					value = RedemptionDate;
					break;
				}
				case "StrikePrice":
				{
					value = StrikePrice;
					break;
				}
				case "StrikeCurrency":
				{
					value = StrikeCurrency;
					break;
				}
				case "OptAttribute":
				{
					value = OptAttribute;
					break;
				}
				case "ContractMultiplier":
				{
					value = ContractMultiplier;
					break;
				}
				case "CouponRate":
				{
					value = CouponRate;
					break;
				}
				case "SecurityExchange":
				{
					value = SecurityExchange;
					break;
				}
				case "Issuer":
				{
					value = Issuer;
					break;
				}
				case "EncodedIssuerLen":
				{
					value = EncodedIssuerLen;
					break;
				}
				case "EncodedIssuer":
				{
					value = EncodedIssuer;
					break;
				}
				case "SecurityDesc":
				{
					value = SecurityDesc;
					break;
				}
				case "EncodedSecurityDescLen":
				{
					value = EncodedSecurityDescLen;
					break;
				}
				case "EncodedSecurityDesc":
				{
					value = EncodedSecurityDesc;
					break;
				}
				case "Pool":
				{
					value = Pool;
					break;
				}
				case "ContractSettlMonth":
				{
					value = ContractSettlMonth;
					break;
				}
				case "CPProgram":
				{
					value = CPProgram;
					break;
				}
				case "CPRegType":
				{
					value = CPRegType;
					break;
				}
				case "EvntGrp":
				{
					value = EvntGrp;
					break;
				}
				case "DatedDate":
				{
					value = DatedDate;
					break;
				}
				case "InterestAccrualDate":
				{
					value = InterestAccrualDate;
					break;
				}
				default:
				{
					return false;
				}
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			Symbol = null;
			SymbolSfx = null;
			SecurityID = null;
			SecurityIDSource = null;
			((IFixReset?)SecAltIDGrp)?.Reset();
			Product = null;
			CFICode = null;
			SecurityType = null;
			SecuritySubType = null;
			MaturityMonthYear = null;
			MaturityDate = null;
			PutOrCall = null;
			CouponPaymentDate = null;
			IssueDate = null;
			RepoCollateralSecurityType = null;
			RepurchaseTerm = null;
			RepurchaseRate = null;
			Factor = null;
			CreditRating = null;
			InstrRegistry = null;
			CountryOfIssue = null;
			StateOrProvinceOfIssue = null;
			LocaleOfIssue = null;
			RedemptionDate = null;
			StrikePrice = null;
			StrikeCurrency = null;
			OptAttribute = null;
			ContractMultiplier = null;
			CouponRate = null;
			SecurityExchange = null;
			Issuer = null;
			EncodedIssuerLen = null;
			EncodedIssuer = null;
			SecurityDesc = null;
			EncodedSecurityDescLen = null;
			EncodedSecurityDesc = null;
			Pool = null;
			ContractSettlMonth = null;
			CPProgram = null;
			CPRegType = null;
			((IFixReset?)EvntGrp)?.Reset();
			DatedDate = null;
			InterestAccrualDate = null;
		}
	}
}
