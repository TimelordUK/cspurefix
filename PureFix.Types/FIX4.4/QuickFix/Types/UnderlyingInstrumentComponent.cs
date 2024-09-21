using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class UnderlyingInstrumentComponent : IFixComponent
	{
		[TagDetails(Tag = 311, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingSymbol {get; set;}
		
		[TagDetails(Tag = 312, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingSymbolSfx {get; set;}
		
		[TagDetails(Tag = 309, Type = TagType.String, Offset = 2, Required = false)]
		public string? UnderlyingSecurityID {get; set;}
		
		[TagDetails(Tag = 305, Type = TagType.String, Offset = 3, Required = false)]
		public string? UnderlyingSecurityIDSource {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public UndSecAltIDGrpComponent? UndSecAltIDGrp {get; set;}
		
		[TagDetails(Tag = 462, Type = TagType.Int, Offset = 5, Required = false)]
		public int? UnderlyingProduct {get; set;}
		
		[TagDetails(Tag = 463, Type = TagType.String, Offset = 6, Required = false)]
		public string? UnderlyingCFICode {get; set;}
		
		[TagDetails(Tag = 310, Type = TagType.String, Offset = 7, Required = false)]
		public string? UnderlyingSecurityType {get; set;}
		
		[TagDetails(Tag = 763, Type = TagType.String, Offset = 8, Required = false)]
		public string? UnderlyingSecuritySubType {get; set;}
		
		[TagDetails(Tag = 313, Type = TagType.MonthYear, Offset = 9, Required = false)]
		public MonthYear? UnderlyingMaturityMonthYear {get; set;}
		
		[TagDetails(Tag = 542, Type = TagType.LocalDate, Offset = 10, Required = false)]
		public DateOnly? UnderlyingMaturityDate {get; set;}
		
		[TagDetails(Tag = 315, Type = TagType.Int, Offset = 11, Required = false)]
		public int? UnderlyingPutOrCall {get; set;}
		
		[TagDetails(Tag = 241, Type = TagType.LocalDate, Offset = 12, Required = false)]
		public DateOnly? UnderlyingCouponPaymentDate {get; set;}
		
		[TagDetails(Tag = 242, Type = TagType.LocalDate, Offset = 13, Required = false)]
		public DateOnly? UnderlyingIssueDate {get; set;}
		
		[TagDetails(Tag = 243, Type = TagType.String, Offset = 14, Required = false)]
		public string? UnderlyingRepoCollateralSecurityType {get; set;}
		
		[TagDetails(Tag = 244, Type = TagType.Int, Offset = 15, Required = false)]
		public int? UnderlyingRepurchaseTerm {get; set;}
		
		[TagDetails(Tag = 245, Type = TagType.Float, Offset = 16, Required = false)]
		public double? UnderlyingRepurchaseRate {get; set;}
		
		[TagDetails(Tag = 246, Type = TagType.Float, Offset = 17, Required = false)]
		public double? UnderlyingFactor {get; set;}
		
		[TagDetails(Tag = 256, Type = TagType.String, Offset = 18, Required = false)]
		public string? UnderlyingCreditRating {get; set;}
		
		[TagDetails(Tag = 595, Type = TagType.String, Offset = 19, Required = false)]
		public string? UnderlyingInstrRegistry {get; set;}
		
		[TagDetails(Tag = 592, Type = TagType.String, Offset = 20, Required = false)]
		public string? UnderlyingCountryOfIssue {get; set;}
		
		[TagDetails(Tag = 593, Type = TagType.String, Offset = 21, Required = false)]
		public string? UnderlyingStateOrProvinceOfIssue {get; set;}
		
		[TagDetails(Tag = 594, Type = TagType.String, Offset = 22, Required = false)]
		public string? UnderlyingLocaleOfIssue {get; set;}
		
		[TagDetails(Tag = 247, Type = TagType.LocalDate, Offset = 23, Required = false)]
		public DateOnly? UnderlyingRedemptionDate {get; set;}
		
		[TagDetails(Tag = 316, Type = TagType.Float, Offset = 24, Required = false)]
		public double? UnderlyingStrikePrice {get; set;}
		
		[TagDetails(Tag = 941, Type = TagType.String, Offset = 25, Required = false)]
		public string? UnderlyingStrikeCurrency {get; set;}
		
		[TagDetails(Tag = 317, Type = TagType.String, Offset = 26, Required = false)]
		public string? UnderlyingOptAttribute {get; set;}
		
		[TagDetails(Tag = 436, Type = TagType.Float, Offset = 27, Required = false)]
		public double? UnderlyingContractMultiplier {get; set;}
		
		[TagDetails(Tag = 435, Type = TagType.Float, Offset = 28, Required = false)]
		public double? UnderlyingCouponRate {get; set;}
		
		[TagDetails(Tag = 308, Type = TagType.String, Offset = 29, Required = false)]
		public string? UnderlyingSecurityExchange {get; set;}
		
		[TagDetails(Tag = 306, Type = TagType.String, Offset = 30, Required = false)]
		public string? UnderlyingIssuer {get; set;}
		
		[TagDetails(Tag = 362, Type = TagType.Length, Offset = 31, Required = false, LinksToTag = 363)]
		public int? EncodedUnderlyingIssuerLen {get; set;}
		
		[TagDetails(Tag = 363, Type = TagType.RawData, Offset = 32, Required = false, LinksToTag = 362)]
		public byte[]? EncodedUnderlyingIssuer {get; set;}
		
		[TagDetails(Tag = 307, Type = TagType.String, Offset = 33, Required = false)]
		public string? UnderlyingSecurityDesc {get; set;}
		
		[TagDetails(Tag = 364, Type = TagType.Length, Offset = 34, Required = false, LinksToTag = 365)]
		public int? EncodedUnderlyingSecurityDescLen {get; set;}
		
		[TagDetails(Tag = 365, Type = TagType.RawData, Offset = 35, Required = false, LinksToTag = 364)]
		public byte[]? EncodedUnderlyingSecurityDesc {get; set;}
		
		[TagDetails(Tag = 877, Type = TagType.String, Offset = 36, Required = false)]
		public string? UnderlyingCPProgram {get; set;}
		
		[TagDetails(Tag = 878, Type = TagType.String, Offset = 37, Required = false)]
		public string? UnderlyingCPRegType {get; set;}
		
		[TagDetails(Tag = 318, Type = TagType.String, Offset = 38, Required = false)]
		public string? UnderlyingCurrency {get; set;}
		
		[TagDetails(Tag = 879, Type = TagType.Float, Offset = 39, Required = false)]
		public double? UnderlyingQty {get; set;}
		
		[TagDetails(Tag = 810, Type = TagType.Float, Offset = 40, Required = false)]
		public double? UnderlyingPx {get; set;}
		
		[TagDetails(Tag = 882, Type = TagType.Float, Offset = 41, Required = false)]
		public double? UnderlyingDirtyPrice {get; set;}
		
		[TagDetails(Tag = 883, Type = TagType.Float, Offset = 42, Required = false)]
		public double? UnderlyingEndPrice {get; set;}
		
		[TagDetails(Tag = 884, Type = TagType.Float, Offset = 43, Required = false)]
		public double? UnderlyingStartValue {get; set;}
		
		[TagDetails(Tag = 885, Type = TagType.Float, Offset = 44, Required = false)]
		public double? UnderlyingCurrentValue {get; set;}
		
		[TagDetails(Tag = 886, Type = TagType.Float, Offset = 45, Required = false)]
		public double? UnderlyingEndValue {get; set;}
		
		[Component(Offset = 46, Required = false)]
		public UnderlyingStipulationsComponent? UnderlyingStipulations {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingSymbol is not null) writer.WriteString(311, UnderlyingSymbol);
			if (UnderlyingSymbolSfx is not null) writer.WriteString(312, UnderlyingSymbolSfx);
			if (UnderlyingSecurityID is not null) writer.WriteString(309, UnderlyingSecurityID);
			if (UnderlyingSecurityIDSource is not null) writer.WriteString(305, UnderlyingSecurityIDSource);
			if (UndSecAltIDGrp is not null) ((IFixEncoder)UndSecAltIDGrp).Encode(writer);
			if (UnderlyingProduct is not null) writer.WriteWholeNumber(462, UnderlyingProduct.Value);
			if (UnderlyingCFICode is not null) writer.WriteString(463, UnderlyingCFICode);
			if (UnderlyingSecurityType is not null) writer.WriteString(310, UnderlyingSecurityType);
			if (UnderlyingSecuritySubType is not null) writer.WriteString(763, UnderlyingSecuritySubType);
			if (UnderlyingMaturityMonthYear is not null) writer.WriteMonthYear(313, UnderlyingMaturityMonthYear.Value);
			if (UnderlyingMaturityDate is not null) writer.WriteLocalDateOnly(542, UnderlyingMaturityDate.Value);
			if (UnderlyingPutOrCall is not null) writer.WriteWholeNumber(315, UnderlyingPutOrCall.Value);
			if (UnderlyingCouponPaymentDate is not null) writer.WriteLocalDateOnly(241, UnderlyingCouponPaymentDate.Value);
			if (UnderlyingIssueDate is not null) writer.WriteLocalDateOnly(242, UnderlyingIssueDate.Value);
			if (UnderlyingRepoCollateralSecurityType is not null) writer.WriteString(243, UnderlyingRepoCollateralSecurityType);
			if (UnderlyingRepurchaseTerm is not null) writer.WriteWholeNumber(244, UnderlyingRepurchaseTerm.Value);
			if (UnderlyingRepurchaseRate is not null) writer.WriteNumber(245, UnderlyingRepurchaseRate.Value);
			if (UnderlyingFactor is not null) writer.WriteNumber(246, UnderlyingFactor.Value);
			if (UnderlyingCreditRating is not null) writer.WriteString(256, UnderlyingCreditRating);
			if (UnderlyingInstrRegistry is not null) writer.WriteString(595, UnderlyingInstrRegistry);
			if (UnderlyingCountryOfIssue is not null) writer.WriteString(592, UnderlyingCountryOfIssue);
			if (UnderlyingStateOrProvinceOfIssue is not null) writer.WriteString(593, UnderlyingStateOrProvinceOfIssue);
			if (UnderlyingLocaleOfIssue is not null) writer.WriteString(594, UnderlyingLocaleOfIssue);
			if (UnderlyingRedemptionDate is not null) writer.WriteLocalDateOnly(247, UnderlyingRedemptionDate.Value);
			if (UnderlyingStrikePrice is not null) writer.WriteNumber(316, UnderlyingStrikePrice.Value);
			if (UnderlyingStrikeCurrency is not null) writer.WriteString(941, UnderlyingStrikeCurrency);
			if (UnderlyingOptAttribute is not null) writer.WriteString(317, UnderlyingOptAttribute);
			if (UnderlyingContractMultiplier is not null) writer.WriteNumber(436, UnderlyingContractMultiplier.Value);
			if (UnderlyingCouponRate is not null) writer.WriteNumber(435, UnderlyingCouponRate.Value);
			if (UnderlyingSecurityExchange is not null) writer.WriteString(308, UnderlyingSecurityExchange);
			if (UnderlyingIssuer is not null) writer.WriteString(306, UnderlyingIssuer);
			if (EncodedUnderlyingIssuer is not null)
			{
				writer.WriteWholeNumber(362, EncodedUnderlyingIssuer.Length);
				writer.WriteBuffer(363, EncodedUnderlyingIssuer);
			}
			if (UnderlyingSecurityDesc is not null) writer.WriteString(307, UnderlyingSecurityDesc);
			if (EncodedUnderlyingSecurityDesc is not null)
			{
				writer.WriteWholeNumber(364, EncodedUnderlyingSecurityDesc.Length);
				writer.WriteBuffer(365, EncodedUnderlyingSecurityDesc);
			}
			if (UnderlyingCPProgram is not null) writer.WriteString(877, UnderlyingCPProgram);
			if (UnderlyingCPRegType is not null) writer.WriteString(878, UnderlyingCPRegType);
			if (UnderlyingCurrency is not null) writer.WriteString(318, UnderlyingCurrency);
			if (UnderlyingQty is not null) writer.WriteNumber(879, UnderlyingQty.Value);
			if (UnderlyingPx is not null) writer.WriteNumber(810, UnderlyingPx.Value);
			if (UnderlyingDirtyPrice is not null) writer.WriteNumber(882, UnderlyingDirtyPrice.Value);
			if (UnderlyingEndPrice is not null) writer.WriteNumber(883, UnderlyingEndPrice.Value);
			if (UnderlyingStartValue is not null) writer.WriteNumber(884, UnderlyingStartValue.Value);
			if (UnderlyingCurrentValue is not null) writer.WriteNumber(885, UnderlyingCurrentValue.Value);
			if (UnderlyingEndValue is not null) writer.WriteNumber(886, UnderlyingEndValue.Value);
			if (UnderlyingStipulations is not null) ((IFixEncoder)UnderlyingStipulations).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingSymbol = view.GetString(311);
			UnderlyingSymbolSfx = view.GetString(312);
			UnderlyingSecurityID = view.GetString(309);
			UnderlyingSecurityIDSource = view.GetString(305);
			if (view.GetView("UndSecAltIDGrp") is IMessageView viewUndSecAltIDGrp)
			{
				UndSecAltIDGrp = new();
				((IFixParser)UndSecAltIDGrp).Parse(viewUndSecAltIDGrp);
			}
			UnderlyingProduct = view.GetInt32(462);
			UnderlyingCFICode = view.GetString(463);
			UnderlyingSecurityType = view.GetString(310);
			UnderlyingSecuritySubType = view.GetString(763);
			UnderlyingMaturityMonthYear = view.GetMonthYear(313);
			UnderlyingMaturityDate = view.GetDateOnly(542);
			UnderlyingPutOrCall = view.GetInt32(315);
			UnderlyingCouponPaymentDate = view.GetDateOnly(241);
			UnderlyingIssueDate = view.GetDateOnly(242);
			UnderlyingRepoCollateralSecurityType = view.GetString(243);
			UnderlyingRepurchaseTerm = view.GetInt32(244);
			UnderlyingRepurchaseRate = view.GetDouble(245);
			UnderlyingFactor = view.GetDouble(246);
			UnderlyingCreditRating = view.GetString(256);
			UnderlyingInstrRegistry = view.GetString(595);
			UnderlyingCountryOfIssue = view.GetString(592);
			UnderlyingStateOrProvinceOfIssue = view.GetString(593);
			UnderlyingLocaleOfIssue = view.GetString(594);
			UnderlyingRedemptionDate = view.GetDateOnly(247);
			UnderlyingStrikePrice = view.GetDouble(316);
			UnderlyingStrikeCurrency = view.GetString(941);
			UnderlyingOptAttribute = view.GetString(317);
			UnderlyingContractMultiplier = view.GetDouble(436);
			UnderlyingCouponRate = view.GetDouble(435);
			UnderlyingSecurityExchange = view.GetString(308);
			UnderlyingIssuer = view.GetString(306);
			EncodedUnderlyingIssuerLen = view.GetInt32(362);
			EncodedUnderlyingIssuer = view.GetByteArray(363);
			UnderlyingSecurityDesc = view.GetString(307);
			EncodedUnderlyingSecurityDescLen = view.GetInt32(364);
			EncodedUnderlyingSecurityDesc = view.GetByteArray(365);
			UnderlyingCPProgram = view.GetString(877);
			UnderlyingCPRegType = view.GetString(878);
			UnderlyingCurrency = view.GetString(318);
			UnderlyingQty = view.GetDouble(879);
			UnderlyingPx = view.GetDouble(810);
			UnderlyingDirtyPrice = view.GetDouble(882);
			UnderlyingEndPrice = view.GetDouble(883);
			UnderlyingStartValue = view.GetDouble(884);
			UnderlyingCurrentValue = view.GetDouble(885);
			UnderlyingEndValue = view.GetDouble(886);
			if (view.GetView("UnderlyingStipulations") is IMessageView viewUnderlyingStipulations)
			{
				UnderlyingStipulations = new();
				((IFixParser)UnderlyingStipulations).Parse(viewUnderlyingStipulations);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingSymbol":
					value = UnderlyingSymbol;
					break;
				case "UnderlyingSymbolSfx":
					value = UnderlyingSymbolSfx;
					break;
				case "UnderlyingSecurityID":
					value = UnderlyingSecurityID;
					break;
				case "UnderlyingSecurityIDSource":
					value = UnderlyingSecurityIDSource;
					break;
				case "UndSecAltIDGrp":
					value = UndSecAltIDGrp;
					break;
				case "UnderlyingProduct":
					value = UnderlyingProduct;
					break;
				case "UnderlyingCFICode":
					value = UnderlyingCFICode;
					break;
				case "UnderlyingSecurityType":
					value = UnderlyingSecurityType;
					break;
				case "UnderlyingSecuritySubType":
					value = UnderlyingSecuritySubType;
					break;
				case "UnderlyingMaturityMonthYear":
					value = UnderlyingMaturityMonthYear;
					break;
				case "UnderlyingMaturityDate":
					value = UnderlyingMaturityDate;
					break;
				case "UnderlyingPutOrCall":
					value = UnderlyingPutOrCall;
					break;
				case "UnderlyingCouponPaymentDate":
					value = UnderlyingCouponPaymentDate;
					break;
				case "UnderlyingIssueDate":
					value = UnderlyingIssueDate;
					break;
				case "UnderlyingRepoCollateralSecurityType":
					value = UnderlyingRepoCollateralSecurityType;
					break;
				case "UnderlyingRepurchaseTerm":
					value = UnderlyingRepurchaseTerm;
					break;
				case "UnderlyingRepurchaseRate":
					value = UnderlyingRepurchaseRate;
					break;
				case "UnderlyingFactor":
					value = UnderlyingFactor;
					break;
				case "UnderlyingCreditRating":
					value = UnderlyingCreditRating;
					break;
				case "UnderlyingInstrRegistry":
					value = UnderlyingInstrRegistry;
					break;
				case "UnderlyingCountryOfIssue":
					value = UnderlyingCountryOfIssue;
					break;
				case "UnderlyingStateOrProvinceOfIssue":
					value = UnderlyingStateOrProvinceOfIssue;
					break;
				case "UnderlyingLocaleOfIssue":
					value = UnderlyingLocaleOfIssue;
					break;
				case "UnderlyingRedemptionDate":
					value = UnderlyingRedemptionDate;
					break;
				case "UnderlyingStrikePrice":
					value = UnderlyingStrikePrice;
					break;
				case "UnderlyingStrikeCurrency":
					value = UnderlyingStrikeCurrency;
					break;
				case "UnderlyingOptAttribute":
					value = UnderlyingOptAttribute;
					break;
				case "UnderlyingContractMultiplier":
					value = UnderlyingContractMultiplier;
					break;
				case "UnderlyingCouponRate":
					value = UnderlyingCouponRate;
					break;
				case "UnderlyingSecurityExchange":
					value = UnderlyingSecurityExchange;
					break;
				case "UnderlyingIssuer":
					value = UnderlyingIssuer;
					break;
				case "EncodedUnderlyingIssuerLen":
					value = EncodedUnderlyingIssuerLen;
					break;
				case "EncodedUnderlyingIssuer":
					value = EncodedUnderlyingIssuer;
					break;
				case "UnderlyingSecurityDesc":
					value = UnderlyingSecurityDesc;
					break;
				case "EncodedUnderlyingSecurityDescLen":
					value = EncodedUnderlyingSecurityDescLen;
					break;
				case "EncodedUnderlyingSecurityDesc":
					value = EncodedUnderlyingSecurityDesc;
					break;
				case "UnderlyingCPProgram":
					value = UnderlyingCPProgram;
					break;
				case "UnderlyingCPRegType":
					value = UnderlyingCPRegType;
					break;
				case "UnderlyingCurrency":
					value = UnderlyingCurrency;
					break;
				case "UnderlyingQty":
					value = UnderlyingQty;
					break;
				case "UnderlyingPx":
					value = UnderlyingPx;
					break;
				case "UnderlyingDirtyPrice":
					value = UnderlyingDirtyPrice;
					break;
				case "UnderlyingEndPrice":
					value = UnderlyingEndPrice;
					break;
				case "UnderlyingStartValue":
					value = UnderlyingStartValue;
					break;
				case "UnderlyingCurrentValue":
					value = UnderlyingCurrentValue;
					break;
				case "UnderlyingEndValue":
					value = UnderlyingEndValue;
					break;
				case "UnderlyingStipulations":
					value = UnderlyingStipulations;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingSymbol = null;
			UnderlyingSymbolSfx = null;
			UnderlyingSecurityID = null;
			UnderlyingSecurityIDSource = null;
			((IFixReset?)UndSecAltIDGrp)?.Reset();
			UnderlyingProduct = null;
			UnderlyingCFICode = null;
			UnderlyingSecurityType = null;
			UnderlyingSecuritySubType = null;
			UnderlyingMaturityMonthYear = null;
			UnderlyingMaturityDate = null;
			UnderlyingPutOrCall = null;
			UnderlyingCouponPaymentDate = null;
			UnderlyingIssueDate = null;
			UnderlyingRepoCollateralSecurityType = null;
			UnderlyingRepurchaseTerm = null;
			UnderlyingRepurchaseRate = null;
			UnderlyingFactor = null;
			UnderlyingCreditRating = null;
			UnderlyingInstrRegistry = null;
			UnderlyingCountryOfIssue = null;
			UnderlyingStateOrProvinceOfIssue = null;
			UnderlyingLocaleOfIssue = null;
			UnderlyingRedemptionDate = null;
			UnderlyingStrikePrice = null;
			UnderlyingStrikeCurrency = null;
			UnderlyingOptAttribute = null;
			UnderlyingContractMultiplier = null;
			UnderlyingCouponRate = null;
			UnderlyingSecurityExchange = null;
			UnderlyingIssuer = null;
			EncodedUnderlyingIssuerLen = null;
			EncodedUnderlyingIssuer = null;
			UnderlyingSecurityDesc = null;
			EncodedUnderlyingSecurityDescLen = null;
			EncodedUnderlyingSecurityDesc = null;
			UnderlyingCPProgram = null;
			UnderlyingCPRegType = null;
			UnderlyingCurrency = null;
			UnderlyingQty = null;
			UnderlyingPx = null;
			UnderlyingDirtyPrice = null;
			UnderlyingEndPrice = null;
			UnderlyingStartValue = null;
			UnderlyingCurrentValue = null;
			UnderlyingEndValue = null;
			((IFixReset?)UnderlyingStipulations)?.Reset();
		}
	}
}
