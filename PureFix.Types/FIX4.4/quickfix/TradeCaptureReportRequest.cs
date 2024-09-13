using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AD", FixVersion.FIX44)]
	public sealed class TradeCaptureReportRequest : IFixMessage
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
		public DateTime? ClearingBusinessDate { get; set; }
		
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
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
