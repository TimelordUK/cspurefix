using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AD")]
	public sealed class TradeCaptureReportRequest : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(568, TagType.String)]
		public string? TradeRequestID { get; set; }
		
		[TagDetails(569, TagType.Int)]
		public int? TradeRequestType { get; set; }
		
		[TagDetails(263, TagType.String)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(571, TagType.String)]
		public string? TradeReportID { get; set; }
		
		[TagDetails(818, TagType.String)]
		public string? SecondaryTradeReportID { get; set; }
		
		[TagDetails(17, TagType.String)]
		public string? ExecID { get; set; }
		
		[TagDetails(150, TagType.String)]
		public string? ExecType { get; set; }
		
		[TagDetails(37, TagType.String)]
		public string? OrderID { get; set; }
		
		[TagDetails(11, TagType.String)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(573, TagType.String)]
		public string? MatchStatus { get; set; }
		
		[TagDetails(828, TagType.Int)]
		public int? TrdType { get; set; }
		
		[TagDetails(829, TagType.Int)]
		public int? TrdSubType { get; set; }
		
		[TagDetails(830, TagType.String)]
		public string? TransferReason { get; set; }
		
		[TagDetails(855, TagType.Int)]
		public int? SecondaryTrdType { get; set; }
		
		[TagDetails(820, TagType.String)]
		public string? TradeLinkID { get; set; }
		
		[TagDetails(880, TagType.String)]
		public string? TrdMatchID { get; set; }
		
		[Component]
		public Parties? Parties { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[Component]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component]
		public TrdCapDtGrp? TrdCapDtGrp { get; set; }
		
		[TagDetails(715, TagType.LocalDate)]
		public DateTime? ClearingBusinessDate { get; set; }
		
		[TagDetails(336, TagType.String)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(625, TagType.String)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(943, TagType.String)]
		public string? TimeBracket { get; set; }
		
		[TagDetails(54, TagType.String)]
		public string? Side { get; set; }
		
		[TagDetails(442, TagType.String)]
		public string? MultiLegReportingType { get; set; }
		
		[TagDetails(578, TagType.String)]
		public string? TradeInputSource { get; set; }
		
		[TagDetails(579, TagType.String)]
		public string? TradeInputDevice { get; set; }
		
		[TagDetails(725, TagType.Int)]
		public int? ResponseTransportType { get; set; }
		
		[TagDetails(726, TagType.String)]
		public string? ResponseDestination { get; set; }
		
		[TagDetails(58, TagType.String)]
		public string? Text { get; set; }
		
		[TagDetails(354, TagType.Length)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(355, TagType.RawData)]
		public byte[]? EncodedText { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
