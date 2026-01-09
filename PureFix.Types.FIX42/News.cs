using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX42.Components;

namespace PureFix.Types.FIX42
{
	[MessageType("B", FixVersion.FIX42)]
	public sealed partial class News : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 42, Type = TagType.UtcTimestamp, Offset = 1, Required = false)]
		public DateTime? OrigTime {get; set;}
		
		[TagDetails(Tag = 61, Type = TagType.String, Offset = 2, Required = false)]
		public string? Urgency {get; set;}
		
		[TagDetails(Tag = 148, Type = TagType.String, Offset = 3, Required = true)]
		public string? Headline {get; set;}
		
		[TagDetails(Tag = 358, Type = TagType.Length, Offset = 4, Required = false)]
		public int? EncodedHeadlineLen {get; set;}
		
		[TagDetails(Tag = 359, Type = TagType.RawData, Offset = 5, Required = false)]
		public byte[]? EncodedHeadline {get; set;}
		
		public sealed partial class NoRoutingIDs : IFixGroup
		{
			[TagDetails(Tag = 216, Type = TagType.Int, Offset = 0, Required = false)]
			public int? RoutingType {get; set;}
			
			[TagDetails(Tag = 217, Type = TagType.String, Offset = 1, Required = false)]
			public string? RoutingID {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (RoutingType is not null) writer.WriteWholeNumber(216, RoutingType.Value);
				if (RoutingID is not null) writer.WriteString(217, RoutingID);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				RoutingType = view.GetInt32(216);
				RoutingID = view.GetString(217);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "RoutingType":
					{
						value = RoutingType;
						break;
					}
					case "RoutingID":
					{
						value = RoutingID;
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
				RoutingType = null;
				RoutingID = null;
			}
		}
		[Group(NoOfTag = 215, Offset = 6, Required = false)]
		public NoRoutingIDs[]? RoutingIDs {get; set;}
		
		public sealed partial class NoRelatedSym : IFixGroup
		{
			[TagDetails(Tag = 46, Type = TagType.String, Offset = 0, Required = false)]
			public string? RelatdSym {get; set;}
			
			[TagDetails(Tag = 65, Type = TagType.String, Offset = 1, Required = false)]
			public string? SymbolSfx {get; set;}
			
			[TagDetails(Tag = 48, Type = TagType.String, Offset = 2, Required = false)]
			public string? SecurityID {get; set;}
			
			[TagDetails(Tag = 22, Type = TagType.String, Offset = 3, Required = false)]
			public string? IDSource {get; set;}
			
			[TagDetails(Tag = 167, Type = TagType.String, Offset = 4, Required = false)]
			public string? SecurityType {get; set;}
			
			[TagDetails(Tag = 200, Type = TagType.MonthYear, Offset = 5, Required = false)]
			public MonthYear? MaturityMonthYear {get; set;}
			
			[TagDetails(Tag = 205, Type = TagType.String, Offset = 6, Required = false)]
			public string? MaturityDay {get; set;}
			
			[TagDetails(Tag = 201, Type = TagType.Int, Offset = 7, Required = false)]
			public int? PutOrCall {get; set;}
			
			[TagDetails(Tag = 202, Type = TagType.Float, Offset = 8, Required = false)]
			public double? StrikePrice {get; set;}
			
			[TagDetails(Tag = 206, Type = TagType.String, Offset = 9, Required = false)]
			public string? OptAttribute {get; set;}
			
			[TagDetails(Tag = 231, Type = TagType.Float, Offset = 10, Required = false)]
			public double? ContractMultiplier {get; set;}
			
			[TagDetails(Tag = 223, Type = TagType.Float, Offset = 11, Required = false)]
			public double? CouponRate {get; set;}
			
			[TagDetails(Tag = 207, Type = TagType.String, Offset = 12, Required = false)]
			public string? SecurityExchange {get; set;}
			
			[TagDetails(Tag = 106, Type = TagType.String, Offset = 13, Required = false)]
			public string? Issuer {get; set;}
			
			[TagDetails(Tag = 348, Type = TagType.Length, Offset = 14, Required = false)]
			public int? EncodedIssuerLen {get; set;}
			
			[TagDetails(Tag = 349, Type = TagType.RawData, Offset = 15, Required = false)]
			public byte[]? EncodedIssuer {get; set;}
			
			[TagDetails(Tag = 107, Type = TagType.String, Offset = 16, Required = false)]
			public string? SecurityDesc {get; set;}
			
			[TagDetails(Tag = 350, Type = TagType.Length, Offset = 17, Required = false)]
			public int? EncodedSecurityDescLen {get; set;}
			
			[TagDetails(Tag = 351, Type = TagType.RawData, Offset = 18, Required = false)]
			public byte[]? EncodedSecurityDesc {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (RelatdSym is not null) writer.WriteString(46, RelatdSym);
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
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				RelatdSym = view.GetString(46);
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
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "RelatdSym":
					{
						value = RelatdSym;
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
					default:
					{
						return false;
					}
				}
				return true;
			}
			
			void IFixReset.Reset()
			{
				RelatdSym = null;
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
			}
		}
		[Group(NoOfTag = 146, Offset = 7, Required = false)]
		public NoRelatedSym[]? RelatedSym {get; set;}
		
		public sealed partial class LinesOfText : IFixGroup
		{
			[TagDetails(Tag = 58, Type = TagType.String, Offset = 0, Required = true)]
			public string? Text {get; set;}
			
			[TagDetails(Tag = 354, Type = TagType.Length, Offset = 1, Required = false)]
			public int? EncodedTextLen {get; set;}
			
			[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 2, Required = false)]
			public byte[]? EncodedText {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (Text is not null) writer.WriteString(58, Text);
				if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
				if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				Text = view.GetString(58);
				EncodedTextLen = view.GetInt32(354);
				EncodedText = view.GetByteArray(355);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "Text":
					{
						value = Text;
						break;
					}
					case "EncodedTextLen":
					{
						value = EncodedTextLen;
						break;
					}
					case "EncodedText":
					{
						value = EncodedText;
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
				Text = null;
				EncodedTextLen = null;
				EncodedText = null;
			}
		}
		[Group(NoOfTag = 33, Offset = 8, Required = true)]
		public LinesOfText[]? LinesOfTextItems {get; set;}
		
		[TagDetails(Tag = 149, Type = TagType.String, Offset = 9, Required = false)]
		public string? URLLink {get; set;}
		
		[TagDetails(Tag = 95, Type = TagType.Length, Offset = 10, Required = false)]
		public int? RawDataLength {get; set;}
		
		[TagDetails(Tag = 96, Type = TagType.RawData, Offset = 11, Required = false)]
		public byte[]? RawData {get; set;}
		
		[Component(Offset = 12, Required = true)]
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
			if (OrigTime is not null) writer.WriteUtcTimeStamp(42, OrigTime.Value);
			if (Urgency is not null) writer.WriteString(61, Urgency);
			if (Headline is not null) writer.WriteString(148, Headline);
			if (EncodedHeadlineLen is not null) writer.WriteWholeNumber(358, EncodedHeadlineLen.Value);
			if (EncodedHeadline is not null) writer.WriteBuffer(359, EncodedHeadline);
			if (RoutingIDs is not null && RoutingIDs.Length != 0)
			{
				writer.WriteWholeNumber(215, RoutingIDs.Length);
				for (int i = 0; i < RoutingIDs.Length; i++)
				{
					((IFixEncoder)RoutingIDs[i]).Encode(writer);
				}
			}
			if (RelatedSym is not null && RelatedSym.Length != 0)
			{
				writer.WriteWholeNumber(146, RelatedSym.Length);
				for (int i = 0; i < RelatedSym.Length; i++)
				{
					((IFixEncoder)RelatedSym[i]).Encode(writer);
				}
			}
			if (LinesOfTextItems is not null && LinesOfTextItems.Length != 0)
			{
				writer.WriteWholeNumber(33, LinesOfTextItems.Length);
				for (int i = 0; i < LinesOfTextItems.Length; i++)
				{
					((IFixEncoder)LinesOfTextItems[i]).Encode(writer);
				}
			}
			if (URLLink is not null) writer.WriteString(149, URLLink);
			if (RawDataLength is not null) writer.WriteWholeNumber(95, RawDataLength.Value);
			if (RawData is not null) writer.WriteBuffer(96, RawData);
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
			OrigTime = view.GetDateTime(42);
			Urgency = view.GetString(61);
			Headline = view.GetString(148);
			EncodedHeadlineLen = view.GetInt32(358);
			EncodedHeadline = view.GetByteArray(359);
			if (view.GetView("NoRoutingIDs") is IMessageView viewNoRoutingIDs)
			{
				var count = viewNoRoutingIDs.GroupCount();
				RoutingIDs = new NoRoutingIDs[count];
				for (int i = 0; i < count; i++)
				{
					RoutingIDs[i] = new();
					((IFixParser)RoutingIDs[i]).Parse(viewNoRoutingIDs.GetGroupInstance(i));
				}
			}
			if (view.GetView("NoRelatedSym") is IMessageView viewNoRelatedSym)
			{
				var count = viewNoRelatedSym.GroupCount();
				RelatedSym = new NoRelatedSym[count];
				for (int i = 0; i < count; i++)
				{
					RelatedSym[i] = new();
					((IFixParser)RelatedSym[i]).Parse(viewNoRelatedSym.GetGroupInstance(i));
				}
			}
			if (view.GetView("LinesOfText") is IMessageView viewLinesOfText)
			{
				var count = viewLinesOfText.GroupCount();
				LinesOfTextItems = new LinesOfText[count];
				for (int i = 0; i < count; i++)
				{
					LinesOfTextItems[i] = new();
					((IFixParser)LinesOfTextItems[i]).Parse(viewLinesOfText.GetGroupInstance(i));
				}
			}
			URLLink = view.GetString(149);
			RawDataLength = view.GetInt32(95);
			RawData = view.GetByteArray(96);
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
				case "OrigTime":
				{
					value = OrigTime;
					break;
				}
				case "Urgency":
				{
					value = Urgency;
					break;
				}
				case "Headline":
				{
					value = Headline;
					break;
				}
				case "EncodedHeadlineLen":
				{
					value = EncodedHeadlineLen;
					break;
				}
				case "EncodedHeadline":
				{
					value = EncodedHeadline;
					break;
				}
				case "NoRoutingIDs":
				{
					value = RoutingIDs;
					break;
				}
				case "NoRelatedSym":
				{
					value = RelatedSym;
					break;
				}
				case "LinesOfText":
				{
					value = LinesOfTextItems;
					break;
				}
				case "URLLink":
				{
					value = URLLink;
					break;
				}
				case "RawDataLength":
				{
					value = RawDataLength;
					break;
				}
				case "RawData":
				{
					value = RawData;
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
			OrigTime = null;
			Urgency = null;
			Headline = null;
			EncodedHeadlineLen = null;
			EncodedHeadline = null;
			RoutingIDs = null;
			RelatedSym = null;
			LinesOfTextItems = null;
			URLLink = null;
			RawDataLength = null;
			RawData = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
