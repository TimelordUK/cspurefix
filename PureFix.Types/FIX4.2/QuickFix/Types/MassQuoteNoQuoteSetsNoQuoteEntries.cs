using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public sealed partial class MassQuoteNoQuoteSetsNoQuoteEntries : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 299, Type = TagType.String, Offset = 0, Required = true)]
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
		
		[TagDetails(Tag = 132, Type = TagType.Float, Offset = 20, Required = false)]
		public double? BidPx { get; set; }
		
		[TagDetails(Tag = 133, Type = TagType.Float, Offset = 21, Required = false)]
		public double? OfferPx { get; set; }
		
		[TagDetails(Tag = 134, Type = TagType.Float, Offset = 22, Required = false)]
		public double? BidSize { get; set; }
		
		[TagDetails(Tag = 135, Type = TagType.Float, Offset = 23, Required = false)]
		public double? OfferSize { get; set; }
		
		[TagDetails(Tag = 62, Type = TagType.UtcTimestamp, Offset = 24, Required = false)]
		public DateTime? ValidUntilTime { get; set; }
		
		[TagDetails(Tag = 188, Type = TagType.Float, Offset = 25, Required = false)]
		public double? BidSpotRate { get; set; }
		
		[TagDetails(Tag = 190, Type = TagType.Float, Offset = 26, Required = false)]
		public double? OfferSpotRate { get; set; }
		
		[TagDetails(Tag = 189, Type = TagType.Float, Offset = 27, Required = false)]
		public double? BidForwardPoints { get; set; }
		
		[TagDetails(Tag = 191, Type = TagType.Float, Offset = 28, Required = false)]
		public double? OfferForwardPoints { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 29, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 30, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 31, Required = false)]
		public DateOnly? FutSettDate { get; set; }
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 32, Required = false)]
		public string? OrdType { get; set; }
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 33, Required = false)]
		public DateOnly? FutSettDate2 { get; set; }
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 34, Required = false)]
		public double? OrderQty2 { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 35, Required = false)]
		public string? Currency { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				QuoteEntryID is not null;
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
			if (BidPx is not null) writer.WriteNumber(132, BidPx.Value);
			if (OfferPx is not null) writer.WriteNumber(133, OfferPx.Value);
			if (BidSize is not null) writer.WriteNumber(134, BidSize.Value);
			if (OfferSize is not null) writer.WriteNumber(135, OfferSize.Value);
			if (ValidUntilTime is not null) writer.WriteUtcTimeStamp(62, ValidUntilTime.Value);
			if (BidSpotRate is not null) writer.WriteNumber(188, BidSpotRate.Value);
			if (OfferSpotRate is not null) writer.WriteNumber(190, OfferSpotRate.Value);
			if (BidForwardPoints is not null) writer.WriteNumber(189, BidForwardPoints.Value);
			if (OfferForwardPoints is not null) writer.WriteNumber(191, OfferForwardPoints.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (FutSettDate is not null) writer.WriteLocalDateOnly(64, FutSettDate.Value);
			if (OrdType is not null) writer.WriteString(40, OrdType);
			if (FutSettDate2 is not null) writer.WriteLocalDateOnly(193, FutSettDate2.Value);
			if (OrderQty2 is not null) writer.WriteNumber(192, OrderQty2.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
		}
	}
}
