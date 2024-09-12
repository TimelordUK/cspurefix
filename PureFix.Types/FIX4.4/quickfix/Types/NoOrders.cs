using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoOrders
	{
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 0)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 1)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 67, Type = TagType.Int, Offset = 2)]
		public int? ListSeqNo { get; set; }
		
		[TagDetails(Tag = 583, Type = TagType.String, Offset = 3)]
		public string? ClOrdLinkID { get; set; }
		
		[TagDetails(Tag = 160, Type = TagType.String, Offset = 4)]
		public string? SettlInstMode { get; set; }
		
		[Component(Offset = 5)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 6)]
		public DateTime? TradeOriginationDate { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 7)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 8)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 9)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 10)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 589, Type = TagType.String, Offset = 11)]
		public string? DayBookingInst { get; set; }
		
		[TagDetails(Tag = 590, Type = TagType.String, Offset = 12)]
		public string? BookingUnit { get; set; }
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 13)]
		public string? AllocID { get; set; }
		
		[TagDetails(Tag = 591, Type = TagType.String, Offset = 14)]
		public string? PreallocMethod { get; set; }
		
		[Component(Offset = 15)]
		public PreAllocGrp? PreAllocGrp { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 16)]
		public string? SettlType { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 17)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(Tag = 544, Type = TagType.String, Offset = 18)]
		public string? CashMargin { get; set; }
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 19)]
		public string? ClearingFeeIndicator { get; set; }
		
		[TagDetails(Tag = 21, Type = TagType.String, Offset = 20)]
		public string? HandlInst { get; set; }
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 21)]
		public string? ExecInst { get; set; }
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 22)]
		public double? MinQty { get; set; }
		
		[TagDetails(Tag = 111, Type = TagType.Float, Offset = 23)]
		public double? MaxFloor { get; set; }
		
		[TagDetails(Tag = 100, Type = TagType.String, Offset = 24)]
		public string? ExDestination { get; set; }
		
		[Component(Offset = 25)]
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		
		[TagDetails(Tag = 81, Type = TagType.String, Offset = 26)]
		public string? ProcessCode { get; set; }
		
		[Component(Offset = 27)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 28)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 140, Type = TagType.Float, Offset = 29)]
		public double? PrevClosePx { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 30)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 401, Type = TagType.Int, Offset = 31)]
		public int? SideValueInd { get; set; }
		
		[TagDetails(Tag = 114, Type = TagType.Boolean, Offset = 32)]
		public bool? LocateReqd { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 33)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 34)]
		public Stipulations? Stipulations { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 35)]
		public int? QtyType { get; set; }
		
		[Component(Offset = 36)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 37)]
		public string? OrdType { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 38)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 39)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 99, Type = TagType.Float, Offset = 40)]
		public double? StopPx { get; set; }
		
		[Component(Offset = 41)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component(Offset = 42)]
		public YieldData? YieldData { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 43)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 44)]
		public string? ComplianceID { get; set; }
		
		[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 45)]
		public bool? SolicitedFlag { get; set; }
		
		[TagDetails(Tag = 23, Type = TagType.String, Offset = 46)]
		public string? IOIID { get; set; }
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 47)]
		public string? QuoteID { get; set; }
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 48)]
		public string? TimeInForce { get; set; }
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 49)]
		public DateTime? EffectiveTime { get; set; }
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 50)]
		public DateTime? ExpireDate { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 51)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 427, Type = TagType.Int, Offset = 52)]
		public int? GTBookingInst { get; set; }
		
		[Component(Offset = 53)]
		public CommissionData? CommissionData { get; set; }
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 54)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 55)]
		public string? OrderRestrictions { get; set; }
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 56)]
		public int? CustOrderCapacity { get; set; }
		
		[TagDetails(Tag = 121, Type = TagType.Boolean, Offset = 57)]
		public bool? ForexReq { get; set; }
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 58)]
		public string? SettlCurrency { get; set; }
		
		[TagDetails(Tag = 775, Type = TagType.Int, Offset = 59)]
		public int? BookingType { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 60)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 61)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 62)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 63)]
		public DateTime? SettlDate2 { get; set; }
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 64)]
		public double? OrderQty2 { get; set; }
		
		[TagDetails(Tag = 640, Type = TagType.Float, Offset = 65)]
		public double? Price2 { get; set; }
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 66)]
		public string? PositionEffect { get; set; }
		
		[TagDetails(Tag = 203, Type = TagType.Int, Offset = 67)]
		public int? CoveredOrUncovered { get; set; }
		
		[TagDetails(Tag = 210, Type = TagType.Float, Offset = 68)]
		public double? MaxShow { get; set; }
		
		[Component(Offset = 69)]
		public PegInstructions? PegInstructions { get; set; }
		
		[Component(Offset = 70)]
		public DiscretionInstructions? DiscretionInstructions { get; set; }
		
		[TagDetails(Tag = 847, Type = TagType.Int, Offset = 71)]
		public int? TargetStrategy { get; set; }
		
		[TagDetails(Tag = 848, Type = TagType.String, Offset = 72)]
		public string? TargetStrategyParameters { get; set; }
		
		[TagDetails(Tag = 849, Type = TagType.Float, Offset = 73)]
		public double? ParticipationRate { get; set; }
		
		[TagDetails(Tag = 494, Type = TagType.String, Offset = 74)]
		public string? Designation { get; set; }
		
	}
}
