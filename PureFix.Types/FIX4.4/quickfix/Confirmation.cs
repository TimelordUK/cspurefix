using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AK", FixVersion.FIX44)]
	public sealed class Confirmation : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 664, Type = TagType.String, Offset = 1)]
		public string? ConfirmID { get; set; }
		
		[TagDetails(Tag = 772, Type = TagType.String, Offset = 2)]
		public string? ConfirmRefID { get; set; }
		
		[TagDetails(Tag = 859, Type = TagType.String, Offset = 3)]
		public string? ConfirmReqID { get; set; }
		
		[TagDetails(Tag = 666, Type = TagType.Int, Offset = 4)]
		public int? ConfirmTransType { get; set; }
		
		[TagDetails(Tag = 773, Type = TagType.Int, Offset = 5)]
		public int? ConfirmType { get; set; }
		
		[TagDetails(Tag = 797, Type = TagType.Boolean, Offset = 6)]
		public bool? CopyMsgIndicator { get; set; }
		
		[TagDetails(Tag = 650, Type = TagType.Boolean, Offset = 7)]
		public bool? LegalConfirm { get; set; }
		
		[TagDetails(Tag = 665, Type = TagType.Int, Offset = 8)]
		public int? ConfirmStatus { get; set; }
		
		[Component(Offset = 9)]
		public Parties? Parties { get; set; }
		
		[Component(Offset = 10)]
		public OrdAllocGrp? OrdAllocGrp { get; set; }
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 11)]
		public string? AllocID { get; set; }
		
		[TagDetails(Tag = 793, Type = TagType.String, Offset = 12)]
		public string? SecondaryAllocID { get; set; }
		
		[TagDetails(Tag = 467, Type = TagType.String, Offset = 13)]
		public string? IndividualAllocID { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 14)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 15)]
		public DateTime? TradeDate { get; set; }
		
		[Component(Offset = 16)]
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		
		[Component(Offset = 17)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 18)]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component(Offset = 19)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 20)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 21)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 22)]
		public YieldData? YieldData { get; set; }
		
		[TagDetails(Tag = 80, Type = TagType.Float, Offset = 23)]
		public double? AllocQty { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 24)]
		public int? QtyType { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 25)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 26)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 27)]
		public string? LastMkt { get; set; }
		
		[Component(Offset = 28)]
		public CpctyConfGrp? CpctyConfGrp { get; set; }
		
		[TagDetails(Tag = 79, Type = TagType.String, Offset = 29)]
		public string? AllocAccount { get; set; }
		
		[TagDetails(Tag = 661, Type = TagType.Int, Offset = 30)]
		public int? AllocAcctIDSource { get; set; }
		
		[TagDetails(Tag = 798, Type = TagType.Int, Offset = 31)]
		public int? AllocAccountType { get; set; }
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 32)]
		public double? AvgPx { get; set; }
		
		[TagDetails(Tag = 74, Type = TagType.Int, Offset = 33)]
		public int? AvgPxPrecision { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 34)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 860, Type = TagType.Float, Offset = 35)]
		public double? AvgParPx { get; set; }
		
		[Component(Offset = 36)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[TagDetails(Tag = 861, Type = TagType.Float, Offset = 37)]
		public double? ReportedPx { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 38)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 39)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 40)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 81, Type = TagType.String, Offset = 41)]
		public string? ProcessCode { get; set; }
		
		[TagDetails(Tag = 381, Type = TagType.Float, Offset = 42)]
		public double? GrossTradeAmt { get; set; }
		
		[TagDetails(Tag = 157, Type = TagType.Int, Offset = 43)]
		public int? NumDaysInterest { get; set; }
		
		[TagDetails(Tag = 230, Type = TagType.LocalDate, Offset = 44)]
		public DateTime? ExDate { get; set; }
		
		[TagDetails(Tag = 158, Type = TagType.Float, Offset = 45)]
		public double? AccruedInterestRate { get; set; }
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 46)]
		public double? AccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 738, Type = TagType.Float, Offset = 47)]
		public double? InterestAtMaturity { get; set; }
		
		[TagDetails(Tag = 920, Type = TagType.Float, Offset = 48)]
		public double? EndAccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 921, Type = TagType.Float, Offset = 49)]
		public double? StartCash { get; set; }
		
		[TagDetails(Tag = 922, Type = TagType.Float, Offset = 50)]
		public double? EndCash { get; set; }
		
		[TagDetails(Tag = 238, Type = TagType.Float, Offset = 51)]
		public double? Concession { get; set; }
		
		[TagDetails(Tag = 237, Type = TagType.Float, Offset = 52)]
		public double? TotalTakedown { get; set; }
		
		[TagDetails(Tag = 118, Type = TagType.Float, Offset = 53)]
		public double? NetMoney { get; set; }
		
		[TagDetails(Tag = 890, Type = TagType.Float, Offset = 54)]
		public double? MaturityNetMoney { get; set; }
		
		[TagDetails(Tag = 119, Type = TagType.Float, Offset = 55)]
		public double? SettlCurrAmt { get; set; }
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 56)]
		public string? SettlCurrency { get; set; }
		
		[TagDetails(Tag = 155, Type = TagType.Float, Offset = 57)]
		public double? SettlCurrFxRate { get; set; }
		
		[TagDetails(Tag = 156, Type = TagType.String, Offset = 58)]
		public string? SettlCurrFxRateCalc { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 59)]
		public string? SettlType { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 60)]
		public DateTime? SettlDate { get; set; }
		
		[Component(Offset = 61)]
		public SettlInstructionsData? SettlInstructionsData { get; set; }
		
		[Component(Offset = 62)]
		public CommissionData? CommissionData { get; set; }
		
		[TagDetails(Tag = 858, Type = TagType.Float, Offset = 63)]
		public double? SharedCommission { get; set; }
		
		[Component(Offset = 64)]
		public Stipulations? Stipulations { get; set; }
		
		[Component(Offset = 65)]
		public MiscFeesGrp? MiscFeesGrp { get; set; }
		
		[Component(Offset = 66)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
