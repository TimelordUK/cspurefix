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
		[TagDetails(571)]
		public string? TradeReportID { get; set; } // STRING
		
		[TagDetails(487)]
		public int? TradeReportTransType { get; set; } // INT
		
		[TagDetails(856)]
		public int? TradeReportType { get; set; } // INT
		
		[TagDetails(828)]
		public int? TrdType { get; set; } // INT
		
		[TagDetails(829)]
		public int? TrdSubType { get; set; } // INT
		
		[TagDetails(855)]
		public int? SecondaryTrdType { get; set; } // INT
		
		[TagDetails(830)]
		public string? TransferReason { get; set; } // STRING
		
		[TagDetails(150)]
		public string? ExecType { get; set; } // CHAR
		
		[TagDetails(572)]
		public string? TradeReportRefID { get; set; } // STRING
		
		[TagDetails(881)]
		public string? SecondaryTradeReportRefID { get; set; } // STRING
		
		[TagDetails(939)]
		public int? TrdRptStatus { get; set; } // INT
		
		[TagDetails(751)]
		public int? TradeReportRejectReason { get; set; } // INT
		
		[TagDetails(818)]
		public string? SecondaryTradeReportID { get; set; } // STRING
		
		[TagDetails(263)]
		public string? SubscriptionRequestType { get; set; } // CHAR
		
		[TagDetails(820)]
		public string? TradeLinkID { get; set; } // STRING
		
		[TagDetails(880)]
		public string? TrdMatchID { get; set; } // STRING
		
		[TagDetails(17)]
		public string? ExecID { get; set; } // STRING
		
		[TagDetails(527)]
		public string? SecondaryExecID { get; set; } // STRING
		
		public Instrument? Instrument { get; set; }
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		[TagDetails(725)]
		public int? ResponseTransportType { get; set; } // INT
		
		[TagDetails(726)]
		public string? ResponseDestination { get; set; } // STRING
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public TrdInstrmtLegGrp? TrdInstrmtLegGrp { get; set; }
		[TagDetails(635)]
		public string? ClearingFeeIndicator { get; set; } // STRING
		
		[TagDetails(528)]
		public string? OrderCapacity { get; set; } // CHAR
		
		[TagDetails(529)]
		public string? OrderRestrictions { get; set; } // MULTIPLEVALUESTRING
		
		[TagDetails(582)]
		public int? CustOrderCapacity { get; set; } // INT
		
		[TagDetails(1)]
		public string? Account { get; set; } // STRING
		
		[TagDetails(660)]
		public int? AcctIDSource { get; set; } // INT
		
		[TagDetails(581)]
		public int? AccountType { get; set; } // INT
		
		[TagDetails(77)]
		public string? PositionEffect { get; set; } // CHAR
		
		[TagDetails(591)]
		public string? PreallocMethod { get; set; } // CHAR
		
		public TrdAllocGrp? TrdAllocGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
