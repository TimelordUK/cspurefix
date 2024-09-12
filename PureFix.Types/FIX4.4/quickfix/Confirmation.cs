using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class Confirmation : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(664)]
		public string? ConfirmID { get; set; } // STRING
		
		[TagDetails(772)]
		public string? ConfirmRefID { get; set; } // STRING
		
		[TagDetails(859)]
		public string? ConfirmReqID { get; set; } // STRING
		
		[TagDetails(666)]
		public int? ConfirmTransType { get; set; } // INT
		
		[TagDetails(773)]
		public int? ConfirmType { get; set; } // INT
		
		[TagDetails(797)]
		public bool? CopyMsgIndicator { get; set; } // BOOLEAN
		
		[TagDetails(650)]
		public bool? LegalConfirm { get; set; } // BOOLEAN
		
		[TagDetails(665)]
		public int? ConfirmStatus { get; set; } // INT
		
		public Parties? Parties { get; set; }
		public OrdAllocGrp? OrdAllocGrp { get; set; }
		[TagDetails(70)]
		public string? AllocID { get; set; } // STRING
		
		[TagDetails(793)]
		public string? SecondaryAllocID { get; set; } // STRING
		
		[TagDetails(467)]
		public string? IndividualAllocID { get; set; } // STRING
		
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(75)]
		public DateTime? TradeDate { get; set; } // LOCALMKTDATE
		
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		public Instrument? Instrument { get; set; }
		public InstrumentExtension? InstrumentExtension { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public YieldData? YieldData { get; set; }
		[TagDetails(80)]
		public double? AllocQty { get; set; } // QTY
		
		[TagDetails(854)]
		public int? QtyType { get; set; } // INT
		
		[TagDetails(54)]
		public string? Side { get; set; } // CHAR
		
		[TagDetails(15)]
		public string? Currency { get; set; } // CURRENCY
		
		[TagDetails(30)]
		public string? LastMkt { get; set; } // EXCHANGE
		
		public CpctyConfGrp? CpctyConfGrp { get; set; }
		[TagDetails(79)]
		public string? AllocAccount { get; set; } // STRING
		
		[TagDetails(661)]
		public int? AllocAcctIDSource { get; set; } // INT
		
		[TagDetails(798)]
		public int? AllocAccountType { get; set; } // INT
		
		[TagDetails(6)]
		public double? AvgPx { get; set; } // PRICE
		
		[TagDetails(74)]
		public int? AvgPxPrecision { get; set; } // INT
		
		[TagDetails(423)]
		public int? PriceType { get; set; } // INT
		
		[TagDetails(860)]
		public double? AvgParPx { get; set; } // PRICE
		
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		[TagDetails(861)]
		public double? ReportedPx { get; set; } // PRICE
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		[TagDetails(81)]
		public string? ProcessCode { get; set; } // CHAR
		
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
		
		[TagDetails(238)]
		public double? Concession { get; set; } // AMT
		
		[TagDetails(237)]
		public double? TotalTakedown { get; set; } // AMT
		
		[TagDetails(118)]
		public double? NetMoney { get; set; } // AMT
		
		[TagDetails(890)]
		public double? MaturityNetMoney { get; set; } // AMT
		
		[TagDetails(119)]
		public double? SettlCurrAmt { get; set; } // AMT
		
		[TagDetails(120)]
		public string? SettlCurrency { get; set; } // CURRENCY
		
		[TagDetails(155)]
		public double? SettlCurrFxRate { get; set; } // FLOAT
		
		[TagDetails(156)]
		public string? SettlCurrFxRateCalc { get; set; } // CHAR
		
		[TagDetails(63)]
		public string? SettlType { get; set; } // CHAR
		
		[TagDetails(64)]
		public DateTime? SettlDate { get; set; } // LOCALMKTDATE
		
		public SettlInstructionsData? SettlInstructionsData { get; set; }
		public CommissionData? CommissionData { get; set; }
		[TagDetails(858)]
		public double? SharedCommission { get; set; } // AMT
		
		public Stipulations? Stipulations { get; set; }
		public MiscFeesGrp? MiscFeesGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
