using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class TradeCaptureReportAck : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(571, TagType.String)]
		public string? TradeReportID { get; set; }
		
		[TagDetails(487, TagType.Int)]
		public int? TradeReportTransType { get; set; }
		
		[TagDetails(856, TagType.Int)]
		public int? TradeReportType { get; set; }
		
		[TagDetails(828, TagType.Int)]
		public int? TrdType { get; set; }
		
		[TagDetails(829, TagType.Int)]
		public int? TrdSubType { get; set; }
		
		[TagDetails(855, TagType.Int)]
		public int? SecondaryTrdType { get; set; }
		
		[TagDetails(830, TagType.String)]
		public string? TransferReason { get; set; }
		
		[TagDetails(150, TagType.String)]
		public string? ExecType { get; set; }
		
		[TagDetails(572, TagType.String)]
		public string? TradeReportRefID { get; set; }
		
		[TagDetails(881, TagType.String)]
		public string? SecondaryTradeReportRefID { get; set; }
		
		[TagDetails(939, TagType.Int)]
		public int? TrdRptStatus { get; set; }
		
		[TagDetails(751, TagType.Int)]
		public int? TradeReportRejectReason { get; set; }
		
		[TagDetails(818, TagType.String)]
		public string? SecondaryTradeReportID { get; set; }
		
		[TagDetails(263, TagType.String)]
		public string? SubscriptionRequestType { get; set; }
		
		[TagDetails(820, TagType.String)]
		public string? TradeLinkID { get; set; }
		
		[TagDetails(880, TagType.String)]
		public string? TrdMatchID { get; set; }
		
		[TagDetails(17, TagType.String)]
		public string? ExecID { get; set; }
		
		[TagDetails(527, TagType.String)]
		public string? SecondaryExecID { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[Component]
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		
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
		public TrdInstrmtLegGrp? TrdInstrmtLegGrp { get; set; }
		
		[TagDetails(635, TagType.String)]
		public string? ClearingFeeIndicator { get; set; }
		
		[TagDetails(528, TagType.String)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(529, TagType.String)]
		public string? OrderRestrictions { get; set; }
		
		[TagDetails(582, TagType.Int)]
		public int? CustOrderCapacity { get; set; }
		
		[TagDetails(1, TagType.String)]
		public string? Account { get; set; }
		
		[TagDetails(660, TagType.Int)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(581, TagType.Int)]
		public int? AccountType { get; set; }
		
		[TagDetails(77, TagType.String)]
		public string? PositionEffect { get; set; }
		
		[TagDetails(591, TagType.String)]
		public string? PreallocMethod { get; set; }
		
		[Component]
		public TrdAllocGrp? TrdAllocGrp { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
		
	}
}
