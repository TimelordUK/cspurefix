using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class ExecutionReport : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? OrderID { get; set; } // 37 STRING
		public string? SecondaryOrderID { get; set; } // 198 STRING
		public string? SecondaryClOrdID { get; set; } // 526 STRING
		public string? SecondaryExecID { get; set; } // 527 STRING
		public string? ClOrdID { get; set; } // 11 STRING
		public string? OrigClOrdID { get; set; } // 41 STRING
		public string? ClOrdLinkID { get; set; } // 583 STRING
		public string? QuoteRespID { get; set; } // 693 STRING
		public string? OrdStatusReqID { get; set; } // 790 STRING
		public string? MassStatusReqID { get; set; } // 584 STRING
		public int? TotNumReports { get; set; } // 911 INT
		public bool? LastRptRequested { get; set; } // 912 BOOLEAN
		public Parties? Parties { get; set; }
		public DateTime? TradeOriginationDate { get; set; } // 229 LOCALMKTDATE
		public ContraGrp? ContraGrp { get; set; }
		public string? ListID { get; set; } // 66 STRING
		public string? CrossID { get; set; } // 548 STRING
		public string? OrigCrossID { get; set; } // 551 STRING
		public int? CrossType { get; set; } // 549 INT
		public string? ExecID { get; set; } // 17 STRING
		public string? ExecRefID { get; set; } // 19 STRING
		public string? ExecType { get; set; } // 150 CHAR
		public string? OrdStatus { get; set; } // 39 CHAR
		public bool? WorkingIndicator { get; set; } // 636 BOOLEAN
		public int? OrdRejReason { get; set; } // 103 INT
		public int? ExecRestatementReason { get; set; } // 378 INT
		public string? Account { get; set; } // 1 STRING
		public int? AcctIDSource { get; set; } // 660 INT
		public int? AccountType { get; set; } // 581 INT
		public string? DayBookingInst { get; set; } // 589 CHAR
		public string? BookingUnit { get; set; } // 590 CHAR
		public string? PreallocMethod { get; set; } // 591 CHAR
		public string? SettlType { get; set; } // 63 CHAR
		public DateTime? SettlDate { get; set; } // 64 LOCALMKTDATE
		public string? CashMargin { get; set; } // 544 CHAR
		public string? ClearingFeeIndicator { get; set; } // 635 STRING
		public Instrument? Instrument { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public string? Side { get; set; } // 54 CHAR
		public Stipulations? Stipulations { get; set; }
		public int? QtyType { get; set; } // 854 INT
		public OrderQtyData? OrderQtyData { get; set; }
		public string? OrdType { get; set; } // 40 CHAR
		public int? PriceType { get; set; } // 423 INT
		public double? Price { get; set; } // 44 PRICE
		public double? StopPx { get; set; } // 99 PRICE
		public PegInstructions? PegInstructions { get; set; }
		public DiscretionInstructions? DiscretionInstructions { get; set; }
		public double? PeggedPrice { get; set; } // 839 PRICE
		public double? DiscretionPrice { get; set; } // 845 PRICE
		public int? TargetStrategy { get; set; } // 847 INT
		public string? TargetStrategyParameters { get; set; } // 848 STRING
		public double? ParticipationRate { get; set; } // 849 PERCENTAGE
		public double? TargetStrategyPerformance { get; set; } // 850 FLOAT
		public string? Currency { get; set; } // 15 CURRENCY
		public string? ComplianceID { get; set; } // 376 STRING
		public bool? SolicitedFlag { get; set; } // 377 BOOLEAN
		public string? TimeInForce { get; set; } // 59 CHAR
		public DateTime? EffectiveTime { get; set; } // 168 UTCTIMESTAMP
		public DateTime? ExpireDate { get; set; } // 432 LOCALMKTDATE
		public DateTime? ExpireTime { get; set; } // 126 UTCTIMESTAMP
		public string? ExecInst { get; set; } // 18 MULTIPLEVALUESTRING
		public string? OrderCapacity { get; set; } // 528 CHAR
		public string? OrderRestrictions { get; set; } // 529 MULTIPLEVALUESTRING
		public int? CustOrderCapacity { get; set; } // 582 INT
		public double? LastQty { get; set; } // 32 QTY
		public double? UnderlyingLastQty { get; set; } // 652 QTY
		public double? LastPx { get; set; } // 31 PRICE
		public double? UnderlyingLastPx { get; set; } // 651 PRICE
		public double? LastParPx { get; set; } // 669 PRICE
		public double? LastSpotRate { get; set; } // 194 PRICE
		public double? LastForwardPoints { get; set; } // 195 PRICEOFFSET
		public string? LastMkt { get; set; } // 30 EXCHANGE
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public string? TimeBracket { get; set; } // 943 STRING
		public string? LastCapacity { get; set; } // 29 CHAR
		public double? LeavesQty { get; set; } // 151 QTY
		public double? CumQty { get; set; } // 14 QTY
		public double? AvgPx { get; set; } // 6 PRICE
		public double? DayOrderQty { get; set; } // 424 QTY
		public double? DayCumQty { get; set; } // 425 QTY
		public double? DayAvgPx { get; set; } // 426 PRICE
		public int? GTBookingInst { get; set; } // 427 INT
		public DateTime? TradeDate { get; set; } // 75 LOCALMKTDATE
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public bool? ReportToExch { get; set; } // 113 BOOLEAN
		public CommissionData? CommissionData { get; set; }
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		public YieldData? YieldData { get; set; }
		public double? GrossTradeAmt { get; set; } // 381 AMT
		public int? NumDaysInterest { get; set; } // 157 INT
		public DateTime? ExDate { get; set; } // 230 LOCALMKTDATE
		public double? AccruedInterestRate { get; set; } // 158 PERCENTAGE
		public double? AccruedInterestAmt { get; set; } // 159 AMT
		public double? InterestAtMaturity { get; set; } // 738 AMT
		public double? EndAccruedInterestAmt { get; set; } // 920 AMT
		public double? StartCash { get; set; } // 921 AMT
		public double? EndCash { get; set; } // 922 AMT
		public bool? TradedFlatSwitch { get; set; } // 258 BOOLEAN
		public DateTime? BasisFeatureDate { get; set; } // 259 LOCALMKTDATE
		public double? BasisFeaturePrice { get; set; } // 260 PRICE
		public double? Concession { get; set; } // 238 AMT
		public double? TotalTakedown { get; set; } // 237 AMT
		public double? NetMoney { get; set; } // 118 AMT
		public double? SettlCurrAmt { get; set; } // 119 AMT
		public string? SettlCurrency { get; set; } // 120 CURRENCY
		public double? SettlCurrFxRate { get; set; } // 155 FLOAT
		public string? SettlCurrFxRateCalc { get; set; } // 156 CHAR
		public string? HandlInst { get; set; } // 21 CHAR
		public double? MinQty { get; set; } // 110 QTY
		public double? MaxFloor { get; set; } // 111 QTY
		public string? PositionEffect { get; set; } // 77 CHAR
		public double? MaxShow { get; set; } // 210 QTY
		public int? BookingType { get; set; } // 775 INT
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public DateTime? SettlDate2 { get; set; } // 193 LOCALMKTDATE
		public double? OrderQty2 { get; set; } // 192 QTY
		public double? LastForwardPoints2 { get; set; } // 641 PRICEOFFSET
		public string? MultiLegReportingType { get; set; } // 442 CHAR
		public string? CancellationRights { get; set; } // 480 CHAR
		public string? MoneyLaunderingStatus { get; set; } // 481 CHAR
		public string? RegistID { get; set; } // 513 STRING
		public string? Designation { get; set; } // 494 STRING
		public DateTime? TransBkdTime { get; set; } // 483 UTCTIMESTAMP
		public DateTime? ExecValuationPoint { get; set; } // 515 UTCTIMESTAMP
		public string? ExecPriceType { get; set; } // 484 CHAR
		public double? ExecPriceAdjustment { get; set; } // 485 FLOAT
		public int? PriorityIndicator { get; set; } // 638 INT
		public double? PriceImprovement { get; set; } // 639 PRICEOFFSET
		public int? LastLiquidityInd { get; set; } // 851 INT
		public ContAmtGrp? ContAmtGrp { get; set; }
		public InstrmtLegExecGrp? InstrmtLegExecGrp { get; set; }
		public bool? CopyMsgIndicator { get; set; } // 797 BOOLEAN
		public MiscFeesGrp? MiscFeesGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
