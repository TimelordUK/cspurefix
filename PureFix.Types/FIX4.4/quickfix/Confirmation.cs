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
		public string? ConfirmID { get; set; } // 664 STRING
		public string? ConfirmRefID { get; set; } // 772 STRING
		public string? ConfirmReqID { get; set; } // 859 STRING
		public int? ConfirmTransType { get; set; } // 666 INT
		public int? ConfirmType { get; set; } // 773 INT
		public bool? CopyMsgIndicator { get; set; } // 797 BOOLEAN
		public bool? LegalConfirm { get; set; } // 650 BOOLEAN
		public int? ConfirmStatus { get; set; } // 665 INT
		public Parties? Parties { get; set; }
		public OrdAllocGrp? OrdAllocGrp { get; set; }
		public string? AllocID { get; set; } // 70 STRING
		public string? SecondaryAllocID { get; set; } // 793 STRING
		public string? IndividualAllocID { get; set; } // 467 STRING
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public DateTime? TradeDate { get; set; } // 75 LOCALMKTDATE
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		public Instrument? Instrument { get; set; }
		public InstrumentExtension? InstrumentExtension { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public YieldData? YieldData { get; set; }
		public double? AllocQty { get; set; } // 80 QTY
		public int? QtyType { get; set; } // 854 INT
		public string? Side { get; set; } // 54 CHAR
		public string? Currency { get; set; } // 15 CURRENCY
		public string? LastMkt { get; set; } // 30 EXCHANGE
		public CpctyConfGrp? CpctyConfGrp { get; set; }
		public string? AllocAccount { get; set; } // 79 STRING
		public int? AllocAcctIDSource { get; set; } // 661 INT
		public int? AllocAccountType { get; set; } // 798 INT
		public double? AvgPx { get; set; } // 6 PRICE
		public int? AvgPxPrecision { get; set; } // 74 INT
		public int? PriceType { get; set; } // 423 INT
		public double? AvgParPx { get; set; } // 860 PRICE
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		public double? ReportedPx { get; set; } // 861 PRICE
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public string? ProcessCode { get; set; } // 81 CHAR
		public double? GrossTradeAmt { get; set; } // 381 AMT
		public int? NumDaysInterest { get; set; } // 157 INT
		public DateTime? ExDate { get; set; } // 230 LOCALMKTDATE
		public double? AccruedInterestRate { get; set; } // 158 PERCENTAGE
		public double? AccruedInterestAmt { get; set; } // 159 AMT
		public double? InterestAtMaturity { get; set; } // 738 AMT
		public double? EndAccruedInterestAmt { get; set; } // 920 AMT
		public double? StartCash { get; set; } // 921 AMT
		public double? EndCash { get; set; } // 922 AMT
		public double? Concession { get; set; } // 238 AMT
		public double? TotalTakedown { get; set; } // 237 AMT
		public double? NetMoney { get; set; } // 118 AMT
		public double? MaturityNetMoney { get; set; } // 890 AMT
		public double? SettlCurrAmt { get; set; } // 119 AMT
		public string? SettlCurrency { get; set; } // 120 CURRENCY
		public double? SettlCurrFxRate { get; set; } // 155 FLOAT
		public string? SettlCurrFxRateCalc { get; set; } // 156 CHAR
		public string? SettlType { get; set; } // 63 CHAR
		public DateTime? SettlDate { get; set; } // 64 LOCALMKTDATE
		public SettlInstructionsData? SettlInstructionsData { get; set; }
		public CommissionData? CommissionData { get; set; }
		public double? SharedCommission { get; set; } // 858 AMT
		public Stipulations? Stipulations { get; set; }
		public MiscFeesGrp? MiscFeesGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
