using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("AD", FixVersion.FIX43)]
	public sealed partial class TradeCaptureReportRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 568, Type = TagType.String, Offset = 1, Required = true)]
		public string? TradeRequestID { get; set; }
		
		[TagDetails(Tag = 569, Type = TagType.Int, Offset = 2, Required = true)]
		public int? TradeRequestType { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 3, Required = false)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 4, Required = false)]
		public string? ExecID { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 5, Required = false)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 6, Required = false)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 7, Required = false)]
		public string? MatchStatus { get; set; }
		
		[Component(Offset = 8, Required = false)]
		public Parties? Parties { get; set; }
		
		[Component(Offset = 9, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[Group(NoOfTag = 580, Offset = 10, Required = false)]
		public TradeCaptureReportRequestNoDates[]? NoDates { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 11, Required = false)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 12, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 13, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 14, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 578, Type = TagType.String, Offset = 15, Required = false)]
		public string? TradeInputSource { get; set; }
		
		[TagDetails(Tag = 579, Type = TagType.String, Offset = 16, Required = false)]
		public string? TradeInputDevice { get; set; }
		
		[Component(Offset = 17, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& TradeRequestID is not null
				&& TradeRequestType is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (TradeRequestID is not null) writer.WriteString(568, TradeRequestID);
			if (TradeRequestType is not null) writer.WriteWholeNumber(569, TradeRequestType.Value);
			if (SubscriptionRequestType is not null) writer.WriteString(263, SubscriptionRequestType);
			if (ExecID is not null) writer.WriteString(17, ExecID);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (MatchStatus is not null) writer.WriteString(573, MatchStatus);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (NoDates is not null && NoDates.Length != 0)
			{
				writer.WriteWholeNumber(580, NoDates.Length);
				for (int i = 0; i < NoDates.Length; i++)
				{
					((IFixEncoder)NoDates[i]).Encode(writer);
				}
			}
			if (Side is not null) writer.WriteString(54, Side);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (TradeInputSource is not null) writer.WriteString(578, TradeInputSource);
			if (TradeInputDevice is not null) writer.WriteString(579, TradeInputDevice);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
