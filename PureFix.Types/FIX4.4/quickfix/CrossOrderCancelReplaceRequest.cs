using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("t", FixVersion.FIX44)]
	public sealed class CrossOrderCancelReplaceRequest : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(37, TagType.String)]
		public string? OrderID { get; set; }
		
		[TagDetails(548, TagType.String)]
		public string? CrossID { get; set; }
		
		[TagDetails(551, TagType.String)]
		public string? OrigCrossID { get; set; }
		
		[TagDetails(549, TagType.Int)]
		public int? CrossType { get; set; }
		
		[TagDetails(550, TagType.Int)]
		public int? CrossPrioritization { get; set; }
		
		[Component]
		public SideCrossOrdModGrp? SideCrossOrdModGrp { get; set; }
		
		[Component]
		public Instrument? Instrument { get; set; }
		
		[Component]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(63, TagType.String)]
		public string? SettlType { get; set; }
		
		[TagDetails(64, TagType.LocalDate)]
		public DateTime? SettlDate { get; set; }
		
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
		
		[TagDetails(140, TagType.Float)]
		public double? PrevClosePx { get; set; }
		
		[TagDetails(114, TagType.Boolean)]
		public bool? LocateReqd { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[Component]
		public Stipulations? Stipulations { get; set; }
		
		[TagDetails(40, TagType.String)]
		public string? OrdType { get; set; }
		
		[TagDetails(423, TagType.Int)]
		public int? PriceType { get; set; }
		
		[TagDetails(44, TagType.Float)]
		public double? Price { get; set; }
		
		[TagDetails(99, TagType.Float)]
		public double? StopPx { get; set; }
		
		[Component]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component]
		public YieldData? YieldData { get; set; }
		
		[TagDetails(15, TagType.String)]
		public string? Currency { get; set; }
		
		[TagDetails(376, TagType.String)]
		public string? ComplianceID { get; set; }
		
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
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
