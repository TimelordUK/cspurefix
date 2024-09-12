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
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 0)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 1)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 2)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 583, Type = TagType.String, Offset = 3)]
		public string? ClOrdLinkID { get; set; }
		
		[Component(Offset = 4)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 5)]
		public DateTime? TradeOriginationDate { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 6)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 7)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 8)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 9)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 589, Type = TagType.String, Offset = 10)]
		public string? DayBookingInst { get; set; }
		
		[TagDetails(Tag = 590, Type = TagType.String, Offset = 11)]
		public string? BookingUnit { get; set; }
		
		[TagDetails(Tag = 591, Type = TagType.String, Offset = 12)]
		public string? PreallocMethod { get; set; }
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 13)]
		public string? AllocID { get; set; }
		
		[Component(Offset = 14)]
		public PreAllocGrp? PreAllocGrp { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 15)]
		public int? QtyType { get; set; }
		
		[Component(Offset = 16)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[Component(Offset = 17)]
		public CommissionData? CommissionData { get; set; }
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 18)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 19)]
		public string? OrderRestrictions { get; set; }
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 20)]
		public int? CustOrderCapacity { get; set; }
		
		[TagDetails(Tag = 121, Type = TagType.Boolean, Offset = 21)]
		public bool? ForexReq { get; set; }
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 22)]
		public string? SettlCurrency { get; set; }
		
		[TagDetails(Tag = 775, Type = TagType.Int, Offset = 23)]
		public int? BookingType { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 24)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 25)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 26)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 27)]
		public string? PositionEffect { get; set; }
		
		[TagDetails(Tag = 203, Type = TagType.Int, Offset = 28)]
		public int? CoveredOrUncovered { get; set; }
		
		[TagDetails(Tag = 544, Type = TagType.String, Offset = 29)]
		public string? CashMargin { get; set; }
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 30)]
		public string? ClearingFeeIndicator { get; set; }
		
		[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 31)]
		public bool? SolicitedFlag { get; set; }
		
		[TagDetails(Tag = 659, Type = TagType.String, Offset = 32)]
		public string? SideComplianceID { get; set; }
		
	}
}
