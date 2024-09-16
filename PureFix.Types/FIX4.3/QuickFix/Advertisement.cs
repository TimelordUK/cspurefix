using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("7", FixVersion.FIX43)]
	public sealed partial class Advertisement : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 2, Type = TagType.String, Offset = 1, Required = true)]
		public string? AdvId { get; set; }
		
		[TagDetails(Tag = 5, Type = TagType.String, Offset = 2, Required = true)]
		public string? AdvTransType { get; set; }
		
		[TagDetails(Tag = 3, Type = TagType.String, Offset = 3, Required = false)]
		public string? AdvRefID { get; set; }
		
		[Component(Offset = 4, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[TagDetails(Tag = 4, Type = TagType.String, Offset = 5, Required = true)]
		public string? AdvSide { get; set; }
		
		[TagDetails(Tag = 53, Type = TagType.Float, Offset = 6, Required = true)]
		public double? Quantity { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 7, Required = false)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 8, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 9, Required = false)]
		public DateOnly? TradeDate { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 10, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 11, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 12, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 13, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 149, Type = TagType.String, Offset = 14, Required = false)]
		public string? URLLink { get; set; }
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 15, Required = false)]
		public string? LastMkt { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 16, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 17, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[Component(Offset = 18, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& AdvId is not null
				&& AdvTransType is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& AdvSide is not null
				&& Quantity is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (AdvId is not null) writer.WriteString(2, AdvId);
			if (AdvTransType is not null) writer.WriteString(5, AdvTransType);
			if (AdvRefID is not null) writer.WriteString(3, AdvRefID);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (AdvSide is not null) writer.WriteString(4, AdvSide);
			if (Quantity is not null) writer.WriteNumber(53, Quantity.Value);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (URLLink is not null) writer.WriteString(149, URLLink);
			if (LastMkt is not null) writer.WriteString(30, LastMkt);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
