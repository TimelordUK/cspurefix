using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class TradeCaptureReport : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? TradeReportID { get; set; } // 571 STRING
		public int? TradeReportTransType { get; set; } // 487 INT
		public int? TradeReportType { get; set; } // 856 INT
		public string? TradeRequestID { get; set; } // 568 STRING
		public int? TrdType { get; set; } // 828 INT
		public int? TrdSubType { get; set; } // 829 INT
		public int? SecondaryTrdType { get; set; } // 855 INT
		public string? TransferReason { get; set; } // 830 STRING
		public string? ExecType { get; set; } // 150 CHAR
		public int? TotNumTradeReports { get; set; } // 748 INT
		public bool? LastRptRequested { get; set; } // 912 BOOLEAN
		public bool? UnsolicitedIndicator { get; set; } // 325 BOOLEAN
		public string? SubscriptionRequestType { get; set; } // 263 CHAR
		public string? TradeReportRefID { get; set; } // 572 STRING
		public string? SecondaryTradeReportRefID { get; set; } // 881 STRING
		public string? SecondaryTradeReportID { get; set; } // 818 STRING
		public string? TradeLinkID { get; set; } // 820 STRING
		public string? TrdMatchID { get; set; } // 880 STRING
		public string? ExecID { get; set; } // 17 STRING
		public string? OrdStatus { get; set; } // 39 CHAR
		public string? SecondaryExecID { get; set; } // 527 STRING
		public int? ExecRestatementReason { get; set; } // 378 INT
		public bool? PreviouslyReported { get; set; } // 570 BOOLEAN
		public int? PriceType { get; set; } // 423 INT
		public Instrument? Instrument { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public OrderQtyData? OrderQtyData { get; set; }
		public int? QtyType { get; set; } // 854 INT
		public YieldData? YieldData { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public string? UnderlyingTradingSessionID { get; set; } // 822 STRING
		public string? UnderlyingTradingSessionSubID { get; set; } // 823 STRING
		public double? LastQty { get; set; } // 32 QTY
		public double? LastPx { get; set; } // 31 PRICE
		public double? LastParPx { get; set; } // 669 PRICE
		public double? LastSpotRate { get; set; } // 194 PRICE
		public double? LastForwardPoints { get; set; } // 195 PRICEOFFSET
		public string? LastMkt { get; set; } // 30 EXCHANGE
		public DateTime? TradeDate { get; set; } // 75 LOCALMKTDATE
		public DateTime? ClearingBusinessDate { get; set; } // 715 LOCALMKTDATE
		public double? AvgPx { get; set; } // 6 PRICE
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		public int? AvgPxIndicator { get; set; } // 819 INT
		public PositionAmountData? PositionAmountData { get; set; }
		public string? MultiLegReportingType { get; set; } // 442 CHAR
		public string? TradeLegRefID { get; set; } // 824 STRING
		public TrdInstrmtLegGrp? TrdInstrmtLegGrp { get; set; }
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		public string? SettlType { get; set; } // 63 CHAR
		public DateTime? SettlDate { get; set; } // 64 LOCALMKTDATE
		public string? MatchStatus { get; set; } // 573 CHAR
		public string? MatchType { get; set; } // 574 STRING
		public TrdCapRptSideGrp? TrdCapRptSideGrp { get; set; }
		public bool? CopyMsgIndicator { get; set; } // 797 BOOLEAN
		public bool? PublishTrdIndicator { get; set; } // 852 BOOLEAN
		public int? ShortSaleReason { get; set; } // 853 INT
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
