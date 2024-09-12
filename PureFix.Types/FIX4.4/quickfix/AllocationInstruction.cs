using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("J")]
	public sealed class AllocationInstruction : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(70, TagType.String)]
		public string? AllocID { get; set; }
		
		[TagDetails(71, TagType.String)]
		public string? AllocTransType { get; set; }
		
		[TagDetails(626, TagType.Int)]
		public int? AllocType { get; set; }
		
		[TagDetails(793, TagType.String)]
		public string? SecondaryAllocID { get; set; }
		
		[TagDetails(72, TagType.String)]
		public string? RefAllocID { get; set; }
		
		[TagDetails(796, TagType.Int)]
		public int? AllocCancReplaceReason { get; set; }
		
		[TagDetails(808, TagType.Int)]
		public int? AllocIntermedReqType { get; set; }
		
		[TagDetails(196, TagType.String)]
		public string? AllocLinkID { get; set; }
		
		[TagDetails(197, TagType.Int)]
		public int? AllocLinkType { get; set; }
		
		[TagDetails(466, TagType.String)]
		public string? BookingRefID { get; set; }
		
		[TagDetails(857, TagType.Int)]
		public int? AllocNoOrdersType { get; set; }
		
		[Component]
		public OrdAllocGrp? OrdAllocGrp { get; set; }
		
		[Component]
		public ExecAllocGrp? ExecAllocGrp { get; set; }
		
		[TagDetails(570, TagType.Boolean)]
		public bool? PreviouslyReported { get; set; }
		
		[TagDetails(700, TagType.Boolean)]
		public bool? ReversalIndicator { get; set; }
		
		[TagDetails(574, TagType.String)]
		public string? MatchType { get; set; }
		
		[TagDetails(54, TagType.String)]
		public string? Side { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[Component]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(53, TagType.Float)]
		public double? Quantity { get; set; }
		
		[TagDetails(854, TagType.Int)]
		public int? QtyType { get; set; }
		
		[TagDetails(30, TagType.String)]
		public string? LastMkt { get; set; }
		
		[TagDetails(229, TagType.LocalDate)]
		public DateTime? TradeOriginationDate { get; set; }
		
		[TagDetails(336, TagType.String)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(625, TagType.String)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(423, TagType.Int)]
		public int? PriceType { get; set; }
		
		[TagDetails(6, TagType.Float)]
		public double? AvgPx { get; set; }
		
		[TagDetails(860, TagType.Float)]
		public double? AvgParPx { get; set; }
		
		[Component]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[TagDetails(15, TagType.String)]
		public string? Currency { get; set; }
		
		[TagDetails(74, TagType.Int)]
		public int? AvgPxPrecision { get; set; }
		
		[Component]
		public Parties? Parties { get; set; }
		
		[TagDetails(75, TagType.LocalDate)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(63, TagType.String)]
		public string? SettlType { get; set; }
		
		[TagDetails(64, TagType.LocalDate)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(775, TagType.Int)]
		public int? BookingType { get; set; }
		
		[TagDetails(381, TagType.Float)]
		public double? GrossTradeAmt { get; set; }
		
		[TagDetails(238, TagType.Float)]
		public double? Concession { get; set; }
		
		[TagDetails(237, TagType.Float)]
		public double? TotalTakedown { get; set; }
		
		[TagDetails(118, TagType.Float)]
		public double? NetMoney { get; set; }
		
		[TagDetails(77, TagType.String)]
		public string? PositionEffect { get; set; }
		
		[TagDetails(754, TagType.Boolean)]
		public bool? AutoAcceptIndicator { get; set; }
		
		[TagDetails(58, TagType.String)]
		public string? Text { get; set; }
		
		[TagDetails(354, TagType.Length)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(355, TagType.RawData)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(157, TagType.Int)]
		public int? NumDaysInterest { get; set; }
		
		[TagDetails(158, TagType.Float)]
		public double? AccruedInterestRate { get; set; }
		
		[TagDetails(159, TagType.Float)]
		public double? AccruedInterestAmt { get; set; }
		
		[TagDetails(540, TagType.Float)]
		public double? TotalAccruedInterestAmt { get; set; }
		
		[TagDetails(738, TagType.Float)]
		public double? InterestAtMaturity { get; set; }
		
		[TagDetails(920, TagType.Float)]
		public double? EndAccruedInterestAmt { get; set; }
		
		[TagDetails(921, TagType.Float)]
		public double? StartCash { get; set; }
		
		[TagDetails(922, TagType.Float)]
		public double? EndCash { get; set; }
		
		[TagDetails(650, TagType.Boolean)]
		public bool? LegalConfirm { get; set; }
		
		[Component]
		public Stipulations? Stipulations { get; set; }
		
		[Component]
		public YieldData? YieldData { get; set; }
		
		[TagDetails(892, TagType.Int)]
		public int? TotNoAllocs { get; set; }
		
		[TagDetails(893, TagType.Boolean)]
		public bool? LastFragment { get; set; }
		
		[Component]
		public AllocGrp? AllocGrp { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
