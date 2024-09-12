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
		[TagDetails(54, TagType.String)]
		public string? Side { get; set; }
		
		[TagDetails(11, TagType.String)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(526, TagType.String)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(583, TagType.String)]
		public string? ClOrdLinkID { get; set; }
		
		[Component]
		public Parties? Parties { get; set; }
		
		[TagDetails(229, TagType.LocalDate)]
		public DateTime? TradeOriginationDate { get; set; }
		
		[TagDetails(75, TagType.LocalDate)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(1, TagType.String)]
		public string? Account { get; set; }
		
		[TagDetails(660, TagType.Int)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(581, TagType.Int)]
		public int? AccountType { get; set; }
		
		[TagDetails(589, TagType.String)]
		public string? DayBookingInst { get; set; }
		
		[TagDetails(590, TagType.String)]
		public string? BookingUnit { get; set; }
		
		[TagDetails(591, TagType.String)]
		public string? PreallocMethod { get; set; }
		
		[TagDetails(70, TagType.String)]
		public string? AllocID { get; set; }
		
		[Component]
		public PreAllocGrp? PreAllocGrp { get; set; }
		
		[TagDetails(854, TagType.Int)]
		public int? QtyType { get; set; }
		
		[Component]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[Component]
		public CommissionData? CommissionData { get; set; }
		
		[TagDetails(528, TagType.String)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(529, TagType.String)]
		public string? OrderRestrictions { get; set; }
		
		[TagDetails(582, TagType.Int)]
		public int? CustOrderCapacity { get; set; }
		
		[TagDetails(121, TagType.Boolean)]
		public bool? ForexReq { get; set; }
		
		[TagDetails(120, TagType.String)]
		public string? SettlCurrency { get; set; }
		
		[TagDetails(775, TagType.Int)]
		public int? BookingType { get; set; }
		
		[TagDetails(58, TagType.String)]
		public string? Text { get; set; }
		
		[TagDetails(354, TagType.Length)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(355, TagType.RawData)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(77, TagType.String)]
		public string? PositionEffect { get; set; }
		
		[TagDetails(203, TagType.Int)]
		public int? CoveredOrUncovered { get; set; }
		
		[TagDetails(544, TagType.String)]
		public string? CashMargin { get; set; }
		
		[TagDetails(635, TagType.String)]
		public string? ClearingFeeIndicator { get; set; }
		
		[TagDetails(377, TagType.Boolean)]
		public bool? SolicitedFlag { get; set; }
		
		[TagDetails(659, TagType.String)]
		public string? SideComplianceID { get; set; }
		
	}
}
