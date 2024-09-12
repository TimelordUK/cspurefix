using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class MultilegOrderCancelReplace : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(37, TagType.String)]
		public string? OrderID { get; set; }
		
		[TagDetails(41, TagType.String)]
		public string? OrigClOrdID { get; set; }
		
		[TagDetails(11, TagType.String)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(526, TagType.String)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(583, TagType.String)]
		public string? ClOrdLinkID { get; set; }
		
		[TagDetails(586, TagType.UtcTimestamp)]
		public DateTime? OrigOrdModTime { get; set; }
		
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
		public PreAllocMlegGrp? PreAllocMlegGrp { get; set; }
		
		[TagDetails(63, TagType.String)]
		public string? SettlType { get; set; }
		
		[TagDetails(64, TagType.LocalDate)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(544, TagType.String)]
		public string? CashMargin { get; set; }
		
		[TagDetails(635, TagType.String)]
		public string? ClearingFeeIndicator { get; set; }
		
		[TagDetails(21, TagType.String)]
		public string? HandlInst { get; set; }
		
		[TagDetails(18, TagType.String)]
		public string? ExecInst { get; set; }
		
		[TagDetails(110, TagType.Float)]
		public double? MinQty { get; set; }
		
		[TagDetails(111, TagType.Float)]
		public double? MaxFloor { get; set; }
		
		[TagDetails(100, TagType.String)]
		public string? ExDestination { get; set; }
		
		[Component]
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		
		[TagDetails(81, TagType.String)]
		public string? ProcessCode { get; set; }
		
		[TagDetails(54, TagType.String)]
		public string? Side { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[TagDetails(140, TagType.Float)]
		public double? PrevClosePx { get; set; }
		
		[Component]
		public LegOrdGrp? LegOrdGrp { get; set; }
		
		[TagDetails(114, TagType.Boolean)]
		public bool? LocateReqd { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(854, TagType.Int)]
		public int? QtyType { get; set; }
		
		[Component]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(40, TagType.String)]
		public string? OrdType { get; set; }
		
		[TagDetails(423, TagType.Int)]
		public int? PriceType { get; set; }
		
		[TagDetails(44, TagType.Float)]
		public double? Price { get; set; }
		
		[TagDetails(99, TagType.Float)]
		public double? StopPx { get; set; }
		
		[TagDetails(15, TagType.String)]
		public string? Currency { get; set; }
		
		[TagDetails(376, TagType.String)]
		public string? ComplianceID { get; set; }
		
		[TagDetails(377, TagType.Boolean)]
		public bool? SolicitedFlag { get; set; }
		
		[TagDetails(23, TagType.String)]
		public string? IOIID { get; set; }
		
		[TagDetails(117, TagType.String)]
		public string? QuoteID { get; set; }
		
		[TagDetails(59, TagType.String)]
		public string? TimeInForce { get; set; }
		
		[TagDetails(168, TagType.UtcTimestamp)]
		public DateTime? EffectiveTime { get; set; }
		
		[TagDetails(432, TagType.LocalDate)]
		public DateTime? ExpireDate { get; set; }
		
		[TagDetails(126, TagType.UtcTimestamp)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(427, TagType.Int)]
		public int? GTBookingInst { get; set; }
		
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
		
		[TagDetails(210, TagType.Float)]
		public double? MaxShow { get; set; }
		
		[Component]
		public PegInstructions? PegInstructions { get; set; }
		
		[Component]
		public DiscretionInstructions? DiscretionInstructions { get; set; }
		
		[TagDetails(847, TagType.Int)]
		public int? TargetStrategy { get; set; }
		
		[TagDetails(848, TagType.String)]
		public string? TargetStrategyParameters { get; set; }
		
		[TagDetails(849, TagType.Float)]
		public double? ParticipationRate { get; set; }
		
		[TagDetails(480, TagType.String)]
		public string? CancellationRights { get; set; }
		
		[TagDetails(481, TagType.String)]
		public string? MoneyLaunderingStatus { get; set; }
		
		[TagDetails(513, TagType.String)]
		public string? RegistID { get; set; }
		
		[TagDetails(494, TagType.String)]
		public string? Designation { get; set; }
		
		[TagDetails(563, TagType.Int)]
		public int? MultiLegRptTypeReq { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
