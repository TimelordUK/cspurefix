using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SideCrossOrdModGrpNoSides : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 0, Required = true)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 1, Required = true)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 583, Type = TagType.String, Offset = 3, Required = false)]
		public string? ClOrdLinkID { get; set; }
		
		[Component(Offset = 4, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 5, Required = false)]
		public DateOnly? TradeOriginationDate { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 6, Required = false)]
		public DateOnly? TradeDate { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 7, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 8, Required = false)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 9, Required = false)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 589, Type = TagType.String, Offset = 10, Required = false)]
		public string? DayBookingInst { get; set; }
		
		[TagDetails(Tag = 590, Type = TagType.String, Offset = 11, Required = false)]
		public string? BookingUnit { get; set; }
		
		[TagDetails(Tag = 591, Type = TagType.String, Offset = 12, Required = false)]
		public string? PreallocMethod { get; set; }
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 13, Required = false)]
		public string? AllocID { get; set; }
		
		[Component(Offset = 14, Required = false)]
		public PreAllocGrp? PreAllocGrp { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 15, Required = false)]
		public int? QtyType { get; set; }
		
		[Component(Offset = 16, Required = true)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[Component(Offset = 17, Required = false)]
		public CommissionData? CommissionData { get; set; }
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 18, Required = false)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 19, Required = false)]
		public string? OrderRestrictions { get; set; }
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 20, Required = false)]
		public int? CustOrderCapacity { get; set; }
		
		[TagDetails(Tag = 121, Type = TagType.Boolean, Offset = 21, Required = false)]
		public bool? ForexReq { get; set; }
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 22, Required = false)]
		public string? SettlCurrency { get; set; }
		
		[TagDetails(Tag = 775, Type = TagType.Int, Offset = 23, Required = false)]
		public int? BookingType { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 24, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 25, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 26, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 27, Required = false)]
		public string? PositionEffect { get; set; }
		
		[TagDetails(Tag = 203, Type = TagType.Int, Offset = 28, Required = false)]
		public int? CoveredOrUncovered { get; set; }
		
		[TagDetails(Tag = 544, Type = TagType.String, Offset = 29, Required = false)]
		public string? CashMargin { get; set; }
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 30, Required = false)]
		public string? ClearingFeeIndicator { get; set; }
		
		[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 31, Required = false)]
		public bool? SolicitedFlag { get; set; }
		
		[TagDetails(Tag = 659, Type = TagType.String, Offset = 32, Required = false)]
		public string? SideComplianceID { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				Side is not null
				&& ClOrdID is not null
				&& OrderQtyData is not null && ((IFixValidator)OrderQtyData).IsValid(in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Side is not null) writer.WriteString(54, Side);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
			if (ClOrdLinkID is not null) writer.WriteString(583, ClOrdLinkID);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (TradeOriginationDate is not null) writer.WriteLocalDateOnly(229, TradeOriginationDate.Value);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (DayBookingInst is not null) writer.WriteString(589, DayBookingInst);
			if (BookingUnit is not null) writer.WriteString(590, BookingUnit);
			if (PreallocMethod is not null) writer.WriteString(591, PreallocMethod);
			if (AllocID is not null) writer.WriteString(70, AllocID);
			if (PreAllocGrp is not null) ((IFixEncoder)PreAllocGrp).Encode(writer);
			if (QtyType is not null) writer.WriteWholeNumber(854, QtyType.Value);
			if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
			if (CommissionData is not null) ((IFixEncoder)CommissionData).Encode(writer);
			if (OrderCapacity is not null) writer.WriteString(528, OrderCapacity);
			if (OrderRestrictions is not null) writer.WriteString(529, OrderRestrictions);
			if (CustOrderCapacity is not null) writer.WriteWholeNumber(582, CustOrderCapacity.Value);
			if (ForexReq is not null) writer.WriteBoolean(121, ForexReq.Value);
			if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
			if (BookingType is not null) writer.WriteWholeNumber(775, BookingType.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (PositionEffect is not null) writer.WriteString(77, PositionEffect);
			if (CoveredOrUncovered is not null) writer.WriteWholeNumber(203, CoveredOrUncovered.Value);
			if (CashMargin is not null) writer.WriteString(544, CashMargin);
			if (ClearingFeeIndicator is not null) writer.WriteString(635, ClearingFeeIndicator);
			if (SolicitedFlag is not null) writer.WriteBoolean(377, SolicitedFlag.Value);
			if (SideComplianceID is not null) writer.WriteString(659, SideComplianceID);
		}
	}
}
