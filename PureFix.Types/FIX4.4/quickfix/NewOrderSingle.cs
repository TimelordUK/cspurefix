using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("D", FixVersion.FIX44)]
	public sealed class NewOrderSingle : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
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
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 15)]
		public string? SettlType { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 16)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(Tag = 544, Type = TagType.String, Offset = 17)]
		public string? CashMargin { get; set; }
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 18)]
		public string? ClearingFeeIndicator { get; set; }
		
		[TagDetails(Tag = 21, Type = TagType.String, Offset = 19)]
		public string? HandlInst { get; set; }
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 20)]
		public string? ExecInst { get; set; }
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 21)]
		public double? MinQty { get; set; }
		
		[TagDetails(Tag = 111, Type = TagType.Float, Offset = 22)]
		public double? MaxFloor { get; set; }
		
		[TagDetails(Tag = 100, Type = TagType.String, Offset = 23)]
		public string? ExDestination { get; set; }
		
		[Component(Offset = 24)]
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		
		[TagDetails(Tag = 81, Type = TagType.String, Offset = 25)]
		public string? ProcessCode { get; set; }
		
		[Component(Offset = 26)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 27)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 28)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 140, Type = TagType.Float, Offset = 29)]
		public double? PrevClosePx { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 30)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 114, Type = TagType.Boolean, Offset = 31)]
		public bool? LocateReqd { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 32)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 33)]
		public Stipulations? Stipulations { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 34)]
		public int? QtyType { get; set; }
		
		[Component(Offset = 35)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 36)]
		public string? OrdType { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 37)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 38)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 99, Type = TagType.Float, Offset = 39)]
		public double? StopPx { get; set; }
		
		[Component(Offset = 40)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component(Offset = 41)]
		public YieldData? YieldData { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 42)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 43)]
		public string? ComplianceID { get; set; }
		
		[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 44)]
		public bool? SolicitedFlag { get; set; }
		
		[TagDetails(Tag = 23, Type = TagType.String, Offset = 45)]
		public string? IOIID { get; set; }
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 46)]
		public string? QuoteID { get; set; }
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 47)]
		public string? TimeInForce { get; set; }
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 48)]
		public DateTime? EffectiveTime { get; set; }
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 49)]
		public DateTime? ExpireDate { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 50)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 427, Type = TagType.Int, Offset = 51)]
		public int? GTBookingInst { get; set; }
		
		[Component(Offset = 52)]
		public CommissionData? CommissionData { get; set; }
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 53)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 54)]
		public string? OrderRestrictions { get; set; }
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 55)]
		public int? CustOrderCapacity { get; set; }
		
		[TagDetails(Tag = 121, Type = TagType.Boolean, Offset = 56)]
		public bool? ForexReq { get; set; }
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 57)]
		public string? SettlCurrency { get; set; }
		
		[TagDetails(Tag = 775, Type = TagType.Int, Offset = 58)]
		public int? BookingType { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 59)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 60)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 61)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 62)]
		public DateTime? SettlDate2 { get; set; }
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 63)]
		public double? OrderQty2 { get; set; }
		
		[TagDetails(Tag = 640, Type = TagType.Float, Offset = 64)]
		public double? Price2 { get; set; }
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 65)]
		public string? PositionEffect { get; set; }
		
		[TagDetails(Tag = 203, Type = TagType.Int, Offset = 66)]
		public int? CoveredOrUncovered { get; set; }
		
		[TagDetails(Tag = 210, Type = TagType.Float, Offset = 67)]
		public double? MaxShow { get; set; }
		
		[Component(Offset = 68)]
		public PegInstructions? PegInstructions { get; set; }
		
		[Component(Offset = 69)]
		public DiscretionInstructions? DiscretionInstructions { get; set; }
		
		[TagDetails(Tag = 847, Type = TagType.Int, Offset = 70)]
		public int? TargetStrategy { get; set; }
		
		[TagDetails(Tag = 848, Type = TagType.String, Offset = 71)]
		public string? TargetStrategyParameters { get; set; }
		
		[TagDetails(Tag = 849, Type = TagType.Float, Offset = 72)]
		public double? ParticipationRate { get; set; }
		
		[TagDetails(Tag = 480, Type = TagType.String, Offset = 73)]
		public string? CancellationRights { get; set; }
		
		[TagDetails(Tag = 481, Type = TagType.String, Offset = 74)]
		public string? MoneyLaunderingStatus { get; set; }
		
		[TagDetails(Tag = 513, Type = TagType.String, Offset = 75)]
		public string? RegistID { get; set; }
		
		[TagDetails(Tag = 494, Type = TagType.String, Offset = 76)]
		public string? Designation { get; set; }
		
		[Component(Offset = 77)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
