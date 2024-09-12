using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class CollateralInquiryAck : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? CollInquiryID { get; set; } // 909 STRING
		public int? CollInquiryStatus { get; set; } // 945 INT
		public int? CollInquiryResult { get; set; } // 946 INT
		public CollInqQualGrp? CollInqQualGrp { get; set; }
		public int? TotNumReports { get; set; } // 911 INT
		public Parties? Parties { get; set; }
		public string? Account { get; set; } // 1 STRING
		public int? AccountType { get; set; } // 581 INT
		public string? ClOrdID { get; set; } // 11 STRING
		public string? OrderID { get; set; } // 37 STRING
		public string? SecondaryOrderID { get; set; } // 198 STRING
		public string? SecondaryClOrdID { get; set; } // 526 STRING
		public ExecCollGrp? ExecCollGrp { get; set; }
		public TrdCollGrp? TrdCollGrp { get; set; }
		public Instrument? Instrument { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public DateTime? SettlDate { get; set; } // 64 LOCALMKTDATE
		public double? Quantity { get; set; } // 53 QTY
		public int? QtyType { get; set; } // 854 INT
		public string? Currency { get; set; } // 15 CURRENCY
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public string? SettlSessID { get; set; } // 716 STRING
		public string? SettlSessSubID { get; set; } // 717 STRING
		public DateTime? ClearingBusinessDate { get; set; } // 715 LOCALMKTDATE
		public int? ResponseTransportType { get; set; } // 725 INT
		public string? ResponseDestination { get; set; } // 726 STRING
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
