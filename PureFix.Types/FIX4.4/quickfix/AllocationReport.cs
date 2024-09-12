using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class AllocationReport : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? AllocReportID { get; set; } // 755 STRING
		public string? AllocID { get; set; } // 70 STRING
		public string? AllocTransType { get; set; } // 71 CHAR
		public string? AllocReportRefID { get; set; } // 795 STRING
		public int? AllocCancReplaceReason { get; set; } // 796 INT
		public string? SecondaryAllocID { get; set; } // 793 STRING
		public int? AllocReportType { get; set; } // 794 INT
		public int? AllocStatus { get; set; } // 87 INT
		public int? AllocRejCode { get; set; } // 88 INT
		public string? RefAllocID { get; set; } // 72 STRING
		public int? AllocIntermedReqType { get; set; } // 808 INT
		public string? AllocLinkID { get; set; } // 196 STRING
		public int? AllocLinkType { get; set; } // 197 INT
		public string? BookingRefID { get; set; } // 466 STRING
		public int? AllocNoOrdersType { get; set; } // 857 INT
		public OrdAllocGrp? OrdAllocGrp { get; set; }
		public ExecAllocGrp? ExecAllocGrp { get; set; }
		public bool? PreviouslyReported { get; set; } // 570 BOOLEAN
		public bool? ReversalIndicator { get; set; } // 700 BOOLEAN
		public string? MatchType { get; set; } // 574 STRING
		public string? Side { get; set; } // 54 CHAR
		public Instrument? Instrument { get; set; }
		public InstrumentExtension? InstrumentExtension { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public double? Quantity { get; set; } // 53 QTY
		public int? QtyType { get; set; } // 854 INT
		public string? LastMkt { get; set; } // 30 EXCHANGE
		public DateTime? TradeOriginationDate { get; set; } // 229 LOCALMKTDATE
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public int? PriceType { get; set; } // 423 INT
		public double? AvgPx { get; set; } // 6 PRICE
		public double? AvgParPx { get; set; } // 860 PRICE
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		public string? Currency { get; set; } // 15 CURRENCY
		public int? AvgPxPrecision { get; set; } // 74 INT
		public Parties? Parties { get; set; }
		public DateTime? TradeDate { get; set; } // 75 LOCALMKTDATE
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public string? SettlType { get; set; } // 63 CHAR
		public DateTime? SettlDate { get; set; } // 64 LOCALMKTDATE
		public int? BookingType { get; set; } // 775 INT
		public double? GrossTradeAmt { get; set; } // 381 AMT
		public double? Concession { get; set; } // 238 AMT
		public double? TotalTakedown { get; set; } // 237 AMT
		public double? NetMoney { get; set; } // 118 AMT
		public string? PositionEffect { get; set; } // 77 CHAR
		public bool? AutoAcceptIndicator { get; set; } // 754 BOOLEAN
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public int? NumDaysInterest { get; set; } // 157 INT
		public double? AccruedInterestRate { get; set; } // 158 PERCENTAGE
		public double? AccruedInterestAmt { get; set; } // 159 AMT
		public double? TotalAccruedInterestAmt { get; set; } // 540 AMT
		public double? InterestAtMaturity { get; set; } // 738 AMT
		public double? EndAccruedInterestAmt { get; set; } // 920 AMT
		public double? StartCash { get; set; } // 921 AMT
		public double? EndCash { get; set; } // 922 AMT
		public bool? LegalConfirm { get; set; } // 650 BOOLEAN
		public Stipulations? Stipulations { get; set; }
		public YieldData? YieldData { get; set; }
		public int? TotNoAllocs { get; set; } // 892 INT
		public bool? LastFragment { get; set; } // 893 BOOLEAN
		public AllocGrp? AllocGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
