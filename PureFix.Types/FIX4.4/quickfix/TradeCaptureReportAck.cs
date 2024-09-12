using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AR", FixVersion.FIX44)]
	public sealed class TradeCaptureReportAck : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 571, Type = TagType.String, Offset = 1)]
		public string? TradeReportID { get; set; }
		
		[TagDetails(Tag = 487, Type = TagType.Int, Offset = 2)]
		public int? TradeReportTransType { get; set; }
		
		[TagDetails(Tag = 856, Type = TagType.Int, Offset = 3)]
		public int? TradeReportType { get; set; }
		
		[TagDetails(Tag = 828, Type = TagType.Int, Offset = 4)]
		public int? TrdType { get; set; }
		
		[TagDetails(Tag = 829, Type = TagType.Int, Offset = 5)]
		public int? TrdSubType { get; set; }
		
		[TagDetails(Tag = 855, Type = TagType.Int, Offset = 6)]
		public int? SecondaryTrdType { get; set; }
		
		[TagDetails(Tag = 830, Type = TagType.String, Offset = 7)]
		public string? TransferReason { get; set; }
		
		[TagDetails(Tag = 150, Type = TagType.String, Offset = 8)]
		public string? ExecType { get; set; }
		
		[TagDetails(Tag = 572, Type = TagType.String, Offset = 9)]
		public string? TradeReportRefID { get; set; }
		
		[TagDetails(Tag = 881, Type = TagType.String, Offset = 10)]
		public string? SecondaryTradeReportRefID { get; set; }
		
		[TagDetails(Tag = 939, Type = TagType.Int, Offset = 11)]
		public int? TrdRptStatus { get; set; }
		
		[TagDetails(Tag = 751, Type = TagType.Int, Offset = 12)]
		public int? TradeReportRejectReason { get; set; }
		
		[TagDetails(Tag = 818, Type = TagType.String, Offset = 13)]
		public string? SecondaryTradeReportID { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 14)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(Tag = 820, Type = TagType.String, Offset = 15)]
		public string? TradeLinkID { get; set; }
		
		[TagDetails(Tag = 880, Type = TagType.String, Offset = 16)]
		public string? TrdMatchID { get; set; }
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 17)]
		public string? ExecID { get; set; }
		
		[TagDetails(Tag = 527, Type = TagType.String, Offset = 18)]
		public string? SecondaryExecID { get; set; }
		
		[Component(Offset = 19)]
		public Instrument? Instrument { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 20)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 21)]
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		
		[TagDetails(Tag = 725, Type = TagType.Int, Offset = 22)]
		public int? ResponseTransportType { get; set; }
		
		[TagDetails(Tag = 726, Type = TagType.String, Offset = 23)]
		public string? ResponseDestination { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 24)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 25)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 26)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 27)]
		public TrdInstrmtLegGrp? TrdInstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 28)]
		public string? ClearingFeeIndicator { get; set; }
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 29)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 30)]
		public string? OrderRestrictions { get; set; }
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 31)]
		public int? CustOrderCapacity { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 32)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 33)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 34)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 35)]
		public string? PositionEffect { get; set; }
		
		[TagDetails(Tag = 591, Type = TagType.String, Offset = 36)]
		public string? PreallocMethod { get; set; }
		
		[Component(Offset = 37)]
		public TrdAllocGrp? TrdAllocGrp { get; set; }
		
		[Component(Offset = 38)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
