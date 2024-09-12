using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class PositionReport : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(721)]
		public string? PosMaintRptID { get; set; } // STRING
		
		[TagDetails(710)]
		public string? PosReqID { get; set; } // STRING
		
		[TagDetails(724)]
		public int? PosReqType { get; set; } // INT
		
		[TagDetails(263)]
		public string? SubscriptionRequestType { get; set; } // CHAR
		
		[TagDetails(727)]
		public int? TotalNumPosReports { get; set; } // INT
		
		[TagDetails(325)]
		public bool? UnsolicitedIndicator { get; set; } // BOOLEAN
		
		[TagDetails(728)]
		public int? PosReqResult { get; set; } // INT
		
		[TagDetails(715)]
		public DateTime? ClearingBusinessDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(716)]
		public string? SettlSessID { get; set; } // STRING
		
		[TagDetails(717)]
		public string? SettlSessSubID { get; set; } // STRING
		
		public Parties? Parties { get; set; }
		[TagDetails(1)]
		public string? Account { get; set; } // STRING
		
		[TagDetails(660)]
		public int? AcctIDSource { get; set; } // INT
		
		[TagDetails(581)]
		public int? AccountType { get; set; } // INT
		
		public Instrument? Instrument { get; set; }
		[TagDetails(15)]
		public string? Currency { get; set; } // CURRENCY
		
		[TagDetails(730)]
		public double? SettlPrice { get; set; } // PRICE
		
		[TagDetails(731)]
		public int? SettlPriceType { get; set; } // INT
		
		[TagDetails(734)]
		public double? PriorSettlPrice { get; set; } // PRICE
		
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public PosUndInstrmtGrp? PosUndInstrmtGrp { get; set; }
		public PositionQty? PositionQty { get; set; }
		public PositionAmountData? PositionAmountData { get; set; }
		[TagDetails(506)]
		public string? RegistStatus { get; set; } // CHAR
		
		[TagDetails(743)]
		public DateTime? DeliveryDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
