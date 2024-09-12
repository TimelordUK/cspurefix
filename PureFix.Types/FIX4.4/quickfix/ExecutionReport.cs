using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class ExecutionReport : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(37)]
		public string? OrderID { get; set; } // STRING
		
		[TagDetails(198)]
		public string? SecondaryOrderID { get; set; } // STRING
		
		[TagDetails(526)]
		public string? SecondaryClOrdID { get; set; } // STRING
		
		[TagDetails(527)]
		public string? SecondaryExecID { get; set; } // STRING
		
		[TagDetails(11)]
		public string? ClOrdID { get; set; } // STRING
		
		[TagDetails(41)]
		public string? OrigClOrdID { get; set; } // STRING
		
		[TagDetails(583)]
		public string? ClOrdLinkID { get; set; } // STRING
		
		[TagDetails(693)]
		public string? QuoteRespID { get; set; } // STRING
		
		[TagDetails(790)]
		public string? OrdStatusReqID { get; set; } // STRING
		
		[TagDetails(584)]
		public string? MassStatusReqID { get; set; } // STRING
		
		[TagDetails(911)]
		public int? TotNumReports { get; set; } // INT
		
		[TagDetails(912)]
		public bool? LastRptRequested { get; set; } // BOOLEAN
		
		public Parties? Parties { get; set; }
		[TagDetails(229)]
		public DateTime? TradeOriginationDate { get; set; } // LOCALMKTDATE
		
		public ContraGrp? ContraGrp { get; set; }
		[TagDetails(66)]
		public string? ListID { get; set; } // STRING
		
		[TagDetails(548)]
		public string? CrossID { get; set; } // STRING
		
		[TagDetails(551)]
		public string? OrigCrossID { get; set; } // STRING
		
		[TagDetails(549)]
		public int? CrossType { get; set; } // INT
		
		[TagDetails(17)]
		public string? ExecID { get; set; } // STRING
		
		[TagDetails(19)]
		public string? ExecRefID { get; set; } // STRING
		
		[TagDetails(150)]
		public string? ExecType { get; set; } // CHAR
		
		[TagDetails(39)]
		public string? OrdStatus { get; set; } // CHAR
		
		[TagDetails(636)]
		public bool? WorkingIndicator { get; set; } // BOOLEAN
		
		[TagDetails(103)]
		public int? OrdRejReason { get; set; } // INT
		
		[TagDetails(378)]
		public int? ExecRestatementReason { get; set; } // INT
		
		[TagDetails(1)]
		public string? Account { get; set; } // STRING
		
		[TagDetails(660)]
		public int? AcctIDSource { get; set; } // INT
		
		[TagDetails(581)]
		public int? AccountType { get; set; } // INT
		
		[TagDetails(589)]
		public string? DayBookingInst { get; set; } // CHAR
		
		[TagDetails(590)]
		public string? BookingUnit { get; set; } // CHAR
		
		[TagDetails(591)]
		public string? PreallocMethod { get; set; } // CHAR
		
		[TagDetails(63)]
		public string? SettlType { get; set; } // CHAR
		
		[TagDetails(64)]
		public DateTime? SettlDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(544)]
		public string? CashMargin { get; set; } // CHAR
		
		[TagDetails(635)]
		public string? ClearingFeeIndicator { get; set; } // STRING
		
		public Instrument? Instrument { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		[TagDetails(54)]
		public string? Side { get; set; } // CHAR
		
		public Stipulations? Stipulations { get; set; }
		[TagDetails(854)]
		public int? QtyType { get; set; } // INT
		
		public OrderQtyData? OrderQtyData { get; set; }
		[TagDetails(40)]
		public string? OrdType { get; set; } // CHAR
		
		[TagDetails(423)]
		public int? PriceType { get; set; } // INT
		
		[TagDetails(44)]
		public double? Price { get; set; } // PRICE
		
		[TagDetails(99)]
		public double? StopPx { get; set; } // PRICE
		
		public PegInstructions? PegInstructions { get; set; }
		public DiscretionInstructions? DiscretionInstructions { get; set; }
		[TagDetails(839)]
		public double? PeggedPrice { get; set; } // PRICE
		
		[TagDetails(845)]
		public double? DiscretionPrice { get; set; } // PRICE
		
		[TagDetails(847)]
		public int? TargetStrategy { get; set; } // INT
		
		[TagDetails(848)]
		public string? TargetStrategyParameters { get; set; } // STRING
		
		[TagDetails(849)]
		public double? ParticipationRate { get; set; } // PERCENTAGE
		
		[TagDetails(850)]
		public double? TargetStrategyPerformance { get; set; } // FLOAT
		
		[TagDetails(15)]
		public string? Currency { get; set; } // CURRENCY
		
		[TagDetails(376)]
		public string? ComplianceID { get; set; } // STRING
		
		[TagDetails(377)]
		public bool? SolicitedFlag { get; set; } // BOOLEAN
		
		[TagDetails(59)]
		public string? TimeInForce { get; set; } // CHAR
		
		[TagDetails(168)]
		public DateTime? EffectiveTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(432)]
		public DateTime? ExpireDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(126)]
		public DateTime? ExpireTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(18)]
		public string? ExecInst { get; set; } // MULTIPLEVALUESTRING
		
		[TagDetails(528)]
		public string? OrderCapacity { get; set; } // CHAR
		
		[TagDetails(529)]
		public string? OrderRestrictions { get; set; } // MULTIPLEVALUESTRING
		
		[TagDetails(582)]
		public int? CustOrderCapacity { get; set; } // INT
		
		[TagDetails(32)]
		public double? LastQty { get; set; } // QTY
		
		[TagDetails(652)]
		public double? UnderlyingLastQty { get; set; } // QTY
		
		[TagDetails(31)]
		public double? LastPx { get; set; } // PRICE
		
		[TagDetails(651)]
		public double? UnderlyingLastPx { get; set; } // PRICE
		
		[TagDetails(669)]
		public double? LastParPx { get; set; } // PRICE
		
		[TagDetails(194)]
		public double? LastSpotRate { get; set; } // PRICE
		
		[TagDetails(195)]
		public double? LastForwardPoints { get; set; } // PRICEOFFSET
		
		[TagDetails(30)]
		public string? LastMkt { get; set; } // EXCHANGE
		
		[TagDetails(336)]
		public string? TradingSessionID { get; set; } // STRING
		
		[TagDetails(625)]
		public string? TradingSessionSubID { get; set; } // STRING
		
		[TagDetails(943)]
		public string? TimeBracket { get; set; } // STRING
		
		[TagDetails(29)]
		public string? LastCapacity { get; set; } // CHAR
		
		[TagDetails(151)]
		public double? LeavesQty { get; set; } // QTY
		
		[TagDetails(14)]
		public double? CumQty { get; set; } // QTY
		
		[TagDetails(6)]
		public double? AvgPx { get; set; } // PRICE
		
		[TagDetails(424)]
		public double? DayOrderQty { get; set; } // QTY
		
		[TagDetails(425)]
		public double? DayCumQty { get; set; } // QTY
		
		[TagDetails(426)]
		public double? DayAvgPx { get; set; } // PRICE
		
		[TagDetails(427)]
		public int? GTBookingInst { get; set; } // INT
		
		[TagDetails(75)]
		public DateTime? TradeDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(113)]
		public bool? ReportToExch { get; set; } // BOOLEAN
		
		public CommissionData? CommissionData { get; set; }
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		public YieldData? YieldData { get; set; }
		[TagDetails(381)]
		public double? GrossTradeAmt { get; set; } // AMT
		
		[TagDetails(157)]
		public int? NumDaysInterest { get; set; } // INT
		
		[TagDetails(230)]
		public DateTime? ExDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(158)]
		public double? AccruedInterestRate { get; set; } // PERCENTAGE
		
		[TagDetails(159)]
		public double? AccruedInterestAmt { get; set; } // AMT
		
		[TagDetails(738)]
		public double? InterestAtMaturity { get; set; } // AMT
		
		[TagDetails(920)]
		public double? EndAccruedInterestAmt { get; set; } // AMT
		
		[TagDetails(921)]
		public double? StartCash { get; set; } // AMT
		
		[TagDetails(922)]
		public double? EndCash { get; set; } // AMT
		
		[TagDetails(258)]
		public bool? TradedFlatSwitch { get; set; } // BOOLEAN
		
		[TagDetails(259)]
		public DateTime? BasisFeatureDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(260)]
		public double? BasisFeaturePrice { get; set; } // PRICE
		
		[TagDetails(238)]
		public double? Concession { get; set; } // AMT
		
		[TagDetails(237)]
		public double? TotalTakedown { get; set; } // AMT
		
		[TagDetails(118)]
		public double? NetMoney { get; set; } // AMT
		
		[TagDetails(119)]
		public double? SettlCurrAmt { get; set; } // AMT
		
		[TagDetails(120)]
		public string? SettlCurrency { get; set; } // CURRENCY
		
		[TagDetails(155)]
		public double? SettlCurrFxRate { get; set; } // FLOAT
		
		[TagDetails(156)]
		public string? SettlCurrFxRateCalc { get; set; } // CHAR
		
		[TagDetails(21)]
		public string? HandlInst { get; set; } // CHAR
		
		[TagDetails(110)]
		public double? MinQty { get; set; } // QTY
		
		[TagDetails(111)]
		public double? MaxFloor { get; set; } // QTY
		
		[TagDetails(77)]
		public string? PositionEffect { get; set; } // CHAR
		
		[TagDetails(210)]
		public double? MaxShow { get; set; } // QTY
		
		[TagDetails(775)]
		public int? BookingType { get; set; } // INT
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		[TagDetails(193)]
		public DateTime? SettlDate2 { get; set; } // LOCALMKTDATE
		
		[TagDetails(192)]
		public double? OrderQty2 { get; set; } // QTY
		
		[TagDetails(641)]
		public double? LastForwardPoints2 { get; set; } // PRICEOFFSET
		
		[TagDetails(442)]
		public string? MultiLegReportingType { get; set; } // CHAR
		
		[TagDetails(480)]
		public string? CancellationRights { get; set; } // CHAR
		
		[TagDetails(481)]
		public string? MoneyLaunderingStatus { get; set; } // CHAR
		
		[TagDetails(513)]
		public string? RegistID { get; set; } // STRING
		
		[TagDetails(494)]
		public string? Designation { get; set; } // STRING
		
		[TagDetails(483)]
		public DateTime? TransBkdTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(515)]
		public DateTime? ExecValuationPoint { get; set; } // UTCTIMESTAMP
		
		[TagDetails(484)]
		public string? ExecPriceType { get; set; } // CHAR
		
		[TagDetails(485)]
		public double? ExecPriceAdjustment { get; set; } // FLOAT
		
		[TagDetails(638)]
		public int? PriorityIndicator { get; set; } // INT
		
		[TagDetails(639)]
		public double? PriceImprovement { get; set; } // PRICEOFFSET
		
		[TagDetails(851)]
		public int? LastLiquidityInd { get; set; } // INT
		
		public ContAmtGrp? ContAmtGrp { get; set; }
		public InstrmtLegExecGrp? InstrmtLegExecGrp { get; set; }
		[TagDetails(797)]
		public bool? CopyMsgIndicator { get; set; } // BOOLEAN
		
		public MiscFeesGrp? MiscFeesGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
