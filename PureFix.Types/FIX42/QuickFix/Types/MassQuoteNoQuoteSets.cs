using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public sealed partial class MassQuoteNoQuoteSets : IFixGroup
	{
		[TagDetails(Tag = 302, Type = TagType.String, Offset = 0, Required = true)]
		public string? QuoteSetID {get; set;}
		
		[TagDetails(Tag = 311, Type = TagType.String, Offset = 1, Required = true)]
		public string? UnderlyingSymbol {get; set;}
		
		[TagDetails(Tag = 312, Type = TagType.String, Offset = 2, Required = false)]
		public string? UnderlyingSymbolSfx {get; set;}
		
		[TagDetails(Tag = 309, Type = TagType.String, Offset = 3, Required = false)]
		public string? UnderlyingSecurityID {get; set;}
		
		[TagDetails(Tag = 305, Type = TagType.String, Offset = 4, Required = false)]
		public string? UnderlyingIDSource {get; set;}
		
		[TagDetails(Tag = 310, Type = TagType.String, Offset = 5, Required = false)]
		public string? UnderlyingSecurityType {get; set;}
		
		[TagDetails(Tag = 313, Type = TagType.MonthYear, Offset = 6, Required = false)]
		public MonthYear? UnderlyingMaturityMonthYear {get; set;}
		
		[TagDetails(Tag = 314, Type = TagType.String, Offset = 7, Required = false)]
		public string? UnderlyingMaturityDay {get; set;}
		
		[TagDetails(Tag = 315, Type = TagType.Int, Offset = 8, Required = false)]
		public int? UnderlyingPutOrCall {get; set;}
		
		[TagDetails(Tag = 316, Type = TagType.Float, Offset = 9, Required = false)]
		public double? UnderlyingStrikePrice {get; set;}
		
		[TagDetails(Tag = 317, Type = TagType.String, Offset = 10, Required = false)]
		public string? UnderlyingOptAttribute {get; set;}
		
		[TagDetails(Tag = 436, Type = TagType.Float, Offset = 11, Required = false)]
		public double? UnderlyingContractMultiplier {get; set;}
		
		[TagDetails(Tag = 435, Type = TagType.Float, Offset = 12, Required = false)]
		public double? UnderlyingCouponRate {get; set;}
		
		[TagDetails(Tag = 308, Type = TagType.String, Offset = 13, Required = false)]
		public string? UnderlyingSecurityExchange {get; set;}
		
		[TagDetails(Tag = 306, Type = TagType.String, Offset = 14, Required = false)]
		public string? UnderlyingIssuer {get; set;}
		
		[TagDetails(Tag = 362, Type = TagType.Length, Offset = 15, Required = false, LinksToTag = 363)]
		public int? EncodedUnderlyingIssuerLen {get; set;}
		
		[TagDetails(Tag = 363, Type = TagType.RawData, Offset = 16, Required = false, LinksToTag = 362)]
		public byte[]? EncodedUnderlyingIssuer {get; set;}
		
		[TagDetails(Tag = 307, Type = TagType.String, Offset = 17, Required = false)]
		public string? UnderlyingSecurityDesc {get; set;}
		
		[TagDetails(Tag = 364, Type = TagType.Length, Offset = 18, Required = false, LinksToTag = 365)]
		public int? EncodedUnderlyingSecurityDescLen {get; set;}
		
		[TagDetails(Tag = 365, Type = TagType.RawData, Offset = 19, Required = false, LinksToTag = 364)]
		public byte[]? EncodedUnderlyingSecurityDesc {get; set;}
		
		[TagDetails(Tag = 367, Type = TagType.UtcTimestamp, Offset = 20, Required = false)]
		public DateTime? QuoteSetValidUntilTime {get; set;}
		
		[TagDetails(Tag = 304, Type = TagType.Int, Offset = 21, Required = true)]
		public int? TotQuoteEntries {get; set;}
		
		[Group(NoOfTag = 295, Offset = 22, Required = true)]
		public MassQuoteNoQuoteEntries[]? NoQuoteEntries {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				QuoteSetID is not null
				&& UnderlyingSymbol is not null
				&& TotQuoteEntries is not null
				&& NoQuoteEntries is not null && FixValidator.IsValid(NoQuoteEntries, in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (QuoteSetID is not null) writer.WriteString(302, QuoteSetID);
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
			if (QuoteSetValidUntilTime is not null) writer.WriteUtcTimeStamp(367, QuoteSetValidUntilTime.Value);
			if (TotQuoteEntries is not null) writer.WriteWholeNumber(304, TotQuoteEntries.Value);
			if (NoQuoteEntries is not null && NoQuoteEntries.Length != 0)
			{
				writer.WriteWholeNumber(295, NoQuoteEntries.Length);
				for (int i = 0; i < NoQuoteEntries.Length; i++)
				{
					((IFixEncoder)NoQuoteEntries[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			QuoteSetID = view.GetString(302);
			UnderlyingSymbol = view.GetString(311);
			UnderlyingSymbolSfx = view.GetString(312);
			UnderlyingSecurityID = view.GetString(309);
			UnderlyingIDSource = view.GetString(305);
			UnderlyingSecurityType = view.GetString(310);
			UnderlyingMaturityMonthYear = view.GetMonthYear(313);
			UnderlyingMaturityDay = view.GetString(314);
			UnderlyingPutOrCall = view.GetInt32(315);
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
			QuoteSetValidUntilTime = view.GetDateTime(367);
			TotQuoteEntries = view.GetInt32(304);
			if (view.GetView("NoQuoteEntries") is IMessageView viewNoQuoteEntries)
			{
				var count = viewNoQuoteEntries.GroupCount();
				NoQuoteEntries = new MassQuoteNoQuoteEntries[count];
				for (int i = 0; i < count; i++)
				{
					NoQuoteEntries[i] = new();
					((IFixParser)NoQuoteEntries[i]).Parse(viewNoQuoteEntries.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "QuoteSetID":
					value = QuoteSetID;
					break;
				case "UnderlyingSymbol":
					value = UnderlyingSymbol;
					break;
				case "UnderlyingSymbolSfx":
					value = UnderlyingSymbolSfx;
					break;
				case "UnderlyingSecurityID":
					value = UnderlyingSecurityID;
					break;
				case "UnderlyingIDSource":
					value = UnderlyingIDSource;
					break;
				case "UnderlyingSecurityType":
					value = UnderlyingSecurityType;
					break;
				case "UnderlyingMaturityMonthYear":
					value = UnderlyingMaturityMonthYear;
					break;
				case "UnderlyingMaturityDay":
					value = UnderlyingMaturityDay;
					break;
				case "UnderlyingPutOrCall":
					value = UnderlyingPutOrCall;
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
				case "QuoteSetValidUntilTime":
					value = QuoteSetValidUntilTime;
					break;
				case "TotQuoteEntries":
					value = TotQuoteEntries;
					break;
				case "NoQuoteEntries":
					value = NoQuoteEntries;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			QuoteSetID = null;
			UnderlyingSymbol = null;
			UnderlyingSymbolSfx = null;
			UnderlyingSecurityID = null;
			UnderlyingIDSource = null;
			UnderlyingSecurityType = null;
			UnderlyingMaturityMonthYear = null;
			UnderlyingMaturityDay = null;
			UnderlyingPutOrCall = null;
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
			QuoteSetValidUntilTime = null;
			TotQuoteEntries = null;
			NoQuoteEntries = null;
		}
	}
}
