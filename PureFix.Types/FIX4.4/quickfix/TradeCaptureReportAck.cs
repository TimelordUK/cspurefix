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
		public override StandardHeader? StandardHeader { get; set; }
		public string? TradeReportID { get; set; } // 571 STRING
		public int? TradeReportTransType { get; set; } // 487 INT
		public int? TradeReportType { get; set; } // 856 INT
		public int? TrdType { get; set; } // 828 INT
		public int? TrdSubType { get; set; } // 829 INT
		public int? SecondaryTrdType { get; set; } // 855 INT
		public string? TransferReason { get; set; } // 830 STRING
		public string? ExecType { get; set; } // 150 CHAR
		public string? TradeReportRefID { get; set; } // 572 STRING
		public string? SecondaryTradeReportRefID { get; set; } // 881 STRING
		public int? TrdRptStatus { get; set; } // 939 INT
		public int? TradeReportRejectReason { get; set; } // 751 INT
		public string? SecondaryTradeReportID { get; set; } // 818 STRING
		public string? SubscriptionRequestType { get; set; } // 263 CHAR
		public string? TradeLinkID { get; set; } // 820 STRING
		public string? TrdMatchID { get; set; } // 880 STRING
		public string? ExecID { get; set; } // 17 STRING
		public string? SecondaryExecID { get; set; } // 527 STRING
		public Instrument? Instrument { get; set; }
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		public int? ResponseTransportType { get; set; } // 725 INT
		public string? ResponseDestination { get; set; } // 726 STRING
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public TrdInstrmtLegGrp? TrdInstrmtLegGrp { get; set; }
		public string? ClearingFeeIndicator { get; set; } // 635 STRING
		public string? OrderCapacity { get; set; } // 528 CHAR
		public string? OrderRestrictions { get; set; } // 529 MULTIPLEVALUESTRING
		public int? CustOrderCapacity { get; set; } // 582 INT
		public string? Account { get; set; } // 1 STRING
		public int? AcctIDSource { get; set; } // 660 INT
		public int? AccountType { get; set; } // 581 INT
		public string? PositionEffect { get; set; } // 77 CHAR
		public string? PreallocMethod { get; set; } // 591 CHAR
		public TrdAllocGrp? TrdAllocGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
