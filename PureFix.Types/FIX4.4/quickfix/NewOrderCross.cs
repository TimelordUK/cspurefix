using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("s", FixVersion.FIX44)]
	public sealed class NewOrderCross : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 548, Type = TagType.String, Offset = 1)]
		public string? CrossID { get; set; }
		
		[TagDetails(Tag = 549, Type = TagType.Int, Offset = 2)]
		public int? CrossType { get; set; }
		
		[TagDetails(Tag = 550, Type = TagType.Int, Offset = 3)]
		public int? CrossPrioritization { get; set; }
		
		[Component(Offset = 4)]
		public SideCrossOrdModGrp? SideCrossOrdModGrp { get; set; }
		
		[Component(Offset = 5)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 6)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 7)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 8)]
		public string? SettlType { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 9)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(Tag = 21, Type = TagType.String, Offset = 10)]
		public string? HandlInst { get; set; }
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 11)]
		public string? ExecInst { get; set; }
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 12)]
		public double? MinQty { get; set; }
		
		[TagDetails(Tag = 111, Type = TagType.Float, Offset = 13)]
		public double? MaxFloor { get; set; }
		
		[TagDetails(Tag = 100, Type = TagType.String, Offset = 14)]
		public string? ExDestination { get; set; }
		
		[Component(Offset = 15)]
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		
		[TagDetails(Tag = 81, Type = TagType.String, Offset = 16)]
		public string? ProcessCode { get; set; }
		
		[TagDetails(Tag = 140, Type = TagType.Float, Offset = 17)]
		public double? PrevClosePx { get; set; }
		
		[TagDetails(Tag = 114, Type = TagType.Boolean, Offset = 18)]
		public bool? LocateReqd { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 19)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 20)]
		public Stipulations? Stipulations { get; set; }
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 21)]
		public string? OrdType { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 22)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 23)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 99, Type = TagType.Float, Offset = 24)]
		public double? StopPx { get; set; }
		
		[Component(Offset = 25)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component(Offset = 26)]
		public YieldData? YieldData { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 27)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 28)]
		public string? ComplianceID { get; set; }
		
		[TagDetails(Tag = 23, Type = TagType.String, Offset = 29)]
		public string? IOIID { get; set; }
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 30)]
		public string? QuoteID { get; set; }
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 31)]
		public string? TimeInForce { get; set; }
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 32)]
		public DateTime? EffectiveTime { get; set; }
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 33)]
		public DateTime? ExpireDate { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 34)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 427, Type = TagType.Int, Offset = 35)]
		public int? GTBookingInst { get; set; }
		
		[TagDetails(Tag = 210, Type = TagType.Float, Offset = 36)]
		public double? MaxShow { get; set; }
		
		[Component(Offset = 37)]
		public PegInstructions? PegInstructions { get; set; }
		
		[Component(Offset = 38)]
		public DiscretionInstructions? DiscretionInstructions { get; set; }
		
		[TagDetails(Tag = 847, Type = TagType.Int, Offset = 39)]
		public int? TargetStrategy { get; set; }
		
		[TagDetails(Tag = 848, Type = TagType.String, Offset = 40)]
		public string? TargetStrategyParameters { get; set; }
		
		[TagDetails(Tag = 849, Type = TagType.Float, Offset = 41)]
		public double? ParticipationRate { get; set; }
		
		[TagDetails(Tag = 480, Type = TagType.String, Offset = 42)]
		public string? CancellationRights { get; set; }
		
		[TagDetails(Tag = 481, Type = TagType.String, Offset = 43)]
		public string? MoneyLaunderingStatus { get; set; }
		
		[TagDetails(Tag = 513, Type = TagType.String, Offset = 44)]
		public string? RegistID { get; set; }
		
		[TagDetails(Tag = 494, Type = TagType.String, Offset = 45)]
		public string? Designation { get; set; }
		
		[Component(Offset = 46)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
