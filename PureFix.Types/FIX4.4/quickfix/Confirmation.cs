using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AK")]
	public sealed class Confirmation : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(664, TagType.String)]
		public string? ConfirmID { get; set; }
		
		[TagDetails(772, TagType.String)]
		public string? ConfirmRefID { get; set; }
		
		[TagDetails(859, TagType.String)]
		public string? ConfirmReqID { get; set; }
		
		[TagDetails(666, TagType.Int)]
		public int? ConfirmTransType { get; set; }
		
		[TagDetails(773, TagType.Int)]
		public int? ConfirmType { get; set; }
		
		[TagDetails(797, TagType.Boolean)]
		public bool? CopyMsgIndicator { get; set; }
		
		[TagDetails(650, TagType.Boolean)]
		public bool? LegalConfirm { get; set; }
		
		[TagDetails(665, TagType.Int)]
		public int? ConfirmStatus { get; set; }
		
		[Component]
		public Parties? Parties { get; set; }
		
		[Component]
		public OrdAllocGrp? OrdAllocGrp { get; set; }
		
		[TagDetails(70, TagType.String)]
		public string? AllocID { get; set; }
		
		[TagDetails(793, TagType.String)]
		public string? SecondaryAllocID { get; set; }
		
		[TagDetails(467, TagType.String)]
		public string? IndividualAllocID { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(75, TagType.LocalDate)]
		public DateTime? TradeDate { get; set; }
		
		[Component]
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		
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
		
		[Component]
		public YieldData? YieldData { get; set; }
		
		[TagDetails(80, TagType.Float)]
		public double? AllocQty { get; set; }
		
		[TagDetails(854, TagType.Int)]
		public int? QtyType { get; set; }
		
		[TagDetails(54, TagType.String)]
		public string? Side { get; set; }
		
		[TagDetails(15, TagType.String)]
		public string? Currency { get; set; }
		
		[TagDetails(30, TagType.String)]
		public string? LastMkt { get; set; }
		
		[Component]
		public CpctyConfGrp? CpctyConfGrp { get; set; }
		
		[TagDetails(79, TagType.String)]
		public string? AllocAccount { get; set; }
		
		[TagDetails(661, TagType.Int)]
		public int? AllocAcctIDSource { get; set; }
		
		[TagDetails(798, TagType.Int)]
		public int? AllocAccountType { get; set; }
		
		[TagDetails(6, TagType.Float)]
		public double? AvgPx { get; set; }
		
		[TagDetails(74, TagType.Int)]
		public int? AvgPxPrecision { get; set; }
		
		[TagDetails(423, TagType.Int)]
		public int? PriceType { get; set; }
		
		[TagDetails(860, TagType.Float)]
		public double? AvgParPx { get; set; }
		
		[Component]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[TagDetails(861, TagType.Float)]
		public double? ReportedPx { get; set; }
		
		[TagDetails(58, TagType.String)]
		public string? Text { get; set; }
		
		[TagDetails(354, TagType.Length)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(355, TagType.RawData)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(81, TagType.String)]
		public string? ProcessCode { get; set; }
		
		[TagDetails(381, TagType.Float)]
		public double? GrossTradeAmt { get; set; }
		
		[TagDetails(157, TagType.Int)]
		public int? NumDaysInterest { get; set; }
		
		[TagDetails(230, TagType.LocalDate)]
		public DateTime? ExDate { get; set; }
		
		[TagDetails(158, TagType.Float)]
		public double? AccruedInterestRate { get; set; }
		
		[TagDetails(159, TagType.Float)]
		public double? AccruedInterestAmt { get; set; }
		
		[TagDetails(738, TagType.Float)]
		public double? InterestAtMaturity { get; set; }
		
		[TagDetails(920, TagType.Float)]
		public double? EndAccruedInterestAmt { get; set; }
		
		[TagDetails(921, TagType.Float)]
		public double? StartCash { get; set; }
		
		[TagDetails(922, TagType.Float)]
		public double? EndCash { get; set; }
		
		[TagDetails(238, TagType.Float)]
		public double? Concession { get; set; }
		
		[TagDetails(237, TagType.Float)]
		public double? TotalTakedown { get; set; }
		
		[TagDetails(118, TagType.Float)]
		public double? NetMoney { get; set; }
		
		[TagDetails(890, TagType.Float)]
		public double? MaturityNetMoney { get; set; }
		
		[TagDetails(119, TagType.Float)]
		public double? SettlCurrAmt { get; set; }
		
		[TagDetails(120, TagType.String)]
		public string? SettlCurrency { get; set; }
		
		[TagDetails(155, TagType.Float)]
		public double? SettlCurrFxRate { get; set; }
		
		[TagDetails(156, TagType.String)]
		public string? SettlCurrFxRateCalc { get; set; }
		
		[TagDetails(63, TagType.String)]
		public string? SettlType { get; set; }
		
		[TagDetails(64, TagType.LocalDate)]
		public DateTime? SettlDate { get; set; }
		
		[Component]
		public SettlInstructionsData? SettlInstructionsData { get; set; }
		
		[Component]
		public CommissionData? CommissionData { get; set; }
		
		[TagDetails(858, TagType.Float)]
		public double? SharedCommission { get; set; }
		
		[Component]
		public Stipulations? Stipulations { get; set; }
		
		[Component]
		public MiscFeesGrp? MiscFeesGrp { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
