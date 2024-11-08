using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("h", FixVersion.FIX43)]
	public sealed partial class TradingSessionStatus : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 335, Type = TagType.String, Offset = 1, Required = false)]
		public string? TradSesReqID {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 2, Required = true)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 3, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		[TagDetails(Tag = 338, Type = TagType.Int, Offset = 4, Required = false)]
		public int? TradSesMethod {get; set;}
		
		[TagDetails(Tag = 339, Type = TagType.Int, Offset = 5, Required = false)]
		public int? TradSesMode {get; set;}
		
		[TagDetails(Tag = 325, Type = TagType.Boolean, Offset = 6, Required = false)]
		public bool? UnsolicitedIndicator {get; set;}
		
		[TagDetails(Tag = 340, Type = TagType.Int, Offset = 7, Required = true)]
		public int? TradSesStatus {get; set;}
		
		[TagDetails(Tag = 567, Type = TagType.Int, Offset = 8, Required = false)]
		public int? TradSesStatusRejReason {get; set;}
		
		[TagDetails(Tag = 341, Type = TagType.UtcTimestamp, Offset = 9, Required = false)]
		public DateTime? TradSesStartTime {get; set;}
		
		[TagDetails(Tag = 342, Type = TagType.UtcTimestamp, Offset = 10, Required = false)]
		public DateTime? TradSesOpenTime {get; set;}
		
		[TagDetails(Tag = 343, Type = TagType.UtcTimestamp, Offset = 11, Required = false)]
		public DateTime? TradSesPreCloseTime {get; set;}
		
		[TagDetails(Tag = 344, Type = TagType.UtcTimestamp, Offset = 12, Required = false)]
		public DateTime? TradSesCloseTime {get; set;}
		
		[TagDetails(Tag = 345, Type = TagType.UtcTimestamp, Offset = 13, Required = false)]
		public DateTime? TradSesEndTime {get; set;}
		
		[TagDetails(Tag = 387, Type = TagType.Float, Offset = 14, Required = false)]
		public double? TotalVolumeTraded {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 15, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 16, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 17, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 18, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& TradingSessionID is not null
				&& TradSesStatus is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (TradSesReqID is not null) writer.WriteString(335, TradSesReqID);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (TradSesMethod is not null) writer.WriteWholeNumber(338, TradSesMethod.Value);
			if (TradSesMode is not null) writer.WriteWholeNumber(339, TradSesMode.Value);
			if (UnsolicitedIndicator is not null) writer.WriteBoolean(325, UnsolicitedIndicator.Value);
			if (TradSesStatus is not null) writer.WriteWholeNumber(340, TradSesStatus.Value);
			if (TradSesStatusRejReason is not null) writer.WriteWholeNumber(567, TradSesStatusRejReason.Value);
			if (TradSesStartTime is not null) writer.WriteUtcTimeStamp(341, TradSesStartTime.Value);
			if (TradSesOpenTime is not null) writer.WriteUtcTimeStamp(342, TradSesOpenTime.Value);
			if (TradSesPreCloseTime is not null) writer.WriteUtcTimeStamp(343, TradSesPreCloseTime.Value);
			if (TradSesCloseTime is not null) writer.WriteUtcTimeStamp(344, TradSesCloseTime.Value);
			if (TradSesEndTime is not null) writer.WriteUtcTimeStamp(345, TradSesEndTime.Value);
			if (TotalVolumeTraded is not null) writer.WriteNumber(387, TotalVolumeTraded.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
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
			TradSesReqID = view.GetString(335);
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
			TradSesMethod = view.GetInt32(338);
			TradSesMode = view.GetInt32(339);
			UnsolicitedIndicator = view.GetBool(325);
			TradSesStatus = view.GetInt32(340);
			TradSesStatusRejReason = view.GetInt32(567);
			TradSesStartTime = view.GetDateTime(341);
			TradSesOpenTime = view.GetDateTime(342);
			TradSesPreCloseTime = view.GetDateTime(343);
			TradSesCloseTime = view.GetDateTime(344);
			TradSesEndTime = view.GetDateTime(345);
			TotalVolumeTraded = view.GetDouble(387);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
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
				case "TradSesReqID":
					value = TradSesReqID;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "TradingSessionSubID":
					value = TradingSessionSubID;
					break;
				case "TradSesMethod":
					value = TradSesMethod;
					break;
				case "TradSesMode":
					value = TradSesMode;
					break;
				case "UnsolicitedIndicator":
					value = UnsolicitedIndicator;
					break;
				case "TradSesStatus":
					value = TradSesStatus;
					break;
				case "TradSesStatusRejReason":
					value = TradSesStatusRejReason;
					break;
				case "TradSesStartTime":
					value = TradSesStartTime;
					break;
				case "TradSesOpenTime":
					value = TradSesOpenTime;
					break;
				case "TradSesPreCloseTime":
					value = TradSesPreCloseTime;
					break;
				case "TradSesCloseTime":
					value = TradSesCloseTime;
					break;
				case "TradSesEndTime":
					value = TradSesEndTime;
					break;
				case "TotalVolumeTraded":
					value = TotalVolumeTraded;
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
			TradSesReqID = null;
			TradingSessionID = null;
			TradingSessionSubID = null;
			TradSesMethod = null;
			TradSesMode = null;
			UnsolicitedIndicator = null;
			TradSesStatus = null;
			TradSesStatusRejReason = null;
			TradSesStartTime = null;
			TradSesOpenTime = null;
			TradSesPreCloseTime = null;
			TradSesCloseTime = null;
			TradSesEndTime = null;
			TotalVolumeTraded = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
