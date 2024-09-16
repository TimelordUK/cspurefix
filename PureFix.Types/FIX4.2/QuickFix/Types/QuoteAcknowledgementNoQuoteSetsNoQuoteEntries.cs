using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public sealed partial class QuoteAcknowledgementNoQuoteSetsNoQuoteEntries : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 299, Type = TagType.String, Offset = 0, Required = false)]
		public string? QuoteEntryID { get; set; }
		
		[TagDetails(Tag = 55, Type = TagType.String, Offset = 1, Required = false)]
		public string? Symbol { get; set; }
		
		[TagDetails(Tag = 65, Type = TagType.String, Offset = 2, Required = false)]
		public string? SymbolSfx { get; set; }
		
		[TagDetails(Tag = 48, Type = TagType.String, Offset = 3, Required = false)]
		public string? SecurityID { get; set; }
		
		[TagDetails(Tag = 22, Type = TagType.String, Offset = 4, Required = false)]
		public string? IDSource { get; set; }
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 5, Required = false)]
		public string? SecurityType { get; set; }
		
		[TagDetails(Tag = 200, Type = TagType.MonthYear, Offset = 6, Required = false)]
		public MonthYear? MaturityMonthYear { get; set; }
		
		[TagDetails(Tag = 205, Type = TagType.String, Offset = 7, Required = false)]
		public string? MaturityDay { get; set; }
		
		[TagDetails(Tag = 201, Type = TagType.Int, Offset = 8, Required = false)]
		public int? PutOrCall { get; set; }
		
		[TagDetails(Tag = 202, Type = TagType.Float, Offset = 9, Required = false)]
		public double? StrikePrice { get; set; }
		
		[TagDetails(Tag = 206, Type = TagType.String, Offset = 10, Required = false)]
		public string? OptAttribute { get; set; }
		
		[TagDetails(Tag = 231, Type = TagType.Float, Offset = 11, Required = false)]
		public double? ContractMultiplier { get; set; }
		
		[TagDetails(Tag = 223, Type = TagType.Float, Offset = 12, Required = false)]
		public double? CouponRate { get; set; }
		
		[TagDetails(Tag = 207, Type = TagType.String, Offset = 13, Required = false)]
		public string? SecurityExchange { get; set; }
		
		[TagDetails(Tag = 106, Type = TagType.String, Offset = 14, Required = false)]
		public string? Issuer { get; set; }
		
		[TagDetails(Tag = 348, Type = TagType.Length, Offset = 15, Required = false, LinksToTag = 349)]
		public int? EncodedIssuerLen { get; set; }
		
		[TagDetails(Tag = 349, Type = TagType.RawData, Offset = 16, Required = false, LinksToTag = 348)]
		public byte[]? EncodedIssuer { get; set; }
		
		[TagDetails(Tag = 107, Type = TagType.String, Offset = 17, Required = false)]
		public string? SecurityDesc { get; set; }
		
		[TagDetails(Tag = 350, Type = TagType.Length, Offset = 18, Required = false, LinksToTag = 351)]
		public int? EncodedSecurityDescLen { get; set; }
		
		[TagDetails(Tag = 351, Type = TagType.RawData, Offset = 19, Required = false, LinksToTag = 350)]
		public byte[]? EncodedSecurityDesc { get; set; }
		
		[TagDetails(Tag = 368, Type = TagType.Int, Offset = 20, Required = false)]
		public int? QuoteEntryRejectReason { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (QuoteEntryID is not null) writer.WriteString(299, QuoteEntryID);
			if (Symbol is not null) writer.WriteString(55, Symbol);
			if (SymbolSfx is not null) writer.WriteString(65, SymbolSfx);
			if (SecurityID is not null) writer.WriteString(48, SecurityID);
			if (IDSource is not null) writer.WriteString(22, IDSource);
			if (SecurityType is not null) writer.WriteString(167, SecurityType);
			if (MaturityMonthYear is not null) writer.WriteMonthYear(200, MaturityMonthYear.Value);
			if (MaturityDay is not null) writer.WriteString(205, MaturityDay);
			if (PutOrCall is not null) writer.WriteWholeNumber(201, PutOrCall.Value);
			if (StrikePrice is not null) writer.WriteNumber(202, StrikePrice.Value);
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
			if (QuoteEntryRejectReason is not null) writer.WriteWholeNumber(368, QuoteEntryRejectReason.Value);
		}
	}
}
