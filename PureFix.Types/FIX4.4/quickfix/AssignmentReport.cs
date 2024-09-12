using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class AssignmentReport : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? AsgnRptID { get; set; } // 833 STRING
		public int? TotNumAssignmentReports { get; set; } // 832 INT
		public bool? LastRptRequested { get; set; } // 912 BOOLEAN
		public Parties? Parties { get; set; }
		public string? Account { get; set; } // 1 STRING
		public int? AccountType { get; set; } // 581 INT
		public Instrument? Instrument { get; set; }
		public string? Currency { get; set; } // 15 CURRENCY
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public PositionQty? PositionQty { get; set; }
		public PositionAmountData? PositionAmountData { get; set; }
		public double? ThresholdAmount { get; set; } // 834 PRICEOFFSET
		public double? SettlPrice { get; set; } // 730 PRICE
		public int? SettlPriceType { get; set; } // 731 INT
		public double? UnderlyingSettlPrice { get; set; } // 732 PRICE
		public DateTime? ExpireDate { get; set; } // 432 LOCALMKTDATE
		public string? AssignmentMethod { get; set; } // 744 CHAR
		public double? AssignmentUnit { get; set; } // 745 QTY
		public double? OpenInterest { get; set; } // 746 AMT
		public string? ExerciseMethod { get; set; } // 747 CHAR
		public string? SettlSessID { get; set; } // 716 STRING
		public string? SettlSessSubID { get; set; } // 717 STRING
		public DateTime? ClearingBusinessDate { get; set; } // 715 LOCALMKTDATE
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
