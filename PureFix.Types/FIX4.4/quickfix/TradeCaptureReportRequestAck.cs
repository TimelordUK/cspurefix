using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class TradeCaptureReportRequestAck : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? TradeRequestID { get; set; } // 568 STRING
		public int? TradeRequestType { get; set; } // 569 INT
		public string? SubscriptionRequestType { get; set; } // 263 CHAR
		public int? TotNumTradeReports { get; set; } // 748 INT
		public int? TradeRequestResult { get; set; } // 749 INT
		public int? TradeRequestStatus { get; set; } // 750 INT
		public Instrument? Instrument { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public string? MultiLegReportingType { get; set; } // 442 CHAR
		public int? ResponseTransportType { get; set; } // 725 INT
		public string? ResponseDestination { get; set; } // 726 STRING
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
