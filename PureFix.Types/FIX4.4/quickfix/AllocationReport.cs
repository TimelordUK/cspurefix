using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class AllocationReport : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(755)]
		public string? AllocReportID { get; set; } // STRING
		
		[TagDetails(70)]
		public string? AllocID { get; set; } // STRING
		
		[TagDetails(71)]
		public string? AllocTransType { get; set; } // CHAR
		
		[TagDetails(795)]
		public string? AllocReportRefID { get; set; } // STRING
		
		[TagDetails(796)]
		public int? AllocCancReplaceReason { get; set; } // INT
		
		[TagDetails(793)]
		public string? SecondaryAllocID { get; set; } // STRING
		
		[TagDetails(794)]
		public int? AllocReportType { get; set; } // INT
		
		[TagDetails(87)]
		public int? AllocStatus { get; set; } // INT
		
		[TagDetails(88)]
		public int? AllocRejCode { get; set; } // INT
		
		[TagDetails(72)]
		public string? RefAllocID { get; set; } // STRING
		
		[TagDetails(808)]
		public int? AllocIntermedReqType { get; set; } // INT
		
		[TagDetails(196)]
		public string? AllocLinkID { get; set; } // STRING
		
		[TagDetails(197)]
		public int? AllocLinkType { get; set; } // INT
		
		[TagDetails(466)]
		public string? BookingRefID { get; set; } // STRING
		
		[TagDetails(857)]
		public int? AllocNoOrdersType { get; set; } // INT
		
		public OrdAllocGrp? OrdAllocGrp { get; set; }
		public ExecAllocGrp? ExecAllocGrp { get; set; }
		[TagDetails(570)]
		public bool? PreviouslyReported { get; set; } // BOOLEAN
		
		[TagDetails(700)]
		public bool? ReversalIndicator { get; set; } // BOOLEAN
		
		[TagDetails(574)]
		public string? MatchType { get; set; } // STRING
		
		[TagDetails(54)]
		public string? Side { get; set; } // CHAR
		
		public Instrument? Instrument { get; set; }
		public InstrumentExtension? InstrumentExtension { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		[TagDetails(53)]
		public double? Quantity { get; set; } // QTY
		
		[TagDetails(854)]
		public int? QtyType { get; set; } // INT
		
		[TagDetails(30)]
		public string? LastMkt { get; set; } // EXCHANGE
		
		[TagDetails(229)]
		public DateTime? TradeOriginationDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(336)]
		public string? TradingSessionID { get; set; } // STRING
		
		[TagDetails(625)]
		public string? TradingSessionSubID { get; set; } // STRING
		
		[TagDetails(423)]
		public int? PriceType { get; set; } // INT
		
		[TagDetails(6)]
		public double? AvgPx { get; set; } // PRICE
		
		[TagDetails(860)]
		public double? AvgParPx { get; set; } // PRICE
		
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		[TagDetails(15)]
		public string? Currency { get; set; } // CURRENCY
		
		[TagDetails(74)]
		public int? AvgPxPrecision { get; set; } // INT
		
		public Parties? Parties { get; set; }
		[TagDetails(75)]
		public DateTime? TradeDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(63)]
		public string? SettlType { get; set; } // CHAR
		
		[TagDetails(64)]
		public DateTime? SettlDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(775)]
		public int? BookingType { get; set; } // INT
		
		[TagDetails(381)]
		public double? GrossTradeAmt { get; set; } // AMT
		
		[TagDetails(238)]
		public double? Concession { get; set; } // AMT
		
		[TagDetails(237)]
		public double? TotalTakedown { get; set; } // AMT
		
		[TagDetails(118)]
		public double? NetMoney { get; set; } // AMT
		
		[TagDetails(77)]
		public string? PositionEffect { get; set; } // CHAR
		
		[TagDetails(754)]
		public bool? AutoAcceptIndicator { get; set; } // BOOLEAN
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		[TagDetails(157)]
		public int? NumDaysInterest { get; set; } // INT
		
		[TagDetails(158)]
		public double? AccruedInterestRate { get; set; } // PERCENTAGE
		
		[TagDetails(159)]
		public double? AccruedInterestAmt { get; set; } // AMT
		
		[TagDetails(540)]
		public double? TotalAccruedInterestAmt { get; set; } // AMT
		
		[TagDetails(738)]
		public double? InterestAtMaturity { get; set; } // AMT
		
		[TagDetails(920)]
		public double? EndAccruedInterestAmt { get; set; } // AMT
		
		[TagDetails(921)]
		public double? StartCash { get; set; } // AMT
		
		[TagDetails(922)]
		public double? EndCash { get; set; } // AMT
		
		[TagDetails(650)]
		public bool? LegalConfirm { get; set; } // BOOLEAN
		
		public Stipulations? Stipulations { get; set; }
		public YieldData? YieldData { get; set; }
		[TagDetails(892)]
		public int? TotNoAllocs { get; set; } // INT
		
		[TagDetails(893)]
		public bool? LastFragment { get; set; } // BOOLEAN
		
		public AllocGrp? AllocGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
