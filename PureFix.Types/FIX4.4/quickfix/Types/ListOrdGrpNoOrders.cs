using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class ListOrdGrpNoOrders : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 0, Required = true)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 1, Required = false)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 67, Type = TagType.Int, Offset = 2, Required = true)]
		public int? ListSeqNo { get; set; }
		
		[TagDetails(Tag = 583, Type = TagType.String, Offset = 3, Required = false)]
		public string? ClOrdLinkID { get; set; }
		
		[TagDetails(Tag = 160, Type = TagType.String, Offset = 4, Required = false)]
		public string? SettlInstMode { get; set; }
		
		[Component(Offset = 5, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 6, Required = false)]
		public DateOnly? TradeOriginationDate { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 7, Required = false)]
		public DateOnly? TradeDate { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 8, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 9, Required = false)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 10, Required = false)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 589, Type = TagType.String, Offset = 11, Required = false)]
		public string? DayBookingInst { get; set; }
		
		[TagDetails(Tag = 590, Type = TagType.String, Offset = 12, Required = false)]
		public string? BookingUnit { get; set; }
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 13, Required = false)]
		public string? AllocID { get; set; }
		
		[TagDetails(Tag = 591, Type = TagType.String, Offset = 14, Required = false)]
		public string? PreallocMethod { get; set; }
		
		[Component(Offset = 15, Required = false)]
		public PreAllocGrp? PreAllocGrp { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 16, Required = false)]
		public string? SettlType { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 17, Required = false)]
		public DateOnly? SettlDate { get; set; }
		
		[TagDetails(Tag = 544, Type = TagType.String, Offset = 18, Required = false)]
		public string? CashMargin { get; set; }
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 19, Required = false)]
		public string? ClearingFeeIndicator { get; set; }
		
		[TagDetails(Tag = 21, Type = TagType.String, Offset = 20, Required = false)]
		public string? HandlInst { get; set; }
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 21, Required = false)]
		public string? ExecInst { get; set; }
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 22, Required = false)]
		public double? MinQty { get; set; }
		
		[TagDetails(Tag = 111, Type = TagType.Float, Offset = 23, Required = false)]
		public double? MaxFloor { get; set; }
		
		[TagDetails(Tag = 100, Type = TagType.String, Offset = 24, Required = false)]
		public string? ExDestination { get; set; }
		
		[Component(Offset = 25, Required = false)]
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		
		[TagDetails(Tag = 81, Type = TagType.String, Offset = 26, Required = false)]
		public string? ProcessCode { get; set; }
		
		[Component(Offset = 27, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 28, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 140, Type = TagType.Float, Offset = 29, Required = false)]
		public double? PrevClosePx { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 30, Required = true)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 401, Type = TagType.Int, Offset = 31, Required = false)]
		public int? SideValueInd { get; set; }
		
		[TagDetails(Tag = 114, Type = TagType.Boolean, Offset = 32, Required = false)]
		public bool? LocateReqd { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 33, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 34, Required = false)]
		public Stipulations? Stipulations { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 35, Required = false)]
		public int? QtyType { get; set; }
		
		[Component(Offset = 36, Required = true)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 37, Required = false)]
		public string? OrdType { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 38, Required = false)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 39, Required = false)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 99, Type = TagType.Float, Offset = 40, Required = false)]
		public double? StopPx { get; set; }
		
		[Component(Offset = 41, Required = false)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component(Offset = 42, Required = false)]
		public YieldData? YieldData { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 43, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 44, Required = false)]
		public string? ComplianceID { get; set; }
		
		[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 45, Required = false)]
		public bool? SolicitedFlag { get; set; }
		
		[TagDetails(Tag = 23, Type = TagType.String, Offset = 46, Required = false)]
		public string? IOIID { get; set; }
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 47, Required = false)]
		public string? QuoteID { get; set; }
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 48, Required = false)]
		public string? TimeInForce { get; set; }
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 49, Required = false)]
		public DateTime? EffectiveTime { get; set; }
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 50, Required = false)]
		public DateOnly? ExpireDate { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 51, Required = false)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 427, Type = TagType.Int, Offset = 52, Required = false)]
		public int? GTBookingInst { get; set; }
		
		[Component(Offset = 53, Required = false)]
		public CommissionData? CommissionData { get; set; }
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 54, Required = false)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 55, Required = false)]
		public string? OrderRestrictions { get; set; }
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 56, Required = false)]
		public int? CustOrderCapacity { get; set; }
		
		[TagDetails(Tag = 121, Type = TagType.Boolean, Offset = 57, Required = false)]
		public bool? ForexReq { get; set; }
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 58, Required = false)]
		public string? SettlCurrency { get; set; }
		
		[TagDetails(Tag = 775, Type = TagType.Int, Offset = 59, Required = false)]
		public int? BookingType { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 60, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 61, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 62, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 63, Required = false)]
		public DateOnly? SettlDate2 { get; set; }
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 64, Required = false)]
		public double? OrderQty2 { get; set; }
		
		[TagDetails(Tag = 640, Type = TagType.Float, Offset = 65, Required = false)]
		public double? Price2 { get; set; }
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 66, Required = false)]
		public string? PositionEffect { get; set; }
		
		[TagDetails(Tag = 203, Type = TagType.Int, Offset = 67, Required = false)]
		public int? CoveredOrUncovered { get; set; }
		
		[TagDetails(Tag = 210, Type = TagType.Float, Offset = 68, Required = false)]
		public double? MaxShow { get; set; }
		
		[Component(Offset = 69, Required = false)]
		public PegInstructions? PegInstructions { get; set; }
		
		[Component(Offset = 70, Required = false)]
		public DiscretionInstructions? DiscretionInstructions { get; set; }
		
		[TagDetails(Tag = 847, Type = TagType.Int, Offset = 71, Required = false)]
		public int? TargetStrategy { get; set; }
		
		[TagDetails(Tag = 848, Type = TagType.String, Offset = 72, Required = false)]
		public string? TargetStrategyParameters { get; set; }
		
		[TagDetails(Tag = 849, Type = TagType.Float, Offset = 73, Required = false)]
		public double? ParticipationRate { get; set; }
		
		[TagDetails(Tag = 494, Type = TagType.String, Offset = 74, Required = false)]
		public string? Designation { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				ClOrdID is not null
				&& ListSeqNo is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& Side is not null
				&& OrderQtyData is not null && ((IFixValidator)OrderQtyData).IsValid(in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
			if (ListSeqNo is not null) writer.WriteWholeNumber(67, ListSeqNo.Value);
			if (ClOrdLinkID is not null) writer.WriteString(583, ClOrdLinkID);
			if (SettlInstMode is not null) writer.WriteString(160, SettlInstMode);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (TradeOriginationDate is not null) writer.WriteLocalDateOnly(229, TradeOriginationDate.Value);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (Account is not null) writer.WriteString(1, Account);
			if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (DayBookingInst is not null) writer.WriteString(589, DayBookingInst);
			if (BookingUnit is not null) writer.WriteString(590, BookingUnit);
			if (AllocID is not null) writer.WriteString(70, AllocID);
			if (PreallocMethod is not null) writer.WriteString(591, PreallocMethod);
			if (PreAllocGrp is not null) ((IFixEncoder)PreAllocGrp).Encode(writer);
			if (SettlType is not null) writer.WriteString(63, SettlType);
			if (SettlDate is not null) writer.WriteLocalDateOnly(64, SettlDate.Value);
			if (CashMargin is not null) writer.WriteString(544, CashMargin);
			if (ClearingFeeIndicator is not null) writer.WriteString(635, ClearingFeeIndicator);
			if (HandlInst is not null) writer.WriteString(21, HandlInst);
			if (ExecInst is not null) writer.WriteString(18, ExecInst);
			if (MinQty is not null) writer.WriteNumber(110, MinQty.Value);
			if (MaxFloor is not null) writer.WriteNumber(111, MaxFloor.Value);
			if (ExDestination is not null) writer.WriteString(100, ExDestination);
			if (TrdgSesGrp is not null) ((IFixEncoder)TrdgSesGrp).Encode(writer);
			if (ProcessCode is not null) writer.WriteString(81, ProcessCode);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (PrevClosePx is not null) writer.WriteNumber(140, PrevClosePx.Value);
			if (Side is not null) writer.WriteString(54, Side);
			if (SideValueInd is not null) writer.WriteWholeNumber(401, SideValueInd.Value);
			if (LocateReqd is not null) writer.WriteBoolean(114, LocateReqd.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (Stipulations is not null) ((IFixEncoder)Stipulations).Encode(writer);
			if (QtyType is not null) writer.WriteWholeNumber(854, QtyType.Value);
			if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
			if (OrdType is not null) writer.WriteString(40, OrdType);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (StopPx is not null) writer.WriteNumber(99, StopPx.Value);
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (YieldData is not null) ((IFixEncoder)YieldData).Encode(writer);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (ComplianceID is not null) writer.WriteString(376, ComplianceID);
			if (SolicitedFlag is not null) writer.WriteBoolean(377, SolicitedFlag.Value);
			if (IOIID is not null) writer.WriteString(23, IOIID);
			if (QuoteID is not null) writer.WriteString(117, QuoteID);
			if (TimeInForce is not null) writer.WriteString(59, TimeInForce);
			if (EffectiveTime is not null) writer.WriteUtcTimeStamp(168, EffectiveTime.Value);
			if (ExpireDate is not null) writer.WriteLocalDateOnly(432, ExpireDate.Value);
			if (ExpireTime is not null) writer.WriteUtcTimeStamp(126, ExpireTime.Value);
			if (GTBookingInst is not null) writer.WriteWholeNumber(427, GTBookingInst.Value);
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
			if (SettlDate2 is not null) writer.WriteLocalDateOnly(193, SettlDate2.Value);
			if (OrderQty2 is not null) writer.WriteNumber(192, OrderQty2.Value);
			if (Price2 is not null) writer.WriteNumber(640, Price2.Value);
			if (PositionEffect is not null) writer.WriteString(77, PositionEffect);
			if (CoveredOrUncovered is not null) writer.WriteWholeNumber(203, CoveredOrUncovered.Value);
			if (MaxShow is not null) writer.WriteNumber(210, MaxShow.Value);
			if (PegInstructions is not null) ((IFixEncoder)PegInstructions).Encode(writer);
			if (DiscretionInstructions is not null) ((IFixEncoder)DiscretionInstructions).Encode(writer);
			if (TargetStrategy is not null) writer.WriteWholeNumber(847, TargetStrategy.Value);
			if (TargetStrategyParameters is not null) writer.WriteString(848, TargetStrategyParameters);
			if (ParticipationRate is not null) writer.WriteNumber(849, ParticipationRate.Value);
			if (Designation is not null) writer.WriteString(494, Designation);
		}
	}
}
