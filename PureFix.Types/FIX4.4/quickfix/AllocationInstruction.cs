using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("J", FixVersion.FIX44)]
	public sealed class AllocationInstruction : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 1, Required = true)]
		public string? AllocID { get; set; }
		
		[TagDetails(Tag = 71, Type = TagType.String, Offset = 2, Required = true)]
		public string? AllocTransType { get; set; }
		
		[TagDetails(Tag = 626, Type = TagType.Int, Offset = 3, Required = true)]
		public int? AllocType { get; set; }
		
		[TagDetails(Tag = 793, Type = TagType.String, Offset = 4, Required = false)]
		public string? SecondaryAllocID { get; set; }
		
		[TagDetails(Tag = 72, Type = TagType.String, Offset = 5, Required = false)]
		public string? RefAllocID { get; set; }
		
		[TagDetails(Tag = 796, Type = TagType.Int, Offset = 6, Required = false)]
		public int? AllocCancReplaceReason { get; set; }
		
		[TagDetails(Tag = 808, Type = TagType.Int, Offset = 7, Required = false)]
		public int? AllocIntermedReqType { get; set; }
		
		[TagDetails(Tag = 196, Type = TagType.String, Offset = 8, Required = false)]
		public string? AllocLinkID { get; set; }
		
		[TagDetails(Tag = 197, Type = TagType.Int, Offset = 9, Required = false)]
		public int? AllocLinkType { get; set; }
		
		[TagDetails(Tag = 466, Type = TagType.String, Offset = 10, Required = false)]
		public string? BookingRefID { get; set; }
		
		[TagDetails(Tag = 857, Type = TagType.Int, Offset = 11, Required = true)]
		public int? AllocNoOrdersType { get; set; }
		
		[Component(Offset = 12, Required = false)]
		public OrdAllocGrp? OrdAllocGrp { get; set; }
		
		[Component(Offset = 13, Required = false)]
		public ExecAllocGrp? ExecAllocGrp { get; set; }
		
		[TagDetails(Tag = 570, Type = TagType.Boolean, Offset = 14, Required = false)]
		public bool? PreviouslyReported { get; set; }
		
		[TagDetails(Tag = 700, Type = TagType.Boolean, Offset = 15, Required = false)]
		public bool? ReversalIndicator { get; set; }
		
		[TagDetails(Tag = 574, Type = TagType.String, Offset = 16, Required = false)]
		public string? MatchType { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 17, Required = true)]
		public string? Side { get; set; }
		
		[Component(Offset = 18, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 19, Required = false)]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component(Offset = 20, Required = false)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 21, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 22, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 53, Type = TagType.Float, Offset = 23, Required = true)]
		public double? Quantity { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 24, Required = false)]
		public int? QtyType { get; set; }
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 25, Required = false)]
		public string? LastMkt { get; set; }
		
		[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 26, Required = false)]
		public DateTime? TradeOriginationDate { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 27, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 28, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 29, Required = false)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 30, Required = true)]
		public double? AvgPx { get; set; }
		
		[TagDetails(Tag = 860, Type = TagType.Float, Offset = 31, Required = false)]
		public double? AvgParPx { get; set; }
		
		[Component(Offset = 32, Required = false)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 33, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 74, Type = TagType.Int, Offset = 34, Required = false)]
		public int? AvgPxPrecision { get; set; }
		
		[Component(Offset = 35, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 36, Required = true)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 37, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 38, Required = false)]
		public string? SettlType { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 39, Required = false)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(Tag = 775, Type = TagType.Int, Offset = 40, Required = false)]
		public int? BookingType { get; set; }
		
		[TagDetails(Tag = 381, Type = TagType.Float, Offset = 41, Required = false)]
		public double? GrossTradeAmt { get; set; }
		
		[TagDetails(Tag = 238, Type = TagType.Float, Offset = 42, Required = false)]
		public double? Concession { get; set; }
		
		[TagDetails(Tag = 237, Type = TagType.Float, Offset = 43, Required = false)]
		public double? TotalTakedown { get; set; }
		
		[TagDetails(Tag = 118, Type = TagType.Float, Offset = 44, Required = false)]
		public double? NetMoney { get; set; }
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 45, Required = false)]
		public string? PositionEffect { get; set; }
		
		[TagDetails(Tag = 754, Type = TagType.Boolean, Offset = 46, Required = false)]
		public bool? AutoAcceptIndicator { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 47, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 48, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 49, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 157, Type = TagType.Int, Offset = 50, Required = false)]
		public int? NumDaysInterest { get; set; }
		
		[TagDetails(Tag = 158, Type = TagType.Float, Offset = 51, Required = false)]
		public double? AccruedInterestRate { get; set; }
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 52, Required = false)]
		public double? AccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 540, Type = TagType.Float, Offset = 53, Required = false)]
		public double? TotalAccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 738, Type = TagType.Float, Offset = 54, Required = false)]
		public double? InterestAtMaturity { get; set; }
		
		[TagDetails(Tag = 920, Type = TagType.Float, Offset = 55, Required = false)]
		public double? EndAccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 921, Type = TagType.Float, Offset = 56, Required = false)]
		public double? StartCash { get; set; }
		
		[TagDetails(Tag = 922, Type = TagType.Float, Offset = 57, Required = false)]
		public double? EndCash { get; set; }
		
		[TagDetails(Tag = 650, Type = TagType.Boolean, Offset = 58, Required = false)]
		public bool? LegalConfirm { get; set; }
		
		[Component(Offset = 59, Required = false)]
		public Stipulations? Stipulations { get; set; }
		
		[Component(Offset = 60, Required = false)]
		public YieldData? YieldData { get; set; }
		
		[TagDetails(Tag = 892, Type = TagType.Int, Offset = 61, Required = false)]
		public int? TotNoAllocs { get; set; }
		
		[TagDetails(Tag = 893, Type = TagType.Boolean, Offset = 62, Required = false)]
		public bool? LastFragment { get; set; }
		
		[Component(Offset = 63, Required = false)]
		public AllocGrp? AllocGrp { get; set; }
		
		[Component(Offset = 64, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
