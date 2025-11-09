using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44.Components;

namespace PureFix.Types.FIX44
{
	[MessageType("s", FixVersion.FIX44)]
	public sealed partial class NewOrderCross : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 548, Type = TagType.String, Offset = 1, Required = true)]
		public string? CrossID {get; set;}
		
		[TagDetails(Tag = 549, Type = TagType.Int, Offset = 2, Required = true)]
		public int? CrossType {get; set;}
		
		[TagDetails(Tag = 550, Type = TagType.Int, Offset = 3, Required = true)]
		public int? CrossPrioritization {get; set;}
		
		[Component(Offset = 4, Required = true)]
		public SideCrossOrdModGrp? SideCrossOrdModGrp {get; set;}
		
		[Component(Offset = 5, Required = true)]
		public Instrument? Instrument {get; set;}
		
		[Component(Offset = 6, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp {get; set;}
		
		[Component(Offset = 7, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp {get; set;}
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 8, Required = false)]
		public string? SettlType {get; set;}
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 9, Required = false)]
		public DateOnly? SettlDate {get; set;}
		
		[TagDetails(Tag = 21, Type = TagType.String, Offset = 10, Required = false)]
		public string? HandlInst {get; set;}
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 11, Required = false)]
		public string? ExecInst {get; set;}
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 12, Required = false)]
		public double? MinQty {get; set;}
		
		[TagDetails(Tag = 111, Type = TagType.Float, Offset = 13, Required = false)]
		public double? MaxFloor {get; set;}
		
		[TagDetails(Tag = 100, Type = TagType.String, Offset = 14, Required = false)]
		public string? ExDestination {get; set;}
		
		[Component(Offset = 15, Required = false)]
		public TrdgSesGrp? TrdgSesGrp {get; set;}
		
		[TagDetails(Tag = 81, Type = TagType.String, Offset = 16, Required = false)]
		public string? ProcessCode {get; set;}
		
		[TagDetails(Tag = 140, Type = TagType.Float, Offset = 17, Required = false)]
		public double? PrevClosePx {get; set;}
		
		[TagDetails(Tag = 114, Type = TagType.Boolean, Offset = 18, Required = false)]
		public bool? LocateReqd {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 19, Required = true)]
		public DateTime? TransactTime {get; set;}
		
		[Component(Offset = 20, Required = false)]
		public Stipulations? Stipulations {get; set;}
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 21, Required = true)]
		public string? OrdType {get; set;}
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 22, Required = false)]
		public int? PriceType {get; set;}
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 23, Required = false)]
		public double? Price {get; set;}
		
		[TagDetails(Tag = 99, Type = TagType.Float, Offset = 24, Required = false)]
		public double? StopPx {get; set;}
		
		[Component(Offset = 25, Required = false)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData {get; set;}
		
		[Component(Offset = 26, Required = false)]
		public YieldData? YieldData {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 27, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 28, Required = false)]
		public string? ComplianceID {get; set;}
		
		[TagDetails(Tag = 23, Type = TagType.String, Offset = 29, Required = false)]
		public string? IOIID {get; set;}
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 30, Required = false)]
		public string? QuoteID {get; set;}
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 31, Required = false)]
		public string? TimeInForce {get; set;}
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 32, Required = false)]
		public DateTime? EffectiveTime {get; set;}
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 33, Required = false)]
		public DateOnly? ExpireDate {get; set;}
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 34, Required = false)]
		public DateTime? ExpireTime {get; set;}
		
		[TagDetails(Tag = 427, Type = TagType.Int, Offset = 35, Required = false)]
		public int? GTBookingInst {get; set;}
		
		[TagDetails(Tag = 210, Type = TagType.Float, Offset = 36, Required = false)]
		public double? MaxShow {get; set;}
		
		[Component(Offset = 37, Required = false)]
		public PegInstructions? PegInstructions {get; set;}
		
		[Component(Offset = 38, Required = false)]
		public DiscretionInstructions? DiscretionInstructions {get; set;}
		
		[TagDetails(Tag = 847, Type = TagType.Int, Offset = 39, Required = false)]
		public int? TargetStrategy {get; set;}
		
		[TagDetails(Tag = 848, Type = TagType.String, Offset = 40, Required = false)]
		public string? TargetStrategyParameters {get; set;}
		
		[TagDetails(Tag = 849, Type = TagType.Float, Offset = 41, Required = false)]
		public double? ParticipationRate {get; set;}
		
		[TagDetails(Tag = 480, Type = TagType.String, Offset = 42, Required = false)]
		public string? CancellationRights {get; set;}
		
		[TagDetails(Tag = 481, Type = TagType.String, Offset = 43, Required = false)]
		public string? MoneyLaunderingStatus {get; set;}
		
		[TagDetails(Tag = 513, Type = TagType.String, Offset = 44, Required = false)]
		public string? RegistID {get; set;}
		
		[TagDetails(Tag = 494, Type = TagType.String, Offset = 45, Required = false)]
		public string? Designation {get; set;}
		
		[Component(Offset = 46, Required = true)]
		public StandardTrailer? StandardTrailer {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return (!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config))) && (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (CrossID is not null) writer.WriteString(548, CrossID);
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
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is IMessageView viewStandardHeader)
			{
				StandardHeader = new();
				((IFixParser)StandardHeader).Parse(viewStandardHeader);
			}
			CrossID = view.GetString(548);
			CrossType = view.GetInt32(549);
			CrossPrioritization = view.GetInt32(550);
			if (view.GetView("SideCrossOrdModGrp") is IMessageView viewSideCrossOrdModGrp)
			{
				SideCrossOrdModGrp = new();
				((IFixParser)SideCrossOrdModGrp).Parse(viewSideCrossOrdModGrp);
			}
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			if (view.GetView("UndInstrmtGrp") is IMessageView viewUndInstrmtGrp)
			{
				UndInstrmtGrp = new();
				((IFixParser)UndInstrmtGrp).Parse(viewUndInstrmtGrp);
			}
			if (view.GetView("InstrmtLegGrp") is IMessageView viewInstrmtLegGrp)
			{
				InstrmtLegGrp = new();
				((IFixParser)InstrmtLegGrp).Parse(viewInstrmtLegGrp);
			}
			SettlType = view.GetString(63);
			SettlDate = view.GetDateOnly(64);
			HandlInst = view.GetString(21);
			ExecInst = view.GetString(18);
			MinQty = view.GetDouble(110);
			MaxFloor = view.GetDouble(111);
			ExDestination = view.GetString(100);
			if (view.GetView("TrdgSesGrp") is IMessageView viewTrdgSesGrp)
			{
				TrdgSesGrp = new();
				((IFixParser)TrdgSesGrp).Parse(viewTrdgSesGrp);
			}
			ProcessCode = view.GetString(81);
			PrevClosePx = view.GetDouble(140);
			LocateReqd = view.GetBool(114);
			TransactTime = view.GetDateTime(60);
			if (view.GetView("Stipulations") is IMessageView viewStipulations)
			{
				Stipulations = new();
				((IFixParser)Stipulations).Parse(viewStipulations);
			}
			OrdType = view.GetString(40);
			PriceType = view.GetInt32(423);
			Price = view.GetDouble(44);
			StopPx = view.GetDouble(99);
			if (view.GetView("SpreadOrBenchmarkCurveData") is IMessageView viewSpreadOrBenchmarkCurveData)
			{
				SpreadOrBenchmarkCurveData = new();
				((IFixParser)SpreadOrBenchmarkCurveData).Parse(viewSpreadOrBenchmarkCurveData);
			}
			if (view.GetView("YieldData") is IMessageView viewYieldData)
			{
				YieldData = new();
				((IFixParser)YieldData).Parse(viewYieldData);
			}
			Currency = view.GetString(15);
			ComplianceID = view.GetString(376);
			IOIID = view.GetString(23);
			QuoteID = view.GetString(117);
			TimeInForce = view.GetString(59);
			EffectiveTime = view.GetDateTime(168);
			ExpireDate = view.GetDateOnly(432);
			ExpireTime = view.GetDateTime(126);
			GTBookingInst = view.GetInt32(427);
			MaxShow = view.GetDouble(210);
			if (view.GetView("PegInstructions") is IMessageView viewPegInstructions)
			{
				PegInstructions = new();
				((IFixParser)PegInstructions).Parse(viewPegInstructions);
			}
			if (view.GetView("DiscretionInstructions") is IMessageView viewDiscretionInstructions)
			{
				DiscretionInstructions = new();
				((IFixParser)DiscretionInstructions).Parse(viewDiscretionInstructions);
			}
			TargetStrategy = view.GetInt32(847);
			TargetStrategyParameters = view.GetString(848);
			ParticipationRate = view.GetDouble(849);
			CancellationRights = view.GetString(480);
			MoneyLaunderingStatus = view.GetString(481);
			RegistID = view.GetString(513);
			Designation = view.GetString(494);
			if (view.GetView("StandardTrailer") is IMessageView viewStandardTrailer)
			{
				StandardTrailer = new();
				((IFixParser)StandardTrailer).Parse(viewStandardTrailer);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "StandardHeader":
				{
					value = StandardHeader;
					break;
				}
				case "CrossID":
				{
					value = CrossID;
					break;
				}
				case "CrossType":
				{
					value = CrossType;
					break;
				}
				case "CrossPrioritization":
				{
					value = CrossPrioritization;
					break;
				}
				case "SideCrossOrdModGrp":
				{
					value = SideCrossOrdModGrp;
					break;
				}
				case "Instrument":
				{
					value = Instrument;
					break;
				}
				case "UndInstrmtGrp":
				{
					value = UndInstrmtGrp;
					break;
				}
				case "InstrmtLegGrp":
				{
					value = InstrmtLegGrp;
					break;
				}
				case "SettlType":
				{
					value = SettlType;
					break;
				}
				case "SettlDate":
				{
					value = SettlDate;
					break;
				}
				case "HandlInst":
				{
					value = HandlInst;
					break;
				}
				case "ExecInst":
				{
					value = ExecInst;
					break;
				}
				case "MinQty":
				{
					value = MinQty;
					break;
				}
				case "MaxFloor":
				{
					value = MaxFloor;
					break;
				}
				case "ExDestination":
				{
					value = ExDestination;
					break;
				}
				case "TrdgSesGrp":
				{
					value = TrdgSesGrp;
					break;
				}
				case "ProcessCode":
				{
					value = ProcessCode;
					break;
				}
				case "PrevClosePx":
				{
					value = PrevClosePx;
					break;
				}
				case "LocateReqd":
				{
					value = LocateReqd;
					break;
				}
				case "TransactTime":
				{
					value = TransactTime;
					break;
				}
				case "Stipulations":
				{
					value = Stipulations;
					break;
				}
				case "OrdType":
				{
					value = OrdType;
					break;
				}
				case "PriceType":
				{
					value = PriceType;
					break;
				}
				case "Price":
				{
					value = Price;
					break;
				}
				case "StopPx":
				{
					value = StopPx;
					break;
				}
				case "SpreadOrBenchmarkCurveData":
				{
					value = SpreadOrBenchmarkCurveData;
					break;
				}
				case "YieldData":
				{
					value = YieldData;
					break;
				}
				case "Currency":
				{
					value = Currency;
					break;
				}
				case "ComplianceID":
				{
					value = ComplianceID;
					break;
				}
				case "IOIID":
				{
					value = IOIID;
					break;
				}
				case "QuoteID":
				{
					value = QuoteID;
					break;
				}
				case "TimeInForce":
				{
					value = TimeInForce;
					break;
				}
				case "EffectiveTime":
				{
					value = EffectiveTime;
					break;
				}
				case "ExpireDate":
				{
					value = ExpireDate;
					break;
				}
				case "ExpireTime":
				{
					value = ExpireTime;
					break;
				}
				case "GTBookingInst":
				{
					value = GTBookingInst;
					break;
				}
				case "MaxShow":
				{
					value = MaxShow;
					break;
				}
				case "PegInstructions":
				{
					value = PegInstructions;
					break;
				}
				case "DiscretionInstructions":
				{
					value = DiscretionInstructions;
					break;
				}
				case "TargetStrategy":
				{
					value = TargetStrategy;
					break;
				}
				case "TargetStrategyParameters":
				{
					value = TargetStrategyParameters;
					break;
				}
				case "ParticipationRate":
				{
					value = ParticipationRate;
					break;
				}
				case "CancellationRights":
				{
					value = CancellationRights;
					break;
				}
				case "MoneyLaunderingStatus":
				{
					value = MoneyLaunderingStatus;
					break;
				}
				case "RegistID":
				{
					value = RegistID;
					break;
				}
				case "Designation":
				{
					value = Designation;
					break;
				}
				case "StandardTrailer":
				{
					value = StandardTrailer;
					break;
				}
				default:
				{
					return false;
				}
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			CrossID = null;
			CrossType = null;
			CrossPrioritization = null;
			((IFixReset?)SideCrossOrdModGrp)?.Reset();
			((IFixReset?)Instrument)?.Reset();
			((IFixReset?)UndInstrmtGrp)?.Reset();
			((IFixReset?)InstrmtLegGrp)?.Reset();
			SettlType = null;
			SettlDate = null;
			HandlInst = null;
			ExecInst = null;
			MinQty = null;
			MaxFloor = null;
			ExDestination = null;
			((IFixReset?)TrdgSesGrp)?.Reset();
			ProcessCode = null;
			PrevClosePx = null;
			LocateReqd = null;
			TransactTime = null;
			((IFixReset?)Stipulations)?.Reset();
			OrdType = null;
			PriceType = null;
			Price = null;
			StopPx = null;
			((IFixReset?)SpreadOrBenchmarkCurveData)?.Reset();
			((IFixReset?)YieldData)?.Reset();
			Currency = null;
			ComplianceID = null;
			IOIID = null;
			QuoteID = null;
			TimeInForce = null;
			EffectiveTime = null;
			ExpireDate = null;
			ExpireTime = null;
			GTBookingInst = null;
			MaxShow = null;
			((IFixReset?)PegInstructions)?.Reset();
			((IFixReset?)DiscretionInstructions)?.Reset();
			TargetStrategy = null;
			TargetStrategyParameters = null;
			ParticipationRate = null;
			CancellationRights = null;
			MoneyLaunderingStatus = null;
			RegistID = null;
			Designation = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
