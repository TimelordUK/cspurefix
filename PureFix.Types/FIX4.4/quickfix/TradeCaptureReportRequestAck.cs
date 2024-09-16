using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AQ", FixVersion.FIX44)]
	public sealed partial class TradeCaptureReportRequestAck : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 568, Type = TagType.String, Offset = 1, Required = true)]
		public string? TradeRequestID { get; set; }
		
		[TagDetails(Tag = 569, Type = TagType.Int, Offset = 2, Required = true)]
		public int? TradeRequestType { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 3, Required = false)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(Tag = 748, Type = TagType.Int, Offset = 4, Required = false)]
		public int? TotNumTradeReports { get; set; }
		
		[TagDetails(Tag = 749, Type = TagType.Int, Offset = 5, Required = true)]
		public int? TradeRequestResult { get; set; }
		
		[TagDetails(Tag = 750, Type = TagType.Int, Offset = 6, Required = true)]
		public int? TradeRequestStatus { get; set; }
		
		[Component(Offset = 7, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 8, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 9, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 442, Type = TagType.String, Offset = 10, Required = false)]
		public string? MultiLegReportingType { get; set; }
		
		[TagDetails(Tag = 725, Type = TagType.Int, Offset = 11, Required = false)]
		public int? ResponseTransportType { get; set; }
		
		[TagDetails(Tag = 726, Type = TagType.String, Offset = 12, Required = false)]
		public string? ResponseDestination { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 13, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 14, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 15, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 16, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& TradeRequestID is not null
				&& TradeRequestType is not null
				&& TradeRequestResult is not null
				&& TradeRequestStatus is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (TradeRequestID is not null) writer.WriteString(568, TradeRequestID);
			if (TradeRequestType is not null) writer.WriteWholeNumber(569, TradeRequestType.Value);
			if (SubscriptionRequestType is not null) writer.WriteString(263, SubscriptionRequestType);
			if (TotNumTradeReports is not null) writer.WriteWholeNumber(748, TotNumTradeReports.Value);
			if (TradeRequestResult is not null) writer.WriteWholeNumber(749, TradeRequestResult.Value);
			if (TradeRequestStatus is not null) writer.WriteWholeNumber(750, TradeRequestStatus.Value);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (MultiLegReportingType is not null) writer.WriteString(442, MultiLegReportingType);
			if (ResponseTransportType is not null) writer.WriteWholeNumber(725, ResponseTransportType.Value);
			if (ResponseDestination is not null) writer.WriteString(726, ResponseDestination);
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
