using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public sealed partial class SecurityDefinitionRequestNoRelatedSym : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 311, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingSymbol { get; set; }
		
		[TagDetails(Tag = 312, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingSymbolSfx { get; set; }
		
		[TagDetails(Tag = 309, Type = TagType.String, Offset = 2, Required = false)]
		public string? UnderlyingSecurityID { get; set; }
		
		[TagDetails(Tag = 305, Type = TagType.String, Offset = 3, Required = false)]
		public string? UnderlyingIDSource { get; set; }
		
		[TagDetails(Tag = 310, Type = TagType.String, Offset = 4, Required = false)]
		public string? UnderlyingSecurityType { get; set; }
		
		[TagDetails(Tag = 313, Type = TagType.MonthYear, Offset = 5, Required = false)]
		public MonthYear? UnderlyingMaturityMonthYear { get; set; }
		
		[TagDetails(Tag = 314, Type = TagType.String, Offset = 6, Required = false)]
		public string? UnderlyingMaturityDay { get; set; }
		
		[TagDetails(Tag = 315, Type = TagType.Int, Offset = 7, Required = false)]
		public int? UnderlyingPutOrCall { get; set; }
		
		[TagDetails(Tag = 316, Type = TagType.Float, Offset = 8, Required = false)]
		public double? UnderlyingStrikePrice { get; set; }
		
		[TagDetails(Tag = 317, Type = TagType.String, Offset = 9, Required = false)]
		public string? UnderlyingOptAttribute { get; set; }
		
		[TagDetails(Tag = 436, Type = TagType.Float, Offset = 10, Required = false)]
		public double? UnderlyingContractMultiplier { get; set; }
		
		[TagDetails(Tag = 435, Type = TagType.Float, Offset = 11, Required = false)]
		public double? UnderlyingCouponRate { get; set; }
		
		[TagDetails(Tag = 308, Type = TagType.String, Offset = 12, Required = false)]
		public string? UnderlyingSecurityExchange { get; set; }
		
		[TagDetails(Tag = 306, Type = TagType.String, Offset = 13, Required = false)]
		public string? UnderlyingIssuer { get; set; }
		
		[TagDetails(Tag = 362, Type = TagType.Length, Offset = 14, Required = false, LinksToTag = 363)]
		public int? EncodedUnderlyingIssuerLen { get; set; }
		
		[TagDetails(Tag = 363, Type = TagType.RawData, Offset = 15, Required = false, LinksToTag = 362)]
		public byte[]? EncodedUnderlyingIssuer { get; set; }
		
		[TagDetails(Tag = 307, Type = TagType.String, Offset = 16, Required = false)]
		public string? UnderlyingSecurityDesc { get; set; }
		
		[TagDetails(Tag = 364, Type = TagType.Length, Offset = 17, Required = false, LinksToTag = 365)]
		public int? EncodedUnderlyingSecurityDescLen { get; set; }
		
		[TagDetails(Tag = 365, Type = TagType.RawData, Offset = 18, Required = false, LinksToTag = 364)]
		public byte[]? EncodedUnderlyingSecurityDesc { get; set; }
		
		[TagDetails(Tag = 319, Type = TagType.Float, Offset = 19, Required = false)]
		public double? RatioQty { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 20, Required = false)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 318, Type = TagType.String, Offset = 21, Required = false)]
		public string? UnderlyingCurrency { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingSymbol is not null) writer.WriteString(311, UnderlyingSymbol);
			if (UnderlyingSymbolSfx is not null) writer.WriteString(312, UnderlyingSymbolSfx);
			if (UnderlyingSecurityID is not null) writer.WriteString(309, UnderlyingSecurityID);
			if (UnderlyingIDSource is not null) writer.WriteString(305, UnderlyingIDSource);
			if (UnderlyingSecurityType is not null) writer.WriteString(310, UnderlyingSecurityType);
			if (UnderlyingMaturityMonthYear is not null) writer.WriteMonthYear(313, UnderlyingMaturityMonthYear.Value);
			if (UnderlyingMaturityDay is not null) writer.WriteString(314, UnderlyingMaturityDay);
			if (UnderlyingPutOrCall is not null) writer.WriteWholeNumber(315, UnderlyingPutOrCall.Value);
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
			if (RatioQty is not null) writer.WriteNumber(319, RatioQty.Value);
			if (Side is not null) writer.WriteString(54, Side);
			if (UnderlyingCurrency is not null) writer.WriteString(318, UnderlyingCurrency);
		}
	}
}
