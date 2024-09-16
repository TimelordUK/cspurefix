using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("6", FixVersion.FIX42)]
	public sealed partial class IOI : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 23, Type = TagType.String, Offset = 1, Required = true)]
		public string? IOIid { get; set; }
		
		[TagDetails(Tag = 28, Type = TagType.String, Offset = 2, Required = true)]
		public string? IOITransType { get; set; }
		
		[TagDetails(Tag = 26, Type = TagType.String, Offset = 3, Required = false)]
		public string? IOIRefID { get; set; }
		
		[TagDetails(Tag = 55, Type = TagType.String, Offset = 4, Required = true)]
		public string? Symbol { get; set; }
		
		[TagDetails(Tag = 65, Type = TagType.String, Offset = 5, Required = false)]
		public string? SymbolSfx { get; set; }
		
		[TagDetails(Tag = 48, Type = TagType.String, Offset = 6, Required = false)]
		public string? SecurityID { get; set; }
		
		[TagDetails(Tag = 22, Type = TagType.String, Offset = 7, Required = false)]
		public string? IDSource { get; set; }
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 8, Required = false)]
		public string? SecurityType { get; set; }
		
		[TagDetails(Tag = 200, Type = TagType.MonthYear, Offset = 9, Required = false)]
		public MonthYear? MaturityMonthYear { get; set; }
		
		[TagDetails(Tag = 205, Type = TagType.String, Offset = 10, Required = false)]
		public string? MaturityDay { get; set; }
		
		[TagDetails(Tag = 201, Type = TagType.Int, Offset = 11, Required = false)]
		public int? PutOrCall { get; set; }
		
		[TagDetails(Tag = 202, Type = TagType.Float, Offset = 12, Required = false)]
		public double? StrikePrice { get; set; }
		
		[TagDetails(Tag = 206, Type = TagType.String, Offset = 13, Required = false)]
		public string? OptAttribute { get; set; }
		
		[TagDetails(Tag = 231, Type = TagType.Float, Offset = 14, Required = false)]
		public double? ContractMultiplier { get; set; }
		
		[TagDetails(Tag = 223, Type = TagType.Float, Offset = 15, Required = false)]
		public double? CouponRate { get; set; }
		
		[TagDetails(Tag = 207, Type = TagType.String, Offset = 16, Required = false)]
		public string? SecurityExchange { get; set; }
		
		[TagDetails(Tag = 106, Type = TagType.String, Offset = 17, Required = false)]
		public string? Issuer { get; set; }
		
		[TagDetails(Tag = 348, Type = TagType.Length, Offset = 18, Required = false, LinksToTag = 349)]
		public int? EncodedIssuerLen { get; set; }
		
		[TagDetails(Tag = 349, Type = TagType.RawData, Offset = 19, Required = false, LinksToTag = 348)]
		public byte[]? EncodedIssuer { get; set; }
		
		[TagDetails(Tag = 107, Type = TagType.String, Offset = 20, Required = false)]
		public string? SecurityDesc { get; set; }
		
		[TagDetails(Tag = 350, Type = TagType.Length, Offset = 21, Required = false, LinksToTag = 351)]
		public int? EncodedSecurityDescLen { get; set; }
		
		[TagDetails(Tag = 351, Type = TagType.RawData, Offset = 22, Required = false, LinksToTag = 350)]
		public byte[]? EncodedSecurityDesc { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 23, Required = true)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 27, Type = TagType.String, Offset = 24, Required = true)]
		public string? IOIShares { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 25, Required = false)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 26, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 62, Type = TagType.UtcTimestamp, Offset = 27, Required = false)]
		public DateTime? ValidUntilTime { get; set; }
		
		[TagDetails(Tag = 25, Type = TagType.String, Offset = 28, Required = false)]
		public string? IOIQltyInd { get; set; }
		
		[TagDetails(Tag = 130, Type = TagType.Boolean, Offset = 29, Required = false)]
		public bool? IOINaturalFlag { get; set; }
		
		[Group(NoOfTag = 199, Offset = 30, Required = false)]
		public IOINoIOIQualifiers[]? NoIOIQualifiers { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 31, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 32, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 33, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 34, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 149, Type = TagType.String, Offset = 35, Required = false)]
		public string? URLLink { get; set; }
		
		[Group(NoOfTag = 215, Offset = 36, Required = false)]
		public IOINoRoutingIDs[]? NoRoutingIDs { get; set; }
		
		[TagDetails(Tag = 218, Type = TagType.Float, Offset = 37, Required = false)]
		public double? SpreadToBenchmark { get; set; }
		
		[TagDetails(Tag = 219, Type = TagType.String, Offset = 38, Required = false)]
		public string? Benchmark { get; set; }
		
		[Component(Offset = 39, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& IOIid is not null
				&& IOITransType is not null
				&& Symbol is not null
				&& Side is not null
				&& IOIShares is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (IOIid is not null) writer.WriteString(23, IOIid);
			if (IOITransType is not null) writer.WriteString(28, IOITransType);
			if (IOIRefID is not null) writer.WriteString(26, IOIRefID);
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
			if (Side is not null) writer.WriteString(54, Side);
			if (IOIShares is not null) writer.WriteString(27, IOIShares);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (ValidUntilTime is not null) writer.WriteUtcTimeStamp(62, ValidUntilTime.Value);
			if (IOIQltyInd is not null) writer.WriteString(25, IOIQltyInd);
			if (IOINaturalFlag is not null) writer.WriteBoolean(130, IOINaturalFlag.Value);
			if (NoIOIQualifiers is not null && NoIOIQualifiers.Length != 0)
			{
				writer.WriteWholeNumber(199, NoIOIQualifiers.Length);
				for (int i = 0; i < NoIOIQualifiers.Length; i++)
				{
					((IFixEncoder)NoIOIQualifiers[i]).Encode(writer);
				}
			}
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (URLLink is not null) writer.WriteString(149, URLLink);
			if (NoRoutingIDs is not null && NoRoutingIDs.Length != 0)
			{
				writer.WriteWholeNumber(215, NoRoutingIDs.Length);
				for (int i = 0; i < NoRoutingIDs.Length; i++)
				{
					((IFixEncoder)NoRoutingIDs[i]).Encode(writer);
				}
			}
			if (SpreadToBenchmark is not null) writer.WriteNumber(218, SpreadToBenchmark.Value);
			if (Benchmark is not null) writer.WriteString(219, Benchmark);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
