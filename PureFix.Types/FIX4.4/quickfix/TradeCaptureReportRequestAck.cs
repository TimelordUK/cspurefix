using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AQ", FixVersion.FIX44)]
	public sealed class TradeCaptureReportRequestAck : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 568, Type = TagType.String, Offset = 1)]
		public string? TradeRequestID { get; set; }
		
		[TagDetails(Tag = 569, Type = TagType.Int, Offset = 2)]
		public int? TradeRequestType { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 3)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(Tag = 748, Type = TagType.Int, Offset = 4)]
		public int? TotNumTradeReports { get; set; }
		
		[TagDetails(Tag = 749, Type = TagType.Int, Offset = 5)]
		public int? TradeRequestResult { get; set; }
		
		[TagDetails(Tag = 750, Type = TagType.Int, Offset = 6)]
		public int? TradeRequestStatus { get; set; }
		
		[Component(Offset = 7)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 8)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 9)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 442, Type = TagType.String, Offset = 10)]
		public string? MultiLegReportingType { get; set; }
		
		[TagDetails(Tag = 725, Type = TagType.Int, Offset = 11)]
		public int? ResponseTransportType { get; set; }
		
		[TagDetails(Tag = 726, Type = TagType.String, Offset = 12)]
		public string? ResponseDestination { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 13)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 14)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 15)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 16)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
