using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoSides
	{
		public string? Side { get; set; } // 54 CHAR
		public string? ClOrdID { get; set; } // 11 STRING
		public string? SecondaryClOrdID { get; set; } // 526 STRING
		public string? ClOrdLinkID { get; set; } // 583 STRING
		public Parties? Parties { get; set; }
		public DateTime? TradeOriginationDate { get; set; } // 229 LOCALMKTDATE
		public DateTime? TradeDate { get; set; } // 75 LOCALMKTDATE
		public string? Account { get; set; } // 1 STRING
		public int? AcctIDSource { get; set; } // 660 INT
		public int? AccountType { get; set; } // 581 INT
		public string? DayBookingInst { get; set; } // 589 CHAR
		public string? BookingUnit { get; set; } // 590 CHAR
		public string? PreallocMethod { get; set; } // 591 CHAR
		public string? AllocID { get; set; } // 70 STRING
		public PreAllocGrp? PreAllocGrp { get; set; }
		public int? QtyType { get; set; } // 854 INT
		public OrderQtyData? OrderQtyData { get; set; }
		public CommissionData? CommissionData { get; set; }
		public string? OrderCapacity { get; set; } // 528 CHAR
		public string? OrderRestrictions { get; set; } // 529 MULTIPLEVALUESTRING
		public int? CustOrderCapacity { get; set; } // 582 INT
		public bool? ForexReq { get; set; } // 121 BOOLEAN
		public string? SettlCurrency { get; set; } // 120 CURRENCY
		public int? BookingType { get; set; } // 775 INT
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public string? PositionEffect { get; set; } // 77 CHAR
		public int? CoveredOrUncovered { get; set; } // 203 INT
		public string? CashMargin { get; set; } // 544 CHAR
		public string? ClearingFeeIndicator { get; set; } // 635 STRING
		public bool? SolicitedFlag { get; set; } // 377 BOOLEAN
		public string? SideComplianceID { get; set; } // 659 STRING
	}
}
