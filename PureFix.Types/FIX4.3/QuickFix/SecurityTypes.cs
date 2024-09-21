using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("w", FixVersion.FIX43)]
	public sealed partial class SecurityTypes : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 320, Type = TagType.String, Offset = 1, Required = true)]
		public string? SecurityReqID {get; set;}
		
		[TagDetails(Tag = 322, Type = TagType.String, Offset = 2, Required = true)]
		public string? SecurityResponseID {get; set;}
		
		[TagDetails(Tag = 323, Type = TagType.Int, Offset = 3, Required = true)]
		public int? SecurityResponseType {get; set;}
		
		[TagDetails(Tag = 557, Type = TagType.Int, Offset = 4, Required = false)]
		public int? TotalNumSecurityTypes {get; set;}
		
		[Group(NoOfTag = 558, Offset = 5, Required = false)]
		public NoSecurityTypes[]? NoSecurityTypes {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 6, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 7, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 8, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 9, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 10, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 11, Required = false)]
		public string? SubscriptionRequestType {get; set;}
		
		[Component(Offset = 12, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& SecurityReqID is not null
				&& SecurityResponseID is not null
				&& SecurityResponseType is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (SecurityReqID is not null) writer.WriteString(320, SecurityReqID);
			if (SecurityResponseID is not null) writer.WriteString(322, SecurityResponseID);
			if (SecurityResponseType is not null) writer.WriteWholeNumber(323, SecurityResponseType.Value);
			if (TotalNumSecurityTypes is not null) writer.WriteWholeNumber(557, TotalNumSecurityTypes.Value);
			if (NoSecurityTypes is not null && NoSecurityTypes.Length != 0)
			{
				writer.WriteWholeNumber(558, NoSecurityTypes.Length);
				for (int i = 0; i < NoSecurityTypes.Length; i++)
				{
					((IFixEncoder)NoSecurityTypes[i]).Encode(writer);
				}
			}
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (SubscriptionRequestType is not null) writer.WriteString(263, SubscriptionRequestType);
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
			SecurityReqID = view.GetString(320);
			SecurityResponseID = view.GetString(322);
			SecurityResponseType = view.GetInt32(323);
			TotalNumSecurityTypes = view.GetInt32(557);
			if (view.GetView("NoSecurityTypes") is IMessageView viewNoSecurityTypes)
			{
				var count = viewNoSecurityTypes.GroupCount();
				NoSecurityTypes = new NoSecurityTypes[count];
				for (int i = 0; i < count; i++)
				{
					NoSecurityTypes[i] = new();
					((IFixParser)NoSecurityTypes[i]).Parse(viewNoSecurityTypes.GetGroupInstance(i));
				}
			}
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
			SubscriptionRequestType = view.GetString(263);
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
					value = StandardHeader;
					break;
				case "SecurityReqID":
					value = SecurityReqID;
					break;
				case "SecurityResponseID":
					value = SecurityResponseID;
					break;
				case "SecurityResponseType":
					value = SecurityResponseType;
					break;
				case "TotalNumSecurityTypes":
					value = TotalNumSecurityTypes;
					break;
				case "NoSecurityTypes":
					value = NoSecurityTypes;
					break;
				case "Text":
					value = Text;
					break;
				case "EncodedTextLen":
					value = EncodedTextLen;
					break;
				case "EncodedText":
					value = EncodedText;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "TradingSessionSubID":
					value = TradingSessionSubID;
					break;
				case "SubscriptionRequestType":
					value = SubscriptionRequestType;
					break;
				case "StandardTrailer":
					value = StandardTrailer;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			SecurityReqID = null;
			SecurityResponseID = null;
			SecurityResponseType = null;
			TotalNumSecurityTypes = null;
			NoSecurityTypes = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			TradingSessionID = null;
			TradingSessionSubID = null;
			SubscriptionRequestType = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
