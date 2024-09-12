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
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(568)]
		public string? TradeRequestID { get; set; } // STRING
		
		[TagDetails(569)]
		public int? TradeRequestType { get; set; } // INT
		
		[TagDetails(263)]
		public string? SubscriptionRequestType { get; set; } // CHAR
		
		[TagDetails(748)]
		public int? TotNumTradeReports { get; set; } // INT
		
		[TagDetails(749)]
		public int? TradeRequestResult { get; set; } // INT
		
		[TagDetails(750)]
		public int? TradeRequestStatus { get; set; } // INT
		
		public Instrument? Instrument { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		[TagDetails(442)]
		public string? MultiLegReportingType { get; set; } // CHAR
		
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
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
