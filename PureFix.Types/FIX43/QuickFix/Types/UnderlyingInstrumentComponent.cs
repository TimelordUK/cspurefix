using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
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
		
		[Group(NoOfTag = 457, Offset = 4, Required = false)]
		public MassQuoteAcknowledgementNoUnderlyingSecurityAltID[]? NoUnderlyingSecurityAltID {get; set;}
		
		[TagDetails(Tag = 462, Type = TagType.Int, Offset = 5, Required = false)]
		public int? UnderlyingProduct {get; set;}
		
		[TagDetails(Tag = 463, Type = TagType.String, Offset = 6, Required = false)]
		public string? UnderlyingCFICode {get; set;}
		
		[TagDetails(Tag = 310, Type = TagType.String, Offset = 7, Required = false)]
		public string? UnderlyingSecurityType {get; set;}
		
		[TagDetails(Tag = 313, Type = TagType.MonthYear, Offset = 8, Required = false)]
		public MonthYear? UnderlyingMaturityMonthYear {get; set;}
		
		[TagDetails(Tag = 542, Type = TagType.LocalDate, Offset = 9, Required = false)]
		public DateOnly? UnderlyingMaturityDate {get; set;}
		
		[TagDetails(Tag = 315, Type = TagType.Int, Offset = 10, Required = false)]
		public int? UnderlyingPutOrCall {get; set;}
		
		[TagDetails(Tag = 241, Type = TagType.String, Offset = 11, Required = false)]
		public string? UnderlyingCouponPaymentDate {get; set;}
		
		[TagDetails(Tag = 242, Type = TagType.String, Offset = 12, Required = false)]
		public string? UnderlyingIssueDate {get; set;}
		
		[TagDetails(Tag = 243, Type = TagType.String, Offset = 13, Required = false)]
		public string? UnderlyingRepoCollateralSecurityType {get; set;}
		
		[TagDetails(Tag = 244, Type = TagType.Int, Offset = 14, Required = false)]
		public int? UnderlyingRepurchaseTerm {get; set;}
		
		[TagDetails(Tag = 245, Type = TagType.Float, Offset = 15, Required = false)]
		public double? UnderlyingRepurchaseRate {get; set;}
		
		[TagDetails(Tag = 246, Type = TagType.Float, Offset = 16, Required = false)]
		public double? UnderlyingFactor {get; set;}
		
		[TagDetails(Tag = 256, Type = TagType.String, Offset = 17, Required = false)]
		public string? UnderlyingCreditRating {get; set;}
		
		[TagDetails(Tag = 595, Type = TagType.String, Offset = 18, Required = false)]
		public string? UnderlyingInstrRegistry {get; set;}
		
		[TagDetails(Tag = 592, Type = TagType.String, Offset = 19, Required = false)]
		public string? UnderlyingCountryOfIssue {get; set;}
		
		[TagDetails(Tag = 593, Type = TagType.String, Offset = 20, Required = false)]
		public string? UnderlyingStateOrProvinceOfIssue {get; set;}
		
		[TagDetails(Tag = 594, Type = TagType.String, Offset = 21, Required = false)]
		public string? UnderlyingLocaleOfIssue {get; set;}
		
		[TagDetails(Tag = 247, Type = TagType.String, Offset = 22, Required = false)]
		public string? UnderlyingRedemptionDate {get; set;}
		
		[TagDetails(Tag = 316, Type = TagType.Float, Offset = 23, Required = false)]
		public double? UnderlyingStrikePrice {get; set;}
		
		[TagDetails(Tag = 317, Type = TagType.String, Offset = 24, Required = false)]
		public string? UnderlyingOptAttribute {get; set;}
		
		[TagDetails(Tag = 436, Type = TagType.Float, Offset = 25, Required = false)]
		public double? UnderlyingContractMultiplier {get; set;}
		
		[TagDetails(Tag = 435, Type = TagType.Float, Offset = 26, Required = false)]
		public double? UnderlyingCouponRate {get; set;}
		
		[TagDetails(Tag = 308, Type = TagType.String, Offset = 27, Required = false)]
		public string? UnderlyingSecurityExchange {get; set;}
		
		[TagDetails(Tag = 306, Type = TagType.String, Offset = 28, Required = false)]
		public string? UnderlyingIssuer {get; set;}
		
		[TagDetails(Tag = 362, Type = TagType.Length, Offset = 29, Required = false, LinksToTag = 363)]
		public int? EncodedUnderlyingIssuerLen {get; set;}
		
		[TagDetails(Tag = 363, Type = TagType.RawData, Offset = 30, Required = false, LinksToTag = 362)]
		public byte[]? EncodedUnderlyingIssuer {get; set;}
		
		[TagDetails(Tag = 307, Type = TagType.String, Offset = 31, Required = false)]
		public string? UnderlyingSecurityDesc {get; set;}
		
		[TagDetails(Tag = 364, Type = TagType.Length, Offset = 32, Required = false, LinksToTag = 365)]
		public int? EncodedUnderlyingSecurityDescLen {get; set;}
		
		[TagDetails(Tag = 365, Type = TagType.RawData, Offset = 33, Required = false, LinksToTag = 364)]
		public byte[]? EncodedUnderlyingSecurityDesc {get; set;}
		
		
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
			if (NoUnderlyingSecurityAltID is not null && NoUnderlyingSecurityAltID.Length != 0)
			{
				writer.WriteWholeNumber(457, NoUnderlyingSecurityAltID.Length);
				for (int i = 0; i < NoUnderlyingSecurityAltID.Length; i++)
				{
					((IFixEncoder)NoUnderlyingSecurityAltID[i]).Encode(writer);
				}
			}
			if (UnderlyingProduct is not null) writer.WriteWholeNumber(462, UnderlyingProduct.Value);
			if (UnderlyingCFICode is not null) writer.WriteString(463, UnderlyingCFICode);
			if (UnderlyingSecurityType is not null) writer.WriteString(310, UnderlyingSecurityType);
			if (UnderlyingMaturityMonthYear is not null) writer.WriteMonthYear(313, UnderlyingMaturityMonthYear.Value);
			if (UnderlyingMaturityDate is not null) writer.WriteLocalDateOnly(542, UnderlyingMaturityDate.Value);
			if (UnderlyingPutOrCall is not null) writer.WriteWholeNumber(315, UnderlyingPutOrCall.Value);
			if (UnderlyingCouponPaymentDate is not null) writer.WriteString(241, UnderlyingCouponPaymentDate);
			if (UnderlyingIssueDate is not null) writer.WriteString(242, UnderlyingIssueDate);
			if (UnderlyingRepoCollateralSecurityType is not null) writer.WriteString(243, UnderlyingRepoCollateralSecurityType);
			if (UnderlyingRepurchaseTerm is not null) writer.WriteWholeNumber(244, UnderlyingRepurchaseTerm.Value);
			if (UnderlyingRepurchaseRate is not null) writer.WriteNumber(245, UnderlyingRepurchaseRate.Value);
			if (UnderlyingFactor is not null) writer.WriteNumber(246, UnderlyingFactor.Value);
			if (UnderlyingCreditRating is not null) writer.WriteString(256, UnderlyingCreditRating);
			if (UnderlyingInstrRegistry is not null) writer.WriteString(595, UnderlyingInstrRegistry);
			if (UnderlyingCountryOfIssue is not null) writer.WriteString(592, UnderlyingCountryOfIssue);
			if (UnderlyingStateOrProvinceOfIssue is not null) writer.WriteString(593, UnderlyingStateOrProvinceOfIssue);
			if (UnderlyingLocaleOfIssue is not null) writer.WriteString(594, UnderlyingLocaleOfIssue);
			if (UnderlyingRedemptionDate is not null) writer.WriteString(247, UnderlyingRedemptionDate);
			if (UnderlyingStrikePrice is not null) writer.WriteNumber(316, UnderlyingStrikePrice.Value);
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
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingSymbol = view.GetString(311);
			UnderlyingSymbolSfx = view.GetString(312);
			UnderlyingSecurityID = view.GetString(309);
			UnderlyingSecurityIDSource = view.GetString(305);
			if (view.GetView("NoUnderlyingSecurityAltID") is IMessageView viewNoUnderlyingSecurityAltID)
			{
				var count = viewNoUnderlyingSecurityAltID.GroupCount();
				NoUnderlyingSecurityAltID = new MassQuoteAcknowledgementNoUnderlyingSecurityAltID[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingSecurityAltID[i] = new();
					((IFixParser)NoUnderlyingSecurityAltID[i]).Parse(viewNoUnderlyingSecurityAltID.GetGroupInstance(i));
				}
			}
			UnderlyingProduct = view.GetInt32(462);
			UnderlyingCFICode = view.GetString(463);
			UnderlyingSecurityType = view.GetString(310);
			UnderlyingMaturityMonthYear = view.GetMonthYear(313);
			UnderlyingMaturityDate = view.GetDateOnly(542);
			UnderlyingPutOrCall = view.GetInt32(315);
			UnderlyingCouponPaymentDate = view.GetString(241);
			UnderlyingIssueDate = view.GetString(242);
			UnderlyingRepoCollateralSecurityType = view.GetString(243);
			UnderlyingRepurchaseTerm = view.GetInt32(244);
			UnderlyingRepurchaseRate = view.GetDouble(245);
			UnderlyingFactor = view.GetDouble(246);
			UnderlyingCreditRating = view.GetString(256);
			UnderlyingInstrRegistry = view.GetString(595);
			UnderlyingCountryOfIssue = view.GetString(592);
			UnderlyingStateOrProvinceOfIssue = view.GetString(593);
			UnderlyingLocaleOfIssue = view.GetString(594);
			UnderlyingRedemptionDate = view.GetString(247);
			UnderlyingStrikePrice = view.GetDouble(316);
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
				case "NoUnderlyingSecurityAltID":
					value = NoUnderlyingSecurityAltID;
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
			NoUnderlyingSecurityAltID = null;
			UnderlyingProduct = null;
			UnderlyingCFICode = null;
			UnderlyingSecurityType = null;
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
		}
	}
}
