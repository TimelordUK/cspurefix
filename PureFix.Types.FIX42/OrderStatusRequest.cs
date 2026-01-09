using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX42.Components;

namespace PureFix.Types.FIX42
{
	[MessageType("H", FixVersion.FIX42)]
	public sealed partial class OrderStatusRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = false)]
		public string? OrderID {get; set;}
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 2, Required = true)]
		public string? ClOrdID {get; set;}
		
		[TagDetails(Tag = 109, Type = TagType.String, Offset = 3, Required = false)]
		public string? ClientID {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 4, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 76, Type = TagType.String, Offset = 5, Required = false)]
		public string? ExecBroker {get; set;}
		
		[TagDetails(Tag = 55, Type = TagType.String, Offset = 6, Required = true)]
		public string? Symbol {get; set;}
		
		[TagDetails(Tag = 65, Type = TagType.String, Offset = 7, Required = false)]
		public string? SymbolSfx {get; set;}
		
		[TagDetails(Tag = 48, Type = TagType.String, Offset = 8, Required = false)]
		public string? SecurityID {get; set;}
		
		[TagDetails(Tag = 22, Type = TagType.String, Offset = 9, Required = false)]
		public string? IDSource {get; set;}
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 10, Required = false)]
		public string? SecurityType {get; set;}
		
		[TagDetails(Tag = 200, Type = TagType.MonthYear, Offset = 11, Required = false)]
		public MonthYear? MaturityMonthYear {get; set;}
		
		[TagDetails(Tag = 205, Type = TagType.String, Offset = 12, Required = false)]
		public string? MaturityDay {get; set;}
		
		[TagDetails(Tag = 201, Type = TagType.Int, Offset = 13, Required = false)]
		public int? PutOrCall {get; set;}
		
		[TagDetails(Tag = 202, Type = TagType.Float, Offset = 14, Required = false)]
		public double? StrikePrice {get; set;}
		
		[TagDetails(Tag = 206, Type = TagType.String, Offset = 15, Required = false)]
		public string? OptAttribute {get; set;}
		
		[TagDetails(Tag = 231, Type = TagType.Float, Offset = 16, Required = false)]
		public double? ContractMultiplier {get; set;}
		
		[TagDetails(Tag = 223, Type = TagType.Float, Offset = 17, Required = false)]
		public double? CouponRate {get; set;}
		
		[TagDetails(Tag = 207, Type = TagType.String, Offset = 18, Required = false)]
		public string? SecurityExchange {get; set;}
		
		[TagDetails(Tag = 106, Type = TagType.String, Offset = 19, Required = false)]
		public string? Issuer {get; set;}
		
		[TagDetails(Tag = 348, Type = TagType.Length, Offset = 20, Required = false)]
		public int? EncodedIssuerLen {get; set;}
		
		[TagDetails(Tag = 349, Type = TagType.RawData, Offset = 21, Required = false)]
		public byte[]? EncodedIssuer {get; set;}
		
		[TagDetails(Tag = 107, Type = TagType.String, Offset = 22, Required = false)]
		public string? SecurityDesc {get; set;}
		
		[TagDetails(Tag = 350, Type = TagType.Length, Offset = 23, Required = false)]
		public int? EncodedSecurityDescLen {get; set;}
		
		[TagDetails(Tag = 351, Type = TagType.RawData, Offset = 24, Required = false)]
		public byte[]? EncodedSecurityDesc {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 25, Required = true)]
		public string? Side {get; set;}
		
		[Component(Offset = 26, Required = true)]
		public StandardTrailer? StandardTrailer {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return (!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config))) && (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (ClientID is not null) writer.WriteString(109, ClientID);
			if (Account is not null) writer.WriteString(1, Account);
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
			if (EncodedIssuerLen is not null) writer.WriteWholeNumber(348, EncodedIssuerLen.Value);
			if (EncodedIssuer is not null) writer.WriteBuffer(349, EncodedIssuer);
			if (SecurityDesc is not null) writer.WriteString(107, SecurityDesc);
			if (EncodedSecurityDescLen is not null) writer.WriteWholeNumber(350, EncodedSecurityDescLen.Value);
			if (EncodedSecurityDesc is not null) writer.WriteBuffer(351, EncodedSecurityDesc);
			if (Side is not null) writer.WriteString(54, Side);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			OrderID = view.GetString(37);
			ClOrdID = view.GetString(11);
			ClientID = view.GetString(109);
			Account = view.GetString(1);
			ExecBroker = view.GetString(76);
			Symbol = view.GetString(55);
			SymbolSfx = view.GetString(65);
			SecurityID = view.GetString(48);
			IDSource = view.GetString(22);
			SecurityType = view.GetString(167);
			MaturityMonthYear = view.GetMonthYear(200);
			MaturityDay = view.GetString(205);
			PutOrCall = view.GetInt32(201);
			StrikePrice = view.GetDouble(202);
			OptAttribute = view.GetString(206);
			ContractMultiplier = view.GetDouble(231);
			CouponRate = view.GetDouble(223);
			SecurityExchange = view.GetString(207);
			Issuer = view.GetString(106);
			EncodedIssuerLen = view.GetInt32(348);
			EncodedIssuer = view.GetByteArray(349);
			SecurityDesc = view.GetString(107);
			EncodedSecurityDescLen = view.GetInt32(350);
			EncodedSecurityDesc = view.GetByteArray(351);
			Side = view.GetString(54);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "StandardHeader":
				{
					value = StandardHeader;
					break;
				}
				case "OrderID":
				{
					value = OrderID;
					break;
				}
				case "ClOrdID":
				{
					value = ClOrdID;
					break;
				}
				case "ClientID":
				{
					value = ClientID;
					break;
				}
				case "Account":
				{
					value = Account;
					break;
				}
				case "ExecBroker":
				{
					value = ExecBroker;
					break;
				}
				case "Symbol":
				{
					value = Symbol;
					break;
				}
				case "SymbolSfx":
				{
					value = SymbolSfx;
					break;
				}
				case "SecurityID":
				{
					value = SecurityID;
					break;
				}
				case "IDSource":
				{
					value = IDSource;
					break;
				}
				case "SecurityType":
				{
					value = SecurityType;
					break;
				}
				case "MaturityMonthYear":
				{
					value = MaturityMonthYear;
					break;
				}
				case "MaturityDay":
				{
					value = MaturityDay;
					break;
				}
				case "PutOrCall":
				{
					value = PutOrCall;
					break;
				}
				case "StrikePrice":
				{
					value = StrikePrice;
					break;
				}
				case "OptAttribute":
				{
					value = OptAttribute;
					break;
				}
				case "ContractMultiplier":
				{
					value = ContractMultiplier;
					break;
				}
				case "CouponRate":
				{
					value = CouponRate;
					break;
				}
				case "SecurityExchange":
				{
					value = SecurityExchange;
					break;
				}
				case "Issuer":
				{
					value = Issuer;
					break;
				}
				case "EncodedIssuerLen":
				{
					value = EncodedIssuerLen;
					break;
				}
				case "EncodedIssuer":
				{
					value = EncodedIssuer;
					break;
				}
				case "SecurityDesc":
				{
					value = SecurityDesc;
					break;
				}
				case "EncodedSecurityDescLen":
				{
					value = EncodedSecurityDescLen;
					break;
				}
				case "EncodedSecurityDesc":
				{
					value = EncodedSecurityDesc;
					break;
				}
				case "Side":
				{
					value = Side;
					break;
				}
				case "StandardTrailer":
				{
					value = StandardTrailer;
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
			((IFixReset?)StandardHeader)?.Reset();
			OrderID = null;
			ClOrdID = null;
			ClientID = null;
			Account = null;
			ExecBroker = null;
			Symbol = null;
			SymbolSfx = null;
			SecurityID = null;
			IDSource = null;
			SecurityType = null;
			MaturityMonthYear = null;
			MaturityDay = null;
			PutOrCall = null;
			StrikePrice = null;
			OptAttribute = null;
			ContractMultiplier = null;
			CouponRate = null;
			SecurityExchange = null;
			Issuer = null;
			EncodedIssuerLen = null;
			EncodedIssuer = null;
			SecurityDesc = null;
			EncodedSecurityDescLen = null;
			EncodedSecurityDesc = null;
			Side = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
