using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AD", FixVersion.FIX44)]
	public sealed class TradeCaptureReportRequest : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 568, Type = TagType.String, Offset = 1)]
		public string? TradeRequestID { get; set; }
		
		[TagDetails(Tag = 569, Type = TagType.Int, Offset = 2)]
		public int? TradeRequestType { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 3)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(Tag = 571, Type = TagType.String, Offset = 4)]
		public string? TradeReportID { get; set; }
		
		[TagDetails(Tag = 818, Type = TagType.String, Offset = 5)]
		public string? SecondaryTradeReportID { get; set; }
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 6)]
		public string? ExecID { get; set; }
		
		[TagDetails(Tag = 150, Type = TagType.String, Offset = 7)]
		public string? ExecType { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 8)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 9)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 10)]
		public string? MatchStatus { get; set; }
		
		[TagDetails(Tag = 828, Type = TagType.Int, Offset = 11)]
		public int? TrdType { get; set; }
		
		[TagDetails(Tag = 829, Type = TagType.Int, Offset = 12)]
		public int? TrdSubType { get; set; }
		
		[TagDetails(Tag = 830, Type = TagType.String, Offset = 13)]
		public string? TransferReason { get; set; }
		
		[TagDetails(Tag = 855, Type = TagType.Int, Offset = 14)]
		public int? SecondaryTrdType { get; set; }
		
		[TagDetails(Tag = 820, Type = TagType.String, Offset = 15)]
		public string? TradeLinkID { get; set; }
		
		[TagDetails(Tag = 880, Type = TagType.String, Offset = 16)]
		public string? TrdMatchID { get; set; }
		
		[Component(Offset = 17)]
		public Parties? Parties { get; set; }
		
		[Component(Offset = 18)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 19)]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component(Offset = 20)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 21)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 22)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 23)]
		public TrdCapDtGrp? TrdCapDtGrp { get; set; }
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 24)]
		public DateTime? ClearingBusinessDate { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 25)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 26)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 943, Type = TagType.String, Offset = 27)]
		public string? TimeBracket { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 28)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 442, Type = TagType.String, Offset = 29)]
		public string? MultiLegReportingType { get; set; }
		
		[TagDetails(Tag = 578, Type = TagType.String, Offset = 30)]
		public string? TradeInputSource { get; set; }
		
		[TagDetails(Tag = 579, Type = TagType.String, Offset = 31)]
		public string? TradeInputDevice { get; set; }
		
		[TagDetails(Tag = 725, Type = TagType.Int, Offset = 32)]
		public int? ResponseTransportType { get; set; }
		
		[TagDetails(Tag = 726, Type = TagType.String, Offset = 33)]
		public string? ResponseDestination { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 34)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 35)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 36)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 37)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
