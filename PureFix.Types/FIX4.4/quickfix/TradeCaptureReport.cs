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
		[TagDetails(571)]
		public string? TradeReportID { get; set; } // STRING
		
		[TagDetails(487)]
		public int? TradeReportTransType { get; set; } // INT
		
		[TagDetails(856)]
		public int? TradeReportType { get; set; } // INT
		
		[TagDetails(568)]
		public string? TradeRequestID { get; set; } // STRING
		
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
		
		[TagDetails(748)]
		public int? TotNumTradeReports { get; set; } // INT
		
		[TagDetails(912)]
		public bool? LastRptRequested { get; set; } // BOOLEAN
		
		[TagDetails(325)]
		public bool? UnsolicitedIndicator { get; set; } // BOOLEAN
		
		[TagDetails(263)]
		public string? SubscriptionRequestType { get; set; } // CHAR
		
		[TagDetails(572)]
		public string? TradeReportRefID { get; set; } // STRING
		
		[TagDetails(881)]
		public string? SecondaryTradeReportRefID { get; set; } // STRING
		
		[TagDetails(818)]
		public string? SecondaryTradeReportID { get; set; } // STRING
		
		[TagDetails(820)]
		public string? TradeLinkID { get; set; } // STRING
		
		[TagDetails(880)]
		public string? TrdMatchID { get; set; } // STRING
		
		[TagDetails(17)]
		public string? ExecID { get; set; } // STRING
		
		[TagDetails(39)]
		public string? OrdStatus { get; set; } // CHAR
		
		[TagDetails(527)]
		public string? SecondaryExecID { get; set; } // STRING
		
		[TagDetails(378)]
		public int? ExecRestatementReason { get; set; } // INT
		
		[TagDetails(570)]
		public bool? PreviouslyReported { get; set; } // BOOLEAN
		
		[TagDetails(423)]
		public int? PriceType { get; set; } // INT
		
		public Instrument? Instrument { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public OrderQtyData? OrderQtyData { get; set; }
		[TagDetails(854)]
		public int? QtyType { get; set; } // INT
		
		public YieldData? YieldData { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		[TagDetails(822)]
		public string? UnderlyingTradingSessionID { get; set; } // STRING
		
		[TagDetails(823)]
		public string? UnderlyingTradingSessionSubID { get; set; } // STRING
		
		[TagDetails(32)]
		public double? LastQty { get; set; } // QTY
		
		[TagDetails(31)]
		public double? LastPx { get; set; } // PRICE
		
		[TagDetails(669)]
		public double? LastParPx { get; set; } // PRICE
		
		[TagDetails(194)]
		public double? LastSpotRate { get; set; } // PRICE
		
		[TagDetails(195)]
		public double? LastForwardPoints { get; set; } // PRICEOFFSET
		
		[TagDetails(30)]
		public string? LastMkt { get; set; } // EXCHANGE
		
		[TagDetails(75)]
		public DateTime? TradeDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(715)]
		public DateTime? ClearingBusinessDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(6)]
		public double? AvgPx { get; set; } // PRICE
		
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		[TagDetails(819)]
		public int? AvgPxIndicator { get; set; } // INT
		
		public PositionAmountData? PositionAmountData { get; set; }
		[TagDetails(442)]
		public string? MultiLegReportingType { get; set; } // CHAR
		
		[TagDetails(824)]
		public string? TradeLegRefID { get; set; } // STRING
		
		public TrdInstrmtLegGrp? TrdInstrmtLegGrp { get; set; }
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		[TagDetails(63)]
		public string? SettlType { get; set; } // CHAR
		
		[TagDetails(64)]
		public DateTime? SettlDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(573)]
		public string? MatchStatus { get; set; } // CHAR
		
		[TagDetails(574)]
		public string? MatchType { get; set; } // STRING
		
		public TrdCapRptSideGrp? TrdCapRptSideGrp { get; set; }
		[TagDetails(797)]
		public bool? CopyMsgIndicator { get; set; } // BOOLEAN
		
		[TagDetails(852)]
		public bool? PublishTrdIndicator { get; set; } // BOOLEAN
		
		[TagDetails(853)]
		public int? ShortSaleReason { get; set; } // INT
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
