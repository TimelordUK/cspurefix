using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44UnitTest.Components;

namespace PureFix.Types.FIX44UnitTest.Components
{
	public sealed partial class InstrumentLeg : IFixComponent
	{
		[TagDetails(Tag = 600, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegSymbol {get; set;}
		
		[TagDetails(Tag = 601, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegSymbolSfx {get; set;}
		
		[TagDetails(Tag = 602, Type = TagType.String, Offset = 2, Required = false)]
		public string? LegSecurityID {get; set;}
		
		[TagDetails(Tag = 603, Type = TagType.String, Offset = 3, Required = false)]
		public string? LegSecurityIDSource {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public LegSecAltIDGrp? LegSecAltIDGrp {get; set;}
		
		[TagDetails(Tag = 607, Type = TagType.Int, Offset = 5, Required = false)]
		public int? LegProduct {get; set;}
		
		[TagDetails(Tag = 608, Type = TagType.String, Offset = 6, Required = false)]
		public string? LegCFICode {get; set;}
		
		[TagDetails(Tag = 609, Type = TagType.String, Offset = 7, Required = false)]
		public string? LegSecurityType {get; set;}
		
		[TagDetails(Tag = 764, Type = TagType.String, Offset = 8, Required = false)]
		public string? LegSecuritySubType {get; set;}
		
		[TagDetails(Tag = 610, Type = TagType.MonthYear, Offset = 9, Required = false)]
		public MonthYear? LegMaturityMonthYear {get; set;}
		
		[TagDetails(Tag = 611, Type = TagType.LocalDate, Offset = 10, Required = false)]
		public DateOnly? LegMaturityDate {get; set;}
		
		[TagDetails(Tag = 248, Type = TagType.LocalDate, Offset = 11, Required = false)]
		public DateOnly? LegCouponPaymentDate {get; set;}
		
		[TagDetails(Tag = 249, Type = TagType.LocalDate, Offset = 12, Required = false)]
		public DateOnly? LegIssueDate {get; set;}
		
		[TagDetails(Tag = 250, Type = TagType.String, Offset = 13, Required = false)]
		public string? LegRepoCollateralSecurityType {get; set;}
		
		[TagDetails(Tag = 251, Type = TagType.Int, Offset = 14, Required = false)]
		public int? LegRepurchaseTerm {get; set;}
		
		[TagDetails(Tag = 252, Type = TagType.Float, Offset = 15, Required = false)]
		public double? LegRepurchaseRate {get; set;}
		
		[TagDetails(Tag = 253, Type = TagType.Float, Offset = 16, Required = false)]
		public double? LegFactor {get; set;}
		
		[TagDetails(Tag = 257, Type = TagType.String, Offset = 17, Required = false)]
		public string? LegCreditRating {get; set;}
		
		[TagDetails(Tag = 599, Type = TagType.String, Offset = 18, Required = false)]
		public string? LegInstrRegistry {get; set;}
		
		[TagDetails(Tag = 596, Type = TagType.String, Offset = 19, Required = false)]
		public string? LegCountryOfIssue {get; set;}
		
		[TagDetails(Tag = 597, Type = TagType.String, Offset = 20, Required = false)]
		public string? LegStateOrProvinceOfIssue {get; set;}
		
		[TagDetails(Tag = 598, Type = TagType.String, Offset = 21, Required = false)]
		public string? LegLocaleOfIssue {get; set;}
		
		[TagDetails(Tag = 254, Type = TagType.LocalDate, Offset = 22, Required = false)]
		public DateOnly? LegRedemptionDate {get; set;}
		
		[TagDetails(Tag = 612, Type = TagType.Float, Offset = 23, Required = false)]
		public double? LegStrikePrice {get; set;}
		
		[TagDetails(Tag = 942, Type = TagType.String, Offset = 24, Required = false)]
		public string? LegStrikeCurrency {get; set;}
		
		[TagDetails(Tag = 613, Type = TagType.String, Offset = 25, Required = false)]
		public string? LegOptAttribute {get; set;}
		
		[TagDetails(Tag = 614, Type = TagType.Float, Offset = 26, Required = false)]
		public double? LegContractMultiplier {get; set;}
		
		[TagDetails(Tag = 615, Type = TagType.Float, Offset = 27, Required = false)]
		public double? LegCouponRate {get; set;}
		
		[TagDetails(Tag = 616, Type = TagType.String, Offset = 28, Required = false)]
		public string? LegSecurityExchange {get; set;}
		
		[TagDetails(Tag = 617, Type = TagType.String, Offset = 29, Required = false)]
		public string? LegIssuer {get; set;}
		
		[TagDetails(Tag = 618, Type = TagType.Length, Offset = 30, Required = false)]
		public int? EncodedLegIssuerLen {get; set;}
		
		[TagDetails(Tag = 619, Type = TagType.RawData, Offset = 31, Required = false)]
		public byte[]? EncodedLegIssuer {get; set;}
		
		[TagDetails(Tag = 620, Type = TagType.String, Offset = 32, Required = false)]
		public string? LegSecurityDesc {get; set;}
		
		[TagDetails(Tag = 621, Type = TagType.Length, Offset = 33, Required = false)]
		public int? EncodedLegSecurityDescLen {get; set;}
		
		[TagDetails(Tag = 622, Type = TagType.RawData, Offset = 34, Required = false)]
		public byte[]? EncodedLegSecurityDesc {get; set;}
		
		[TagDetails(Tag = 623, Type = TagType.Float, Offset = 35, Required = false)]
		public double? LegRatioQty {get; set;}
		
		[TagDetails(Tag = 624, Type = TagType.String, Offset = 36, Required = false)]
		public string? LegSide {get; set;}
		
		[TagDetails(Tag = 556, Type = TagType.String, Offset = 37, Required = false)]
		public string? LegCurrency {get; set;}
		
		[TagDetails(Tag = 740, Type = TagType.String, Offset = 38, Required = false)]
		public string? LegPool {get; set;}
		
		[TagDetails(Tag = 739, Type = TagType.LocalDate, Offset = 39, Required = false)]
		public DateOnly? LegDatedDate {get; set;}
		
		[TagDetails(Tag = 955, Type = TagType.MonthYear, Offset = 40, Required = false)]
		public MonthYear? LegContractSettlMonth {get; set;}
		
		[TagDetails(Tag = 956, Type = TagType.LocalDate, Offset = 41, Required = false)]
		public DateOnly? LegInterestAccrualDate {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegSymbol is not null) writer.WriteString(600, LegSymbol);
			if (LegSymbolSfx is not null) writer.WriteString(601, LegSymbolSfx);
			if (LegSecurityID is not null) writer.WriteString(602, LegSecurityID);
			if (LegSecurityIDSource is not null) writer.WriteString(603, LegSecurityIDSource);
			if (LegSecAltIDGrp is not null) ((IFixEncoder)LegSecAltIDGrp).Encode(writer);
			if (LegProduct is not null) writer.WriteWholeNumber(607, LegProduct.Value);
			if (LegCFICode is not null) writer.WriteString(608, LegCFICode);
			if (LegSecurityType is not null) writer.WriteString(609, LegSecurityType);
			if (LegSecuritySubType is not null) writer.WriteString(764, LegSecuritySubType);
			if (LegMaturityMonthYear is not null) writer.WriteMonthYear(610, LegMaturityMonthYear.Value);
			if (LegMaturityDate is not null) writer.WriteLocalDateOnly(611, LegMaturityDate.Value);
			if (LegCouponPaymentDate is not null) writer.WriteLocalDateOnly(248, LegCouponPaymentDate.Value);
			if (LegIssueDate is not null) writer.WriteLocalDateOnly(249, LegIssueDate.Value);
			if (LegRepoCollateralSecurityType is not null) writer.WriteString(250, LegRepoCollateralSecurityType);
			if (LegRepurchaseTerm is not null) writer.WriteWholeNumber(251, LegRepurchaseTerm.Value);
			if (LegRepurchaseRate is not null) writer.WriteNumber(252, LegRepurchaseRate.Value);
			if (LegFactor is not null) writer.WriteNumber(253, LegFactor.Value);
			if (LegCreditRating is not null) writer.WriteString(257, LegCreditRating);
			if (LegInstrRegistry is not null) writer.WriteString(599, LegInstrRegistry);
			if (LegCountryOfIssue is not null) writer.WriteString(596, LegCountryOfIssue);
			if (LegStateOrProvinceOfIssue is not null) writer.WriteString(597, LegStateOrProvinceOfIssue);
			if (LegLocaleOfIssue is not null) writer.WriteString(598, LegLocaleOfIssue);
			if (LegRedemptionDate is not null) writer.WriteLocalDateOnly(254, LegRedemptionDate.Value);
			if (LegStrikePrice is not null) writer.WriteNumber(612, LegStrikePrice.Value);
			if (LegStrikeCurrency is not null) writer.WriteString(942, LegStrikeCurrency);
			if (LegOptAttribute is not null) writer.WriteString(613, LegOptAttribute);
			if (LegContractMultiplier is not null) writer.WriteNumber(614, LegContractMultiplier.Value);
			if (LegCouponRate is not null) writer.WriteNumber(615, LegCouponRate.Value);
			if (LegSecurityExchange is not null) writer.WriteString(616, LegSecurityExchange);
			if (LegIssuer is not null) writer.WriteString(617, LegIssuer);
			if (EncodedLegIssuerLen is not null) writer.WriteWholeNumber(618, EncodedLegIssuerLen.Value);
			if (EncodedLegIssuer is not null) writer.WriteBuffer(619, EncodedLegIssuer);
			if (LegSecurityDesc is not null) writer.WriteString(620, LegSecurityDesc);
			if (EncodedLegSecurityDescLen is not null) writer.WriteWholeNumber(621, EncodedLegSecurityDescLen.Value);
			if (EncodedLegSecurityDesc is not null) writer.WriteBuffer(622, EncodedLegSecurityDesc);
			if (LegRatioQty is not null) writer.WriteNumber(623, LegRatioQty.Value);
			if (LegSide is not null) writer.WriteString(624, LegSide);
			if (LegCurrency is not null) writer.WriteString(556, LegCurrency);
			if (LegPool is not null) writer.WriteString(740, LegPool);
			if (LegDatedDate is not null) writer.WriteLocalDateOnly(739, LegDatedDate.Value);
			if (LegContractSettlMonth is not null) writer.WriteMonthYear(955, LegContractSettlMonth.Value);
			if (LegInterestAccrualDate is not null) writer.WriteLocalDateOnly(956, LegInterestAccrualDate.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegSymbol = view.GetString(600);
			LegSymbolSfx = view.GetString(601);
			LegSecurityID = view.GetString(602);
			LegSecurityIDSource = view.GetString(603);
			if (view.GetView("LegSecAltIDGrp") is IMessageView viewLegSecAltIDGrp)
			{
				LegSecAltIDGrp = new();
				((IFixParser)LegSecAltIDGrp).Parse(viewLegSecAltIDGrp);
			}
			LegProduct = view.GetInt32(607);
			LegCFICode = view.GetString(608);
			LegSecurityType = view.GetString(609);
			LegSecuritySubType = view.GetString(764);
			LegMaturityMonthYear = view.GetMonthYear(610);
			LegMaturityDate = view.GetDateOnly(611);
			LegCouponPaymentDate = view.GetDateOnly(248);
			LegIssueDate = view.GetDateOnly(249);
			LegRepoCollateralSecurityType = view.GetString(250);
			LegRepurchaseTerm = view.GetInt32(251);
			LegRepurchaseRate = view.GetDouble(252);
			LegFactor = view.GetDouble(253);
			LegCreditRating = view.GetString(257);
			LegInstrRegistry = view.GetString(599);
			LegCountryOfIssue = view.GetString(596);
			LegStateOrProvinceOfIssue = view.GetString(597);
			LegLocaleOfIssue = view.GetString(598);
			LegRedemptionDate = view.GetDateOnly(254);
			LegStrikePrice = view.GetDouble(612);
			LegStrikeCurrency = view.GetString(942);
			LegOptAttribute = view.GetString(613);
			LegContractMultiplier = view.GetDouble(614);
			LegCouponRate = view.GetDouble(615);
			LegSecurityExchange = view.GetString(616);
			LegIssuer = view.GetString(617);
			EncodedLegIssuerLen = view.GetInt32(618);
			EncodedLegIssuer = view.GetByteArray(619);
			LegSecurityDesc = view.GetString(620);
			EncodedLegSecurityDescLen = view.GetInt32(621);
			EncodedLegSecurityDesc = view.GetByteArray(622);
			LegRatioQty = view.GetDouble(623);
			LegSide = view.GetString(624);
			LegCurrency = view.GetString(556);
			LegPool = view.GetString(740);
			LegDatedDate = view.GetDateOnly(739);
			LegContractSettlMonth = view.GetMonthYear(955);
			LegInterestAccrualDate = view.GetDateOnly(956);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegSymbol":
				{
					value = LegSymbol;
					break;
				}
				case "LegSymbolSfx":
				{
					value = LegSymbolSfx;
					break;
				}
				case "LegSecurityID":
				{
					value = LegSecurityID;
					break;
				}
				case "LegSecurityIDSource":
				{
					value = LegSecurityIDSource;
					break;
				}
				case "LegSecAltIDGrp":
				{
					value = LegSecAltIDGrp;
					break;
				}
				case "LegProduct":
				{
					value = LegProduct;
					break;
				}
				case "LegCFICode":
				{
					value = LegCFICode;
					break;
				}
				case "LegSecurityType":
				{
					value = LegSecurityType;
					break;
				}
				case "LegSecuritySubType":
				{
					value = LegSecuritySubType;
					break;
				}
				case "LegMaturityMonthYear":
				{
					value = LegMaturityMonthYear;
					break;
				}
				case "LegMaturityDate":
				{
					value = LegMaturityDate;
					break;
				}
				case "LegCouponPaymentDate":
				{
					value = LegCouponPaymentDate;
					break;
				}
				case "LegIssueDate":
				{
					value = LegIssueDate;
					break;
				}
				case "LegRepoCollateralSecurityType":
				{
					value = LegRepoCollateralSecurityType;
					break;
				}
				case "LegRepurchaseTerm":
				{
					value = LegRepurchaseTerm;
					break;
				}
				case "LegRepurchaseRate":
				{
					value = LegRepurchaseRate;
					break;
				}
				case "LegFactor":
				{
					value = LegFactor;
					break;
				}
				case "LegCreditRating":
				{
					value = LegCreditRating;
					break;
				}
				case "LegInstrRegistry":
				{
					value = LegInstrRegistry;
					break;
				}
				case "LegCountryOfIssue":
				{
					value = LegCountryOfIssue;
					break;
				}
				case "LegStateOrProvinceOfIssue":
				{
					value = LegStateOrProvinceOfIssue;
					break;
				}
				case "LegLocaleOfIssue":
				{
					value = LegLocaleOfIssue;
					break;
				}
				case "LegRedemptionDate":
				{
					value = LegRedemptionDate;
					break;
				}
				case "LegStrikePrice":
				{
					value = LegStrikePrice;
					break;
				}
				case "LegStrikeCurrency":
				{
					value = LegStrikeCurrency;
					break;
				}
				case "LegOptAttribute":
				{
					value = LegOptAttribute;
					break;
				}
				case "LegContractMultiplier":
				{
					value = LegContractMultiplier;
					break;
				}
				case "LegCouponRate":
				{
					value = LegCouponRate;
					break;
				}
				case "LegSecurityExchange":
				{
					value = LegSecurityExchange;
					break;
				}
				case "LegIssuer":
				{
					value = LegIssuer;
					break;
				}
				case "EncodedLegIssuerLen":
				{
					value = EncodedLegIssuerLen;
					break;
				}
				case "EncodedLegIssuer":
				{
					value = EncodedLegIssuer;
					break;
				}
				case "LegSecurityDesc":
				{
					value = LegSecurityDesc;
					break;
				}
				case "EncodedLegSecurityDescLen":
				{
					value = EncodedLegSecurityDescLen;
					break;
				}
				case "EncodedLegSecurityDesc":
				{
					value = EncodedLegSecurityDesc;
					break;
				}
				case "LegRatioQty":
				{
					value = LegRatioQty;
					break;
				}
				case "LegSide":
				{
					value = LegSide;
					break;
				}
				case "LegCurrency":
				{
					value = LegCurrency;
					break;
				}
				case "LegPool":
				{
					value = LegPool;
					break;
				}
				case "LegDatedDate":
				{
					value = LegDatedDate;
					break;
				}
				case "LegContractSettlMonth":
				{
					value = LegContractSettlMonth;
					break;
				}
				case "LegInterestAccrualDate":
				{
					value = LegInterestAccrualDate;
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
			LegSymbol = null;
			LegSymbolSfx = null;
			LegSecurityID = null;
			LegSecurityIDSource = null;
			((IFixReset?)LegSecAltIDGrp)?.Reset();
			LegProduct = null;
			LegCFICode = null;
			LegSecurityType = null;
			LegSecuritySubType = null;
			LegMaturityMonthYear = null;
			LegMaturityDate = null;
			LegCouponPaymentDate = null;
			LegIssueDate = null;
			LegRepoCollateralSecurityType = null;
			LegRepurchaseTerm = null;
			LegRepurchaseRate = null;
			LegFactor = null;
			LegCreditRating = null;
			LegInstrRegistry = null;
			LegCountryOfIssue = null;
			LegStateOrProvinceOfIssue = null;
			LegLocaleOfIssue = null;
			LegRedemptionDate = null;
			LegStrikePrice = null;
			LegStrikeCurrency = null;
			LegOptAttribute = null;
			LegContractMultiplier = null;
			LegCouponRate = null;
			LegSecurityExchange = null;
			LegIssuer = null;
			EncodedLegIssuerLen = null;
			EncodedLegIssuer = null;
			LegSecurityDesc = null;
			EncodedLegSecurityDescLen = null;
			EncodedLegSecurityDesc = null;
			LegRatioQty = null;
			LegSide = null;
			LegCurrency = null;
			LegPool = null;
			LegDatedDate = null;
			LegContractSettlMonth = null;
			LegInterestAccrualDate = null;
		}
	}
}
