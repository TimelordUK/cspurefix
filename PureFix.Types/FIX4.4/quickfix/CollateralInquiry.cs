using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class CollateralInquiry : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(909)]
		public string? CollInquiryID { get; set; } // STRING
		
		public CollInqQualGrp? CollInqQualGrp { get; set; }
		[TagDetails(263)]
		public string? SubscriptionRequestType { get; set; } // CHAR
		
		[TagDetails(725)]
		public int? ResponseTransportType { get; set; } // INT
		
		[TagDetails(726)]
		public string? ResponseDestination { get; set; } // STRING
		
		public Parties? Parties { get; set; }
		[TagDetails(1)]
		public string? Account { get; set; } // STRING
		
		[TagDetails(581)]
		public int? AccountType { get; set; } // INT
		
		[TagDetails(11)]
		public string? ClOrdID { get; set; } // STRING
		
		[TagDetails(37)]
		public string? OrderID { get; set; } // STRING
		
		[TagDetails(198)]
		public string? SecondaryOrderID { get; set; } // STRING
		
		[TagDetails(526)]
		public string? SecondaryClOrdID { get; set; } // STRING
		
		public ExecCollGrp? ExecCollGrp { get; set; }
		public TrdCollGrp? TrdCollGrp { get; set; }
		public Instrument? Instrument { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		[TagDetails(64)]
		public DateTime? SettlDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(53)]
		public double? Quantity { get; set; } // QTY
		
		[TagDetails(854)]
		public int? QtyType { get; set; } // INT
		
		[TagDetails(15)]
		public string? Currency { get; set; } // CURRENCY
		
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		[TagDetails(899)]
		public double? MarginExcess { get; set; } // AMT
		
		[TagDetails(900)]
		public double? TotalNetValue { get; set; } // AMT
		
		[TagDetails(901)]
		public double? CashOutstanding { get; set; } // AMT
		
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		[TagDetails(54)]
		public string? Side { get; set; } // CHAR
		
		[TagDetails(44)]
		public double? Price { get; set; } // PRICE
		
		[TagDetails(423)]
		public int? PriceType { get; set; } // INT
		
		[TagDetails(159)]
		public double? AccruedInterestAmt { get; set; } // AMT
		
		[TagDetails(920)]
		public double? EndAccruedInterestAmt { get; set; } // AMT
		
		[TagDetails(921)]
		public double? StartCash { get; set; } // AMT
		
		[TagDetails(922)]
		public double? EndCash { get; set; } // AMT
		
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		public Stipulations? Stipulations { get; set; }
		public SettlInstructionsData? SettlInstructionsData { get; set; }
		[TagDetails(336)]
		public string? TradingSessionID { get; set; } // STRING
		
		[TagDetails(625)]
		public string? TradingSessionSubID { get; set; } // STRING
		
		[TagDetails(716)]
		public string? SettlSessID { get; set; } // STRING
		
		[TagDetails(717)]
		public string? SettlSessSubID { get; set; } // STRING
		
		[TagDetails(715)]
		public DateTime? ClearingBusinessDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
