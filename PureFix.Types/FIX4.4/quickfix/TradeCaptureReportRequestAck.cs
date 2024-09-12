using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class TradeCaptureReportRequestAck : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(568, TagType.String)]
		public string? TradeRequestID { get; set; }
		
		[TagDetails(569, TagType.Int)]
		public int? TradeRequestType { get; set; }
		
		[TagDetails(263, TagType.String)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(748, TagType.Int)]
		public int? TotNumTradeReports { get; set; }
		
		[TagDetails(749, TagType.Int)]
		public int? TradeRequestResult { get; set; }
		
		[TagDetails(750, TagType.Int)]
		public int? TradeRequestStatus { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(442, TagType.String)]
		public string? MultiLegReportingType { get; set; }
		
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
