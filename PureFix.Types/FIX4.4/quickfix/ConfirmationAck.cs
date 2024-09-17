using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AU", FixVersion.FIX44)]
	public sealed partial class ConfirmationAck : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 664, Type = TagType.String, Offset = 1, Required = true)]
		public string? ConfirmID {get; set;}
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 2, Required = true)]
		public DateOnly? TradeDate {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 3, Required = true)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 940, Type = TagType.Int, Offset = 4, Required = true)]
		public int? AffirmStatus {get; set;}
		
		[TagDetails(Tag = 774, Type = TagType.Int, Offset = 5, Required = false)]
		public int? ConfirmRejReason {get; set;}
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 6, Required = false)]
		public string? MatchStatus {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 7, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 8, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 9, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 10, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& ConfirmID is not null
				&& TradeDate is not null
				&& TransactTime is not null
				&& AffirmStatus is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (ConfirmID is not null) writer.WriteString(664, ConfirmID);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (AffirmStatus is not null) writer.WriteWholeNumber(940, AffirmStatus.Value);
			if (ConfirmRejReason is not null) writer.WriteWholeNumber(774, ConfirmRejReason.Value);
			if (MatchStatus is not null) writer.WriteString(573, MatchStatus);
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
			ConfirmID = view.GetString(664);
			TradeDate = view.GetDateOnly(75);
			TransactTime = view.GetDateTime(60);
			AffirmStatus = view.GetInt32(940);
			ConfirmRejReason = view.GetInt32(774);
			MatchStatus = view.GetString(573);
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
				case "ConfirmID":
					value = ConfirmID;
					break;
				case "TradeDate":
					value = TradeDate;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "AffirmStatus":
					value = AffirmStatus;
					break;
				case "ConfirmRejReason":
					value = ConfirmRejReason;
					break;
				case "MatchStatus":
					value = MatchStatus;
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
	}
}
