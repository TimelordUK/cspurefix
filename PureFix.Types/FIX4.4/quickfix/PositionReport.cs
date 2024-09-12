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
		public string? PosMaintRptID { get; set; } // 721 STRING
		public string? PosReqID { get; set; } // 710 STRING
		public int? PosReqType { get; set; } // 724 INT
		public string? SubscriptionRequestType { get; set; } // 263 CHAR
		public int? TotalNumPosReports { get; set; } // 727 INT
		public bool? UnsolicitedIndicator { get; set; } // 325 BOOLEAN
		public int? PosReqResult { get; set; } // 728 INT
		public DateTime? ClearingBusinessDate { get; set; } // 715 LOCALMKTDATE
		public string? SettlSessID { get; set; } // 716 STRING
		public string? SettlSessSubID { get; set; } // 717 STRING
		public Parties? Parties { get; set; }
		public string? Account { get; set; } // 1 STRING
		public int? AcctIDSource { get; set; } // 660 INT
		public int? AccountType { get; set; } // 581 INT
		public Instrument? Instrument { get; set; }
		public string? Currency { get; set; } // 15 CURRENCY
		public double? SettlPrice { get; set; } // 730 PRICE
		public int? SettlPriceType { get; set; } // 731 INT
		public double? PriorSettlPrice { get; set; } // 734 PRICE
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public PosUndInstrmtGrp? PosUndInstrmtGrp { get; set; }
		public PositionQty? PositionQty { get; set; }
		public PositionAmountData? PositionAmountData { get; set; }
		public string? RegistStatus { get; set; } // 506 CHAR
		public DateTime? DeliveryDate { get; set; } // 743 LOCALMKTDATE
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
