using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("F", FixVersion.FIX42)]
	public sealed partial class OrderCancelRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 41, Type = TagType.String, Offset = 1, Required = true)]
		public string? OrigClOrdID { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 2, Required = false)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 3, Required = true)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 4, Required = false)]
		public string? ListID { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 5, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 109, Type = TagType.String, Offset = 6, Required = false)]
		public string? ClientID { get; set; }
		
		[TagDetails(Tag = 76, Type = TagType.String, Offset = 7, Required = false)]
		public string? ExecBroker { get; set; }
		
		[TagDetails(Tag = 55, Type = TagType.String, Offset = 8, Required = true)]
		public string? Symbol { get; set; }
		
		[TagDetails(Tag = 65, Type = TagType.String, Offset = 9, Required = false)]
		public string? SymbolSfx { get; set; }
		
		[TagDetails(Tag = 48, Type = TagType.String, Offset = 10, Required = false)]
		public string? SecurityID { get; set; }
		
		[TagDetails(Tag = 22, Type = TagType.String, Offset = 11, Required = false)]
		public string? IDSource { get; set; }
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 12, Required = false)]
		public string? SecurityType { get; set; }
		
		[TagDetails(Tag = 200, Type = TagType.MonthYear, Offset = 13, Required = false)]
		public MonthYear? MaturityMonthYear { get; set; }
		
		[TagDetails(Tag = 205, Type = TagType.String, Offset = 14, Required = false)]
		public string? MaturityDay { get; set; }
		
		[TagDetails(Tag = 201, Type = TagType.Int, Offset = 15, Required = false)]
		public int? PutOrCall { get; set; }
		
		[TagDetails(Tag = 202, Type = TagType.Float, Offset = 16, Required = false)]
		public double? StrikePrice { get; set; }
		
		[TagDetails(Tag = 206, Type = TagType.String, Offset = 17, Required = false)]
		public string? OptAttribute { get; set; }
		
		[TagDetails(Tag = 231, Type = TagType.Float, Offset = 18, Required = false)]
		public double? ContractMultiplier { get; set; }
		
		[TagDetails(Tag = 223, Type = TagType.Float, Offset = 19, Required = false)]
		public double? CouponRate { get; set; }
		
		[TagDetails(Tag = 207, Type = TagType.String, Offset = 20, Required = false)]
		public string? SecurityExchange { get; set; }
		
		[TagDetails(Tag = 106, Type = TagType.String, Offset = 21, Required = false)]
		public string? Issuer { get; set; }
		
		[TagDetails(Tag = 348, Type = TagType.Length, Offset = 22, Required = false, LinksToTag = 349)]
		public int? EncodedIssuerLen { get; set; }
		
		[TagDetails(Tag = 349, Type = TagType.RawData, Offset = 23, Required = false, LinksToTag = 348)]
		public byte[]? EncodedIssuer { get; set; }
		
		[TagDetails(Tag = 107, Type = TagType.String, Offset = 24, Required = false)]
		public string? SecurityDesc { get; set; }
		
		[TagDetails(Tag = 350, Type = TagType.Length, Offset = 25, Required = false, LinksToTag = 351)]
		public int? EncodedSecurityDescLen { get; set; }
		
		[TagDetails(Tag = 351, Type = TagType.RawData, Offset = 26, Required = false, LinksToTag = 350)]
		public byte[]? EncodedSecurityDesc { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 27, Required = true)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 28, Required = true)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 38, Type = TagType.Float, Offset = 29, Required = false)]
		public double? OrderQty { get; set; }
		
		[TagDetails(Tag = 152, Type = TagType.Float, Offset = 30, Required = false)]
		public double? CashOrderQty { get; set; }
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 31, Required = false)]
		public string? ComplianceID { get; set; }
		
		[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 32, Required = false)]
		public bool? SolicitedFlag { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 33, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 34, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 35, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 36, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& OrigClOrdID is not null
				&& ClOrdID is not null
				&& Symbol is not null
				&& Side is not null
				&& TransactTime is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (OrigClOrdID is not null) writer.WriteString(41, OrigClOrdID);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (ListID is not null) writer.WriteString(66, ListID);
			if (Account is not null) writer.WriteString(1, Account);
			if (ClientID is not null) writer.WriteString(109, ClientID);
			if (ExecBroker is not null) writer.WriteString(76, ExecBroker);
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
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (OrderQty is not null) writer.WriteNumber(38, OrderQty.Value);
			if (CashOrderQty is not null) writer.WriteNumber(152, CashOrderQty.Value);
			if (ComplianceID is not null) writer.WriteString(376, ComplianceID);
			if (SolicitedFlag is not null) writer.WriteBoolean(377, SolicitedFlag.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
