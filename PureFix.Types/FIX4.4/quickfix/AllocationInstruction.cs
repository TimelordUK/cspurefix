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
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 1)]
		public string? AllocID { get; set; }
		
		[TagDetails(Tag = 71, Type = TagType.String, Offset = 2)]
		public string? AllocTransType { get; set; }
		
		[TagDetails(Tag = 626, Type = TagType.Int, Offset = 3)]
		public int? AllocType { get; set; }
		
		[TagDetails(Tag = 793, Type = TagType.String, Offset = 4)]
		public string? SecondaryAllocID { get; set; }
		
		[TagDetails(Tag = 72, Type = TagType.String, Offset = 5)]
		public string? RefAllocID { get; set; }
		
		[TagDetails(Tag = 796, Type = TagType.Int, Offset = 6)]
		public int? AllocCancReplaceReason { get; set; }
		
		[TagDetails(Tag = 808, Type = TagType.Int, Offset = 7)]
		public int? AllocIntermedReqType { get; set; }
		
		[TagDetails(Tag = 196, Type = TagType.String, Offset = 8)]
		public string? AllocLinkID { get; set; }
		
		[TagDetails(Tag = 197, Type = TagType.Int, Offset = 9)]
		public int? AllocLinkType { get; set; }
		
		[TagDetails(Tag = 466, Type = TagType.String, Offset = 10)]
		public string? BookingRefID { get; set; }
		
		[TagDetails(Tag = 857, Type = TagType.Int, Offset = 11)]
		public int? AllocNoOrdersType { get; set; }
		
		[Component(Offset = 12)]
		public OrdAllocGrp? OrdAllocGrp { get; set; }
		
		[Component(Offset = 13)]
		public ExecAllocGrp? ExecAllocGrp { get; set; }
		
		[TagDetails(Tag = 570, Type = TagType.Boolean, Offset = 14)]
		public bool? PreviouslyReported { get; set; }
		
		[TagDetails(Tag = 700, Type = TagType.Boolean, Offset = 15)]
		public bool? ReversalIndicator { get; set; }
		
		[TagDetails(Tag = 574, Type = TagType.String, Offset = 16)]
		public string? MatchType { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 17)]
		public string? Side { get; set; }
		
		[Component(Offset = 18)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 19)]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component(Offset = 20)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 21)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 22)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 53, Type = TagType.Float, Offset = 23)]
		public double? Quantity { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 24)]
		public int? QtyType { get; set; }
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 25)]
		public string? LastMkt { get; set; }
		
		[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 26)]
		public DateTime? TradeOriginationDate { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 27)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 28)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 29)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 30)]
		public double? AvgPx { get; set; }
		
		[TagDetails(Tag = 860, Type = TagType.Float, Offset = 31)]
		public double? AvgParPx { get; set; }
		
		[Component(Offset = 32)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 33)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 74, Type = TagType.Int, Offset = 34)]
		public int? AvgPxPrecision { get; set; }
		
		[Component(Offset = 35)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 36)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 37)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 38)]
		public string? SettlType { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 39)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(Tag = 775, Type = TagType.Int, Offset = 40)]
		public int? BookingType { get; set; }
		
		[TagDetails(Tag = 381, Type = TagType.Float, Offset = 41)]
		public double? GrossTradeAmt { get; set; }
		
		[TagDetails(Tag = 238, Type = TagType.Float, Offset = 42)]
		public double? Concession { get; set; }
		
		[TagDetails(Tag = 237, Type = TagType.Float, Offset = 43)]
		public double? TotalTakedown { get; set; }
		
		[TagDetails(Tag = 118, Type = TagType.Float, Offset = 44)]
		public double? NetMoney { get; set; }
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 45)]
		public string? PositionEffect { get; set; }
		
		[TagDetails(Tag = 754, Type = TagType.Boolean, Offset = 46)]
		public bool? AutoAcceptIndicator { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 47)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 48)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 49)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 157, Type = TagType.Int, Offset = 50)]
		public int? NumDaysInterest { get; set; }
		
		[TagDetails(Tag = 158, Type = TagType.Float, Offset = 51)]
		public double? AccruedInterestRate { get; set; }
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 52)]
		public double? AccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 540, Type = TagType.Float, Offset = 53)]
		public double? TotalAccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 738, Type = TagType.Float, Offset = 54)]
		public double? InterestAtMaturity { get; set; }
		
		[TagDetails(Tag = 920, Type = TagType.Float, Offset = 55)]
		public double? EndAccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 921, Type = TagType.Float, Offset = 56)]
		public double? StartCash { get; set; }
		
		[TagDetails(Tag = 922, Type = TagType.Float, Offset = 57)]
		public double? EndCash { get; set; }
		
		[TagDetails(Tag = 650, Type = TagType.Boolean, Offset = 58)]
		public bool? LegalConfirm { get; set; }
		
		[Component(Offset = 59)]
		public Stipulations? Stipulations { get; set; }
		
		[Component(Offset = 60)]
		public YieldData? YieldData { get; set; }
		
		[TagDetails(Tag = 892, Type = TagType.Int, Offset = 61)]
		public int? TotNoAllocs { get; set; }
		
		[TagDetails(Tag = 893, Type = TagType.Boolean, Offset = 62)]
		public bool? LastFragment { get; set; }
		
		[Component(Offset = 63)]
		public AllocGrp? AllocGrp { get; set; }
		
		[Component(Offset = 64)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
