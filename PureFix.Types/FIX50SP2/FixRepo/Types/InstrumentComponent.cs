using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo.Types
{
	public sealed partial class InstrumentComponent : IFixComponent
	{
		[TagDetails(Tag = 55, Type = TagType.String, Offset = 0, Required = false)]
		public string? Symbol {get; set;}
		
		[TagDetails(Tag = 65, Type = TagType.String, Offset = 1, Required = false)]
		public string? SymbolSfx {get; set;}
		
		[TagDetails(Tag = 48, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecurityID {get; set;}
		
		[TagDetails(Tag = 22, Type = TagType.String, Offset = 3, Required = false)]
		public string? SecurityIDSource {get; set;}
		
		[TagDetails(Tag = 460, Type = TagType.Int, Offset = 4, Required = false)]
		public int? Product {get; set;}
		
		[TagDetails(Tag = 1227, Type = TagType.String, Offset = 5, Required = false)]
		public string? ProductComplex {get; set;}
		
		[TagDetails(Tag = 1151, Type = TagType.String, Offset = 6, Required = false)]
		public string? SecurityGroup {get; set;}
		
		[TagDetails(Tag = 461, Type = TagType.String, Offset = 7, Required = false)]
		public string? CFICode {get; set;}
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 8, Required = false)]
		public string? SecurityType {get; set;}
		
		[TagDetails(Tag = 762, Type = TagType.String, Offset = 9, Required = false)]
		public string? SecuritySubType {get; set;}
		
		[TagDetails(Tag = 200, Type = TagType.MonthYear, Offset = 10, Required = false)]
		public MonthYear? MaturityMonthYear {get; set;}
		
		[TagDetails(Tag = 541, Type = TagType.LocalDate, Offset = 11, Required = false)]
		public DateOnly? MaturityDate {get; set;}
		
		[TagDetails(Tag = 1079, Type = TagType.String, Offset = 12, Required = false)]
		public string? MaturityTime {get; set;}
		
		[TagDetails(Tag = 966, Type = TagType.String, Offset = 13, Required = false)]
		public string? SettleOnOpenFlag {get; set;}
		
		[TagDetails(Tag = 1049, Type = TagType.String, Offset = 14, Required = false)]
		public string? InstrmtAssignmentMethod {get; set;}
		
		[TagDetails(Tag = 965, Type = TagType.String, Offset = 15, Required = false)]
		public string? SecurityStatus {get; set;}
		
		[TagDetails(Tag = 224, Type = TagType.LocalDate, Offset = 16, Required = false)]
		public DateOnly? CouponPaymentDate {get; set;}
		
		[TagDetails(Tag = 225, Type = TagType.LocalDate, Offset = 17, Required = false)]
		public DateOnly? IssueDate {get; set;}
		
		[TagDetails(Tag = 239, Type = TagType.String, Offset = 18, Required = false)]
		public string? RepoCollateralSecurityType {get; set;}
		
		[TagDetails(Tag = 226, Type = TagType.Int, Offset = 19, Required = false)]
		public int? RepurchaseTerm {get; set;}
		
		[TagDetails(Tag = 227, Type = TagType.Float, Offset = 20, Required = false)]
		public double? RepurchaseRate {get; set;}
		
		[TagDetails(Tag = 228, Type = TagType.Float, Offset = 21, Required = false)]
		public double? Factor {get; set;}
		
		[TagDetails(Tag = 255, Type = TagType.String, Offset = 22, Required = false)]
		public string? CreditRating {get; set;}
		
		[TagDetails(Tag = 543, Type = TagType.String, Offset = 23, Required = false)]
		public string? InstrRegistry {get; set;}
		
		[TagDetails(Tag = 470, Type = TagType.String, Offset = 24, Required = false)]
		public string? CountryOfIssue {get; set;}
		
		[TagDetails(Tag = 471, Type = TagType.String, Offset = 25, Required = false)]
		public string? StateOrProvinceOfIssue {get; set;}
		
		[TagDetails(Tag = 472, Type = TagType.String, Offset = 26, Required = false)]
		public string? LocaleOfIssue {get; set;}
		
		[TagDetails(Tag = 240, Type = TagType.LocalDate, Offset = 27, Required = false)]
		public DateOnly? RedemptionDate {get; set;}
		
		[TagDetails(Tag = 202, Type = TagType.Float, Offset = 28, Required = false)]
		public double? StrikePrice {get; set;}
		
		[TagDetails(Tag = 947, Type = TagType.String, Offset = 29, Required = false)]
		public string? StrikeCurrency {get; set;}
		
		[TagDetails(Tag = 967, Type = TagType.Float, Offset = 30, Required = false)]
		public double? StrikeMultiplier {get; set;}
		
		[TagDetails(Tag = 968, Type = TagType.Float, Offset = 31, Required = false)]
		public double? StrikeValue {get; set;}
		
		[TagDetails(Tag = 206, Type = TagType.String, Offset = 32, Required = false)]
		public string? OptAttribute {get; set;}
		
		[TagDetails(Tag = 231, Type = TagType.Float, Offset = 33, Required = false)]
		public double? ContractMultiplier {get; set;}
		
		[TagDetails(Tag = 969, Type = TagType.Float, Offset = 34, Required = false)]
		public double? MinPriceIncrement {get; set;}
		
		[TagDetails(Tag = 1146, Type = TagType.Float, Offset = 35, Required = false)]
		public double? MinPriceIncrementAmount {get; set;}
		
		[TagDetails(Tag = 996, Type = TagType.String, Offset = 36, Required = false)]
		public string? UnitOfMeasure {get; set;}
		
		[TagDetails(Tag = 1147, Type = TagType.Float, Offset = 37, Required = false)]
		public double? UnitOfMeasureQty {get; set;}
		
		[TagDetails(Tag = 1191, Type = TagType.String, Offset = 38, Required = false)]
		public string? PriceUnitOfMeasure {get; set;}
		
		[TagDetails(Tag = 1192, Type = TagType.Float, Offset = 39, Required = false)]
		public double? PriceUnitOfMeasureQty {get; set;}
		
		[TagDetails(Tag = 1193, Type = TagType.String, Offset = 40, Required = false)]
		public string? SettlMethod {get; set;}
		
		[TagDetails(Tag = 1194, Type = TagType.Int, Offset = 41, Required = false)]
		public int? ExerciseStyle {get; set;}
		
		[TagDetails(Tag = 1195, Type = TagType.Float, Offset = 42, Required = false)]
		public double? OptPayoutAmount {get; set;}
		
		[TagDetails(Tag = 1196, Type = TagType.String, Offset = 43, Required = false)]
		public string? PriceQuoteMethod {get; set;}
		
		[TagDetails(Tag = 1197, Type = TagType.String, Offset = 44, Required = false)]
		public string? ValuationMethod {get; set;}
		
		[TagDetails(Tag = 1198, Type = TagType.Int, Offset = 45, Required = false)]
		public int? ListMethod {get; set;}
		
		[TagDetails(Tag = 1199, Type = TagType.Float, Offset = 46, Required = false)]
		public double? CapPrice {get; set;}
		
		[TagDetails(Tag = 1200, Type = TagType.Float, Offset = 47, Required = false)]
		public double? FloorPrice {get; set;}
		
		[TagDetails(Tag = 201, Type = TagType.Int, Offset = 48, Required = false)]
		public int? PutOrCall {get; set;}
		
		[TagDetails(Tag = 1244, Type = TagType.Boolean, Offset = 49, Required = false)]
		public bool? FlexibleIndicator {get; set;}
		
		[TagDetails(Tag = 1242, Type = TagType.Boolean, Offset = 50, Required = false)]
		public bool? FlexProductEligibilityIndicator {get; set;}
		
		[TagDetails(Tag = 997, Type = TagType.String, Offset = 51, Required = false)]
		public string? TimeUnit {get; set;}
		
		[TagDetails(Tag = 223, Type = TagType.Float, Offset = 52, Required = false)]
		public double? CouponRate {get; set;}
		
		[TagDetails(Tag = 207, Type = TagType.String, Offset = 53, Required = false)]
		public string? SecurityExchange {get; set;}
		
		[TagDetails(Tag = 970, Type = TagType.Int, Offset = 54, Required = false)]
		public int? PositionLimit {get; set;}
		
		[TagDetails(Tag = 971, Type = TagType.Int, Offset = 55, Required = false)]
		public int? NTPositionLimit {get; set;}
		
		[TagDetails(Tag = 106, Type = TagType.String, Offset = 56, Required = false)]
		public string? Issuer {get; set;}
		
		[TagDetails(Tag = 348, Type = TagType.Length, Offset = 57, Required = false, LinksToTag = 349)]
		public int? EncodedIssuerLen {get; set;}
		
		[TagDetails(Tag = 349, Type = TagType.RawData, Offset = 58, Required = false, LinksToTag = 348)]
		public byte[]? EncodedIssuer {get; set;}
		
		[TagDetails(Tag = 107, Type = TagType.String, Offset = 59, Required = false)]
		public string? SecurityDesc {get; set;}
		
		[TagDetails(Tag = 350, Type = TagType.Length, Offset = 60, Required = false, LinksToTag = 351)]
		public int? EncodedSecurityDescLen {get; set;}
		
		[TagDetails(Tag = 351, Type = TagType.RawData, Offset = 61, Required = false, LinksToTag = 350)]
		public byte[]? EncodedSecurityDesc {get; set;}
		
		[Component(Offset = 62, Required = false)]
		public SecurityXMLComponent? SecurityXML {get; set;}
		
		[TagDetails(Tag = 691, Type = TagType.String, Offset = 63, Required = false)]
		public string? Pool {get; set;}
		
		[TagDetails(Tag = 667, Type = TagType.MonthYear, Offset = 64, Required = false)]
		public MonthYear? ContractSettlMonth {get; set;}
		
		[TagDetails(Tag = 875, Type = TagType.Int, Offset = 65, Required = false)]
		public int? CPProgram {get; set;}
		
		[TagDetails(Tag = 876, Type = TagType.String, Offset = 66, Required = false)]
		public string? CPRegType {get; set;}
		
		[TagDetails(Tag = 873, Type = TagType.LocalDate, Offset = 67, Required = false)]
		public DateOnly? DatedDate {get; set;}
		
		[TagDetails(Tag = 874, Type = TagType.LocalDate, Offset = 68, Required = false)]
		public DateOnly? InterestAccrualDate {get; set;}
		
		[TagDetails(Tag = 1435, Type = TagType.Int, Offset = 69, Required = false)]
		public int? ContractMultiplierUnit {get; set;}
		
		[TagDetails(Tag = 1439, Type = TagType.Int, Offset = 70, Required = false)]
		public int? FlowScheduleType {get; set;}
		
		[TagDetails(Tag = 1449, Type = TagType.String, Offset = 71, Required = false)]
		public string? RestructuringType {get; set;}
		
		[TagDetails(Tag = 1450, Type = TagType.String, Offset = 72, Required = false)]
		public string? Seniority {get; set;}
		
		[TagDetails(Tag = 1451, Type = TagType.Float, Offset = 73, Required = false)]
		public double? NotionalPercentageOutstanding {get; set;}
		
		[TagDetails(Tag = 1452, Type = TagType.Float, Offset = 74, Required = false)]
		public double? OriginalNotionalPercentageOutstanding {get; set;}
		
		[TagDetails(Tag = 1457, Type = TagType.Float, Offset = 75, Required = false)]
		public double? AttachmentPoint {get; set;}
		
		[TagDetails(Tag = 1458, Type = TagType.Float, Offset = 76, Required = false)]
		public double? DetachmentPoint {get; set;}
		
		[TagDetails(Tag = 1478, Type = TagType.Int, Offset = 77, Required = false)]
		public int? StrikePriceDeterminationMethod {get; set;}
		
		[TagDetails(Tag = 1479, Type = TagType.Int, Offset = 78, Required = false)]
		public int? StrikePriceBoundaryMethod {get; set;}
		
		[TagDetails(Tag = 1480, Type = TagType.Float, Offset = 79, Required = false)]
		public double? StrikePriceBoundaryPrecision {get; set;}
		
		[TagDetails(Tag = 1481, Type = TagType.Int, Offset = 80, Required = false)]
		public int? UnderlyingPriceDeterminationMethod {get; set;}
		
		[TagDetails(Tag = 1482, Type = TagType.Int, Offset = 81, Required = false)]
		public int? OptPayoutType {get; set;}
		
		
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
			if (Product is not null) writer.WriteWholeNumber(460, Product.Value);
			if (ProductComplex is not null) writer.WriteString(1227, ProductComplex);
			if (SecurityGroup is not null) writer.WriteString(1151, SecurityGroup);
			if (CFICode is not null) writer.WriteString(461, CFICode);
			if (SecurityType is not null) writer.WriteString(167, SecurityType);
			if (SecuritySubType is not null) writer.WriteString(762, SecuritySubType);
			if (MaturityMonthYear is not null) writer.WriteMonthYear(200, MaturityMonthYear.Value);
			if (MaturityDate is not null) writer.WriteLocalDateOnly(541, MaturityDate.Value);
			if (MaturityTime is not null) writer.WriteString(1079, MaturityTime);
			if (SettleOnOpenFlag is not null) writer.WriteString(966, SettleOnOpenFlag);
			if (InstrmtAssignmentMethod is not null) writer.WriteString(1049, InstrmtAssignmentMethod);
			if (SecurityStatus is not null) writer.WriteString(965, SecurityStatus);
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
			if (StrikeMultiplier is not null) writer.WriteNumber(967, StrikeMultiplier.Value);
			if (StrikeValue is not null) writer.WriteNumber(968, StrikeValue.Value);
			if (OptAttribute is not null) writer.WriteString(206, OptAttribute);
			if (ContractMultiplier is not null) writer.WriteNumber(231, ContractMultiplier.Value);
			if (MinPriceIncrement is not null) writer.WriteNumber(969, MinPriceIncrement.Value);
			if (MinPriceIncrementAmount is not null) writer.WriteNumber(1146, MinPriceIncrementAmount.Value);
			if (UnitOfMeasure is not null) writer.WriteString(996, UnitOfMeasure);
			if (UnitOfMeasureQty is not null) writer.WriteNumber(1147, UnitOfMeasureQty.Value);
			if (PriceUnitOfMeasure is not null) writer.WriteString(1191, PriceUnitOfMeasure);
			if (PriceUnitOfMeasureQty is not null) writer.WriteNumber(1192, PriceUnitOfMeasureQty.Value);
			if (SettlMethod is not null) writer.WriteString(1193, SettlMethod);
			if (ExerciseStyle is not null) writer.WriteWholeNumber(1194, ExerciseStyle.Value);
			if (OptPayoutAmount is not null) writer.WriteNumber(1195, OptPayoutAmount.Value);
			if (PriceQuoteMethod is not null) writer.WriteString(1196, PriceQuoteMethod);
			if (ValuationMethod is not null) writer.WriteString(1197, ValuationMethod);
			if (ListMethod is not null) writer.WriteWholeNumber(1198, ListMethod.Value);
			if (CapPrice is not null) writer.WriteNumber(1199, CapPrice.Value);
			if (FloorPrice is not null) writer.WriteNumber(1200, FloorPrice.Value);
			if (PutOrCall is not null) writer.WriteWholeNumber(201, PutOrCall.Value);
			if (FlexibleIndicator is not null) writer.WriteBoolean(1244, FlexibleIndicator.Value);
			if (FlexProductEligibilityIndicator is not null) writer.WriteBoolean(1242, FlexProductEligibilityIndicator.Value);
			if (TimeUnit is not null) writer.WriteString(997, TimeUnit);
			if (CouponRate is not null) writer.WriteNumber(223, CouponRate.Value);
			if (SecurityExchange is not null) writer.WriteString(207, SecurityExchange);
			if (PositionLimit is not null) writer.WriteWholeNumber(970, PositionLimit.Value);
			if (NTPositionLimit is not null) writer.WriteWholeNumber(971, NTPositionLimit.Value);
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
			if (SecurityXML is not null) ((IFixEncoder)SecurityXML).Encode(writer);
			if (Pool is not null) writer.WriteString(691, Pool);
			if (ContractSettlMonth is not null) writer.WriteMonthYear(667, ContractSettlMonth.Value);
			if (CPProgram is not null) writer.WriteWholeNumber(875, CPProgram.Value);
			if (CPRegType is not null) writer.WriteString(876, CPRegType);
			if (DatedDate is not null) writer.WriteLocalDateOnly(873, DatedDate.Value);
			if (InterestAccrualDate is not null) writer.WriteLocalDateOnly(874, InterestAccrualDate.Value);
			if (ContractMultiplierUnit is not null) writer.WriteWholeNumber(1435, ContractMultiplierUnit.Value);
			if (FlowScheduleType is not null) writer.WriteWholeNumber(1439, FlowScheduleType.Value);
			if (RestructuringType is not null) writer.WriteString(1449, RestructuringType);
			if (Seniority is not null) writer.WriteString(1450, Seniority);
			if (NotionalPercentageOutstanding is not null) writer.WriteNumber(1451, NotionalPercentageOutstanding.Value);
			if (OriginalNotionalPercentageOutstanding is not null) writer.WriteNumber(1452, OriginalNotionalPercentageOutstanding.Value);
			if (AttachmentPoint is not null) writer.WriteNumber(1457, AttachmentPoint.Value);
			if (DetachmentPoint is not null) writer.WriteNumber(1458, DetachmentPoint.Value);
			if (StrikePriceDeterminationMethod is not null) writer.WriteWholeNumber(1478, StrikePriceDeterminationMethod.Value);
			if (StrikePriceBoundaryMethod is not null) writer.WriteWholeNumber(1479, StrikePriceBoundaryMethod.Value);
			if (StrikePriceBoundaryPrecision is not null) writer.WriteNumber(1480, StrikePriceBoundaryPrecision.Value);
			if (UnderlyingPriceDeterminationMethod is not null) writer.WriteWholeNumber(1481, UnderlyingPriceDeterminationMethod.Value);
			if (OptPayoutType is not null) writer.WriteWholeNumber(1482, OptPayoutType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			Symbol = view.GetString(55);
			SymbolSfx = view.GetString(65);
			SecurityID = view.GetString(48);
			SecurityIDSource = view.GetString(22);
			Product = view.GetInt32(460);
			ProductComplex = view.GetString(1227);
			SecurityGroup = view.GetString(1151);
			CFICode = view.GetString(461);
			SecurityType = view.GetString(167);
			SecuritySubType = view.GetString(762);
			MaturityMonthYear = view.GetMonthYear(200);
			MaturityDate = view.GetDateOnly(541);
			MaturityTime = view.GetString(1079);
			SettleOnOpenFlag = view.GetString(966);
			InstrmtAssignmentMethod = view.GetString(1049);
			SecurityStatus = view.GetString(965);
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
			StrikeMultiplier = view.GetDouble(967);
			StrikeValue = view.GetDouble(968);
			OptAttribute = view.GetString(206);
			ContractMultiplier = view.GetDouble(231);
			MinPriceIncrement = view.GetDouble(969);
			MinPriceIncrementAmount = view.GetDouble(1146);
			UnitOfMeasure = view.GetString(996);
			UnitOfMeasureQty = view.GetDouble(1147);
			PriceUnitOfMeasure = view.GetString(1191);
			PriceUnitOfMeasureQty = view.GetDouble(1192);
			SettlMethod = view.GetString(1193);
			ExerciseStyle = view.GetInt32(1194);
			OptPayoutAmount = view.GetDouble(1195);
			PriceQuoteMethod = view.GetString(1196);
			ValuationMethod = view.GetString(1197);
			ListMethod = view.GetInt32(1198);
			CapPrice = view.GetDouble(1199);
			FloorPrice = view.GetDouble(1200);
			PutOrCall = view.GetInt32(201);
			FlexibleIndicator = view.GetBool(1244);
			FlexProductEligibilityIndicator = view.GetBool(1242);
			TimeUnit = view.GetString(997);
			CouponRate = view.GetDouble(223);
			SecurityExchange = view.GetString(207);
			PositionLimit = view.GetInt32(970);
			NTPositionLimit = view.GetInt32(971);
			Issuer = view.GetString(106);
			EncodedIssuerLen = view.GetInt32(348);
			EncodedIssuer = view.GetByteArray(349);
			SecurityDesc = view.GetString(107);
			EncodedSecurityDescLen = view.GetInt32(350);
			EncodedSecurityDesc = view.GetByteArray(351);
			if (view.GetView("SecurityXML") is IMessageView viewSecurityXML)
			{
				SecurityXML = new();
				((IFixParser)SecurityXML).Parse(viewSecurityXML);
			}
			Pool = view.GetString(691);
			ContractSettlMonth = view.GetMonthYear(667);
			CPProgram = view.GetInt32(875);
			CPRegType = view.GetString(876);
			DatedDate = view.GetDateOnly(873);
			InterestAccrualDate = view.GetDateOnly(874);
			ContractMultiplierUnit = view.GetInt32(1435);
			FlowScheduleType = view.GetInt32(1439);
			RestructuringType = view.GetString(1449);
			Seniority = view.GetString(1450);
			NotionalPercentageOutstanding = view.GetDouble(1451);
			OriginalNotionalPercentageOutstanding = view.GetDouble(1452);
			AttachmentPoint = view.GetDouble(1457);
			DetachmentPoint = view.GetDouble(1458);
			StrikePriceDeterminationMethod = view.GetInt32(1478);
			StrikePriceBoundaryMethod = view.GetInt32(1479);
			StrikePriceBoundaryPrecision = view.GetDouble(1480);
			UnderlyingPriceDeterminationMethod = view.GetInt32(1481);
			OptPayoutType = view.GetInt32(1482);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "Symbol":
					value = Symbol;
					break;
				case "SymbolSfx":
					value = SymbolSfx;
					break;
				case "SecurityID":
					value = SecurityID;
					break;
				case "SecurityIDSource":
					value = SecurityIDSource;
					break;
				case "Product":
					value = Product;
					break;
				case "ProductComplex":
					value = ProductComplex;
					break;
				case "SecurityGroup":
					value = SecurityGroup;
					break;
				case "CFICode":
					value = CFICode;
					break;
				case "SecurityType":
					value = SecurityType;
					break;
				case "SecuritySubType":
					value = SecuritySubType;
					break;
				case "MaturityMonthYear":
					value = MaturityMonthYear;
					break;
				case "MaturityDate":
					value = MaturityDate;
					break;
				case "MaturityTime":
					value = MaturityTime;
					break;
				case "SettleOnOpenFlag":
					value = SettleOnOpenFlag;
					break;
				case "InstrmtAssignmentMethod":
					value = InstrmtAssignmentMethod;
					break;
				case "SecurityStatus":
					value = SecurityStatus;
					break;
				case "CouponPaymentDate":
					value = CouponPaymentDate;
					break;
				case "IssueDate":
					value = IssueDate;
					break;
				case "RepoCollateralSecurityType":
					value = RepoCollateralSecurityType;
					break;
				case "RepurchaseTerm":
					value = RepurchaseTerm;
					break;
				case "RepurchaseRate":
					value = RepurchaseRate;
					break;
				case "Factor":
					value = Factor;
					break;
				case "CreditRating":
					value = CreditRating;
					break;
				case "InstrRegistry":
					value = InstrRegistry;
					break;
				case "CountryOfIssue":
					value = CountryOfIssue;
					break;
				case "StateOrProvinceOfIssue":
					value = StateOrProvinceOfIssue;
					break;
				case "LocaleOfIssue":
					value = LocaleOfIssue;
					break;
				case "RedemptionDate":
					value = RedemptionDate;
					break;
				case "StrikePrice":
					value = StrikePrice;
					break;
				case "StrikeCurrency":
					value = StrikeCurrency;
					break;
				case "StrikeMultiplier":
					value = StrikeMultiplier;
					break;
				case "StrikeValue":
					value = StrikeValue;
					break;
				case "OptAttribute":
					value = OptAttribute;
					break;
				case "ContractMultiplier":
					value = ContractMultiplier;
					break;
				case "MinPriceIncrement":
					value = MinPriceIncrement;
					break;
				case "MinPriceIncrementAmount":
					value = MinPriceIncrementAmount;
					break;
				case "UnitOfMeasure":
					value = UnitOfMeasure;
					break;
				case "UnitOfMeasureQty":
					value = UnitOfMeasureQty;
					break;
				case "PriceUnitOfMeasure":
					value = PriceUnitOfMeasure;
					break;
				case "PriceUnitOfMeasureQty":
					value = PriceUnitOfMeasureQty;
					break;
				case "SettlMethod":
					value = SettlMethod;
					break;
				case "ExerciseStyle":
					value = ExerciseStyle;
					break;
				case "OptPayoutAmount":
					value = OptPayoutAmount;
					break;
				case "PriceQuoteMethod":
					value = PriceQuoteMethod;
					break;
				case "ValuationMethod":
					value = ValuationMethod;
					break;
				case "ListMethod":
					value = ListMethod;
					break;
				case "CapPrice":
					value = CapPrice;
					break;
				case "FloorPrice":
					value = FloorPrice;
					break;
				case "PutOrCall":
					value = PutOrCall;
					break;
				case "FlexibleIndicator":
					value = FlexibleIndicator;
					break;
				case "FlexProductEligibilityIndicator":
					value = FlexProductEligibilityIndicator;
					break;
				case "TimeUnit":
					value = TimeUnit;
					break;
				case "CouponRate":
					value = CouponRate;
					break;
				case "SecurityExchange":
					value = SecurityExchange;
					break;
				case "PositionLimit":
					value = PositionLimit;
					break;
				case "NTPositionLimit":
					value = NTPositionLimit;
					break;
				case "Issuer":
					value = Issuer;
					break;
				case "EncodedIssuerLen":
					value = EncodedIssuerLen;
					break;
				case "EncodedIssuer":
					value = EncodedIssuer;
					break;
				case "SecurityDesc":
					value = SecurityDesc;
					break;
				case "EncodedSecurityDescLen":
					value = EncodedSecurityDescLen;
					break;
				case "EncodedSecurityDesc":
					value = EncodedSecurityDesc;
					break;
				case "SecurityXML":
					value = SecurityXML;
					break;
				case "Pool":
					value = Pool;
					break;
				case "ContractSettlMonth":
					value = ContractSettlMonth;
					break;
				case "CPProgram":
					value = CPProgram;
					break;
				case "CPRegType":
					value = CPRegType;
					break;
				case "DatedDate":
					value = DatedDate;
					break;
				case "InterestAccrualDate":
					value = InterestAccrualDate;
					break;
				case "ContractMultiplierUnit":
					value = ContractMultiplierUnit;
					break;
				case "FlowScheduleType":
					value = FlowScheduleType;
					break;
				case "RestructuringType":
					value = RestructuringType;
					break;
				case "Seniority":
					value = Seniority;
					break;
				case "NotionalPercentageOutstanding":
					value = NotionalPercentageOutstanding;
					break;
				case "OriginalNotionalPercentageOutstanding":
					value = OriginalNotionalPercentageOutstanding;
					break;
				case "AttachmentPoint":
					value = AttachmentPoint;
					break;
				case "DetachmentPoint":
					value = DetachmentPoint;
					break;
				case "StrikePriceDeterminationMethod":
					value = StrikePriceDeterminationMethod;
					break;
				case "StrikePriceBoundaryMethod":
					value = StrikePriceBoundaryMethod;
					break;
				case "StrikePriceBoundaryPrecision":
					value = StrikePriceBoundaryPrecision;
					break;
				case "UnderlyingPriceDeterminationMethod":
					value = UnderlyingPriceDeterminationMethod;
					break;
				case "OptPayoutType":
					value = OptPayoutType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			Symbol = null;
			SymbolSfx = null;
			SecurityID = null;
			SecurityIDSource = null;
			Product = null;
			ProductComplex = null;
			SecurityGroup = null;
			CFICode = null;
			SecurityType = null;
			SecuritySubType = null;
			MaturityMonthYear = null;
			MaturityDate = null;
			MaturityTime = null;
			SettleOnOpenFlag = null;
			InstrmtAssignmentMethod = null;
			SecurityStatus = null;
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
			StrikeMultiplier = null;
			StrikeValue = null;
			OptAttribute = null;
			ContractMultiplier = null;
			MinPriceIncrement = null;
			MinPriceIncrementAmount = null;
			UnitOfMeasure = null;
			UnitOfMeasureQty = null;
			PriceUnitOfMeasure = null;
			PriceUnitOfMeasureQty = null;
			SettlMethod = null;
			ExerciseStyle = null;
			OptPayoutAmount = null;
			PriceQuoteMethod = null;
			ValuationMethod = null;
			ListMethod = null;
			CapPrice = null;
			FloorPrice = null;
			PutOrCall = null;
			FlexibleIndicator = null;
			FlexProductEligibilityIndicator = null;
			TimeUnit = null;
			CouponRate = null;
			SecurityExchange = null;
			PositionLimit = null;
			NTPositionLimit = null;
			Issuer = null;
			EncodedIssuerLen = null;
			EncodedIssuer = null;
			SecurityDesc = null;
			EncodedSecurityDescLen = null;
			EncodedSecurityDesc = null;
			((IFixReset?)SecurityXML)?.Reset();
			Pool = null;
			ContractSettlMonth = null;
			CPProgram = null;
			CPRegType = null;
			DatedDate = null;
			InterestAccrualDate = null;
			ContractMultiplierUnit = null;
			FlowScheduleType = null;
			RestructuringType = null;
			Seniority = null;
			NotionalPercentageOutstanding = null;
			OriginalNotionalPercentageOutstanding = null;
			AttachmentPoint = null;
			DetachmentPoint = null;
			StrikePriceDeterminationMethod = null;
			StrikePriceBoundaryMethod = null;
			StrikePriceBoundaryPrecision = null;
			UnderlyingPriceDeterminationMethod = null;
			OptPayoutType = null;
		}
	}
}
