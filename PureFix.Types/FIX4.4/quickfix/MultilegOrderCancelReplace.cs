using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AC", FixVersion.FIX44)]
	public sealed class MultilegOrderCancelReplace : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 41, Type = TagType.String, Offset = 2)]
		public string? OrigClOrdID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 3)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 4)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 583, Type = TagType.String, Offset = 5)]
		public string? ClOrdLinkID { get; set; }
		
		[TagDetails(Tag = 586, Type = TagType.UtcTimestamp, Offset = 6)]
		public DateTime? OrigOrdModTime { get; set; }
		
		[Component(Offset = 7)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 8)]
		public DateTime? TradeOriginationDate { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 9)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 10)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 11)]
		public int? AcctIDSource { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 12)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 589, Type = TagType.String, Offset = 13)]
		public string? DayBookingInst { get; set; }
		
		[TagDetails(Tag = 590, Type = TagType.String, Offset = 14)]
		public string? BookingUnit { get; set; }
		
		[TagDetails(Tag = 591, Type = TagType.String, Offset = 15)]
		public string? PreallocMethod { get; set; }
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 16)]
		public string? AllocID { get; set; }
		
		[Component(Offset = 17)]
		public PreAllocMlegGrp? PreAllocMlegGrp { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 18)]
		public string? SettlType { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 19)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(Tag = 544, Type = TagType.String, Offset = 20)]
		public string? CashMargin { get; set; }
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 21)]
		public string? ClearingFeeIndicator { get; set; }
		
		[TagDetails(Tag = 21, Type = TagType.String, Offset = 22)]
		public string? HandlInst { get; set; }
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 23)]
		public string? ExecInst { get; set; }
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 24)]
		public double? MinQty { get; set; }
		
		[TagDetails(Tag = 111, Type = TagType.Float, Offset = 25)]
		public double? MaxFloor { get; set; }
		
		[TagDetails(Tag = 100, Type = TagType.String, Offset = 26)]
		public string? ExDestination { get; set; }
		
		[Component(Offset = 27)]
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		
		[TagDetails(Tag = 81, Type = TagType.String, Offset = 28)]
		public string? ProcessCode { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 29)]
		public string? Side { get; set; }
		
		[Component(Offset = 30)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 31)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(Tag = 140, Type = TagType.Float, Offset = 32)]
		public double? PrevClosePx { get; set; }
		
		[Component(Offset = 33)]
		public LegOrdGrp? LegOrdGrp { get; set; }
		
		[TagDetails(Tag = 114, Type = TagType.Boolean, Offset = 34)]
		public bool? LocateReqd { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 35)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 36)]
		public int? QtyType { get; set; }
		
		[Component(Offset = 37)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 38)]
		public string? OrdType { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 39)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 40)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 99, Type = TagType.Float, Offset = 41)]
		public double? StopPx { get; set; }
		
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
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 62)]
		public string? PositionEffect { get; set; }
		
		[TagDetails(Tag = 203, Type = TagType.Int, Offset = 63)]
		public int? CoveredOrUncovered { get; set; }
		
		[TagDetails(Tag = 210, Type = TagType.Float, Offset = 64)]
		public double? MaxShow { get; set; }
		
		[Component(Offset = 65)]
		public PegInstructions? PegInstructions { get; set; }
		
		[Component(Offset = 66)]
		public DiscretionInstructions? DiscretionInstructions { get; set; }
		
		[TagDetails(Tag = 847, Type = TagType.Int, Offset = 67)]
		public int? TargetStrategy { get; set; }
		
		[TagDetails(Tag = 848, Type = TagType.String, Offset = 68)]
		public string? TargetStrategyParameters { get; set; }
		
		[TagDetails(Tag = 849, Type = TagType.Float, Offset = 69)]
		public double? ParticipationRate { get; set; }
		
		[TagDetails(Tag = 480, Type = TagType.String, Offset = 70)]
		public string? CancellationRights { get; set; }
		
		[TagDetails(Tag = 481, Type = TagType.String, Offset = 71)]
		public string? MoneyLaunderingStatus { get; set; }
		
		[TagDetails(Tag = 513, Type = TagType.String, Offset = 72)]
		public string? RegistID { get; set; }
		
		[TagDetails(Tag = 494, Type = TagType.String, Offset = 73)]
		public string? Designation { get; set; }
		
		[TagDetails(Tag = 563, Type = TagType.Int, Offset = 74)]
		public int? MultiLegRptTypeReq { get; set; }
		
		[Component(Offset = 75)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
