using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("t", FixVersion.FIX44)]
	public sealed partial class CrossOrderCancelReplaceRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = false)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 548, Type = TagType.String, Offset = 2, Required = true)]
		public string? CrossID { get; set; }
		
		[TagDetails(Tag = 551, Type = TagType.String, Offset = 3, Required = true)]
		public string? OrigCrossID { get; set; }
		
		[TagDetails(Tag = 549, Type = TagType.Int, Offset = 4, Required = true)]
		public int? CrossType { get; set; }
		
		[TagDetails(Tag = 550, Type = TagType.Int, Offset = 5, Required = true)]
		public int? CrossPrioritization { get; set; }
		
		[Component(Offset = 6, Required = true)]
		public SideCrossOrdModGrp? SideCrossOrdModGrp { get; set; }
		
		[Component(Offset = 7, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 8, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 9, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 10, Required = false)]
		public string? SettlType { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 11, Required = false)]
		public DateOnly? SettlDate { get; set; }
		
		[TagDetails(Tag = 21, Type = TagType.String, Offset = 12, Required = false)]
		public string? HandlInst { get; set; }
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 13, Required = false)]
		public string? ExecInst { get; set; }
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 14, Required = false)]
		public double? MinQty { get; set; }
		
		[TagDetails(Tag = 111, Type = TagType.Float, Offset = 15, Required = false)]
		public double? MaxFloor { get; set; }
		
		[TagDetails(Tag = 100, Type = TagType.String, Offset = 16, Required = false)]
		public string? ExDestination { get; set; }
		
		[Component(Offset = 17, Required = false)]
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		
		[TagDetails(Tag = 81, Type = TagType.String, Offset = 18, Required = false)]
		public string? ProcessCode { get; set; }
		
		[TagDetails(Tag = 140, Type = TagType.Float, Offset = 19, Required = false)]
		public double? PrevClosePx { get; set; }
		
		[TagDetails(Tag = 114, Type = TagType.Boolean, Offset = 20, Required = false)]
		public bool? LocateReqd { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 21, Required = true)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 22, Required = false)]
		public Stipulations? Stipulations { get; set; }
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 23, Required = true)]
		public string? OrdType { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 24, Required = false)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 25, Required = false)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 99, Type = TagType.Float, Offset = 26, Required = false)]
		public double? StopPx { get; set; }
		
		[Component(Offset = 27, Required = false)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[Component(Offset = 28, Required = false)]
		public YieldData? YieldData { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 29, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 30, Required = false)]
		public string? ComplianceID { get; set; }
		
		[TagDetails(Tag = 23, Type = TagType.String, Offset = 31, Required = false)]
		public string? IOIID { get; set; }
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 32, Required = false)]
		public string? QuoteID { get; set; }
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 33, Required = false)]
		public string? TimeInForce { get; set; }
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 34, Required = false)]
		public DateTime? EffectiveTime { get; set; }
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 35, Required = false)]
		public DateOnly? ExpireDate { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 36, Required = false)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 427, Type = TagType.Int, Offset = 37, Required = false)]
		public int? GTBookingInst { get; set; }
		
		[TagDetails(Tag = 210, Type = TagType.Float, Offset = 38, Required = false)]
		public double? MaxShow { get; set; }
		
		[Component(Offset = 39, Required = false)]
		public PegInstructions? PegInstructions { get; set; }
		
		[Component(Offset = 40, Required = false)]
		public DiscretionInstructions? DiscretionInstructions { get; set; }
		
		[TagDetails(Tag = 847, Type = TagType.Int, Offset = 41, Required = false)]
		public int? TargetStrategy { get; set; }
		
		[TagDetails(Tag = 848, Type = TagType.String, Offset = 42, Required = false)]
		public string? TargetStrategyParameters { get; set; }
		
		[TagDetails(Tag = 849, Type = TagType.Float, Offset = 43, Required = false)]
		public double? ParticipationRate { get; set; }
		
		[TagDetails(Tag = 480, Type = TagType.String, Offset = 44, Required = false)]
		public string? CancellationRights { get; set; }
		
		[TagDetails(Tag = 481, Type = TagType.String, Offset = 45, Required = false)]
		public string? MoneyLaunderingStatus { get; set; }
		
		[TagDetails(Tag = 513, Type = TagType.String, Offset = 46, Required = false)]
		public string? RegistID { get; set; }
		
		[TagDetails(Tag = 494, Type = TagType.String, Offset = 47, Required = false)]
		public string? Designation { get; set; }
		
		[Component(Offset = 48, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& CrossID is not null
				&& OrigCrossID is not null
				&& CrossType is not null
				&& CrossPrioritization is not null
				&& SideCrossOrdModGrp is not null && ((IFixValidator)SideCrossOrdModGrp).IsValid(in config)
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& TransactTime is not null
				&& OrdType is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (CrossID is not null) writer.WriteString(548, CrossID);
			if (OrigCrossID is not null) writer.WriteString(551, OrigCrossID);
			if (CrossType is not null) writer.WriteWholeNumber(549, CrossType.Value);
			if (CrossPrioritization is not null) writer.WriteWholeNumber(550, CrossPrioritization.Value);
			if (SideCrossOrdModGrp is not null) ((IFixEncoder)SideCrossOrdModGrp).Encode(writer);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (SettlType is not null) writer.WriteString(63, SettlType);
			if (SettlDate is not null) writer.WriteLocalDateOnly(64, SettlDate.Value);
			if (HandlInst is not null) writer.WriteString(21, HandlInst);
			if (ExecInst is not null) writer.WriteString(18, ExecInst);
			if (MinQty is not null) writer.WriteNumber(110, MinQty.Value);
			if (MaxFloor is not null) writer.WriteNumber(111, MaxFloor.Value);
			if (ExDestination is not null) writer.WriteString(100, ExDestination);
			if (TrdgSesGrp is not null) ((IFixEncoder)TrdgSesGrp).Encode(writer);
			if (ProcessCode is not null) writer.WriteString(81, ProcessCode);
			if (PrevClosePx is not null) writer.WriteNumber(140, PrevClosePx.Value);
			if (LocateReqd is not null) writer.WriteBoolean(114, LocateReqd.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (Stipulations is not null) ((IFixEncoder)Stipulations).Encode(writer);
			if (OrdType is not null) writer.WriteString(40, OrdType);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (StopPx is not null) writer.WriteNumber(99, StopPx.Value);
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (YieldData is not null) ((IFixEncoder)YieldData).Encode(writer);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (ComplianceID is not null) writer.WriteString(376, ComplianceID);
			if (IOIID is not null) writer.WriteString(23, IOIID);
			if (QuoteID is not null) writer.WriteString(117, QuoteID);
			if (TimeInForce is not null) writer.WriteString(59, TimeInForce);
			if (EffectiveTime is not null) writer.WriteUtcTimeStamp(168, EffectiveTime.Value);
			if (ExpireDate is not null) writer.WriteLocalDateOnly(432, ExpireDate.Value);
			if (ExpireTime is not null) writer.WriteUtcTimeStamp(126, ExpireTime.Value);
			if (GTBookingInst is not null) writer.WriteWholeNumber(427, GTBookingInst.Value);
			if (MaxShow is not null) writer.WriteNumber(210, MaxShow.Value);
			if (PegInstructions is not null) ((IFixEncoder)PegInstructions).Encode(writer);
			if (DiscretionInstructions is not null) ((IFixEncoder)DiscretionInstructions).Encode(writer);
			if (TargetStrategy is not null) writer.WriteWholeNumber(847, TargetStrategy.Value);
			if (TargetStrategyParameters is not null) writer.WriteString(848, TargetStrategyParameters);
			if (ParticipationRate is not null) writer.WriteNumber(849, ParticipationRate.Value);
			if (CancellationRights is not null) writer.WriteString(480, CancellationRights);
			if (MoneyLaunderingStatus is not null) writer.WriteString(481, MoneyLaunderingStatus);
			if (RegistID is not null) writer.WriteString(513, RegistID);
			if (Designation is not null) writer.WriteString(494, Designation);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
