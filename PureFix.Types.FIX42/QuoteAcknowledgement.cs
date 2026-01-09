using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX42.Components;

namespace PureFix.Types.FIX42
{
	[MessageType("b", FixVersion.FIX42)]
	public sealed partial class QuoteAcknowledgement : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 131, Type = TagType.String, Offset = 1, Required = false)]
		public string? QuoteReqID {get; set;}
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 2, Required = false)]
		public string? QuoteID {get; set;}
		
		[TagDetails(Tag = 297, Type = TagType.Int, Offset = 3, Required = true)]
		public int? QuoteAckStatus {get; set;}
		
		[TagDetails(Tag = 300, Type = TagType.Int, Offset = 4, Required = false)]
		public int? QuoteRejectReason {get; set;}
		
		[TagDetails(Tag = 301, Type = TagType.Int, Offset = 5, Required = false)]
		public int? QuoteResponseLevel {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 6, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 7, Required = false)]
		public string? Text {get; set;}
		
		public sealed partial class NoQuoteSets : IFixGroup
		{
			[TagDetails(Tag = 302, Type = TagType.String, Offset = 0, Required = false)]
			public string? QuoteSetID {get; set;}
			
			[TagDetails(Tag = 311, Type = TagType.String, Offset = 1, Required = false)]
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
			
			[TagDetails(Tag = 362, Type = TagType.Length, Offset = 15, Required = false)]
			public int? EncodedUnderlyingIssuerLen {get; set;}
			
			[TagDetails(Tag = 363, Type = TagType.RawData, Offset = 16, Required = false)]
			public byte[]? EncodedUnderlyingIssuer {get; set;}
			
			[TagDetails(Tag = 307, Type = TagType.String, Offset = 17, Required = false)]
			public string? UnderlyingSecurityDesc {get; set;}
			
			[TagDetails(Tag = 364, Type = TagType.Length, Offset = 18, Required = false)]
			public int? EncodedUnderlyingSecurityDescLen {get; set;}
			
			[TagDetails(Tag = 365, Type = TagType.RawData, Offset = 19, Required = false)]
			public byte[]? EncodedUnderlyingSecurityDesc {get; set;}
			
			[TagDetails(Tag = 304, Type = TagType.Int, Offset = 20, Required = false)]
			public int? TotQuoteEntries {get; set;}
			
			public sealed partial class NoQuoteEntries : IFixGroup
			{
				[TagDetails(Tag = 299, Type = TagType.String, Offset = 0, Required = false)]
				public string? QuoteEntryID {get; set;}
				
				[TagDetails(Tag = 55, Type = TagType.String, Offset = 1, Required = false)]
				public string? Symbol {get; set;}
				
				[TagDetails(Tag = 65, Type = TagType.String, Offset = 2, Required = false)]
				public string? SymbolSfx {get; set;}
				
				[TagDetails(Tag = 48, Type = TagType.String, Offset = 3, Required = false)]
				public string? SecurityID {get; set;}
				
				[TagDetails(Tag = 22, Type = TagType.String, Offset = 4, Required = false)]
				public string? IDSource {get; set;}
				
				[TagDetails(Tag = 167, Type = TagType.String, Offset = 5, Required = false)]
				public string? SecurityType {get; set;}
				
				[TagDetails(Tag = 200, Type = TagType.MonthYear, Offset = 6, Required = false)]
				public MonthYear? MaturityMonthYear {get; set;}
				
				[TagDetails(Tag = 205, Type = TagType.String, Offset = 7, Required = false)]
				public string? MaturityDay {get; set;}
				
				[TagDetails(Tag = 201, Type = TagType.Int, Offset = 8, Required = false)]
				public int? PutOrCall {get; set;}
				
				[TagDetails(Tag = 202, Type = TagType.Float, Offset = 9, Required = false)]
				public double? StrikePrice {get; set;}
				
				[TagDetails(Tag = 206, Type = TagType.String, Offset = 10, Required = false)]
				public string? OptAttribute {get; set;}
				
				[TagDetails(Tag = 231, Type = TagType.Float, Offset = 11, Required = false)]
				public double? ContractMultiplier {get; set;}
				
				[TagDetails(Tag = 223, Type = TagType.Float, Offset = 12, Required = false)]
				public double? CouponRate {get; set;}
				
				[TagDetails(Tag = 207, Type = TagType.String, Offset = 13, Required = false)]
				public string? SecurityExchange {get; set;}
				
				[TagDetails(Tag = 106, Type = TagType.String, Offset = 14, Required = false)]
				public string? Issuer {get; set;}
				
				[TagDetails(Tag = 348, Type = TagType.Length, Offset = 15, Required = false)]
				public int? EncodedIssuerLen {get; set;}
				
				[TagDetails(Tag = 349, Type = TagType.RawData, Offset = 16, Required = false)]
				public byte[]? EncodedIssuer {get; set;}
				
				[TagDetails(Tag = 107, Type = TagType.String, Offset = 17, Required = false)]
				public string? SecurityDesc {get; set;}
				
				[TagDetails(Tag = 350, Type = TagType.Length, Offset = 18, Required = false)]
				public int? EncodedSecurityDescLen {get; set;}
				
				[TagDetails(Tag = 351, Type = TagType.RawData, Offset = 19, Required = false)]
				public byte[]? EncodedSecurityDesc {get; set;}
				
				[TagDetails(Tag = 368, Type = TagType.Int, Offset = 20, Required = false)]
				public int? QuoteEntryRejectReason {get; set;}
				
				
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
					if (EncodedIssuerLen is not null) writer.WriteWholeNumber(348, EncodedIssuerLen.Value);
					if (EncodedIssuer is not null) writer.WriteBuffer(349, EncodedIssuer);
					if (SecurityDesc is not null) writer.WriteString(107, SecurityDesc);
					if (EncodedSecurityDescLen is not null) writer.WriteWholeNumber(350, EncodedSecurityDescLen.Value);
					if (EncodedSecurityDesc is not null) writer.WriteBuffer(351, EncodedSecurityDesc);
					if (QuoteEntryRejectReason is not null) writer.WriteWholeNumber(368, QuoteEntryRejectReason.Value);
				}
				
				void IFixParser.Parse(IMessageView? view)
				{
					if (view is null) return;
					
					QuoteEntryID = view.GetString(299);
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
					QuoteEntryRejectReason = view.GetInt32(368);
				}
				
				bool IFixLookup.TryGetByTag(string name, out object? value)
				{
					value = null;
					switch (name)
					{
						case "QuoteEntryID":
						{
							value = QuoteEntryID;
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
						case "QuoteEntryRejectReason":
						{
							value = QuoteEntryRejectReason;
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
					QuoteEntryID = null;
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
					QuoteEntryRejectReason = null;
				}
			}
			[Group(NoOfTag = 295, Offset = 21, Required = false)]
			public NoQuoteEntries[]? QuoteEntries {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
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
				if (EncodedUnderlyingIssuerLen is not null) writer.WriteWholeNumber(362, EncodedUnderlyingIssuerLen.Value);
				if (EncodedUnderlyingIssuer is not null) writer.WriteBuffer(363, EncodedUnderlyingIssuer);
				if (UnderlyingSecurityDesc is not null) writer.WriteString(307, UnderlyingSecurityDesc);
				if (EncodedUnderlyingSecurityDescLen is not null) writer.WriteWholeNumber(364, EncodedUnderlyingSecurityDescLen.Value);
				if (EncodedUnderlyingSecurityDesc is not null) writer.WriteBuffer(365, EncodedUnderlyingSecurityDesc);
				if (TotQuoteEntries is not null) writer.WriteWholeNumber(304, TotQuoteEntries.Value);
				if (QuoteEntries is not null && QuoteEntries.Length != 0)
				{
					writer.WriteWholeNumber(295, QuoteEntries.Length);
					for (int i = 0; i < QuoteEntries.Length; i++)
					{
						((IFixEncoder)QuoteEntries[i]).Encode(writer);
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
				TotQuoteEntries = view.GetInt32(304);
				if (view.GetView("NoQuoteEntries") is IMessageView viewNoQuoteEntries)
				{
					var count = viewNoQuoteEntries.GroupCount();
					QuoteEntries = new NoQuoteEntries[count];
					for (int i = 0; i < count; i++)
					{
						QuoteEntries[i] = new();
						((IFixParser)QuoteEntries[i]).Parse(viewNoQuoteEntries.GetGroupInstance(i));
					}
				}
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "QuoteSetID":
					{
						value = QuoteSetID;
						break;
					}
					case "UnderlyingSymbol":
					{
						value = UnderlyingSymbol;
						break;
					}
					case "UnderlyingSymbolSfx":
					{
						value = UnderlyingSymbolSfx;
						break;
					}
					case "UnderlyingSecurityID":
					{
						value = UnderlyingSecurityID;
						break;
					}
					case "UnderlyingIDSource":
					{
						value = UnderlyingIDSource;
						break;
					}
					case "UnderlyingSecurityType":
					{
						value = UnderlyingSecurityType;
						break;
					}
					case "UnderlyingMaturityMonthYear":
					{
						value = UnderlyingMaturityMonthYear;
						break;
					}
					case "UnderlyingMaturityDay":
					{
						value = UnderlyingMaturityDay;
						break;
					}
					case "UnderlyingPutOrCall":
					{
						value = UnderlyingPutOrCall;
						break;
					}
					case "UnderlyingStrikePrice":
					{
						value = UnderlyingStrikePrice;
						break;
					}
					case "UnderlyingOptAttribute":
					{
						value = UnderlyingOptAttribute;
						break;
					}
					case "UnderlyingContractMultiplier":
					{
						value = UnderlyingContractMultiplier;
						break;
					}
					case "UnderlyingCouponRate":
					{
						value = UnderlyingCouponRate;
						break;
					}
					case "UnderlyingSecurityExchange":
					{
						value = UnderlyingSecurityExchange;
						break;
					}
					case "UnderlyingIssuer":
					{
						value = UnderlyingIssuer;
						break;
					}
					case "EncodedUnderlyingIssuerLen":
					{
						value = EncodedUnderlyingIssuerLen;
						break;
					}
					case "EncodedUnderlyingIssuer":
					{
						value = EncodedUnderlyingIssuer;
						break;
					}
					case "UnderlyingSecurityDesc":
					{
						value = UnderlyingSecurityDesc;
						break;
					}
					case "EncodedUnderlyingSecurityDescLen":
					{
						value = EncodedUnderlyingSecurityDescLen;
						break;
					}
					case "EncodedUnderlyingSecurityDesc":
					{
						value = EncodedUnderlyingSecurityDesc;
						break;
					}
					case "TotQuoteEntries":
					{
						value = TotQuoteEntries;
						break;
					}
					case "NoQuoteEntries":
					{
						value = QuoteEntries;
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
				TotQuoteEntries = null;
				QuoteEntries = null;
			}
		}
		[Group(NoOfTag = 296, Offset = 8, Required = false)]
		public NoQuoteSets[]? QuoteSets {get; set;}
		
		[Component(Offset = 9, Required = true)]
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
			if (QuoteReqID is not null) writer.WriteString(131, QuoteReqID);
			if (QuoteID is not null) writer.WriteString(117, QuoteID);
			if (QuoteAckStatus is not null) writer.WriteWholeNumber(297, QuoteAckStatus.Value);
			if (QuoteRejectReason is not null) writer.WriteWholeNumber(300, QuoteRejectReason.Value);
			if (QuoteResponseLevel is not null) writer.WriteWholeNumber(301, QuoteResponseLevel.Value);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (Text is not null) writer.WriteString(58, Text);
			if (QuoteSets is not null && QuoteSets.Length != 0)
			{
				writer.WriteWholeNumber(296, QuoteSets.Length);
				for (int i = 0; i < QuoteSets.Length; i++)
				{
					((IFixEncoder)QuoteSets[i]).Encode(writer);
				}
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is IMessageView viewStandardHeader)
			{
				StandardHeader = new();
				((IFixParser)StandardHeader).Parse(viewStandardHeader);
			}
			QuoteReqID = view.GetString(131);
			QuoteID = view.GetString(117);
			QuoteAckStatus = view.GetInt32(297);
			QuoteRejectReason = view.GetInt32(300);
			QuoteResponseLevel = view.GetInt32(301);
			TradingSessionID = view.GetString(336);
			Text = view.GetString(58);
			if (view.GetView("NoQuoteSets") is IMessageView viewNoQuoteSets)
			{
				var count = viewNoQuoteSets.GroupCount();
				QuoteSets = new NoQuoteSets[count];
				for (int i = 0; i < count; i++)
				{
					QuoteSets[i] = new();
					((IFixParser)QuoteSets[i]).Parse(viewNoQuoteSets.GetGroupInstance(i));
				}
			}
			if (view.GetView("StandardTrailer") is IMessageView viewStandardTrailer)
			{
				StandardTrailer = new();
				((IFixParser)StandardTrailer).Parse(viewStandardTrailer);
			}
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
				case "QuoteReqID":
				{
					value = QuoteReqID;
					break;
				}
				case "QuoteID":
				{
					value = QuoteID;
					break;
				}
				case "QuoteAckStatus":
				{
					value = QuoteAckStatus;
					break;
				}
				case "QuoteRejectReason":
				{
					value = QuoteRejectReason;
					break;
				}
				case "QuoteResponseLevel":
				{
					value = QuoteResponseLevel;
					break;
				}
				case "TradingSessionID":
				{
					value = TradingSessionID;
					break;
				}
				case "Text":
				{
					value = Text;
					break;
				}
				case "NoQuoteSets":
				{
					value = QuoteSets;
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
			QuoteReqID = null;
			QuoteID = null;
			QuoteAckStatus = null;
			QuoteRejectReason = null;
			QuoteResponseLevel = null;
			TradingSessionID = null;
			Text = null;
			QuoteSets = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
