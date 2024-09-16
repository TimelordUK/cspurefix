using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AD", FixVersion.FIX44)]
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
		
		[TagDetails(Tag = 571, Type = TagType.String, Offset = 4, Required = false)]
		public string? TradeReportID { get; set; }
		
		[TagDetails(Tag = 818, Type = TagType.String, Offset = 5, Required = false)]
		public string? SecondaryTradeReportID { get; set; }
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 6, Required = false)]
		public string? ExecID { get; set; }
		
		[TagDetails(Tag = 150, Type = TagType.String, Offset = 7, Required = false)]
		public string? ExecType { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 8, Required = false)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 9, Required = false)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 10, Required = false)]
		public string? MatchStatus { get; set; }
		
		[TagDetails(Tag = 828, Type = TagType.Int, Offset = 11, Required = false)]
		public int? TrdType { get; set; }
		
		[TagDetails(Tag = 829, Type = TagType.Int, Offset = 12, Required = false)]
		public int? TrdSubType { get; set; }
		
		[TagDetails(Tag = 830, Type = TagType.String, Offset = 13, Required = false)]
		public string? TransferReason { get; set; }
		
		[TagDetails(Tag = 855, Type = TagType.Int, Offset = 14, Required = false)]
		public int? SecondaryTrdType { get; set; }
		
		[TagDetails(Tag = 820, Type = TagType.String, Offset = 15, Required = false)]
		public string? TradeLinkID { get; set; }
		
		[TagDetails(Tag = 880, Type = TagType.String, Offset = 16, Required = false)]
		public string? TrdMatchID { get; set; }
		
		[Component(Offset = 17, Required = false)]
		public Parties? Parties { get; set; }
		
		[Component(Offset = 18, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 19, Required = false)]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component(Offset = 20, Required = false)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 21, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 22, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 23, Required = false)]
		public TrdCapDtGrp? TrdCapDtGrp { get; set; }
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 24, Required = false)]
		public DateOnly? ClearingBusinessDate { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 25, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 26, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 943, Type = TagType.String, Offset = 27, Required = false)]
		public string? TimeBracket { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 28, Required = false)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 442, Type = TagType.String, Offset = 29, Required = false)]
		public string? MultiLegReportingType { get; set; }
		
		[TagDetails(Tag = 578, Type = TagType.String, Offset = 30, Required = false)]
		public string? TradeInputSource { get; set; }
		
		[TagDetails(Tag = 579, Type = TagType.String, Offset = 31, Required = false)]
		public string? TradeInputDevice { get; set; }
		
		[TagDetails(Tag = 725, Type = TagType.Int, Offset = 32, Required = false)]
		public int? ResponseTransportType { get; set; }
		
		[TagDetails(Tag = 726, Type = TagType.String, Offset = 33, Required = false)]
		public string? ResponseDestination { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 34, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 35, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 36, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 37, Required = true)]
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
			if (TradeReportID is not null) writer.WriteString(571, TradeReportID);
			if (SecondaryTradeReportID is not null) writer.WriteString(818, SecondaryTradeReportID);
			if (ExecID is not null) writer.WriteString(17, ExecID);
			if (ExecType is not null) writer.WriteString(150, ExecType);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (MatchStatus is not null) writer.WriteString(573, MatchStatus);
			if (TrdType is not null) writer.WriteWholeNumber(828, TrdType.Value);
			if (TrdSubType is not null) writer.WriteWholeNumber(829, TrdSubType.Value);
			if (TransferReason is not null) writer.WriteString(830, TransferReason);
			if (SecondaryTrdType is not null) writer.WriteWholeNumber(855, SecondaryTrdType.Value);
			if (TradeLinkID is not null) writer.WriteString(820, TradeLinkID);
			if (TrdMatchID is not null) writer.WriteString(880, TrdMatchID);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (InstrumentExtension is not null) ((IFixEncoder)InstrumentExtension).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (TrdCapDtGrp is not null) ((IFixEncoder)TrdCapDtGrp).Encode(writer);
			if (ClearingBusinessDate is not null) writer.WriteLocalDateOnly(715, ClearingBusinessDate.Value);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (TimeBracket is not null) writer.WriteString(943, TimeBracket);
			if (Side is not null) writer.WriteString(54, Side);
			if (MultiLegReportingType is not null) writer.WriteString(442, MultiLegReportingType);
			if (TradeInputSource is not null) writer.WriteString(578, TradeInputSource);
			if (TradeInputDevice is not null) writer.WriteString(579, TradeInputDevice);
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
