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
		[TagDetails(54)]
		public string? Side { get; set; } // CHAR
		
		[TagDetails(11)]
		public string? ClOrdID { get; set; } // STRING
		
		[TagDetails(526)]
		public string? SecondaryClOrdID { get; set; } // STRING
		
		[TagDetails(583)]
		public string? ClOrdLinkID { get; set; } // STRING
		
		public Parties? Parties { get; set; }
		[TagDetails(229)]
		public DateTime? TradeOriginationDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(75)]
		public DateTime? TradeDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(1)]
		public string? Account { get; set; } // STRING
		
		[TagDetails(660)]
		public int? AcctIDSource { get; set; } // INT
		
		[TagDetails(581)]
		public int? AccountType { get; set; } // INT
		
		[TagDetails(589)]
		public string? DayBookingInst { get; set; } // CHAR
		
		[TagDetails(590)]
		public string? BookingUnit { get; set; } // CHAR
		
		[TagDetails(591)]
		public string? PreallocMethod { get; set; } // CHAR
		
		[TagDetails(70)]
		public string? AllocID { get; set; } // STRING
		
		public PreAllocGrp? PreAllocGrp { get; set; }
		[TagDetails(854)]
		public int? QtyType { get; set; } // INT
		
		public OrderQtyData? OrderQtyData { get; set; }
		public CommissionData? CommissionData { get; set; }
		[TagDetails(528)]
		public string? OrderCapacity { get; set; } // CHAR
		
		[TagDetails(529)]
		public string? OrderRestrictions { get; set; } // MULTIPLEVALUESTRING
		
		[TagDetails(582)]
		public int? CustOrderCapacity { get; set; } // INT
		
		[TagDetails(121)]
		public bool? ForexReq { get; set; } // BOOLEAN
		
		[TagDetails(120)]
		public string? SettlCurrency { get; set; } // CURRENCY
		
		[TagDetails(775)]
		public int? BookingType { get; set; } // INT
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		[TagDetails(77)]
		public string? PositionEffect { get; set; } // CHAR
		
		[TagDetails(203)]
		public int? CoveredOrUncovered { get; set; } // INT
		
		[TagDetails(544)]
		public string? CashMargin { get; set; } // CHAR
		
		[TagDetails(635)]
		public string? ClearingFeeIndicator { get; set; } // STRING
		
		[TagDetails(377)]
		public bool? SolicitedFlag { get; set; } // BOOLEAN
		
		[TagDetails(659)]
		public string? SideComplianceID { get; set; } // STRING
		
	}
}
