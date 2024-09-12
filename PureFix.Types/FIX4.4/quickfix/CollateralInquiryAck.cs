using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class CollateralInquiryAck : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(909)]
		public string? CollInquiryID { get; set; } // STRING
		
		[TagDetails(945)]
		public int? CollInquiryStatus { get; set; } // INT
		
		[TagDetails(946)]
		public int? CollInquiryResult { get; set; } // INT
		
		public CollInqQualGrp? CollInqQualGrp { get; set; }
		[TagDetails(911)]
		public int? TotNumReports { get; set; } // INT
		
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
		
		[TagDetails(725)]
		public int? ResponseTransportType { get; set; } // INT
		
		[TagDetails(726)]
		public string? ResponseDestination { get; set; } // STRING
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
