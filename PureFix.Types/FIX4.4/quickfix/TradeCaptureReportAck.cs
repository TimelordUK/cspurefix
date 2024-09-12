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
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 571, Type = TagType.String, Offset = 1, Required = true)]
		public string? TradeReportID { get; set; }
		
		[TagDetails(Tag = 487, Type = TagType.Int, Offset = 2, Required = false)]
		public int? TradeReportTransType { get; set; }
		
		[TagDetails(Tag = 856, Type = TagType.Int, Offset = 3, Required = false)]
		public int? TradeReportType { get; set; }
		
		[TagDetails(Tag = 828, Type = TagType.Int, Offset = 4, Required = false)]
		public int? TrdType { get; set; }
		
		[TagDetails(Tag = 829, Type = TagType.Int, Offset = 5, Required = false)]
		public int? TrdSubType { get; set; }
		
		[TagDetails(Tag = 855, Type = TagType.Int, Offset = 6, Required = false)]
		public int? SecondaryTrdType { get; set; }
		
		[TagDetails(Tag = 830, Type = TagType.String, Offset = 7, Required = false)]
		public string? TransferReason { get; set; }
		
		[TagDetails(Tag = 150, Type = TagType.String, Offset = 8, Required = true)]
		public string? ExecType { get; set; }
		
		[TagDetails(Tag = 572, Type = TagType.String, Offset = 9, Required = false)]
		public string? TradeReportRefID { get; set; }
		
		[TagDetails(Tag = 881, Type = TagType.String, Offset = 10, Required = false)]
		public string? SecondaryTradeReportRefID { get; set; }
		
		[TagDetails(Tag = 939, Type = TagType.Int, Offset = 11, Required = false)]
		public int? TrdRptStatus { get; set; }
		
		[TagDetails(Tag = 751, Type = TagType.Int, Offset = 12, Required = false)]
		public int? TradeReportRejectReason { get; set; }
		
		[TagDetails(Tag = 818, Type = TagType.String, Offset = 13, Required = false)]
		public string? SecondaryTradeReportID { get; set; }
		
		[TagDetails(Tag = 263, Type = TagType.String, Offset = 14, Required = false)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(Tag = 820, Type = TagType.String, Offset = 15, Required = false)]
		public string? TradeLinkID { get; set; }
		
		[TagDetails(Tag = 880, Type = TagType.String, Offset = 16, Required = false)]
		public string? TrdMatchID { get; set; }
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 17, Required = false)]
		public string? ExecID { get; set; }
		
		[TagDetails(Tag = 527, Type = TagType.String, Offset = 18, Required = false)]
		public string? SecondaryExecID { get; set; }
		
		[Component(Offset = 19, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 20, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 21, Required = false)]
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		
		[TagDetails(Tag = 725, Type = TagType.Int, Offset = 22, Required = false)]
		public int? ResponseTransportType { get; set; }
		
		[TagDetails(Tag = 726, Type = TagType.String, Offset = 23, Required = false)]
		public string? ResponseDestination { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 24, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 25, Required = false)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 26, Required = false)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 27, Required = false)]
		public TrdInstrmtLegGrp? TrdInstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 28, Required = false)]
		public string? ClearingFeeIndicator { get; set; }
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 29, Required = false)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 30, Required = false)]
		public string? OrderRestrictions { get; set; }
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 31, Required = false)]
		public int? CustOrderCapacity { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 32, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 33, Required = false)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 34, Required = false)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 35, Required = false)]
		public string? PositionEffect { get; set; }
		
		[TagDetails(Tag = 591, Type = TagType.String, Offset = 36, Required = false)]
		public string? PreallocMethod { get; set; }
		
		[Component(Offset = 37, Required = false)]
		public TrdAllocGrp? TrdAllocGrp { get; set; }
		
		[Component(Offset = 38, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
