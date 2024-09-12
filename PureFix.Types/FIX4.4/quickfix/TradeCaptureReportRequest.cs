using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class TradeCaptureReportRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? TradeRequestID { get; set; } // 568 STRING
		public int? TradeRequestType { get; set; } // 569 INT
		public string? SubscriptionRequestType { get; set; } // 263 CHAR
		public string? TradeReportID { get; set; } // 571 STRING
		public string? SecondaryTradeReportID { get; set; } // 818 STRING
		public string? ExecID { get; set; } // 17 STRING
		public string? ExecType { get; set; } // 150 CHAR
		public string? OrderID { get; set; } // 37 STRING
		public string? ClOrdID { get; set; } // 11 STRING
		public string? MatchStatus { get; set; } // 573 CHAR
		public int? TrdType { get; set; } // 828 INT
		public int? TrdSubType { get; set; } // 829 INT
		public string? TransferReason { get; set; } // 830 STRING
		public int? SecondaryTrdType { get; set; } // 855 INT
		public string? TradeLinkID { get; set; } // 820 STRING
		public string? TrdMatchID { get; set; } // 880 STRING
		public Parties? Parties { get; set; }
		public Instrument? Instrument { get; set; }
		public InstrumentExtension? InstrumentExtension { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public TrdCapDtGrp? TrdCapDtGrp { get; set; }
		public DateTime? ClearingBusinessDate { get; set; } // 715 LOCALMKTDATE
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public string? TimeBracket { get; set; } // 943 STRING
		public string? Side { get; set; } // 54 CHAR
		public string? MultiLegReportingType { get; set; } // 442 CHAR
		public string? TradeInputSource { get; set; } // 578 STRING
		public string? TradeInputDevice { get; set; } // 579 STRING
		public int? ResponseTransportType { get; set; } // 725 INT
		public string? ResponseDestination { get; set; } // 726 STRING
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
