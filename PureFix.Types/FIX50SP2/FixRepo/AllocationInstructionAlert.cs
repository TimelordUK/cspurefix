using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("AllocationInstructionAlert", FixVersion.FIX50SP2)]
	public sealed partial class AllocationInstructionAlert : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 1, Required = true)]
		public string? AllocID {get; set;}
		
		[TagDetails(Tag = 71, Type = TagType.String, Offset = 2, Required = true)]
		public string? AllocTransType {get; set;}
		
		[TagDetails(Tag = 626, Type = TagType.Int, Offset = 3, Required = true)]
		public int? AllocType {get; set;}
		
		[TagDetails(Tag = 793, Type = TagType.String, Offset = 4, Required = false)]
		public string? SecondaryAllocID {get; set;}
		
		[TagDetails(Tag = 72, Type = TagType.String, Offset = 5, Required = false)]
		public string? RefAllocID {get; set;}
		
		[TagDetails(Tag = 796, Type = TagType.Int, Offset = 6, Required = false)]
		public int? AllocCancReplaceReason {get; set;}
		
		[TagDetails(Tag = 808, Type = TagType.Int, Offset = 7, Required = false)]
		public int? AllocIntermedReqType {get; set;}
		
		[TagDetails(Tag = 196, Type = TagType.String, Offset = 8, Required = false)]
		public string? AllocLinkID {get; set;}
		
		[TagDetails(Tag = 197, Type = TagType.Int, Offset = 9, Required = false)]
		public int? AllocLinkType {get; set;}
		
		[TagDetails(Tag = 466, Type = TagType.String, Offset = 10, Required = false)]
		public string? BookingRefID {get; set;}
		
		[TagDetails(Tag = 857, Type = TagType.Int, Offset = 11, Required = false)]
		public int? AllocNoOrdersType {get; set;}
		
		[TagDetails(Tag = 570, Type = TagType.Boolean, Offset = 12, Required = false)]
		public bool? PreviouslyReported {get; set;}
		
		[TagDetails(Tag = 700, Type = TagType.Boolean, Offset = 13, Required = false)]
		public bool? ReversalIndicator {get; set;}
		
		[TagDetails(Tag = 574, Type = TagType.String, Offset = 14, Required = false)]
		public string? MatchType {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 15, Required = true)]
		public string? Side {get; set;}
		
		[Component(Offset = 16, Required = true)]
		public InstrumentComponent? Instrument {get; set;}
		
		[Component(Offset = 17, Required = false)]
		public InstrumentExtensionComponent? InstrumentExtension {get; set;}
		
		[Component(Offset = 18, Required = false)]
		public FinancingDetailsComponent? FinancingDetails {get; set;}
		
		[Component(Offset = 19, Required = false)]
		public UndInstrmtGrpComponent? UndInstrmtGrp {get; set;}
		
		[Component(Offset = 20, Required = false)]
		public InstrmtLegGrpComponent? InstrmtLegGrp {get; set;}
		
		[TagDetails(Tag = 53, Type = TagType.Float, Offset = 21, Required = true)]
		public double? Quantity {get; set;}
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 22, Required = false)]
		public int? QtyType {get; set;}
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 23, Required = false)]
		public string? LastMkt {get; set;}
		
		[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 24, Required = false)]
		public DateOnly? TradeOriginationDate {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 25, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 26, Required = false)]
		public string? TradingSessionSubID {get; set;}
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 27, Required = false)]
		public int? PriceType {get; set;}
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 28, Required = false)]
		public double? AvgPx {get; set;}
		
		[TagDetails(Tag = 860, Type = TagType.Float, Offset = 29, Required = false)]
		public double? AvgParPx {get; set;}
		
		[Component(Offset = 30, Required = false)]
		public SpreadOrBenchmarkCurveDataComponent? SpreadOrBenchmarkCurveData {get; set;}
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 31, Required = false)]
		public string? Currency {get; set;}
		
		[TagDetails(Tag = 74, Type = TagType.Int, Offset = 32, Required = false)]
		public int? AvgPxPrecision {get; set;}
		
		[Group(NoOfTag = 1012, Offset = 33, Required = false)]
		public AllocationInstructionAlertParties[]? Parties {get; set;}
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 34, Required = true)]
		public DateOnly? TradeDate {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 35, Required = false)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 36, Required = false)]
		public string? SettlType {get; set;}
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 37, Required = false)]
		public DateOnly? SettlDate {get; set;}
		
		[TagDetails(Tag = 775, Type = TagType.Int, Offset = 38, Required = false)]
		public int? BookingType {get; set;}
		
		[TagDetails(Tag = 381, Type = TagType.Float, Offset = 39, Required = false)]
		public double? GrossTradeAmt {get; set;}
		
		[TagDetails(Tag = 238, Type = TagType.Float, Offset = 40, Required = false)]
		public double? Concession {get; set;}
		
		[TagDetails(Tag = 237, Type = TagType.Float, Offset = 41, Required = false)]
		public double? TotalTakedown {get; set;}
		
		[TagDetails(Tag = 118, Type = TagType.Float, Offset = 42, Required = false)]
		public double? NetMoney {get; set;}
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 43, Required = false)]
		public string? PositionEffect {get; set;}
		
		[TagDetails(Tag = 754, Type = TagType.Boolean, Offset = 44, Required = false)]
		public bool? AutoAcceptIndicator {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 45, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 46, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 47, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[TagDetails(Tag = 157, Type = TagType.Int, Offset = 48, Required = false)]
		public int? NumDaysInterest {get; set;}
		
		[TagDetails(Tag = 158, Type = TagType.Float, Offset = 49, Required = false)]
		public double? AccruedInterestRate {get; set;}
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 50, Required = false)]
		public double? AccruedInterestAmt {get; set;}
		
		[TagDetails(Tag = 540, Type = TagType.Float, Offset = 51, Required = false)]
		public double? TotalAccruedInterestAmt {get; set;}
		
		[TagDetails(Tag = 738, Type = TagType.Float, Offset = 52, Required = false)]
		public double? InterestAtMaturity {get; set;}
		
		[TagDetails(Tag = 920, Type = TagType.Float, Offset = 53, Required = false)]
		public double? EndAccruedInterestAmt {get; set;}
		
		[TagDetails(Tag = 921, Type = TagType.Float, Offset = 54, Required = false)]
		public double? StartCash {get; set;}
		
		[TagDetails(Tag = 922, Type = TagType.Float, Offset = 55, Required = false)]
		public double? EndCash {get; set;}
		
		[TagDetails(Tag = 650, Type = TagType.Boolean, Offset = 56, Required = false)]
		public bool? LegalConfirm {get; set;}
		
		[Group(NoOfTag = 1019, Offset = 57, Required = false)]
		public AllocationInstructionAlertStipulations[]? Stipulations {get; set;}
		
		[Component(Offset = 58, Required = false)]
		public YieldDataComponent? YieldData {get; set;}
		
		[Group(NoOfTag = 1014, Offset = 59, Required = false)]
		public AllocationInstructionAlertPositionAmountData[]? PositionAmountData {get; set;}
		
		[TagDetails(Tag = 892, Type = TagType.Int, Offset = 60, Required = false)]
		public int? TotNoAllocs {get; set;}
		
		[TagDetails(Tag = 893, Type = TagType.Boolean, Offset = 61, Required = false)]
		public bool? LastFragment {get; set;}
		
		[TagDetails(Tag = 819, Type = TagType.Int, Offset = 62, Required = false)]
		public int? AvgPxIndicator {get; set;}
		
		[TagDetails(Tag = 715, Type = TagType.LocalDate, Offset = 63, Required = false)]
		public DateOnly? ClearingBusinessDate {get; set;}
		
		[TagDetails(Tag = 828, Type = TagType.Int, Offset = 64, Required = false)]
		public int? TrdType {get; set;}
		
		[TagDetails(Tag = 829, Type = TagType.Int, Offset = 65, Required = false)]
		public int? TrdSubType {get; set;}
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 66, Required = false)]
		public int? CustOrderCapacity {get; set;}
		
		[TagDetails(Tag = 578, Type = TagType.String, Offset = 67, Required = false)]
		public string? TradeInputSource {get; set;}
		
		[TagDetails(Tag = 442, Type = TagType.String, Offset = 68, Required = false)]
		public string? MultiLegReportingType {get; set;}
		
		[TagDetails(Tag = 1011, Type = TagType.String, Offset = 69, Required = false)]
		public string? MessageEventSource {get; set;}
		
		[TagDetails(Tag = 991, Type = TagType.Float, Offset = 70, Required = false)]
		public double? RndPx {get; set;}
		
		[Component(Offset = 71, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& AllocID is not null
				&& AllocTransType is not null
				&& AllocType is not null
				&& Side is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& Quantity is not null
				&& TradeDate is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (AllocID is not null) writer.WriteString(70, AllocID);
			if (AllocTransType is not null) writer.WriteString(71, AllocTransType);
			if (AllocType is not null) writer.WriteWholeNumber(626, AllocType.Value);
			if (SecondaryAllocID is not null) writer.WriteString(793, SecondaryAllocID);
			if (RefAllocID is not null) writer.WriteString(72, RefAllocID);
			if (AllocCancReplaceReason is not null) writer.WriteWholeNumber(796, AllocCancReplaceReason.Value);
			if (AllocIntermedReqType is not null) writer.WriteWholeNumber(808, AllocIntermedReqType.Value);
			if (AllocLinkID is not null) writer.WriteString(196, AllocLinkID);
			if (AllocLinkType is not null) writer.WriteWholeNumber(197, AllocLinkType.Value);
			if (BookingRefID is not null) writer.WriteString(466, BookingRefID);
			if (AllocNoOrdersType is not null) writer.WriteWholeNumber(857, AllocNoOrdersType.Value);
			if (PreviouslyReported is not null) writer.WriteBoolean(570, PreviouslyReported.Value);
			if (ReversalIndicator is not null) writer.WriteBoolean(700, ReversalIndicator.Value);
			if (MatchType is not null) writer.WriteString(574, MatchType);
			if (Side is not null) writer.WriteString(54, Side);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (InstrumentExtension is not null) ((IFixEncoder)InstrumentExtension).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (Quantity is not null) writer.WriteNumber(53, Quantity.Value);
			if (QtyType is not null) writer.WriteWholeNumber(854, QtyType.Value);
			if (LastMkt is not null) writer.WriteString(30, LastMkt);
			if (TradeOriginationDate is not null) writer.WriteLocalDateOnly(229, TradeOriginationDate.Value);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (AvgPx is not null) writer.WriteNumber(6, AvgPx.Value);
			if (AvgParPx is not null) writer.WriteNumber(860, AvgParPx.Value);
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (AvgPxPrecision is not null) writer.WriteWholeNumber(74, AvgPxPrecision.Value);
			if (Parties is not null && Parties.Length != 0)
			{
				writer.WriteWholeNumber(1012, Parties.Length);
				for (int i = 0; i < Parties.Length; i++)
				{
					((IFixEncoder)Parties[i]).Encode(writer);
				}
			}
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (SettlType is not null) writer.WriteString(63, SettlType);
			if (SettlDate is not null) writer.WriteLocalDateOnly(64, SettlDate.Value);
			if (BookingType is not null) writer.WriteWholeNumber(775, BookingType.Value);
			if (GrossTradeAmt is not null) writer.WriteNumber(381, GrossTradeAmt.Value);
			if (Concession is not null) writer.WriteNumber(238, Concession.Value);
			if (TotalTakedown is not null) writer.WriteNumber(237, TotalTakedown.Value);
			if (NetMoney is not null) writer.WriteNumber(118, NetMoney.Value);
			if (PositionEffect is not null) writer.WriteString(77, PositionEffect);
			if (AutoAcceptIndicator is not null) writer.WriteBoolean(754, AutoAcceptIndicator.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (NumDaysInterest is not null) writer.WriteWholeNumber(157, NumDaysInterest.Value);
			if (AccruedInterestRate is not null) writer.WriteNumber(158, AccruedInterestRate.Value);
			if (AccruedInterestAmt is not null) writer.WriteNumber(159, AccruedInterestAmt.Value);
			if (TotalAccruedInterestAmt is not null) writer.WriteNumber(540, TotalAccruedInterestAmt.Value);
			if (InterestAtMaturity is not null) writer.WriteNumber(738, InterestAtMaturity.Value);
			if (EndAccruedInterestAmt is not null) writer.WriteNumber(920, EndAccruedInterestAmt.Value);
			if (StartCash is not null) writer.WriteNumber(921, StartCash.Value);
			if (EndCash is not null) writer.WriteNumber(922, EndCash.Value);
			if (LegalConfirm is not null) writer.WriteBoolean(650, LegalConfirm.Value);
			if (Stipulations is not null && Stipulations.Length != 0)
			{
				writer.WriteWholeNumber(1019, Stipulations.Length);
				for (int i = 0; i < Stipulations.Length; i++)
				{
					((IFixEncoder)Stipulations[i]).Encode(writer);
				}
			}
			if (YieldData is not null) ((IFixEncoder)YieldData).Encode(writer);
			if (PositionAmountData is not null && PositionAmountData.Length != 0)
			{
				writer.WriteWholeNumber(1014, PositionAmountData.Length);
				for (int i = 0; i < PositionAmountData.Length; i++)
				{
					((IFixEncoder)PositionAmountData[i]).Encode(writer);
				}
			}
			if (TotNoAllocs is not null) writer.WriteWholeNumber(892, TotNoAllocs.Value);
			if (LastFragment is not null) writer.WriteBoolean(893, LastFragment.Value);
			if (AvgPxIndicator is not null) writer.WriteWholeNumber(819, AvgPxIndicator.Value);
			if (ClearingBusinessDate is not null) writer.WriteLocalDateOnly(715, ClearingBusinessDate.Value);
			if (TrdType is not null) writer.WriteWholeNumber(828, TrdType.Value);
			if (TrdSubType is not null) writer.WriteWholeNumber(829, TrdSubType.Value);
			if (CustOrderCapacity is not null) writer.WriteWholeNumber(582, CustOrderCapacity.Value);
			if (TradeInputSource is not null) writer.WriteString(578, TradeInputSource);
			if (MultiLegReportingType is not null) writer.WriteString(442, MultiLegReportingType);
			if (MessageEventSource is not null) writer.WriteString(1011, MessageEventSource);
			if (RndPx is not null) writer.WriteNumber(991, RndPx.Value);
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
			AllocID = view.GetString(70);
			AllocTransType = view.GetString(71);
			AllocType = view.GetInt32(626);
			SecondaryAllocID = view.GetString(793);
			RefAllocID = view.GetString(72);
			AllocCancReplaceReason = view.GetInt32(796);
			AllocIntermedReqType = view.GetInt32(808);
			AllocLinkID = view.GetString(196);
			AllocLinkType = view.GetInt32(197);
			BookingRefID = view.GetString(466);
			AllocNoOrdersType = view.GetInt32(857);
			PreviouslyReported = view.GetBool(570);
			ReversalIndicator = view.GetBool(700);
			MatchType = view.GetString(574);
			Side = view.GetString(54);
			if (view.GetView("Instrument") is IMessageView viewInstrument)
			{
				Instrument = new();
				((IFixParser)Instrument).Parse(viewInstrument);
			}
			if (view.GetView("InstrumentExtension") is IMessageView viewInstrumentExtension)
			{
				InstrumentExtension = new();
				((IFixParser)InstrumentExtension).Parse(viewInstrumentExtension);
			}
			if (view.GetView("FinancingDetails") is IMessageView viewFinancingDetails)
			{
				FinancingDetails = new();
				((IFixParser)FinancingDetails).Parse(viewFinancingDetails);
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
			Quantity = view.GetDouble(53);
			QtyType = view.GetInt32(854);
			LastMkt = view.GetString(30);
			TradeOriginationDate = view.GetDateOnly(229);
			TradingSessionID = view.GetString(336);
			TradingSessionSubID = view.GetString(625);
			PriceType = view.GetInt32(423);
			AvgPx = view.GetDouble(6);
			AvgParPx = view.GetDouble(860);
			if (view.GetView("SpreadOrBenchmarkCurveData") is IMessageView viewSpreadOrBenchmarkCurveData)
			{
				SpreadOrBenchmarkCurveData = new();
				((IFixParser)SpreadOrBenchmarkCurveData).Parse(viewSpreadOrBenchmarkCurveData);
			}
			Currency = view.GetString(15);
			AvgPxPrecision = view.GetInt32(74);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				var count = viewParties.GroupCount();
				Parties = new AllocationInstructionAlertParties[count];
				for (int i = 0; i < count; i++)
				{
					Parties[i] = new();
					((IFixParser)Parties[i]).Parse(viewParties.GetGroupInstance(i));
				}
			}
			TradeDate = view.GetDateOnly(75);
			TransactTime = view.GetDateTime(60);
			SettlType = view.GetString(63);
			SettlDate = view.GetDateOnly(64);
			BookingType = view.GetInt32(775);
			GrossTradeAmt = view.GetDouble(381);
			Concession = view.GetDouble(238);
			TotalTakedown = view.GetDouble(237);
			NetMoney = view.GetDouble(118);
			PositionEffect = view.GetString(77);
			AutoAcceptIndicator = view.GetBool(754);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			NumDaysInterest = view.GetInt32(157);
			AccruedInterestRate = view.GetDouble(158);
			AccruedInterestAmt = view.GetDouble(159);
			TotalAccruedInterestAmt = view.GetDouble(540);
			InterestAtMaturity = view.GetDouble(738);
			EndAccruedInterestAmt = view.GetDouble(920);
			StartCash = view.GetDouble(921);
			EndCash = view.GetDouble(922);
			LegalConfirm = view.GetBool(650);
			if (view.GetView("Stipulations") is IMessageView viewStipulations)
			{
				var count = viewStipulations.GroupCount();
				Stipulations = new AllocationInstructionAlertStipulations[count];
				for (int i = 0; i < count; i++)
				{
					Stipulations[i] = new();
					((IFixParser)Stipulations[i]).Parse(viewStipulations.GetGroupInstance(i));
				}
			}
			if (view.GetView("YieldData") is IMessageView viewYieldData)
			{
				YieldData = new();
				((IFixParser)YieldData).Parse(viewYieldData);
			}
			if (view.GetView("PositionAmountData") is IMessageView viewPositionAmountData)
			{
				var count = viewPositionAmountData.GroupCount();
				PositionAmountData = new AllocationInstructionAlertPositionAmountData[count];
				for (int i = 0; i < count; i++)
				{
					PositionAmountData[i] = new();
					((IFixParser)PositionAmountData[i]).Parse(viewPositionAmountData.GetGroupInstance(i));
				}
			}
			TotNoAllocs = view.GetInt32(892);
			LastFragment = view.GetBool(893);
			AvgPxIndicator = view.GetInt32(819);
			ClearingBusinessDate = view.GetDateOnly(715);
			TrdType = view.GetInt32(828);
			TrdSubType = view.GetInt32(829);
			CustOrderCapacity = view.GetInt32(582);
			TradeInputSource = view.GetString(578);
			MultiLegReportingType = view.GetString(442);
			MessageEventSource = view.GetString(1011);
			RndPx = view.GetDouble(991);
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
					value = StandardHeader;
					break;
				case "AllocID":
					value = AllocID;
					break;
				case "AllocTransType":
					value = AllocTransType;
					break;
				case "AllocType":
					value = AllocType;
					break;
				case "SecondaryAllocID":
					value = SecondaryAllocID;
					break;
				case "RefAllocID":
					value = RefAllocID;
					break;
				case "AllocCancReplaceReason":
					value = AllocCancReplaceReason;
					break;
				case "AllocIntermedReqType":
					value = AllocIntermedReqType;
					break;
				case "AllocLinkID":
					value = AllocLinkID;
					break;
				case "AllocLinkType":
					value = AllocLinkType;
					break;
				case "BookingRefID":
					value = BookingRefID;
					break;
				case "AllocNoOrdersType":
					value = AllocNoOrdersType;
					break;
				case "PreviouslyReported":
					value = PreviouslyReported;
					break;
				case "ReversalIndicator":
					value = ReversalIndicator;
					break;
				case "MatchType":
					value = MatchType;
					break;
				case "Side":
					value = Side;
					break;
				case "Instrument":
					value = Instrument;
					break;
				case "InstrumentExtension":
					value = InstrumentExtension;
					break;
				case "FinancingDetails":
					value = FinancingDetails;
					break;
				case "UndInstrmtGrp":
					value = UndInstrmtGrp;
					break;
				case "InstrmtLegGrp":
					value = InstrmtLegGrp;
					break;
				case "Quantity":
					value = Quantity;
					break;
				case "QtyType":
					value = QtyType;
					break;
				case "LastMkt":
					value = LastMkt;
					break;
				case "TradeOriginationDate":
					value = TradeOriginationDate;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "TradingSessionSubID":
					value = TradingSessionSubID;
					break;
				case "PriceType":
					value = PriceType;
					break;
				case "AvgPx":
					value = AvgPx;
					break;
				case "AvgParPx":
					value = AvgParPx;
					break;
				case "SpreadOrBenchmarkCurveData":
					value = SpreadOrBenchmarkCurveData;
					break;
				case "Currency":
					value = Currency;
					break;
				case "AvgPxPrecision":
					value = AvgPxPrecision;
					break;
				case "Parties":
					value = Parties;
					break;
				case "TradeDate":
					value = TradeDate;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "SettlType":
					value = SettlType;
					break;
				case "SettlDate":
					value = SettlDate;
					break;
				case "BookingType":
					value = BookingType;
					break;
				case "GrossTradeAmt":
					value = GrossTradeAmt;
					break;
				case "Concession":
					value = Concession;
					break;
				case "TotalTakedown":
					value = TotalTakedown;
					break;
				case "NetMoney":
					value = NetMoney;
					break;
				case "PositionEffect":
					value = PositionEffect;
					break;
				case "AutoAcceptIndicator":
					value = AutoAcceptIndicator;
					break;
				case "Text":
					value = Text;
					break;
				case "EncodedTextLen":
					value = EncodedTextLen;
					break;
				case "EncodedText":
					value = EncodedText;
					break;
				case "NumDaysInterest":
					value = NumDaysInterest;
					break;
				case "AccruedInterestRate":
					value = AccruedInterestRate;
					break;
				case "AccruedInterestAmt":
					value = AccruedInterestAmt;
					break;
				case "TotalAccruedInterestAmt":
					value = TotalAccruedInterestAmt;
					break;
				case "InterestAtMaturity":
					value = InterestAtMaturity;
					break;
				case "EndAccruedInterestAmt":
					value = EndAccruedInterestAmt;
					break;
				case "StartCash":
					value = StartCash;
					break;
				case "EndCash":
					value = EndCash;
					break;
				case "LegalConfirm":
					value = LegalConfirm;
					break;
				case "Stipulations":
					value = Stipulations;
					break;
				case "YieldData":
					value = YieldData;
					break;
				case "PositionAmountData":
					value = PositionAmountData;
					break;
				case "TotNoAllocs":
					value = TotNoAllocs;
					break;
				case "LastFragment":
					value = LastFragment;
					break;
				case "AvgPxIndicator":
					value = AvgPxIndicator;
					break;
				case "ClearingBusinessDate":
					value = ClearingBusinessDate;
					break;
				case "TrdType":
					value = TrdType;
					break;
				case "TrdSubType":
					value = TrdSubType;
					break;
				case "CustOrderCapacity":
					value = CustOrderCapacity;
					break;
				case "TradeInputSource":
					value = TradeInputSource;
					break;
				case "MultiLegReportingType":
					value = MultiLegReportingType;
					break;
				case "MessageEventSource":
					value = MessageEventSource;
					break;
				case "RndPx":
					value = RndPx;
					break;
				case "StandardTrailer":
					value = StandardTrailer;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			AllocID = null;
			AllocTransType = null;
			AllocType = null;
			SecondaryAllocID = null;
			RefAllocID = null;
			AllocCancReplaceReason = null;
			AllocIntermedReqType = null;
			AllocLinkID = null;
			AllocLinkType = null;
			BookingRefID = null;
			AllocNoOrdersType = null;
			PreviouslyReported = null;
			ReversalIndicator = null;
			MatchType = null;
			Side = null;
			((IFixReset?)Instrument)?.Reset();
			((IFixReset?)InstrumentExtension)?.Reset();
			((IFixReset?)FinancingDetails)?.Reset();
			((IFixReset?)UndInstrmtGrp)?.Reset();
			((IFixReset?)InstrmtLegGrp)?.Reset();
			Quantity = null;
			QtyType = null;
			LastMkt = null;
			TradeOriginationDate = null;
			TradingSessionID = null;
			TradingSessionSubID = null;
			PriceType = null;
			AvgPx = null;
			AvgParPx = null;
			((IFixReset?)SpreadOrBenchmarkCurveData)?.Reset();
			Currency = null;
			AvgPxPrecision = null;
			Parties = null;
			TradeDate = null;
			TransactTime = null;
			SettlType = null;
			SettlDate = null;
			BookingType = null;
			GrossTradeAmt = null;
			Concession = null;
			TotalTakedown = null;
			NetMoney = null;
			PositionEffect = null;
			AutoAcceptIndicator = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			NumDaysInterest = null;
			AccruedInterestRate = null;
			AccruedInterestAmt = null;
			TotalAccruedInterestAmt = null;
			InterestAtMaturity = null;
			EndAccruedInterestAmt = null;
			StartCash = null;
			EndCash = null;
			LegalConfirm = null;
			Stipulations = null;
			((IFixReset?)YieldData)?.Reset();
			PositionAmountData = null;
			TotNoAllocs = null;
			LastFragment = null;
			AvgPxIndicator = null;
			ClearingBusinessDate = null;
			TrdType = null;
			TrdSubType = null;
			CustOrderCapacity = null;
			TradeInputSource = null;
			MultiLegReportingType = null;
			MessageEventSource = null;
			RndPx = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
